using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukuSolver
{
    public class Cell
    {
        public int Value { get; set; }
        /// <summary>
        /// 0 based
        /// </summary>
        public int XCoordinates { get; set; }
        /// <summary>
        /// 0 based
        /// </summary>
        public int YCoordinates { get; set; }

        /// <summary>
        /// index between 0-80
        /// </summary>
        public int PositionInArray
        {
            get
            {
                return YCoordinates * 9 + XCoordinates;
            }
        }
    }
}
