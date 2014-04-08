using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Platformer.GameStateManagement;
using Platformer.GameStateManagement.Screens;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PlatformerGame : Game
    {
        #region Members

        #endregion

        #region Methods
        public PlatformerGame()
        {

            new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Content.RootDirectory = "Content";

            // Add the ScreenManager
            var screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            // First screens
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            //TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        #endregion
    }

    #region Starting point
    #if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (var game = new PlatformerGame())
            {
                game.Run();
            }
        }
    }
    #endif
    #endregion
}
