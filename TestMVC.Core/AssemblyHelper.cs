using System.Diagnostics;

namespace TestMVC.Core
{
    public class AssemblyHelper
    {
        public static string GetFormattedAssemblyInfo(object type)
        {
            var className = type.ToString();
            var assemblyVersion = GetAssemblyVersion();
            return $"{className}.{assemblyVersion}";
        }

        public static string GetAssemblyVersion() => FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
    }
}