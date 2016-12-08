/* CollisonManager.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng:      2016.12.01: Created & Coded
 *      Aaron MacPherson:   2016.12.06: Coded
 *      Aaron MacPherson:   2016.12.07: Coded
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
        private List<HealthItem> healthItems;
        private SoundEffect punch;
        private SoundEffect playerHit;
        private SoundEffect playerDead;

        public CollisionManager(Game game,
            Player player1,
            List<Enemy> enemies, 
            List<HealthItem> healthItems,
            SoundEffect punch, SoundEffect playerHit, 
            SoundEffect playerDead) : base(game)
        {
            this.player1 = player1;
            this.enemies = enemies;
            this.healthItems = healthItems;
            this.punch = punch;
            this.playerHit = playerHit;
            this.playerDead = playerDead;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            Rectangle player1Rectangle = player1.getBounds();
            List<Rectangle> healthItemRectangles = new List<Rectangle>();
            List<Rectangle> enemiesRectangle = new List<Rectangle>();

            //upper wall
            if (player1.Position.Y < 325)
            {
                player1.Position = new Vector2(player1.Position.X, 325);
            }
            //left wall
            if (player1.Position.X < 0)
            {
                player1.Position = new Vector2(0, player1.Position.Y);
            }
            //bottom wall
            if (player1.Position.Y > 420)
            {
                player1.Position = new Vector2(player1.Position.X, 420);
            }

            for (int i = 0; i < healthItems.Count; i++)
            {
                healthItemRectangles.Add(healthItems[i].getBounds());
            }

            for (int i = 0; i < healthItemRectangles.Count; i++)
            {
                if (player1Rectangle.Intersects(healthItemRectangles[i]))
                {
                    if (player1.Health == 500)
                    {
                        player1.Health = player1.Health;
                        healthItems[i].Enabled = false;
                        healthItems[i].Visible = false;
                        
                    }
                    if (player1.Health < 500)
                    {
                        player1.Health += 20;
                        healthItems[i].Enabled = false;
                        healthItems[i].Visible = false;
                    }
                }
                else
                {
                    player1.Health = player1.Health;
                }
            }


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
                        punch.Play();
                        enemies[enemyCounter].Health -= player1.AttackStrength;
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

                    if (player1.Health == 0)
                    {
                        player1.State = Player.CharacterState.Death;
                        player1.Movement = Player.Direction.Idle;

                        if (player1.FrameIndex >= player1.CurrentFrames.Count - 1)
                        {
                            Shared.gameOver = true;
                        }
                    }
                }
                enemyCounter++;
            }

            base.Update(gameTime);
        }
    }
}

