using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Web : Entity
    {
        private Sprite _sprite = new Sprite("Graphics/SpiderWeb.png");
        private AABB _hitbox = new AABB(16, 16);
        Timer timer = new Timer();

        public Web(float x, float y) : base(x, y)
        {
            AddChild(_sprite);
            AddChild(_hitbox);
            OnUpdate += WebPlayer;
        }

        public void WebPlayer(float deltaTime)
        {
            if (Player.Instance == null || Parent == null)
            {
                return;
            }

            else if (Player.Instance != null)
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
