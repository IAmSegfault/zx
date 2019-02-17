using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Runtime.Remoting;
using Engine;
using ECSProcessor;
using RogueSharp;

[Serializable]
public class Ecs : Node
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    private ulong idCtr;
    private List<Entity> entities;
    private Map loadedMap; 


    private List<List<Processor>> processors;
    public override void _Ready()
    {
        this.idCtr = 0;
        loadedMap = new Map();

        this.processors = new List<List<Processor>>();
        for(int i = 0; i < 16; i++)
        {
            List<Processor> row = new List<Processor>();
            this.processors.Add(row);
        }
        // Called every time the node is added to the scene.
        // Initialization here
        
    }
    
    public void addProcessor(dynamic priority, dynamic processor)
    {
        if(priority >= 16)
        {
            priority = 15;
        }

        var row = this.processors[(int)priority];
        row.Add(processor);
    }

    public void removeProcessor(string processor)
    {
        for(int i = 0; i < 16; i++)
        {
            var row = this.processors[i];
            row.RemoveAll(x => x.GetType().Name == processor);
        }
    }

    public void _on_Loader_load_processor(object[] processors, object[] priorities)
    {
        var processorList = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "ECSProcessor").ToList();
        List<string> classList = new List<string>();

        foreach(Type type in processorList)
        {
            if(type.Namespace == "ECSProcessor")
            {
                classList.Add(type.Name);
            }
        }

        for(int i = 0; i < processors.Length; i++)
        {
            string pname = (string)processors[i];
            if(classList.Contains(pname))
            {  
                string className = "ECSProcessor." + pname; 
                var proccessInstance = Activator.CreateInstance(Assembly.GetExecutingAssembly().GetName().Name, className);
                dynamic process = proccessInstance.Unwrap();
                dynamic priority = priorities[i];
                this.addProcessor(priority, process);
            }
        }
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        for(int i = 0; i < 16; i++)
        {
            List<Processor> row = this.processors[i];
            foreach(Processor processor in row)
            {
                processor.process(this, delta);
            }
        }        
    }
}