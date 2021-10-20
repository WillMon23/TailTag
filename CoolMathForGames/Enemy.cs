using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace CoolMathForGames
{
    class Enemy : Actor
    {

        private float _speed;
        private Vector2 _volocity;
        private Player _player;

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
        public Enemy(char icon, float x, float y, float speed, string name, Player player, Color color) : base(icon, x, y, color, name)
        {
            _speed = speed;
            _player = player;
        }

        public override void Start()
        {
            base.Start();

            Volocity = new Vector2 { X = 2, Y = 2 };
        }

        public override void Update(float deltaTime)
        {
            Volocity = _player.Posistion - Posistion;

            Posistion += Volocity.Normalzed * Speed * deltaTime;

            //Posistion += -Volocity.Normalzed * Speed * deltaTime;


        }

        public override void OnCollision(Actor actor)
        {
            //if(actor.Name == "Wall")
            // Posistion -= Volocity;

            Fallow();
        }

        private void Fallow()
        {

        }
    }
}
