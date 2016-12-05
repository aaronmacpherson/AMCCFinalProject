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
    public class StatusMenu : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont;
        private Vector2 position;
        private string message;
        Color colour;

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        public StatusMenu(Game game, 
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            Vector2 position,
            string message,
            Color colour) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.position = position;
            this.message = message;
            this.colour = colour;
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
            spriteBatch.DrawString(regularFont, message, position, colour);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
