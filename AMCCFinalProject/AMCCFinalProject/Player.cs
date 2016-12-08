/* Player.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.01: Created & Coded
 *      Cynthia Cheng: 2016.12.04: Coded
 *      Cynthia Cheng: 2016.12.06: Coded
 *      Cynthia Cheng: 2016.12.07: Coded
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
    public class Player : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> currentFrames, idleFrames, eastWalkingFrames, eastJumpFrames, eastUppercutFrames,
           westWalkingFrames, westJumpFrames, westUppercutFrames, walkingFrames, jumpFrames, uppercutFrames, deathFrames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        private const int DEFAULT_SPEED = 5;
        private const int ATTACK_SPEED = 2;
        private int speed = 3;
        private int health;
        private int score;
        private int attackStrength;

        public enum CharacterState
        {
            Idle,
            Walking,
            Jump,
            Uppercut,
            Death,
        }

        private CharacterState state = CharacterState.Walking;

        public CharacterState State
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
            Idle,
            North,
            NorthEast,
            East,
            SouthEast,
            South,
            SouthWest,
            West,
            NorthWest,
        }

        private Direction movement = Direction.Idle;

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

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
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

        public Player(Game game,
            SpriteBatch spriteBatch,
            Texture2D texture,
            Vector2 position,
            int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.delay = delay;

            score = 0;
            health = 500;
            dimension = new Vector2(64, 64);
            attackStrength = 5;

            this.Enabled = true;
            this.Visible = true;

            createFrames();
        }

        private void createFrames()
        {
            idleFrames = new List<Rectangle>();
            for (int i = 0; i < 4; i++)
            {
                int x = i * (int)dimension.X;
                int y = 0;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                idleFrames.Add(r);
            }

            eastWalkingFrames = new List<Rectangle>();
            for (int i = 0; i < 8; i++)
            {
                int x = i * (int)dimension.X;
                int y = 1 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                eastWalkingFrames.Add(r);
            }

            westWalkingFrames = new List<Rectangle>();
            for (int i = 1; i < 9; i++)
            {
                int x = texture.Width - (i * (int)dimension.X);
                int y = 17 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                westWalkingFrames.Add(r);
            }

            eastJumpFrames = new List<Rectangle>();
            for (int i = 0; i < 8; i++)
            {
                int x = i * (int)dimension.X;
                int y = 2 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                eastJumpFrames.Add(r);
            }

            westJumpFrames = new List<Rectangle>();
            for (int i = 1; i < 9; i++)
            {
                int x = texture.Width - (i * (int)dimension.X);
                int y = 18 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                westJumpFrames.Add(r);
            }

            eastUppercutFrames = new List<Rectangle>();
            for (int i = 0; i < 6; i++)
            {
                int x = i * (int)dimension.X;
                int y = 15 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                eastUppercutFrames.Add(r);
            }

            westUppercutFrames = new List<Rectangle>();
            for (int i = 1; i < 7; i++)
            {
                int x = texture.Width - (i * (int)dimension.X);
                int y = 31 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                westUppercutFrames.Add(r);
            }

            deathFrames = new List<Rectangle>();
            for (int i = 0; i < 7; i++)
            {
                int x = i * (int)dimension.X;
                int y = 4 * (int)dimension.Y;
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
                playerFrameUpdate();
                playerMovementUpdate();
                delayCounter = 0;
            }

            base.Update(gameTime);
        }

        public void playerFrameUpdate()
        {
            if (state == CharacterState.Idle)
            {
                if (frameIndex > 3)
                {
                    frameIndex = 0;
                }
                speed = DEFAULT_SPEED;
                currentFrames = idleFrames;
            }
            else if (state == CharacterState.Walking)
            {
                if (frameIndex > 7)
                {
                    frameIndex = 0;
                }
                speed = DEFAULT_SPEED;
                if (movement != Direction.West && movement != Direction.NorthWest && movement != Direction.SouthWest)
                {
                    walkingFrames = eastWalkingFrames;
                }
                else
                {
                    walkingFrames = westWalkingFrames;
                }
                currentFrames = walkingFrames;

            }
            else if (state == CharacterState.Jump)
            {
                if (frameIndex > 7)
                {
                    frameIndex = 0;
                }
                if (movement != Direction.West && movement != Direction.NorthWest && movement != Direction.SouthWest)
                {
                    jumpFrames = eastJumpFrames;
                }
                else
                {
                    jumpFrames = westJumpFrames;
                }
                currentFrames = jumpFrames;
            }
            else if (state == CharacterState.Uppercut)
            {
                if (frameIndex > 5)
                {
                    frameIndex = 0;
                }
                speed = ATTACK_SPEED;
                if (movement != Direction.West && movement != Direction.NorthWest && movement != Direction.SouthWest)
                {
                    uppercutFrames = eastUppercutFrames;
                }
                else
                {
                    uppercutFrames = westUppercutFrames;
                }
                currentFrames = uppercutFrames;
            }
            else if (state == CharacterState.Death)
            {
                speed = DEFAULT_SPEED;
                if (currentFrames != deathFrames)
                {
                    frameIndex = 0;
                    currentFrames = deathFrames;
                }
            }

            if (state == CharacterState.Death && frameIndex == currentFrames.Count - 1)
            {
                frameIndex = currentFrames.Count - 1;
                Enabled = false;
            }
        }

        public void playerMovementUpdate()
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

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, 64, 64);
        }
    }
}
