using System;
using System.Collections.Generic;
using System.IO;

namespace GraphicalTestApp
{
    delegate void Event();
    delegate void StartEvent();
    delegate void UpdateEvent(float deltaTime);
    delegate void DrawEvent();

    class Actor
    {
        public StartEvent OnStart;
        public UpdateEvent OnUpdate;
        public DrawEvent OnDraw;

        public bool Started { get; private set; } = false;
        public Actor Parent { get; private set; } = null;
        public bool Solid { get; set; } = false;
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public static Actor CurrentScene { get; set; }
        public Sprite Sprite { get; set; }
        private bool[,] _collision;

        protected List<Actor> _children = new List<Actor>();
        protected List<Actor> _additions = new List<Actor>();
        protected List<Actor> _removals = new List<Actor>();
        private List<Actor>[,] _tracking;
        private List<Entity>[,] _tracking2;

        private Matrix3 _localTransform = new Matrix3();
        private Matrix3 _globalTransform = new Matrix3();

        public float X
        {
            //## Implement the relative X coordinate ##//
            get { return _localTransform.m13; }

            set
            {
                _localTransform.SetTranslation(value, Y, 1);
                UpdateTransform();
            }
        }
        public float XAbsolute
        {
            //## Implement the absolute X coordinate ##//
            get { return _globalTransform.m13; }
        }
        public float Y
        {
            //## Implement the relative Y coordinate ##//
            get { return _localTransform.m23; }

            set
            {
                _localTransform.SetTranslation(X, value, 1);
            }
        }
        public float YAbsolute
        {
            //## Implement the absolute Y coordinate ##//
            get { return _globalTransform.m23; }
        }

        public float GetRotation()
        {
            //## Implement getting the rotation of _localTransform ##//
            return (float)Math.Atan2(_localTransform.m21, _localTransform.m11);
        }

        public float GetRotationAbsolute()
        {
            return (float)Math.Atan2(_globalTransform.m21, _globalTransform.m11);
        }

        public Vector3 GetDirection()
        {
            return new Vector3(_localTransform.m12, _localTransform.m11, 0);
        }

        public new Vector3 GetDirectionAbsolute()
        {
            return new Vector3(_globalTransform.m12, _globalTransform.m11, 0);
        }

        public void Rotate(float radians)
        {
            //## Implement rotating _localTransform ##//
            _localTransform.RotateZ(radians);
            UpdateTransform();
        }

        public float GetScale()
        {
            //## Implement getting the scale of _localTransform ##//
            return 1;
        }

        public void Scale(float width, float height)
        {
            //## Implement scaling _localTransform ##//
            _localTransform.Scale(width, height, 1);
        }

        public void AddChild(Actor child)
        {
            //## Implement AddChild(Actor) ##//
            if (_additions.Contains(child) || child == null)
            {
                return;
            }

            child.Parent = this;
            _additions.Add(child);
        }

        public void RemoveChild(Actor child)
        {
            //## Implement RemoveChild(Actor) ##//
            if (_removals.Contains(child))
            {
                return;
            }

            _removals.Add(child);
            child.Parent = null;
        }

        public List<Entity> GetEntities(float x, float y)
        {
            int checkX = (int)Math.Round(x);
            int checkY = (int)Math.Round(y);

            if (x >= 0 && y >= 0 && x < SizeX && y < SizeY)
            {
                return _tracking2[checkX, checkY];
            }

            else
            {
                return new List<Entity>(0);
            }
        }

        public void UpdateTransform()
        {
            //## Implment UpdateTransform() ##//
            if (Parent != null)
            {
                _globalTransform = Parent._globalTransform * _localTransform;
            }

            else
            {
                _globalTransform = _localTransform;
            }

            foreach (Actor child in _children)
            {
                child.UpdateTransform();
            }
        }

        //Call the OnStart events of the Actor and its children
        public virtual void Start()
        {
            //Call this Actor's OnStart events
            OnStart?.Invoke();

            //Start all of this Actor's children
            foreach (Actor child in _children)
            {
                child.Start();
            }

            //Flag this Actor as having already started
            Started = true;
        }

        //Call the OnUpdate events of the Actor and its children
        public virtual void Update(float deltaTime)
        {
            //Update this Actor and its children's transforms
            UpdateTransform();

            //Call this Actor's OnUpdate events
            OnUpdate?.Invoke(deltaTime);

            //Add all the Actors readied for addition
            foreach (Actor a in _additions)

            {

                //Add a to _children

                _children.Add(a);

            }

            //Reset the addition list
            _additions.Clear();

            //Remove all the Actors readied for removal

            foreach (Actor a in _removals)

            {
                //Add a to _children
                _children.Remove(a);

            }

            //Reset the removal list
            _removals.Clear();

            //Update all of this Actor's children

            foreach (Actor child in _children)
            {
                child.Update(deltaTime);
            }
        }

        //Call the OnDraw events of the Actor and its children
        public virtual void Draw()
        {
            //Call this Actor's OnDraw events
            OnDraw?.Invoke();

            //Update all of this Actor's children
            foreach (Actor child in _children)
            {
                child.Draw();
            }
        }
    }
}
