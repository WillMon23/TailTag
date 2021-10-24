using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace TailTag
{
    class Wall : Actor
    {
        private Scene _currentScene;

        public Wall(char icon, float x, float y, Color color, float collision, Scene currentScene, string name = "Wall" ) : base(icon, x, y, color, collision, name)
        {
            _currentScene = currentScene;
        }

       public void Borders()
        {
            for (int i = 0; i < 800; i++)
                for (int j = 0; j < 450; j++)
                    _currentScene;
        }


    }
}
