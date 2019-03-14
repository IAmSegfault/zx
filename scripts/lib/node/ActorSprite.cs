using System;
using Godot;
using GContainers = Godot.Collections;
namespace Zona.Engine
{
    public class KinematicActor : KinematicBody2D
    {
        public int x;
        public int y;
        public int spriteX;
        public int spriteY;
        public int spriteW;
        public int spriteH;
        public string texture;
        public string guid;
        public Vector2 dest;
        public KinematicActor(int x, int y, int spriteX, int spriteY, int spriteW, int spriteH, string texture, string guid)
        {
            Vector2 pos = new Vector2();
            pos.x = x;
            pos.y = y;
            this.SetPosition(pos);
            this.spriteX = spriteX;
            this.spriteY = spriteY;
            this.spriteW = spriteW;
            this.spriteH = spriteH;
            this.texture = texture;
            this.guid = guid;
            this.SetName("KinematicActor." + guid);
        }
        public override void _Ready()
        {

            Sprite sprite = new Sprite();
            sprite.Texture = (Godot.Texture)GD.Load(texture);
            sprite.RegionEnabled = true;
            sprite.Centered = false;
            Rect2 rect = new Rect2();
            Vector2 start = new Vector2();
            start.x = spriteX;
            start.y = spriteY;
            Vector2 end = new Vector2();
            end.x = spriteW;
            end.y = spriteH;
            rect.Position = start;
            rect.Size = end;
            sprite.SetRegionRect(rect);
            sprite.SetName("Sprite." + this.guid);

            Position2D position2D = new Position2D();
            position2D.SetPosition(this.Position);
            position2D.SetName("Position2D." + this.guid);
            position2D.AddChild(sprite, true);

            this.AddChild(position2D, true);

            Tween tween = new Tween();
            tween.SetName("Tween." + this.guid);
            this.AddChild(tween, true);
        }

        public Tween getTween()
        {
            GContainers.Array arr = this.GetChildren();
            foreach(System.Object obj in arr)
            {
                Node node = (Node)obj;
                if(node.Name.StartsWith("Tween."))
                {
                    return (Tween)node;
                }
            }
            return null;
        }

        public Sprite getSprite()
        {
            GContainers.Array arr = this.GetChildren();
            foreach(System.Object obj in arr)
            {
                Node node = (Node)obj;
                if(node.Name.StartsWith("Position2D."))
                {
                    Position2D position2D = (Position2D)node;
                    Sprite sprite = (Sprite)position2D.GetChild(0);
                    return sprite;
                }
            }
            return null;
        }

        public Position2D getPosition2D()
        {
            GContainers.Array arr = this.GetChildren();

            foreach(System.Object obj in arr)
            {
                Node node = (Node)obj;
                if(node.Name.StartsWith("Position2D."))
                {
                    Position2D position2D = (Position2D)node;
                    return position2D;
                }
            }
            return null;
        }
    }
    public class ActorSprite : EntityNode, INode
    {
        private bool loaded;
        public int x;
        public int y;
        public int spriteX;
        public int spriteY;
        public int spriteW;
        public int spriteH;
        public string texture;
        public KinematicActor kinematicActor;

        public string guid;
        public ActorSprite(int x, int y, int spriteX, int spriteY, int spriteW, int spriteH, string texture, string guid) : base()
        {
            this.loaded = false;
            this.x = x;
            this.y = y;
            this.spriteX = spriteX;
            this.spriteY = spriteY;
            this.spriteW = spriteW;
            this.spriteH = spriteH;
            this.texture = texture;
            this.guid = guid;
            this.SetName("ActorSprite." + this.guid);
        }

        public void load()
        {
            if(!loaded)
            {
                this.loaded = true;
                Position2D position2D = this.kinematicActor.getPosition2D();
                var gm = (GameMap)GetNode("/root/scene/kernel/Ecs/gamespace/map/GameMap");
                Vector2 v2 = new Vector2(x, y);
                Vector2 pos = gm.MapToWorld(v2);
                position2D.SetPosition(pos);
                this.nodePaths.Add("/root/scene/kernel/Ecs/gamespace/map/GameMap/" + kinematicActor.Name);
            }
            
        }
        public override void _Ready()
        {
            KinematicActor kinematicActor = new KinematicActor(x, y, spriteX, spriteY, spriteW, spriteH, texture, guid);
            this.kinematicActor = kinematicActor;
            var gm = (GameMap)GetNode("/root/scene/kernel/Ecs/gamespace/map/GameMap");

            gm.AddChild(this.kinematicActor, true);
        }
        public void moveTo(Vector2 target)
        {
            this.x = (int)Math.Floor(target.x);
            this.y = (int)Math.Floor(target.y);
            Vector2 pos = this.kinematicActor.getPosition2D().Position;
            Position2D position2D = this.kinematicActor.getPosition2D();
            var direction = (target - pos).Normalized();
            Tween tween = this.kinematicActor.getTween();
            tween.InterpolateProperty(position2D, "position", -direction + pos, target, 0.2f, Tween.TransitionType.Back, Tween.EaseType.In);
            tween.Start();
            //position2D.SetPosition(target);
        }
        
        public void setModulate(string hexcode)
        {   Color color = new Color(hexcode);
            Sprite sprite = this.kinematicActor.getSprite();
            sprite.SetModulate(color);
        }
    }
}