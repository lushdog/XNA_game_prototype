#region File Description
//-----------------------------------------------------------------------------
// Particle.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    public class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Acceleration;

        private float lifetime;
        public float Lifetime
        {
            get { return lifetime; }
            set { lifetime = value; }
        }

        private float timeSinceStart;
        public float TimeSinceStart
        {
            get { return timeSinceStart; }
            set { timeSinceStart = value; }
        }

        private float scale;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Rotation in radians
        /// </summary>
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private float rotationSpeed;
        public float RotationSpeed
        {
            get { return rotationSpeed; }
            set { rotationSpeed = value; }
        }

        public bool Active
        {
            get { return TimeSinceStart < Lifetime; }
        }
        
        public void Initialize(Vector2 position, Vector2 velocity, Vector2 acceleration,
        float lifetime, float scale, float rotationSpeed)
        {
            // set the values to the requested values
            this.Position = position;
            this.Velocity = velocity;
            this.Acceleration = acceleration;
            this.Lifetime = lifetime;
            this.Scale = scale;
            this.RotationSpeed = rotationSpeed;
            
            // reset TimeSinceStart - we have to do this because particles will be
            // reused.
            this.TimeSinceStart = 0.0f;

            // set rotation to some random value between 0 and 360 degrees.
            this.Rotation = UtilityMethods.RandomBetween(0, MathHelper.TwoPi);
        }

        public void Update(float dt)
        {
            Velocity += Acceleration * dt;
            Position += Velocity * dt;
            Rotation += RotationSpeed * dt;
            TimeSinceStart += dt;
        }
    }
}
