using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public struct ColorConfig
    {
        public Color m_StartColor; // Typically from 0f to 255ff
        public Color m_StartColorVariance; // Typically from 0f to 255f
        public Color m_EndColor; // Typically from 0f to 255f
        public Color m_EndColorVariance; // Typically from 0f to 255f
    }
}
