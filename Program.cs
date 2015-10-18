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
                Debug.WriteLine($"gVimClient: {gvimbin}");
                Process.Start(gvimbin);
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
