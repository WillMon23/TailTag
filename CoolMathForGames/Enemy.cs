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

        private float _lineOfSightRange = 200f;

        private bool _alive = true;

        Scene _currentScene;

        int _tally;

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
        public Enemy(char icon, float x, float y, float speed, string name, Scene currentScene, Actor target, Color color) : base(icon, x, y, color, name)
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

            Volocity = _target.Posistion - Posistion;

            Posistion += Volocity.Normalzed * 10 * deltaTime;

            //Posistion += Volocity.Normalzed * Speed * deltaTime;
            if (GetTargetInSight())
            {
                
                Posistion += Volocity.Normalzed * Speed * deltaTime;
                if (_tally >= 1000)
                {
                    AddBullet();
                    _tally = 0;
                }
            }
            _tally++;

        }
        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "PlayerBullet")
            { 
                //_alive = false;
            }
        }

        /// <summary>
        /// If the 
        /// </summary>
        /// <returns></returns>
        private bool GetTargetInSight()
        {
            Vector2 directionTarget = (_target.Posistion - Posistion).Normalzed;

            float distance = Vector2.Distance(_target.Posistion, Posistion);

            float cosTarget = distance / Posistion.Magnitude;

            return cosTarget < _lineOfSightRange && (distance < _lineOfSightRange) && Vector2.DotProduct(directionTarget, Forward) < 0;
        }

        /// <summary>
        /// Creats Bullets to be sepolyed by the enemy 
        /// </summary>
        private void AddBullet()
        {
            Random rng = new Random();

            int chance = rng.Next(1, 5);

            Bullet shot = new Bullet('.', Posistion, Color.GREEN, (Speed * 2), new Vector2(-1, 0),_currentScene, "EnemyBullet");


            if (chance == 1)
                shot = new Bullet('.', Posistion, Color.RED, (Speed * 3),  new Vector2(-1, 0), _currentScene, "EnemyBullet");

            else if (chance == 2)
                shot = new Bullet('.', Posistion, Color.BLUE, (Speed * 4),  new Vector2(-1, 0), _currentScene, "EnemyBullet");

            else if (chance >= 3)
                 shot = new Bullet('.', Posistion, Color.GREEN, (Speed * 5),  new Vector2(-1, 0), _currentScene, "EnemyBullet");

           _currentScene.AddActor(shot);
        }
        
        private void Fallow()
        {

        }

    }
}
