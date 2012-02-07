using System.Collections.Generic;
using System.Linq;

namespace Hadouken
{
    public interface IValidFileFactory
    {
        /// <summary>
        /// Gets the specified file extension keyvaluepair.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns></returns>
        KeyValuePair<string, bool> Get(string fileExtension);
    }

    public class ValidFileFactory : IValidFileFactory
    {

        private Dictionary<string, bool> _validFileExtensions;

        public ValidFileFactory()
        {
            _validFileExtensions = GetValidFileExtensionList();
        }

        public KeyValuePair<string, bool> Get(string fileExtension)
        {
            var foundFile = _validFileExtensions.Where(x => x.Key == fileExtension);

            return foundFile.Any() ? foundFile.First() : new KeyValuePair<string, bool>("unknown", false);
        }

        private Dictionary<string, bool> GetValidFileExtensionList()
        {
            _validFileExtensions = new Dictionary<string, bool>();

            _validFileExtensions.Add(".txt", true);
            _validFileExtensions.Add(".cs", true);
            _validFileExtensions.Add(".vsmidi", true);
            _validFileExtensions.Add(".sln", true);
            _validFileExtensions.Add(".user", true);
            _validFileExtensions.Add(".cshtml", true);
            _validFileExtensions.Add(".vb", true);
            _validFileExtensions.Add(".xml", true);
            _validFileExtensions.Add(".config", true);
            _validFileExtensions.Add(".html", true);
            _validFileExtensions.Add(".htm", true);
            _validFileExtensions.Add(".js", true);
            _validFileExtensions.Add(".asmx", true);
            _validFileExtensions.Add(".edmx", true);
            _validFileExtensions.Add(".wdproj", true);
            _validFileExtensions.Add(".asax", true);
            _validFileExtensions.Add(".svc", true);
            _validFileExtensions.Add(".aspx", true);
            _validFileExtensions.Add(".ascx", true);
            _validFileExtensions.Add(".master", true);
            _validFileExtensions.Add(".csproj", true);

            return _validFileExtensions;
        }

       
    }
}
