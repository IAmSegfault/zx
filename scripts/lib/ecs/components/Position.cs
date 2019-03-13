using System;
using Zona.Engine;

namespace Zona.ECSComponent
{
    public class WorldPosition : Component
    {
        // World position is from 0,0 to 63,63
        public int worldX;
        public int worldY;
        // Chunk position is from 0,0,0 to 23,23,127
        public int chunkX;
        public int chunkY;
        public int chunkZ;
        // Local map position is from 0,0 to 127,127
        public int localMapX;
        public int localMapY;

        public WorldPosition()
        {
            this.worldX = 30;
            this.worldY = 63;
            this.chunkX = 11;
            this.chunkY = 11;
            this.chunkZ = 63;


        }

        public WorldPosition(int worldX, int worldY, int chunkX, int chunkY, int chunkZ, int localMapX, int localMapY)
        {
            this.worldX = worldX;
            this.worldY = worldY;
            this.chunkX = chunkX;
            this.chunkY = chunkY;
            this.chunkZ = chunkZ;
            this.localMapX = localMapX;
            this.localMapY = localMapY;
        }
    }
}