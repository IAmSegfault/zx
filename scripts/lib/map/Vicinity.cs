using System;
using Godot;

namespace Zona
{
    namespace Engine
    {

        public class VicinityType : EnumClass
        {
            public static VicinityType Dirt = new VicinityType(47, 47, "dirt", "fff8c0");
            public static VicinityType BrickWall = new VicinityType(8, 20, "brickwall", "d44e52");

            public string colorFG{get; set;}
            public int terminalValue{get;set;}
            public VicinityType(){}
            public VicinityType(int value, int terminalValue, string displayName, string colorFG) : base(value, displayName)
            {
                this.colorFG = colorFG;
                this.terminalValue = terminalValue;
            }

        }
        public class Vicinity
        {
            public int x{get;set;}
            public int y{get;set;}

            public VicinityType type{private get; set;}

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
}