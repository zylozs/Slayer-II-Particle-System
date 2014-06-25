using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Particle_System
{
    public class Particle : Sprite
    {
        /********* MOVEMENT *********/
        /*public Vector2 m_Position;
        public Vector2 m_Velocity;
        public Vector2 m_Acceleration;
        public float m_Dampening;*/

        /********* ROTATION *********/
        /*public float m_Rotation;
        public float m_RotationVelocity;
        public float m_RotationDampening;*/

        /********* SCALE *********/
        /*public float m_Scale;
        public float m_ScaleVelocity;
        public float m_ScaleAcceleration;
        public float m_ScaleMax;*/

        /********* COLOR *********/
        /*public Color m_Color;
        public Color m_StartColor;
        public Color m_EndColor;*/

        /********* AGE *********/
        /*public int m_Lifespan;
        public int m_FadeLifespan;*/

        /********* SPRITE *********/
        //public Sprite m_Sprite;

        /*public ParticleConfig m_ParticleConfig;
        public GravityConfig m_GravityConfig;
        public RadialConfig m_RadialConfig;
        public ColorConfig m_ColorConfig;*/
        public EmitterType m_Type;

        public fColor m_ColorDelta;

        public fColor m_StartColor;
        public fColor m_EndColor;

        public Vector2 m_StartPos;
        public Vector2 m_Velocity;
        public float m_RadialAcceleration;
        public float m_TangentialAcceleration;
        public float m_EmitterRadius;
        public float m_EmitterRadiusDelta;
        public float m_EmitterRotation;
        public float m_EmitterRotationDelta;
        public float m_RotationDelta;
        public float m_ScaleDelta;
        public float m_DepthDelta;
        public float m_Speed;

        public float m_CurrentLife;
        public float m_Lifespan;

        #region Constructors
        /*  Function:  public Particle();
         *   Purpose:  Constructor
         ****************************************************************/
        public Particle()
        {
            
        }
        #endregion

        #region Create Particle

        public void CreateParticle(Texture2D texture, ParticleInfo pInfo, EmitterType type)
        {
            CreateParticle(texture, Rectangle.Empty, pInfo, type);
        }

        public void CreateParticle(Texture2D texture, Rectangle sourceRect, ParticleInfo pInfo, EmitterType type)
        {
            if (sourceRect != Rectangle.Empty)
                this.Initialize(texture, sourceRect);
            else
                this.Initialize(texture);

            m_Type = type;

            m_CurrentLife = pInfo.m_Lifespan;
            m_Lifespan = pInfo.m_Lifespan;
            m_Position = pInfo.m_Pos;
            m_StartPos = pInfo.m_StartPos;
            m_Velocity = pInfo.m_Velocity;
            m_EmitterRadius = pInfo.m_EmitterRadius;
            m_EmitterRadiusDelta = pInfo.m_EmitterRadiusDelta;
            m_EmitterRotation = pInfo.m_EmitterRotation;
            m_EmitterRotationDelta = pInfo.m_EmitterRotationDelta;
            m_RadialAcceleration = pInfo.m_RadialAcceleration;
            m_TangentialAcceleration = pInfo.m_TangentialAcceleration;
            m_Scale = pInfo.m_Scale;
            m_ScaleDelta = pInfo.m_ScaleDelta;
            m_Color = pInfo.m_StartColor;
            m_StartColor = pInfo.m_StartColor;
            m_EndColor = pInfo.m_EndColor;
            m_ColorDelta = pInfo.m_ColorDelta;
            m_Rotation = pInfo.m_Rotation;
            m_RotationDelta = pInfo.m_RotationDelta;
            m_Depth = pInfo.m_Depth;
            m_DepthDelta = pInfo.m_DepthDelta;
            m_Speed = pInfo.m_Speed;
        }

        #endregion

        #region Update
        /*  Function:  public void Update(GameTime gameTime)
         *   Purpose:  This will update the particle
         ****************************************************************/
        public void Update(GameTime gameTime, float minRadius, float gravityX, float gravityY)
        {
            m_CurrentLife -= gameTime.ElapsedGameTime.Milliseconds;

            if (m_CurrentLife <= 0)
                return;

            // Update the particle
            if (m_Type == EmitterType.GRAVITY)
            {
                float distX = m_Position.X - m_StartPos.X;
                float distY = m_Position.Y - m_StartPos.Y;
                float dist = (float)Math.Sqrt((double)(distX * distX + distY * distY));
                if (dist < 0.01f)
                    dist = 0.01f;

                // ac = v^2 / r
                // ac = v^2 / dist
                // radial_acceleration = v^2 / dist
                // radial_acceleration * dist = v^2

                float radialX = distX / dist;
                float radialY = distY / dist;
                float tangX = radialX;
                float tangY = radialY;

                radialX *= m_RadialAcceleration;
                radialY *= m_RadialAcceleration;

                float newY = tangX;
                tangX = -tangY * m_TangentialAcceleration;
                tangY = newY * m_TangentialAcceleration;

                // at = r * angular_acceleration 

                //m_Velocity.X += gameTime.ElapsedGameTime.Milliseconds * (gravityX + radialX + tangX) * (float)Math.Cos(MathHelper.ToRadians(m_EmitterRotation));
                //m_Velocity.Y += gameTime.ElapsedGameTime.Milliseconds * (gravityY + radialY + tangY) * (float)Math.Sin(MathHelper.ToRadians(m_EmitterRotation));

                //m_Velocity.X = m_Speed * (float)Math.Cos(MathHelper.ToRadians(m_EmitterRotation)) + ((gravityX + radialX + tangX) * gameTime.ElapsedGameTime.Milliseconds / 1000);
                //m_Velocity.Y = m_Speed * (float)Math.Sin(MathHelper.ToRadians(m_EmitterRotation)) + ((gravityY + radialY + tangY) * gameTime.ElapsedGameTime.Milliseconds / 1000);

                m_Velocity.X = m_Speed * (float)Math.Cos(MathHelper.ToRadians(m_EmitterRotation));
                m_Velocity.Y = m_Speed * (float)Math.Sin(MathHelper.ToRadians(m_EmitterRotation));

                m_Velocity.X += gravityX + radialX + tangX;
                m_Velocity.Y += gravityY + radialY + tangY;

                /*m_Velocity.X += tangX * gameTime.ElapsedGameTime.Milliseconds / 1000;
                m_Velocity.Y += tangY * gameTime.ElapsedGameTime.Milliseconds / 1000;*/

                //m_Position.X += ((m_Velocity.X * gameTime.ElapsedGameTime.Milliseconds) / 1000);// *(float)Math.Cos(MathHelper.ToRadians(m_EmitterRotation));
                //m_Position.Y += ((m_Velocity.Y * gameTime.ElapsedGameTime.Milliseconds) / 1000);// *(float)Math.Sin(MathHelper.ToRadians(m_EmitterRotation));

                m_Position.X += (m_Velocity.X * gameTime.ElapsedGameTime.Milliseconds) / 1000;
                m_Position.Y += (m_Velocity.Y * gameTime.ElapsedGameTime.Milliseconds) / 1000;
            }
            else if (m_Type == EmitterType.RADIAL)
            {
                m_EmitterRotation += m_EmitterRotationDelta * gameTime.ElapsedGameTime.Milliseconds;
                m_EmitterRadius -= m_EmitterRadiusDelta * gameTime.ElapsedGameTime.Milliseconds;
                m_Position.X = m_StartPos.X - (float)Math.Cos(MathHelper.ToRadians(m_EmitterRotation)) * m_EmitterRadius;
                m_Position.Y = m_StartPos.Y - (float)Math.Sin(MathHelper.ToRadians(m_EmitterRotation)) * m_EmitterRadius;

                if (m_EmitterRadius < minRadius)
                    m_Lifespan = 0;
            }

            m_Scale += m_ScaleDelta * gameTime.ElapsedGameTime.Milliseconds;
            m_Rotation += m_RotationDelta * gameTime.ElapsedGameTime.Milliseconds;

            m_Color.R += m_ColorDelta.R * gameTime.ElapsedGameTime.Milliseconds;
            m_Color.G += m_ColorDelta.G * gameTime.ElapsedGameTime.Milliseconds;
            m_Color.B += m_ColorDelta.B * gameTime.ElapsedGameTime.Milliseconds;
            m_Color.A += m_ColorDelta.A * gameTime.ElapsedGameTime.Milliseconds;

            m_Depth += m_DepthDelta * gameTime.ElapsedGameTime.Milliseconds;
            //float value = m_CurrentLife / m_Lifespan;
            //value = 1.0f - value; // inverts it to make sure it is done properly

            //m_Color.R = MathHelper.Lerp(m_StartColor.R, m_EndColor.R, value);
            //m_Color.G = MathHelper.Lerp(m_StartColor.G, m_EndColor.G, value);
            //m_Color.B = MathHelper.Lerp(m_StartColor.B, m_EndColor.B, value);
            //m_Color.A = MathHelper.Lerp(m_StartColor.A, m_EndColor.A, value);

            //Console.WriteLine("Value: " + value + " " + m_Color.ToString());
        }

        #endregion

        #region Draw
        /*  Function:  public void Draw(SpriteBatch spriteBatch)
         *   Purpose:  This will draw the particle to the screen if they are still alive
         ****************************************************************/
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (m_CurrentLife <= 0)
                return;

            spriteBatch.Draw(m_Texture, m_Position, m_SourceRectangle, m_Color.toColor(), m_Rotation, m_Origin, m_Scale, SpriteEffects.None, m_Depth);
        }
        #endregion
    }
}
