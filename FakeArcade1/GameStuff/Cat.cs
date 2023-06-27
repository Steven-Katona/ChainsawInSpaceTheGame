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
    internal class Cat : Enemy
    {
        double time_to_shoot = 3;
        double last_shot;
        public Cat(Texture2D thisTexture, int x, int y, int realWidth, int realHeight, int count, int trigger, float frame_speed, Vector2 terminal, int maxWidth, int maxHeight) : base(thisTexture, x, y, realWidth, realHeight, count, trigger, frame_speed, terminal, maxWidth, maxHeight)
        {
            last_shot = 3;
            moving = (-1, 0);
            setSpeed(150);
            pre_update_logic_options = 1;
        }




        public void setShootTime(double newtime)
        {
            time_to_shoot = newtime;
        }

        public override void trigger_behavior()
        {
            speed = 0;
        }

        public override void zero_trigger_behavior()
        {
            setDead();
        }

        public override void preUpdateLogic(GameTime gameTime, Player player)
        {
            if (getSpeed() == 0)
            {

                int testX = (int)getPosition().X;
                int testY = (int)getPosition().X;



                if (time_to_shoot < 0)
                {
                    will_Shoot= true;
                    time_to_shoot = last_shot - 1;
                    last_shot = time_to_shoot;
                }
                else
                {
                    setShootTime(time_to_shoot - gameTime.ElapsedGameTime.TotalSeconds);
                }

                if (last_shot < 0)
                {
                    time_to_shoot = 3;
                    last_shot= 3;
                }

                
            }

        }

        public override void preUpdateLogic(GameTime gameTime, ContentManager content, Player player)
        {
            
        }
    }
}
