using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Sword : Actor
    {
        private static Sword _instance;
        AABB _hitbox;

        public Sword()
        {
            _instance = this;

            Sprite sprite = new Sprite("Graphics/Dagger.png");
            AddChild(sprite);

            _hitbox = new AABB(sprite.Width, sprite.Height);
            AddChild(_hitbox);
        }
        
        public static Sword Instance
        {
            get { return _instance; }
        }

        public AABB Hitbox
        {
            get { return _hitbox; }
        }
    }
}
