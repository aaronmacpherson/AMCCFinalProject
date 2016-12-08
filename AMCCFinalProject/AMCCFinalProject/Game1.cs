/* Game1.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng:    2016.12.01: Created & Coded
 *      Aaron MacPherson: 2016.12.07: Coded
 *      Cynthia Cheng:    2016.12.07: Coded
 *      
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace AMCCFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private HowToPlayScene howToPlayScene;
        private AboutScene aboutScene;
        private GameOverScene gameOverScene;
        private LevelManagerScene levelManagerScene;
        private Song menuSong;
        private Song actionSong;
        private KeyboardState oldState;

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
            Shared.graphics = graphics;

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

            howToPlayScene = new HowToPlayScene(this, spriteBatch);
            this.Components.Add(howToPlayScene);

            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);

            gameOverScene = new GameOverScene(this, spriteBatch);
            this.Components.Add(gameOverScene);

            levelManagerScene = new LevelManagerScene(this, spriteBatch);
            this.Components.Add(levelManagerScene);

            menuSong = this.Content.Load<Song>("music/menu");
            actionSong = this.Content.Load<Song>("music/level1");


            HideAllScenes();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(menuSong);
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
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter) && !oldState.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    MediaPlayer.Stop();
                    MediaPlayer.Play(actionSong);
                    MediaPlayer.IsRepeating = true;
                    actionScene.Show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    howToPlayScene.Show();
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    helpScene.Enabled = true;
                    helpScene.Show();
                }
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    aboutScene.Show();
                }

                // Exits/Closes program
                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }

                oldState = ks;

            }


            if (Shared.gameOver == true)
            {
                HideAllScenes();
                gameOverScene.Show();
            }

            if (gameOverScene.Enabled)
            {
                Shared.level = 1;
                Shared.currentScore = 0;
                Shared.gameOver = false;
                actionScene.Dispose();
                actionScene = new ActionScene(this, spriteBatch);
                this.Components.Add(actionScene);

                selectedIndex = gameOverScene.MyMenuComponent.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    oldState = ks;
                    HideAllScenes();
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(menuSong);
                    Shared.gameOver = false;
                    startScene.Show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    HideAllScenes();
                    actionScene.Show();
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }


            if (Shared.nextLevel == true)
            {
                HideAllScenes();
                levelManagerScene.Show();
            }


            if (levelManagerScene.Enabled)
            {
                Shared.gameOver = false;
                Shared.nextLevel = false;
                actionScene.Dispose();
                actionScene = new ActionScene(this, spriteBatch);
                this.Components.Add(actionScene);

                selectedIndex = levelManagerScene.MyMenuComponent.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    Shared.level++;
                    oldState = ks;
                    HideAllScenes();
                    actionScene.Show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    Shared.level = 1;
                    Shared.currentScore = 0;
                    oldState = ks;
                    HideAllScenes();
                    startScene.Show();
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    Shared.level = 1;
                    Shared.currentScore = 0;
                    HideAllScenes();
                    actionScene.Show();
                }
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (actionScene.Enabled || helpScene.Enabled || howToPlayScene.Enabled || aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    HideAllScenes();
                    MediaPlayer.Stop();
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(menuSong);
                    Shared.gameOver = false;
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
