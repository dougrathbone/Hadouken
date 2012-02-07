
namespace Hadouken
{
    public interface IFileValidator
    {
        /// <summary>
        /// Determines whether the file extension is a valid file
        /// </summary>
        /// <param name="fileExtenion">The file extenion.</param>
        /// <returns>
        ///   <c>true</c> if [is valid file] [the specified file extenion]; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidFile(string fileExtenion);
    }

    public class FileValidator : IFileValidator
    {

        private IValidFileFactory _validFileFactory;

        public FileValidator(IValidFileFactory validFileFactory)
        {
           _validFileFactory = validFileFactory;
        }

        public FileValidator()
        {
            _validFileFactory = new ValidFileFactory();
        }

        public bool IsValidFile(string fileExtenion)
        {
           return _validFileFactory.Get(fileExtenion).Value;
        }
    }

}
