using System;
using Godot;

class Global : Node
{
    public string test;

    public override void _Ready()
    {
        this.test = "foo";
    }
    
}