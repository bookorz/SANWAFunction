﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANWA
{
    public class EncoderFFU
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
                    byte[] cmdAry = new byte[7];
                    cmdAry[0] = Convert.ToByte("2", 16);//start

                    cmdAry[1] = Convert.ToByte("69", 16);
                    cmdAry[2] = Convert.ToByte("2", 16);//len
                    cmdAry[3] = Convert.ToByte(Address, 16);//id
                    cmdAry[4] = Convert.ToByte(Convert.ToInt32(Value) / 10);//speed
                    cmdAry[5] = ACDTCheckSum(cmdAry,1,4);
                    cmdAry[6] = 3;//end
                    commandStr = BitConverter.ToString(cmdAry);
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

                    byte[] cmdAry = new byte[6];
                    cmdAry[0] = Convert.ToByte("2", 16);//start

                    cmdAry[1] = Convert.ToByte("68", 16);
                    cmdAry[2] = Convert.ToByte("1", 16);//len
                    cmdAry[3] = Convert.ToByte(Address, 16);//id
                    cmdAry[4] = ACDTCheckSum(cmdAry, 1, 3);

                    cmdAry[5] = 3;//end
                    commandStr = BitConverter.ToString(cmdAry);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return commandStr;
        }

        private byte ACDTCheckSum(byte[] commandAry, int startIdx, int endIdx)
        {
            byte result = 0;
            for(int i = startIdx; i <= endIdx; i++)
            {
                result += commandAry[i];
            }
            return result;
        }
    }
}
