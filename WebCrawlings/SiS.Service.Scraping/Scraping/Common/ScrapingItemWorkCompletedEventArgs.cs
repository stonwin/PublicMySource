using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 항목 작업을 완료했을때 발생하는 이벤트에 대한 매개변수를 전달하는 입니다.
    /// </summary>
    public class ScrapingItemWorkCompletedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        private ScrapingItemWorkCompletedEventArgs()
        {
        }

        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        /// <param name="scrapingItem">현재 스크래핑 작업된 스크래핑 아이템 입니다. </param>
        /// <param name="scrapingDataSource">스크래핑된 데이터 소스 입니다.</param>
        public ScrapingItemWorkCompletedEventArgs(ScrapingItemBase scrapingItem, object scrapingDataSource)
        {
            this.CompletedType = Enums.CompletedType.Valid;
            this.ScrapingItem = scrapingItem;
            this.CurrentScrapingItemSource = scrapingDataSource;
        }

        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        /// <param name="scrapingItem">현재 스크래핑 작업된 스크래핑 아이템 입니다. </param>
        /// <param name="scrapingDataSource">스크래핑된 데이터 소스 입니다.</param>
        /// <param name="error">예외 객체 입니다.</param>
        public ScrapingItemWorkCompletedEventArgs(ScrapingItemBase scrapingItem, object scrapingDataSource, Exception error)
        {            
            this.ScrapingItem = scrapingItem;
            this.CurrentScrapingItemSource = scrapingDataSource;
            this.Error = error;
            if (Error != null)
            {
                this.CompletedType = Enums.CompletedType.Invalid;
            }
            else
            {
                this.CompletedType = Enums.CompletedType.Valid;
            }
        }

        /// <summary>
        /// 현재 스크래핑 작업을 수행한 데이터 소스 입니다.
        /// </summary>
        public object CurrentScrapingItemSource { get; private set; }     

        /// <summary>
        /// 작업이 유효하게 완료되었는지 여부를 가져오거나 설정 합니다.
        /// </summary>
        public Enums.CompletedType CompletedType { get; private set; }

        /// <summary>
        /// 이벤트가 발생한 블럭에서 사용되는 스크래핑 아규먼트 입니다.
        /// </summary>
        public ScrapingArgumentBase ScrapingArgument { get; internal set; }


        /// <summary>
        /// 현재 ScrapingItem을 가져오거나 설정 합니다.
        /// </summary>
        public ScrapingItemBase ScrapingItem { get; internal set; }

        /// <summary>
        /// 예외가 발생한 경우 예외 객체를 가져오거나 설정 합니다.
        /// </summary>
        public Exception Error { get; private set; }

    }

    /// <summary>
    /// 스크래핑의 항목 작업 완료를 알리는 이벤트 핸들러 입니다.
    /// </summary>
    /// <param name="s">이벤트가 발생한 소스 입니다.</param>
    /// <param name="e">스크래핑 항목 작업 완료 이벤트 아규먼트 클래스 입니다.</param>
    public delegate void ScrapingItemWorkCompletedEventHandler(object s, ScrapingItemWorkCompletedEventArgs e);
}
