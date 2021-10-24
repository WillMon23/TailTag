using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace TailTag
{
    class Bullet : Actor
    {
        private Actor _target;

        private float _speed;

        private Vector2 _volocity; 


        public float Speed { get { return _speed; } } 

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public Bullet(Char icon, Vector2 postion, Color color, float speed, float collision, Vector2 volocoty, string name = "Bullet") : base(icon, postion, color, collision, name)
        {
            
            _speed = speed;
            _volocity = volocoty;
        }

        public Bullet() { }


        public override void Start()
        {
            base.Start();
            
        }

        public override void Update(float deltaTime)
        {
            float bulletSpeed = Speed * 2;

            Posistion +=  Volocity * Speed * deltaTime;
        }

        public override void OnCollision(Actor actor)
        {
            if(actor.Icon.Symbol == '.')
            {
                
            }
        }
    }
}
