using System;
using System.Collections.Generic;
using RogueSharp;
namespace Zona
{
    namespace Engine
    {
        public class Chunk
        {
            public int x;
            public int y;
            public int z;
            public int width;
            public int height;
            public int length;
            public Dictionary<Point, LocalMap> loadedMaps;
            public List<ChunkMapPosition> chunkMaps;

            public Chunk(int x, int y, int z, int width, int height, int length)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.width = width;
                this.height = height;
                this.length = length;
                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < height; j++)
                    {
                        
                        for(int k = 0; k < length; k++)
                        {
                            ChunkMapPosition position = new ChunkMapPosition(i, j, k);
                            this.chunkMaps.Add(position);
                        }

                    }
                }
            }
        }
    }
}