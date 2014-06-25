using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public struct GravityConfig
    {
        public float m_XVariance; // In pixels, typically from 0 to 1,000
        public float m_YVariance; // In pixels, typically from 0 to 1,000
        public float m_Speed; // In pixels, typically from 0 to 500
        public float m_SpeedVariance; // In pixels, typically from 0 to 500
        public float m_GravityX; // In pixels, typically from -500 to 500
        public float m_GravityY; // In pixels, typically from -500 to 500
        public float m_TangentialAcceleration; // In pixels, typically from -500 to 500
        public float m_TangentialAccelerationVariance; // In pixels, typically from 0 to 500
        public float m_RadialAcceleration; // In pixels, typically from -500 to 500
        public float m_RadialAccelerationVariance; // In pixels, typically from 0 to 500
    }
}
