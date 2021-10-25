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

        private float _health;
        private int _lives;



        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public float Health { get { return _health; } }

        public int Lives { get { return _lives; } }

        public bool Alive {  get { return _alive; } }

        public Player(char icon, float x, float y, float speed, Color color, Scene currentScne, string name = "Player") 
            :base( icon,  x,  y,color,  name = "Player"  )
        {
            _speed = speed;
            _currentScene = currentScne;
            _tally = 0;
            _health = 100;
            _lives = 3;

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
            if (actor.Name == "EnemyBullet")
            { 
                //_alive = false;
            }
        }


        private void AddBullet()
        {
            Random rng = new Random();

            int chance = rng.Next(1, 5);

            Bullet pShot = new Bullet('.', Posistion , Color.GREEN, (Speed * 2), new Vector2(1,0), _currentScene, "PlayerBullet");

                if (chance == 1)
                    pShot = new Bullet('.', Posistion, Color.RED, (Speed * 2),  new Vector2(1, 0), _currentScene, "PlayerBullet");

                else if (chance == 2)
                    pShot = new Bullet('.', Posistion, Color.BLUE, (Speed * 2), new Vector2(1, 0), _currentScene, "PlayerBullet");

                else if (chance >= 3)
                    pShot = new Bullet('.', Posistion, Color.GREEN, (Speed * 2),  new Vector2(1, 0), _currentScene, "PlayerBullet");
            



            _currentScene.AddActor(pShot);
        }


    }
}
