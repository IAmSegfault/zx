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
            public Dictionary<int, Vicinity> vicinities;
            public List<KeyPoint> locales;
            public int width;
            public int height;
            public CSVClass staticData;

            public MapPosition mapPosition;
            public LocalMap(int width, int height, MapPosition mapPosition)
            {
                this.dirty = true;
                this.width = width;
                this.height = height;
                this.mapPosition = mapPosition;
                this.vicinities = new Dictionary<int, Vicinity>();
                this.locales = new List<KeyPoint>();
                this.Initialize(width, height);
                foreach(Cell cell in this.GetAllCells())
                {
                    this.SetCellProperties(cell.X, cell.Y, true, true, false);
                    Vicinity vicinity = new Vicinity(cell.X, cell.Y, VicinityType.Dirt);
                    vicinities.Add(new KeyPoint(cell.X, cell.Y).GetHashCode(), vicinity);
                    locales.Add(new KeyPoint(cell.X, cell.Y));
                }
            }

            public LocalMap(int width, int height)
            {
                this.dirty = true;
                this.width = width;
                this.height = height;
                this.vicinities = new Dictionary<int, Vicinity>();
                this.locales = new List<KeyPoint>();
                this.Initialize(width, height);
                foreach(Cell cell in this.GetAllCells())
                {
                    this.SetCellProperties(cell.X, cell.Y, true, true, false);
                    Vicinity vicinity = new Vicinity(cell.X, cell.Y, VicinityType.Dirt);
                    vicinities.Add(new KeyPoint(cell.X, cell.Y).GetHashCode(), vicinity);
                    locales.Add(new KeyPoint(cell.X, cell.Y));
                }
            }

            public void setMapPosition(MapPosition mapPosition)
            {
                this.mapPosition = mapPosition;
            }
            public void setVType(int x, int y, VicinityType type)
            {
                KeyPoint kp = new KeyPoint(x, y);
                int hc = kp.GetHashCode();
                if(vicinities.ContainsKey(hc))
                {
                    this.vicinities[hc].type = type;
                    Cell cell = (Cell)this.GetCell(x, y);
                    this.SetCellProperties(x, y, type.transparent, type.walkable, cell.IsExplored);
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