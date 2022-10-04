using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 항목 작업 내부의 페이지 등과 같은 블럭 작업에 대한 완료 이벤트 매개변수를 전달하는 클래스 입니다.
    /// </summary>
    public class ScrapingBlockWorkCompletedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        private ScrapingBlockWorkCompletedEventArgs()
        {

        }

        /// <summary>
        /// 생성자 입니다. 현재 블럭의 데이터를 조회된 데이터를 매개변수로 받습니다.
        /// </summary>
        public ScrapingBlockWorkCompletedEventArgs(object blockDataSource)
        {
            this.CurrentBlockDataSource = blockDataSource;
        }

        /// <summary>
        /// 생성자 입니다. 현재 블럭의 데이터를 조회된 데이터를 매개변수로 받습니다.
        /// </summary>
        public ScrapingBlockWorkCompletedEventArgs(object blockDataSource, ScrapingArgumentBase arg, ScrapingItemBase item)
        {
            this.CurrentBlockDataSource = blockDataSource;
            this.ScrapingArgument = arg;
            this.ScrapingItem = item;
        }

        /// <summary>
        /// 현재 블럭의 데이터 소스 입니다. 
        /// </summary>
        public object CurrentBlockDataSource { get; private set; }


        /// <summary>
        /// 이벤트가 발생한 블럭에서 사용되는 스크래핑 아규먼트 입니다.
        /// </summary>
        public ScrapingArgumentBase ScrapingArgument { get; internal set; }

        /// <summary>
        /// 이벤트가 발생한 블럭에서 사용되는 스크래핑 항목 입니다.
        /// </summary>
        public ScrapingItemBase ScrapingItem { get; internal set; }

    }

    /// <summary>
    /// 스크래핑의 항목 작업 완료를 알리는 이벤트 핸들러 입니다. 
    /// </summary>
    /// <param name="s">이벤트가 발생한 소스 입니다.</param>
    /// <param name="e">스크래핑 블럭 작업 완료 이벤트 아규먼트 클래스 입니다.</param>
    public delegate void ScrapingBlockWorkCompletedEventHandler(object s, ScrapingBlockWorkCompletedEventArgs e);

}
