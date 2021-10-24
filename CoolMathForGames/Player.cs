using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace TailTag
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _volocity;
        private Scene _currentScene;
        private int _tally;
        private bool _alive = false;


        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public bool Alive {  get { return _alive; } }

        public Player(char icon, float x, float y, float speed, Color color, float collision, Scene currentScne, string name = "Actor") 
            :base( icon,  x,  y,color,collision,  name = "Actor"  )
        {
            _speed = speed;
            _currentScene = currentScne;
            _tally = 0;

        }

        public override void Update(float deltaTime)
        {

            if (_tally >= 1000)
            {
                if ((Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)))
                {
                    AddBullet();
                    _tally = 0;
                }
            }
            _tally++;


            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) 
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));

            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + 
                Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));


            Vector2 moveDirecton = new Vector2(xDirection, yDirection);

            Volocity =  moveDirecton.Normalzed * Speed * deltaTime ;

            Posistion += Volocity.Normalzed;

            base.Update(deltaTime);

        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Icon.Symbol == '.')
                _alive = true;

        }


        private void AddBullet()
        {
            Random rng = new Random();

            int chance = rng.Next(1, 5);

            Bullet pShot = new Bullet('.', Posistion, Color.GREEN, (Speed * 2), 10, new Vector2(1,0), _currentScene);

                if (chance == 1)
                    pShot = new Bullet('.', Posistion, Color.RED, (Speed * 2), 10, new Vector2(1, 0), _currentScene);

                else if (chance == 2)
                    pShot = new Bullet('.', Posistion, Color.BLUE, (Speed * 2), 10, new Vector2(1, 0), _currentScene);

                else if (chance >= 3)
                    pShot = new Bullet('.', Posistion, Color.GREEN, (Speed * 2), 10, new Vector2(1, 0), _currentScene);
            



            _currentScene.AddActor(pShot);
        }


    }
}
