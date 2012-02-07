using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hadouken.IO;

namespace Hadouken
{
    public class FindReplaceHelper
    {
        private string startPath;
        private string newSolutionName;
        private string magicWord;
        private IDirectoryDataSource directoryProvider;
        private IFileDataSource fileProvider;
        private IFileValidator _fileValidator;
        private IOutputController outputController;


        public FindReplaceHelper(string startingPath, string newSolutionValue)
        {
            startPath = startingPath;
            newSolutionName = newSolutionValue;
            magicWord = Config.MAGIC_WORD;
            directoryProvider = new DirectoryDataSource();
            fileProvider = new FileDataSource();
            outputController=new OutputController();
            _fileValidator = new FileValidator();
        }

        public FindReplaceHelper(string startingPath, string newSolutionValue, IDirectoryDataSource dirDataSource, IFileDataSource fileDataSource, IOutputController outputControl,IFileValidator fileValidator)
        {
            startPath = startingPath;
            newSolutionName = newSolutionValue;
            magicWord = Config.MAGIC_WORD;
            directoryProvider = dirDataSource;
            fileProvider = fileDataSource;
            outputController = outputControl;
            _fileValidator = fileValidator;
        }

        public void DoCoolStuff()
        {
            string[] files = directoryProvider.GetFiles(startPath, "*.*", SearchOption.AllDirectories);
            
            Parallel.ForEach(files, ConductFindReplace);
        }

        private void ConductFindReplace(string filePath)
        {
            string text;
            if (IsValidFileForReplace(filePath))
            {
                outputController.WriteLine("Find&Replace on file: {0}", filePath);
                text = fileProvider.ReadAllText(filePath);
                text = text.Replace(magicWord, newSolutionName);
                fileProvider.WriteAllText(filePath, text);
            }
        }

        private bool IsValidFileForReplace(string file)
        {
            string ext = Path.GetExtension(file);
            int lastIndexOfDot = ext != null ? ext.LastIndexOf(".") : 0;

            if (lastIndexOfDot > 0 && ext != null)
                ext = ext.Substring(lastIndexOfDot, ext.Length);

            return ext != null ? _fileValidator.IsValidFile(ext.ToLower()) : false;
        }


    }
}
