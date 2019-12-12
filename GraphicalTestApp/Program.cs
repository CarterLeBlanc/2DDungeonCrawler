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
            RoomControl CurrentRoom = new RoomControl();
            Player player = new Player(100, 100, RoomControl.Rooms[0], root);

            RoomControl.Rooms[0].AddChild(player);
            root.AddChild(RoomControl.Rooms[0]);

            

            game.Run();
        }
    }
}
