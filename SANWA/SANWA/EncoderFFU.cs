using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA
{
    class EncoderFFU
    {
        private string Supplier;

        /// <summary>
        /// Aligner Encoder
        /// </summary>
        /// <param name="supplier"> 設備供應商 </param>
        /// <param name="dtCommand"> Parameter List </param>
        public EncoderFFU(string supplier)
        {
            try
            {
                Supplier = supplier;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
       
        public string SetSpeed(string Address, string Value)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "ACDT":

                    commandStr = "69 02 {0} {1}";
                    commandStr = string.Format(commandStr, Address,(Convert.ToInt32(Value,16)/10).ToString("X").PadLeft(2,'0'));
                    commandStr = commandStr + " " + CheckSum(commandStr);
                    commandStr = "02 " + commandStr + " 03";
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
        }

        public string GetStatus(string Address)
        {
            string commandStr = "";
            switch (Supplier)
            {
                case "ACDT":

                    commandStr = "68 01 {0}";
                    commandStr = string.Format(commandStr, Address);
                    commandStr = commandStr + " " + CheckSum(commandStr);
                    commandStr = "02 " + commandStr + " 03";
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
        }

        private string CheckSum(string commandStr)
        {
            string result = "";
            string[] ary = commandStr.Split(' ');
            int sum = 0;
            foreach(string each in ary)
            {
                if (!each.Trim().Equals(""))
                {
                    sum += Convert.ToInt32(each,16);
                }
            }
            result = sum.ToString("X");
            result = result.Substring(result.Length-2,2);
            return result;
        }
    }
}
