using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace TailTag
{
    class Debrie : Actor 
    {
        public Debrie(char icon, float x, float y, Color color, string name) : base( icon,  x,  y,  color, name)
        {

        }

        public override void Update(float deltaTime)
        {
            Random rng = new Random();

            int chance = rng.Next(1, 5);


            
        }
    }
}
