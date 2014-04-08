#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.GameStateManagement.Helpers;
#endregion

namespace Platformer.GameStateManagement.Screens
{
    /// <summary>
    /// Where the game run :)
    /// </summary>
    public class GameCoreScreen : GameScreen
    {
        #region Fields
        #endregion

        #region Initialization
        public GameCoreScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            #region Actions initialization
            //TODO: Define all actions that player may want to do
            InputManager.AddAction("ActivatePause").Add(Keys.Escape)
                                                   .Add(Buttons.Start);
            #endregion
        }

        public override void LoadContent()
        {
            if (Content == null)
                Content = new ContentManager(ScreenManager.Game.Services, "Content");

            //TODO: Add content for the game (sprites, sounds, etc...)
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }
        #endregion

        #region Handle Input
        public override void HandleInput()
        {
            InputManager.Update();

            if (InputManager["ActivatePause"].IsTapped)
            {
                //TODO: Externalize text in resource file
                ScreenManager.AddScreen(new MessageBoxScreen("Do you want to exit the game ?"), PlayerIndex.One); 
            }
        }
        #endregion

        #region Update and Draw
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                               bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            SpriteFont font = ScreenManager.Font;

            const string message = "GAME CORE IS RUNNING";

            var viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            spriteBatch.DrawString(font, message, textPosition, color);

            spriteBatch.End();
        }
        #endregion
    }
}
