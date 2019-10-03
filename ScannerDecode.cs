using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ScannerDisplay
{
    public class ScannerDecode
    {
        public int Block
        { get; set; }

        public int ID
        { get; set; }

        public int ToatalLength
        { get; set; }

        public int Block_From_Calc
        { get; set; }

        public List<short> Distance
        { get; set; }

        public int Start_Index
        { get; set; }

        public int Stop_Index
        { get; set; }

        public int Resolution
        { get; set; }

        public ScannerDecode Decode(byte[] rawbyte)
        {
            ScannerDecode Dec_Data = new ScannerDecode()
            {
                ID = ScannerFunctionality.GetID(rawbyte),
                Block = ScannerFunctionality.GetBlock(rawbyte),
                ToatalLength = ScannerFunctionality.GetTotalLength(rawbyte)
            };

            if (Dec_Data.ID == 1)
            {
                Dec_Data.Start_Index = ScannerFunctionality.GetStartIndex(rawbyte);
                Dec_Data.Stop_Index = ScannerFunctionality.GetStopIndex(rawbyte);
                Dec_Data.Resolution = ScannerFunctionality.GetResolution(rawbyte);
                Dec_Data.Block_From_Calc = ScannerFunctionality.GetBlockFromCalculation(rawbyte);
            }
            else
            {
                Dec_Data.Distance = ScannerFunctionality.GetDistanceList(rawbyte);
            }

            return Dec_Data;
        }
    }
}
