using System.Collections.Generic;
using Zona.Engine;

namespace Zona.Util
{
    public class TileType : EnumClass
    {
        public int value;
        public string displayName;
        public int asciiCode;
        public string hexCode;
        public string tileID;
        public string tileTerminalID;
        public bool transparent;
        public bool walkable;
        public static List<TileType> types = new List<TileType>();
        public static int ctr = 0;
        public static TileType Dirt = new TileType("dirt", 46, "#fff8c0", "47_fff8c0", "47_fff8c0", true, true);
        public static TileType Water = new TileType("water", 247, "#fff8c0", "39_38607c", "39_38607c", true, true);
        public static TileType BrickWall = new TileType("brick wall", 186, "#d44e52", "8_d44e52", "20_d44e52", false, false);
        public static TileType ConcreteWall = new TileType("concrete wall", 186, "#84545c", "8_84545c", "20_84545c", false, false);
        public static TileType WoodWall = new TileType("wood wall", 186, "", "", "", false, false);
        public TileType(string displayName, int asciiCode, string hexCode, string tileID, string tileTerminalID, bool transparent, bool walkable) : base(TileType.ctr, displayName)
        {
            TileType.ctr++;
            this.displayName = displayName;
            this.asciiCode = asciiCode;
            this.hexCode = hexCode;
            this.tileID = tileID;
            this.tileTerminalID = tileTerminalID;
            this.transparent = transparent;
            this.walkable = walkable;
            TileType.types.Add(this);
        }
    }
}