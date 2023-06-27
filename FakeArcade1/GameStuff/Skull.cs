using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade1.GameStuff
{
    internal class Skull : Enemy
    {
        int starting_X_Position;
        int start_Y_position;
        public Skull(Texture2D thisTexture, int x, int y, int realWidth, int realHeight, int trigger, int count, float frame_speed,  Vector2 terminal, int maxWidth, int maxHeight) : base(thisTexture, x, y, realWidth, realHeight,  trigger, count, frame_speed, terminal ,maxWidth, maxHeight)
        {
            setSpeed(250);
            this.moving = (-1.0f, 0.0f);
            starting_X_Position = x;
            start_Y_position = y;
            pre_update_logic_options = 0;
        }

        public override void trigger_behavior()
        {
            will_Shoot = true;
        }

       
        

        public override void zero_trigger_behavior()
        {
            resetBehavior();
            setPosition(starting_X_Position, start_Y_position);
            resetZeroBehavior();
            speed += 50;
        }

        public override void preUpdateLogic(GameTime gameTime, Player player){}

        public override void preUpdateLogic(GameTime gameTime, ContentManager content, Player player){}
    }
}
