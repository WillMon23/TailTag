using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace TailTag
{
    public struct Icon
    {
        public char Symbol;
        public Color Color;
    }
    class Actor
    {
        private Icon _icon;
        private string _name;
        private Vector2 _position;
        private bool _started;
        private Vector2 _froward = new Vector2(1,0);
        private Collider _collider;

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started { get { return _started; } }

        public Vector2 Position { get { return _position; } set { _position = value; } }
        
        public Icon Icon { get { return _icon; } set { _icon = value; } }

        public string Name { get { return _name; } }

        public Vector2 Forward { get { return _froward; } set { _froward = value; } }

        public Collider Collider { get { return _collider; } set { _collider = value; } }

        public Actor() 
        {
          
        }

        public Actor(char icon, Vector2 position, Color color, string name = "Actor")
        {
            _icon = new Icon { Symbol = icon, Color = color }; 
            _name = name;
            _position = position;

        }

        public Actor(char icon, float x, float y, Color color, string name = "Actor") :
            this(icon, new Vector2 { X = x, Y = y }, color, name){ }

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
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.X, (int)Position.Y, 20  , Icon.Color);
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

       

    }
}
