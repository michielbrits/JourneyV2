using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyIntoNyx
{
    class Player
    {
        public Texture2D texture;
        public AnimationPlayer animationPlayer;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle playerRect;
        private bool hasJumped = false;

        Animation walkAnimation;
        Animation idleAnimation;
        
        /*
        public int Heigth { return PlayerAnimation.FrameWidth; }
        public int Heigth { return PlayerAnimation.FrameHeigth; }
        */

        public Vector2 Position
        {
            get { return position; }
        }

        public void Load(ContentManager Content)
        {
            
            walkAnimation = new Animation(Content.Load<Texture2D>(@"spriteRight"), 23, 0.1f, true);
            idleAnimation = new Animation(Content.Load<Texture2D>(@"spriteStraight"), 23, 0.1f, true);
            animationPlayer.PlayAnimation(walkAnimation);
            animationPlayer.PlayAnimation(idleAnimation);
            //Add new animations here
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;
            playerRect = new Rectangle((int)position.X, (int)position.Y, walkAnimation.FrameWidth, walkAnimation.FrameHeight - 2);

            Input(gameTime);

            if (velocity.Y < 10)
                velocity.Y += 0.4f;
        }

        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 3;
            else velocity.X = 0f;

            if(Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
            }

            if (velocity.X != 0)
                animationPlayer.PlayAnimation(walkAnimation);
            else if (velocity.X == 0)
                animationPlayer.PlayAnimation(idleAnimation);
                
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (playerRect.TouchTopOf(newRectangle)){
                playerRect.Y = newRectangle.Y - playerRect.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (playerRect.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - playerRect.Width - 2;
            }

            if (playerRect.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }

            if (playerRect.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - playerRect.Width) position.X = xOffset - playerRect.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - playerRect.Height) position.Y = yOffset - playerRect.Height;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            if (velocity.X >= 0)
                flip = SpriteEffects.None;
            else if (velocity.X <= 0)
                flip = SpriteEffects.FlipHorizontally;
            
            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
        }
    }
}
