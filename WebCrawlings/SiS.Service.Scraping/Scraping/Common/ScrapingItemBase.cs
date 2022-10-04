using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 항목의 베이스 클래스 입니다. 
    /// </summary>
    public class ScrapingItemBase : PropertyContainerBase
    {

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        protected internal ScrapingItemBase() : base()
        {
            //조회기간 형식의 기본 값 지정
            //this.RetrievePeriodType = Enums.RetrievePeriodType.DaysBetween;
            // 항목 작업 형식에 대한 기본 값 지정
            this.ItemDataWorkType = Enums.ItemDataWorkType.Retrieve;
            this.FromDate = DateTime.Today;
            this.ToDate = DateTime.Today;
        }

        // 부모가 되는 아규먼트
        private ScrapingArgumentBase _parentArgument;
        /// <summary>
        /// 부모 아규먼트를 가져오거나 설정 합니다.
        /// </summary>
        public ScrapingArgumentBase ParentArgument
        {
            get
            {
                return _parentArgument;
            }
            internal set
            {
                if (_parentArgument == value) return;
                _parentArgument = value;
            }
        }

        // 페이지(스크래핑 시 페이지 단위로 처리 하는 경우 사용) 번호
        private int _PageNo = 0;
        /// <summary>
        /// 페이지 번호를 가져오거나 설정 합니다. 페이징된 데이터를 표시하는 페이지에서 사용되는 속성 입니다.
        /// </summary>
        public int PageNo
        {
            get { return _PageNo; }
            set { _PageNo = value; }
        }

        ///// <summary>
        ///// 조회기간의 형식에 대한 값을 가져오거나 설정 합니다. 기본값은 DayBetween(일별 기간) 입니다.
        ///// </summary>
        //public Enums.RetrievePeriodType RetrievePeriodType
        //{
        //    get { return (Enums.RetrievePeriodType)this.GetProperty("RetrievePeriodType"); }
        //    set { this.SetProperty("RetrievePeriodType", value); }
        //}

        /// <summary>
        /// 조회 시작일을 가져오거나 설정 합니다. 
        /// </summary>
        public DateTime FromDate
        {
            get { return (DateTime)this.GetProperty("FromDate"); }
            set { this.SetProperty("FromDate", value); }
        }

        /// <summary>
        /// 조회 종료일을 가져오거나 설정 합니다. 
        /// </summary>
        public DateTime ToDate
        {
            get { return (DateTime)this.GetProperty("ToDate"); }
            set { this.SetProperty("ToDate", value); }
        }

        /// <summary>
        /// 항목의 작업 타입을 가져오거나 지정합니다. 수집과 업로드(전송) 중 선택할 수 있으며 기본값은 Retrieve(수집) 입니다. 
        /// </summary>
        public Enums.ItemDataWorkType ItemDataWorkType
        {
            get { return (Enums.ItemDataWorkType)this.GetProperty("ItemDataWorkType"); }
            set { this.SetProperty("ItemDataWorkType", value); }
        }

        /// <summary>
        /// 태그 입니다. 
        /// </summary>
        public object Tag { get; set; }

        // 아래 속성은 필요한 경우 추후 주석 제거후 사용 한다.
        ///// <summary>
        ///// 스크래핑에 사용되는 조건의 원본 데이터 소스, 조회 완료시마다 데이터 소스에 직접 상태 정보를 입력해야 하는 상황을 대비해 만든 속성
        ///// </summary>
        //public object ScrapingRequestSource { get; set; }

        /// <summary>
        /// 항목의 작업명을 리턴 합니다.
        /// </summary>
        public virtual string WorkName()
        {
            return "";
        }
    }
}
