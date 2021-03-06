using System.Collections.Generic;
using System.IO;
using System.Linq;
using Exception;

namespace Util
{
    public class FileHelper
    {
        private static Dictionary<string, Dictionary<string, string>> _filePathByFileNameBySearchPath =
            new Dictionary<string, Dictionary<string, string>>();

        // create a cache of all files under the searchPath the first time it's called, should make searching for lots of textures quicker
        public static string GetTexture(string searchPath, string fileName)
        {
            if (!_filePathByFileNameBySearchPath.ContainsKey(searchPath))
            {
                _filePathByFileNameBySearchPath[searchPath] = CreateFilePathForFileNameDictionary(searchPath);
            }

            if (!_filePathByFileNameBySearchPath[searchPath].ContainsKey(fileName))
            {
                throw new TextureNotFoundException($"Texture {fileName} does not exist under the path {searchPath}");
            }

            return _filePathByFileNameBySearchPath[searchPath][fileName];
        }

        private static Dictionary<string, string> CreateFilePathForFileNameDictionary(string searchPath)
        {
            return Directory.GetFiles(searchPath, "*.jpg", SearchOption.AllDirectories)
                .ToDictionary(f => Path.GetFileName(f), f => f);
        }
    }
}
