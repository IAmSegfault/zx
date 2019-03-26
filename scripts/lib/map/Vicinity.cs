using System;
using System.Collections.Generic;
using Godot;

namespace Zona.Engine
{
    public class VicinityType : EnumClass
    {
        public static List<VicinityType> types = new List<VicinityType>();
        public static VicinityType Dirt = new VicinityType(47, 47, "dirt", "fff8c0", true, true);
        public static VicinityType BrickWall = new VicinityType(8, 20, "brick wall", "d44e52", false, false);
        public static VicinityType WoodWall = new VicinityType(8, 22, "wood wall", "b27e56", false, false);
        public static VicinityType Tarp = new VicinityType(8, 21, "tarp", "5c7a56", true, false);
        public static VicinityType Water = new VicinityType(39, 39, "water", "38607c", true, true);

        public string colorFG{get; set;}
        public int terminalValue{get;set;}
        public bool transparent;
        public bool walkable;

        public VicinityType(){}
        public VicinityType(int value, int terminalValue, string displayName, string colorFG, bool walkable, bool transparent) : base(value, displayName)
        {
            this.colorFG = colorFG;
            this.terminalValue = terminalValue;
            this.walkable = walkable;
            this.transparent = transparent;
            VicinityType.types.Add(this);
        }

    }
    public class Vicinity
    {
        public int x{get;set;}
        public int y{get;set;}

        public VicinityType type{get; set;}

        private LocalMap parent;
        public Vicinity(int x, int y , VicinityType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }

        public void setType(VicinityType type)
        {
            this.type = type;
        }
        public string getColor()
        {
            return this.type.colorFG;
        }

        public string getValue()
        {
            return this.type.Value.ToString();
        }

        public string getTerminalValue()
        {
            return this.type.terminalValue.ToString();
        }

        public string getName()
        {
            return this.type.DisplayName;
        }
    }
}