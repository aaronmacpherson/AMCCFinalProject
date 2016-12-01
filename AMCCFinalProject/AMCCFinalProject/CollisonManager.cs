/* CollisonManager.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.01: Created & Coded
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace AMCCFinalProject
{
    public class CollisionManager : GameComponent
    {
        private Player player1;

        public CollisionManager(Game game,
            Player player1) : base(game)
        {
            this.player1 = player1;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

