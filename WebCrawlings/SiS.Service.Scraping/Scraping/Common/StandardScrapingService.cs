using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 표준 스크래핑 서비스 클래스 입니다. 
    /// </summary>
    public class StandardScrapingService: ScrapingServiceBase
    {

        /// <summary>
        ///  생성자 입니다.
        /// </summary>
        protected StandardScrapingService() : base()
        {
            // 생성자
            // 부분 조회 범위 일수 필요한 경우 파생 클래스에서 재정의 한다. 
            this.RetrieveIntervalDays = 10;
            //this.RetrieveIntervalSleepTime = 100;
            this.RetrieveIntervalMaxDays = 30;

            this.RetrieveIntervalSleepTime = 100;
            this.SleepTimeVariableScope = 0;

        }

        #region 재정의 되거나 숨김되는 속성
        /// <summary>
        /// 서비스 이름입니다.
        /// </summary>
        public override string ServiceName
        {
            get
            {
                return "StandardService";
            }
        }
        /// <summary>
        /// 현재 아규먼트 입니다. 
        /// </summary>
        public new StandardScrapingArgument CurrentArgument
        {
            get
            {
                return (StandardScrapingArgument)base.CurrentArgument;
            }
            protected set
            {
                base.CurrentArgument = value;
            }
        }
        /// <summary>
        /// 현재 항목 입니다. 
        /// </summary>
        public new StandardScrapingItem CurrentItem
        {
            get
            {
                return (StandardScrapingItem) base.CurrentItem;
            }
            protected set
            {
                base.CurrentItem = value;
            }
        }

        #endregion


        #region 현재 수준에서 추가되는 속성

        /// <summary>
        /// 조회 부분 범위 일 수 입니다. 기본 값은 10일이며 필요한 경우 파생클래스에서 재정의 할 수 있습니다. 
        /// </summary>
        public virtual int RetrieveIntervalDays { get; set; }
        /// <summary>
        /// 조회 부분 범위의 최대 일 수 입니다. 기본 값은 30일 이며 필요한 경우 값을 변경해서 사용 합니다.
        /// </summary>
        public int RetrieveIntervalMaxDays { get; set; }
        private int _sleepTime = 100;
        /// <summary>
        /// 부분 범위 조회  블럭의 SleepTime(1/1000 초 단위)을 설정 합니다. 기본 값은 100 입니다.
        /// </summary>
        public virtual int RetrieveIntervalSleepTime
        {
            get 
            {
                int intervalSleepTime = _sleepTime;
                if (this.SleepTimeVariableScope > 0)
                {
                    System.Random rnd = new Random();
                    intervalSleepTime += rnd.Next(0, this.SleepTimeVariableScope);
                }
                return intervalSleepTime; 
            }
            set { _sleepTime = value; }
        }

        /// <summary>
        /// 셋팅된 시간 간격 만큼 슬립 시킨다.
        /// </summary>
        protected void Sleep()
        {
            int sleepTime = this.RetrieveIntervalSleepTime;
            WriteLog("SleepTime:" + sleepTime.ToString());
            System.Threading.Thread.Sleep(sleepTime);
        }
        /// <summary>
        /// 부분 범위 조회 블럭의 SleepTime을 가변적으로 운용할때 사용하는 속성 입니다. 1/000초 단위로 셋팅 합니다. 
        /// 셋팅된 값을 최대로 RetrieveIntervalSleepTime의 값을 랜덤하게 증가 시킵니다. 기본 값은 0 입니다. 
        /// </summary>
        public virtual int SleepTimeVariableScope { get; set; }

        #endregion


        #region 구간조회에 대한 위임 구현

        /// <summary>
        /// 기간별 조회를 위임 받아서 처리하는 메서드 입니다. 조회된 데이터의 수를 리턴 합니다. 
        /// </summary>
        /// <param name="retrieveInvoker">위임자 입니다.</param>
        /// <param name="item">스크래핑 항목</param>
        /// <param name="itemSource">항목 데이터소스</param>
        /// <returns></returns>
        protected int CommonPeriodDataRetrieve(PeriodDataRetrieveInvoker retrieveInvoker, ScrapingItemBase item, DataSet itemSource)
        {
            int retrieveCouunt = 0;
            DateTime startDate = item.FromDate.AddDays(-1);
            DateTime endDate = startDate;
            int currentIntervalDays = this.RetrieveIntervalDays;
            do
            {
                startDate = endDate.AddDays(1);
                endDate = startDate.AddDays(currentIntervalDays - 1);
                if (endDate > item.ToDate) endDate = item.ToDate;
                // 기간에 대한 블럭조회
                this.WriteLog(string.Format(" {0} ~ {1} 기간 조회", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));


                int currentCount = retrieveInvoker.Invoke(item, itemSource, startDate, endDate); // ItemApprovalDataItemizeRetrieve(item, itemSource, startDate, endDate); 
                // 조회 건수에 따라 조회 기간 동적 변환 
                if (currentCount < 300)
                {
                    // 조회 건수가 작은 경우 
                    currentIntervalDays += 10;
                    if (currentIntervalDays > this.RetrieveIntervalMaxDays) currentIntervalDays = this.RetrieveIntervalMaxDays;
                }
                else if (currentCount > 1000)
                {
                    // 조회 건수가 많은 경우
                    currentIntervalDays -= 5;
                    if (currentIntervalDays < 2) currentIntervalDays = 2;
                }
                retrieveCouunt += currentCount;
                if (item.ToDate < endDate)
                {
                    this.Sleep();
                }

            } while (item.ToDate > endDate);

            return retrieveCouunt;
        }


        /// <summary>
        /// 기간별 조회를 위임받아서 처리하는 메서드 입니다. 리턴 값이 없는 void 타입 입니다.
        /// </summary>
        /// <param name="retrieveInvoker"></param>
        /// <param name="item"></param>
        /// <param name="itemSource"></param>
        /// <returns></returns>
        protected void CommonPeriodDataRetrieve(PeriodDataFillInvoker retrieveInvoker, ScrapingItemBase item, DataSet itemSource)
        {
            //int retrieveCouunt = 0;
            DateTime startDate = item.FromDate.AddDays(-1);
            DateTime endDate = startDate;
            int currentIntervalDays = this.RetrieveIntervalDays;
            do
            {
                startDate = endDate.AddDays(1);
                endDate = startDate.AddDays(currentIntervalDays - 1);
                if (endDate > item.ToDate) endDate = item.ToDate;
                // 기간에 대한 블럭조회
                this.WriteLog(string.Format(" {0} ~ {1} 기간 조회", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));

                retrieveInvoker.Invoke(item, itemSource, startDate, endDate); // ItemApprovalDataItemizeRetrieve(item, itemSource, startDate, endDate); 
               
                if (item.ToDate < endDate)
                {
                    this.Sleep();
                }

            } while (item.ToDate > endDate);

            //return retrieveCouunt;
        }
        #endregion


        #region 파생 클래스에서 재정의 또는 반드시 재정의 해야 하는 메서드 

        /// <summary>
        /// 초기화 메서드 입니다. 파생클래스에서 구현이 필요합니다. 
        /// </summary>
        protected override void ScrapingInitialize()
        {
            // 파생 클래스에서 구현
            throw new NotImplementedException();
        }
        /// <summary>
        /// 로그인 처리 메서드 입니다. 파생클래스에서 구현이 필요합니다. 로그인 처리 결과를 알리기 위해 OnLoginProcessEnd 메서드를 이용해 LoginProcessEnd 이벤트가 발생하도록 해줘야 한다. 
        /// </summary>
        protected override void LoginProcess()
        {
            // 파생클래스에서 구현
            throw new NotImplementedException();
        }


        /// <summary>
        /// Item 단위로 조회나 푸쉬 전처리 작업 필요 한 경우 이 메서드를 재정의 해서 사용 한다. 
        /// ItemDataRetrieve 또는 ItemDataPush 메서드 보다 앞서 실행되며 Item 단위로 1회 선 실행 작업이 필요한 경우 이 메서드를 재정의 해서 사용 한다. 
        /// </summary>
        /// <param name="scrapingItemBase"></param>
        protected virtual void DataWorkHeadProcess(ScrapingItemBase scrapingItemBase)
        {
            // 이 메서드는 재정의 후 사용 한다. 

        }

        /// <summary>
        /// ScrapingItem에 대해 조회 코드를 구현 한다. ScrapingItem로 조회가 끝나면 OnScrapingItemWorkCompleted 메서드를 이용해 ScrapingItemWorkCompleted 이벤트가 발생하도록 해서
        /// ScrapingItem에 대한 조회가 완료되었음을 알린다. 
        /// 내부적으로 조회 기간 분할해서 조회하도록 구현 처리 --> 조회 방식이 다른 경우에만 재정의 해서 구현 한다. 
        /// </summary>
        /// <param name="scrapItem">ScrapingItem 입니다.</param>
        protected override void ItemDataRetrieve(ScrapingItemBase scrapItem)
        {
            // 여기에 기간에 대한 구간별 조회가 되도록 구현 한다. 조회 방식이 다른 경우 
            // 항목 작업 후 데이터를 병합하기 위한 항목 기준 데이터 소스
            System.Type t = this.ScrapingDataSource.GetType();
            DataSet itemSource = Activator.CreateInstance(t) as DataSet;
            try
            {
                // Item에 대한 해더 작업을 위한 메서드 호출
                DataWorkHeadProcess(scrapItem);

                // 항목의 조회 작업에 대한 실제 구현 메서드를 호출 한다. 
                this.ItemDataRetrieve((StandardScrapingItem)scrapItem, itemSource);

                //  데이터 집합 병합 작업을 한다.
                this.MergeItemDataSource(itemSource);

                // ScrapingItem의 조회 완료 이벤트를 발생 시킨다. 
                ScrapingItemWorkCompletedEventArgs errArg = new ScrapingItemWorkCompletedEventArgs(scrapItem, itemSource);
                OnScrapingItemWorkCompleted(errArg);


            }
            catch (ScrapingAbortException abortEx)
            {
                // 중단 예외일 경우 그대로 전파.
                throw abortEx;
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 다음 항목으로 넘어가도록 하기 위해서 여기서는 에러 처리만 하고 다음 진행
                this.WriteError(ex);
                ScrapingItemWorkCompletedEventArgs errArg = new ScrapingItemWorkCompletedEventArgs(scrapItem, itemSource, ex);
                OnScrapingItemWorkCompleted(errArg);
            }
        }

        /// <summary>
        /// ScrapingItem별로 데이터 푸쉬(업로드, 전송) 작업을 한다.  
        /// </summary>
        /// <param name="scrapItem">작업용 항목</param>
        protected override void ItemDataPush(ScrapingItemBase scrapItem)
        {
            // 여기에 항목 작업에 대한 
            // 항목 작업 후 데이터를 병합하기 위한 항목 기준 데이터 소스
            System.Type t = this.ScrapingDataSource.GetType();
            DataSet itemSource = Activator.CreateInstance(t) as DataSet;
            try
            {
                // Item에 대한 해더 작업을 위한 메서드 호출
                DataWorkHeadProcess(scrapItem);

                // 항목의 푸시 작업에 대한 실제 구현 메서드를 호출 한다. 
                ItemDataPush((StandardScrapingItem)scrapItem, itemSource);

                //  데이터 집합 병합 작업을 한다.
                this.MergeItemDataSource(itemSource);

                // ScrapingItem의 조회 완료 이벤트를 발생 시킨다. 
                ScrapingItemWorkCompletedEventArgs errArg = new ScrapingItemWorkCompletedEventArgs(scrapItem, itemSource);
                OnScrapingItemWorkCompleted(errArg);


            }
            catch (Exception ex)
            {
                // 예외가 발생하면 다음 항목으로 넘어가도록 하기 위해서 여기서는 에러 처리만 하고 다음 진행
                this.WriteError(ex);
                ScrapingItemWorkCompletedEventArgs errArg = new ScrapingItemWorkCompletedEventArgs(scrapItem, itemSource, ex);
                OnScrapingItemWorkCompleted(errArg);
            }
        }

        /// <summary>
        /// ScrapingItem별로 데이터 푸쉬(업로드, 전송) 작업을 위한 코드를 구현 한다. 구현후 리턴이 필요한 데이터는 itemSource에 담는다.
        /// </summary>
        /// <param name="scrapItem">작업용 항목</param>
        /// <param name="itemSource">항목의 데이터 소스</param>
        protected virtual void ItemDataPush(StandardScrapingItem scrapItem, DataSet itemSource)
        {
            throw new NotImplementedException("ItemDataPush 메서드가 구현되지 않았습니다.");
        }

        /// <summary>
        /// 데이터를 조회 합니다. 기간별 조회에 대한 프로세스가 구현되어 있습니다. 기간별 조회를 하지 않을 경우 
        /// 재정의 해서 사용해야 합니다. 
        /// </summary>
        /// <param name="scrapItem">스크래핑 항목</param>
        /// <param name="itemSource">항목의 데이터소스</param>
        protected virtual void ItemDataRetrieve(StandardScrapingItem scrapItem, DataSet itemSource)
        {
            // 여기에 기간에 대한 구간별 조회가 되도록 구현 한다. 조회 방식이 다른 경우 
            //base.ItemDataRetrieve(scrapItem);
            // 건별 조회(기간으로)
            PeriodDataFillInvoker mi = new PeriodDataFillInvoker(InvokerItemDataRetrieve);
            this.CommonPeriodDataRetrieve(mi, scrapItem, itemSource);

            //DateTime startDate = scrapItem.FromDate.AddDays(-1);
            //DateTime endDate = startDate;

            //do
            //{
            //    startDate = endDate.AddDays(1);
            //    endDate = startDate.AddDays(this.RetrieveIntervalDays - 1);
            //    if (endDate > scrapItem.ToDate) endDate = scrapItem.ToDate;
            //    // 기간에 대한 블럭조회
            //    this.WriteLog(string.Format(" {0} ~ {1} 기간 조회", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));
            //    ItemDataRetrieve((StandardScrapingItem)scrapItem, itemSource, startDate, endDate);
            //    //System.Threading.Thread.Sleep(this.RetrieveIntervalSleepTime);
            //    this.Sleep();
            //} while (scrapItem.ToDate > endDate);


        }

        private void InvokerItemDataRetrieve(ScrapingItemBase scrapItem, DataSet itemSource, DateTime startDate, DateTime endDate)
        {
            this.ItemDataRetrieve((StandardScrapingItem)scrapItem, itemSource, startDate, endDate);
        }

        /// <summary>
        /// ScrapingItem에 대해 조회 코드를 구현 한다. 조회 방식이 From ~ To 기간으로 되는 경우에만 재정의 해서 구현 한다. 여기에서 From ~ To는
        /// ScrapingArgument에 있는 FromDate, ToDate에 대한 부분 범위로 셋팅 된다. 
        /// 조회된 데이터는 반드시 서비스에서 사용하는 DataSet에 매핑된 데이터로 채우고 리턴 한다. 
        /// </summary>
        /// <param name="scrapItem">스크래핑 항목</param>
        /// <param name="itemSource">현재 ScrapItem에서 조회된 데이터를 채울 데이터집합</param>
        /// <param name="startDate">조회 시작일</param>
        /// <param name="endDate">조회 종료일</param>
        protected virtual void ItemDataRetrieve(StandardScrapingItem scrapItem, DataSet itemSource, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException("ItemDataRetrieve 메서드가 구현되지 않았습니다.");
        }

        /// <summary>
        /// 파생 클래스에서 ScrapingItem 수준에서 조회된 데이터를 Service레벨의 데이터소스에 병합하는 작업을 구현 한다. 
        /// </summary>
        /// <param name="itemSource">ScrapingItem 수준에서 조회된 데이터집합</param>
        protected virtual void MergeItemDataSource(DataSet itemSource)
        {
            throw new NotImplementedException("MergeItemDataSource 메서드가 구현되지 않았습니다.");
        }

        
        #endregion

    }
}
