using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    /// <summary>
    /// ParticleSystem is an abstract class that provides the basic functionality to
    /// create a particle effect. Different subclasses will have different effects,
    /// such as fire, explosions, and plumes of smoke. To use these subclasses, 
    /// simply call AddParticles, and pass in where the particles should exist
    /// </summary>
    public abstract class ParticleSystem : DrawableGameComponent
    {
        // these two values control the order that particle systems are drawn in.
        // typically, particles that use additive blending should be drawn on top of
        // particles that use regular alpha blending. ParticleSystems should therefore
        // set their DrawOrder to the appropriate value in InitializeConstants, though
        // it is possible to use other values for more advanced effects.
        public const int AlphaBlendDrawOrder = 100;
        public const int AdditiveDrawOrder = 200;

        // a reference to the main game; we'll keep this around because it exposes a
        // content manager and a sprite batch for us to use.
        protected Game1 game;

        // this number represents the maximum number of effects this particle system
        // will be expected to draw at one time. this is set in the constructor and is
        // used to calculate how many particles we will need.
        private int howManyEffects;
        
        // the array of particles used by this system. these are reused, so that calling
        // AddParticles will not cause any allocations.
        protected Particle[] particles;

        // the queue of free particles keeps track of particles that are not curently
        // being used by an effect. when a new effect is requested, particles are taken
        // from this queue. when particles are finished they are put onto this queue.
        protected Queue<Particle> freeParticles;

        /// <summary>
        /// returns the number of particles that are available for a new effect.
        /// </summary>
        protected int FreeParticleCount
        {
            get { return freeParticles.Count; }
        }

        // This region of values control the "look" of the particle system, and should 
        // be set by deriving particle systems in the InitializeConstants method. The
        // values are then used by the virtual function InitializeParticle. Subclasses
        // can override InitializeParticle for further
        // customization.
        #region constants to be set by subclasses

        /// <summary>
        /// minNumParticles and maxNumParticles control the number of particles that are
        /// added when AddParticles is called. The number of particles will be a random
        /// number between minNumParticles and maxNumParticles.
        /// </summary>
        protected int minNumParticles;
        protected int maxNumParticles;
       
        /// <summary>
        /// minInitialSpeed and maxInitialSpeed are used to control the initial velocity
        /// of the particles. The particle's initial speed will be a random number 
        /// between these two. The direction is determined by the function 
        /// PickRandomDirection, which can be overriden.
        /// </summary>
        protected float minInitialSpeed;
        protected float maxInitialSpeed;

        /// <summary>
        /// minAcceleration and maxAcceleration are used to control the acceleration of
        /// the particles. The particle's acceleration will be a random number between
        /// these two. By default, the direction of acceleration is the same as the
        /// direction of the initial velocity.
        /// </summary>
        protected float minAcceleration;
        protected float maxAcceleration;

        /// <summary>
        /// minRotationSpeed and maxRotationSpeed control the particles' angular
        /// velocity: the speed at which particles will rotate. Each particle's rotation
        /// speed will be a random number between minRotationSpeed and maxRotationSpeed.
        /// Use smaller numbers to make particle systems look calm and wispy, and large 
        /// numbers for more violent effects.
        /// </summary>
        protected float minRotationSpeed;
        protected float maxRotationSpeed;

        /// <summary>
        /// minLifetime and maxLifetime are used to control the lifetime. Each
        /// particle's lifetime will be a random number between these two. Lifetime
        /// is used to determine how long a particle "lasts." Also, in the base
        /// implementation of Draw, lifetime is also used to calculate alpha and scale
        /// values to avoid particles suddenly "popping" into view
        /// </summary>
        protected float minLifetime;
        protected float maxLifetime;

        /// <summary>
        /// to get some additional variance in the appearance of the particles, we give
        /// them all random scales. the scale is a value between minScale and maxScale,
        /// and is additionally affected by the particle's lifetime to avoid particles
        /// "popping" into view.
        /// </summary>
        protected float minScale;
        protected float maxScale;

        /// <summary>
        /// different effects can use different blend modes. fire and explosions work
        /// well with additive blending, for example.
        /// </summary>
        protected SpriteBlendMode spriteBlendMode;

        #endregion
        
        /// <summary>
        /// Constructs a new ParticleSystem.
        /// </summary>
        /// <param name="game">The host for this particle system. The game keeps the 
        /// content manager and sprite batch for us.</param>
        /// <param name="howManyEffects">the maximum number of particle effects that
        /// are expected on screen at once.</param>
        /// <remarks>it is tempting to set the value of howManyEffects very high.
        /// However, this value should be set to the minimum possible, because
        /// it has a large impact on the amount of memory required, and slows down the
        /// Update and Draw functions.</remarks>
        protected ParticleSystem(Game1 game, int howManyEffects)
            : base(game)
        {            
            this.game = game;
            this.howManyEffects = howManyEffects;
        }

        /// <summary>
        /// override the base class's Initialize to do some additional work; we want to
        /// call InitializeConstants to let subclasses set the constants that we'll use.
        /// 
        /// also, the particle array and freeParticles queue are set up here.
        /// </summary>
        public override void Initialize()
        {
            InitializeConstants();
            
            // calculate the total number of particles we will ever need, using the
            // max number of effects and the max number of particles per effect.
            // once these particles are allocated, they will be reused, so that
            // we don't put any pressure on the garbage collector.
            particles = new Particle[howManyEffects * maxNumParticles];
            freeParticles = new Queue<Particle>(howManyEffects * maxNumParticles);
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle();
                freeParticles.Enqueue(particles[i]);
            }
            base.Initialize();
        }

        /// <summary>
        /// this abstract function must be overriden by subclasses of ParticleSystem.
        /// It's here that they should set all the constants marked in the region
        /// "constants to be set by subclasses", which give each ParticleSystem its
        /// specific flavor.
        /// </summary>
        protected abstract void InitializeConstants();

        public abstract void AddParticles(Vector2 where);

        protected abstract void InitializeParticle(Particle p, Vector2 where);       
       
    }
}
