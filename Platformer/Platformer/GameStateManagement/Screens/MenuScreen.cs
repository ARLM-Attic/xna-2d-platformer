using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer.GameStateManagement.Helpers;
using System;
using System.Collections.Generic;

namespace Platformer.GameStateManagement.Screens
{
    /// <summary>
    /// Classe de base pour un menu.
    /// </summary>
    public class MenuScreen : GameScreen
    {
        #region Fields
        List<MenuEntry> menuEntries = new List<MenuEntry>();

        int selectedEntry = 0;

        string menuTitle;
        #endregion

        #region Properties
        protected IList<MenuEntry> MenuEntries
        {
            get { return menuEntries; }
        }
        #endregion

        #region Initialization
        public MenuScreen(string menuTitle)
        {
            this.menuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }
        #endregion

        #region Handle Input
        public override void HandleInput(InputState input)
        {
            // Déplacement vers l'entrée précédente du menu
            if (input.IsMenuUp(ControllingPlayer))
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            // Déplacement vers la prochaine entrée du menu
            if (input.IsMenuDown(ControllingPlayer))
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            PlayerIndex playerIndex;

            if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
            else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
            {
                OnCancel(playerIndex);
            }
        }

        /// <summary>
        /// Choix d'une entrée par un joueur.
        /// </summary>
        /// <param name="entryIndex">Le choix du joueur</param>
        /// <param name="playerIndex">Le joueur effectuant le choix</param>
        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            menuEntries[entryIndex].OnSelectEntry(playerIndex);
        }

        /// <summary>
        /// Le joueur décide de revenir en arrière (B ou Echap par exemple).
        /// </summary>
        /// <param name="playerIndex">Le joueur à l'origine du retour en arrière</param>
        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }

        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }
        #endregion

        #region Update & Draw
        protected virtual void UpdateMenuEntryLocations()
        {
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            Vector2 position = new Vector2(0f, 175f);

            foreach (MenuEntry entry in menuEntries)
            {
                position.X = ScreenManager.GraphicsDevice.Viewport.Width / 2 - entry.GetWidth(this) / 2;

                if (ScreenState == ScreenState.TransitionOn)
                {
                    position.X -= transitionOffset * 256;
                }
                else
                {
                    position.X += transitionOffset * 256;
                }

                entry.Position = position;

                position.Y += entry.GetHeight(this);
            }
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                menuEntries[i].Update(this, isSelected, gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            UpdateMenuEntryLocations();

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch sb = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            sb.Begin();

            for (int i = 0; i < menuEntries.Count; i++)
            {
                MenuEntry entry = menuEntries[i];

                bool isSelected = IsActive && (i == selectedEntry);

                entry.Draw(this, isSelected, gameTime);
            }

            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            Color titleColor = new Color(255, 255, 255) * TransitionAlpha;
            float titleScale = 1.25f;

            titlePosition.Y -= transitionOffset * 100;

            sb.DrawString(font, menuTitle, titlePosition, titleColor, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);

            sb.End();
        }
        #endregion

    }
}
