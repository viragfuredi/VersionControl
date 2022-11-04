using irf_gyak7.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace irf_gyak7.Entities
{
    public class PresentFactory : IToyFactory
    {
        public Color PresentColor { get; set; }

        public Toy CreateNew()
        {
            return new Present(PresentColor);
        }
    }
}
