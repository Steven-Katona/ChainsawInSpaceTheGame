using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade1.GameStuff
{
    internal class Ship : Enemy
    {
        public Ship(Texture2D thisTexture, int x, int y, int realWidth, int realHeight, int trigger, int count, float frame_speed, Vector2 terminal, int maxWidth, int maxHeight) : base(thisTexture, x, y, realWidth, realHeight,  trigger, count, frame_speed, terminal, maxWidth, maxHeight)
        {
            variable_Terminal = terminal;
            zero_behavior_active = false;
            pre_update_logic_options = 1;
            speed = 500;
            moving = (-1, 0);

        }

      
        public override void preUpdateLogic(GameTime gameTime, Player player)
        {

            if (getSpeed() == 0 || getPosition().Y < 0 || getPosition().Y > maxH)
            {
                
                int newPos = rand.Next(10, 90);
                setPosition(maxW, (int)Math.Floor(maxH * newPos * 0.01d));
                resetBehavior();
                setSpeed(500);
                moving = (-1, 0);
                setTerminal(player.getPosition());
            }

            if (getPosition().X > maxW && !Trigger_Status())
            {
                resetMovement();
            }
        }

        public override void preUpdateLogic(GameTime gameTime, ContentManager content, Player player){}


        public override void trigger_behavior()
        {
            moving = calc.newDirection((int)getPosition().X, (int)getPosition().Y, variable_Terminal);
            resetZeroBehavior();

        }

        public override void zero_trigger_behavior()
        {
            speed = 0;
        }

        public void resetMovement()
        {
            moving = (-1, 0);
        }
    }
}
