using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hadouken
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("======================================================");
			Console.WriteLine("= Hadouken - Your Friendly Solution Preparation Tool =",Environment.NewLine);
			Console.WriteLine("======================================================{0}", Environment.NewLine);
			Console.WriteLine("help: doug@diaryofaninja.com{0}",Environment.NewLine);
			Console.WriteLine("This tool will:");
			Console.WriteLine("- rename all the files and folders that contain our \"Magic Word\"");
			Console.WriteLine("- find and replace the content of all text files that contain our \"Magic Word\"{0}", Environment.NewLine);

			Initialise(args);

			Console.WriteLine("String to be replaced: \"{0}\"{1}{1}", Config.MAGIC_WORD, Environment.NewLine);

			Console.WriteLine("Please enter the new solution name:");

			string newSolutionName = Console.ReadLine();

			Console.WriteLine("{0}{0}Starting magic stuff...", Environment.NewLine);

			string startingFilePath = Environment.CurrentDirectory;
			try
			{
				new RenameHelper(startingFilePath, newSolutionName).DoCoolStuff();
				new FindReplaceHelper(startingFilePath, newSolutionName).DoCoolStuff();
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Console.WriteLine("{0}{0}Press any key to continue...", Environment.NewLine);
			Console.ReadKey();
		}

		private static void Initialise(string[] args)
		{
			if (ArgExist(args, "?"))
			{
				OutputArgumentHelp("Help:");
				Environment.Exit(0);
			}
			if (ArgExist(args, "r"))
			{
				Config.MAGIC_WORD = ArgString(args, "r", Config.MAGIC_WORD);
			}
		}

		private static void OutputArgumentHelp(string message)
		{
			if (!String.IsNullOrEmpty(message))
			{
				Console.Error.WriteLine(message);
			}

			Console.Error.WriteLine("/r {string}            String to replace. By default this is set to \"MySolution\"");
		}

		static string[] ArgArray(string[] args, string option)
		{
			int start = 0, length = 0;
			if (args.Length > 0)
				for (int i = 0; i < args.Length; i++)
				{
					if (args[i].StartsWith("-") && args[i].Length > 1 && args[i].Substring(1) == option)
					{
						start = i + 1;
						for (int j = 1; i + j < args.Length; j++)
						{
							if (args[i + j].StartsWith("-")) break;
							length = j;
						}
						break;
					}
				}

			string[] result = new string[length];
			if (length > 0)
				Array.Copy(args, start, result, 0, length);

			return result;
		}

		static string ArgString(string[] args, string option, string defaultValue)
		{
			string[] result = ArgArray(args, option);
			if (result.Length > 0)
				return result[0];
			else
				return defaultValue;
		}

		static bool ArgExist(string[] args, string option)
		{
			for (int i = 0; i < args.Length; i++)
				if (args[i].StartsWith("/") && args[i].Length > 1 && args[i].Substring(1).Contains(option))
					return true;
			return false;
		}
	}
}
