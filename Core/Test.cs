using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Seagull.BarTender.Print;

using FPath = Fluent.IO.Path;

namespace Core {
    public static class Test {
        public static void TestPrint(string value) {
            using var engine = new Engine(true);
            using var TemplateFile = VirtualFile.FromStream(
                GetTemplateResourceStreamByName("test01.btw"));

            var format = engine.Documents
                .Open(TemplateFile.Path,
                    "TSC TTP-345 (Ethernet)");

            format.PrintSetup.IdenticalCopiesOfLabel = 1;
            format.PrintSetup.NumberOfSerializedLabels = 1;

            format.SubStrings["Name"].Value = value;

            format.Print("Test", 3000);

        }

        static Stream GetTemplateResourceStreamByName(string name) {
            var assem = Assembly.GetExecutingAssembly();
            return
                assem.GetManifestResourceStream(
                    assem.GetManifestResourceNames()
                        .Single(str => str.EndsWith(name)));
        }
    }

    class VirtualFile : IDisposable {
        public string Path { get; }
        VirtualFile(string path) {
            Path = path;
        }
        public static VirtualFile FromStream(Stream contentStream) {

            int counter = 1;
            string baseName = "_temp_";
            FPath basePath = new FPath(AppDomain.CurrentDomain.BaseDirectory);
            FPath uniquePath;

            do {
                uniquePath = basePath.Combine(baseName + counter + ".btw");
                if (!uniquePath.Exists) break;
                if (counter > 100) throw new TooManyVirtualFilesException();
                counter++;
            } while (true);

            using var fs = new FileStream(uniquePath.ToString(), FileMode.CreateNew);
            contentStream.CopyTo(fs);

            return new VirtualFile(uniquePath.ToString());
        }
        public void Dispose() {
            File.Delete(Path);
        }

        public class TooManyVirtualFilesException : Exception {
            public TooManyVirtualFilesException() { }
            public TooManyVirtualFilesException(string message) : base(message) { }
            public TooManyVirtualFilesException(string message, Exception inner) : base(message, inner) { }
            protected TooManyVirtualFilesException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}
