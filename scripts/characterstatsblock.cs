using Godot;
using System;

public class characterstatsblock : RichTextLabel
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";


    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public void _on_gamepaneltabs_tab_changed(int tab)
    {
        if(tab == 1)
        {
            this.Clear();
            this.AddText("Name: Strelok\n");
            this.AddText("STR: 18\n");
            this.AddText("DEX: 18\n");
            this.AddText("CON: 18\n");
            this.AddText("INT: 18\n");
            this.AddText("WIS: 18\n");
            this.AddText("CHA: 18\n");

        }
        
    }
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
