/* AboutScene.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.07: Created & Coded
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
    public class AboutScene: GameScene
    {
        SpriteBatch spriteBatch;
        Texture2D aboutTex;
        public AboutScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            aboutTex = game.Content.Load<Texture2D>("images/about");
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
            spriteBatch.Draw(aboutTex, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}