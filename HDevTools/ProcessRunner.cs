using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HDevTools
{
	class ProcessRunner
	{
		public static string RunCommand(string command, string cwd)
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo("cmd")
				{
					Arguments = "/C " + command,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					UseShellExecute = false,
					WorkingDirectory = cwd,
					CreateNoWindow = true
				}
			};
			var output = new StringBuilder();
			process.OutputDataReceived += delegate(object o, DataReceivedEventArgs args)
			{
				if (args.Data == null) return;
				output.Append(args.Data);
				if (args.Data[args.Data.Length - 1] != '\n') output.Append('\n');
			};
			process.ErrorDataReceived += delegate(object o, DataReceivedEventArgs args)
			{
				output.Append(args.Data);
			};
			process.Start();
			process.BeginErrorReadLine();
			process.BeginOutputReadLine();
			process.WaitForExit();
			return output.ToString();
		}
	}
}
