using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    class FrameCounter
    {
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }

        public int MaxFrame = 100;

        private Queue<float> SampleBuffer = new Queue<float>();

        public bool Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            SampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (SampleBuffer.Count > MaxFrame)
            {
                SampleBuffer.Dequeue();
                AverageFramesPerSecond = SampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
            return true;
        }
    }
}
