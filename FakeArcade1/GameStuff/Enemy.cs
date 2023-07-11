using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Content;

namespace FakeArcade1.GameStuff
{
    internal abstract class Enemy: Sprite
    {
        //int health_stat = 1;
        protected int speed;
        protected (float, float) moving;

        protected int width_trigger;
        protected bool trigger_behavior_active = true;
        protected bool zero_behavior_active = true;
        protected bool will_Shoot = false;
        protected Vector2 variable_Terminal;
        protected static PublicCalculations calc;
        protected static Random rand;
        protected int maxW;
        protected int maxH;
        public int pre_update_logic_options;
        protected Texture2D deathTexture;



        public Enemy(Texture2D thisTexture, Rectangle bounds) : base(thisTexture, bounds)
        {

            //stats.SetPane(new int[6]);
            //LoadMyContent();
        }

        public Enemy(Texture2D thisTexture, int x, int y, int realWidth, int realHeight, int trigger, int count, float frame_speed, Vector2 terminal, int maxWidth, int maxHeight) : base(thisTexture, x, y, realWidth, realHeight, frame_speed, count)
        {
      
            variable_Terminal= terminal;
            calc = new PublicCalculations();
            rand = new Random();
            width_trigger = trigger;
            maxW= maxWidth;
            maxH= maxHeight;
        }

        public void setTerminal(Vector2 newTerminal)
        {

            variable_Terminal = newTerminal; //used specifically with projectiles.

        }

        public void setDeathTexture(Texture2D thisistheend)
        {
            deathTexture = thisistheend;
        }

        public Texture2D getDeadTexture()
        {
            return deathTexture;
        }



        public bool Trigger_Status()
        {
            return trigger_behavior_active;
        }

        public void resetShoot()
        {
            will_Shoot = false;
        }

        public bool willEnemyShoot()
        {
            return will_Shoot;
        }



        abstract public void trigger_behavior(); //denotes behavior that a certain enemy will take once it has reached a certain location along the x axis
        abstract public void zero_trigger_behavior(); //denotes behavior that a certain enemy will take once it exited the screen on the left side.

        public abstract void preUpdateLogic(GameTime gameTime, Player player);

        public abstract void preUpdateLogic(GameTime gameTime, ContentManager content, Player player);

        public void resetZeroBehavior()
        {
            zero_behavior_active = true;
        }

        public void resetBehavior()
        {
            trigger_behavior_active = true;
        }

        


        public override void Update(GameTime gameTime)
        {
            if (!Dead())
            {
                if (Math.Abs(getPosition().X - width_trigger) < 8 && trigger_behavior_active == true)
                {
                    trigger_behavior_active = false;
                    trigger_behavior();
                }

                if (getPosition().X <= 0 - (getWidth()) && zero_behavior_active == true)
                {
                    zero_behavior_active = false;
                    zero_trigger_behavior();
                }

                (float, float) moving2 = ((float)gameTime.ElapsedGameTime.TotalSeconds * moving.Item1 * speed, (float)gameTime.ElapsedGameTime.TotalSeconds * moving.Item2 * speed);
                move_Position(moving2);
            }
        }

        public void setSpeed(int newSpeed)
        {
            speed = newSpeed;
        }

        public int getSpeed() { return speed; }
    }
}
