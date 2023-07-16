using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeArcade1.GameStuff
{
    struct AnimationLogic
    {

        Animation animation;
        int frameIndex;
        private float time;
        private int variableHeight;
        private int variableWidth;
        private float draw_priority;

        public Animation getCurrentAnimation()
        {
            return animation;
        }
        
        public void animationPlay(Animation the_current_Animation)
        {
            if (animation == the_current_Animation)
            {
                return; 
            }

            this.animation = the_current_Animation;
            frameIndex = 0;
            time = 0;

        }

        public void setDrawPriority(float value)
        {
            draw_priority = value;
        }

        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (getCurrentAnimation() == null)
            {
                throw new NotSupportedException("no animation present"); // if no animationDX is allocated, throw an error!
            }

            variableHeight = getCurrentAnimation().getTexture().Height;
            variableWidth = getCurrentAnimation().getSize_offset();

            time += (float)gameTime.ElapsedGameTime.TotalSeconds; // time increments!
            while (time > getCurrentAnimation().getFrametime()) // if time is larger than the animation's frame time
            {
                time -= getCurrentAnimation().getFrametime(); // time get subtracted by the animation's frame time

                if(getCurrentAnimation().getLoopInfo()) // this uses modular to simulate a reoccuring animation loop 
                {
                    frameIndex = (frameIndex + 1) % (getCurrentAnimation().getFrameCount());
                }
                else
                {
                    frameIndex = Math.Min(frameIndex + 1, getCurrentAnimation().getFrameCount() - 1); // pauses the animation on the last frame!
                }
            }

            Rectangle source = new(frameIndex * variableWidth, 0, variableWidth, variableHeight);
            spriteBatch.Draw(getCurrentAnimation().getTexture(), position, source, Color.White, 0.0f, new Vector2(variableWidth / 2.0f, getCurrentAnimation().getTexture().Bounds.Height / 2.0f), 1.0f, spriteEffects, draw_priority); //position??
        }
    }
}