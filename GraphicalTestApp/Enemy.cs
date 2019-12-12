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
        private AABB _hitbox;
        private Sprite _sprite;
        public Room CurrentRoom;
        Timer timer = new Timer();

        public float Speed { get; set; } = 100f;

        public Enemy(float x, float y) : base(x, y)
        {
            _facing = Direction.North;
            OnUpdate += Move;
            OnUpdate += TouchSword;
            OnUpdate += TouchPlayer;

            _sprite = new Sprite("Graphics/spooder.png");
            AddChild(_sprite);

            _hitbox = new AABB(_sprite.Width, _sprite.Height);
            AddChild(_hitbox);
        }

        public AABB Hitbox
        {
            get { return _hitbox; }
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
            if (!CurrentRoom.GetCollision(X, _hitbox.Top))
            {
                YVelocity = -Speed;
            }

            else
            {
                _facing = Direction.West;
            }
        }

        private void MoveDown(float deltaTime)
        {
            if (!CurrentRoom.GetCollision(X, _hitbox.Bottom))
            {
                YVelocity = Speed;
            }

            else
            {
                _facing = Direction.East;
            }
        }

        private void MoveLeft(float deltaTime)
        {
            if (!CurrentRoom.GetCollision(_hitbox.Left, Y))
            {
                XVelocity = -Speed;
            }

            else
            {
                _facing = Direction.South;
            }
        }

        private void MoveRight(float deltaTime)
        {
            if (!CurrentRoom.GetCollision(_hitbox.Right, Y))
            {
                XVelocity = Speed;
            }

            else
            {
                _facing = Direction.North;
            }
        }

        private void TouchSword(float deltaTime)
        {
            if (Sword.Instance != null)
            {
                if (_hitbox.DetectCollision(Sword.Instance.Hitbox) && timer.Seconds >= 1f)
                {
                    Y = -1000;
                    Parent.RemoveChild(this);
                }
            }
        }

        private void TouchPlayer(float deltaTime)
        {
            if (Player.Instance == null || Parent == null)
            {
                return;
            }

            else if(Player.Instance != null)
            {
                if (_hitbox.DetectCollision(Player.Instance.Hitbox) && timer.Seconds >= 1f)
                {
                    Player.Instance.Y = -1000;
                    Parent.RemoveChild(Player.Instance);
                }
            }
        }
    }
}
