using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public class ParticleSystem
    {
        /********* PARTICLE SYSTEM DATA *********/
        public List<ParticleEmitter> m_ParticleEmitters;
        private ContentManager m_Content;
        private GraphicsDeviceManager m_Graphics;

        #region Constructors
        /*  Function:  public ParticleSystem()
         *   Purpose:  Constructor
         ****************************************************************/
        public ParticleSystem()
        {
            m_ParticleEmitters = new List<ParticleEmitter>();
        }
        #endregion

        #region LoadContent
        /*  Function:  public void LoadContent(ContentManager content)
         *   Purpose:  This will store the content manager for later use by the particle effects
         ****************************************************************/
        public void LoadContent(ContentManager content, GraphicsDeviceManager graphics)
        {
            m_Content = content;
            m_Graphics = graphics;
        }
        #endregion

        #region AddParticleEffect
        /*  Function:  public void AddParticleEmitter(ParticleEmitter emitter)
         *   Purpose:  This will add a particle emitter to the particle system (this is using a builder pattern)
         ****************************************************************/
        public void AddParticleEmitter(ParticleEmitter emitter)
        {
            ParticleEmitter tempEmitter = emitter;
            tempEmitter.Initialize(m_Content, m_Graphics);
            m_ParticleEmitters.Add(tempEmitter);
        }
        #endregion

        #region Update
        /*  Function:  public void Update(GameTime gameTime)
         *   Purpose:  This will update each of the particle effects
         ****************************************************************/
        public void Update(GameTime gameTime)
        {
            for (int i = m_ParticleEmitters.Count - 1; i >= 0; i--)
            {
                m_ParticleEmitters[i].Update(gameTime);

                if (!m_ParticleEmitters[i].isAlive())
                    m_ParticleEmitters.RemoveAt(i);
            }
        }
        #endregion

        #region Draw
        /*  Function:  public void Update(GameTime gameTime)
         *   Purpose:  This will draw each of the particle effects
         ****************************************************************/
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ParticleEmitter emitter in m_ParticleEmitters)
            {
                emitter.Draw(spriteBatch);
            }
        }
        #endregion
    }
}