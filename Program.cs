using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace gVimClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string gvimbin = LocategVimBin();

            if (gvimbin == null)
            {
                Debug.WriteLine("gVimClient: no vim binary found.");
                return;
            }

            if (args.Length == 0)
            {
                Debug.WriteLine($"gVimClient: {gvimbin}");
                Process.Start(gvimbin);
            }
            else
            {
                // Insert --remote-tab-silent argument.
                List<string> argsList = new List<string>(args);
                int modifierIdx = 0;
                if (argsList.Exists(a => a.StartsWith("-")))
                {
                    modifierIdx = argsList.FindLastIndex(a => a.StartsWith("-"));
                }
                argsList.Insert(modifierIdx, "--remote-tab-silent");

                StringBuilder optsBuilder = new StringBuilder();
                foreach (string arg in args)
                {
                    optsBuilder.Append(arg).Append(" ");
                }

                string opts = optsBuilder.ToString();
                Debug.WriteLine($"gVimClient: {gvimbin} {opts}");

                Process proc = Process.Start(gvimbin, opts);
                if (argsList.Contains("-f"))
                {
                    proc.WaitForExit();
                }
            }
        }

        static string LocategVimBin()
        {
            string[] locations = new string[]
            {
                @"C:\Program Files\Vim\vim74\gvim.exe",
            };

            foreach (string loc in locations)
            {
                if (File.Exists(loc))
                {
                    return loc;
                }
            }

            return null;
        }
    }
}
