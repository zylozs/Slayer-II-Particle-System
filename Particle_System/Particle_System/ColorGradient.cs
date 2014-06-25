using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particle_System
{
	public class ColorGradient
	{
		public float[] position;
		public Color[] color;

		public ColorGradient()
        {
			position = new float[1] {1};
			color = new Color[1] {Color.White};
		}

		public ColorGradient(ColorGradient cg)
        {
			position = cg.position;
			color = cg.color;
		}

		public ColorGradient(float[] p, Color[] c)
        {
			position = p;
			color = c;
		}

		public Color getColor(float p)
        {
			for(int i = 0; i < position.Length; i++)
            {
				if(position[i] == p)
					return color[i];
				else if(i > 0 && position[i] > p)
                {
					float f1 = p-position[i-1];
					float f3 = f1/(position[i] - position[i-1]);
					Vector4 c1 = color[i-1].ToVector4();
					Vector4 c2 = color[i].ToVector4();
					return new Color(Vector4.SmoothStep(c1, c2, f3));
				}
			}
			return color[color.Length-1];
		}
	}
}
