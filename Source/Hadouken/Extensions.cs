using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hadouken
{
	public static class Extensions
	{
		public static string ReplaceLastOccurance(this string str, string Find, string Replace)
		{
			int Place = str.LastIndexOf(Find);
			string result = str.Remove(Place, Find.Length).Insert(Place, Replace);
			return result;
		}
	}
}
