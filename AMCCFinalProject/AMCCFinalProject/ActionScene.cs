/* ActionScene.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng:      2016.12.01: Created & Coded
 *      Cynthia Cheng:      2016.12.02: Coded
 *      Cynthia Cheng:      2016.12.04: Coded
 *      Aaron MacPherson:   2016.12.06: Coded
 *      Cynthia Cheng:      2016.12.06: Coded
 *      Aaron MacPherson:   2016.12.07: Coded
 *      Cynthia Cheng:      2016.12.07: Coded
 *      
 *      
 *      There is a boss.Defeated bool for when the boss is defeated
 *      For the end of level/round score/save use this boss.Defeated == true && boss.Position.X > 800 (the boss has left the screen) before
 *      loading the end of the level
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
using Microsoft.Xna.Framework.Media;


namespace AMCCFinalProject
{
    public class ActionScene : GameScene
    {
        SpriteBatch spriteBatch;

        private Player player1;
        private Vector2 initialPosition = new Vector2(0, 380);
        Texture2D player1Texture;
        private int initialDelay = 3;

        private HealthItem healthItem;
        private List<HealthItem> healthItems;
        private const int MAX_HEALTH_ITEMS = 20;
        private TimeSpan previousHealthSpawn = TimeSpan.Zero;
        private TimeSpan healthItemSpawn = TimeSpan.FromSeconds(5f);
        private Texture2D healthItemTexture;
        private int healthCounter = 0;

        private StrengthItem strengthItem;
        private List<StrengthItem> strengthItems;
        private const int MAX_STRENGTH_ITEMS = 10;
        private TimeSpan previousStrengthSpawn = TimeSpan.Zero;
        private TimeSpan strengthItemSpawn = TimeSpan.FromSeconds(10f);
        private Texture2D strengthItemTexture;
        private int strengthCounter = 0;

        private List<Enemy> enemies;
        private const float enemyAttackDistance = 20f;
        private const float bossAttackDistance = 30f;
        private int maxEnemyCount; //adjust this based on loaded level
        private TimeSpan enemySpawnTime = TimeSpan.FromSeconds(1.0f);
        private TimeSpan previousSpawnTime = TimeSpan.Zero;
        private Random random;
        private Texture2D enemyTexture, enemy1Texture, enemy2Texture, enemy3Texture, enemy4Texture, boss1Texture;
        private Boss boss;
        private int bossVersion = 1;
        private bool bossActivated = false;

        Vector2 stage;

        private StatusMenu statusMenu;
        private SpriteFont regularFont;

        private CollisionManager collisionManager;

        KeyboardState oldState;

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
            random = new Random();
            int randomY = 0;

            Texture2D levelTexture = game.Content.Load<Texture2D>("levels/level1");
            Vector2 levelPosition1 = new Vector2(0, Shared.graphics.PreferredBackBufferHeight - 800);
            Rectangle levelSourceRectangle = new Rectangle(0, 0, 1920, 800);
            LevelBackground level1 = new LevelBackground(game, 
                spriteBatch, 
                levelTexture,
                levelSourceRectangle, 
                levelPosition1, 
                new Vector2(2, 0));
            this.Components.Add(level1);

            maxEnemyCount = Shared.level * 10;

            healthItemTexture = game.Content.Load<Texture2D>("items/lasagna1");
            healthItems = new List<HealthItem>();

            
            for (int i = 0; i < MAX_HEALTH_ITEMS; i++)
            {
                randomY = random.Next(380, 450);
                healthItem = new HealthItem(game, spriteBatch, healthItemTexture, new Vector2(800, randomY), new Vector2(2, 0));
                healthItems.Add(healthItem);
            }

            strengthItemTexture = game.Content.Load<Texture2D>("items/StrengthItem");
            strengthItems = new List<StrengthItem>();

            for (int i = 0; i < MAX_STRENGTH_ITEMS; i++)
            {
                randomY = random.Next(380, 450);
                strengthItem = new StrengthItem(game, spriteBatch, strengthItemTexture, new Vector2(800, randomY), new Vector2(2, 0));
                strengthItems.Add(strengthItem);
            }

            player1Texture = game.Content.Load<Texture2D>("images/player1");
            player1 = new Player(game, spriteBatch, player1Texture, initialPosition, initialDelay);
            this.Components.Add(player1);
 
            enemies = new List<Enemy>();

            enemy1Texture = game.Content.Load<Texture2D>("images/enemy1");
            enemy2Texture = game.Content.Load<Texture2D>("images/enemy2");
            enemy3Texture = game.Content.Load<Texture2D>("images/enemy3");
            enemy4Texture = game.Content.Load<Texture2D>("images/enemy4");

            boss1Texture = game.Content.Load<Texture2D>("images/boss1");
            boss = new Boss(Game, spriteBatch, boss1Texture, new Vector2(800, 400), initialDelay, bossVersion);

            regularFont = game.Content.Load<SpriteFont>("fonts/regularFont");
            string statusMenuScore = "Player Health" + player1.Health + " Current Score " + Shared.currentScore;
            statusMenu = new StatusMenu(game, spriteBatch, regularFont, Vector2.Zero, statusMenuScore, Color.Black);
            this.Components.Add(statusMenu);

            SoundEffect punch = game.Content.Load<SoundEffect>("soundeffects/punch");
            SoundEffect playerHit = game.Content.Load<SoundEffect>("soundeffects/playerhit");
            SoundEffect playerDead = game.Content.Load<SoundEffect>("soundeffects/meow");
            SoundEffect itemPickup = game.Content.Load<SoundEffect>("soundeffects/powerup");

            collisionManager = new CollisionManager(game, player1, enemies, healthItems, strengthItems, punch, playerHit,
                playerDead, itemPickup, boss);
            this.Components.Add(collisionManager);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            updateEnemy(gameTime);
            addHealth(gameTime);
            addStrength(gameTime);
            if (Shared.currentScore > Shared.hiScore)
            {
                Shared.hiScore = Shared.currentScore;
            }
            string statusMenuScore = "Player Health: " + player1.Health + " Current Score: " + Shared.currentScore + " HiScore: " + Shared.hiScore + " Current Level: " + Shared.level;
            statusMenu.Message = statusMenuScore;

            KeyboardState keyboardState = Keyboard.GetState();

            playerDirection(keyboardState);
            playerAction(keyboardState);

            oldState = keyboardState;

            if (player1.State == Player.CharacterState.Death && player1.FrameIndex >= player1.CurrentFrames.Count - 1)
            {
                Shared.currentScore = 0;
                Shared.level = 1;
                Shared.gameOver = true;
            }

            if (boss.Defeated && boss.Position.X > Shared.stage.X)
            {
                Shared.nextLevel = true;
            }

            base.Update(gameTime);

        }

        public void addHealth(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousHealthSpawn > healthItemSpawn)
            {
                previousHealthSpawn = gameTime.TotalGameTime;
                healthItemSpawn = TimeSpan.FromSeconds(20f);
                if (healthCounter < MAX_HEALTH_ITEMS)
                {
                    this.Components.Add(healthItems[healthCounter]);
                    healthCounter++;
                }
            }
        }

        public void addStrength(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousStrengthSpawn > strengthItemSpawn)
            {
                previousStrengthSpawn = gameTime.TotalGameTime;
                strengthItemSpawn = TimeSpan.FromSeconds(30f);
                if (strengthCounter < MAX_STRENGTH_ITEMS)
                {
                    this.Components.Add(strengthItems[strengthCounter]);
                    strengthCounter++;
                }
            }
        }

        public void addEnemy()
        {
            int i = enemies.Count;
            
            if (i <= maxEnemyCount)
            {
                int enemyPositionY = 0;
                int enemyVersion = random.Next(1, 4);
                switch (enemyVersion)
                {
                    case 1:
                        enemyTexture = enemy1Texture;
                        enemyPositionY = random.Next(300, 450);
                        break;
                    case 2:
                        enemyTexture = enemy2Texture;
                        enemyPositionY = random.Next(300, 450);
                        break;
                    case 3:
                        enemyTexture = enemy3Texture;
                        enemyPositionY = random.Next(100, 300);
                        break;
                    default:
                        break;
                }
                Enemy enemy = new Enemy(Game, spriteBatch, enemyTexture, new Vector2(800, enemyPositionY), initialDelay, enemyVersion);
                enemies.Add(enemy);
                this.Components.Add(enemies[i]);
            }

        }
    

        public void updateEnemy(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;
                enemySpawnTime = TimeSpan.FromSeconds((float)random.Next(1, 3));
                addEnemy();
                if (--maxEnemyCount <= 0 && enemies.Count == 0)
                {
                    if (!bossActivated)
                    {
                        this.Components.Add(boss);
                        bossActivated = true;
                    }
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

            if (bossActivated)
            {
                bossAIState(boss, gameTime);
                bossAIDirection(boss);
            }
        }


    public void bossAIState(Boss boss, GameTime gameTime)
    {
        float distanceFromPlayer1 = Vector2.Distance(boss.Position, player1.Position);
        float enemyAttackThreshold = bossAttackDistance;
        if (distanceFromPlayer1 < enemyAttackThreshold && boss.Active == true)
        {
            boss.State = Boss.BossState.Attack;
        }
        else if (boss.Health <= 0)
        {
            boss.State = Boss.BossState.Death;
            boss.Movement = Boss.Direction.East;
            boss.Speed = 40;
            boss.Defeated = true;
            if (!boss.ScoreAdded)
            {
                Shared.currentScore += boss.ScoreValue;
                boss.ScoreAdded = true;
            }
        }
        else
        {
            boss.State = Boss.BossState.Move;
            bossAIDirection(boss);
        }
    }

    public void bossAIDirection(Boss boss)
    {
        if (boss.State == Boss.BossState.Move)
        {
            if (player1.Position.X > boss.Position.X - 20 && player1.Position.Y > boss.Position.Y)
            {
                boss.Movement = Boss.Direction.SouthEast;
            }
            else if (player1.Position.X > boss.Position.X - 20 && player1.Position.Y < boss.Position.Y)
            {
                boss.Movement = Boss.Direction.NorthEast;
            }
            else if (player1.Position.X < boss.Position.X - 20 && player1.Position.Y > boss.Position.Y)
            {
                boss.Movement = Boss.Direction.SouthWest;
            }
            else if (player1.Position.X < boss.Position.X - 20 && player1.Position.Y < boss.Position.Y)
            {
                boss.Movement = Boss.Direction.NorthWest;
            }
            else if (player1.Position.X >= boss.Position.X - 20)
            {
                boss.Movement = Boss.Direction.East;
            }
            else if (player1.Position.X < boss.Position.X - 20)
            {
                boss.Movement = Boss.Direction.West;
            }
            else if (player1.Position.Y >= boss.Position.Y - 20)
            {
                boss.Movement = Boss.Direction.North;
            }
            else if (player1.Position.Y < boss.Position.Y - 20)
            {
                boss.Movement = Boss.Direction.South;
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
                    Shared.currentScore += enemy.ScoreValue;
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
                else
                {
                    player1.State = Player.CharacterState.Walking;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}