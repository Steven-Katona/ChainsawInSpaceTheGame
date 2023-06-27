using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.CompilerServices;

namespace FakeArcade1.GameStuff
{
    internal class Projectile : Enemy
    {
        public bool outOfBounds { get;  set; }
        public (float, float) movementDirection { get; set; }

 
        //Rectangle bounds;
        public Projectile(Texture2D thisTexture, int x, int y, int realWidth, int realHeight, int trigger, int count, float frame_speed, Vector2 terminal, int maxWidth, int maxHeight) : base(thisTexture, x, y, realWidth, realHeight, trigger,  count, frame_speed, terminal, maxWidth, maxHeight)
        {
            movementDirection = calc.newDirection(x, y, terminal);
            trigger_behavior_active = false;
            zero_behavior_active = false;
            speed = 600;
            pre_update_logic_options = 0;
            maxW = maxWidth; maxH = maxHeight;
            outOfBounds = false;

        }

        public bool checkOutOfBounds()
        {

            if(this.getPosition().X > this.maxW || this.getPosition().X < 0 || this.getPosition().Y < 0 || this.getPosition().Y > this.maxH)
            {
                outOfBounds= true;
            }

            return outOfBounds;
                
        }

        public override void preUpdateLogic(GameTime gameTime, Player player){}
        public override void preUpdateLogic(GameTime gameTime, ContentManager content, Player player){}

        public override void trigger_behavior()
        {

        }

        public override void zero_trigger_behavior()
        {

        }

        public override void Update(GameTime gameTime)
        {
            
            move_Position((movementDirection.Item1 * speed * (float)gameTime.ElapsedGameTime.TotalSeconds, movementDirection.Item2 * speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
            
        }

    }
}
