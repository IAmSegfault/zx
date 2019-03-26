using System;
using RogueSharp;

namespace Zona.Engine
{
    public class KeyPoint
    {
        public int X{get;set;}
        public int Y{get;set;}
        
        public KeyPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }


        public override bool Equals(object obj)
        {
            var other = obj as KeyPoint;
            if(other == null)
            {
                return false;
            }
            return this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ (int)RotateLeft((uint)this.Y.GetHashCode(), 16);
        }

        public uint RotateLeft(uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }
    }
}