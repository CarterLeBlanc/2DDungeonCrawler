using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Wall : Actor
    {
        public Wall(int x, int y)
        {
            X = x;
            Y = y;
            Solid = true;

            Sprite sprite = new Sprite("Graphics/Wall.png");
            AddChild(sprite);

            AABB Hitbox = new AABB(sprite.Width, sprite.Height);
            AddChild(Hitbox);
        }
    }
}
