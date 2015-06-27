using HDevTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDevToolsTest
{
	class Program
	{
		static void Main(string[] args)
		{
			HDevToolsRunner.StartServer(@"C:\Users\Luka\Documents\Haskell\snap-test\src\Application.hs");
			var res = HDevToolsRunner.GetType(@"C:\Users\Luka\Documents\Haskell\snap-test\src\Application.hs", 22, 1);
		}
	}
}
