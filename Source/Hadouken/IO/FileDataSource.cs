using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hadouken.IO
{
	public interface IFileDataSource
	{
		string ReadAllText(string file);

		void WriteAllText(string file, string text);

		void Move(string file, string newFileNamePlusPath);
	}
	public class FileDataSource : IFileDataSource
	{
		public string ReadAllText(string file)
		{
			return File.ReadAllText(file);
		}

		public void WriteAllText(string file, string text)
		{
			File.WriteAllText(file, text);
		}

		public void Move(string file, string newFileNamePlusPath)
		{
			System.IO.File.Move(file, newFileNamePlusPath);
		}
	}
}
