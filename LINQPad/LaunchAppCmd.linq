<Query Kind="Program" />

void Main()
{
       var appcmdOutput = CatchOutput();
	   System.IO.File.WriteAllText(@"SomeFileName",appcmdOutput);       
}

// Define other methods and classes here
static string CatchOutput()
{
       String consoleOut = String.Empty;
       System.Diagnostics.ProcessStartInfo startInfo = new ProcessStartInfo();
       startInfo.CreateNoWindow = false;
       startInfo.UseShellExecute = false;
       startInfo.WindowStyle = ProcessWindowStyle.Hidden;
       startInfo.FileName = @"C:\Windows\system32\inetsrv\appcmd.exe";
       startInfo.Arguments = "list site -xml";
       startInfo.RedirectStandardOutput = true;
       
       try
       {              
              using (Process exeProc = Process.Start(startInfo))
              {
                     consoleOut = exeProc.StandardOutput.ReadToEnd();
              }
       }
       catch (Exception ex)
       {
              
              throw;
       }
       
       return consoleOut;
}

