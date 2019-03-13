using System;
using Godot;

namespace Zona
{
    namespace Engine
    {
        public interface Processor
        {
           void process(Ecs ecs, float delta, params dynamic[] args);

           void postInit(Ecs ecs); 
        }

    }
}