using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hadouken.IO
{
	public interface IOutputController
	{

		void WriteLine(string p, string[] replacements);

		void WriteLine(string p, string folder);
	}
	public class OutputController : IOutputController
	{
		public void WriteLine(string p, string[] replacements)
		{
			Console.WriteLine(p, replacements);
		}

		public void WriteLine(string p, string replacement)
		{
			WriteLine(p, new string[]{replacement});
		}
	}
}
