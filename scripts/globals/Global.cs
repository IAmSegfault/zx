using System;
using System.Collections.Generic;
using Godot;
using GContainers = Godot.Collections;
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
            Directory directory = new Directory();
            File f = new File();
            if(!f.FileExists("user://actionmap.json"))
            {
                f.Open("res://data/directive/actionmap.json", 1);
                string json = f.GetAsText();
                f.Close();
                f.Open("user://actionmap.json", 7);
                f.StoreString(json);
                f.Close();
            }
        }
        
        public override void _Input(InputEvent @event)
        {
            if(@event.IsAction("fullscreen"))
            {   
                if(Input.IsActionJustPressed("fullscreen"))
                {
                    OS.WindowFullscreen = !OS.WindowFullscreen;
                }
                
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

        public GContainers.Dictionary getStatBlocks()
        {
            GContainers.Dictionary dict = new GContainers.Dictionary();
            for(int i = 0; i < 5; i++)
            {
                GContainers.Array sb = new GContainers.Array();
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