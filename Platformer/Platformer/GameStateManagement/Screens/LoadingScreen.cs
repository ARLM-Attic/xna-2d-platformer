using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer.GameStateManagement.Screens
{
    public class LoadingScreen : GameScreen
    {
        #region Fields

        readonly bool _loadingIsSlow;
        bool _otherScreensAreGone;

        readonly GameScreen[] _screensToLoad;
        #endregion

        #region Initialization
        /// <summary>
        /// Ne pas utiliser directement en dehors de la classe (private).
        /// Privilégier la méthode static Load().
        /// </summary>
        private LoadingScreen(bool loadingIsSlow, GameScreen[] screensToLoad)
        {
            _loadingIsSlow = loadingIsSlow;
            _screensToLoad = screensToLoad;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Méthode de chargement d'un écran de chargement
        /// </summary>
        public static void Load(ScreenManager screenManager, bool loadingIsSlow, PlayerIndex? controllingPlayer, params GameScreen[] screensToLoad)
        {
            foreach (GameScreen screen in screenManager.GetScreens())
            {
                screen.ExitScreen();
            }

            var loadingScreen = new LoadingScreen(loadingIsSlow, screensToLoad);

            screenManager.AddScreen(loadingScreen, controllingPlayer);
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Si tous les écrans ont terminé leur transition, il est temps de charger !
            if (_otherScreensAreGone)
            {
                ScreenManager.RemoveScreen(this);

                foreach (GameScreen screen in _screensToLoad)
                {
                    if (screen != null)
                    {
                        ScreenManager.AddScreen(screen, ControllingPlayer);
                    }
                }

                ScreenManager.Game.ResetElapsedTime();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // On cherche à savoir si on est le seul écran actif, ce qui signifie que le chargement est terminé.
            if ((ScreenState == ScreenState.Active) && (ScreenManager.GetScreens().Length == 1))
            {
                _otherScreensAreGone = true;
            }

            if (_loadingIsSlow)
            {
                SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                SpriteFont font = ScreenManager.Font;

                const string message = "Loading...";

                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
                var viewportSize = new Vector2(viewport.Width, viewport.Height);
                Vector2 textSize = font.MeasureString(message);
                Vector2 textPosition = (viewportSize - textSize) / 2;

                Color color = Color.White * TransitionAlpha;

                spriteBatch.Begin();
                spriteBatch.DrawString(font, message, textPosition, color);
                spriteBatch.End();
            }
        }
        #endregion
    }
}
