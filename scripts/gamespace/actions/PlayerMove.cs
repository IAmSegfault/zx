using Godot;
using System;
using System.Collections.Generic;
using Zona.ECSEntity;
using Zona.ECSComponent;
using RogueSharp;
namespace Zona.Engine
{
    public class PlayerMove : Node, IAction
    {
        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";
        public void execute(params dynamic[] args)
        {
            var direction = args[0];
            Ecs ecs = (Ecs)GetNode("/root/scene/gamespace/Ecs");
            LocalMap map = ecs.loadedMaps[(int)NeighbourMapDirection.center];
            List<string> playerTag = new List<string>();
            playerTag.Add("Player");
            List<string> cameraTag = new List<string>();
            cameraTag.Add("Camera2D");
            List<Entity> playerList = ecs.getEntities(playerTag);
            List<Entity> cameraList = ecs.getEntities(cameraTag);
            Player player = (Player)playerList[0];
            Zona.ECSEntity.Camera2D camera = (Zona.ECSEntity.Camera2D)cameraList[0];
            WorldPosition p = player.getComponent<WorldPosition>();
            ViewPort viewPort = camera.getComponent<ViewPort>();
            switch(direction)
            {
                case "west":
                    if(p.localMapX > 0)
                    {
                        Cell cell = (Cell)map.GetCell(p.localMapX - 1, p.localMapY);
                        if(cell.IsWalkable)
                        {
                            p.localMapX -= 1;
                            viewPort.x -= 1;
                            map.dirty = true;
                        }
                        else
                        {
                            UiLog log = (UiLog)GetNode("/root/scene/ui/uicontainer/logpanel/UiLog");
                            KeyPoint kp = new KeyPoint(cell.X, cell.Y);
                            int hash = kp.GetHashCode();
                            string msg = "You bump into the " + map.vicinities[hash].type.DisplayName + "!";
                            log.append(msg);
                        }
                    }
                    break;
                case "east":
                    if(p.localMapX < 127)
                    {
                        Cell cell = (Cell)map.GetCell(p.localMapX + 1, p.localMapY);
                        if(cell.IsWalkable)
                        {
                            p.localMapX += 1;
                            viewPort.x += 1;
                            map.dirty = true;
                        }
                        else
                        {
                            UiLog log = (UiLog)GetNode("/root/scene/ui/uicontainer/logpanel/UiLog");
                            KeyPoint kp = new KeyPoint(cell.X, cell.Y);
                            int hash = kp.GetHashCode();
                            string msg = "You bump into the " + map.vicinities[hash].type.DisplayName + "!";
                            log.append(msg);
                        }
                    }
                    break;
                case "north":
                    if(p.localMapY > 0)
                    {
                        Cell cell = (Cell)map.GetCell(p.localMapX, p.localMapY - 1);
                        if(cell.IsWalkable)
                        {
                            p.localMapY -= 1;
                            viewPort.y -= 1;
                            map.dirty = true;
                        }
                        else
                        {
                            UiLog log = (UiLog)GetNode("/root/scene/ui/uicontainer/logpanel/UiLog");
                            KeyPoint kp = new KeyPoint(cell.X, cell.Y);
                            int hash = kp.GetHashCode();
                            string msg = "You bump into the " + map.vicinities[hash].type.DisplayName + "!";
                            log.append(msg);
                        }
                    }
                    break;
                case "south":
                    if(p.localMapY < 127)
                    {
                        Cell cell = (Cell)map.GetCell(p.localMapX, p.localMapY + 1);
                        if(cell.IsWalkable)
                        {
                            p.localMapY += 1;
                            viewPort.y += 1;
                            map.dirty = true;
                        }
                        else
                        {
                            UiLog log = (UiLog)GetNode("/root/scene/ui/uicontainer/logpanel/UiLog");
                            KeyPoint kp = new KeyPoint(cell.X, cell.Y);
                            int hash = kp.GetHashCode();
                            string msg = "You bump into the " + map.vicinities[hash].type.DisplayName + "!";
                            log.append(msg);
                        }
                    }
                    break;
                case "northwest":
                    if(p.localMapX > 0 && p.localMapY > 0)
                    {
                        Cell cell = (Cell)map.GetCell(p.localMapX - 1, p.localMapY - 1);
                        if(cell.IsWalkable)
                        {
                            p.localMapY -= 1;
                            p.localMapX -= 1;
                            viewPort.y -= 1;
                            viewPort.x -= 1;
                            map.dirty = true;
                        }
                        else
                        {
                            UiLog log = (UiLog)GetNode("/root/scene/ui/uicontainer/logpanel/UiLog");
                            KeyPoint kp = new KeyPoint(cell.X, cell.Y);
                            int hash = kp.GetHashCode();
                            string msg = "You bump into the " + map.vicinities[hash].type.DisplayName + "!";
                            log.append(msg);
                        }
                    }
                    break;
                case "northeast":
                    if(p.localMapX < 127 && p.localMapY > 0)
                    {
                        Cell cell = (Cell)map.GetCell(p.localMapX + 1, p.localMapY - 1);
                        if(cell.IsWalkable)
                        {
                            p.localMapY -= 1;
                            p.localMapX += 1;
                            viewPort.y -= 1;
                            viewPort.x += 1;
                            map.dirty = true;
                        }
                        else
                        {
                            UiLog log = (UiLog)GetNode("/root/scene/ui/uicontainer/logpanel/UiLog");
                            KeyPoint kp = new KeyPoint(cell.X, cell.Y);
                            int hash = kp.GetHashCode();
                            string msg = "You bump into the " + map.vicinities[hash].type.DisplayName + "!";
                            log.append(msg);
                        }
                    }
                    break;
                case "southwest":
                    if(p.localMapX > 0 && p.localMapY < 127)
                    {
                        Cell cell = (Cell)map.GetCell(p.localMapX + 1, p.localMapY - 1);
                        if(cell.IsWalkable)
                        {
                            p.localMapY += 1;
                            p.localMapX -= 1;
                            viewPort.y += 1;
                            viewPort.x -= 1;
                            map.dirty = true;
                        }
                        else
                        {
                            UiLog log = (UiLog)GetNode("/root/scene/ui/uicontainer/logpanel/UiLog");
                            KeyPoint kp = new KeyPoint(cell.X, cell.Y);
                            int hash = kp.GetHashCode();
                            string msg = "You bump into the " + map.vicinities[hash].type.DisplayName + "!";
                            log.append(msg);
                        }
                    }
                    break;
                case "southeast":
                    if(p.localMapX < 127 && p.localMapY < 127)
                    {
                        Cell cell = (Cell)map.GetCell(p.localMapX + 1, p.localMapY + 1);
                        if(cell.IsWalkable)
                        {
                            p.localMapY += 1;
                            p.localMapX += 1;
                            viewPort.y += 1;
                            viewPort.x += 1;
                            map.dirty = true;
                        }
                        else
                        {
                            UiLog log = (UiLog)GetNode("/root/scene/ui/uicontainer/logpanel/UiLog");
                            KeyPoint kp = new KeyPoint(cell.X, cell.Y);
                            int hash = kp.GetHashCode();
                            string msg = "You bump into the " + map.vicinities[hash].type.DisplayName + "!";
                            log.append(msg);
                        }
                    }
                    break;
                default:
                    return;
            }
        }
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            
        }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    }
}
