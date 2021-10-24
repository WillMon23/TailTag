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

        public Actor Target { get { return _target; } }

        public float Speed { get { return _speed; } } 

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public Bullet(Char icon, Vector2 postion, Color color, Actor target, float speed, float collision, string name = "Bullet") : base(icon, postion, color, collision, name)
        {
            _target = target;
            _speed = speed;
        }

        public Bullet() { }


        public override void Start()
        {
            base.Start();
            _volocity = Target.Posistion;
        }

        public override void Update(float deltaTime)
        {
            float bulletSpeed = Speed / 2;

            Volocity = Target.Posistion - Posistion;

            Posistion += new Vector2(-1,0) * Speed * deltaTime;
        }

        public override void OnCollision(Actor actor)
        {
            
        }
    }
}
