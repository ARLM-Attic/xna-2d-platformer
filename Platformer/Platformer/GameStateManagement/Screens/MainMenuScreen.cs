using System;
using Microsoft.Xna.Framework;
using Platformer.GameStateManagement.Helpers;

namespace Platformer.GameStateManagement.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        #region Initialization
        public MainMenuScreen()
            : base("Platformer")
        {
            // Menu entries
            MenuEntry newGameMenuEntry = new MenuEntry("New Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Add event to each entry
            newGameMenuEntry.Selected += newGameMenuEntry_Selected;
            optionsMenuEntry.Selected += optionsMenuEntry_Selected;
            exitMenuEntry.Selected += OnCancel;

            // Finally 
            MenuEntries.Add(newGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }
        #endregion

        #region Methods
        private void optionsMenuEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void newGameMenuEntry_Selected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameCoreScreen());
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            ScreenManager.Game.Exit();
        }
        #endregion
    }
}
