using System;
using System.Collections.Generic;
using System.IO;

namespace API.Helpers.FileManager
{
    public interface IFileManager
    {
        Guid Save(Stream stream, string filename, string type, string path);
        bool Delete(string path, string fileName);

        byte[] Download(string path, string fileName);
        List<byte[]> Download(string path, List<string> fileNames);
    }
}