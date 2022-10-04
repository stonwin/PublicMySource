using System;
using System.Collections.Generic;
using System.Data;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 작업을 위한 인터페이스 입니다.
    /// </summary>
    internal interface IScrapingService
    {
        /// <summary>
        /// 스크래핑 작업의 실행을 요청 합니다.
        /// </summary>
        /// <param name="args">스크래핑 작업에 필요한 정보를 저장하는 아규먼트 클래스 입니다.</param>
        void Inquiry(ScrapingArgumentBase args);

        /// <summary>
        /// 스크래핑된 결과를 설정하거나 가져옵니다.
        /// </summary>
         DataSet ScrapingDataSource { get; set; }

        /// <summary>
        /// 스크래핑 항목 작업의 완료를 알리는 이벤트 입니다.
        /// </summary>
        event ScrapingItemWorkCompletedEventHandler ScrapingItemWorkCompleted;

        /// <summary>
        /// Inquiry 작업이 완료되었음을 알리는 이벤트 입니다.
        /// </summary>
        event InquiryCompletedEventHandler InquiryCompleted;
    }

    
}


