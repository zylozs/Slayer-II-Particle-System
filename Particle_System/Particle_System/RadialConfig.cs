using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public struct RadialConfig
    {
        public float m_MinRadius; // In pixels, typically from 0 to 500
        public float m_MaxRadius; // In pixels, typically from 0 to 500
        public float m_MaxRadiusVariance; // In pixels, typically from 0 to 500
        public float m_Degrees; // In degrees, typically from -360 to 360
        public float m_DegreesVariance; // In degrees, typically from 0 to 360
    }
}
