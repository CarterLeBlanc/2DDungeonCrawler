using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphicalTestApp
{
    class RoomControl : Actor
    {
        public static Room[] Rooms = new Room[5];

        public RoomControl()
        {
            RoomStart();
        }

        //List of Rooms
        public Room MainRoom = new Room();
        Room NorthRoom = new Room();
        Room SouthRoom = new Room();
        Room EastRoom = new Room();
        Room WestRoom = new Room();

        public void RoomStart()
        {
            MainRoom.LoadRoom("Rooms/MainRoom.txt");
            Rooms[0] = MainRoom;
            
            NorthRoom.LoadRoom("Rooms/NorthRoom.txt");
            Rooms[1] = NorthRoom;

            SouthRoom.LoadRoom("Rooms/SouthRoom.txt");
            Rooms[2] = SouthRoom;

            EastRoom.LoadRoom("Rooms/EastRoom.txt");
            Rooms[3] = EastRoom;

            WestRoom.LoadRoom("Rooms/WestRoom.txt");
            Rooms[4] = WestRoom;

            Rooms[0].North = Rooms[1];
            Rooms[1].South = Rooms[0];

            Rooms[0].South = Rooms[2];
            Rooms[2].North = Rooms[0];

            Rooms[0].East = Rooms[3];
            Rooms[3].West = Rooms[0];

            Rooms[0].West = Rooms[4];
            Rooms[4].East = Rooms[0];
        }
    }
}
