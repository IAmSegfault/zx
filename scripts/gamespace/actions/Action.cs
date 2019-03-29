using System;
using Godot;

namespace Zona.Engine
{
    public interface IAction
    {
        void execute(params dynamic[] args);
    }
}