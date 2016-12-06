/* ActionScene.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng:      2016.12.1: Created & Coded
 *      Cynthia Cheng:      2016.12.2: Coded
 *      Aaron MacPherson:   2016.12.6: Coded
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
        private Vector2 initialPosition = new Vector2(0, 400);
        private int initialDelay = 3;

        private List<Enemy> enemies;
        private const float enemyAttackDistance = 20f;
        private int maxEnemyCount; //adjust this based on loaded level
        private TimeSpan enemySpawnTime = TimeSpan.FromSeconds(1.0f);
        private TimeSpan previousSpawnTime = TimeSpan.Zero;
        private Random random;
        private Texture2D enemyTexture, enemy1Texture, enemy2Texture, enemy3Texture, enemy4Texture;
        Vector2 stage;

        private int level;

        private StatusMenu statusMenu;
        private SpriteFont regularFont;
        private int hiScore;

        private CollisionManager collisionManager;

        KeyboardState oldState; //Not currently used

        public List<Enemy> Enemies
        {
            get
            {
                return enemies;
            }

            set
            {
                enemies = value;
            }
        }

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            stage = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            Texture2D levelTexture = game.Content.Load<Texture2D>("levels/level1");
            Vector2 levelPosition1 = new Vector2(0, GraphicsDevice.Viewport.Height - 800);
            Rectangle levelSourceRectangle = new Rectangle(0, 0, 1920, 800);

            LevelBackground level1 = new LevelBackground(game, spriteBatch, levelTexture,
                levelSourceRectangle, levelPosition1, new Vector2(2, 0));

            this.Components.Add(level1);

            Texture2D player1Texture = game.Content.Load<Texture2D>("images/player1");
            player1 = new Player(game, spriteBatch, player1Texture, initialPosition, initialDelay);
            this.Components.Add(player1);

            random = new Random();
            enemies = new List<Enemy>();

            enemy1Texture = game.Content.Load<Texture2D>("images/enemy1");
            enemy2Texture = game.Content.Load<Texture2D>("images/enemy2");
            enemy3Texture = game.Content.Load<Texture2D>("images/enemy3");
            enemy4Texture = game.Content.Load<Texture2D>("images/enemy4");

            regularFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            string statusMenuScore = "Player Health" + player1.Health + " Current Score " + player1.Score;
            statusMenu = new StatusMenu(game, spriteBatch, regularFont, Vector2.Zero, statusMenuScore, Color.Black);
            this.Components.Add(statusMenu);

            collisionManager = new CollisionManager(game, player1, enemies);
            this.Components.Add(collisionManager);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            updateEnemy(gameTime);
            if (player1.Score > hiScore)
            {
                hiScore = player1.Score;
            }
            string statusMenuScore = "Player Health: " + player1.Health + " Current Score: " + player1.Score + " HiScore: " + hiScore;
            statusMenu.Message = statusMenuScore;

            KeyboardState keyboardState = Keyboard.GetState();

            playerDirection(keyboardState);
            playerAction(keyboardState);

            oldState = keyboardState;
            base.Update(gameTime);
        }

        public void addEnemy()
        {
            int enemyVersion = random.Next(1, 4);
            switch (enemyVersion)
            {
                case 1:
                    enemyTexture = enemy1Texture;
                    break;
                case 2:
                    enemyTexture = enemy2Texture;
                    break;
                case 3:
                    enemyTexture = enemy3Texture;
                    break;
                default:
                    break;
            }
            Enemy enemy = new Enemy(Game, spriteBatch, enemyTexture, new Vector2(800, 400), initialDelay, enemyVersion);
            enemies.Add(enemy);
            int i = enemies.Count - 1;
            Game.Components.Add(enemies[i]);
        }

        public void updateEnemy(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;
                enemySpawnTime = TimeSpan.FromSeconds((float)random.Next(1, 3));
                addEnemy();
                if (--maxEnemyCount <= 0)
                {
                    //start boss fight
                }
            }

            for (int i = enemies.Count-1; i >= 0; i--)
            {
                enemyAIState(enemies[i]);
                enemyAIDirection(enemies[i]);
                if (!enemies[i].Active)
                {
                    enemies.RemoveAt(i);
                }
            }
            
        }

        public void enemyAIState(Enemy enemy)
        {
            float distanceFromPlayer1 = Vector2.Distance(enemy.Position, player1.Position);
            float enemyAttackThreshold = enemyAttackDistance;
            if (distanceFromPlayer1 < enemyAttackThreshold)
            {
                enemy.State = Enemy.EnemyState.Attack;
            }
            else if (enemy.Health <= 0)
            {
                enemy.State = Enemy.EnemyState.Death;
                enemy.Movement = Enemy.Direction.Stop;
                if (!enemy.ScoreAdded)
                {
                    player1.Score += enemy.ScoreValue;
                    enemy.ScoreAdded = true;
                }
            }
            else
            {
                enemy.State = Enemy.EnemyState.Move;
            }
        }

        public void enemyAIDirection(Enemy enemy)
        {
            if (enemy.State == Enemy.EnemyState.Move)
            {
                if (player1.Position.X > enemy.Position.X && player1.Position.Y > enemy.Position.Y)
                {
                    enemy.Movement = Enemy.Direction.SouthEast;
                }
                else if (player1.Position.X > enemy.Position.X && player1.Position.Y < enemy.Position.Y)
                {
                    enemy.Movement = Enemy.Direction.NorthEast;
                }
                else if (player1.Position.X < enemy.Position.X && player1.Position.Y > enemy.Position.Y)
                {
                    enemy.Movement = Enemy.Direction.SouthWest;
                }
                else if (player1.Position.X < enemy.Position.X && player1.Position.Y < enemy.Position.Y)
                {
                    enemy.Movement = Enemy.Direction.NorthWest;
                }
                else if (player1.Position.X >= enemy.Position.X)
                {
                    enemy.Movement = Enemy.Direction.East;
                }
                else if (player1.Position.X < enemy.Position.X)
                {
                    enemy.Movement = Enemy.Direction.West;
                }
                else if (player1.Position.Y >= enemy.Position.Y)
                {
                    enemy.Movement = Enemy.Direction.North;
                }
                else if (player1.Position.Y < enemy.Position.Y)
                {
                    enemy.Movement = Enemy.Direction.South;
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
            if (player1.Health > 0)
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
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}