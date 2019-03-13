using System;
using Zona.Engine;

namespace Zona.ECSComponent
{
    public class LightRadius : Component
    {
        public int radius;
        public LightRadius(int radius)
        {
            this.radius = radius;
        }
    }
}