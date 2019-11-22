using System;

namespace GraphicalTestApp
{
    class AABB : Actor
    {
        private Vector3 _min = new Vector3(float.NegativeInfinity,
                                    float.NegativeInfinity,
                                    float.NegativeInfinity);
        private Vector3 _max = new Vector3(float.PositiveInfinity,
                                    float.PositiveInfinity,
                                    float.PositiveInfinity);

        public float Width { get; set; } = 1;
        public float Height { get; set; } = 1;

        //Returns the Y coordinate at the top of the box
        public float Top
        {
            get { return YAbsolute - Height / 2; }
        }

        //Returns the Y coordinate at the top of the box
        public float Bottom
        {
            get { return YAbsolute + Height / 2; }
        }

        //Returns the X coordinate at the top of the box
        public float Left
        {
            get { return XAbsolute - Width / 2; }
        }

        //Returns the X coordinate at the top of the box
        public float Right
        {
            get { return XAbsolute + Width / 2; }
        }

        //Creates an AABB of the specifed size
        public AABB(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public bool DetectCollision(AABB other)
        {
            //## Implement DetectCollision(AABB) ##//
            return !(_max.x < other._min.x || _max.y < other._min.y ||
                _min.x > other._max.x || _min.y > other._max.y);
        }

        public bool DetectCollision(Vector3 point)
        {
            //## Implement DetectCollision(Vector3) ##//
            return !(point.x < _min.x || point.y < _min.y ||
                point.x > _max.x || point.y > _max.y);
        }

        //Draw the bounding box to the screen
        public override void Draw()
        {

            Raylib.Rectangle rec = new Raylib.Rectangle(

                XAbsolute - Width / 2,

                YAbsolute - Height / 2,

                Width,

                Height);

            Raylib.Raylib.DrawRectangleLinesEx(rec, 1, Raylib.Color.RED);

            base.Draw();
        }
    }
}
