using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Player : Entity
    {
        private static Player _instance;
        AABB _hitbox;
        Actor SwordNode = new Actor();
        Sword sword = new Sword();
        private Room _room;
        private Actor _root;
        public Room CurrentRoom;
        private bool _rotating;
        Timer timer = new Timer();

        public Player(float x, float y, Room room, Actor root) : base (x, y)
        {
            OnUpdate += MoveUp;
            OnUpdate += MoveDown;
            OnUpdate += MoveLeft;
            OnUpdate += MoveRight;
            OnUpdate += OrbitClock;
            OnUpdate += OrbitCounter;
            OnUpdate += ExtendSword;
            OnUpdate += RetractSword;
            OnUpdate += Spin;

            _instance = this;

            Sprite sprite = new Sprite("Graphics/Mage.png");
            AddChild(sprite);
            
            AddChild(SwordNode);            
            SwordNode.AddChild(sword);

            sword.X = 20;
            sword.Y = 0;
            _hitbox = new AABB(sprite.Width, sprite.Height);
            AddChild(_hitbox);

            CurrentRoom = room;
            _root = root;
        }

        public static Player Instance
        {
            get { return _instance; }
        }

        public AABB Hitbox
        {
            get { return _hitbox; }
        }

        private void MoveUp(float deltaTime)
        {
            if (Y - 1 < 25)
            {
                if (CurrentRoom is Room)
                {
                    Travel(CurrentRoom.North);
                    Y = 120;
                }
            }

            else if (Input.IsKeyDown(87) && !CurrentRoom.GetCollision(X, _hitbox.Top))
            {
                Y -= 100 * deltaTime;
            }
        }

        private void MoveDown(float deltaTime)
        {
            if (Y + 1 > 130)
            {
                if (CurrentRoom is Room)
                {
                    Travel(CurrentRoom.South);
                    Y = 35;
                }
            }

            else if (Input.IsKeyDown(83) && !CurrentRoom.GetCollision(X, _hitbox.Bottom))
            {
                Y += 100 * deltaTime;
            }
        }

        private void MoveLeft(float deltaTime)
        {
            if (X - 1 < 10)
            {
                if (CurrentRoom is Room)
                {
                    Travel(CurrentRoom.West);
                    X = 275;
                }
            }

            else if (Input.IsKeyDown(65) && !CurrentRoom.GetCollision(_hitbox.Left, Y))
            {
                X -= 100 * deltaTime;
            }
        }

        private void MoveRight(float deltaTime)
        {
            if (X + 1 > 300)
            {
                if (CurrentRoom is Room)
                {
                    Travel(CurrentRoom.East);
                    X = 15;
                }
            }

            else if (Input.IsKeyDown(68) && !CurrentRoom.GetCollision(_hitbox.Right, Y))
            {
                X += 100 * deltaTime;
            }
        }

        private void Travel(Room destination)
        {
            _root.RemoveChild(CurrentRoom);
            _root.AddChild(destination);

            CurrentRoom = destination;

            if (Parent == null)
            {
                return;
            }

            Parent.RemoveChild(this);
            CurrentRoom.AddChild(this);
        }

        private void OrbitClock(float deltaTime)
        {
            if (Input.IsKeyDown(263))
            {
                SwordNode.Rotate(3f * deltaTime);
                _rotating = true;
            }
            _rotating = false;
        }

        private void OrbitCounter(float deltaTime)
        {
            if (Input.IsKeyDown(262))
            {
                SwordNode.Rotate(-3f * deltaTime);
                _rotating = true;
            }
        }

        private void ExtendSword(float deltaTime)
        {
            if (Input.IsKeyDown(265))
            {
                if (sword.X <= 75 && sword.X >= -75)
                {
                    sword.X++;
                } 
            }
        }

        private void RetractSword(float deltaTime)
        {
            if (Input.IsKeyPressed(264))
            {
                sword.X = 20;
            }
        }

        private void Spin(float deltaTime)
        {
            if (!_rotating)
            {
                sword.Rotate(3f * deltaTime);
            }
        }
    }
}
