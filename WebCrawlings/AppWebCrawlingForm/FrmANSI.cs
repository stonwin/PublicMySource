using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppWebCrawlingForm
{
    public partial class FrmANSI : Form
    {
        public FrmANSI()
        {
            InitializeComponent();
            this.Shown += FrmANSI_Shown;
            this.FormClosing += FrmANSI_FormClosing;
        }

        private void FrmANSI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Dispose();
            }
        }

        private void FrmANSI_Shown(object sender, EventArgs e)
        {
            _driver = GlobalMethod.CreateChromeObject();
        }

        private ChromeDriver _driver = null;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //
            string userId = "skai1";
            string pwd = "dong**8634";

            _driver.Navigate().GoToUrl(@"https://www.ansi.or.kr/web/login/InsideLogin.asp");

            Thread.Sleep(1000);
            // id 입력
            var eleId = _driver.FindElementById("me_id");
            eleId.SendKeys(userId);

            //// 암호 입력
            //var elePwd = _driver.FindElementById("me_pass");
            //elePwd.SendKeys(pwd);

            //// 로그인 버튼 클릭
            //var eleBtnLogin = _driver.FindElementByXPath("//*[@id=\"loginBtn\"]");
            //eleBtnLogin.Click();

        }
    }
}
