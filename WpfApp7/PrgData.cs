using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7
{
    class PrgData
    {
        public string Name = "";
        public string PathF = "";
        public string PathL = "";

        public PrgData(string IName, string IPathF, string IPathL)
        {
            Name = IName;
            PathF = IPathF;
            PathL = IPathL;

        }
    }
}
