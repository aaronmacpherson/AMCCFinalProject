/* ActionScene.cs
 * Final Project
 * Revision History
 *      Aaron MacPherson: 2016.12.2: Created & Coded
 *      Aaron MacPherson: 2016.12.06: Coded
 *      
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
    public class LevelBackground : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D levelTexture;
        private Rectangle sourceRectangle;
        private Vector2 position;
        private Vector2 scrollSpeed;
        private Vector2 position1, position2;

        public LevelBackground(Game game, SpriteBatch spriteBatch,
            Texture2D levelTexture, Rectangle sourceRectangle,
            Vector2 position, Vector2 scrollSpeed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.levelTexture = levelTexture;
            this.sourceRectangle = sourceRectangle;
            this.position = position;
            this.scrollSpeed = scrollSpeed;

            position1 = position;
            position2 = new Vector2(position1.X + levelTexture.Width, position1.Y);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            position1 -= scrollSpeed;
            if (position1.X < -levelTexture.Width)
            {
                position1.X = position2.X + levelTexture.Width;
            }

            position2 -= scrollSpeed;
            if (position2.X < -levelTexture.Width)
            {
                position2.X = position1.X + levelTexture.Width;
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(levelTexture, position1, sourceRectangle, Color.White);
            spriteBatch.Draw(levelTexture, position2, sourceRectangle, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

