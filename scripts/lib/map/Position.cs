using System;
using System.Collections.Generic;

namespace Zona
{
    namespace Engine
    {
        public class LocalMapPosition
        {
            public int x;
            public int y;
            
            public LocalMapPosition(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public class ChunkMapPosition : LocalMapPosition
        {
            public int z;
            public ChunkMapPosition(int x, int y, int z) : base(x, y)
            {
                this.z = z;
            }
        }

        public class WorldMapPosition : LocalMapPosition
        {
            public WorldMapPosition(int x, int y) : base(x, y)
            {

            }
        }

        public class MapPosition
        {
            public WorldMapPosition worldMapPosition;
            public ChunkMapPosition chunkMapPosition;

            public MapPosition(int cX, int cY, int cZ, int wX, int wY)
            {
                this.worldMapPosition = new WorldMapPosition(wX, wY);
                this.chunkMapPosition = new ChunkMapPosition(cX, cY, cZ);
            }
            
            public bool Equals(MapPosition other)
            {
                if(this.chunkMapPosition.x == other.chunkMapPosition.x && this.chunkMapPosition.y == other.chunkMapPosition.y && this.chunkMapPosition.z == other.chunkMapPosition.z && this.worldMapPosition.x == other.worldMapPosition.y)
                {
                    return false;
                }
                return true;
            }
        }
    }
}