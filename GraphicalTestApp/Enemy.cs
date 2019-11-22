using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Enemy : Entity
    {
        private static float x;
        private static float y;

        private Direction _facing;

        public float Speed { get; set; } = 10f;

        public Enemy(float x, float y) : base(12, 76)
        {
            _facing = Direction.North;
            OnUpdate += Move;
            OnUpdate += TouchPlayer;
            OnUpdate += Spin;

            Sprite sprite = new Sprite("Graphics/BitesZaDusto.png");
            AddChild(sprite);

            AABB Hitbox = new AABB(sprite.Width, sprite.Height);
            AddChild(Hitbox);
        }

        private void TouchPlayer(float deltaTime)
        {
            List<Entity> touched;
            touched = CurrentScene.GetEntities(X, Y);
            bool hit = false;
            
            foreach (Entity e in touched)
            {
                if (e is Player)
                {
                    hit = true;
                    break;
                }
            }

            if (hit)
            {
                CurrentScene.RemoveChild(this);
            }
        }

        private void Move(float deltaTime)
        {
            switch (_facing)
            {
                case Direction.North:
                    MoveUp(deltaTime);
                    break;
                case Direction.South:
                    MoveDown(deltaTime);
                    break;
                case Direction.East:
                    MoveRight(deltaTime);
                    break;
                case Direction.West:
                    MoveLeft(deltaTime);
                    break;
            }
        }

        private void MoveUp(float deltaTime)
        {
            if (!CurrentScene.GetCollision(XAbsolute, Sprite.Top - Speed * deltaTime))
            {
                YVelocity = -Speed * deltaTime;
            }

            else
            {
                YVelocity = 0f;
                _facing = Direction.East;
            }
        }

        private void MoveDown(float deltaTime)
        {
            if (!CurrentScene.GetCollision(XAbsolute, Sprite.Bottom + Speed * deltaTime))
            {
                YVelocity = Speed * deltaTime;
            }

            else
            {
                YVelocity = 0f;
                _facing = Direction.West;
            }
        }

        private void MoveLeft(float deltaTime)
        {
            if (!CurrentScene.GetCollision(Sprite.Left - Speed * deltaTime, YAbsolute))
            {
                XVelocity = -Speed * deltaTime;
            }

            else
            {
                XVelocity = 0f;
                _facing = Direction.North;
            }
        }

        private void MoveRight(float deltaTime)
        {
            if (!CurrentScene.GetCollision(Sprite.Right + Speed * deltaTime, YAbsolute))
            {
                XVelocity = Speed * deltaTime;
            }

            else
            {
                XVelocity = 0f;
                _facing = Direction.South;
            }
        }

        public void Spin(float deltaTime)
        {
            Rotate(0.01f);
        }
    }
}
