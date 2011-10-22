using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hadouken.IO;

namespace Hadouken
{
	public class RenameHelper
	{
		private string startPath;
		private string newSolutionValue;
		private string magicWord;
		private IDirectoryDataSource directoryProvider;
		private IFileDataSource fileProvider;
		private IOutputController outputController;

		public IEnumerable<string> FolderList;
		public IEnumerable<string> FileList;
		public int FileRenameCounter = 0;
		public int FolderRenameCounter = 0;


		public RenameHelper(string startingPath, string newSolutionVal)
		{
			startPath = startingPath;
			newSolutionValue = newSolutionVal;
			magicWord = Config.MAGIC_WORD;
			directoryProvider = new DirectoryDataSource();
			fileProvider = new FileDataSource();
			outputController = new OutputController();
		}

		public RenameHelper(string startingPath, string newSolutionVal, IDirectoryDataSource dirProvider, IFileDataSource fileProv, IOutputController outputControl)
		{
			startPath = startingPath;
			newSolutionValue = newSolutionVal;
			magicWord = Config.MAGIC_WORD;
			directoryProvider = dirProvider;
			fileProvider = fileProv;
			outputController = outputControl;
		}

		public void DoCoolStuff()
		{
			outputController.WriteLine("Renaming all files and folders that contain the magic word '{0}'", magicWord);

			FolderList = directoryProvider.EnumerateDirectories(startPath);
			foreach (string folder in FolderList)
			{
				string folderNameWithoutBase = folder.Replace(startPath, "");
				if (folderNameWithoutBase.Contains(magicWord))
				{
					string newFolderPath = folder.ReplaceLastOccurance(magicWord, newSolutionValue);
					directoryProvider.Move(folder, newFolderPath);
					outputController.WriteLine("Renaming folder {0}", folder);
					FolderRenameCounter++;
				}
			}

			FileList = directoryProvider.GetFiles(startPath, "*.*", SearchOption.AllDirectories);

			foreach (string file in FileList)
			{
				string fileName = Path.GetFileName(file);
				if (fileName.Contains(magicWord))
				{
					string newFileName = fileName.ReplaceLastOccurance(magicWord, newSolutionValue);
					string newFileNamePlusPath = String.Format("{0}\\{1}",Path.GetDirectoryName(file),newFileName);
					fileProvider.Move(file, newFileNamePlusPath);
					outputController.WriteLine("Renaming file {0}", file);
					FileRenameCounter++;
				}
			}
		}
	}
}
