/* CollisonManager.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng:      2016.12.01: Created & Coded
 *      Cynthia Cheng:      2016.12.04: Coded
 *      Aaron MacPherson:   2016.12.06: Coded
 *      Cynthia Cheng:      2016.12.06: Coded
 *      Aaron MacPherson:   2016.12.07: Coded
 *      Cynthia Cheng:      2016.12.07: Coded
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
        private List<StrengthItem> strengthItems;
        private SoundEffect punch;
        private SoundEffect playerHit;
        private SoundEffect playerDead;
        private SoundEffect itemPickup;
        private Boss boss;

        public CollisionManager(Game game,
            Player player1,
            List<Enemy> enemies, 
            List<HealthItem> healthItems,
            List<StrengthItem> strengthItems,
            SoundEffect punch,
            SoundEffect playerHit, 
            SoundEffect playerDead,
            SoundEffect itemPickup,
            Boss boss) : base(game)
        {
            this.player1 = player1;
            this.enemies = enemies;
            this.healthItems = healthItems;
            this.strengthItems = strengthItems;
            this.punch = punch;
            this.playerHit = playerHit;
            this.playerDead = playerDead;
            this.itemPickup = itemPickup;
            this.boss = boss;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            Rectangle player1Rectangle = player1.getBounds();
            List<Rectangle> healthItemRectangles = new List<Rectangle>();
            List<Rectangle> strengthItemRectangles = new List<Rectangle>();
            List<Rectangle> enemiesRectangle = new List<Rectangle>();

            //upper wall
            if (player1.Position.Y < 325)
            {
                player1.Position = new Vector2(player1.Position.X, 325);
            }

            //bottom wall
            if (player1.Position.Y > 410)
            {
                player1.Position = new Vector2(player1.Position.X, 410);
            }

            //left wall
            if (player1.Position.X < 0)
            {
                player1.Position = new Vector2(0, player1.Position.Y);
            }

            //right wall
            if (player1.Position.X > Shared.stage.X - 50)
            {
                player1.Position = new Vector2(Shared.stage.X - 50, player1.Position.Y);
            }

            for (int i = 0; i < healthItems.Count; i++)
            {
                healthItemRectangles.Add(healthItems[i].getBounds());
            }

            for (int i = 0; i < strengthItems.Count; i++)
            {
                strengthItemRectangles.Add(strengthItems[i].getBounds());
            }



            for (int i = 0; i < healthItemRectangles.Count; i++)
            {
                if (player1Rectangle.Intersects(healthItemRectangles[i]) && healthItems[i].Enabled)
                {

                    if (player1.Health >= 500)
                    {
                        itemPickup.Play();
                        player1.Health = 500;
                        healthItems[i].Enabled = false;
                        healthItems[i].Visible = false;

                    }
                    if (player1.Health < 500)
                    {
                        itemPickup.Play();
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
               
            for (int i = 0; i < strengthItemRectangles.Count; i++)
            {
                if (player1Rectangle.Intersects(strengthItemRectangles[i]) && strengthItems[i].Enabled)
                {
                    itemPickup.Play();
                    player1.AttackStrength += 2;
                    strengthItems[i].Enabled = false;
                    strengthItems[i].Visible = false;
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
                    if (player1.State == Player.CharacterState.Uppercut && player1.FrameIndex >= player1.CurrentFrames.Count - 1)
                    {
                        punch.Play();
                        enemies[enemyCounter].Health -= player1.AttackStrength;
                    }

                    if (enemies[enemyCounter].State == Enemy.EnemyState.Attack && enemies[enemyCounter].FrameIndex >= enemies[enemyCounter].CurrentFrames.Count - 2)
                    {
                        if (player1.Health > 0)
                        {
                            playerHit.Play();
                            player1.Health -= enemies[enemyCounter].AttackStrength;
                        }
                        else
                        {
                            player1.Health = 0;
                        }
                    }
                }
                enemyCounter++;
            }

            foreach (Enemy enemy in enemies)
            {
                if (enemy.Position.Y < 325)
                {
                    enemy.Position = new Vector2(enemy.Position.X, 325);
                }

                if (enemy.Position.X < 0)
                {
                    enemy.Position = new Vector2(0, enemy.Position.Y);
                }
            }
            Rectangle bossRectangle = boss.getBounds();
            if (player1Rectangle.Intersects(bossRectangle))
            {
                if (player1.State == Player.CharacterState.Uppercut && player1.FrameIndex >= player1.CurrentFrames.Count - 1)
                {
                    boss.Health -= player1.AttackStrength;
                }

                if (boss.State == Boss.BossState.Attack && boss.FrameIndex >= boss.CurrentFrames.Count - 3)
                {
                    if (player1.Health > 0)
                    {
                        playerHit.Play();
                        player1.Health -= boss.AttackStrength;
                    }
                    else
                    {
                        player1.Health = 0;
                    }
                }
            }

            if (boss.Position.Y < 325)
            {
                boss.Position = new Vector2(boss.Position.X, 325);
            }

            if (boss.Position.X < 0)
            {
                boss.Position = new Vector2(0, boss.Position.Y);
            }

            if (boss.Position.Y > Shared.stage.Y - boss.Dimension.Y)
            {
                boss.Position = new Vector2(boss.Position.X, Shared.stage.Y - boss.Dimension.Y);
            }

            if (player1.Health == 0)
                    {
                        player1.State = Player.CharacterState.Death;
                        player1.Movement = Player.Direction.Idle;

                        if (player1.FrameIndex >= player1.CurrentFrames.Count - 1)
                        {
                            playerDead.Play();
                            Shared.gameOver = true;
                        }
                    }

            base.Update(gameTime);
        }
    }
}

