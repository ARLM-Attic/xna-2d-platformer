using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer.GameStateManagement.Helpers
{
    /// <summary>
    /// Classe de base pour un menu.
    /// </summary>
    public class MenuScreen : GameScreen
    {
        #region Fields

        readonly List<MenuEntry> _menuEntries = new List<MenuEntry>();

        int _selectedEntry;

        readonly string _menuTitle;
        #endregion

        #region Properties
        protected IList<MenuEntry> MenuEntries
        {
            get { return _menuEntries; }
        }
        #endregion

        #region Initialization
        public MenuScreen(string menuTitle)
        {
            _selectedEntry = 0;
            _menuTitle = menuTitle;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            #region Actions initialization
            InputManager.AddAction("MenuUp").Add(Keys.Up).Add(Buttons.LeftThumbstickUp).Add(Buttons.DPadUp);
            InputManager.AddAction("MenuDown").Add(Keys.Down)
                                              .Add(Buttons.LeftThumbstickDown).Add(Buttons.DPadDown);
            InputManager.AddAction("MenuLeft").Add(Keys.Left)
                                              .Add(Buttons.LeftThumbstickLeft).Add(Buttons.DPadLeft);
            InputManager.AddAction("MenuRight").Add(Keys.Right)
                                               .Add(Buttons.LeftThumbstickRight).Add(Buttons.DPadRight);
            InputManager.AddAction("MenuSelect").Add(Keys.Space).Add(Keys.Enter)
                                                .Add(Buttons.A).Add(Buttons.Start);
            InputManager.AddAction("MenuCancel").Add(Keys.Escape)
                                                .Add(Buttons.B).Add(Buttons.Back);
            #endregion
        }
        #endregion

        #region Handle Input
        public override void HandleInput()
        {
            InputManager.Update();
            // Déplacement vers l'entrée précédente du menu
            if (InputManager["MenuUp"].IsTapped)
            {
                _selectedEntry--;

                if (_selectedEntry < 0)
                    _selectedEntry = _menuEntries.Count - 1;
            }

            // Déplacement vers la prochaine entrée du menu
            if (InputManager["MenuDown"].IsTapped)
            {
                _selectedEntry++;

                if (_selectedEntry >= _menuEntries.Count)
                    _selectedEntry = 0;
            }

            const PlayerIndex playerIndex = PlayerIndex.One;

            if (InputManager["MenuSelect"].IsDown)
            {
                OnSelectEntry(_selectedEntry, playerIndex);
            }
            else if (InputManager["MenuCancel"].IsDown)
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
            _menuEntries[entryIndex].OnSelectEntry(playerIndex);
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
            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            var position = new Vector2(0f, 175f);

            foreach (MenuEntry entry in _menuEntries)
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

            for (int i = 0; i < _menuEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == _selectedEntry);

                _menuEntries[i].Update(this, isSelected, gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            UpdateMenuEntryLocations();

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch sb = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            sb.Begin();

            for (var i = 0; i < _menuEntries.Count; i++)
            {
                MenuEntry entry = _menuEntries[i];

                bool isSelected = IsActive && (i == _selectedEntry);

                entry.Draw(this, isSelected, gameTime);
            }

            var transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            if (graphics != null)
            {
                Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
                Vector2 titleOrigin = font.MeasureString(_menuTitle) / 2;
                Color titleColor = new Color(255, 255, 255) * TransitionAlpha;
                const float titleScale = 1.25f;

                titlePosition.Y -= transitionOffset * 100;

                sb.DrawString(font, _menuTitle, titlePosition, titleColor, 0,
                    titleOrigin, titleScale, SpriteEffects.None, 0);
            }

            sb.End();
        }
        #endregion

    }
}
