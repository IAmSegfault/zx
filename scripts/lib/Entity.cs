using System;
using System.Linq;
using System.Collections.Generic;


namespace Engine
{
    [Serializable]
    public class Entity
    {
        private ulong id{get; set;}
        private string guid{get; set;}
        private Dictionary<string, List<string>> containers;
        private List<Component> components;

        public List<string> tags;

        public Entity()
        {
            this.guid = Guid.NewGuid().ToString();
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

        public void addComponent(Component component)
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

        public List<string> getContainer(string container)
        {
            List<string> c = this.containers[container];
            return c;
        }
    }
}