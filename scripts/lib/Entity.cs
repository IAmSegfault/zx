using System;
using Godot;
using System.Linq;
using System.Collections.Generic;

namespace Zona
{
    namespace Engine
    {
        [Serializable]
        public class Entity : Node
        {
            public ulong id{get; private set;}
            public string guid{get; private set;}
            private System.Collections.Generic.Dictionary<string, List<string>> containers;
            private List<dynamic> components;

            public List<string> tags;

            public List<INode> nodes;

            public Entity()
            {

                this.guid = Guid.NewGuid().ToString();
                this.components = new List<dynamic>();
                this.tags = new List<string>();
                this.nodes = new List<INode>();
            }

            public override void _Ready()
            {
                var global = (Global)GetNode("/root/Global");
                this.id = global.generateEntityID();
            }

            public T getComponent<T>() where T : Component
            {
                foreach(Component component in this.components)
                {
                    if(component.GetType().Equals(typeof(T)))
                    {
                        return (T)component;
                    }
                }                
                return null;
            }

            public bool hasComponents(List<string> componentCheck)
            {
                return !componentCheck.Except(this.tags).Any();
            }

            public bool hasTag(string tag)
            {
                if(this.tags.Contains(tag))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void addComponent(dynamic component)
            {
                this.components.Add(component);
                component.owner = this;
                this.tags.Add(component.GetType().Name);
            }

            public void removeComponent(string component)
            {
                this.components.RemoveAll(x => x.GetType().Name == component);
                this.tags.RemoveAll(x => x == component);
            }

            public void addTag(string tag)
            {
                this.tags.Add(tag);
            }

            public void removeTag(string tag)
            {
                this.tags.RemoveAll(x => x == tag);
            }

            public List<string> getContainer(string container)
            {
                List<string> c = this.containers[container];
                return c;
            }
        }
    }
}