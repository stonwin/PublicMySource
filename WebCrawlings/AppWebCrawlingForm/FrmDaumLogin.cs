﻿using OpenQA.Selenium.Chrome;
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
    public partial class FrmDaumLogin : Form
    {

        private ChromeDriver _driver = null;
        //private ChromeOptions _options = null;
        //private ChromeDriverService _driverService = null;

        public FrmDaumLogin()
        {
            InitializeComponent();

            this.Shown += FrmDaumLogin_Shown;
            this.FormClosing += FrmMain_FormClosing;
        }

        private void FrmDaumLogin_Shown(object sender, EventArgs e)
        {
            CreateChromeObject();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Dispose();
            }

            //_driverService.Dispose();
            //_options = null;
        }

        private void CreateChromeObject()
        {

            _driver = GlobalMethod.CreateChromeObject();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string id = txtUserID.Text;
            string pwd = txtPWD.Text;

            

            // https://logins.daum.net/accounts/signinform.do?url=https%3A%2F%2Fwww.daum.net%2F
            //_driver.Navigate().GoToUrl(@"https://www.daum.net");         // 사이트 접속 
            // _driver.Navigate().GoToUrl(@"https://logins.daum.net/accounts/signinform.do?url=https%3A%2F%2Fwww.daum.net%2F");         // 사이트 접속 
            //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // 로그인 버튼 XPath //*[@id="inner_login"]/a[1]
            var eleDaumLogin = _driver.FindElementByXPath("//*[@id=\"inner_login\"]/a[1]");
            eleDaumLogin.Click();

            Thread.Sleep(1000);

            // id 입력
            var eleId = _driver.FindElementByXPath("//*[@id=\"id_email_2\"]");
            eleId.SendKeys(id);

            // 암호 입력
            var elePwd = _driver.FindElementByXPath("//*[@id=\"id_password_3\"]");  // //*[@id=\"id_password_3\"]
            elePwd.SendKeys(pwd);

            // 로그인 버튼 클릭
            var eleBtnLogin = _driver.FindElementByXPath("//*[@id=\"login-form\"]/fieldset/div[8]/button[1]");  // //*[@id=\"login-form\"]/fieldset/div[8]/button[1]
            eleBtnLogin.Click();



            //
            
        }

        private void btnGoDaum_Click(object sender, EventArgs e)
        {
            _driver.Navigate().GoToUrl(@"https://www.daum.net");
        }
    }
}
