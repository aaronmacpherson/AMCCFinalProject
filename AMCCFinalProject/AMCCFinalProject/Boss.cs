﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AMCCFinalProject
{
    public class Boss : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 dimension;
        private int bossVersion;
        private List<Rectangle> currentFrames, moveFrames, attackFrames, deathFrames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private int speed;
        private bool active;
        private int health;
        private int attackStrength;
        private int scoreValue;
        private bool scoreAdded = false;

        public enum BossState
        {
            Move,
            Attack,
            Death,
        }

        private BossState state = BossState.Move;

        public BossState State
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

        public Boss(Game game,
            SpriteBatch spriteBatch,
            Texture2D texture,
            Vector2 position,
            int delay, int bossVersion) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.delay = delay;
            this.bossVersion = bossVersion;

            dimension = new Vector2(64, 64);
            active = true;
            health = 200;

            this.Enabled = true;
            this.Visible = true;

            createFrames();
        }

        private void createFrames()
        {
            if (bossVersion == 1)
            {
                speed = 4;
                attackStrength = 20;
                scoreValue = 1000;
                moveFrames = new List<Rectangle>();
                for (int i = 0; i < 5; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    moveFrames.Add(r);
                }
                attackFrames = new List<Rectangle>();
                for (int i = 0; i < 6; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = 2 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    attackFrames.Add(r);
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
            else if (bossVersion == 2)
            {
                speed = 3;
                attackStrength = 25;
                scoreValue = 1500;
                moveFrames = new List<Rectangle>();
                for (int i = 0; i < 4; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    moveFrames.Add(r);
                }
                attackFrames = new List<Rectangle>();
                for (int i = 0; i < 4; i++)
                {
                    int x = i * (int)dimension.X;
                    int y = 2 * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    attackFrames.Add(r);
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
                bossFrameUpdate();
                bossMovementUpdate();
                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        public void bossFrameUpdate()
        {
            if (bossVersion == 1)
            {
                if (state == BossState.Move)
                {
                    if (frameIndex > 4)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = moveFrames;
                }
                else if (state == BossState.Attack)
                {
                    if (frameIndex > 5)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = attackFrames;
                }
                else if (state == BossState.Death)
                {
                    if (frameIndex > 6)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = deathFrames;
                }
            }
            else if (bossVersion == 2)
            {
                if (state == BossState.Move)
                {
                    if (frameIndex > 3)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = moveFrames;
                }
                else if (state == BossState.Attack)
                {
                    if (frameIndex > 3)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = attackFrames;
                }
                else if (state == BossState.Death)
                {
                    if (frameIndex > 6)
                    {
                        frameIndex = 0;
                    }
                    currentFrames = deathFrames;
                }
            }
        }

        public void bossMovementUpdate()
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

            if (state == BossState.Death && FrameIndex == currentFrames.Count - 1)
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