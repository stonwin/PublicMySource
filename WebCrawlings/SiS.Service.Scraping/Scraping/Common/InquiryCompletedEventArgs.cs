using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 서비스의 스크래핑 작업이 완료되었을때 사용할 이벤트 아규먼트 클래스
    /// </summary>
    public class InquiryCompletedEventArgs: System.EventArgs
    {
        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        /// <param name="scrapingArgument">현재 스크래핑 작업된 스크래핑 아규먼트 입니다. </param>
        /// <param name="scrapingDataSource">스크래핑된 데이터 소스 입니다.</param>
        public InquiryCompletedEventArgs(ScrapingArgumentBase scrapingArgument, object scrapingDataSource)
        {
            this.CompletedType = Enums.CompletedType.Valid;
            this.ScrapingArgument = scrapingArgument;
            this.ScrapingSource = scrapingDataSource;
        }

        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        /// <param name="scrapingArgument">현재 스크래핑 작업된 스크래핑 아규먼트 입니다. </param>
        /// <param name="scrapingDataSource">스크래핑된 데이터 소스 입니다.</param>
        /// <param name="error">작업중 발생한 예외 객체 입니다.</param>
        public InquiryCompletedEventArgs(ScrapingArgumentBase scrapingArgument, object scrapingDataSource, Exception error)
        {
            this.CompletedType = Enums.CompletedType.Invalid;
            this.ScrapingArgument = scrapingArgument;
            this.ScrapingSource = scrapingDataSource;
            this.Error = error;
        }
        /// <summary>
        /// 스크래핑 작업을 수행한 데이터 소스 입니다.
        /// </summary>
        public object ScrapingSource { get; private set; }
       
        /// <summary>
        /// 스크래핑 작업에 사용된 아규먼트 객체를 가져오거나 설정 합니다.
        /// </summary>
        public ScrapingArgumentBase ScrapingArgument { get; private set; }

        /// <summary>
        /// 작업이 유효하게 완료되었는지 여부를 가져오거나 설정 합니다.
        /// </summary>
        public Enums.CompletedType CompletedType { get; private set; }

        /// <summary>
        /// 예외가 발생한 경우 예외 객체를 가져오거나 설정 합니다.
        /// </summary>
        public Exception Error { get; private set; }

    }

    /// <summary>
    /// 스크래핑의 작업 완료를 알리는 이벤트 핸들러 입니다.
    /// </summary>
    /// <param name="s">이벤트가 발생한 소스 입니다.</param>
    /// <param name="e">스크래핑 작업 완료 이벤트 아규먼트 클래스 입니다.</param>
    public delegate void InquiryCompletedEventHandler(object s, InquiryCompletedEventArgs e);
}
