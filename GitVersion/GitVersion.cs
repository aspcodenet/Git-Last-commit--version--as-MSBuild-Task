using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
/*
 * 
 * LICENSE MIT
 * 
 * (C) Stefan Holmberg Systementor AB http://www.aspcode.net
 * 
 */

namespace GitVersion
{
    public class GitVersion : Task
    {

        public GitVersion()
        {
            LocalPath = ".";
        }

        public string LocalPath { get; set; }


        [Output]
        public string LastCommit { get; set; }
        public override bool Execute()
        {
            System.Diagnostics.ProcessStartInfo oInfo = new System.Diagnostics.ProcessStartInfo("git.exe", " log --oneline -1");
            oInfo.UseShellExecute = false;
            oInfo.ErrorDialog = false;
            //oInfo.CreateNoWindow = true;
            oInfo.CreateNoWindow = false;
            oInfo.WorkingDirectory = LocalPath;
            oInfo.RedirectStandardOutput = true;

            try
            {
                Process p = System.Diagnostics.Process.Start(oInfo);
                p.WaitForExit();

                System.IO.StreamReader oReader2 = p.StandardOutput;
                string sRes = oReader2.ReadToEnd();
                oReader2.Close();
                LastCommit = sRes.Split(' ')[0];
                return true;
                //
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            
            return false;

        }
    }
}
