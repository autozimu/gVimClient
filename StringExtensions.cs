using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gVimClient
{
	public static class StringExtensions
	{
		public static string ReplaceLastOccurrence(this string str, string find, string replace)
		{
			int i = str.IndexOf(find);
			return str.Remove(i, find.Length).Insert(i, replace);
		}
	}
}
