using System;
using Godot;
using System.Collections.Generic;
using Zona.Engine;
using Zona.ECSComponent;

namespace Zona.ECSProcessor
{
    public class CTProcessor : Node, Processor
    {
        private int resolutionThreshold;
        private List<Entity> cache;
        public CTProcessor()
        {
            this.resolutionThreshold = 100;
            this.cache = new List<Entity>();
        }

        public void process(Ecs ecs, float delta, params dynamic[] args)
        {
            if(args[0] == "render")
            {
                return;
            }
            else
            {
                var gameMap = (GameMap)GetNode("/root/scene/gamespace/GameMap");
                if(gameMap.state == "init")
                {
                    gameMap.state = "ct_await";
                }
                if(gameMap.state != "ct_await" && gameMap.state != "player_at" && gameMap.state != "npc_at")
                {
                    this.cache.Clear();
                    return;
                }
                if(cache.Count == 0)
                {
                    bool AT = false;
                    while(!AT)
                    {
                        foreach(Entity entity in ecs.entities)
                        {
                            if(entity.hasTag("Actor"))
                            {
                                Actor actor  = entity.getComponent<Actor>();
                                actor.ct += actor.speed;
                            }
                        }

                        foreach(Entity entity in ecs.entities)
                        {
                            if(entity.hasTag("Actor"))
                            {
                                Actor actor = entity.getComponent<Actor>();
                                if(actor.ct >= resolutionThreshold)
                                {
                                    this.cache.Add(entity);
                                    AT = true;
                                }
                            }
                        }
                    }

                    if(cache.Count > 0)
                    {
                        this.cache.Sort((self, other) => self.getComponent<Actor>().ct.CompareTo(other.getComponent<Actor>().ct));
                        this.cache.Reverse();
                        Entity entity = this.cache[0];
                        this.cache.RemoveAt(0);
                        gameMap.AT = entity;
                        Actor actor = entity.getComponent<Actor>();
                        if(actor.isPlayerCharacter)
                        {
                            gameMap.state = "player_at";
                            return;
                        }
                        else
                        {
                            gameMap.state = "npc_at";
                            return;
                        }
                    }
                }
                else if( gameMap.state == "ct_await")
                {
                    Entity entity = this.cache[0];
                    this.cache.RemoveAt(0);
                    gameMap.AT = entity;
                    Actor actor = entity.getComponent<Actor>();
                    if(actor.isPlayerCharacter)
                    {
                        gameMap.state = "player_at";
                    }
                    else
                    {
                        gameMap.state = "npc_at";
                    }
                }
            }
        }

        public void postInit(Ecs ecs)
        {

        }
    }
}