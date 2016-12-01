/* HelpScene.cs
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
    public class HelpScene : GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D helpTex;
        public HelpScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            helpTex = game.Content.Load<Texture2D>("images/helpImage");
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
            spriteBatch.Draw(helpTex, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}