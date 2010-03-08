using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    public abstract class ParticleSystem : DrawableGameComponent
    {        
        protected Game1 game;
        private int maxConcurrentEffects;
        protected string textureFilename;
        protected Particle[] particles;
        protected Queue<Particle> freeParticles;
        protected float minStartSpeed;
        protected float maxStartSpeed;
        protected float minAcceleration;
        protected float maxAcceleration;
        protected float minLifetime;
        protected float maxLifetime;
        protected float minScale;
        protected float maxScale;
        protected float minRotationSpeed;
        protected float maxRotationSpeed;
        protected int minNumParticles;
        protected int maxNumParticles;
        protected int minInitialSpeed;
        protected int maxInitialSpeed;
        protected SpriteBlendMode spriteBlendMode;

        public const int AlphaBlendDrawOrder = 100; 
        public const int AdditiveDrawOrder = 200; 

        protected ParticleSystem(Game1 game, int maxConcurrentEffects)
            : base(game)
        {            
            this.game = game;
            this.maxConcurrentEffects = maxConcurrentEffects;
        }

        public override void Initialize()
        {
            // calculate the total number of particles we will ever need, using the
            // max number of effects and the max number of particles per effect.
            // once these particles are allocated, they will be reused, so that
            // we don't put any pressure on the garbage collector.
            particles = new Particle[maxConcurrentEffects * maxNumParticles];
            freeParticles = new Queue<Particle>(maxConcurrentEffects * maxNumParticles);
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle();
                freeParticles.Enqueue(particles[i]);
            }
            base.Initialize();

            //TODO: validate settings in ParticleSystemSettings.
        }

        protected abstract void InitializeParameters();

        public void AddParticles(Vector2 effectLocation)
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
                InitializeParticle(p, effectLocation);
            }
        }

        /// <summary>
        /// InitializeParticle randomizes some properties for a particle, then
        /// calls initialize on it. It can be overriden by subclasses if they 
        /// want to modify the way particles are created.
        /// </summary>
        /// <param name="particle">the particle to initialize</param>
        /// <param name="particleLocation">the position on the screen that the particle should be
        /// </param>
        protected virtual void InitializeParticle(Particle particle, Vector2 particleLocation)
        {
            // first, call PickRandomDirection to figure out which way the particle
            // will be moving. velocity and acceleration's values will come from this.
            Vector2 direction = UtilityMethods.PickRandomDirection();

            // pick some random values for our particle
            float velocity =
                UtilityMethods.RandomBetween(minStartSpeed, maxStartSpeed);
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
            particle.Initialize(particleLocation, velocity * direction, acceleration * direction,
                lifetime, scale, rotationSpeed);
        }

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

        public override void Draw(GameTime gameTime)
        {
            // tell sprite batch to begin, using the spriteBlendMode specified in initializeConstants
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

                game.spriteBatch.Draw(Textures.Instance.SpriteSheet.Texture, p.Position, sourceRectangle,
                            color, p.Rotation, new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2),
                                scale, SpriteEffects.None, 0.5f);

            }

            game.spriteBatch.End();

            base.Draw(gameTime);
        }       
       
    }
}
