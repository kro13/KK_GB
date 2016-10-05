using System;

namespace Assets.Scripts.Util
{
    public class MenuEventArgs:EventArgs
    {
        private string[] args;
        public MenuEventArgs(params string[] args)
        {
            this.args = args;
        }

        public string[] GetArgs()
        {
            return args;
        }

    }
}