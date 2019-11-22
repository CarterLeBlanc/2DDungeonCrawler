using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(1280, 760, "Graphical Test Application");

            Actor root = new Actor();
            game.Root = root;

            //## Set up game here ##//
            Player player = new Player(12, 76);

            Sword sword = new Sword();
            //player.AddChild(sword);

            Wall wall = new Wall(13, 76);

            Enemy enemy = new Enemy(12, 76);

            root.AddChild(player);
            //root.AddChild(wall);
            //root.AddChild(enemy);

            game.Run();
        }
    }
}
