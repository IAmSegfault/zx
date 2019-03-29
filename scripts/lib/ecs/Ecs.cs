using Godot;
using GContainers = Godot.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Runtime.Remoting;
using Zona.ECSProcessor;
using Zona.ECSEntity;
using Zona.ECSComponent;
using Zona.Util;
using RogueSharp;

namespace Zona.Engine
{
    public enum NeighbourMapDirection
    {
        northwest = 0,
        north = 1,
        northeast = 2,
        west = 3,
        center = 4,
        east = 5,
        southwest = 6,
        south = 7,
        southeast = 8
    }

    [Serializable]
    public class Ecs : Node
    {
        private ulong idCtr;
        public List<Entity> entities;
        public LocalMap[] loadedMaps{get; private set;}
        private Dictionary<WorldPosition, Chunk> worldMapChunks;
        private Dictionary<MapPosition, CSVClass> staticMapStore;
        //Denotes that a given localmap should be loaded from cold storage rather than the static map store.
        private System.Collections.Generic.Dictionary<MapPosition, bool> dirtyStaticMaps;

        private WorldPosition loadedChunk;

        private List<List<Processor>> processors;

        private void preloadStaticMaps()
        {
            string dirPath = "res://data/staticmap";
            var dir = new Directory();
            dir.Open(dirPath);
            dir.ListDirBegin();
            List<string> files = new List<string>();
            while(true)
            {
                var f = dir.GetNext();
                if(f == "")
                {
                    break;
                }
                else if(f.EndsWith(".json"))
                {
                    files.Add(f);
                }
            }

            File file = new File();
            foreach(string f in files)
            {
                string filepath = dirPath + "/" + f;
                file.Open(filepath, 1);
                string text = file.GetAsText();
                file.Close();
                var jsonData = JSON.Parse(text);
                GContainers.Dictionary dataDict = jsonData.Result as GContainers.Dictionary;
                string csvFile = (string)dataDict["file"];
                CSVClass csvClass = CSVParser.parse(csvFile);
                var pos = dataDict["position"] as GContainers.Dictionary;
                
                var cPos = pos["chunk_pos"] as GContainers.Array;
                var wPos = pos["world_map_pos"] as GContainers.Array;
                int cW = Convert.ToInt32(cPos[0]);
                int cH = Convert.ToInt32(cPos[1]);
                int cL = Convert.ToInt32(cPos[2]);
                int wW = Convert.ToInt32(wPos[0]);
                int wH = Convert.ToInt32(wPos[1]);
                MapPosition position = new MapPosition(cW, cH, cL, wW, wH);
                staticMapStore.Add(position, csvClass);       
            }

            foreach(LocalMap localMap in loadedMaps)
            {
                foreach(KeyValuePair<MapPosition, CSVClass> kv in staticMapStore)
                {
                    if(localMap != null && localMap.mapPosition != null)
                    {
                        if(localMap.mapPosition.Equals(kv.Key))
                        {
                            for(int i = 0; i < kv.Value.rows; i++)
                            {
                                int x = Convert.ToInt32(kv.Value.data[i, 0]);
                                int y = Convert.ToInt32(kv.Value.data[i, 1]);
                                int t = Convert.ToInt32(kv.Value.data[i, 2]);
                                string fg = kv.Value.data[i, 3];
                                string bg = kv.Value.data[i, 4];
                                foreach(CP437Mapping mapping in CP437Mapping.mapList)
                                {
                                   if(mapping.Value == t && mapping.colorBG == bg)
                                   {
                                       localMap.setVType(x, y, mapping.mapping);
                                       localMap.dirty = true;
                                       break;
                                   } 
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void loadProcessors()
        {
            Godot.File file = new Godot.File();
            file.Open("res://data/directive/gameworldprocessors.json", 1);
            string text = file.GetAsText();
            file.Close();
            var data = JSON.Parse(text);        
            GContainers.Dictionary dataDict = data.Result as GContainers.Dictionary;
            
            GContainers.Array processors = (GContainers.Array)dataDict["processors"];
            GContainers.Array priorities = (GContainers.Array)dataDict["priorities"];
            
            var processorList = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "Zona.ECSProcessor").ToList();
            List<string> processorClassList = new List<string>();

            foreach(Type type in processorList)
            {
                if(type.Namespace == "Zona.ECSProcessor")
                {
                    processorClassList.Add(type.Name);
                }
            }

            for(int i = 0; i < processors.Count; i++)
            {
                string pname = (string)processors[i];
                if(processorClassList.Contains(pname))
                {  
                    string className = "Zona.ECSProcessor." + pname; 
                    var proccessInstance = Activator.CreateInstance(Assembly.GetExecutingAssembly().GetName().Name, className);
                    dynamic process = proccessInstance.Unwrap();
                    this.AddChild(process);
                    dynamic priority = priorities[i];
                    this.addProcessor(priority, process);
                }
            }
        }

        public void loadEntities()
        {
            Godot.File entityFile = new Godot.File();
            entityFile.Open("res://data/directive/gameworldentities.json", 1);
            string entityText = entityFile.GetAsText();
            entityFile.Close();
            Godot.JSONParseResult entityData = JSON.Parse(entityText);        
            GContainers.Dictionary entityDataDict = (GContainers.Dictionary)entityData.Result;
            GContainers.Array gEntities = (GContainers.Array)entityDataDict["entities"];
            
            GContainers.Array gArgs = (GContainers.Array)entityDataDict["args"];
            
            var entityList = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "Zona.ECSEntity").ToList();
            List<Type> loadedClasses = new List<Type>();
            foreach(object entity in gEntities)
            {
                string e = Convert.ToString(entity);
                foreach(Type classType in entityList)
                {
                    if(classType.Name == e)
                    {
                        loadedClasses.Add(classType);
                        break;
                    }
                }
            }

            for(int i = 0; i < gArgs.Count; i++)
            {
                GContainers.Array gargs = (GContainers.Array)gArgs[i];
                var c = gargs.Count;
                object[] args = new object[c];
                for(int j = 0; j < c; j++)
                {
                    args[j] = (object)gargs[j];
                }
                var entityInstance = (Entity)Activator.CreateInstance(loadedClasses[i], args);
                this.entities.Add(entityInstance);
                this.AddChild(entityInstance);
            }
        }

        public void loadNodes()
        {
            foreach(Entity entity in entities)
            {
                foreach(INode inode in entity.nodes)
                {
                    entity.AddChild((EntityNode)inode, true);
                    inode.load();
                    inode.setModulate("fff8c0");
                }
            }
        }

        private void setMapPositions()
        {
            Player player = null;
        
            foreach(Entity entity in this.entities)
            {
                if(entity.hasTag("Player"))
                {
                    player = (Player)entity;
                    break;
                }
            }
            WorldPosition wp = player.getComponent<WorldPosition>();
            MapPosition center = new MapPosition(wp.chunkX, wp.chunkY, wp.chunkZ, wp.worldX, wp.worldY);

            MapPosition nw = new MapPosition(wp.chunkX - 1, wp.chunkY, wp.chunkZ - 1, wp.worldX, wp.worldY);
            MapPosition n = new MapPosition(wp.chunkX, wp.chunkY, wp.chunkZ - 1, wp.worldX, wp.worldY);
            MapPosition ne = new MapPosition(wp.chunkX + 1, wp.chunkY, wp.chunkZ - 1, wp.worldX, wp.worldY);
            MapPosition w = new MapPosition(wp.chunkX - 1, wp.chunkY, wp.chunkZ, wp.worldX, wp.worldY);
            MapPosition e = new MapPosition(wp.chunkX + 1, wp.chunkY, wp.chunkZ, wp.worldX, wp.worldY);
            MapPosition sw = new MapPosition(wp.chunkX - 1, wp.chunkY, wp.chunkZ + 1, wp.worldX, wp.worldY);
            MapPosition s = new MapPosition(wp.chunkX, wp.chunkY, wp.chunkZ + 1, wp.worldX, wp.worldY);
            MapPosition se = new MapPosition(wp.chunkX + 1, wp.chunkY, wp.chunkZ + 1, wp.worldX, wp.worldY);

            this.loadedMaps[(int)NeighbourMapDirection.center].setMapPosition(center);
            if(wp.chunkX > 0)
            {
                this.loadedMaps[(int)NeighbourMapDirection.west].setMapPosition(w);
            }
            else
            {
                this.loadedMaps[(int)NeighbourMapDirection.west] = null;
            }
            if(wp.chunkX < 39)
            {
                this.loadedMaps[(int)NeighbourMapDirection.east].setMapPosition(e);
            }
            else
            {
                this.loadedMaps[(int)NeighbourMapDirection.east] = null;
            }
            if(wp.chunkZ > 0)
            {
                this.loadedMaps[(int)NeighbourMapDirection.north].setMapPosition(n);
            }
            else
            {
                this.loadedMaps[(int)NeighbourMapDirection.north] = null;
            }
            if(wp.chunkZ < 39)
            {
                this.loadedMaps[(int)NeighbourMapDirection.south].setMapPosition(s);
            }
            else
            {
                this.loadedMaps[(int)NeighbourMapDirection.south] = null;
            }
            if(wp.chunkX > 0 && wp.chunkZ > 0)
            {
                this.loadedMaps[(int)NeighbourMapDirection.northwest].setMapPosition(nw);
            }
            else
            {
                this.loadedMaps[(int)NeighbourMapDirection.northwest] = null;
            }
            if(wp.chunkX > 0 && wp.chunkZ < 39)
            {
                this.loadedMaps[(int)NeighbourMapDirection.southwest].setMapPosition(sw);
            }
            else
            {
                this.loadedMaps[(int)NeighbourMapDirection.southwest] = null;
            }
            if(wp.chunkX < 39 && wp.chunkZ > 0)
            {
                this.loadedMaps[(int)NeighbourMapDirection.northeast].setMapPosition(ne);
            }
            else
            {
                this.loadedMaps[(int)NeighbourMapDirection.northeast] = null;
            }
            if(wp.chunkX < 39 && wp.chunkZ < 39)
            {
                this.loadedMaps[(int)NeighbourMapDirection.northwest].setMapPosition(se);
            }
            else
            {
                this.loadedMaps[(int)NeighbourMapDirection.southeast] = null;
            }
        }
        public override void _Ready()
        {
            this.staticMapStore = new Dictionary<MapPosition, CSVClass>();
            this.idCtr = 0;
            this.loadedMaps = new LocalMap[9];
            this.entities = new List<Entity>();
            for(int i = 0; i < 9; i++)
            {
                this.loadedMaps[i] = new LocalMap(128, 128);
            }

            this.processors = new List<List<Processor>>();
            for(int i = 0; i < 16; i++)
            {
                List<Processor> row = new List<Processor>();
                this.processors.Add(row);
            }
            // Called every time the node is added to the scene.
            // Initialization here
            
            //Processor loader
            this.loadProcessors();

            //Entity Loader
            this.loadEntities();
            this.loadNodes();
            this.setMapPositions();
            this.preloadStaticMaps();
        }
        
        public void addProcessor(dynamic priority, dynamic processor)
        {
            if(priority >= 16)
            {
                priority = 15;
            }
            var row = this.processors[(int)priority];
            row.Add(processor);
        }

        public void removeProcessor(string processor)
        {
            for(int i = 0; i < 16; i++)
            {
                var row = this.processors[i];
                row.RemoveAll(x => x.GetType().Name == processor);
            }
        }

        public List<Entity> getEntities(List<string> tags)
        {
            List<Entity> entities = new List<Entity>();
            foreach(Entity entity in this.entities)
            {
                bool hasAllComponents = true;
                foreach(string tag in tags)
                {
                    if(!entity.hasTag(tag))
                    {
                        hasAllComponents = false;
                        break;
                    }
                }
                if(hasAllComponents)
                {
                    entities.Add(entity);
                }
            }
            return entities;
        }
        public override void _Process(float delta)
        {

            //The logic loop
            for(int i = 0; i < 16; i++)
            {
                List<Processor> row = this.processors[i];
                foreach(Processor processor in row)
                {

                    processor.process(this, delta, "update");
                }
            }

            //The render loop
            for(int i = 0; i < 16; i++)
            {
                List<Processor> row = this.processors[i];
                foreach(Processor processor in row)
                {
                    //TODO: Query the state of the game so we can render the local map or the world map.
                    processor.process(this, delta, "render", "local");
                }
            }  

        }

    }
}