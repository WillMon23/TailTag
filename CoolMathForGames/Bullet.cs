using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace TailTag
{
    class Bullet : Actor
    { 

        private float _speed;

        private Vector2 _volocity;

        private Scene _currentScene;

        private bool _alive = true;

        float _tally;


        public float Speed { get { return _speed; } } 

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public bool Alive { get { return _alive; } }

        public Bullet(Vector2 postion, float speed, Vector2 volocoty, Scene currentScene, string name = "Bullet", string path = "") : base(postion, name, path)
        {
            _currentScene = currentScene;
            _speed = speed;
            _volocity = volocoty;
        }

        public Bullet() { }


        public override void Start()
        {
            _tally = 0f; 
            base.Start();
            
        }

        public override void Update(float deltaTime)
        {
            float bulletSpeed = Speed * 2;

            Position +=  Volocity * Speed * deltaTime;

            //if (!_alive)
            //    _currentScene.RemoveActor(this);

            if (_tally >= 5000)
            {
                _currentScene.RemoveActor(this);
                //this.End();
                _tally = 0;
            }
            _tally++;

        }
        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }

        public override void OnCollision(Actor actor)
        {
            if(actor.Name == "Player")
                _currentScene.RemoveActor(this);


            if (actor.Name == "PlayerBullet")
                _currentScene.RemoveActor(this);

            if (actor.Name == "EnemyBullet")
                _currentScene.RemoveActor(this);

            if (actor.Name == "Enemy")
                _currentScene.RemoveActor(this);


        }
    }
}
