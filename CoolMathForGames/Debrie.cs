using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace TailTag
{
    class Debrie : Actor 
    {
        public Debrie( float x, float y, Color color, string name, string path = "") : base(  x,  y, name, path)
        {

        }

        public override void Update(float deltaTime)
        {
            Random rng = new Random();

            int chance = rng.Next(1, 5);


            
        }
    }
}
