using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hadouken.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Hadouken.Tests
{
	[TestClass]
	public class RenameHelperTests
	{
		private IFileDataSource fs;
		private IDirectoryDataSource ds;
		private IOutputController o;
		private int fileMoveCounter = 0;
		private int folderMoveCounter = 0;

		[TestInitialize]
		public void Setup()
		{
			fs = Substitute.For<IFileDataSource>();
			ds= Substitute.For<IDirectoryDataSource>();
			o = Substitute.For<IOutputController>();

			ds.EnumerateDirectories(Arg.Any<string>()).Returns(new List<string>()
			                                                   	{
			                                                   		@"C:\test\directory1",
																	@"C:\test\directory1\directory2",
																	@"C:\test\directory1\MySolution.directory2",
																	@"C:\test\directory1\MySolution.directory3"
			                                                   	});

			ds.EnumerateFiles(Arg.Any<string>()).Returns(new List<string>()
			                                             	{
			                                             		@"C:\test\directory1\test.txt",
			                                             		@"C:\test\directory1\directory2\test2.txt",
																@"C:\test\directory1\directory2\test2.dll",
																@"C:\test\directory1\directory2\MySolution.test2.dll",
																@"C:\test\directory1\directory2\MySolution.test2.txt",
																@"C:\test\directory1\directory2\MySolution.test3.html",
																@"C:\test\directory1\directory2\MySolution.test4.csproj",
																@"C:\test\directory1\directory2\MySolution.test5.sln"
			                                             	});

			fs.When(x => x.Move(Arg.Any<string>(), Arg.Any<string>())).Do(x => fileMoveCounter++);
			ds.When(x => x.Move(Arg.Any<string>(), Arg.Any<string>())).Do(x => folderMoveCounter++);
		}

		[TestMethod]
		public void DoCoolStuff_5ValidFiles_ShouldRename5Files()
		{
			RenameHelper r = new RenameHelper("C:\test", "NewValue", ds, fs, o);
			r.DoCoolStuff();

			Assert.IsTrue(r.FileRenameCounter == 5,"Incorrect number of files renamed");
		}

		[TestMethod]
		public void DoCoolStuff_2ValidFolders_ShouldRename2Folders()
		{
			RenameHelper r = new RenameHelper("C:\test", "NewValue", ds, fs, o);
			r.DoCoolStuff();

			Assert.IsTrue(r.FolderRenameCounter == 2,"Incorrect number of folders renamed");
		}

		[TestMethod]
		public void DoCoolStuff_2ValidFolders_ShouldCallFolderMoveTwice()
		{
			RenameHelper r = new RenameHelper("C:\test", "NewValue", ds, fs, o);
			r.DoCoolStuff();

			Assert.IsTrue(folderMoveCounter == 2,"Move called incorrect amount of times");
		}

		[TestMethod]
		public void DoCoolStuff_5ValidFiles_ShouldCallMove5Times()
		{
			RenameHelper r = new RenameHelper("C:\test", "NewValue", ds, fs, o);
			r.DoCoolStuff();

			Assert.IsTrue(fileMoveCounter == 5,"Move called incorrect amount of times");
		}
	}
}
