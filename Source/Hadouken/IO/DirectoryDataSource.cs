using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hadouken.IO
{
	public interface IDirectoryDataSource
	{
		string[] GetFiles(string startPath, string p, System.IO.SearchOption searchOption);

		IEnumerable<string> EnumerateDirectories(string startPath);

		void Move(string folder, string newFolderPath);

		IEnumerable<string> EnumerateFiles(string startPath);
	}
	public class DirectoryDataSource : IDirectoryDataSource
	{
		public string[] GetFiles(string startPath, string p, System.IO.SearchOption searchOption)
		{
			return Directory.GetFiles(startPath, p, searchOption);	
		}

		public IEnumerable<string> EnumerateDirectories(string startPath)
		{
			return System.IO.Directory.EnumerateDirectories(startPath,"*",SearchOption.AllDirectories);
		}

		public void Move(string folder, string newFolderPath)
		{
			System.IO.Directory.Move(folder, newFolderPath);
		}


		public IEnumerable<string> EnumerateFiles(string startPath)
		{
			return System.IO.Directory.EnumerateFiles(startPath);
		}
	}
}
