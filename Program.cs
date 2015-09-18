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
				if (IsVimInstanceExist(gvimbin))
				{
					string opts = "--remote-send <Esc>:tabnew<CR>";
					Debug.WriteLine($"gVimClient: {gvimbin} {opts}");
					Process.Start(gvimbin, opts);
				}
				else
				{
					Debug.WriteLine($"gVimClient: {gvimbin}");
					Process.Start(gvimbin);
				}

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

		static bool IsVimInstanceExist(string gvimbin)
		{
			string vimbin = gvimbin.ReplaceLastOccurrence("gvim.exe", "vim.exe");

			Process p = new Process();
			p.StartInfo.FileName = vimbin;
			p.StartInfo.Arguments = "--serverlist";
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.CreateNoWindow = true;
			p.Start();

			string stdout = p.StandardOutput.ReadToEnd();
			p.WaitForExit();

			if (string.IsNullOrWhiteSpace(stdout))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
    }
}
