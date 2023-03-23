using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.TaskScheduler;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;

namespace Optimize
{
    class Program
    {
        static string games;
        static List<string> list = new List<string>();
        static List<string> killed = new List<string>();
        static string useless;
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static string[] code = { @"set taskDefinition=taskService.NewTask()", "set taskDefinition.RegistrationInfo.Description=\"a basic pc optimizer\"", "set trigger=new LogonTrigger()", "set taskDefinition.Triggers.Add(%trigger%)", "set action=new ExecAction(\"C:\\Users%USERNAME%\\AppData\\Roaming\\optimizerq\\main.exe\\\")", "set taskDefinition.Actions.Add(%action%)", "taskService.RootFolder.RegisterTaskDefinition(\"optimizer\", %taskDefinition%)" };
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);
            if ((!Directory.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq") || !File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\list.txt") || !File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\games.txt")) == true)
            {
                try
                {
                    var outpath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\Microsoft.Win32.TaskScheduler.dll";
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    const string name = "Optimize.emb.SHED.dll";

                    using (Stream stream = assembly.GetManifestResourceStream(name))
                    {
                        using (BinaryReader r = new BinaryReader(stream))
                        {
                            using (FileStream fs = new FileStream(outpath, FileMode.Create))
                            {
                                using (BinaryWriter b = new BinaryWriter(fs))
                                {
                                    b.Write(r.ReadBytes((int)stream.Length));
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
                Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq");
                try
                {
                    File.Copy(Assembly.GetExecutingAssembly().Location, @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\main.exe");
                }
                catch
                {

                }
                using (FileStream fs2 = new FileStream(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\list.txt", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    var data = "msedge.exe notepad.exe mspaint.exe control.exe firefox.exe chrome.exe CCleaner64.exe CCleaner32.exe";
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    fs2.Write(bytes, 0, bytes.Length);
                    fs2.Flush();
                    fs2.Dispose();
                }
                using (FileStream fs3 = new FileStream(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\games.txt", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    var data = "RobloxPlayerLauncher.exe RobloxPlayerBeta.exe RobloxStudioLauncherBeta.exe RobloxStudioLauncher.exe javaw.exe UnityCrashHandler64.exe UnityCrashHandler32.exe GeometryDash.exe";
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    fs3.Write(bytes, 0, bytes.Length);
                    fs3.Flush();
                    fs3.Dispose();
                }
                Process.Start(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\main.exe");
                try
                {
                    var outpath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\start.bat";
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    const string name = "Optimize.emb.start.bat";

                    using (Stream stream = assembly.GetManifestResourceStream(name))
                    {
                        using (BinaryReader r = new BinaryReader(stream))
                        {
                            using (FileStream fs = new FileStream(outpath, FileMode.Create))
                            {
                                using (BinaryWriter b = new BinaryWriter(fs))
                                {
                                    b.Write(r.ReadBytes((int)stream.Length));
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
                Thread.Sleep(100);
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\start.bat";
                psi.Arguments = "runas";
                Process.Start(psi);
                Environment.Exit(0);
            }
            using (FileStream fs = new FileStream(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\list.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                useless = sr.ReadToEnd();
                sr.Close();
            }
            using (FileStream fs1 = new FileStream(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\optimizerq\games.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sr1 = new StreamReader(fs1))
            {
                games = sr1.ReadToEnd();
                sr1.Close();
            }
            while (true)
            {
                Thread.Sleep(2000);
                Process[] processes = Process.GetProcesses();
                list.Clear();
                foreach (var process1 in processes)
                {
                    if (games.ToLower().Contains(process1.ProcessName))
                    {
                        foreach (Process p in processes)
                        {
                            list.Add(p.ProcessName);
                        }
                        foreach (string ps in list)
                        {
                            if (useless.ToLower().Contains(ps.ToLower()))
                            {
                                foreach (var process in Process.GetProcessesByName(ps))
                                {
                                    killed.Add(ps);
                                    process.Kill();
                                }
                            }
                        }
                    } else if (games.ToLower().Contains(process1.ProcessName) == false)
                    {
                        foreach (var process in killed)
                        {
                            Process.Start(process);
                        }
                        killed.Clear();
                    }
                }
            }
        }
    }
}
