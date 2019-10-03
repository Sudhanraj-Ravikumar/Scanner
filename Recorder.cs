using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Threading.Tasks;

namespace ScannerDisplay
{
    public enum DataType
    {
        Undef,
        Raw,
        Norm
    }

    public class Recorder
    {
        public Recorder(string scannerName)
        {
            Locker = new object();

            ScannerName = scannerName;

            LogFiler = new Logger("Recorder " + scannerName);

            CreateRecordDirectory();
        }

        private Logger LogFiler
        { get; set; }

        private StreamWriter RecordWriter
        { get; set; }

        private FileStream FileReader
        { get; set; }

        public List<Tuple<string, List<Tuple<int, int, int>>>> RecordData
        { get; set; }
        
        private object Locker
        { get; }

        private string RecordDirectory
        { get; set; }

        private string FilePath
        { get; set; }

        private string ScannerName
        { get; set; }

        private bool IsRecording
        { get; set; }

        private DateTime RecordTime
        { get; set; }

        public void StartRecording(Scanner sc)
        {
            RecordTime = DateTime.Now;

            RecordWriter = CreateStreamWriter();

            Subscribe(sc);
        }

        // this method subscribes the recorder to the scanner so that the normalized data can get archived
        private void Subscribe(Scanner sc)
        {
            sc.ScannerInputNormalized += new Scanner.ScannerDataInput(LogData);
        }

        public void StopRecording(Scanner sc)
        {
            Unsubscribe(sc);

            Dispose();
        }

        // this method unsubscribes the recorder from the scanner so that the normalized data will no longer be archived
        private void Unsubscribe(Scanner sc)
        {
            sc.ScannerInputNormalized -= LogData;
        }

        private void CreateRecordDirectory()
        {
            RecordDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Trim();

            if (RecordDirectory?[RecordDirectory.Length - 1] != '\\')
            {
                RecordDirectory += '\\';
            }

            RecordDirectory += "Recording\\";

            if (!Directory.Exists(RecordDirectory))
            {
                Directory.CreateDirectory(RecordDirectory);
            }
        }

        private void ExpandRecordDirectory()
        {
            RecordDirectory += DateTime.Now.ToString("yyyyMMdd_HHmm") + '\\';

            if (!Directory.Exists(RecordDirectory))
            {
                Directory.CreateDirectory(RecordDirectory);
            }
        }

        private StreamWriter CreateStreamWriter()
        {
            CreateRecordDirectory();
            ExpandRecordDirectory();

            FilePath = RecordDirectory + ScannerName + "_" + RecordTime.ToString("HHmm") + ".log";

            StreamWriter streamWriter = File.AppendText(FilePath);

            return streamWriter;
        }

        private void LogData(object sender, List<Tuple<int, int, int>> Data)
        {
            try
            {
                lock (Locker)
                {
                    if ((DateTime.Now - RecordTime).Days > 0)
                    {
                        if (RecordWriter != null)
                        {
                            Dispose();
                        }

                        RecordWriter = CreateStreamWriter();
                    }

                    if (RecordWriter != null)
                    {
                        string message = GetMessagefromList(Data);

                        string inputMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff;") + message;

                        RecordWriter.BaseStream.Seek(0, SeekOrigin.End);
                        RecordWriter.WriteLine(inputMessage);
                        RecordWriter.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }
        }

        private string GetMessagefromList(List<Tuple<int, int, int>> data)
        {
            string sMessage = string.Empty;

            try
            {
                foreach (var ok in data)
                {
                    sMessage = sMessage + $"{ok.Item1.ToString()};{ok.Item2.ToString()};{ok.Item3.ToString()};";
                }
            }
            catch (Exception ex)
            {
                LogFiler.Log(Category.Error, MethodBase.GetCurrentMethod().DeclaringType.Name + "_" + MethodBase.GetCurrentMethod().Name + ": " + ex.Message);
            }

            return sMessage;
        }

        public void GetRecordData(string directoryPath)
        {
            RecordData = new List<Tuple<string, List<Tuple<int, int, int>>>>();

            GetFile(directoryPath);
        }

        private void GetFile(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Path.GetFileName(files[i]);

                string[] file = fileName.Split('_');

                if (file[0] == ScannerName)
                {
                    GetFileInformation(files[i]);
                }
            }
        }

        private void GetFileInformation(string filePath)
        {
            string[] file = File.ReadAllLines(filePath);

            if (file.Length != 0)
            {
                GetIncludedData(file);
            }
        }

        private void GetIncludedData(string[] file)
        {
            DateTime start = DateTime.Now;
            
            List<Tuple<string, List<Tuple<int, int, int>>>> Data = new List<Tuple<string, List<Tuple<int, int, int>>>>();

            for (int i = 0; i < file.Length; i++)
            {
                string[] data = file[i].Split(';');

                Tuple<string, List<Tuple<int, int, int>>> dataset = IncludeData(data);

                Data.Add(dataset);
            }

            RecordData = Data;
        }

        private Tuple<string, List<Tuple<int, int, int>>> IncludeData(string[] dataset)
        {
            List<Tuple<int, int, int>> Data = new List<Tuple<int, int, int>>();

            string timestamp = dataset[0];

            for (int i = 1; i < dataset.Length - 1; i+=3)
            {
                try
                {
                    Data.Add(new Tuple<int, int, int>(int.Parse(dataset[i]), int.Parse(dataset[i + 1]), int.Parse(dataset[i + 2])));
                }
                catch (FormatException)
                {
                    //
                }
            }

            return new Tuple<string, List<Tuple<int, int, int>>>(timestamp, Data);
        }

        private string GetNewFilePath(string filepath)
        {
            string[] path = filepath.Split('.');

            string newPath = path[0] + "_" + (DateTime.Now.Minute - RecordTime.Minute) + "_minutes.log";

            return newPath;
        }

        private void Dispose()
        {
            if (RecordWriter != null)
            {
                RecordWriter.Close();
                RecordWriter.Dispose();
            }

            try
            {
                File.Move(FilePath, GetNewFilePath(FilePath));
            }
            catch (IOException)
            {
                //
            }
        }

        public void Close()
        {
            Dispose();

            if (LogFiler != null)
            {
                LogFiler.Close();
                LogFiler = null;
            }

            RecordWriter = null;
        }
    }
}
