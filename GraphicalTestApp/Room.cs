using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphicalTestApp
{
    enum Direction
    {
        North,
        East,
        South,
        West
    }

    class Room : Actor
    {
        private Vector3 _gridSize = new Vector3(16, 16, 0);
        private Vector3 _roomSize = new Vector3();

        private bool[,] _collision;

        public bool GetCollision(float x, float y)
        {
            x /= _gridSize.x;
            y /= _gridSize.y;

            if (x >= 0 && y >= 0 && x < _roomSize.x && y < _roomSize.y)
            {
                return _collision[(int)x, (int)y];
            }

            else
            {
                return true;
            }
        }

        private Room _north;
        private Room _east;
        private Room _south;
        private Room _west;

        public Room North
        {
            get { return _north; }

            set
            {
                if (value != null)
                {
                    value._south = this;
                }

                else
                {
                    _north._south = null;
                }

                _north = value;
            }
        }

        public Room East
        {
            get { return _east; }

            set
            {
                if (value != null)
                {
                    value._west = this;
                }

                else
                {
                    _east._west = null;
                }

                _east = value;
            }
        }

        public Room South
        {
            get { return _south; }

            set
            {
                if (value != null)
                {
                    value._north = this;
                }

                else
                {
                    _south._north = null;
                }

                _south = value;
            }
        }

        public Room West
        {
            get { return _west; }

            set
            {
                if (value != null)
                {
                    value._east = this;
                }

                else
                {
                    _west._north = null;
                }

                _west = value;
            }
        }

        public void LoadRoom(string path)
        {
            StreamReader reader = new StreamReader(path);

            int width, height;

            Int32.TryParse(reader.ReadLine(), out width);
            Int32.TryParse(reader.ReadLine(), out height);

            _roomSize.x = width;
            _roomSize.y = height;

            _collision = new bool[width, height];

            for (int y = 0; y < height; y++)
            {
                string row = reader.ReadLine();
                for (int x = 0; x < width; x++)
                {
                    char tile = row[x];
                    switch (tile)
                    {
                        case '1':
                            _collision[x, y] = true;
                            Sprite WallSprite = new Sprite("Graphics/Wall.png");
                            WallSprite.X = x * _gridSize.x;
                            WallSprite.Y = y * _gridSize.y;
                            AABB hitbox = new AABB(WallSprite.Width, WallSprite.Height);
                            hitbox.X = x * _gridSize.x + 8;
                            hitbox.Y = y * _gridSize.y + 8;
                            AddChild(WallSprite);
                            AddChild(hitbox);
                            break;

                        case 'e':
                            Enemy e = new Enemy(x * _gridSize.x, y * _gridSize.y);
                            AddChild(e);
                            e.CurrentRoom = this;
                            break;

                        case 's':
                            SpiderBoss spiderBoss = new SpiderBoss(x * _gridSize.x, y * _gridSize.y);
                            AddChild(spiderBoss);
                            spiderBoss.CurrentRoom = this;
                            break;
                    }
                }
            }

        }
    }
}
