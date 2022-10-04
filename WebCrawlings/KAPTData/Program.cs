using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAPTData
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SiS.Framework.SmartClient.WinAppStyleManager.DefaultFont = new System.Drawing.Font("맑은 고딕", 9);
            Application.ThreadException += Application_ThreadException;
            Application.Run(new FrmMain());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            SiS.Framework.Win.WinMethod.ShowError(e.Exception);
        }
    }
}
