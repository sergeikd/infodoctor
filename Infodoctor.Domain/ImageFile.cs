using System;
using System.IO;

namespace Infodoctor.Domain
{
    public class ImageFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public static object FromStream(Stream inputStream, bool v1, bool v2)
        {
            throw new NotImplementedException();
        }
    }
}
