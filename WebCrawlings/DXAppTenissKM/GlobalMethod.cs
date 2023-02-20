﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using System.Windows.Forms;
using System.Threading;

namespace DXAppTenissKM
{
    public static class GlobalMethod
    {

        //private static ChromeOptions _options = null;
        private static ChromeDriverService _driverService = null;

        static GlobalMethod()
        {
            _driverService = ChromeDriverService.CreateDefaultService(@"C:\chromedriver_win32");
            _driverService.HideCommandPromptWindow = true;
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (_driverService != null)
            {
                _driverService.Dispose();
            }
        }

        public static ChromeDriver CreateChromeObject(bool isHeadless = false)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("disable-gpu");
            if (isHeadless) options.AddArgument("headless");
            options.AddArgument("window-size=1920x1080");
            ChromeDriver driver = new ChromeDriver(_driverService, options);
            return driver;
        }

    }

}
