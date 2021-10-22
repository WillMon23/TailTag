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

        public float Speed { get { return _speed; } } 

        public Bullet(Char icon, Vector2 postion, Color color, Actor target, float speed, string name = "Bullet") : base(icon, postion, color, name)
        {
            _target = target;
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            float bulletSpeed = Speed * 2;

            Vector2 direction = _target.Posistion - Posistion;

            Posistion += direction.Normalzed * bulletSpeed * deltaTime;
        }
    }
}
