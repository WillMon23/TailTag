using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace CoolMathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _volocity;
        
        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public Player(char icon, float x, float y, float speed, Color color, string name = "Actor") 
            :base( icon,  x,  y,color,  name = "Actor"  )
        {
            _speed = speed;
            
        }

        public override void Update(float deltaTime)
        {

            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) 
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));

            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + 
                Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            Vector2 moveDirecton = new Vector2(xDirection, yDirection);

            Volocity =  moveDirecton.Normalzed * Speed * deltaTime ;

            Posistion += Volocity;

            base.Update(deltaTime);

        }

        public override void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision Occured");
        }


    }
}
