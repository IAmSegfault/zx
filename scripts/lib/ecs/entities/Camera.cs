using System;
using Zona.ECSComponent;

namespace Zona
{
    namespace ECSEntity
    {
        public class Camera2D : Zona.Engine.Entity
        {
            public Camera2D(params object[] args)
            {
                int x = Convert.ToInt32(args[0]);
                int y = Convert.ToInt32(args[1]);
                int w = Convert.ToInt32(args[2]);
                int h = Convert.ToInt32(args[3]);
                ViewportType type = (ViewportType)Convert.ToInt32(args[4]);
                ViewPort viewport = new ViewPort(x, y, w, h, type);
                this.addComponent(viewport);
                this.addTag("Camera2D");
                if(type == ViewportType.local)
                {
                    this.addTag("LocalCamera");
                }
                else if(type == ViewportType.world)
                {
                    this.addTag("WorldCamera");
                }
            }
        }
    }
}