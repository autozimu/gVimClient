using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

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
                string opts = "--remote-send <Esc>:tabnew<CR>";
                Debug.WriteLine($"gVimClient: {gvimbin} {opts}");
                Process.Start(gvimbin, opts);
            }
            else
            {
                string opts = "--remote-tab-silent";
                foreach (string arg in args)
                {
                    opts += $" \"{arg}\"";
                }
                Debug.WriteLine($"gVimClient: {gvimbin} {opts}");
                Process.Start(gvimbin, opts);
            }
        }

        static string LocategVimBin()
        {
            string[] locations = new string[]
            {
                @"C:\Program Files\Vim\gvim.exe",
                @"C:\Program Files (x86)\Vim\gvim.exe",
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
