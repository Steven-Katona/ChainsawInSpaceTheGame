using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FakeArcade1.GameStuff
{
    internal abstract class Sprite
    {
        Rectangle bounds;
        Texture2D texture;
        Vector2 origin;
        Vector2 position;
        private Animation myAnimation;
        public AnimationLogic myAnimationPlayer;
        bool dead;
        private int offsetX;
        private int offsetY;
        protected double scl = 1.0;
        

        public Sprite(Texture2D thisTexture, Rectangle mybounds)
        {
            texture = thisTexture;
            bounds= mybounds;
            position = new(bounds.X, bounds.Y);
            
        }

        public Sprite(Texture2D thisTexture, int x, int y, int width, int height, float frame_speed, int count) 
        { 
            texture= thisTexture;
            width = (int)Math.Floor(scl * width);
            height = (int)Math.Floor(scl * height);

            
            origin = (new Vector2(bounds.Width / 2.0f, bounds.Height / 2.0f));
            bounds = new Rectangle(((int)origin.X), ((int)origin.Y), width, height);
            //position = new(origin.X, origin.Y);
            myAnimation = new(thisTexture, frame_speed, true, width, count);
            offsetX = width;
            offsetY = height;
            myAnimationPlayer.animationPlay(myAnimation);
            setPosition(x, y);
        }

        public void setAnimation(Animation animate)
        {
            myAnimation = animate;
        }

        public void autoHitbox()
        {

        }

        public void givePlayerAnimation(Animation animation)
        {
            myAnimationPlayer.animationPlay(animation);
            myAnimation = animation;

            if (myAnimationPlayer.getCurrentAnimation() == null)
            {
                throw new Exception();
            }
        }

        public int getHeight()
        {
            return bounds.Height;
        }

        public int getWidth()
        {
            return offsetX;
        }

        public Vector2 getOrigin()
        {
            return origin;
        }

        public Texture2D getTexture()
        { return texture; }

        public void setTexture(Texture2D newTexture)
        {
            texture = newTexture;
        }

        public void setPosition(int x, int y)
        {
            position = new Vector2(x, y);
            bounds.X = (int)(x - offsetX/2.0f); 
            bounds.Y = (int)(y - offsetY/2.0f);
        }

        public void setRectangle(Rectangle newBounds)
        {
            bounds = newBounds;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setDimensions(int width, int height)
        {
            origin = new Vector2(width / 2.0f, height / 2.0f);
            bounds = new Rectangle((int)position.X, (int)position.Y, width,height);
        }

        public void setDimensions(int x, int y, int width, int height)
        {
            origin = new Vector2(width / 2.0f, height / 2.0f);
            bounds = new Rectangle(x, y, width, height);
        }

        public void move_Position((float,float) movement)
        {
            this.position.X += (int)Math.Ceiling(movement.Item1);
            this.position.Y += (int)Math.Ceiling(movement.Item2);
            this.bounds.X += (int)Math.Ceiling(movement.Item1);
            this.bounds.Y += (int)Math.Ceiling(movement.Item2);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            myAnimationPlayer.Draw(gameTime, spriteBatch, getPosition(), SpriteEffects.None);
        }

        public bool Dead()
        {
            return dead;
        }

        public void setDead()
        {
            dead = true;
        }

        public abstract void Update(GameTime gameTime);
        




    }
}
