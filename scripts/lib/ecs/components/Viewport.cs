using System;
using Zona.Engine;
namespace Zona
{
    namespace ECSComponent
    {
        public enum ViewportType
        {
            local = 0,
            world,
        }
        public class ViewPort : Component
        {
            public int x;
            public int y;

            public int w;
            public int h;

            public ViewportType type;
            public ViewPort(int x, int y, int w, int h, ViewportType type)
            {
                this.type = type;
                this.x = x;
                this.y = y;
                this.w = w;
                this.h = h;
            }
        }
    }
}