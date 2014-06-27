using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Texture_Loader;

namespace Particle_System
{
    public class DynamicParticleEmitter : ParticleEmitter
    {
        protected static Texture2D m_DefaultTexture = null;
        protected static TextureLoader m_TextureLoader;

        #region Properties

        // Misc
        protected string m_TextureFilepath;

        public Texture2D ParticleTexture { get { return m_ParticleTexture; } set { m_ParticleTexture = value; } }
        public Vector2 Origin { get { return m_Origin; } set { m_Origin = value; } }
        public int EmitterDuration { get { return m_EmitterDuration; } set { m_EmitterDuration = value; } }
        public bool IsInfinite { get { return m_IsInfinite; } set { m_IsInfinite = value; } }
        public bool IsOn { get { return m_IsOn; } set { m_IsOn = value; } }
        public EmitterType Type { get { return m_Type; } set { m_Type = value; } }

        // Particle Colors
        public Color StartColor { get { return m_StartColor.toColor(); } set { m_StartColor.fromColor(value); } }
        public Color StartColorVariance { get { return m_StartColorVariance.toColor(); } set { m_StartColorVariance.fromColor(value); } }
        public Color EndColor { get { return m_EndColor.toColor(); } set { m_EndColor.fromColor(value); } }
        public Color EndColorVariance { get { return m_EndColorVariance.toColor(); } set { m_EndColorVariance.fromColor(value); } }

        // Particle Configuration
        public int MaxParticles { get { return m_MaxParticles; } set { m_MaxParticles = value; } }
        public float Lifespan { get { return m_Lifespan; } set { m_Lifespan = value; } }
        public float LifespanVariance { get { return m_LifespanVariance; } set { m_LifespanVariance = value; } }
        public float StartSize { get { return m_StartSize; } set { m_StartSize = value; } }
        public float StartSizeVariance { get { return m_StartSizeVariance; } set { m_StartSizeVariance = value; } }
        public float EndSize { get { return m_EndSize; } set { m_EndSize = value; } }
        public float EndSizeVariance { get { return m_EndSizeVariance; } set { m_EndSizeVariance = value; } }
        public float EmitterAngle { get { return m_EmitterAngle; } set { m_EmitterAngle = value; } }
        public float EmitterAngleVariance { get { return m_EmitterAngleVariance; } set { m_EmitterAngleVariance = value; } }
        public float StartRotation { get { return m_StartRotation; } set { m_StartRotation = value; } }
        public float StartRotationVariance { get { return m_StartRotationVariance; } set { m_StartRotationVariance = value; } }
        public float EndRotation { get { return m_EndRotation; } set { m_EndRotation = value; } }
        public float EndRotationVariance { get { return m_EndRotationVariance; } set { m_EndRotationVariance = value; } }

        // Gravity (Gravity Emitter)
        public float X_Variance { get { return m_XVariance; } set { m_XVariance = value; } }
        public float Y_Variance { get { return m_YVariance; } set { m_YVariance = value; } }
        public float Speed { get { return m_Speed; } set { m_Speed = value; } }
        public float SpeedVariance { get { return m_SpeedVariance; } set { m_SpeedVariance = value; } }
        public float GravityX { get { return m_GravityX; } set { m_GravityX = value; } }
        public float GravityY { get { return m_GravityY; } set { m_GravityY = value; } }
        public float TangentialAcceleration { get { return m_TangentialAcceleration; } set { m_TangentialAcceleration = value; } }
        public float TangentialAccelerationVariance { get { return m_TangentialAccelerationVariance; } set { m_TangentialAccelerationVariance = value; } }
        public float RadialAcceleration { get { return m_RadialAcceleration; } set { m_RadialAcceleration = value; } }
        public float RadialAccelerationVariance { get { return m_RadialAccelerationVariance; } set { m_RadialAccelerationVariance = value; } }

        // Rotation (Radial Emitter)
        public float MinRadius { get { return m_MinRadius; } set { m_MinRadius = value; } }
        public float MaxRadius { get { return m_MaxRadius; } set { m_MaxRadius = value; } }
        public float MaxRadiusVariance { get { return m_MaxRadiusVariance; } set { m_MaxRadiusVariance = value; } }
        public float Degrees { get { return m_Degrees; } set { m_Degrees = value; } }
        public float DegreesVariance { get { return m_DegreesVariance; } set { m_DegreesVariance = value; } }

        // Spritebatch
        public BlendState BlendType { get { return m_BlendType; } set { m_BlendType = value; } }
        public SpriteSortMode SortMode { get { return m_SortMode; } set { m_SortMode = value; } }

        #endregion

        #region Constructors

        public DynamicParticleEmitter()
            : base()
        {

        }

        #endregion

        #region Load Content

        public override void LoadContent(ContentManager content)
        {
            LoadContent(content, "");
        }

        public void LoadContent(ContentManager content, string assetFilepath)
        {
            if (assetFilepath == "")
            {
                m_ParticleTexture = content.Load<Texture2D>("whiteCircle");
                m_TextureFilepath = "default";
            }
            else
            {
                // Try loading with the content pipeline first
                try
                {
                    m_ParticleTexture = content.Load<Texture2D>(assetFilepath);
                    m_TextureFilepath = assetFilepath;
                }
                catch (Exception ex)
                {
                    // If it doesn't work by the content pipeline, then it is probably an external file
                    // Next we try loading it from the file directly
                    try
                    {
                        m_ParticleTexture = m_TextureLoader.FromFile(@assetFilepath);
                        m_TextureFilepath = @assetFilepath;
                    }
                    catch (Exception e) // If once again it is unsuccessful at loading, it loads the default texture (white circle)
                    {
                        m_ParticleTexture = content.Load<Texture2D>("whiteCircle");
                        m_TextureFilepath = "default";
                        Console.WriteLine("The texture you provided does not exist!");
                    }
                }
            }

            if (m_DefaultTexture == null)
                m_DefaultTexture = m_ParticleTexture;
        }

        #endregion

        #region Initialize

        public override void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            m_ContentManager = content;
            m_Graphics = graphics;
            Initialize("");
        }

        public void Initialize(ContentManager content, GraphicsDeviceManager graphics, string assetFilepath)
        {
            m_ContentManager = content;
            m_Graphics = graphics;
            Initialize(assetFilepath);
        }

        protected void Initialize(string assetFilepath)
        {
            // Misc
            m_Origin = new Vector2(m_Graphics.PreferredBackBufferWidth / 2, m_Graphics.PreferredBackBufferHeight / 2);
            m_EmitterDuration = 0;
            m_IsInfinite = true;
            m_IsOn = true;

            m_Type = EmitterType.GRAVITY;

            m_SortMode = SpriteSortMode.FrontToBack;
            m_BlendType = BlendState.AlphaBlend;

            if (m_TextureLoader == null)
                m_TextureLoader = new TextureLoader(m_Graphics.GraphicsDevice);

            LoadContent(m_ContentManager, assetFilepath);

            // Color
            InitializeColorConfig();

            // Particles
            InitializeParticleConfig();

            // Gravity
            InitializeGravityConfig();

            // Radial
            InitializeRadialConfig();
        }

        #endregion

        #region Get / Set Config

        public EmitterConfig GetConfig()
        {
            EmitterConfig config = new EmitterConfig();
            config.m_TextureFilePath = m_TextureFilepath;
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
            config.m_Lifespan = m_Lifespan / 1000;
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
            if (config.m_TextureFilePath == "default" || config.m_TextureFilePath == "")
                //ParticleTexture = m_ContentManager.Load<Texture2D>("whiteCircle.png");
                ParticleTexture = m_DefaultTexture;
            else
            {
                try
                {
                    ParticleTexture = m_TextureLoader.FromFile(config.m_TextureFilePath);
                }
                catch (System.Exception ex)
                {
                    ParticleTexture = m_DefaultTexture;
                    Console.WriteLine("The texture you provided could not be found!");
                }
            }

            Origin = config.m_Origin;
            EmitterDuration = config.m_EmitterDuration;
            IsInfinite = config.m_IsInfinite;
            IsOn = config.m_IsOn;
            Type = config.m_Type;
            StartColor = config.m_StartColor;
            StartColorVariance = config.m_StartColorVariance;
            EndColor = config.m_EndColor;
            EndColorVariance = config.m_EndColorVariance;
            MaxParticles = config.m_MaxParticles;
            Lifespan = config.m_Lifespan * 1000;
            StartSize = config.m_StartSize;
            StartSizeVariance = config.m_StartSizeVariance;
            EndSize = config.m_EndSize;
            EndSizeVariance = config.m_EndSizeVariance;
            EmitterAngle = config.m_EmitterAngle;
            EmitterAngleVariance = config.m_EmitterAngleVariance;
            StartRotation = config.m_StartRotation;
            StartRotationVariance = config.m_StartRotationVariance;
            EndRotation = config.m_EndRotation;
            EndRotationVariance = config.m_EndRotationVariance;
            X_Variance = config.m_XVariance;
            Y_Variance = config.m_YVariance;
            Speed = config.m_Speed;
            SpeedVariance = config.m_SpeedVariance;
            GravityX = config.m_GravityX;
            GravityY = config.m_GravityY;
            TangentialAcceleration = config.m_TangentialAcceleration;
            TangentialAccelerationVariance = config.m_TangentialAccelerationVariance;
            RadialAcceleration = config.m_RadialAcceleration;
            RadialAccelerationVariance = config.m_RadialAccelerationVariance;
            MinRadius = config.m_MinRadius;
            MaxRadius = config.m_MaxRadius;
            MaxRadiusVariance = config.m_MaxRadiusVariance;
            Degrees = config.m_Degrees;
            DegreesVariance = config.m_DegreesVariance;
            BlendType = config.m_BlendType;
            SortMode = config.m_SortMode;
        }

        public ParticleConfig GetParticleConfig()
        {
            ParticleConfig config = new ParticleConfig();
            config.m_MaxParticles = m_MaxParticles;
            config.m_Lifespan = m_Lifespan / 1000;
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
            MaxParticles = config.m_MaxParticles;
            Lifespan = config.m_Lifespan * 1000;
            StartSize = config.m_StartSize;
            StartSizeVariance = config.m_StartSizeVariance;
            EndSize = config.m_EndSize;
            EndSizeVariance = config.m_EndSizeVariance;
            EmitterAngle = config.m_EmitterAngle;
            EmitterAngleVariance = config.m_EmitterAngleVariance;
            StartRotation = config.m_StartRotation;
            StartRotationVariance = config.m_StartRotationVariance;
            EndRotation = config.m_EndRotation;
            EndRotationVariance = config.m_EndRotationVariance;
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
            X_Variance = config.m_XVariance;
            Y_Variance = config.m_YVariance;
            Speed = config.m_Speed;
            SpeedVariance = config.m_SpeedVariance;
            GravityX = config.m_GravityX;
            GravityY = config.m_GravityY;
            TangentialAcceleration = config.m_TangentialAcceleration;
            TangentialAccelerationVariance = config.m_TangentialAccelerationVariance;
            RadialAcceleration = config.m_RadialAcceleration;
            RadialAccelerationVariance = config.m_RadialAccelerationVariance;
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
            MinRadius = config.m_MinRadius;
            MaxRadius = config.m_MaxRadius;
            MaxRadiusVariance = config.m_MaxRadiusVariance;
            Degrees = config.m_Degrees;
            DegreesVariance = config.m_DegreesVariance;
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
            StartColor = config.m_StartColor;
            StartColorVariance = config.m_StartColorVariance;
            EndColor = config.m_EndColor;
            EndColorVariance = config.m_EndColorVariance;
        }
        #endregion
    }
}