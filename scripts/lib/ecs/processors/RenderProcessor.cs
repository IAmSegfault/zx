using System;
using System.Collections.Generic;
using Godot;
using Zona.Engine;
using Zona.ECSComponent;
using Zona.ECSEntity;
using RogueSharp;

namespace Zona.ECSProcessor
{
    public class RenderProcessor : Node, Processor
    {
        public RenderProcessor()
        {

        }

        public void postInit(Ecs ecs)
        {
            LocalMap map = ecs.loadedMaps[(int)NeighbourMapDirection.center];
        }
        public void process(Ecs ecs, float delta, params dynamic[] args)
        {
            if(args[0] == "render")
            {
                if(args[1] == "local")
                {
                    LocalMap map = ecs.loadedMaps[(int)NeighbourMapDirection.center];
                    if(map.dirty)
                    {
                        TileMap gameMap = (TileMap)GetNode("/root/scene/gamespace/GameMap");
                        var tileSet = gameMap.TileSet;
                        map.dirty = false;
                        List<string> tags = new List<string>();
                        tags.Add("Camera2D");
                        tags.Add("LocalCamera");
                        tags.Add("ViewPort");
                        List<Entity> entities = ecs.getEntities(tags);
                        var camera = entities[0];
                        ViewPort viewPort = camera.getComponent<ViewPort>();
                        int w = viewPort.x + viewPort.w;
                        int h = viewPort.y + viewPort.h;
                        List<string> playerTags = new List<string>();
                        playerTags.Add("player");
                        List<Entity> playerList = ecs.getEntities(playerTags);
                        Player player = (Player)playerList[0];
                        LightRadius lightRadius = player.getComponent<LightRadius>();
                        WorldPosition position = player.getComponent<WorldPosition>();
                        map.ComputeFov(position.localMapX, position.localMapY, lightRadius.radius, true);
                        
                        foreach(Cell cell in map.GetAllCells())
                        {
                            if(cell.IsInFov && !cell.IsExplored)
                            {
                                map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                            }
                        }

                        foreach(Cell cell in map.GetAllCells())
                        {
                            int x = cell.X - viewPort.x;
                            int y = cell.Y - viewPort.y;

                            if(cell.X >= viewPort.x && cell.X < w && cell.Y >= viewPort.y && cell.Y < h)
                            {
                                Point p = new Point(cell.X, cell.Y);
                                Point p2 = new Point(cell.X, cell.Y -1);
                                Vicinity neighbour;
                                bool hasNeighbour = false;
                                if(cell.Y - 1 > 0)
                                {
                                    hasNeighbour = true;
                                    neighbour = map.vicinities[p2];
                                }
                                else
                                {
                                    neighbour = map.vicinities[p];
                                }
                                Vicinity vicinity = map.vicinities[p];
                                string tileID = vicinity.getValue();
                                string tileColor = vicinity.getColor();
                                if(hasNeighbour && neighbour.getValue() == vicinity.getValue())
                                {
                                    tileID = vicinity.getTerminalValue();
                                }


                                if(cell.IsExplored && cell.IsInFov)
                                {
                                    string tileName = tileID + "_" + tileColor;
                                    int id = tileSet.FindTileByName(tileName);
                                    gameMap.SetCell(cell.X, cell.Y, id);
                                }
                                else if(cell.IsExplored)
                                {
                                    string tileName = tileID + "_" + "2a2a3a";
                                    int id = tileSet.FindTileByName(tileName);
                                    gameMap.SetCell(cell.X, cell.Y, id);
                                    
                                }
                                else
                                {
                                    gameMap.SetCell(cell.X, cell.Y, -1);
                                }
                                
                            }
                        }
                    }
                }
            }
        }
    }
}