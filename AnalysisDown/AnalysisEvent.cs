using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisDown
{
    public static class AnalysisEvent
    {
        /// <summary>
        /// Делегат, для методов информирования
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public delegate void PrintResult(string text);
        public static PrintResult PrintCompileInfo;
        public static PrintResult PrintMessage;
    }
}
