using Godot;
using System;
using RogueSharp;

public class RMap : Map
{
    private int width{get;set;}
    private int height{get;set;}

    public RMap(int width, int height)
    {
        this.width = width;
        this.height = height;

        this.Initialize(width, height);
        foreach(Cell cell in this.GetAllCells())
        {
            this.SetCellProperties(cell.X, cell.Y, true, true, false);
        }
    }
}

public class GameMap : TileMap
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    private RMap _rMap;
    public override void _Ready()
    {
        this._rMap = new RMap(128, 128);

    }

    public void clearMap()
    {
        foreach(Cell cell in this._rMap.GetAllCells())
        {
            this._rMap.SetCellProperties(cell.X, cell.Y, true, true, false);

        }

        this.Clear();
    }
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
