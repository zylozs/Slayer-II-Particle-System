using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Particle_System
{
    public class Sprite
    {
        /********* DATA *********/
        public Texture2D m_Texture;
        public Vector2 m_Position;
        public Vector2 m_Origin;
        public Rectangle m_SourceRectangle;
        public float m_Rotation;
        public float m_Scale;
        public float m_Depth;
        public fColor m_Color;

        #region Constructors

        /*  Function:  public Sprite();
         *   Purpose:  Constructor
         ****************************************************************/
        public Sprite()
        {
        }

        /*  Function:  public Sprite(Texture2D texture);
         *   Purpose:  Constructor
         ****************************************************************/
        public Sprite(Texture2D texture)
        {
            this.m_Texture = texture;
        }

        /*  Function:  public Sprite(Texture2D texture, Vector2 position);
         *   Purpose:  Constructor
         ****************************************************************/
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.m_Texture = texture;
            this.m_Position = position;
        }

        /*  Function:  public Sprite(Texture2D texture, Rectangle sourceRectangle);
         *   Purpose:  Constructor
         ****************************************************************/
        public Sprite(Texture2D texture, Rectangle sourceRectangle)
        {
            this.m_Texture = texture;
            this.m_SourceRectangle = sourceRectangle;
        }

        /*  Function:  public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position);
         *   Purpose:  Constructor
         ****************************************************************/
        public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position)
        {
            this.m_Texture = texture;
            this.m_SourceRectangle = sourceRectangle;
            this.m_Position = position;
        }

        /*  Function:  public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, Color color, Vector2 origin, float rotation, float scale, float depth);
         *   Purpose:  Constructor
         ****************************************************************/
        public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, Color color, Vector2 origin, float rotation, float scale, float depth)
        {
            this.m_Texture = texture;
            this.m_SourceRectangle = sourceRectangle;
            this.m_Position = position;
            this.m_Color = new fColor(color);
            this.m_Origin = origin;
            this.m_Rotation = rotation;
            this.m_Scale = scale;
            this.m_Depth = depth;
        }
        #endregion

        #region Initialize

        /*  Function:  public void Initialize(Texture2D texture)
         *   Purpose:  To initialize the sprite to default values
         ****************************************************************/
        public void Initialize(Texture2D texture)
        {
            this.m_Texture = texture;
            this.m_Position = new Vector2();
            this.m_Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.m_SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            this.m_Rotation = 0f;
            this.m_Scale = 1f;
            this.m_Depth = 0f;
            this.m_Color = new fColor(Color.White);
        }

        /*  Function:  public void Initialize(Texture2D texture, int sourceX, int sourceY)
         *   Purpose:  To initialize the sprite to default values
         ****************************************************************/
        public void Initialize(Texture2D texture, Rectangle sourceRect)
        {
            this.m_Texture = texture;
            this.m_Position = new Vector2();
            this.m_Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.m_SourceRectangle = sourceRect;
            this.m_Rotation = 0f;
            this.m_Scale = 1f;
            this.m_Depth = 0f;
            this.m_Color = new fColor(Color.White);
        }
        #endregion

        #region Update
        /*  Function:  public void Update(GameTime gameTime)
         *   Purpose:  To update the sprite (if needed)
         ****************************************************************/
        public virtual void Update(GameTime gameTime)
        {
        }
        #endregion

        #region Draw
        /*  Function:  public void Draw(SpriteBatch spriteBatch)
         *   Purpose:  To draw the sprite
         ****************************************************************/
        public virtual void Draw(SpriteBatch spriteBatch)
        {
             spriteBatch.Draw(m_Texture, m_Position, m_SourceRectangle, m_Color.toColor(), m_Rotation, m_Origin, m_Scale, SpriteEffects.None, m_Depth);
        }
        #endregion
    }
}
