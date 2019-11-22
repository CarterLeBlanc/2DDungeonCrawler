using System;
using Raylib;
using RL = Raylib.Raylib;
using System.IO;

namespace GraphicalTestApp
{
    class Game
    {
        //The current root Actor
        private Actor _root = null;
        //The next root Actor
        private Actor _next = null;
        //The Timer for the entire Game
        private Timer _gameTimer = new Timer();

        //Creates a Game and new Scene instance as its active Scene
        public Game(int width, int height, string title)
        {
            RL.InitWindow(width, height, title);
            RL.SetTargetFPS(0);
        }
        
        //Run the game loop
        public void Run()
        {
            //Update and draw until the game is over
            while (!RL.WindowShouldClose())
            {
                //Change the Scene if needed
                if (_root != _next)
                {
                    _root = _next;
                }

                //Start the Scene if needed
                if (!_root.Started)
                {
                    _root.Start();
                }

                //Update the active Scene
                _root.Update(_gameTimer.GetDeltaTime());

                //Draw the active Scene
                RL.BeginDrawing();
                RL.ClearBackground(Color.BLACK);
                _root.Draw();
                RL.EndDrawing();
            }

            //End the game
            RL.CloseWindow();
        }

        //The Actor we are currently running
        public Actor Root
        {
            get { return _root; }
            set
            {
                _next = value;
                if (_root == null) _root = value;
            }
        }

        private Room LoadRoom(string path)
        {
            StreamReader reader = new StreamReader(path);

            int width, height;

            Int32.TryParse(reader.ReadLine(), out width);
            Int32.TryParse(reader.ReadLine(), out height);

            Room room = new Room(width, height);

            for (int y = 0; y < height; y++)
            {
                string row = reader.ReadLine();
                for (int x = 0; x < width; x++)
                {
                    char tile = row[x];
                    switch (tile)
                    {
                        case '1':
                            room.AddChild(new Wall(x, y));
                            break;
                        case '@':
                            Player p = new Player(x, y);
                            p.X = x;
                            p.Y = y;
                            room.AddChild(p);
                            break;
                        case 'e':
                            Enemy e = new Enemy(x, y);
                            e.X = x;
                            e.Y = y;
                            break;
                    }
                }
            }
            return room;
        }
    }
}
