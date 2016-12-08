/* Enemy.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng:      2016.12.2: Created & Coded
 *      Cynthia Cheng:      2016.12.04: Coded
 *      Cynthia Cheng:      2016.12.06: Coded
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

namespace AMCCFinalProject
{
    public class Enemy : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 dimension;
        private int enemyVersion;
        private List<Rectangle> currentFrames, moveFrames, attackFrames, deathFrames, eastMoveFrames, westMoveFrames, eastAttackFrames, westAttackFrames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private int speed;
        private bool active;
        private int health;
        private int attackStrength;
        private int scoreValue;
        private bool scoreAdded = false;

        public enum EnemyState
        {
            Move,
            Attack,
            Death,
        }

        private EnemyState state = EnemyState.Move;

        public EnemyState State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public int FrameIndex
        {
            get
            {
                return frameIndex;
            }

            set
            {
                frameIndex = value;
            }
        }

        public enum Direction
        {
            North,
            NorthEast,
            East,
            SouthEast,
            South,
            SouthWest,
            West,
            NorthWest,
            Stop,
        }

        private Direction movement = Direction.West;

        public Direction Movement
        {
            get
            {
                return movement;
            }

            set
            {
                movement = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public List<Rectangle> CurrentFrames
        {
            get
            {
                return currentFrames;
            }

            set
            {
                currentFrames = value;
            }
        }

        public int AttackStrength
        {
            get
            {
                return attackStrength;
            }

            set
            {
                attackStrength = value;
            }
        }

        public int ScoreValue
        {
            get
            {
                return scoreValue;
            }

            set
            {
                scoreValue = value;
            }
        }

        public bool ScoreAdded
        {
            get
            {
                return scoreAdded;
            }

            set
            {
                scoreAdded = value;
            }
        }

        public int EnemyVersion
        {
            get
            {
                return enemyVersion;
            }

            set
            {
                enemyVersion = value;
            }
        }

        public Enemy(Game game,
            SpriteBatch spriteBatch,
            Texture2D texture,
            Vector2 position,
            int delay, int enemyVersion) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.delay = delay;
            this.enemyVersion = enemyVersion;

            dimension = new Vector2(64, 64);
            active = true;
            health = 50;

            this.Enabled = true;
            this.Visible = true;

            createFrames();
        }

        private void createFrames()
        {
            if (enemyVersion == 1)
            {
                speed = 2;
                attackStrength = 10;
                scoreValue = 100;
                eastMoveFrames = new List<Rectangle>();
                for (int i = 0; i < 5; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    eastMoveFrames.Add(r);
                }
                westMoveFrames = new List<Rectangle>();
                for (int i = 1; i < 6; i++)
                {
                    int x = texture.Width - (i * (int)dimension.X);
                    int y = 8 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    westMoveFrames.Add(r);
                }
                eastAttackFrames = new List<Rectangle>();
                for (int i = 0; i < 6; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = 2 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    eastAttackFrames.Add(r);
                }
                westAttackFrames = new List<Rectangle>();
                for (int i = 1; i < 7; i++)
                {
                    int x = texture.Width - (i * (int)dimension.X);
                    int y = 9 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    westAttackFrames.Add(r);
                }
                deathFrames = new List<Rectangle>();
                for (int i = 0; i < 7; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = 3 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    deathFrames.Add(r);
                }
            }
            else if (enemyVersion == 2)
            {
                speed = 1;
                attackStrength = 15;
                scoreValue = 150;
                eastMoveFrames = new List<Rectangle>();
                for (int i = 0; i < 4; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    eastMoveFrames.Add(r);
                }
                westMoveFrames = new List<Rectangle>();
                for (int i = 1; i < 5; i++)
                {
                    int x = texture.Width - (i * (int)dimension.X);
                    int y = 8 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    westMoveFrames.Add(r);
                }
                eastAttackFrames = new List<Rectangle>();
                for (int i = 0; i < 4; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = 2 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    eastAttackFrames.Add(r);
                }
                westAttackFrames = new List<Rectangle>();
                for (int i = 1; i < 5; i++)
                {
                    int x = texture.Width - (i * (int)dimension.X);
                    int y = 9 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    westAttackFrames.Add(r);
                }
                deathFrames = new List<Rectangle>();
                for (int i = 0; i < 7; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = 3 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    deathFrames.Add(r);
                }
            }
            else if (enemyVersion == 3)
            {
                speed = 3;
                attackStrength = 5;
                scoreValue = 200;
                eastMoveFrames = new List<Rectangle>();
                for (int i = 0; i < 5; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = 0;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    eastMoveFrames.Add(r);
                }
                westMoveFrames = new List<Rectangle>();
                for (int i = 1; i < 6; i++)
                {
                    int x = texture.Width - (i * (int)dimension.X);
                    int y = 4 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    westMoveFrames.Add(r);
                }
                eastAttackFrames = new List<Rectangle>();
                for (int i = 0; i < 5; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    eastAttackFrames.Add(r);
                }
                westAttackFrames = new List<Rectangle>();
                for (int i = 1; i < 6; i++)
                {
                    int x = texture.Width - (i * (int)dimension.X);
                    int y = 5 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    westAttackFrames.Add(r);
                }
                deathFrames = new List<Rectangle>();
                for (int i = 0; i < 8; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = 2 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    deathFrames.Add(r);
                }
            }

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                enemyFrameUpdate();
                enemyMovementUpdate();
                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        public void enemyFrameUpdate()
        {
            if (enemyVersion == 1)
            {
                if (state == EnemyState.Move)
                {
                    if (frameIndex > 4)
                    {
                        frameIndex = 0;
                    }
                    if (movement == Direction.East || movement == Direction.NorthEast || movement == Direction.SouthEast)
                    {
                        moveFrames = eastMoveFrames;
                    }
                    else
                    {
                        moveFrames = westMoveFrames;
                    }
                    currentFrames = moveFrames;
                }
                else if (state == EnemyState.Attack)
                {
                    if (frameIndex > 5)
                    {
                        frameIndex = 0;
                    }

                    if (movement == Direction.East || movement == Direction.NorthEast || movement == Direction.SouthEast)
                    {
                        attackFrames = eastAttackFrames;
                    }
                    else
                    {
                        attackFrames = westAttackFrames;
                    }

                    currentFrames = attackFrames;
                }
                else if (state == EnemyState.Death)
                {
                    if (frameIndex > 6)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = deathFrames;
                }
            }
            else if (enemyVersion == 2)
            {
                if (state == EnemyState.Move)
                {
                    if (frameIndex > 3)
                    {
                        frameIndex = 0;
                    }
                    if (movement == Direction.East || movement == Direction.NorthEast || movement == Direction.SouthEast)
                    {
                        moveFrames = eastMoveFrames;
                    }
                    else
                    {
                        moveFrames = westMoveFrames;
                    }
                    currentFrames = moveFrames;
                }
                else if (state == EnemyState.Attack)
                {
                    if (frameIndex > 3)
                    {
                        frameIndex = 0;
                    }

                    if (movement == Direction.East || movement == Direction.NorthEast || movement == Direction.SouthEast)
                    {
                        attackFrames = eastAttackFrames;
                    }
                    else
                    {
                        attackFrames = westAttackFrames;
                    }

                    currentFrames = attackFrames;
                }
                else if (state == EnemyState.Death)
                {
                    if (frameIndex > 6)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = deathFrames;
                }
            }
            else if (enemyVersion == 3)
            {
                if (state == EnemyState.Move)
                {
                    if (frameIndex > 4)
                    {
                        frameIndex = 0;
                    }

                    if (movement == Direction.East || movement == Direction.NorthEast || movement == Direction.SouthEast)
                    {
                        moveFrames = eastMoveFrames;
                    }
                    else
                    {
                        moveFrames = westMoveFrames;
                    }

                    currentFrames = moveFrames;
                }
                else if (state == EnemyState.Attack)
                {
                    if (frameIndex > 4)
                    {
                        frameIndex = 0;
                    }

                    if (movement == Direction.East || movement == Direction.NorthEast || movement == Direction.SouthEast)
                    {
                        attackFrames = eastAttackFrames;
                    }
                    else
                    {
                        attackFrames = westAttackFrames;
                    }

                    currentFrames = attackFrames;
                }
                else if (state == EnemyState.Death)
                {
                    if (frameIndex > 7)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = deathFrames;
                }
            }
        }

        public void enemyMovementUpdate()
        {
            if (movement == Direction.East)
            {
                position.X += speed;
            }
            else if (movement == Direction.West)
            {
                position.X -= speed;
            }
            else if (movement == Direction.North)
            {
                position.Y -= speed;
            }
            else if (movement == Direction.South)
            {
                position.Y += speed;
            }
            else if (movement == Direction.NorthEast)
            {
                position.X += speed;
                position.Y -= speed;
            }
            else if (movement == Direction.NorthWest)
            {
                position.X -= speed;
                position.Y -= speed;
            }
            else if (movement == Direction.SouthEast)
            {
                position.X += speed;
                position.Y += speed;
            }
            else if (movement == Direction.SouthWest)
            {
                position.X -= speed;
                position.Y += speed;
            }
            else if (movement == Direction.Stop)
            {
                speed = 0;
                position.X += speed;
                position.Y += speed;
            }

            if (state == EnemyState.Death && FrameIndex == currentFrames.Count - 1)
            {
                Enabled = false;
                Active = false;
                Visible = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                spriteBatch.Draw(texture, position, currentFrames[frameIndex], Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y);
        }
    }
}