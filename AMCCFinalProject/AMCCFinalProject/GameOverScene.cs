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
    public class GameOverScene : GameScene
    {
        private MenuComponent myMenuComponent;
        private SpriteBatch spriteBatch;
        string[] menus = { "Main Menu", "Reset", "Quit" };

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

        public GameOverScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            myMenuComponent = new MenuComponent(game, spriteBatch, game.Content.Load<SpriteFont>("fonts/regularFont"),
                game.Content.Load<SpriteFont>("fonts/hilightFont"), menus);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
