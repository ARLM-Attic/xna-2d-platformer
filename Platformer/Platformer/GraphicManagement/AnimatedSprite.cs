using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.GraphicManagement
{
    public class AnimatedSprite : Sprite
    {
        #region Fields
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int _currentFrame;
        private readonly int _totalFrames;
        #endregion

        #region Methods
        public AnimatedSprite(Texture2D texture, Vector2 position, int rows, int columns) : base(texture, position)
        {
            Rows = rows;
            Columns = columns;
            _currentFrame = 0;
            _totalFrames = Rows * Columns;
        }

        public void Update()
        {
            _currentFrame++;
            if (_currentFrame == _totalFrames)
                _currentFrame = 0;
        }


        #endregion
    }
}