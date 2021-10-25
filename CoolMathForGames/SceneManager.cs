using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using System.Diagnostics;

namespace TailTag
{
    class SceneManager
    {
        public void Start()
        {
            //Creats a window  using raylib
            Raylib.InitWindow(800, 450, "Math For Games");

            Raylib.SetTargetFPS(0);

            //Initulises the characters 
            Scene scene = new Scene();


            Player player = new Player('>', 30, 225, 500, Color.PINK, 30, scene, "Player");
            scene.AddActor(player);


            Enemy wampus = new Enemy('<', 800, 100, 20, 50, "Enemy", scene, player, Color.BLUE);
            scene.AddActor(wampus);


            Enemy wampus2 = new Enemy('<', 800, 300, 20, 30, "Enemy", scene, player, Color.BLUE);
            scene.AddActor(wampus2);

            Enemy wampus3 = new Enemy('<', 800, 200, 20, 30, "Enemy", scene, player, Color.BLUE);
            scene.AddActor(wampus3);
        }

        public void Update()
        {

        }






    }
}
