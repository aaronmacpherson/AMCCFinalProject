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
        private List<Enemy> enemies;

        public CollisionManager(Game game,
            Player player1,
            List<Enemy> enemies) : base(game)
        {
            this.player1 = player1;
            this.enemies = enemies;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            Rectangle player1Rectangle = player1.getBounds();
            List<Rectangle> enemiesRectangle = new List<Rectangle>();

            for (int i = 0; i < enemies.Count; i++)
            {
                enemiesRectangle.Add(enemies[i].getBounds());
            }

            int enemyCounter = 0;
            foreach (Rectangle enemyRectangle in enemiesRectangle)
            {
                if (player1Rectangle.Intersects(enemyRectangle))
                {
                    if (player1.State == Player.CharacterState.Uppercut && player1.FrameIndex >= player1.CurrentFrames.Count-1)
                    {
                        enemies[enemyCounter].Health -= 5;
                    }

                    if (enemies[enemyCounter].State == Enemy.EnemyState.Attack)
                    {
                        if (player1.Health > 0)
                        {
                            player1.Health -= enemies[enemyCounter].AttackStrength;
                        }
                        else
                        {
                            player1.Health = 0;
                        }
                    }

                    if (player1.Health <= 0)
                    {
                        player1.State = Player.CharacterState.Death;
                        player1.Movement = Player.Direction.Idle;
                    }
                }
                enemyCounter++;
            }

            base.Update(gameTime);
        }
    }
}

