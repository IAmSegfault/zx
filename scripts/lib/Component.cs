using System;
using Godot;

namespace Engine
{
    public class Component
    {
        private Entity _owner;
        public Entity owner
        {
            get
            {
                return this._owner;
            }

            set
            {
                this._owner = value;
            }
        }
        public virtual void init()
        {

        }
    }
}