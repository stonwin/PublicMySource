using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Data;
using SiS.Service.Scraping.Log;

namespace SiS.Service.Scraping.Common
{

    /// <summary>
    /// 스크래핑 서비스를 위한 베이스 클래스 입니다. 파생클래스를 반드시 구현해서 사용해야 합니다. 
    /// </summary>
    public abstract class ScrapingServiceBase : IScrapingService, IDisposable
    {
        /// <summary>
        /// 서비스의 호출 상태에 대한 열거형 값
        /// </summary>
        protected enum ServiceExecuteState
        {
            /// <summary>
            /// 통상 호출 상태, 기본값으로 로그인 부터 조회 로그아웃 까지 일괄 처리
            /// </summary>
            Batch = 0,
            /// <summary>
            /// 로그인과 로그아웃의 프로세스로 동작 한다. 
            /// </summary>
            LoginTest = 1,
            /// <summary>
            /// 로그인 작업만 수행 한다. 
            /// </summary>
            LoginOnly = 2,
            /// <summary>
            /// 조회 작업만 수행 한다. 
            /// </summary>
            RetrieveOnly=3

        }
        /// <summary>
        /// 서비스의 현재 호출 상태를 나타 낸다.
        /// </summary>
        protected ServiceExecuteState ServiceState { get; set; }

        /// <summary>
        /// 이 서비스의 라이센스가 유효한지를 가져오거나 설정 합니다.
        /// </summary>
        public bool IsValidLicence { get; internal set; }
        
        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        protected ScrapingServiceBase()
        {
            this.Helper.Cookie = new System.Net.CookieContainer();
            this.WorkTime = DateTime.Now;
            // 기본값은 배치로 설정
            this.ServiceState = ServiceExecuteState.Batch;
            this.IsValidLicence = false;
        }

        #region 속성 
        private ScrapingArgumentBase _scrapArgument;
        /// <summary>
        /// 현재 스크래핑 중인 아규먼트 입니다.
        /// </summary>
        public ScrapingArgumentBase CurrentArgument
        {
            get
            {
                return _scrapArgument;
            }
            protected set
            {
                if (_scrapArgument == value) return;
                _scrapArgument = value;
            }
        }

        private ScrapingItemBase _scrapItem;
        /// <summary>
        /// 현재 스크래핑 중인 아이템 입니다.
        /// 파생 클래스에서 이속성에 적절한 셋팅이 필요 합니다.
        /// </summary>
        public ScrapingItemBase CurrentItem
        {
            get
            {
                return _scrapItem;
            }
            protected set
            {
                if (_scrapItem == value) return;
                _scrapItem = value;
            }
        }

        /// <summary>
        /// 서비스를 설명하는 이름을 설정하거나 가져 옵니다. 파생 클래스에서 구현해야 합니다.
        /// </summary>
        public abstract string ServiceName { get; }

        private Queue<ScrapingItemBase> _scrapingItemQueue = new Queue<ScrapingItemBase>();
        /// <summary>
        /// 작업할 ScrapingItem을 대기열에서 가져 옵니다. 
        /// </summary>
        /// <returns>ScrapingItem을 가져 옵니다.</returns>
        protected ScrapingItemBase GetScrapingItemFromQueue()
        {
            if (_scrapingItemQueue.Count == 0) return null;
            return _scrapingItemQueue.Dequeue();
        }

        /// <summary>
        /// 남아 있는 스크래핑 아이템의 숫자를 가져오도록 반드시 구현 합니다. 파생 클래스에서 구현해야 합니다.
        /// </summary>
        public virtual int RemainingItemCount
        {
            get
            {
                return _scrapingItemQueue.Count;
            }
        }

        /// <summary>
        /// 스크래핑 항목의 대기열을 다시 구성 합니다. (기존 항목을 모두 클리어 하고 다시 만듦.
        /// </summary>
        protected void MakeScrapingItemQueue()
        {
            _scrapingItemQueue.Clear();
            // 작업 대상이 되는 스크래핑 항목을 대기열에 놓는다.
            foreach (var item in this.CurrentArgument.ScrapingItems)
            {
                _scrapingItemQueue.Enqueue(item);
            }
        }

        private ScrapingHelper _Helper = null;
        /// <summary>
        /// 스크래핑에 필요한 핼퍼 메서드를 제공하는 객체를 가져오거나 설정 합니다.
        /// </summary>
        public ScrapingHelper Helper
        {
            get
            {
                if (_Helper == null)
                {
                    _Helper = ScrapingHelper.GetHelperInstance();
                }
                return _Helper;
            }
        }

        /// <summary>
        /// 작업 시간을 가져오거나 설정 합니다.
        /// </summary>
        public DateTime WorkTime { get; set; }

        #endregion

        #region 표준 프로세스 처리를 위한 메서드 
        // 01 초기화
        // 02 로그인 프로세스
        // 03 데이터 작업 프로세스 (기본 조회 작업)
        // 04 로그아웃
        // 05 완료 처리 ==> 웹 브라우저 방식인 경우 비동기로 진행되기 때문에 OnLoginProcessEnd 메서드를 재정의 해서 순서에 맞게 다시 구현한다.

        /// <summary>
        /// 스크래핑 서비스의 초기화 작업을 수행 하도록 구현 합니다. 
        /// 기본 설정 및 보안 객체 관련 설정을 여기에서 재정의 후 구현 합니다. 
        /// </summary>
        protected virtual void ScrapingInitialize()
        {
            // 여기에 초기화 작업을 수행하도록 재정의 한 코드를 작성 합니다.
        }

        /// <summary>
        /// 로그인 처리 프로세스 입니다. 
        /// 로그인 프로세스에서는 반드시 LoginProcessEnd 이벤트를 발생(OnLoginProcessEnd 메서드 호출)시키고 인증 성공 여부를 
        /// 이벤트에 알린다.
        /// </summary>
        /// <returns>로그인 처리가 정상적으로 완료된 경우 tru를 리턴 합니다.</returns>
        protected abstract void LoginProcess();

        /// <summary>
        /// 로그인 프로세스가 완료되면 발생하는 이벤트 입니다. 
        /// </summary>
        public event LoginProcessEndEventHandler LoginProcessEnd;
        /// <summary>
        /// 로그인 프로세스 종료(LoginProcessEnd) 이벤트를 호출 합니다. 
        /// 웹브라우저 컨트롤을 이용한 스크래핑 작업을 할 경우 이 메서드를 재정의 해서 조회 처리를 해야 합니다.
        /// </summary>
        /// <param name="arg">로그인 프로세스 종료 이벤트의 아규먼트 클래스 입니다.</param>
        protected virtual void OnLoginProcessEnd(LoginProcessEndEventArgs arg)
        {


            if (arg.IsAuthented) this.WriteLog("로그인 성공", Enums.LogLevel.Information);

                switch (this.ServiceState)
                {
                    case ServiceExecuteState.LoginTest:     // 로그인 테스트 프로세스
                        // 로그인 테스트만 할 것이기 때문에 InquiryCompleted 이벤트는 발생시키지 않는다. 
                        arg.NextProcess = Enums.LoginNextProcess.Logout;
                        // 로그인 종료 이벤트를 발생 시킨다. 
                        // 로그인 완료 이벤트가 null인지 여부를 체크
                        if (LoginProcessEnd != null) LoginProcessEnd(this, arg);
                        try
                        {
                            //로그인 되어 있는 경우만 로그아웃
                            if (arg.IsAuthented)
                            {
                                this.WriteLog("로그 아웃", Enums.LogLevel.Information);
                                this.LogOut();
                            }

                        }
                        catch (Exception ex)
                        {
                            ScrapingException sex = new ScrapingException("로그아웃 중 예외 발생 " + ex.Message, ex);
                            arg = new LoginProcessEndEventArgs(arg.IsAuthented, arg.ScrapingArgument, null, sex, arg.NextProcess);

                        }

                        break;
                        // ==================== 로그인 테스트 여기까지 ========================================================


                    case ServiceExecuteState.Batch:     // 전체 일괄 작업 프로세스(로그인-->데이터처리작업-->로그아웃)
                        // 정상 조회 과정 진행
                        arg.NextProcess = Enums.LoginNextProcess.DataProcess;
                        // 로그인 종료 이벤트를 발생 시킨다. 
                        // 로그인 완료 이벤트가 null인지 여부를 체크
                        if (LoginProcessEnd != null) LoginProcessEnd(this, arg);
                        try
                        {
                            // 조회와 로그아웃 비정상 실행 시 예외 처리 
                            if (arg.IsAuthented && !arg.IsError)
                            {
                                // 조회
                                this.WriteLog("항목 작업 시작", Enums.LogLevel.Information);
                                this.DataWorkProcess();

                                this.WriteLog("로그 아웃", Enums.LogLevel.Information);
                                this.LogOut();
                            }

                            if (arg.IsError)
                            {
                                throw arg.Error;
                            }

                        }
                        catch (ScrapingAbortException abortEX)
                        {
                            // 작업 중단 예외 로그는 기록 하지 않는다. 
                            InquiryCompletedEventArgs inqueryErrArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource, abortEX);
                            OnInquiryCompleted(inqueryErrArg);
                            return;
                        }
                        catch (Exception ex)
                        {
                            // 예외 발생으로 작업 완료
                            this.WriteError(ex);
                            InquiryCompletedEventArgs inqueryErrArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource, ex);
                            OnInquiryCompleted(inqueryErrArg);
                            return;
                        }

                        // 작업 정상 완료
                        this.WriteLog("서비스 작업 완료", Enums.LogLevel.Information);
                        InquiryCompletedEventArgs inqueryArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource);
                        OnInquiryCompleted(inqueryArg);
                        break;

                    case ServiceExecuteState.LoginOnly:
                        // 로그인 프로세스만 처리 이후의 데이터처리 및 로그 아웃 과정은 처리 하지 않음.
                        arg.NextProcess = Enums.LoginNextProcess.StandBy;

                        try
                        {

                            // 로그인 종료 이벤트를 발생 시킨다.  
                            // 로그인 완료 이벤트가 null인지 여부를 체크
                            if (LoginProcessEnd != null) LoginProcessEnd(this, arg); LoginProcessEnd(this, arg);

                        }
                        catch (Exception ex)
                        {
                            ScrapingException sex = new ScrapingException("예외 발생 " + ex.Message, ex);
                            arg = new LoginProcessEndEventArgs(arg.IsAuthented, arg.ScrapingArgument, null, sex, arg.NextProcess);

                        }

                        break;

                    case ServiceExecuteState.RetrieveOnly:
                        // 조회만 : 이 블럭은 처리할 내용이 없음.
                        break;
                }

            
        }

        /// <summary>
        /// 데이터 처리 작업을 위한 위한 코드를 구현 한다. 대기열에 있는 ScrapingItem들을 대상으로 조회 호출 한다. 
        /// 표준에서 벗어나는 형태인 경우 이 메서드를 재정의 한다. 
        /// </summary>
        protected virtual void DataWorkProcess()
        {
            if (this.RemainingItemCount > 0)
            {
                this.CurrentItem = this.GetScrapingItemFromQueue();
            }
            else
            {
                this.CurrentItem = null;
            }

            if (this.CurrentItem != null)
            {

                // 항목에 대한 공통 작업 메서드.
                //DataWorkHeadProcess(this.CurrentItem);

                // 조회 작업인지 푸쉬 작업 인지 구분 호출
                if (this.CurrentItem.ItemDataWorkType == Enums.ItemDataWorkType.Retrieve)
                {
                    this.WriteLog(">>조회블럭시작", Enums.LogLevel.Information);
                    this.ItemDataRetrieve(this.CurrentItem);
                }
                else
                {
                    this.WriteLog(">>전송블럭시작", Enums.LogLevel.Information);
                    this.ItemDataPush(this.CurrentItem);
                }

               
            }
        }

        ///// <summary>
        ///// Item 단위로 DataWorkProcess의 앞 쪽 작업 필요 한 경우 이 메서드를 재정의 해서 사용 한다. 
        ///// ItemDataRetrieve 또는 ItemDataPush 메서드 보다 앞서 실행되며 Item 단위로 1회 선 실행 작업이 필요한 경우 이 메서드를 재정의 해서 사용 한다. 
        ///// </summary>
        ///// <param name="scrapingItemBase"></param>
        //protected virtual void DataWorkHeadProcess(ScrapingItemBase scrapingItemBase)
        //{
        //    // 이 메서드는 재정의 후 사용 한다. 

        //}

        /// <summary>
        /// 데이터처리 작업만 처리하는 메서드(로그아웃 과정은 진행하지 않는다.)
        /// </summary>
        public virtual void DataWorkProcessOnly()
        {
            // 
            this.ServiceState = ServiceExecuteState.RetrieveOnly;
            try
            {

                this.MakeScrapingItemQueue();
                // DataWorkProcess는 내부적으로 item에 대해 모두 작업이 끝날때 까지 반복 실행 됨
                this.WriteLog("항목 작업 시작", Enums.LogLevel.Verbose);
                this.DataWorkProcess();

            }
            catch (ScrapingAbortException abortEx)
            {
                // 예외 로그 없이 
                InquiryCompletedEventArgs inqueryErrArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource, abortEx);
                OnInquiryCompleted(inqueryErrArg);
                return;
            }
            catch (Exception ex)
            {
                // 예외 발생으로 작업 완료
                this.WriteError(ex);
                InquiryCompletedEventArgs inqueryErrArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource, ex);
                OnInquiryCompleted(inqueryErrArg);
                return;
            }

            // 작업 정상 완료
            this.WriteLog("서비스 작업 완료", Enums.LogLevel.Information);
            InquiryCompletedEventArgs inqueryArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource);
            OnInquiryCompleted(inqueryArg);

        }


        /// <summary>
        /// ScrapingItem 별로 조회 관련 처리를 위한 코드를 구현 한다. 
        /// OnScrapingItemWorkCompleted 메서드를 이용해 ScrapingItemWorkCompleted 이벤트 호출이 되도록 필요한 코드를 구현 해야 한다. 
        /// 또한 조회가 완료된 경우 ScrapingDataSource에 데이터를 담을 수 있도록 처리한다. 
        /// </summary>
        /// <param name="scrapItem">조회할 스크래핑 아이템 입니다. CurrentItem과 동일한 객체를 가르킵니다.</param>
        protected virtual void ItemDataRetrieve(ScrapingItemBase scrapItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ScrapingItem별로 데이터 푸쉬(업로드, 전송) 작업을 위한 코드를 구현 한다. 
        /// OnScrapingItemWorkCompleted 메서드를 이용해 ScrapingItemWorkCompleted 이벤트 호출이 되도록 필요한 코드를 구현 해야 한다. 
        /// 또한 작업이 완료된 경우 ScrapingDataSource에 데이터를 담을 수 있도록 처리한다.
        /// </summary>
        /// <param name="scrapItem">작업용 항목</param>
        protected virtual void ItemDataPush(ScrapingItemBase scrapItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 파생 클래스에서 이 메서드를 재정의 하고 로그아웃과 관련한 코드를 작성 한다. 
        /// </summary>
        public virtual void LogOut()
        {
            // 파생클래스에서 재정의 하고 여기에 로그아웃을 위한 코드를 작성 한다. 

        }
        
        #endregion


        #region IScrapingService 멤버


        /// <summary>
        /// ScrapingService 에 스크래핑 작업을 시작하도록 요창 한다. (예외 발생시 처리를 외부에서 하도록 한다.)
        /// </summary>
        /// <param name="args"></param>
        public void Inquiry(ScrapingArgumentBase args)
        {

            try
            {


                // 초기화 --> 로그인 (이후의 로그인 프로세서 종료 --> 조회(또는 푸시 작업) --> 로그아웃 --> 서비스 완료 프로세스는 ServiceState의 값에 따라 결정)을 호출하고
                // 일괄 작업으로 완료되도록 ServiceExecuteState.Batch 상태로 설정
                // 로그인 부터 조회 로그아웃 까지 일과 배치 상태 임을 표시함.
                this.ServiceState = ServiceExecuteState.Batch;
                this.WriteLog("작업 호출", Enums.LogLevel.Information);
                this.CurrentArgument = args;
                // 항목 대기열 생성
                this.MakeScrapingItemQueue();

                // 라이센스 체크
                if (this.IsValidLicence == false)
                {
                    // 라이센스가 유효하지 않음. 
                    throw new ScrapingException("라이센스가 유효하지 않습니다.");
                }

                this.WriteLog("초기화 시작", Enums.LogLevel.Information);
                this.ScrapingInitialize();

                this.WriteLog("로그인 시작", Enums.LogLevel.Information);
                
                this.LoginProcess();

                if (args.LoginType == Enums.LoginType.None)
                {
                    // 로그인 과정이 없으므로 로그인 완료 처리
                    LoginProcessEndEventArgs loginArg = new LoginProcessEndEventArgs(true, CurrentArgument);
                    OnLoginProcessEnd(loginArg);
                }
            }
            catch (ScrapingAbortException abortEx)
            {
                // 예외 로그 없이 
                InquiryCompletedEventArgs inqueryErrArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource, abortEx);
                OnInquiryCompleted(inqueryErrArg);
            }
            catch (Exception ex)
            {
                // 예외 발생 ==> 작업 완료이벤트에 예외 정보를 넣어서 전달 한다. 
                this.WriteError(ex);
                InquiryCompletedEventArgs inqueryErrArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource, ex);
                OnInquiryCompleted(inqueryErrArg);
            }


        }


        /// <summary>
        /// 인증 테스트를 합니다. 호출 후 LoginProcessEnd 이벤트를 통해 인증 여부를 확인 할 수 있습니다.
        /// </summary>
        /// <param name="arg">인증되는지를 테스트할 아규먼트 객체</param>
        public bool LoginTest(ScrapingArgumentBase arg)
        {
            try
            {
                // 상태 로그인 테스트로 설정
                this.ServiceState = ServiceExecuteState.LoginTest;
                this.WriteLog( "로그인 테스트 호출", Enums.LogLevel.Information);
                _scrapArgument = arg;

                MakeScrapingItemQueue();

                this.CurrentItem = this.GetScrapingItemFromQueue();
                //초기화 --> 로그인 프로세스 --> 로그인 종료 이벤트 --> 로그아웃
                this.WriteLog("초기화 시작", Enums.LogLevel.Information);
                this.ScrapingInitialize();
                this.WriteLog("로그인 시작", Enums.LogLevel.Information);
                this.LoginProcess();
                return true;
            }
            catch (ScrapingAbortException abortEx)
            {
                // 예외 로그 없이 
                LoginProcessEndEventArgs loginArg = new LoginProcessEndEventArgs(false, this.CurrentArgument, null, abortEx, Enums.LoginNextProcess.Logout);
                OnLoginProcessEnd(loginArg);
                return false;
            }
            catch (Exception ex)
            {
                // 예외 발생 ==> 작업 완료이벤트에 예외 정보를 넣어서 전달 한다. 
                this.WriteError(ex);
                LoginProcessEndEventArgs loginArg = new LoginProcessEndEventArgs(false, this.CurrentArgument, null, ex, Enums.LoginNextProcess.Logout);
                OnLoginProcessEnd(loginArg);
                //InquiryCompletedEventArgs inqueryErrArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource, ex);
                //OnInquiryCompleted(inqueryErrArg);
                return false;
            }
            

        }

        /// <summary>
        /// 로그인 과정만 수행 한다. 
        /// </summary>
        /// <param name="arg">로그인 과정을 수행할 아규먼트 객체</param>
        public bool LoginOnly(ScrapingArgumentBase arg)
        {
            try
            {
                // 상태 로그인 Only로 설정
                this.ServiceState = ServiceExecuteState.LoginOnly;
                this.WriteLog("로그인 Only 호출", Enums.LogLevel.Information);
                _scrapArgument = arg;

                MakeScrapingItemQueue();

                this.CurrentItem = this.GetScrapingItemFromQueue();
                //초기화 --> 로그인 프로세스 --> 로그인 종료 이벤트 --> 로그아웃
                this.WriteLog("초기화 시작", Enums.LogLevel.Information);
                this.ScrapingInitialize();
                this.WriteLog("로그인 시작", Enums.LogLevel.Information);
                this.LoginProcess();
                return true;
            }
            catch (ScrapingAbortException abortEx)
            {
                // 예외 로그 없이 
                LoginProcessEndEventArgs loginArg = new LoginProcessEndEventArgs(false, this.CurrentArgument, null, abortEx, Enums.LoginNextProcess.Logout);
                OnLoginProcessEnd(loginArg);
                return false;
            }
            catch (Exception ex)
            {
                // 예외 발생 ==> 작업 완료이벤트에 예외 정보를 넣어서 전달 한다. 
                this.WriteError(ex);
                LoginProcessEndEventArgs loginArg = new LoginProcessEndEventArgs(false, this.CurrentArgument, null, ex, Enums.LoginNextProcess.Logout);
                OnLoginProcessEnd(loginArg);
                return false;
                //InquiryCompletedEventArgs inqueryErrArg = new InquiryCompletedEventArgs(this.CurrentArgument, this.ScrapingDataSource, ex);
                //OnInquiryCompleted(inqueryErrArg);
            }
        }


        private DataSet _ScrapingDataSouce;
        /// <summary>
        /// 스크래핑 결과를 담을 데이터소스를 설정하거나 가져 옵니다.
        /// </summary>
        public DataSet ScrapingDataSource
        {
            get
            {
                return _ScrapingDataSouce;
            }
            set
            {
                if (_ScrapingDataSouce == value) return;
                _ScrapingDataSouce = value;

            }
        }


        /// <summary>
        /// 스크래핑 블럭단위(주로 요청 단위) 작업에 대한 완료를 알리는 이벤트 입니다.
        /// </summary>
        public event ScrapingBlockWorkCompletedEventHandler ScrapingBlockWorkCompleted;

        /// <summary>
        /// 스크래핑 블럭단위에 대한 작업 완료를 알리는 이벤트 입니다.
        /// </summary>
        /// <param name="blockDataSource">이벤트 발생 시점의 블럭 레벨로 조회된 소스</param>
        protected virtual void OnScrapingBlockWorkCompleted(Object blockDataSource)
        {
            if (ScrapingBlockWorkCompleted != null)
            {
                ScrapingBlockWorkCompletedEventArgs arg = new ScrapingBlockWorkCompletedEventArgs(blockDataSource);
                arg.ScrapingArgument = this.CurrentArgument;
                arg.ScrapingItem = this.CurrentItem;
                ScrapingBlockWorkCompleted(this, arg);
            }
        }

        /// <summary>
        /// 스크래핑 블럭단위에 대한 작업 완료를 알리는 이벤트 입니다.
        /// </summary>
        /// <param name="arg">이벤트 아규먼트 클래스</param>
        protected virtual void OnScrapingBlockWorkCompleted(ScrapingBlockWorkCompletedEventArgs arg)
        {
            if (ScrapingBlockWorkCompleted != null)
            {
                arg.ScrapingArgument = this.CurrentArgument;
                arg.ScrapingItem = this.CurrentItem;
                ScrapingBlockWorkCompleted(this, arg);
            }
        }

        /// <summary>
        /// 스크래핑 항목에 대한 작업 완료를 알리는 이벤트 입니다.
        /// </summary>
        public event ScrapingItemWorkCompletedEventHandler ScrapingItemWorkCompleted;
        /// <summary>
        /// ScrapingItemWorkCompleted 이벤트를 호출 합니다. 
        /// </summary>
        /// <param name="arg">ScrapingItemWorkCompleted 이벤트에 필요한 아규먼트 클래스 입니다.</param>
        protected virtual void OnScrapingItemWorkCompleted(ScrapingItemWorkCompletedEventArgs arg)
        {
            // 항목 조회에 대한 완료 이벤트
            if (ScrapingItemWorkCompleted != null)
            {
                
                arg.ScrapingArgument = this.CurrentArgument;
                ScrapingItemWorkCompleted(this, arg);
                // 하나의 항목이 끝나고 나서 다음 항목이 존재하는지 여부를 체크 후 있으면 조회 다시 호출
                if (this.RemainingItemCount > 0) this.DataWorkProcess();
            }
            
        }

        /// <summary>
        /// 스크래핑 서비스의 작업이 모두 완료 되었음을 알리는 이벤트 입니다. 
        /// </summary>
        public event InquiryCompletedEventHandler InquiryCompleted;

        /// <summary>
        /// InquiryCompleted 이벤트를 호출 합니다. 
        /// </summary>
        /// <param name="arg">InquiryCompleted 이벤트에 필요한 아규먼트 클래스 입니다.</param>
        protected virtual void OnInquiryCompleted(InquiryCompletedEventArgs arg)
        {
            if (InquiryCompleted != null)
                InquiryCompleted(this, arg);
        }

        #endregion

        // 아래 코드는 일단 보류
        ///// <summary>
        ///// 스크래핑에 사용할 아규먼트를 생성해서 리턴한다. 파생클래스에서 반드시 구현해야 한다. 
        ///// </summary>
        //public abstract ScrapingArgumentBase CreateScrapingArgumentInstance();

        ///// <summary>
        ///// 지정된 타입의 아규먼트 클래스를 만듭니다. 
        ///// </summary>
        ///// <typeparam name="T">ScrapingArgumentBase의 파생 타입</typeparam>
        ///// <returns></returns>
        //protected T CreateScrapingArgumentInstance<T>() where T : ScrapingArgumentBase
        //{ 
        //    Type instanceT = typeof(T);
        //    ScrapingArgumentBase argInstance = (ScrapingArgumentBase) Activator.CreateInstance(instanceT);
        //    return argInstance as T;
        //}

        /// <summary>
        /// 서비스의 현재 작업 단계에 대한 설명을 리턴 합니다. 
        /// </summary>
        protected virtual string ServiceCurrentWorkName()
        {
            string rtnName = ""; // this.ServiceName + "/";
            if (this.CurrentArgument != null)
            {
                rtnName += this.CurrentArgument.WorkName() + "/";
            }

            if (this.CurrentItem != null)
            {
                string itemWorkName = this.CurrentItem.WorkName();
                rtnName += itemWorkName.Length > 0 ? itemWorkName + "/" : "";
            }

            return rtnName;
        }

        #region 로그 기록

        /// <summary>
        /// 로그를 기록 합니다.
        /// </summary>
        /// <param name="message">기록할 로그 내용</param>
        /// <param name="logLevel">로그 레벨 기본 값은 Verbose 입니다.</param>
        protected void WriteLog(string message, Enums.LogLevel logLevel)
        {
            // todo: 시간/서비스/레벨/내용/트레이스 정보 등을 포멧으로 기록할 로그 기록 관리 클래스를 만들어 사용 한다.
            // 또한 로그 관련 이벤트를 제공해 로그 처리를 확장할 수 있도록 추후 구현이 필요.
            string stackTraceData = LogWriter.GetTraceData();
            //if (logLevel == Enums.LogLevel.Information || logLevel == Enums.LogLevel.Verbose)
            //{
                message = this.ServiceCurrentWorkName() + message;
            //}
            Helper.WriteLog(this.ServiceName, logLevel, message, stackTraceData);       
        }

        /// <summary>
        /// 로그를 기록 합니다. 
        /// </summary>
        /// <param name="title">타이틀</param>
        /// <param name="message">메세지</param>
        /// <param name="logLevel">로그 레벨</param>
        protected void WriteLog(string title, string message, Enums.LogLevel logLevel)
        {
            // todo: 시간/서비스/레벨/내용/트레이스 정보 등을 포멧으로 기록할 로그 기록 관리 클래스를 만들어 사용 한다.
            // 또한 로그 관련 이벤트를 제공해 로그 처리를 확장할 수 있도록 추후 구현이 필요.
            string stackTraceData = LogWriter.GetTraceData();
            title = this.ServiceCurrentWorkName() + title;
            Helper.WriteLog(this.ServiceName + "_" + title, logLevel, message, stackTraceData);
        }

        /// <summary>
        /// 로그를 기록 합니다.(LogLevel = Verbose 임.)
        /// </summary>
        /// <param name="message">기록할 로그 내용</param>
        protected void WriteLog(string message)
        {
            this.WriteLog(message, Enums.LogLevel.Verbose);
        }

        /// <summary>
        /// 예외를 기록 합니다.
        /// </summary>
        /// <param name="ex">예외</param>
        protected void WriteError(Exception ex)
        {
            LogWriter.WriteError(this.ServiceName, ex);
        }

        /// <summary>
        /// 응답메세지를 로그로 기록 합니다. 
        /// </summary>
        /// <param name="responseContents"></param>
        protected void WriteResponseData(string responseContents)
        {

            string stackTraceData = LogWriter.GetTraceData();
            LogWriter.WriteLog(this.ServiceName, Enums.LogLevel.VeryDetail, responseContents, stackTraceData);
        }
        #endregion

        #region IDisposable 인터페이스 구현
        private bool _disposedValue = false;

        /// <summary>
        /// 객체를 Dispose 합니다. 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposedValue == false)
            {
                if (disposing)
                {
                }
            }

            this._disposedValue = true;
        }
        /// <summary>
        /// 객체를 Dispose 합니다. 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
