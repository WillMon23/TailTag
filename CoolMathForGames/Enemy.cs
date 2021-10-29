using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace TailTag
{
    class Enemy : Actor
    {

        private float _speed;

        private Vector2 _volocity;
        
        private Actor _target;

        private float _lineOfSightRange = 300f;

        private bool _alive = true;

        Scene _currentScene;

        float _tally;

        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector2 Volocity { get { return _volocity; } set { _volocity = value; } }

        public bool Alive { get { return _alive; } }

        /// <summary>
        /// Enemy Contructor 
        /// What defines to be a enemy 
        /// </summary>
        /// <param name="icon">What it looks like in the console</param>
        /// <param name="x">x cooridinet position</param>
        /// <param name="y">y cooridinet position</param>
        /// <param name="name"> classification</param>
        /// <param name="color">There Color</param>
        public Enemy( float x, float y, float speed, string name, Scene currentScene, Actor target, string path = "") : base( x, y, name, path)
        {
            _speed = speed;
            _target = target;
            _currentScene = currentScene;
        }

        public override void Start()
        {
            base.Start();

            _tally = 0;


            Volocity = new Vector2 { X = 2, Y = 2 };
        }

        public override void Update(float deltaTime)
        {

            Fallow(deltaTime);
            if (GetTargetInSight())
            {
                
                Position += Volocity.Normalzed * Speed * deltaTime;
                if (_tally >= .55)
                {
                    AddBullet();
                    _tally = 0;
                }
            }
            _tally += deltaTime;

            if (!Alive)
                _currentScene.RemoveActor(this);

        }
        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "PlayerBullet")
            { 
                _alive = false;
                //_currentScene.RemoveActor(this);
            }
        }

        /// <summary>
        /// If the 
        /// </summary>
        /// <returns></returns>
        private bool GetTargetInSight()
        {
            Vector2 directionTarget = (_target.Position - Position).Normalzed;

            float distance = Vector2.Distance(_target.Position, Position);

            float cosTarget = distance / Position.Magnitude;

            return cosTarget < _lineOfSightRange && (distance < _lineOfSightRange) && Vector2.DotProduct(directionTarget, Forward) < 0;
        }

        /// <summary>
        /// Creats Bullets to be sepolyed by the enemy 
        /// </summary>
        private void AddBullet()
        {
            Random rng = new Random();

            int chance = rng.Next(1, 5);

            Bullet shot = new Bullet(Position, (Speed * 2), new Vector2(-1, 0),_currentScene, "EnemyBullet", "Images/bullet.png");


            if (chance == 1)
                shot = new Bullet(Position, (Speed * 3),  new Vector2(-1, 0), _currentScene, "EnemyBullet", "Images/bullet.png");

            else if (chance == 2)
                shot = new Bullet(Position, (Speed * 4),  new Vector2(-1, 0), _currentScene, "EnemyBullet", "Images/bullet.png");

            else if (chance >= 3)
                 shot = new Bullet(Position, (Speed * 5),  new Vector2(-1, 0), _currentScene, "EnemyBullet", "Images/bullet.png");

            shot.SetScale(50, 50);

            CircleCollider shotCircleCollider = new CircleCollider(10, shot);
            shot.Collider = shotCircleCollider;


           _currentScene.AddActor(shot);
        }
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        /// <summary>
        /// Fallows Player
        /// </summary>
        private void Fallow(float deltaTime)
        {
            Volocity = _target.Position - Position;

            Position += Volocity.Normalzed * 10 * deltaTime;
        }

    }
}
