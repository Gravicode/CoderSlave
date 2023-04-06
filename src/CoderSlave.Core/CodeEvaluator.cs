using CSScriptLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoderSlave.Core
{
    public class CodeEvaluator
    {
        IsolatedRunner IsolatedInstance { get; set; }
        StringWriter Sw { get; set; }
        public CodeEvaluator()
        {
            this.Sw = new StringWriter();
            Console.SetOut(new MultiTextWriter(Sw, Console.Out));
            IsolatedInstance = new IsolatedRunner();
        }
        public string CodeContent { get; set; }
        public ExecutionResult RunCodeInIsolation()
        {
            ExecutionResult rs=null;
            IsolatedInstance.ExecuteCode(() => {
                rs = RunCode();
            });
            return rs;
        }

            public ExecutionResult RunCode()
        {
            Sw.Reset();
            ExecutionResult res = new ExecutionResult();
            if (string.IsNullOrEmpty(CodeContent))
            {
                res.Result = "Code is empty, cannot compile.";
                return res;

            }
            Stopwatch sw = new Stopwatch();
            try
            {
               
                sw.Start();
                var info = new CompileInfo { RootClass = "main_script", AssemblyFile = $"script_{Guid.NewGuid().ToString().Replace("-","_")}.dll" };

                var app_asm = CSScript.Evaluator
                                          .CompileCode(
                                             CodeContent, info);

                var obj = app_asm
                    .GetType("main_script+Program")
                    .GetMethod("Run")
                    .Invoke(null, null);
                sw.Stop();
                res.ProcessorTime = sw.Elapsed;
                res.TotalMemoryAllocated = GC.GetTotalAllocatedBytes();
                res.IsSucceed = true;
                res.Result = Sw.GetContent();
            }
            catch (Exception ex)
            {
                res.Result = ex.ToString();
                Console.WriteLine(ex);
            }
            return res;
          
        }
    }
}
