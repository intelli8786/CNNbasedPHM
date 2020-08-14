using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNNBasedPHM.Extension
{
    public static class ExString
    {
        public static String Parse(this String str, String start, String end, int startPoint = 0)
        {
            //Console.WriteLine("문자열:" + str);
            int m_intTargetStartPoint = str.IndexOf(start, startPoint) + start.Length;
            //Console.WriteLine("시작지점:" + m_intTargetStartPoint);
            int m_intTargetLength = str.IndexOf(end, m_intTargetStartPoint) - m_intTargetStartPoint;
            return str.Substring(m_intTargetStartPoint, m_intTargetLength);
        }

        public static String ReplaceFirst(this String str, String oldValue, String newValue)
        {

            int pos = str.IndexOf(oldValue);
            if (pos < 0)
            {
                return str;
            }
            return str.Substring(0, pos) + newValue + str.Substring(pos + oldValue.Length);
        }

        public static String ReplaceLast(this String str, String oldValue, String newValue)
        {
            int pos = str.LastIndexOf(oldValue);
            if (pos < 0)
            {
                return str;
            }
            return str.Substring(0, pos) + newValue + str.Substring(pos + oldValue.Length);
        }
    }
}
