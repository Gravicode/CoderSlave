using DotNetIsolator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoderSlave.Core
{
    public class IsolatedRunner
    {
        public IsolatedRuntimeHost host { get; set; }
        public IsolatedRuntime runtime { get; set; }
        public IsolatedRunner()
        {
            // Set up an isolated runtime
            host = new IsolatedRuntimeHost().WithBinDirectoryAssemblyLoader();
            runtime = new IsolatedRuntime(host);

            // Output: I'm running on X64
            Console.WriteLine($"I'm running on {RuntimeInformation.OSArchitecture}");

        }

        public string GetInfo()
        {
            var realInfo = GetEnvironmentInfo();
            var isolatedInfo = runtime.Invoke(GetEnvironmentInfo);
            Console.WriteLine($"Real env: {realInfo}");
            Console.WriteLine($"Isolated env: {isolatedInfo}");
            return isolatedInfo.ToString();            
}
        static EnvInfo GetEnvironmentInfo()
        {
            var sysRoot = Environment.GetEnvironmentVariable("SystemRoot") ?? "(Not set)";
            return new EnvInfo(
                Environment.GetEnvironmentVariables().Count,
                $"SystemRoot={sysRoot}");
        }

        // Demonstrates that you can return arbitrarily-typed objects
        record EnvInfo(int NumEnvVars, string ExampleEnvVar)
        {
            public override string ToString() => $"{NumEnvVars} entries, including {ExampleEnvVar}";
        }
        public void ExecuteCode(Action action)
        {
            runtime.Invoke(() =>
            {
                // Output: I'm running on Wasm
                action.Invoke();
            });
        }
    }
}
