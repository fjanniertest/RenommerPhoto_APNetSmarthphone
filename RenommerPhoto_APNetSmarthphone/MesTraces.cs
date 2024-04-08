using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace RenommerPhoto_APNetSmarthphone
{
    public class MesTraces : TraceListener
    {
        private static MesTraces mesTraces;
        TextWriterTraceListener textWriterTraceListener;

        /// <summary>
        /// Constructeur
        /// </summary>
        public MesTraces()
        {
            string strRepertoireLogs = System.Configuration.ConfigurationManager.AppSettings["LOGS"];
            strRepertoireLogs += DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
            if (File.Exists(strRepertoireLogs))
                File.Delete(strRepertoireLogs);
            System.IO.FileStream myTraceLog = null;
            if (!System.IO.File.Exists(strRepertoireLogs))
                myTraceLog = new System.IO.FileStream(strRepertoireLogs, System.IO.FileMode.OpenOrCreate);
            else
                myTraceLog = new System.IO.FileStream(strRepertoireLogs, System.IO.FileMode.Append);
            textWriterTraceListener = new TextWriterTraceListener(myTraceLog);
            // TODO : html !!!!
        }

        /// <summary>
        /// GetInstance
        /// </summary>
        /// <returns></returns>
        public static MesTraces GetInstance()
        {
            if (mesTraces == null)
            {
                mesTraces = new MesTraces();
            }
            return mesTraces;
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message)
        {
            message = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss : ") + message;
            textWriterTraceListener.Write(message);
            textWriterTraceListener.Flush();
        }

        /// <summary>
        /// WriteLine
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message)
        {
            message = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss : ") + message;
            textWriterTraceListener.WriteLine(message);
            textWriterTraceListener.Flush();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            textWriterTraceListener.Close();
            base.Dispose(disposing);
        }
    }


}
