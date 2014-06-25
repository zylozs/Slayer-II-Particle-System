using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public struct ParticleConfig
    {
        public int m_MaxParticles; // Typical ranges are 1-1,000
        public float m_Lifespan; // In milliseconds, typically from 0 to 10,000
        public float m_LifespanVariance; // In milliseconds, typically from 0 to 10,000
        public float m_StartSize; // In pixels, typically from 0 to 70
        public float m_StartSizeVariance; // In pixels, typically from 0 to 70
        public float m_EndSize; // In pixels, typically from 0 to 70
        public float m_EndSizeVariance; // In pixels, typically from 0 to 70
        public float m_EmitterAngle; // In degrees, from 0 to 360 (90 is down, 180 is left, 270 is up, 0 & 360 are right)
        public float m_EmitterAngleVariance; // In degrees, from 0 to 360
        public float m_StartRotation; // In degrees, from 0 to 360
        public float m_StartRotationVariance; // In degrees, from 0 to 360
        public float m_EndRotation; // In degrees, from 0 to 360
        public float m_EndRotationVariance; // In degrees, from 0 to 360
    }
}
