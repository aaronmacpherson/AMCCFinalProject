/* StrengthItem.cs
 * Final Project
 * Revision History
 *      Aaron MacPherson: 2016.12.06: Created
 *      Aaron MacPherson: 2016.12.07: Coded
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
    public class StrengthItem : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D itemTexture;
        Vector2 dimension;
        Vector2 position;
        Vector2 speed;

        public StrengthItem(Game game, SpriteBatch spriteBatch, Texture2D itemTexture, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.itemTexture = itemTexture;
            this.position = position;
            this.speed = speed;

            dimension = new Vector2(12, 23);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            position -= speed;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(itemTexture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y);
        }
    }
}
