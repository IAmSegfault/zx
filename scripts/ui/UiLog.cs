using Godot;
using System;

public class UiLog : RichTextLabel
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    private int maxLines;
    private int currentLines;
    public override void _Ready()
    {
        this.maxLines = 100;
        this.currentLines = 0;
    }

    public void append(string bbcode)
    {
        this.AppendBbcode(bbcode + "\n");
        currentLines++;
        if(currentLines > maxLines)
        {
            this.RemoveLine(currentLines - maxLines);
            currentLines--;
        }
    }
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
