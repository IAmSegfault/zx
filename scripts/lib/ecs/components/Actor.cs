using System;
using Zona.Engine;
namespace Zona.ECSComponent
{
    public class Actor : Component
    {
        public int speed;
        public bool isPlayerCharacter;
        public int ct;

        public Actor(int speed, bool isPlayerCharacter=false)
        {
            this.speed = speed;
            this.isPlayerCharacter = isPlayerCharacter;
            this.ct = 60;
        }
    }
}