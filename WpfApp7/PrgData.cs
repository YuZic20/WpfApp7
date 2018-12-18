using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp7
{
    class PrgData
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public PrgData(string IName, string IPath)
        {
            Name = IName;
            Path = IPath;

        }
    }
}
