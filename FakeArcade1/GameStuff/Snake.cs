using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace FakeArcade1.GameStuff
{
    internal class Snake : Enemy
    {
        bool dormant = false;
        double snake_timer;
        int speedIncrease = 500;

        public Snake(Texture2D thisTexture, int x, int y, int realWidth, int realHeight, int trigger, int count, float frame_speed, Vector2 terminal, int maxWidth, int maxHeight) : base(thisTexture, x, y, realWidth, realHeight, trigger, count, frame_speed, terminal, maxWidth, maxHeight)       
        {
            this.speed = 500;
            this.moving = (-1.0f, 0.0f);
            this.trigger_behavior_active = false;
            pre_update_logic_options = 2;
            width_trigger = (int)Math.Floor(maxWidth + (realWidth / 2.0f)); 
       
        }

        public void setSnakeTimer(double time)
        {
            snake_timer = time;
        }

        public double getSnakeTimer()
        {
            return snake_timer;
        }

        public override void trigger_behavior()
        {
            speed = 0;
            //move_Position((getPosition().X + getWidth(), getPosition().Y));
            dormant = true;
        }

        public override void zero_trigger_behavior()
        {
            speed = 0;
            //move_Position((getPosition().X - getWidth(), getPosition().Y));
            dormant = true;
            
        }

        public bool getDormant()
        {
            return dormant;
        }

        public void setDormant(bool dor)
        {
            dormant = dor;
        }

        public void beginMovingRight()
        {
            speedIncrease += 100;
            speed = speedIncrease;
            moving = (1.0f, 0.0f);
            resetBehavior();
        }

        public void beginMovingLeft()
        {
            speedIncrease += 100;
            speed = speedIncrease;
            moving = (-1.0f, 0.0f);
            resetZeroBehavior();
        }

        public override void preUpdateLogic(GameTime gameTime, Player player)
        { }

        public override void preUpdateLogic(GameTime gameTime, ContentManager content, Player player)
        {

            if (getSpeed() == 0 && getDormant())
            {
                setSnakeTimer(rand.Next(1, 5));
                setDormant(false);
            }

            if (getSnakeTimer() > 0 && getSpeed() == 0)
            {
                setSnakeTimer(((getSnakeTimer() - gameTime.ElapsedGameTime.TotalSeconds)));
            }

            if (getSnakeTimer() <= 0 && getSpeed() == 0)
            {
                setSnakeTimer(0);
                Texture2D dynamicText;
                Animation mov2;

                if (getPosition().X < (int)(.50 * maxW))
                {
                    dynamicText = content.Load<Texture2D>("snake_sheet_right");
                    beginMovingRight();
                }
                else
                {
                    dynamicText = content.Load<Texture2D>("snake_sheet");
                    beginMovingLeft();
                }

                int random_Offset = rand.Next(-64, 64);
                setPosition((int)getPosition().X, (int)player.getPosition().Y + random_Offset);
                mov2 = new(dynamicText, .25f, true, 255, 3);
                givePlayerAnimation(mov2);
                setDormant(false);


            }

        }


    }
}
