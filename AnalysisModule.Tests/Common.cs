using System.IO;
using System.Reflection;

namespace AnalysisModule.Tests
{
    public static class Common
    {
        public static string ReadFile(string file)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = " AnalysisModule.Tests.";
            var fileName = resourceName + file;
            var names = assembly.GetManifestResourceNames();

            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd() ;
            }
        }
    }
}
