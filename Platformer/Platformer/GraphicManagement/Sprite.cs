using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Platformer.GraphicManagement
{
    public class Sprite
    {
        #region Fields
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        #endregion

        #region Methods
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;
            this.Velocity = Vector2.Zero;
        }
        #endregion
    }
}
