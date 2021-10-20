using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace TailTag
{
    class UIText : Actor
    {

        private string _text;
        private int _width;
        private int _height;
        public int FontSize;
        public Font Font;

        /// <summary>
        /// Text being utalized 
        /// </summary>
        public string Text { get { return _text; } set { _text = value; } }

        public int Width { get { return _width; } set { _width = value; } }

        public int Height { get { return _height; } set { _height = value; } }



        public UIText(float x, float y, string name, Color color, int width, int height, int fontSize, string text = "")
            : base('\0', x, y, color, name)
        {
            _text = text;
            _width = width;
            _height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
        }

        public override void Draw()
        {
            Rectangle textBox = new Rectangle(Posistion.X, Posistion.Y, Width, Height);
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, Icon.Color);
        }
        

    }
}
