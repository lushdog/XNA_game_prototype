using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    public abstract class EmitterParticleSystem : ParticleSystem
    {
        
        /// <summary>
        /// this controls the texture that the particle system uses. It will be used as
        /// an argument to ContentManager.Load.
        /// </summary>
        protected string textureFilename;

        // the origin when we're drawing textures. this will be the middle of the
        // texture.
        private Vector2 origin;

        protected EmitterParticleSystem(Game1 game, int howManyEffects)
            : base(game, howManyEffects)
        {}

        /// <summary>
        /// AddParticles's job is to add an effect somewhere on the screen. If there 
        /// aren't enough particles in the freeParticles queue, it will use as many as 
        /// it can. This means that if there not enough particles available, calling
        /// AddParticles will have no effect.
        /// </summary>
        /// <param name="where">where the particle effect should be created</param>
        public override void AddParticles(Vector2 where)
        {
            // the number of particles we want for this effect is a random number
            // somewhere between the two constants specified by the subclasses.
            int numParticles =
                new Random().Next(minNumParticles, maxNumParticles);

            // create that many particles, if you can.
            for (int i = 0; i < numParticles && freeParticles.Count > 0; i++)
            {
                // grab a particle from the freeParticles queue, and Initialize it.
                Particle p = freeParticles.Dequeue();
                InitializeParticle(p, where);
            }
        }

        /// <summary>
        /// InitializeParticle randomizes some properties for a particle, then
        /// calls initialize on it. It can be overriden by subclasses if they 
        /// want to modify the way particles are created. For example, 
        /// SmokePlumeParticleSystem overrides this function make all particles
        /// accelerate to the right, simulating wind.
        /// </summary>
        /// <param name="p">the particle to initialize</param>
        /// <param name="where">the position on the screen that the particle should be
        /// </param>
        protected override void InitializeParticle(Particle p, Vector2 where)
        {
            // first, call PickRandomDirection to figure out which way the particle
            // will be moving. velocity and acceleration's values will come from this.
            Vector2 direction = UtilityMethods.PickRandomDirection();

            // pick some random values for our particle
            float velocity =
                UtilityMethods.RandomBetween(minInitialSpeed, maxInitialSpeed);
            float acceleration =
                UtilityMethods.RandomBetween(minAcceleration, maxAcceleration);
            float lifetime =
                UtilityMethods.RandomBetween(minLifetime, maxLifetime);
            float scale =
                UtilityMethods.RandomBetween(minScale, maxScale);
            float rotationSpeed =
                UtilityMethods.RandomBetween(minRotationSpeed, maxRotationSpeed);

            // then initialize it with those random values. initialize will save those,
            // and make sure it is marked as active.
            p.Initialize(
                where, velocity * direction, acceleration * direction,
                lifetime, scale, rotationSpeed);
        }

        /// <summary>
        /// overriden from DrawableGameComponent, Update will update all of the active
        /// particles.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // calculate dt, the change in the since the last frame. the particle
            // updates will use this value.
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // go through all of the particles...
            foreach (Particle p in particles)
            {

                if (p.Active)
                {
                    // ... and if they're active, update them.
                    p.Update(dt);
                    // if that update finishes them, put them onto the free particles
                    // queue.
                    if (!p.Active)
                    {
                        freeParticles.Enqueue(p);
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// overriden from DrawableGameComponent, Draw will use ParticleSampleGame's 
        /// sprite batch to render all of the active particles.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // tell sprite batch to begin, using the spriteBlendMode specified in
            // initializeConstants
            game.spriteBatch.Begin(spriteBlendMode, SpriteSortMode.BackToFront, SaveStateMode.None, Settings.Instance.Resolution.Scale);
            
            foreach (Particle p in particles)
            {
                // skip inactive particles
                if (!p.Active)
                    continue;

                // normalized lifetime is a value from 0 to 1 and represents how far
                // a particle is through its life. 0 means it just started, .5 is half
                // way through, and 1.0 means it's just about to be finished.
                // this value will be used to calculate alpha and scale, to avoid 
                // having particles suddenly appear or disappear.
                float normalizedLifetime = p.TimeSinceStart / p.Lifetime;

                // we want particles to fade in and fade out, so we'll calculate alpha
                // to be (normalizedLifetime) * (1-normalizedLifetime). this way, when
                // normalizedLifetime is 0 or 1, alpha is 0. the maximum value is at
                // normalizedLifetime = .5, and is
                // (normalizedLifetime) * (1-normalizedLifetime)
                // (.5)                 * (1-.5)
                // .25
                // since we want the maximum alpha to be 1, not .25, we'll scale the 
                // entire equation by 4.
                float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
                Color color = new Color(new Vector4(1, 1, 1, alpha));

                // make particles grow as they age. they'll start at 75% of their size,
                // and increase to 100% once they're finished.
                float scale = p.Scale * (.75f + .25f * normalizedLifetime);

                Rectangle sourceRectangle = Textures.Instance.SpriteSheet.SourceRectangle(textureFilename);

                game.spriteBatch.Draw(Textures.Instance.SpriteSheet.Texture, p.Position, sourceRectangle ,
                            color, p.Rotation, new Vector2(sourceRectangle.Width/2, sourceRectangle.Height/2),
                                scale, SpriteEffects.None, 0.5f);

            }

            game.spriteBatch.End();

            base.Draw(gameTime);
        }       

    }
}
