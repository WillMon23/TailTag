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

        private Bullet[] _bullets;

        private Bullet _shot;

        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector2 Volocity { get { return _volocity; } set { _volocity = value; } }

        /// <summary>
        /// Enemy Contructor 
        /// What defines to be a enemy 
        /// </summary>
        /// <param name="icon">What it looks like in the console</param>
        /// <param name="x">x cooridinet position</param>
        /// <param name="y">y cooridinet position</param>
        /// <param name="name"> classification</param>
        /// <param name="color">There Color</param>
        public Enemy(char icon, float x, float y, float speed, string name, Actor target, Color color) : base(icon, x, y, color, name)
        {
            _speed = speed;
            _target = target;
        }

        public override void Start()
        {
            base.Start();

            _bullets = new Bullet[0];

            _shot = new Bullet('.', Posistion, Color.RED, _target, (Speed * 2));

            Volocity = new Vector2 { X = 2, Y = 2 };
        }

        public override void Update(float deltaTime)
        {
            int tally = 0;
            for (int i = 0; i < _bullets.Length; i++)
                if (_bullets[i].Started)
                    _bullets[i].Start();
            
            Volocity = _target.Posistion - Posistion;

            
            
            tally++;

            //Posistion += Volocity.Normalzed * Speed * deltaTime;
            if (GetTargetInSight())
            {
                AddBullet();
                if (tally == 1000)
                { 
                    
                    tally = 0;
                }
               

                Posistion += Volocity.Normalzed * Speed * deltaTime;
            }
            _shot.Update(deltaTime);
            _shot.Draw();
            
            UpdateBullet(deltaTime);

        }
        public override void OnCollision(Actor actor)
        { 

            Fallow();
        }

        private bool GetTargetInSight()
        {
            Vector2 directionTarget = (_target.Posistion - Posistion).Normalzed;

            float distance = Vector2.Distance(_target.Posistion, Posistion);

            float cosTarget = distance / Posistion.Magnitude;

            return(distance < _lineOfSightRange) && Vector2.DotProduct(directionTarget, Forward) > 0;
        }

        private void AddBullet()
        {
            Bullet shot = new Bullet('.', Posistion, Color.RED, _target, (Speed * 2));
            Bullet[] temp = new Bullet[_bullets.Length + 1];

            for(int i = 0; i < _bullets.Length; i++)
            {
                temp[i] = _bullets[i];
            }

            temp[_bullets.Length] = shot;

            _bullets = temp;
        }
        
        private void UpdateBullet(float deltaTime)
        {
            for(int i = 0; i < _bullets.Length; i++)
            {
                _bullets[i].Draw();
                _bullets[i].Update(deltaTime);
                
            }
        }

        private void DrawBullet()
        {
            for (int i = 0; i < _bullets.Length; i++)
            {
                _bullets[i].Draw();
            }
        }

        private void Fallow()
        {

        }

        private void ShotPlayer(float deltaTime)
        {
            
        }


    }
}
