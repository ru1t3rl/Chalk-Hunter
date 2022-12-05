using System;

namespace Ru1t3rl.Events.Args
{
    public class ViewArguments : EventArgs
    {
        public readonly bool isPerspective;

        public ViewArguments(bool isPerspective)
        {
            this.isPerspective = isPerspective;
        }
    }
}