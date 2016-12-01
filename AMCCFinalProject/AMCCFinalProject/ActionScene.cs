/* ActionScene.cs
 * Final Project
 * Revision History
 *      Cynthia Cheng: 2016.12.1: Created & Coded
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
        private CollisionManager collisionManager; //Base Template only
        KeyboardState oldState; //Not currently used

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            Texture2D texture = game.Content.Load<Texture2D>("images/player1");
            player1 = new Player(game, spriteBatch, texture, initialPosition, initialDelay);
            this.Components.Add(player1);

            collisionManager = new CollisionManager(game, player1);
            this.Components.Add(collisionManager);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            KeyboardState keyboardState = Keyboard.GetState();

            playerDirection(keyboardState);
            playerAction(keyboardState);

            oldState = keyboardState;
            base.Update(gameTime);
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