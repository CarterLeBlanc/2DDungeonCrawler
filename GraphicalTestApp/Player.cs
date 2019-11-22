using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Player : Entity
    {
        private PlayerInput _input = new PlayerInput();

        public Player(float x, float y) : base(12, 76)
        {
            OnUpdate += MoveUp;
            OnUpdate += MoveDown;
            OnUpdate += MoveLeft;
            OnUpdate += MoveRight;

            Sprite sprite = new Sprite("Graphics/BitesZaDusto.png");
            AddChild(sprite);

            AABB HitBox = new AABB(sprite.Width, sprite.Height);
            AddChild(HitBox);
        }

        private void MoveUp(float deltaTime)
        {
            if (Input.IsKeyDown(87))
            {
                Y -= 100 * deltaTime;
            }
        }

        private void MoveDown(float deltaTime)
        {
            if (Input.IsKeyDown(83))
            {
                Y += 100 * deltaTime;
            }
        }

        private void MoveLeft(float deltaTime)
        {
            if (Input.IsKeyDown(65))
            {
                X -= 100 * deltaTime;
            }
        }

        private void MoveRight(float deltaTime)
        {
            if (Input.IsKeyDown(68))
            {
                X += 100 * deltaTime;
            }
        }
    }
}
