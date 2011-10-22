using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hadouken.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Hadouken.Tests
{
	[TestClass]
	public class FindReplaceHelperTests
	{
		private IFileDataSource fs;
		private IDirectoryDataSource ds;
		private IOutputController o;
		private int fileReadCount = 0;
		private int fileWriteCount = 0;

		[TestInitialize]
		public void Setup()
		{
			fs = Substitute.For<IFileDataSource>();
			ds = Substitute.For<IDirectoryDataSource>();
			o = Substitute.For<IOutputController>();

			ds.EnumerateDirectories(Arg.Any<string>()).Returns(new List<string>()
			                                                   	{
			                                                   		@"C:\test\directory1",
			                                                   		@"C:\test\directory1\directory2",
			                                                   		@"C:\test\directory1\MySolution.directory2",
			                                                   		@"C:\test\directory1\MySolution.directory3"
			                                                   	});

			ds.GetFiles(Arg.Any<string>(),Arg.Any<string>(),Arg.Any<SearchOption>()).Returns(new string[]
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
		}

		[TestMethod]
		public void DoCoolStuff_6ValidFiles_ShouldCallRead6Times()
		{
			fs.ReadAllText("test").ReturnsForAnyArgs(x =>
			{
				fileReadCount++;
				return "test";
			});

			FindReplaceHelper r = new FindReplaceHelper("C:\test", "NewValue", ds, fs, o);
			r.DoCoolStuff();

			Assert.IsTrue(fileReadCount == 6, "Incorrect number of files read (called {0})", fileReadCount);
		}

		[TestMethod]
		public void DoCoolStuff_6ValidFiles_ShouldCallWrite6Times()
		{
			fs.When(x => x.WriteAllText(Arg.Any<string>(), Arg.Any<string>())).Do(x => 	fileWriteCount++);

			FindReplaceHelper r = new FindReplaceHelper("C:\test", "NewValue", ds, fs, o);
			r.DoCoolStuff();

			Assert.IsTrue(fileWriteCount == 6, "Incorrect number of files written to (called {0})", fileWriteCount);
		}

	}
}
