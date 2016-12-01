/* Player.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.1: Created & Coded
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
        private List<Rectangle> currentFrames, idleFrames, walkingFrames, jumpFrames, uppercutFrames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private int speed = 3;

        public enum CharacterState
        {
            Idle,
            Walking,
            Jump,
            Uppercut,
        }

        private CharacterState state = CharacterState.Idle;

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

            dimension = new Vector2(64, 64);

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

            walkingFrames = new List<Rectangle>();
            for (int i = 0; i < 8; i++)
            {
                int x = i * (int)dimension.X;
                int y = 1 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                walkingFrames.Add(r);
            }

            jumpFrames = new List<Rectangle>();
            for (int i = 0; i < 8; i++)
            {
                int x = i * (int)dimension.X;
                int y = 2 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                jumpFrames.Add(r);
            }

            uppercutFrames = new List<Rectangle>();
            for (int i = 0; i < 6; i++)
            {
                int x = i * (int)dimension.X;
                int y = 15 * (int)dimension.Y;
                Rectangle r = new Rectangle(x, y, (int)dimension.X,
                    (int)dimension.Y);
                uppercutFrames.Add(r);
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
                currentFrames = idleFrames;
            }
            else if (state == CharacterState.Walking)
            {
                if (frameIndex > 7)
                {
                    frameIndex = 0;
                }
                currentFrames = walkingFrames;

            }
            else if (state == CharacterState.Jump)
            {
                if (frameIndex > 7)
                {
                    frameIndex = 0;
                }
                currentFrames = jumpFrames;
            }
            else if (state == CharacterState.Uppercut)
            {
                if (frameIndex > 5)
                {
                    frameIndex = 0;
                }
                currentFrames = uppercutFrames;
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
    }
}
