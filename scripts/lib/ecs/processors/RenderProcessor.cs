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
                        playerTags.Add("Player");
                        List<Entity> playerList = ecs.getEntities(playerTags);
                        Player player = (Player)playerList[0];
                        LightRadius lightRadius = player.getComponent<LightRadius>();
                        WorldPosition position = player.getComponent<WorldPosition>();
                        map.ComputeFov(position.localMapX, position.localMapY, lightRadius.radius, true);
                        
                        foreach(Cell cell in map.GetAllCells())
                        {
                            if(cell.IsInFov)
                            {
                                map.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                            }
                        }
                        //NOTE: This is a hack to get the camera to display in the top left of the screen. 
                        //Consider setting this in the camera class instead??
                        int x = 0;
                        int y = 0;
                        for(int i = viewPort.x; i < (viewPort.x + viewPort.w); i++)
                        {
                            for(int j = viewPort.y; j < (viewPort.y + viewPort.h); j++)
                            {
                                if(i < 0 || j < 0 || i > map.Width || j > map.Height)
                                {
                                    gameMap.SetCell(x, y, -1);
                                }
                                else
                                {
                                    KeyPoint p = new KeyPoint(i, j);
                                    KeyPoint p2 = new KeyPoint(i, j -1);
                                    KeyPoint p3 = new KeyPoint(i, j +1);
                                    int hc1 = p.GetHashCode();
                                    int hc2 = p2.GetHashCode();
                                    int hc3 = p3.GetHashCode();
                                    
                                    Vicinity topNeighbour;
                                    Vicinity bottomNeighbour;
                                    bool hasTopNeighbour = false;
                                    bool hasBottomNeighbour = false;
                                    if(j >= 1)
                                    {
                                        hasTopNeighbour = true;
                                        topNeighbour = map.vicinities[hc2];
                                    }
                                    else
                                    {
                                        topNeighbour = map.vicinities[hc1];
                                    }
                                    if(j <= 126)
                                    {
                                        hasBottomNeighbour = true;
                                        bottomNeighbour = map.vicinities[hc3];
                                    }
                                    else
                                    {
                                        bottomNeighbour = map.vicinities[hc1];
                                    }

                                    Vicinity vicinity = map.vicinities[hc1];
                                    string tileID = vicinity.getValue();
                                    string tileColor = vicinity.getColor();
                                    if(hasTopNeighbour && topNeighbour.getValue() == vicinity.getValue() && !hasBottomNeighbour)
                                    {   
                                        tileID = vicinity.getTerminalValue();
                                    }
                                    else if(hasTopNeighbour && topNeighbour.getValue() == vicinity.getValue() && hasBottomNeighbour && bottomNeighbour.getValue() != vicinity.getValue())
                                    {
                                        tileID = vicinity.getTerminalValue();
                                    }
                                    else if (hasTopNeighbour && hasBottomNeighbour && topNeighbour.getValue() != vicinity.getValue() && bottomNeighbour.getValue() != vicinity.getValue())
                                    {
                                        tileID = vicinity.getTerminalValue();
                                    }
                                    else
                                    {
                                        tileID = vicinity.getValue();
                                    }
                                    Cell cell = (Cell)map.GetCell(i, j);
                                    if(cell.IsExplored && cell.IsInFov)
                                    {
                                        string tileName = tileID + "_" + tileColor;
                                        int id = tileSet.FindTileByName(tileName);
                                        gameMap.SetCell(x, y, id);
                                    }
                                    else if(cell.IsExplored)
                                    {
                                        string tileName = tileID + "_" + "101024";
                                        int id = tileSet.FindTileByName(tileName);
                                        gameMap.SetCell(x, y, id);
                                        
                                    }
                                    else
                                    {
                                        gameMap.SetCell(x, y, -1);
                                    }
                                }
                                y += 1;
                            }
                            x += 1;
                            y = 0;
                        }
                    }
                }
            }
        }
    }
}