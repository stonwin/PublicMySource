using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 작업을 실행하고 진행상태를 표시 관리하는 클래스 입니다. 
    /// </summary>
    public class ScrapingLauncherManager
    {

     //   private System.Windows.Forms.WebBrowser m_web;

        // 스크래핑 작업 대기열
        private Queue<Scraping.Common.ScrapingArgumentBase> _scrapingQueue = new Queue<Scraping.Common.ScrapingArgumentBase>();

        // 스크래핑 아규먼트 리스트
        private List<ScrapingArgumentBase> _scrapingArgList = new List<ScrapingArgumentBase>();

        // 현재 작업중인 스크래핑 서비스
        private ScrapingServiceBase _CurrentService = null;

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        public ScrapingLauncherManager()
        {

        }

        #region 속성

        /// <summary>
        ///  작업 상태를 보여주는 시그널 창을 볼지 여부를 가져오거나 설정 한다. 
        /// </summary>
        public bool IsViewSignalWindow { get; set; }
        

        /// <summary>
        /// 남아있는 서비스(작업중인 서비스 제외)의 수를 리턴 합니다. 
        /// </summary>
        public int RemainServiceCount
        {
            get
            {
                return _scrapingQueue.Count;
            }
        }

        ///// <summary>
        ///// 웹 브라우저 컨트롤을 가져오거나 설정 합니다. 
        ///// </summary>
        //public System.Windows.Forms.WebBrowser WebBrowser
        //{
        //    get
        //    {
        //        return m_web;
        //    }
        //    set
        //    {
        //        if (m_web == value)
        //            return;
        //        m_web = value;
        //    }
        //}

        /// <summary>
        /// 남아있는 작업 항목(현재 작업중인 항목은 제외)의 수를 리턴 합니다. 
        /// (작업중인 서비스의 대기 중인 작업은 포함됨)
        /// </summary>
        public int RemainWorkItemCount
        {
            get
            {
                int remainCount = 0;
                if (CurrentService != null)
                {
                    remainCount += CurrentService.RemainingItemCount;
                }

                if (this.RemainServiceCount == 0) return remainCount;
                foreach (var arg in _scrapingQueue.ToArray())
                {
                    remainCount += arg.ScrapingItems.Count;
                }
                return remainCount;
            }
        }

        /// <summary>
        /// 현재 스크래핑 중인 서비스를 가져 옵니다. 
        /// </summary>
        public ScrapingServiceBase CurrentService
        {
            get
            {
                return _CurrentService;
            }
            private set
            {
                _CurrentService = value;
            }
        }
        #endregion

        #region public 메서드

        /// <summary>
        /// 남아있는 작업 항목 비율.
        /// </summary>
        public decimal RemainWorkItemPercentage()
        {
            int totalItemCount = 0;
            foreach (var arg in _scrapingArgList)
            {
                totalItemCount += arg.ScrapingItems.Count;
            }
            if (totalItemCount == 0) return (decimal)0;
            return Math.Round((decimal)(RemainWorkItemCount / totalItemCount), 2);
        }



        /// <summary>
        /// 스크래핑할 아규먼트 클래스를 추가 합니다. 
        /// </summary>
        /// <param name="arg">아규먼트 클래스 입니다.</param>
        public void AddArgmuent(ScrapingArgumentBase arg)
        {
            this._scrapingArgList.Add(arg);
        }

        /// <summary>
        ///  스크래핑 작업을 시작 합니다. 
        /// </summary>
        public void StartScraping()
        {

            if (this.IsViewSignalWindow)
            {
                Log.SignalViewManager.SignalCall(this);
            }


            ScrapingAbort = false;

            _scrapingQueue.Clear();
            foreach (ScrapingArgumentBase arg in _scrapingArgList)
            {
                _scrapingQueue.Enqueue(arg);
            }
            InqueryService();
        }

        /// <summary>
        /// 현재 실행 중인 스크래핑 작업을 취소 합니다. 
        /// </summary>
        public void AbortScraping()
        {
            // 스크래핑 작업 취소를 위해 ScrapingAbortRequest 변수의 값을 True 변경 합니다. 
            ScrapingAbort = true;
        }

        /// <summary>
        /// 현재 실행중인 스크래핑을 중단하도록 상태 값을 셋팅하거나 가져 옵니다. 
        /// </summary>
        internal static bool ScrapingAbort { get; set; }


        /// <summary>
        /// 스크래핑 작업을 위한 아규먼트를 모두 클리어 합니다. 
        /// </summary>
        public void ClearArgument()
        {
            _scrapingArgList.Clear();
        }

        #endregion

        #region private 메서드
        // 서비스 조회 요청
        private void InqueryService()
        {
            RemoveServiceEventHandler(this.CurrentService);
            ScrapingArgumentBase arg = null;
            if (_scrapingQueue.Count > 0)
            {
                arg = _scrapingQueue.Dequeue();
            }

            if (arg == null)
            {
                this.CurrentService = null;
                //AppendLogMessage("============================== 모든 서비스 호출 작업 완료 ====================================");
                OnLauncherWorkCompleted();
                return;
            }
            ScrapingServiceBase svc = arg.CreateScrapingServiceInstance();
            this.CurrentService = svc;
            AddServiceEventHandler(this.CurrentService);
            this.CurrentService.Inquiry(arg);

        }

        // 서비스 객체의 이벤트 핸들러를 제거 합니다. 
        private void RemoveServiceEventHandler(ScrapingServiceBase svc)
        {
            if (svc == null) return;
            svc.LoginProcessEnd -= LoginProcessEnd;
            svc.InquiryCompleted -= InquiryCompleted;
            svc.ScrapingItemWorkCompleted -= ScrapingItemWorkCompleted;
            svc.ScrapingBlockWorkCompleted -= ScrapingBlockWorkCompleted;
        }

        // 서비스 객체의 이벤트 핸들러를 추가 합니다.
        private void AddServiceEventHandler(ScrapingServiceBase svc)
        {
            svc.LoginProcessEnd += LoginProcessEnd;
            svc.InquiryCompleted += InquiryCompleted;
            svc.ScrapingItemWorkCompleted += ScrapingItemWorkCompleted;
            svc.ScrapingBlockWorkCompleted += ScrapingBlockWorkCompleted;
        } 
        #endregion

        #region 이벤트 처리
        /// <summary>
        /// 로그인 작업이 끝났을때 발생하는 이벤트 입니다. 
        /// </summary>
        public event LoginWorkCompletedEventHandler LoginProcessCompleted;
        /// <summary>
        /// 스크래핑의 블럭단위 작업이 끝나면 발생하는 이벤트 입니다. 블럭단위 작업은 특정 날짜 구간 또는 날짜 등 스크래핑의 최소 작업 단위가 됩니다.
        /// </summary>
        public event BlockWorkCompletedEventHandler BlockWorkCompleted;
        /// <summary>
        /// 항목의 작업이 끝나면 발생하는 이벤트 입니다. 
        /// </summary>
        public event ItemWorkCompletedEventHandler ItemWorkCompleted;
        /// <summary>
        /// 서비스의 작업이 끝나면 발생하는 이벤트 입니다. 
        /// </summary>
        public event ServiceWorkCompletedEventHandler ServiceWorkCompleted;
        /// <summary>
        /// 런처의 작업이 끝나면 발생하는 이벤트 입니다. 
        /// </summary>
        public event ScrapingLauncherWorkCompletedEventHandler LauncherWorkCompleted;

        private void OnLauncherWorkCompleted()
        {
            if (this.IsViewSignalWindow)
            {
                Log.SignalViewManager.SignalClose();
            }
            if (LauncherWorkCompleted != null)
            {
                // 런처 작업 완료 이벤트 발생
                ScrapingLauncherWorkCompletedEventArgs arg = new ScrapingLauncherWorkCompletedEventArgs(ScrapingLauncherManager.ScrapingAbort);
                ScrapingLauncherManager.ScrapingAbort = false;
                if (arg.IsAbort)
                {                  
                    Log.LogWriter.WriteLog(this.GetType().ToString(), Enums.LogLevel.Information, "중단요청에 의한 런처 작업 취소");
                }
                LauncherWorkCompleted(this, arg);
            }
        }

        private void LoginProcessEnd(object s, LoginProcessEndEventArgs e)
        {
            // 스크래핑 작업 중단 체크
            if (ScrapingLauncherManager.ScrapingAbort)
            {
                throw new ScrapingAbortException("스크래핑 작업 중단 요청으로 중단되었습니다.");
            }
            if (LoginProcessCompleted != null)
            {
                // 로그인 프로세스 완료 이벤트 발생
                LoginProcessCompleted(this, e);
            }
        }

        private void ScrapingBlockWorkCompleted(object s, ScrapingBlockWorkCompletedEventArgs e)
        {
            // 스크래핑 작업 중단 체크
            if (ScrapingLauncherManager.ScrapingAbort)
            {
                throw new ScrapingAbortException("스크래핑 작업 중단 요청으로 중단되었습니다.");
            }
            if (BlockWorkCompleted != null)
            {
                // 블럭작업 완료 이벤트 발생
                BlockWorkCompleted(this, e);
            }
        }

        private void ScrapingItemWorkCompleted(object s, ScrapingItemWorkCompletedEventArgs e)
        {
            // 스크래핑 작업 중단 체크
            if (ScrapingLauncherManager.ScrapingAbort)
            {
                throw new ScrapingAbortException("스크래핑 작업 중단 요청으로 중단되었습니다.");
            }
            if (ItemWorkCompleted != null)
            {
                // 항목작업 완료 이벤트 발생
                ItemWorkCompleted(this, e);
            }
        }

        private void InquiryCompleted(object s, InquiryCompletedEventArgs e)
        {
            // 스크래핑 작업 중단 체크
            if (ScrapingLauncherManager.ScrapingAbort)
            {
                // 런처 완료 시키고 다음 스크래핑 작업 하지 않음.
                OnLauncherWorkCompleted();
                return;
            }
            if (ServiceWorkCompleted != null)
            {               
                // 서비스 작업 완료 이벤트 발생
                ServiceWorkCompleted(this, e);
            }

            // 다음 서비스 작업 호출(대기열에 서비스가 있는 경우만 계속 실행)
            InqueryService();

        } 
        #endregion

    }

    #region 재정의된 이벤트 핸들러
    ///// <summary>
    ///// 로그인 작업이 끝나고 나서 발생하는 이벤트에 대한 아규먼트 클래스 입니다. 
    ///// </summary>
    //public class LoginWorkCompleted : LoginProcessEndEventArgs
    //{

    //}

    /// <summary>
    /// 로그인 작업이 끝나고 나서 발생하는 이벤트의 핸들러 대리자 입니다. 
    /// </summary>
    /// <param name="s">이벤트가 발행한 소스 입니다.</param>
    /// <param name="e">발행한 이벤트의 아규먼트 클래스 입니다.</param>
    public delegate void LoginWorkCompletedEventHandler(object s, LoginProcessEndEventArgs e);

    ///// <summary>
    ///// 블럭의 작업이 끝나고 나서 발생하는 이벤트에 대한 아규먼트 클래스 입니다. 
    ///// </summary>
    //public class BlockWorkCompleted : ScrapingBlockWorkCompletedEventArgs
    //{

    //}
    /// <summary>
    /// 블럭의 작업이 끝나고 나서 발생하는 이벤트의 핸들러 대리자 입니다. 
    /// </summary>
    /// <param name="s">이벤트가 발행한 소스 입니다.</param>
    /// <param name="e">발행한 이벤트의 아규먼트 클래스 입니다.</param>
    public delegate void BlockWorkCompletedEventHandler(object s, ScrapingBlockWorkCompletedEventArgs e);

    ///// <summary>
    ///// 항목의 작업이 끝나고 나서 발생하는 이벤트에 대한 아규먼트 클래스 입니다. 
    ///// </summary>
    //public class ItemWorkCompleted : ScrapingItemWorkCompletedEventArgs
    //{

    //}
    /// <summary>
    /// 항목의 작업이 끝나고 나서 발생하는 이벤트의 핸들러 대리자 입니다. 
    /// </summary>
    /// <param name="s">이벤트가 발행한 소스 입니다.</param>
    /// <param name="e">발행한 이벤트의 아규먼트 클래스 입니다.</param>
    public delegate void ItemWorkCompletedEventHandler(object s, ScrapingItemWorkCompletedEventArgs e);

    ///// <summary>
    ///// 서비스의 작업이 끝나고 나서 발생하는 이벤트에 대한 아규먼트 클래스 입니다. 
    ///// </summary>
    //public class ServiceWorkCompleted : InquiryCompletedEventArgs
    //{ 

    //}
    /// <summary>
    /// 서비스의 스크래핑 작업이 끝나고 나서 발생하는 이벤트의 핸들러 대리자 입니다. 
    /// </summary>
    /// <param name="s">이벤트가 발행한 소스 입니다.</param>
    /// <param name="e">발행한 이벤트의 아규먼트 클래스 입니다.</param>
    public delegate void ServiceWorkCompletedEventHandler(object s, InquiryCompletedEventArgs e);
    #endregion

} 
    
