using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Engine
{
    public enum SysCall
    {
        getstate=0,
        setstate,
    }

    public class gwk : Node
    {
        // Member variables here, example:
        // private int a = 2;
        // private string b = "textvar";
        //private int a = 2;
        private string state = "init";
        private uint activeTurn = 0;


        public override void _Ready()
        {
            // Called every time the node is added to the scene.
            // Initialization here
        }

        public dynamic sysCall(SysCall callid, params dynamic[] args)
        {
            switch(callid)
            {
                case SysCall.getstate:
                    return this.state;
                
                case SysCall.setstate:
                    this.state = args[0];
                    return true;
                    
                default:
                    return false;
            }
        }
        public override void _Process(float delta)
        {        // Called every frame. Delta is time since last frame.        // Update game logic here.        
            if (Input.IsActionJustPressed("fullscreen"))
            {
                Console.WriteLine("Window");
                OS.WindowFullscreen = !OS.WindowFullscreen;
            }
        }
    }
}
