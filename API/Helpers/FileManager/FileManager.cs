using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace API.Helpers.FileManager
{
    public class FileManager : IFileManager
    {
        public Guid Save(Stream stream, string filename, string type, string path)
        {
            var guid = Guid.Empty;
            string fullFilename = string.Empty;

            if (stream == null || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(path))
                return guid;

            if (string.IsNullOrEmpty(filename)) //ako nije naznacio kako zeli da mu se file zove generis novi po guid-u
            {
                guid = Guid.NewGuid();
                filename = $"{guid}";
            }

            fullFilename = $"{filename}{type}";

            string location = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(path), fullFilename);

            try
            {
                var fileStream = new FileStream(location, FileMode.Create, FileAccess.Write);
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
                fileStream.Dispose();

                return guid;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public bool Delete(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(fileName))
                return false;

            var filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(path), fileName);

            if (!File.Exists(filePath))
                return true;

            try
            {
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public byte[] Download(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(fileName))
                return null;

            byte[] filestream = null;

            try
            {
                string location = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(path), fileName.ToString());

                filestream = File.ReadAllBytes(location);
            }
            catch
            {
                throw;
            }

            return filestream;
        }

        public List<byte[]> Download(string path, List<string> fileNames)
        {
            if (string.IsNullOrEmpty(path) || fileNames.Count == 0)
                return null;

            return fileNames.Select(x => Download(path, x)).ToList();
        }
    }
}