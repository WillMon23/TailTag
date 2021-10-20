using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace TailTag
{
    class Enemy : Actor
    {

        private float _speed = 2;

        private Vector2 _volocity;
        
        private Actor _target;

        private float _maxView;

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
        public Enemy(char icon, float x, float y, float speed, string name, float maxView, Actor target, Color color) : base(icon, x, y, color, name)
        {
            _speed = speed;
            _target = target;
            _maxView = maxView;
        }

        public override void Start()
        {
            base.Start();

            Volocity = new Vector2 { X = 2, Y = 2 };
        }

        public override void Draw()
        {
           
        }

        public override void Update(float deltaTime)
        {
             Volocity = _target.Posistion - Posistion;

            //Posistion += Volocity.Normalzed * Speed * deltaTime;
                if (GetTargetInSight())
                    Posistion += Volocity.Normalzed * Speed * deltaTime;

            //Posistion += -Volocity.Normalzed * Speed * deltaTime;


        }

        public override void OnCollision(Actor actor)
        {
            //if(actor.Name == "Wall")
            // Posistion -= Volocity;

            Fallow();
        }

        private bool GetTargetInSight()
        {
            Vector2 directionTarget = (_target.Posistion - Posistion).Normalzed;

            float dotProduct = Vector2.DotProduct(directionTarget, Forward);

            //float distance = Vector2.Distance(_target.Posistion, Posistion);

            float cosTarget = (float)Math.Acosh(dotProduct);

            Console.WriteLine("Cosin Target: " + cosTarget);
            
            return cosTarget < _maxView && dotProduct > 0;
        }
        
        private void Fallow()
        {

        }

    
    }
}
