using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace TailTag
{
    class Actor
    {
        private string _name;
        private bool _started;
        private Vector2 _froward = new Vector2(1, 0);
        private Collider _collider;
        private Sprite _sprite;

        private Matrix3 _transform = Matrix3.Identity;
        private Matrix3 _scaler = Matrix3.Identity;

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started { get { return _started; } }

        public Vector2 Position { get { return new Vector2(_transform.M02, _transform.M12); } 
                                  set { _transform.M02 = value.X; _transform.M12 = value.Y; } }

        public string Name { get { return _name; } }

        public Vector2 Forward { get { return _froward; } set { _froward = value; } }

        public Collider Collider { get { return _collider; } set { _collider = value; } }

        public Actor() 
        {
          
        }

        public Actor( Vector2 position,  string name = "Actor", string path = "")
        {
            _name = name;
            Position = position;
            if (path != "")
                _sprite = new Sprite(path);


        }

        public Actor( float x, float y, string name = "Actor", string path = "") :
            this( new Vector2 { X = x, Y = y }, name, path){ }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {
            Console.WriteLine(Name + ": " + Position.X + " , " + Position.Y);
        }

        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(_transform);
            Collider.Draw();

            //Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.X, (int)Position.Y, 20  , Color.WHITE);
            //Raylib.DrawCircleLines((int)Posistion.X, (int)Posistion.Y, CollisionRadius , Color.LIME);
        }

        public virtual void End()
        {

        }

        public virtual void OnCollision( Actor actor)
        {
           
        }

        public virtual bool CheckForColision(Actor other)
        {
            if (Collider == null || other == null)
                return false;
            return Collider.CheckCollision(other);
        }
        public void SetScale(float x, float y)
        {
            _scaler.M00 = x;   
            _scaler.M11 = y;

            _transform *= _scaler;

        }




    }
}
