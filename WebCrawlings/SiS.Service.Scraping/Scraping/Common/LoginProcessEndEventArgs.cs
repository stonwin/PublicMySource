using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 로그인 프로세스가 끝났을때 발생하는 이벤트의 아규먼트 클래스 입니다. 
    /// </summary>
    public class LoginProcessEndEventArgs : System.EventArgs
    {
        
        /// <summary>
        /// 인증에 성공했는지 여부를 가져오거나 설정 합니다.
        /// </summary>
        public bool IsAuthented { get; private set; }
        /// <summary>
        /// 조회에 사용된 아규먼트 객체를 가져오거나 설정 합니다.
        /// </summary>
        public ScrapingArgumentBase ScrapingArgument { get; private set; }
        /// <summary>
        /// 로그인과 관련한 상태 정보를 가져오거나 설정 합니다.
        /// </summary>
        public object State { get; private set; }
        //public string Message { get; set; }
        /// <summary>
        /// 로그인시 예외가 발생한 경우 예외 객체를 가져오거나 설정 합니다.
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// 에러 발생 여부를 가져 옵니다.
        /// </summary>
        public bool IsError { get; private set; }
        private Enums.LoginNextProcess _nextProcess = Enums.LoginNextProcess.DataProcess;
        /// <summary>
        /// 로그인 이후 다음 프로세스를 가져오거나 설정 합니다.
        /// </summary>
        public Enums.LoginNextProcess NextProcess
        {
            get
            {
                return _nextProcess;
            }
            internal set
            {
                if (_nextProcess == value) return;
                _nextProcess = value;
            }
        }

        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        /// <param name="isAuthented">인증에 성공했는지 여부를 지정 합니다.</param>
        /// <param name="arg">아규먼트 객체 입니다.</param>
        public LoginProcessEndEventArgs(bool isAuthented, ScrapingArgumentBase arg)
        {
            this.IsAuthented = isAuthented;
            this.ScrapingArgument = arg;
            
        }

        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        /// <param name="isAuthented">인증에 성공했는지 여부를 지정 합니다.</param>
        /// <param name="arg">아규먼트 객체 입니다.</param>
        /// <param name="state">로그인과 관련한 추가적인 상태정보를 지정 합니다.</param>
        public LoginProcessEndEventArgs(bool isAuthented, ScrapingArgumentBase arg, object state)
        {
            this.IsAuthented = isAuthented;
            this.ScrapingArgument = arg;
            this.State = state;
        }

        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        /// <param name="isAuthented">인증에 성공했는지 여부를 지정 합니다.</param>
        /// <param name="arg">아규먼트 객체 입니다.</param>
        /// <param name="state">로그인과 관련한 추가적인 상태정보를 지정 합니다.</param>
        /// <param name="error">로그인시 예외가 발생한 경우 지정 합니다.</param>
        /// <param name="nextProcess">로그인 후 다음 처리과정을 지정 합니다.</param>
        public LoginProcessEndEventArgs(bool isAuthented, ScrapingArgumentBase arg, object state, Exception error, Enums.LoginNextProcess nextProcess = Enums.LoginNextProcess.DataProcess)
        {
            this.IsAuthented = isAuthented;
            this.ScrapingArgument = arg;
            this.State = state;
            //this.Message= msg;
            this.Error = error;
            if (this.Error != null)
            {
                this.IsError = true;
                this.IsAuthented = false;
            }
            this.NextProcess = nextProcess; 
        }

    }

    /// <summary>
    /// 로그인 프로세스가 끝났을때 사용되는 이벤트 핸들러 입니다.
    /// </summary>
    /// <param name="s">이벤트가 발생한 소스 입니다.</param>
    /// <param name="e">이벤트에 필요한 아규먼트 클래스 입니다.</param>
    public delegate void LoginProcessEndEventHandler(object s, LoginProcessEndEventArgs e);
}
