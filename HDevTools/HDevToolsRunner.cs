using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HDevTools
{
    public class HDevToolsRunner
    {
		public static void StartServer(string path)
		{
			ProcessRunner.RunCommand("hdevtools --start-server", Path.GetDirectoryName(path));
		}

		public static void StopServer()
		{
			ProcessRunner.RunCommand("hdevtools --stop-server", Path.GetPathRoot(Assembly.GetExecutingAssembly().Location));
		}

		public static TypeInfo GetType(string path, int line, int column)
		{
			var info = new FileInfo(path);
			var res = ProcessRunner.RunCommand("hdevtools type \"" + info.FullName + "\" " + line + " " + column, info.DirectoryName);
			if (res == "" || res.StartsWith("Error")) return null;
			var reader = new StringReader(res);
			var lineStr = reader.ReadLine().Split(' ');
			var type = "";
			for (int i = 4; i< lineStr.Length; ++i)
			{
				type += lineStr[i] + " ";
			}
			type = type.Substring(0, type.IndexOf('"', 1));
			return new TypeInfo
			{
				StartLine = Int32.Parse(lineStr[0]),
				StartCol = Int32.Parse(lineStr[1]),
				EndLine = Int32.Parse(lineStr[2]),
				EndCol = Int32.Parse(lineStr[3]),
				Type = type.Substring(1, type.Length - 1)
			};
		}

		public static string Check(string path)
		{
			var info = new FileInfo(path);
			return ProcessRunner.RunCommand("ghc-mod check \"" + info.FullName + "\"", info.DirectoryName);
		}
    }
}
