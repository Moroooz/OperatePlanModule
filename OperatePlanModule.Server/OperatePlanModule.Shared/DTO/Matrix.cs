using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatePlanModule.Shared.DTO
{
    public class Matrix
    {
        public int CountDetails {  get; set; }
        public int CountMachines { get; set; }
        public double[][] Values { get; set; }
    }
}
