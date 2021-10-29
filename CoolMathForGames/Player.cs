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
        private float _tally;
        private bool _alive = false;

        private float _health;
        private int _lives;



        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public float Health { get { return _health; } }

        public int Lives { get { return _lives; } }

        public bool Alive {  get { return _alive; } }

        public Player( float x, float y, float speed, Scene currentScne, string name = "Player", string path = "") 
            :base( x,  y,  name = "Player", path)
        {
            _speed = speed;
            _currentScene = currentScne;
            _tally = 0;
            _health = 100;
            _lives = 3;

        }

        public override void Update(float deltaTime)
        {

            if (_tally >= .5f)
            {
                if ((Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)))
                {
                    AddBullet();
                    _tally = 0;
                }
            }
             _tally += deltaTime; ;


            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) 
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));

            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + 
                Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));


            Vector2 moveDirecton = new Vector2(xDirection, yDirection);

            Volocity =  moveDirecton.Normalzed * Speed * deltaTime ;

            Position += Volocity.Normalzed;

            base.Update(deltaTime);

        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "EnemyBullet")
            {
                _currentScene.RemoveActor(actor);
                _health -= 10f;
            }
            if (actor.Name == "Enemy")
            {
                _currentScene.RemoveActor(actor);
                _health -= 30f;

            }
        }
        public override void Draw()
        {
            base.Draw();
            //Collider.Draw();
        }


        private void AddBullet()
        {
            Random rng = new Random();

            int chance = rng.Next(1, 5);

            Bullet pShot = new Bullet(Position, (Speed * 2), new Vector2(1,0), _currentScene, "PlayerBullet", "Images/bullet.png");

                if (chance == 1)
                    pShot = new Bullet(Position, (Speed * 2),  new Vector2(1, 0), _currentScene, "PlayerBullet", "Images/bullet.png");

                else if (chance == 2)
                    pShot = new Bullet(Position, (Speed * 2), new Vector2(1, 0), _currentScene, "PlayerBullet", "Images/bullet.png");

                else if (chance >= 3)
                    pShot = new Bullet(Position, (Speed * 2),  new Vector2(1, 0), _currentScene, "PlayerBullet", "Images/bullet.png");
            pShot.SetScale(50, 50);

            CircleCollider pShotCircleCollider = new CircleCollider(10, pShot);
            pShot.Collider = pShotCircleCollider;

            _currentScene.AddActor(pShot);
        }


    }
}
