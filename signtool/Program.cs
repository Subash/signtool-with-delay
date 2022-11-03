using System.Diagnostics;

class Program
{
    static int Main(string[] args)
    {
        int exitCode = ExecuteSignTool(args);
        Console.WriteLine("Waiting 15 Seconds");
        Thread.Sleep(15000);
        return exitCode;
    }

    static int ExecuteSignTool(string[] args)
    {
        string execPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string execDir = Path.GetDirectoryName(execPath);
        string signToolPath = Path.Join(execDir, "real-signtool.exe");

        ProcessStartInfo startInfo = new ProcessStartInfo(signToolPath);

        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardError = true;
        startInfo.RedirectStandardOutput = true;

        foreach (string argument in args)
        {
            startInfo.ArgumentList.Add(argument);
        }

        Process process = Process.Start(startInfo);
        process.WaitForExit();
        int exitCode = process.ExitCode;
        process.Close();

        return exitCode;
    }
}