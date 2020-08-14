using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CNNBasedPHM
{
    class PythonPlugin
    {
        StreamWriter writer;
        StreamReader reader;
        
        public PythonPlugin(String command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = @"C:\Users\KimJiSeong\AppData\Local\conda\conda\envs\tensorflow\python.exe";
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardInput = true;
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processInfo.CreateNoWindow = true;
            processInfo.Arguments = @"..\..\..\NeuralNetwork\" + command;

            Process process = Process.Start(processInfo);
            writer = process.StandardInput;
            reader = process.StandardOutput;
        }

        public void WriteLine(String str)
        {
            Thread.Sleep(10);
            writer.WriteLine(str);
        }

        public String ReadLine()
        {
            Thread.Sleep(10);
            return reader.ReadLine();
        }
    }
}
