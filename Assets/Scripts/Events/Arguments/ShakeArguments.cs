using System;

namespace Ru1t3rl.Events.Args
{
    public class ShakeArguments : EventArgs
    {
        public readonly float duration;
        public readonly float magnitude;
        public readonly float interval;

        public ShakeArguments(float duration, float magnitude, float interval)
        {
            this.duration = duration;
            this.magnitude = magnitude;
            this.interval = interval;
        }
    }
}