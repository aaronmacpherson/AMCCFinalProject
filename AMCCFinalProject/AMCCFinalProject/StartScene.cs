/* StartScene.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.1: Created & Coded
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AMCCFinalProject
{

    public class StartScene : GameScene
    {
        private MenuComponent myMenuComponent;

        public MenuComponent MyMenuComponent
        {
            get
            {
                return myMenuComponent;
            }

            set
            {
                myMenuComponent = value;
            }
        }
        
        private Texture2D startScreenTexture;
        private SpriteBatch spriteBatch;
        string[] menus = {  "Start Game",
                            "How To Play",
                            "Help",
                            "About",
                            "Quit" };
        public StartScene(Game game,
            SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            startScreenTexture = game.Content.Load<Texture2D>("sceneImages/startScreen");
            myMenuComponent = new MenuComponent(game,
                spriteBatch,
                game.Content.Load<SpriteFont>("fonts/regularFont"),
                game.Content.Load<SpriteFont>("fonts/hilightFont"),
                menus);
            this.Components.Add(myMenuComponent);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(startScreenTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
