using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SiS.Service.Scraping.Common;

namespace SiS.Service.Scraping.Log
{
    /// <summary>
    /// 스크래핑 디버거 뷰어를 위한 관리 클래스 입니다. 
    /// </summary>
    public class SignalViewManager
    {
        private static System.Threading.Thread _WorkerThread;
        private static System.Threading.ThreadStart _ThreadStart;
        private static FrmScrapingSignal signalDlog;
        private static Scraping.Common.ScrapingLauncherManager _launcher = null;
        //private static WebBrowser _wb;

        /// <summary>
        /// 스크래핑 시그널 창을 호출 합니다. 
        /// </summary>
        public static void SignalCall()
        {
            if (_isOpened) return;
            InitThread();
        }

        /// <summary>
        /// 스크래핑 시그널 창을 호출 합니다. 
        /// </summary>
        public static void SignalCall(ScrapingLauncherManager launcher)
        {
            _launcher = launcher;
            if (_isOpened) return;
            InitThread();
        }

        /// <summary>
        /// 스크래핑 시그널 창을 닫습니다.
        /// </summary>
        public static void SignalClose()
        {
            if (signalDlog == null) return;            
            if (signalDlog.InvokeRequired)
            {
                MethodInvoker mi = new MethodInvoker(SignalClose);
                signalDlog.Invoke(mi);
            }
            else
            {
                if (signalDlog.IsDisposed) return;
                signalDlog.Close();
                signalDlog.Dispose();
                signalDlog = null;
            }
        }

        private static void InitThread()
        {
            //if (debugDlog != null)
            //{
            //    // 호출 취소
            //    DebugTraceShow();
            //    return;

            //}

            _ThreadStart = new System.Threading.ThreadStart(SignalWindowStart);
            _WorkerThread = new System.Threading.Thread(_ThreadStart);
            _WorkerThread.IsBackground = true;
            _WorkerThread.TrySetApartmentState(System.Threading.ApartmentState.STA);
            _WorkerThread.Start();
        }

        private static bool _isOpened = false;
        private static void SignalWindowStart()
        {
            if (signalDlog == null || signalDlog.IsDisposed)
            {
                signalDlog = new FrmScrapingSignal();
                signalDlog.FormClosed += Signal_FormClosed;
                _isOpened = true;
            }
            signalDlog.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(signalDlog);
        }

        static void Signal_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isOpened = false;
            (sender as Form).FormClosed -= Signal_FormClosed;
        }

    }
}
