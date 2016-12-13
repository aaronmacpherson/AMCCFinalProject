/* MenuIcons.cs
 * Final Project
 * Revision History
 *      Aaron MacPherson:   2016.12.09: Created
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
    class MenuIcons : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D iconTexture;
        Vector2 position;

        public MenuIcons(Game game, SpriteBatch spriteBatch, Texture2D itemTexture, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.iconTexture = itemTexture;
            this.position = position;

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
            spriteBatch.Draw(iconTexture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

