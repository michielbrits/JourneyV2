using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyIntoNyx
{
    class Animation
    {
        Texture2D spriteSheet;
        public Vector2 Position;
        float scale;
        int elapsedTime, frameCount, currentFrame;
        public int FrameWidth, FrameHeigth;
        public bool Active, Looping;
        Color color;
        Rectangle sourceRect, destRect = new Rectangle();
        
    }
}
