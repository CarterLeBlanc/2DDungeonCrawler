using System;
using Raylib;
using RL = Raylib.Raylib;

namespace GraphicalTestApp
{
    class Sprite : Actor
    {
        private Texture2D _texture = new Texture2D();
        private Image _image = new Image();

        //The width of the Sprite
        public float Width
        {
            get { return _texture.width; }
        }
        //The height of the Sprite
        public float Height
        {
            get { return _texture.height; }
        }

        //Default constructor
        public Sprite(string path)
        {
            _image = RL.LoadImage(path);
            _texture = RL.LoadTextureFromImage(_image);
            X = -Width / 2;
            Y = -Height / 2;
        }

        //Draw the Sprite to the screen
        public override void Draw()
        {
            RL.DrawTextureEx(
                _texture,
                new Vector2(XAbsolute, YAbsolute),
                GetRotationAbsolute() * (float)(180.0f / Math.PI),
                GetScale(),
                Color.WHITE);
            base.Draw();
        }
    }
}
