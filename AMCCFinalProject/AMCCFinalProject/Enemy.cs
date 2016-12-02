/* Enemy.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.2: Created & Coded
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
        private List<Rectangle> currentFrames, moveFrames, attackFrames, deathFrames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private int speed = 2;
        private Boolean active;
        private int health;

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

        public Enemy(Game game,
            SpriteBatch spriteBatch,
            Texture2D texture,
            Vector2 position,
            int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.delay = delay;

            dimension = new Vector2(64, 64);
            active = true;
            health = 20;

            this.Enabled = true;
            this.Visible = true;

            createFrames();
        }

        private void createFrames()
        {
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
            if (state == EnemyState.Move)
            {
                if (frameIndex > 4)
                {
                    frameIndex = 0;
                }
                currentFrames = moveFrames;
            }
            else if (state == EnemyState.Attack)
            {
                if (frameIndex > 5)
                {
                    frameIndex = 0;
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
    }
}