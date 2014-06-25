using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public class ParticleEmitter
    {
        /********* PARTICLE EFFECT DATA *********/
        protected Texture2D m_ParticleTexture;
        protected List<Particle> m_Particles;

        protected static Random m_Rand;

        protected Vector2 m_Origin;
        protected int m_EmitterDuration;
        protected bool m_IsInfinite;
        protected bool m_IsOn;
        protected ContentManager m_ContentManager;
        protected GraphicsDeviceManager m_Graphics;
        protected EmitterType m_Type;

        // Particle Colors
        protected fColor m_StartColor; // Typically from 0f to 255ff
        protected fColor m_StartColorVariance; // Typically from 0f to 255f
        protected fColor m_EndColor; // Typically from 0f to 255f
        protected fColor m_EndColorVariance; // Typically from 0f to 255f

        // Particle Configuration
        protected int m_MaxParticles; // Typical ranges are 1-1,000
        protected float m_Lifespan; // In milliseconds, typically from 0 to 10,000
        protected float m_LifespanVariance; // In milliseconds, typically from 0 to 10,000
        protected float m_StartSize; // In pixels, typically from 0 to 70
        protected float m_StartSizeVariance; // In pixels, typically from 0 to 70
        protected float m_EndSize; // In pixels, typically from 0 to 70
        protected float m_EndSizeVariance; // In pixels, typically from 0 to 70
        protected float m_EmitterAngle; // In degrees, from 0 to 360 (90 is down, 180 is left, 270 is up, 0 & 360 are right)
        protected float m_EmitterAngleVariance; // In degrees, from 0 to 360
        protected float m_StartRotation; // In degrees, from 0 to 360
        protected float m_StartRotationVariance; // In degrees, from 0 to 360
        protected float m_EndRotation; // In degrees, from 0 to 360
        protected float m_EndRotationVariance; // In degrees, from 0 to 360

        // Gravity (Gravity Emitter)
        protected float m_XVariance; // In pixels, typically from 0 to 1,000
        protected float m_YVariance; // In pixels, typically from 0 to 1,000
        protected float m_Speed; // In pixels, typically from 0 to 500
        protected float m_SpeedVariance; // In pixels, typically from 0 to 500
        protected float m_GravityX; // In pixels, typically from -500 to 500
        protected float m_GravityY; // In pixels, typically from -500 to 500
        protected float m_TangentialAcceleration; // In pixels, typically from -500 to 500
        protected float m_TangentialAccelerationVariance; // In pixels, typically from 0 to 500
        protected float m_RadialAcceleration; // In pixels, typically from -500 to 500
        protected float m_RadialAccelerationVariance; // In pixels, typically from 0 to 500

        // Rotation (Radial Emitter)
        protected float m_MinRadius; // In pixels, typically from 0 to 500
        protected float m_MaxRadius; // In pixels, typically from 0 to 500
        protected float m_MaxRadiusVariance; // In pixels, typically from 0 to 500
        protected float m_Degrees; // In degrees, typically from -360 to 360
        protected float m_DegreesVariance; // In degrees, typically from 0 to 360

        // Blending
        protected BlendState m_BlendType;
        protected SpriteSortMode m_SortMode;

        #region Constructors
        /*  Function:  public ParticleEffect()
         *   Purpose:  Constructor
         ****************************************************************/
        public ParticleEmitter()
        {
            m_ParticleTexture = null;
            m_Particles = new List<Particle>();
            m_Rand = new Random();
        }

        #endregion

        #region LoadContent
        /*  Function:  public abstract void LoadContent(ContentManager content)
         *   Purpose:  This function is abstract and will be defined by a derived class
         ****************************************************************/
        public virtual void LoadContent(ContentManager content)
        {
            m_ParticleTexture = content.Load<Texture2D>("whiteCircle");
        }
        #endregion

        #region Functions
        /*  Function:  public bool isAlive()
         *   Purpose:  This will determine whether or not the particle effect is alive and return a boolean
         ****************************************************************/
        public bool isAlive()
        {
            if (!m_IsInfinite && m_EmitterDuration > 0)
                return true;
            else if (m_IsInfinite)
                return true;
            else if (m_Particles.Count > 0)
                return true;
            else
                return false;
        }

        /*  Function:  private bool shouldEmit()
         *   Purpose:  This will determine whether or not it should emit more particles and return a boolean
         ****************************************************************/
        private bool shouldEmit()
        {
            if (!m_IsInfinite && m_EmitterDuration > 0 && m_IsOn)
                return true;
            else if (m_IsInfinite && m_IsOn)
                return true;
            else
                return false;
        }

        // Returns a number between -1 and 1
        private float rand()
        {
            return (float)(ParticleEmitter.m_Rand.NextDouble() * 2.0 - 1.0);
        }

        #endregion

        #region Initialize
        /*  Function:  public abstract void Initialize()
         *   Purpose:  This function is abstract and will be defined by a derived class
         ****************************************************************/
        protected virtual void Initialize()
        {
            // Misc
            m_Origin = new Vector2(m_Graphics.PreferredBackBufferWidth / 2, m_Graphics.PreferredBackBufferHeight / 2);
            m_EmitterDuration = 0;
            m_IsInfinite = true;
            m_IsOn = true;

            m_Type = EmitterType.GRAVITY;

            m_SortMode = SpriteSortMode.BackToFront;
            m_BlendType = BlendState.AlphaBlend;

            LoadContent(m_ContentManager);

            // Color
            InitializeColorConfig();

            // Particles
            InitializeParticleConfig();

            // Gravity
            InitializeGravityConfig();

            // Radial
            InitializeRadialConfig();
        }

        /*  Function:  public abstract void Initialize(ContentManager content)
         *   Purpose:  This function is abstract and will be defined by a derived class
         ****************************************************************/
        public virtual void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            m_Graphics = graphics;
            m_ContentManager = content;
            Initialize();
        }

        protected void InitializeColorConfig()
        {
            m_StartColor = new fColor(255f, 0f, 0f, 153f);
            m_StartColorVariance = new fColor(0f, 0f, 0f, 0f);

            m_EndColor = new fColor(255f, 127f, 0f, 0f);
            m_EndColorVariance = new fColor(0f, 0f, 0f, 0f);
        }

        protected void InitializeParticleConfig()
        {
            m_MaxParticles = 500;
            m_Lifespan = 2000f;
            m_LifespanVariance = 1900f;
            m_StartSize = 70f;
            m_StartSizeVariance = 49.5f;
            m_EndSize = 10f;
            m_EndSizeVariance = 5f;
            m_EmitterAngle = 270f;
            m_EmitterAngleVariance = 0f;
            m_StartRotation = 0f;
            m_StartRotationVariance = 0f;
            m_EndRotation = 0f;
            m_EndRotationVariance = 0f;
        }

        protected void InitializeGravityConfig()
        {
            m_XVariance = 0f;
            m_YVariance = 0f;
            m_Speed = 100f;
            m_SpeedVariance = 30f;
            m_GravityX = 0f;
            m_GravityY = 0f;
            m_TangentialAcceleration = 0f;
            m_TangentialAccelerationVariance = 0f;
            m_RadialAcceleration = 0f;
            m_RadialAccelerationVariance = 0f;
        }

        protected void InitializeRadialConfig()
        {
            m_MaxRadius = 100f;
            m_MaxRadiusVariance = 0f;
            m_MinRadius = 0f;
            m_Degrees = 0f;
            m_DegreesVariance = 0f;
        }
        #endregion

        #region CreateParticles
        /*  Function:  public abstract void createParticles()
         *   Purpose:  This function is abstract and will be defined by a derived class
         ****************************************************************/
        public virtual void CreateParticles()
        {
            Particle part = new Particle();
            ParticleInfo pInfo = new ParticleInfo();

            float angle = m_EmitterAngle + m_EmitterAngleVariance * rand(),
                  speed = m_Speed + m_SpeedVariance * rand(),
                  startSize = m_StartSize + m_StartSizeVariance * rand(),
                  endSize = m_EndSize + m_EndSizeVariance * rand();

            if (startSize < 0.1f)
                startSize = 0.1f;
            if (endSize < 0.1f)
                endSize = 0.1f;

            pInfo.m_Lifespan = m_Lifespan + m_LifespanVariance * rand();
            pInfo.m_Pos = new Vector2(m_Origin.X + m_XVariance * rand(), m_Origin.Y + m_YVariance * rand());
            pInfo.m_StartPos = new Vector2(m_Origin.X, m_Origin.Y);
            pInfo.m_Velocity = new Vector2(speed * (float)Math.Cos(angle), speed * (float)Math.Sin(angle));
            pInfo.m_EmitterRadius = m_MaxRadius + m_MaxRadiusVariance * rand();
            pInfo.m_EmitterRadiusDelta = (m_MaxRadius - m_MinRadius) / pInfo.m_Lifespan;
            pInfo.m_EmitterRotation = m_EmitterAngle + m_EmitterAngleVariance * rand();
            pInfo.m_EmitterRotationDelta = m_Degrees + m_DegreesVariance * rand();
            pInfo.m_RadialAcceleration = m_RadialAcceleration + m_RadialAccelerationVariance * rand();
            pInfo.m_TangentialAcceleration = m_TangentialAcceleration + m_TangentialAccelerationVariance * rand();
            pInfo.m_Scale = startSize / m_ParticleTexture.Width;
            pInfo.m_ScaleDelta = ((endSize - startSize) / pInfo.m_Lifespan) / m_ParticleTexture.Width;
            pInfo.m_Rotation = m_StartRotation + m_StartRotationVariance * rand();
            pInfo.m_RotationDelta = ((m_EndRotation + m_EndRotationVariance * rand()) - pInfo.m_Rotation) / pInfo.m_Lifespan;
            pInfo.m_Depth = 0f;
            pInfo.m_DepthDelta = (1f / pInfo.m_Lifespan);
            pInfo.m_Speed = m_Speed + m_SpeedVariance * rand();

            float sr = m_StartColor.R,
                  sg = m_StartColor.G,
                  sb = m_StartColor.B,
                  sa = m_StartColor.A,
                  fr = m_EndColor.R,
                  fg = m_EndColor.G,
                  fb = m_EndColor.B,
                  fa = m_EndColor.A;

            sr += m_StartColorVariance.R * rand();
            sg += m_StartColorVariance.G * rand();
            sb += m_StartColorVariance.B * rand();
            sa += m_StartColorVariance.A * rand();

            fr += m_EndColorVariance.R * rand();
            fg += m_EndColorVariance.G * rand();
            fb += m_EndColorVariance.B * rand();
            fa += m_EndColorVariance.A * rand();

            pInfo.m_StartColor = new fColor(sr, sg, sb, sa);

            pInfo.m_EndColor = new fColor(fr, fg, fb, fa);

            pInfo.m_ColorDelta = new fColor((fr - sr) / pInfo.m_Lifespan, 
                                            (fg - sg) / pInfo.m_Lifespan, 
                                            (fb - sb) / pInfo.m_Lifespan, 
                                            (fa - sa) / pInfo.m_Lifespan);

            part.CreateParticle(m_ParticleTexture, m_ParticleTexture.Bounds, pInfo, m_Type);
            m_Particles.Add(part);
        }
        #endregion

        #region Update
        /*  Function:  public virtual void Update(GameTime gameTime)
         *   Purpose:  This will update the particle effect (this can be overriden by a derived class)
         ****************************************************************/
        public virtual void Update(GameTime gameTime)
        {
            if (!m_IsInfinite && m_IsOn)
                m_EmitterDuration -= gameTime.ElapsedGameTime.Milliseconds;

            if (m_Particles.Count < m_MaxParticles && shouldEmit())
            {
                for (int i = 0; i < m_MaxParticles - m_Particles.Count; i++)
                    CreateParticles();
            }

            for (int i = m_Particles.Count() - 1; i >= 0; i--)
            {
                m_Particles[i].Update(gameTime, m_MinRadius, m_GravityX, m_GravityY);

                if (m_Particles[i].m_CurrentLife <= 0)
                    m_Particles.RemoveAt(i);
            }
        }
        #endregion

        #region Draw

        /*  Function:  public virtual void Draw(SpriteBatch spriteBatch)
         *   Purpose:  This will draw the particle effect (this can be overriden by a derived class)
         ****************************************************************/
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (m_IsOn)
            {
                spriteBatch.Begin(m_SortMode, m_BlendType);
                foreach (Particle particle in m_Particles)
                {
                    particle.Draw(spriteBatch);
                }
                spriteBatch.End();
            }
        }

        #endregion

        #region Clear

        public void Clear()
        {
            m_Particles.Clear();
        }

        #endregion

        /*
        #region Get / Set Config

        public EmitterConfig GetConfig()
        {
            EmitterConfig config = new EmitterConfig();
            config.m_Origin = m_Origin;
            config.m_EmitterDuration = m_EmitterDuration;
            config.m_IsInfinite = m_IsInfinite;
            config.m_IsOn = m_IsOn;
            config.m_Type = m_Type;
            config.m_StartColor = m_StartColor.toColor();
            config.m_StartColorVariance = m_StartColorVariance.toColor();
            config.m_EndColor = m_EndColor.toColor();
            config.m_EndColorVariance = m_EndColorVariance.toColor();
            config.m_MaxParticles = m_MaxParticles;
            config.m_Lifespan = m_Lifespan;
            config.m_StartSize = m_StartSize;
            config.m_StartSizeVariance = m_StartSizeVariance;
            config.m_EndSize = m_EndSize;
            config.m_EndSizeVariance = m_EndSizeVariance;
            config.m_EmitterAngle = m_EmitterAngle;
            config.m_EmitterAngleVariance = m_EmitterAngleVariance;
            config.m_StartRotation = m_StartRotation;
            config.m_StartRotationVariance = m_StartRotationVariance;
            config.m_EndRotation = m_EndRotation;
            config.m_EndRotationVariance = m_EndRotationVariance;
            config.m_XVariance = m_XVariance;
            config.m_YVariance = m_YVariance;
            config.m_Speed = m_Speed;
            config.m_SpeedVariance = m_SpeedVariance;
            config.m_GravityX = m_GravityX;
            config.m_GravityY = m_GravityY;
            config.m_TangentialAcceleration = m_TangentialAcceleration;
            config.m_TangentialAccelerationVariance = m_TangentialAccelerationVariance;
            config.m_RadialAcceleration = m_RadialAcceleration;
            config.m_RadialAccelerationVariance = m_RadialAccelerationVariance;
            config.m_MinRadius = m_MinRadius;
            config.m_MaxRadius = m_MaxRadius;
            config.m_MaxRadiusVariance = m_MaxRadiusVariance;
            config.m_Degrees = m_Degrees;
            config.m_DegreesVariance = m_DegreesVariance;
            config.m_BlendType = m_BlendType;
            config.m_SortMode = m_SortMode;

            return config;
        }
        public void SetConfig(EmitterConfig config)
        {
            m_Origin = config.m_Origin;
            m_EmitterDuration = config.m_EmitterDuration;
            m_IsInfinite = config.m_IsInfinite;
            m_IsOn = config.m_IsOn;
            m_Type = config.m_Type;
            m_StartColor.fromColor(config.m_StartColor);
            m_StartColorVariance.fromColor(config.m_StartColorVariance);
            m_EndColor.fromColor(config.m_EndColor);
            m_EndColorVariance.fromColor(config.m_EndColorVariance);
            m_MaxParticles = config.m_MaxParticles;
            m_Lifespan = config.m_Lifespan;
            m_StartSize = config.m_StartSize;
            m_StartSizeVariance = config.m_StartSizeVariance;
            m_EndSize = config.m_EndSize;
            m_EndSizeVariance = config.m_EndSizeVariance;
            m_EmitterAngle = config.m_EmitterAngle;
            m_EmitterAngleVariance = config.m_EmitterAngleVariance;
            m_StartRotation = config.m_StartRotation;
            m_StartRotationVariance = config.m_StartRotationVariance;
            m_EndRotation = config.m_EndRotation;
            m_EndRotationVariance = config.m_EndRotationVariance;
            m_XVariance = config.m_XVariance;
            m_YVariance = config.m_YVariance;
            m_Speed = config.m_Speed;
            m_SpeedVariance = config.m_SpeedVariance;
            m_GravityX = config.m_GravityX;
            m_GravityY = config.m_GravityY;
            m_TangentialAcceleration = config.m_TangentialAcceleration;
            m_TangentialAccelerationVariance = config.m_TangentialAccelerationVariance;
            m_RadialAcceleration = config.m_RadialAcceleration;
            m_RadialAccelerationVariance = config.m_RadialAccelerationVariance;
            m_MinRadius = config.m_MinRadius;
            m_MaxRadius = config.m_MaxRadius;
            m_MaxRadiusVariance = config.m_MaxRadiusVariance;
            m_Degrees = config.m_Degrees;
            m_DegreesVariance = config.m_DegreesVariance;
            m_BlendType = config.m_BlendType;
            m_SortMode = config.m_SortMode;
        }

        public ParticleConfig GetParticleConfig()
        {
            ParticleConfig config = new ParticleConfig();
            config.m_MaxParticles = m_MaxParticles;
            config.m_Lifespan = m_Lifespan;
            config.m_StartSize = m_StartSize;
            config.m_StartSizeVariance = m_StartSizeVariance;
            config.m_EndSize = m_EndSize;
            config.m_EndSizeVariance = m_EndSizeVariance;
            config.m_EmitterAngle = m_EmitterAngle;
            config.m_EmitterAngleVariance = m_EmitterAngleVariance;
            config.m_StartRotation = m_StartRotation;
            config.m_StartRotationVariance = m_StartRotationVariance;
            config.m_EndRotation = m_EndRotation;
            config.m_EndRotationVariance = m_EndRotationVariance;

            return config;
        }
        public void SetParticleConfig(ParticleConfig config)
        {
            m_MaxParticles = config.m_MaxParticles;
            m_Lifespan = config.m_Lifespan;
            m_StartSize = config.m_StartSize;
            m_StartSizeVariance = config.m_StartSizeVariance;
            m_EndSize = config.m_EndSize;
            m_EndSizeVariance = config.m_EndSizeVariance;
            m_EmitterAngle = config.m_EmitterAngle;
            m_EmitterAngleVariance = config.m_EmitterAngleVariance;
            m_StartRotation = config.m_StartRotation;
            m_StartRotationVariance = config.m_StartRotationVariance;
            m_EndRotation = config.m_EndRotation;
            m_EndRotationVariance = config.m_EndRotationVariance;
        }

        public GravityConfig GetGravityConfig()
        {
            GravityConfig config = new GravityConfig();
            config.m_XVariance = m_XVariance;
            config.m_YVariance = m_YVariance;
            config.m_Speed = m_Speed;
            config.m_SpeedVariance = m_SpeedVariance;
            config.m_GravityX = m_GravityX;
            config.m_GravityY = m_GravityY;
            config.m_TangentialAcceleration = m_TangentialAcceleration;
            config.m_TangentialAccelerationVariance = m_TangentialAccelerationVariance;
            config.m_RadialAcceleration = m_RadialAcceleration;
            config.m_RadialAccelerationVariance = m_RadialAccelerationVariance;

            return config;
        }
        public void SetGravityConfig(GravityConfig config)
        {
            m_XVariance = config.m_XVariance;
            m_YVariance = config.m_YVariance;
            m_Speed = config.m_Speed;
            m_SpeedVariance = config.m_SpeedVariance;
            m_GravityX = config.m_GravityX;
            m_GravityY = config.m_GravityY;
            m_TangentialAcceleration = config.m_TangentialAcceleration;
            m_TangentialAccelerationVariance = config.m_TangentialAccelerationVariance;
            m_RadialAcceleration = config.m_RadialAcceleration;
            m_RadialAccelerationVariance = config.m_RadialAccelerationVariance;
        }

        public RadialConfig GetRadialConfig()
        {
            RadialConfig config = new RadialConfig();
            config.m_MinRadius = m_MinRadius;
            config.m_MaxRadius = m_MaxRadius;
            config.m_MaxRadiusVariance = m_MaxRadiusVariance;
            config.m_Degrees = m_Degrees;
            config.m_DegreesVariance = m_DegreesVariance;

            return config;
        }
        public void SetRadialConfig(RadialConfig config)
        {
            m_MinRadius = config.m_MinRadius;
            m_MaxRadius = config.m_MaxRadius;
            m_MaxRadiusVariance = config.m_MaxRadiusVariance;
            m_Degrees = config.m_Degrees;
            m_DegreesVariance = config.m_DegreesVariance;
        }

        public ColorConfig GetColorConfig()
        {
            ColorConfig config = new ColorConfig();
            config.m_StartColor = m_StartColor.toColor();
            config.m_StartColorVariance = m_StartColorVariance.toColor();
            config.m_EndColor = m_EndColor.toColor();
            config.m_EndColorVariance = m_EndColorVariance.toColor();

            return config;
        }
        public void SetColorConfig(ColorConfig config)
        {
            m_StartColor.fromColor(config.m_StartColor);
            m_StartColorVariance.fromColor(config.m_StartColorVariance);
            m_EndColor.fromColor(config.m_EndColor);
            m_EndColorVariance.fromColor(config.m_EndColorVariance);
        }
        #endregion
        */
    }
}