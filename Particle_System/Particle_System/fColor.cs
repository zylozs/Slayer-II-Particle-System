using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public class fColor
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }

        public fColor(float r, float g, float b)
        {
            set(r, g, b);
        }

        public fColor(float r, float g, float b, float a)
        {
            set(r, g, b, a);
        }

        public fColor(Color color)
        {
            fromColor(color);
        }

        public void set(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public void set(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color toColor()
        {
            return new Color((byte)R, (byte)G, (byte)B, (byte)A);
        }

        public void fromColor(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        public string toString()
        {
            string temp = "";

            temp += "R: " + R;
            temp += " G: " + G;
            temp += " B: " + B;
            temp += " A: " + A;

            return temp;
        }
    }
}
