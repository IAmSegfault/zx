using Godot;
using System;

public class UiLog : RichTextLabel
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        for(int i = 0; i < 10; i++)
        {
            this.AppendBbcode("[color=#FF00FF]Text in fuchsia[/color]\n");
        }
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
