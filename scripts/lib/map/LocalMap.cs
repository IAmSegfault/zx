using System;
using System.Collections.Generic;
using Zona.Util;
using RogueSharp;

namespace Zona
{
    namespace Engine
    {
        public class LocalMap : Map
        {
            public bool dirty;
            public Dictionary<Point, Vicinity> vicinities;
            public List<Point> locales;
            public int width;
            public int height;
            public CSVClass staticData;
            public LocalMap(int width, int height)
            {
                this.dirty = true;
                this.width = width;
                this.height = height;
                this.vicinities = new Dictionary<Point, Vicinity>();
                this.locales = new List<Point>();
                this.Initialize(width, height);
                foreach(Cell cell in this.GetAllCells())
                {
                    this.SetCellProperties(cell.X, cell.Y, true, true, false);
                    Vicinity vicinity = new Vicinity(cell.X, cell.Y, VicinityType.Dirt);
                    vicinities.Add(new Point(cell.X, cell.Y), vicinity);
                    locales.Add(new Point(cell.X, cell.Y));
                }
            }

            public void setVType(int x, int y, VicinityType type)
            {
                if(vicinities.ContainsKey(new Point(x, y)))
                {
                    Vicinity vicinity = vicinities[new Point(x, y)];
                    vicinity.type = type;
                    this.dirty = true;
                }
            }

            public void setStaticData(CSVClass cls)
            {
                this.staticData = cls;
            }
        }
    }
}