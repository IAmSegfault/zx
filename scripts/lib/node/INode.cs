using System;
using Godot;
using Containers = System.Collections.Generic;

namespace Zona.Engine
{
    public interface INode
    {
        void load();
        void setModulate(string hexcode);

    }

    public class EntityNode : Node
    {
        public Containers.List<string> nodePaths;
        public Containers.List<Node> nodes; 

        public EntityNode()
        {
            this.nodePaths = new Containers.List<string>();
            this.nodes = new Containers.List<Node>();
        }
    }
}