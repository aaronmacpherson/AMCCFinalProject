/* ActionScene.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.1: Created & Coded
 *      Cynthia Cheng: 2016.12.2: Coded
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
    public class ActionScene : GameScene
    {
        SpriteBatch spriteBatch;
        private Player player1;
        private Enemy enemy1;
        private Vector2 initialPosition = new Vector2(0, 400);
        private int initialPlayerDelay = 3;
        TimeSpan enemySpawnTime = TimeSpan.FromSeconds(1.0f);
        TimeSpan previousSpawnTime = TimeSpan.Zero;
        Random random = new Random();
        private CollisionManager collisionManager; //Base Template only
        KeyboardState oldState; //Not currently used
        private const float enemyAttackDistance = 20f;
        private List<Enemy> enemies;
        private int level;
        Texture2D enemy1Texture;

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;

            Texture2D player1Texture = game.Content.Load<Texture2D>("images/player1");
            player1 = new Player(game, spriteBatch, player1Texture, initialPosition, initialPlayerDelay);
            this.Components.Add(player1);



            enemy1Texture = game.Content.Load<Texture2D>("images/enemy1");
            enemy1 = new Enemy(game, spriteBatch, enemy1Texture, new Vector2(300, 400), initialPlayerDelay);
            this.Components.Add(enemy1);

            collisionManager = new CollisionManager(game, player1);
            this.Components.Add(collisionManager);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            addEnemy(gameTime);
            updateEnemy();

            KeyboardState keyboardState = Keyboard.GetState();

            playerDirection(keyboardState);
            playerAction(keyboardState);


            oldState = keyboardState;
            base.Update(gameTime);
        }

        public void addEnemy(GameTime gametime)
        {
        }

        public void updateEnemy()
        {
            enemyAIState();
            enemyAIDirection();
        }

        public void enemyAIState()
        {
            float distanceFromPlayer1 = Vector2.Distance(enemy1.Position, player1.Position);
            float enemyAttackThreshold = enemyAttackDistance;
            if (distanceFromPlayer1 < enemyAttackThreshold)
            {
                enemy1.State = Enemy.EnemyState.Attack;
            }
            else if (enemy1.Health <= 0)
            {
                enemy1.State = Enemy.EnemyState.Death;
            }
            else
            {
                enemy1.State = Enemy.EnemyState.Move;
            }
        }

        public void enemyAIDirection()
        {
            if (enemy1.State == Enemy.EnemyState.Move)
            {
                if (player1.Position.X > enemy1.Position.X && player1.Position.Y > enemy1.Position.Y)
                {
                    enemy1.Movement = Enemy.Direction.SouthEast;
                }
                else if (player1.Position.X > enemy1.Position.X && player1.Position.Y < enemy1.Position.Y)
                {
                    enemy1.Movement = Enemy.Direction.NorthEast;
                }
                else if (player1.Position.X < enemy1.Position.X && player1.Position.Y > enemy1.Position.Y)
                {
                    enemy1.Movement = Enemy.Direction.SouthWest;
                }
                else if (player1.Position.X < enemy1.Position.X && player1.Position.Y < enemy1.Position.Y)
                {
                    enemy1.Movement = Enemy.Direction.NorthWest;
                }
                else if (player1.Position.X >= enemy1.Position.X)
                {
                    enemy1.Movement = Enemy.Direction.East;
                }
                else if (player1.Position.X < enemy1.Position.X)
                {
                    enemy1.Movement = Enemy.Direction.West;
                }
                else if (player1.Position.Y >= enemy1.Position.Y)
                {
                    enemy1.Movement = Enemy.Direction.North;
                }
                else if (player1.Position.Y < enemy1.Position.Y)
                {
                    enemy1.Movement = Enemy.Direction.South;
                }
            }
        }

        public void playerDirection(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
            {
                player1.Movement = Player.Direction.NorthWest;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
            {
                player1.Movement = Player.Direction.SouthWest;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
            {
                player1.Movement = Player.Direction.NorthEast;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
            {
                player1.Movement = Player.Direction.SouthEast;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                player1.Movement = Player.Direction.East;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                player1.Movement = Player.Direction.West;
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                player1.Movement = Player.Direction.North;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                player1.Movement = Player.Direction.South;
            }
            else
            {
                player1.Movement = Player.Direction.Idle;
            }
        }
        public void playerAction(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.X))
            {
                player1.State = Player.CharacterState.Uppercut;
            }
            else if (keyboardState.IsKeyDown(Keys.Z))
            {
                player1.State = Player.CharacterState.Jump;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Right))
            {
                player1.State = Player.CharacterState.Walking;
            }
            else
            {
                player1.State = Player.CharacterState.Idle;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}