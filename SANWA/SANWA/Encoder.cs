using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace SANWA.Utility
{
    public class Encoder
    {
        public EncoderAligner Aligner;
        public EncoderRobot Robot;
        public EncoderOCR OCR;
        public EncoderLoadPort LoadPort;
        public Encoder_SmartTag SmartTag;
        public EncoderFFU FFU;

        private string Supplier;
     


        /// <summary>
        /// Encoder
        /// </summary>
        /// <param name="supplier"> Equipment supplier </param>
        public Encoder(string supplier)
        {
     
          
            try
            {
                Supplier = supplier.ToUpper();

                Aligner = new EncoderAligner(Supplier);
                Robot = new EncoderRobot(Supplier);
                OCR = new EncoderOCR(Supplier);
                LoadPort = new EncoderLoadPort(Supplier, EncoderLoadPort.CommandMode.TDK_A);
                FFU = new EncoderFFU(Supplier);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
             
            }
        }

       
    }
}
