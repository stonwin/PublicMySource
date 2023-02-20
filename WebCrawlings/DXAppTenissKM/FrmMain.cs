using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;

namespace DXAppTenissKM
{
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {

        private ChromeDriver _driver = null;

        private string _userId = "stonwin";
        private string _userPwd = "gmlwjd4001*";
        private int _waitSeconds = 2;



        public FrmMain()
        {
            InitializeComponent();
            this.Shown += FrmDaumLogin_Shown;
            this.FormClosing += FrmMain_FormClosing;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Dispose();
            }
        }

        private void FrmDaumLogin_Shown(object sender, EventArgs e)
        {
            
        }

        private void CreateChromeObject()
        {

            _driver = GlobalMethod.CreateChromeObject();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_waitSeconds);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // 객체 생성. 
            CreateChromeObject();
        }


        private void MoveLogin()
        {
            //# 보안문자 없는 페이지.
            string loginUrl = "https://www.gmuc.co.kr/user/login/login.do";
            _driver.Navigate().GoToUrl(loginUrl);

            //# 로그인 정보 입력. 
            var idText = _driver.FindElement(OpenQA.Selenium.By.Id("userId"));
            var pwdText = _driver.FindElement(OpenQA.Selenium.By.Id("userPw"));
            idText.SendKeys(_userId);
            pwdText.SendKeys(_userPwd);    

            var btnLogin = _driver.FindElement(OpenQA.Selenium.By.Id("loginBtn"));
            btnLogin.Click();



        }

        private void MoveCourtPage()
        {
            //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // 테니스장 이동 .... 
            string goUrl = "https://www.gmuc.co.kr/user/conn/tennisIntro.do";
            _driver.Navigate().GoToUrl(goUrl);
            //WebDriverWait
            //WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));


            var btnMoveTenissPage = _driver.FindElement(By.CssSelector("a.btn_w"));
            //var btnMoveTeniss = wait.Until()
            btnMoveTenissPage.Click();

            // 창 포커스 변경
            _driver.SwitchTo().Window(_driver.WindowHandles[1]); // .switch_to.window(driver.window_handles[1])
            //var body = _driver.FindElement(By.TagName("body"));
            //rechtml.Text = body.Text;

        }

        private void MoveBookingPgae()
        {
            string gourl = "https://reserve.gmuc.co.kr/user/tennis/tennisMain.do?menuFlag=T";
            //driver.implicitly_wait(waitingTime)
            _driver.Navigate().GoToUrl(gourl);


            //# 관내 예약 클릭.
            var resbtn1 = _driver.FindElement(By.CssSelector("button.btn_res1'"));

            //# xPath로 찾기.
            //#resbtn1 = driver.find_element_by_xpath('/html/body/section/section/section/section/div/div[1]/a[2]/button')
            resbtn1.Click();

            //cookies = GetCookies(driver)
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 로그인 페이지 로딩. 
            MoveLogin();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 테니스 코트페이지 이동.
            MoveCourtPage();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 객체 생성. 
            CreateChromeObject();

            // 로그인 페이지 로딩. 
            MoveLogin();

            // 테니스 예약페이지 이동.
            MoveCourtPage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 예약페이지.
            this.MoveBookingPgae();
        }
    }
}
