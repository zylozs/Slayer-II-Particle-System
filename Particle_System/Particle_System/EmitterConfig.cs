using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public struct EmitterConfig
    {
        public string m_TextureFilePath;
        public Vector2 m_Origin;
        public int m_EmitterDuration;
        public bool m_IsInfinite;
        public bool m_IsOn;
        public EmitterType m_Type;

        // Particle Colors
        public Color m_StartColor;
        public Color m_StartColorVariance;
        public Color m_EndColor;
        public Color m_EndColorVariance;

        // Particle Configuration
        public int m_MaxParticles;
        public float m_Lifespan;
        public float m_LifespanVariance;
        public float m_StartSize;
        public float m_StartSizeVariance;
        public float m_EndSize;
        public float m_EndSizeVariance;
        public float m_EmitterAngle;
        public float m_EmitterAngleVariance;
        public float m_StartRotation;
        public float m_StartRotationVariance;
        public float m_EndRotation;
        public float m_EndRotationVariance;

        // Gravity (Gravity Emitter)
        public float m_XVariance;
        public float m_YVariance;
        public float m_Speed;
        public float m_SpeedVariance;
        public float m_GravityX;
        public float m_GravityY;
        public float m_TangentialAcceleration;
        public float m_TangentialAccelerationVariance;
        public float m_RadialAcceleration;
        public float m_RadialAccelerationVariance;

        // Rotation (Radial Emitter)
        public float m_MinRadius;
        public float m_MaxRadius;
        public float m_MaxRadiusVariance;
        public float m_Degrees;
        public float m_DegreesVariance;

        // Spritebatch
        public BlendState m_BlendType;
        public SpriteSortMode m_SortMode;
    }
}
