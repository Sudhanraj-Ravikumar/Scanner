using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using IPS7Lnk.Advanced;
using ScannerDisplay.DatablockObjects;

namespace ScannerDisplay
{
    public enum PlcInformationType
    {
        Undef = 0,
        PlcStatus = 1,
        EdgePulse = 2
    }

    public class OpcClient
    {
        public OpcClient()
        {
            try
            {
                LogFiler = new Logger("OpcClient");

                try
                {
                    Rack = int.Parse(ConfigurationManager.AppSettings["Client.Rack"]);
                }
                catch (Exception ex)
                {
                    LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
                    return;
                }

                try
                {
                    Slot = int.Parse(ConfigurationManager.AppSettings["Client.Slot"]);
                }
                catch (Exception ex)
                {
                    LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
                    return;
                }

                try
                {
                    EndPoint = ConfigurationManager.AppSettings["Client.IpAddress"];
                    IPEndPoint = new IPDeviceEndPoint(System.Net.IPAddress.Parse(EndPoint), Rack, Slot);
                }
                catch (Exception ex)
                {
                    LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
                    return;
                }

                IsConnected = OpenConnection();

                BeginProcessing();
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private SiemensDevice Client
        { get; set; }

        private IPDeviceEndPoint IPEndPoint
        { get; set; }

        private PlcDeviceConnection PlcConnection
        { get; set; }

        private Logger LogFiler
        { get; set; }

        private string EndPoint
        { get; set; }

        private int Rack
        { get; set; }

        private int Slot
        { get; set; }

        private bool IsConnected
        { get; set; }

        private void BeginProcessing()
        {
            AnalysisFunctionality.GetInformationfromPOI += new AnalysisFunctionality.POIDetected(CalculateInformation);
        }

        private bool OpenConnection()
        {
            try
            {
                Client = new SiemensDevice(IPEndPoint, SiemensDeviceType.S71500);

                Licenser.LicenseKey = "lgAAAA29d9Q/xtEBlgFDb21wYW55TmFtZT1Mb2dvVGVrICBHbWJIIEdlc2VsbHNjaGFmdCBmw7xyIEluZm9ybWF0aW9uc3RlY2hub2xvZ2llO0ZpcnN0TmFtZT1DaHJpc3RvcGhlcjtMYXN0TmFtZT1Lw7ZtcGVsO0VtYWlsPWNocmlzdG9waGVyLmtvZW1wZWxAbG9nb3Rlay1nbWJoLmRlO0NvdW50cnlOYW1lPUQ7Q2l0eU5hbWU9TWFya3RoZWlkZW5mZWxkO1ppcENvZGU9OTc4Mjg7U3RyZWV0TmFtZT1BbiBkZXIgS8O2aGxlcmVpIDc7U3RyZWV0TnVtYmVyPTtSZXRhaWxlck5hbWU9VHJhZWdlciBJbmR1c3RyeSBDb21wb25lbnRzO1ZvbHVtZT0xO1NlcmlhbE51bWJlcj0xMDAxO1N1cHBvcnRFeHBpcnlEYXRlPTA2LzE0LzIwMTcgMDA6MDA6MDA7VXNlTm9CcmFuZGluZz1GYWxzZTtDb250YWN0Rmlyc3ROYW1lPTtDb250YWN0TGFzdE5hbWU9GQwP4pqjgIkqQ3rkHBitUvrSkZA87Wf+QGXIW7F54n+Fnqh7gR8rfZy/oUnKKTGz";
                PlcConnection = Client.CreateConnection();
                PlcConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);

                return false;
            }
        }

        public void CalculateInformation(List<Tuple<int, int, int>> POI)
        {
            ScannerInformation info = new ScannerInformation();
            //info.GetNecessaryInformation(POI);
            //PlcConnection.Write(info);
        }

        //public void WriteDataToPlc(PlcInformationType type, object obj)
        //{
        //    if (type == PlcInformationType.PlcStatus)
        //    {
        //        DatablockObjects.PlcStatus status = new DatablockObjects.PlcStatus();
        //        status.ParseDataBlockInformation(obj);

        //        PlcConnection.Write(status);
        //    }
        //}

        public void Close()
        {
            LogFiler.Close();
        }
    }
}
