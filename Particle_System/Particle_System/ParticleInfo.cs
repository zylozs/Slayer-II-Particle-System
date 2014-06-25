using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public struct ParticleInfo
    {
        public float m_Lifespan;
        public Vector2 m_Pos;
        public Vector2 m_StartPos;
        public Vector2 m_Velocity;
        public float m_EmitterRadius;
        public float m_EmitterRadiusDelta;
        public float m_EmitterRotation;
        public float m_EmitterRotationDelta;
        public float m_RadialAcceleration;
        public float m_TangentialAcceleration;
        public float m_Scale;
        public float m_ScaleDelta;
        public fColor m_StartColor;
        public fColor m_EndColor;
        public fColor m_ColorDelta;
        public float m_Rotation;
        public float m_RotationDelta;
        public float m_Depth;
        public float m_DepthDelta;
        public float m_Speed;
    };
}
