using System;
using System.Collections.Generic;

namespace Zona.Engine
{
    public class CP437Mapping : EnumClass
    {
        public string colorFG;
        public string colorBG;
        public VicinityType mapping;
        public static List<CP437Mapping> mapList = new List<CP437Mapping>();


        public static CP437Mapping Dirt = new CP437Mapping(46, "#FFFFFF", "#000000", "dirt", VicinityType.Dirt);
        public static CP437Mapping BrickWall = new CP437Mapping(186, "#FFFFFF", "#D44E52", "brick wall", VicinityType.BrickWall);
        public static CP437Mapping WoodWall = new CP437Mapping(186, "#FFFFFF", "#B27E56", "wood wall", VicinityType.WoodWall);
        public static CP437Mapping Tarp = new CP437Mapping(186, "#FFFFFF", "#5C7A56", "tarp", VicinityType.Tarp);
        public static CP437Mapping Water = new CP437Mapping(247, "#FFFFFF", "#000000", "water", VicinityType.Water);
        public CP437Mapping(int value, string colorFG, string colorBG, string displayName, VicinityType mapping) : base(value, displayName)
        {
            this.colorBG = colorBG;
            this.mapping = mapping;
            CP437Mapping.mapList.Add(this);
        }
    }
}