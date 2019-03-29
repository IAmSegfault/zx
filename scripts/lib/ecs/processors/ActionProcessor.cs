using System;
using System.Collections.Generic;
using Godot;
using GContainers = Godot.Collections;
using Zona.Engine;
namespace Zona.ECSProcessor
{
    public class ActionProcessor : Node, Processor
    {
        private Queue<string> inputActionQueue;
        private List<string> actionList;
        public ActionProcessor()
        {
            this.inputActionQueue = new Queue<string>();
            this.actionList = new List<string>();
            File f = new File();
            f.Open("user://actionmap.json", 1);
            string json = f.GetAsText();
            f.Close();
            JSONParseResult data = JSON.Parse(json);
            GContainers.Dictionary dataDict = data.Result as GContainers.Dictionary;
            GContainers.Array actions = dataDict["actions"] as GContainers.Array;
            foreach(object a in actions)
            {
                string action = Convert.ToString(a);
                actionList.Add(action);
            }
        }

        public void postInit(Ecs ecs)
        {

        }

        public void process(Ecs ecs, float delta, params dynamic[] args)
        {
            if(args[0] == "update")
            {
                GameMap gameMap = (GameMap)GetNode("/root/scene/gamespace/GameMap");
                if(gameMap.state == "player_at" && this.inputActionQueue.Count != 0)
                {
                    string action = this.inputActionQueue.Dequeue();
                    switch(action)
                    {
                        case "player_move_west":
                            PlayerMove pm = (PlayerMove)GetNode("/root/scene/gamespace/actions/PlayerMove");
                            pm.execute("west");
                            break;
                        case "player_move_east":
                            pm = (PlayerMove)GetNode("/root/scene/gamespace/actions/PlayerMove");
                            pm.execute("east");
                            break;
                        case "player_move_north":
                            pm = (PlayerMove)GetNode("/root/scene/gamespace/actions/PlayerMove");
                            pm.execute("north");
                            break;
                        case "player_move_south":
                            pm = (PlayerMove)GetNode("/root/scene/gamespace/actions/PlayerMove");
                            pm.execute("south");
                            break;
                        case "player_move_northwest":
                            pm = (PlayerMove)GetNode("/root/scene/gamespace/actions/PlayerMove");
                            pm.execute("northwest");
                            break;
                        case "player_move_northeast":
                            pm = (PlayerMove)GetNode("/root/scene/gamespace/actions/PlayerMove");
                            pm.execute("northeast");
                            break;
                        case "player_move_southwest":
                            pm = (PlayerMove)GetNode("/root/scene/gamespace/actions/PlayerMove");
                            pm.execute("southwest");
                            break;
                        case "player_move_southeast":
                            pm = (PlayerMove)GetNode("/root/scene/gamespace/actions/PlayerMove");
                            pm.execute("southeast");
                            break;
                        case "player_wait":
                        default:
                            return;
                    }
                }
                else
                {

                }
            }
        }

        public override void _Input(InputEvent @event)
        {
                GameMap gameMap = (GameMap)GetNode("/root/scene/gamespace/GameMap");
                if(gameMap.state == "player_at")
                {
                    foreach(string action in this.actionList)
                    {
                        if(@event.IsAction(action) && Input.IsActionJustPressed(action))
                        {
                            this.inputActionQueue.Enqueue(action);
                        }
                    }
                }
        }
        public override void _Ready()
        {
            
        }
    }
}