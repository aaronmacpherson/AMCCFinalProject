/* Game1.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.1: Created & Coded
 *      
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AMCCFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);

            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            startScene.Show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        //Helper function/method to hide all scenes
        private void HideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in this.Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.Hide();
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Auto exits game if ESC is pressed, delete if using escape explicitly (i.e. changing scenes back to start menu)
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here
            
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.MyMenuComponent.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {

                    HideAllScenes();
                    actionScene.Show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    helpScene.Show();
                }

                // Exits/Closes program
                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }

            }
            
            if (actionScene.Enabled || helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    startScene.Show();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
