using System;
using Zona.Engine;
using Zona.ECSComponent;
using Godot;
namespace Zona.ECSEntity
{
    public class Player : Entity
    {
        private Vector2 inputDirection()
        {
            int right = 0;
            int left = 0;
            int up = 0;
            int down = 0;
            if(Input.IsActionJustPressed("ui_right"))
            {
                right = 1;
            }
            if(Input.IsActionJustPressed("ui_left"))
            {
                left = 1;
            }
            if(Input.IsActionJustPressed("ui_up"))
            {
                up = 1;
            }
            if(Input.IsActionJustPressed("ui_down"))
            {
                down = 1;
            }
            return new Vector2(right - left, down - up);
        }

        //TODO: Move this animation code to a mob entity, the player should be in the center of the map and not animated.
        public override void _Process(float delta)
        {

            Vector2 direction = inputDirection();
            if(direction.x == 0 && direction.y == 0)
            {
                
            }
            else
            {
                KinematicActor ka = null;
                ActorSprite a = null;
                foreach(EntityNode enode in nodes)
                {
                    if(enode.Name.StartsWith("ActorSprite"))
                    {
                        ActorSprite AS = (ActorSprite)enode;
                        a = AS;
                        ka = AS.kinematicActor;
                    }
                }
                Sprite sprite = ka.getSprite();
                if(direction.x > 0)
                {
                    sprite.FlipH = true;
                }
                else if(direction.x < 0)
                {
                    sprite.FlipH = false;
                }

                Position2D pos2D = ka.getPosition2D();
                Vector2 pos = pos2D.Position;
                var gm = (GameMap)GetNode("/root/scene/gamespace/GameMap");
                Vector2 gPos = gm.WorldToMap(pos);
                Vector2 newGPos = gPos + direction;
                Vector2 newPos = gm.MapToWorld(newGPos);
                a.moveTo(newPos);
            }
        }
        public Player(params object[] args)
        {
            int worldX = Convert.ToInt32(args[0]); 
            int worldY = Convert.ToInt32(args[1]);
            int chunkX = Convert.ToInt32(args[2]);
            int chunkY = Convert.ToInt32(args[3]);
            int chunkZ = Convert.ToInt32(args[4]);
            int localMapX = Convert.ToInt32(args[5]);
            int localMapY = Convert.ToInt32(args[6]); 
            int lightRadius = Convert.ToInt32(args[7]);
            int speed = Convert.ToInt32(args[8]);
            this.addTag("Player");
            this.addComponent(new WorldPosition(worldX, worldY, chunkX, chunkY, chunkZ, localMapX, localMapY));
            this.addComponent(new LightRadius(lightRadius));
            this.addComponent(new Actor(speed, true));
        }
        public override void _Ready()
        {
            ActorSprite AS = new ActorSprite(0,0,0,0,16,24,"res://assets/oryx_roguelike_2.0/Monsters_Scifi.png", this.guid);
            this.nodes.Add(AS);
        }

    }   
}