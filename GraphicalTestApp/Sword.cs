using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Sword : Actor
    {
        public Sword()
        {
            Sprite sprite = new Sprite("Graphics/Breakdown.png");
            AddChild(sprite);

            AABB Hitbox = new AABB(sprite.Width, sprite.Height);
            AddChild(Hitbox);
        }

        private void Orbit(float deltaTime)
        {
            foreach(Actor sword in _children)
            {
                sword.Rotate(5f * deltaTime);
            }
        }
    }
}
