using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AudioPrecision.API;


namespace TestBenchApplication
{
    public class APrunner
    {
        APx500 APx = new APx500();
        public void SetupAP()
        {
            APx.Visible = true;
        }
        public void OpenAPproject(string fileName)
        {
            APx.OpenProject(fileName);
        }
        public int RunAPproject()
        {
            return 1;
        }
    }
    
}
