using System;
using System.Collections.Generic;
using Godot;
using RogueSharp.DiceNotation;

namespace Zona.Engine
{


    class Global : Node
    {
        public List<string> initList;
        public ulong enitityCtr;

        public override void _Ready()
        {
            this.initList = new List<string>();
            this.enitityCtr = 0;
        }
        
        public override void _Input(InputEvent @event)
        {
            if(@event.IsAction("fullscreen"))
            {   
                OS.WindowFullscreen = !OS.WindowFullscreen;
            }		
        }

        public void initScene(string scene)
        {
            if(!initList.Contains(scene))
            {
                initList.Add(scene);
            }
        }

        public bool isInit(string scene)
        {
            if(initList.Contains(scene))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Godot.Dictionary getStatBlocks()
        {
            Godot.Dictionary dict = new Godot.Dictionary();
            for(int i = 0; i < 5; i++)
            {
                Godot.Array sb = new Godot.Array();
                for(int j = 0; j < 6; j++)
                {
                    int stat = Dice.Roll("3d6");
                    sb.Add((object)stat);
                }
                string key = i.ToString();
                dict.Add((object)key, (object)sb);
            }
            return dict;
        }

        public ulong generateEntityID()
        {
            ulong id = this.enitityCtr;
            this.enitityCtr++;
            return id;
        }
    }
}