using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 런처의 모든 작업이 완료되면 발생하는 이벤트의 아규먼트 클래스 입니다. 
    /// </summary>
    public class ScrapingLauncherWorkCompletedEventArgs:System.EventArgs
    {
        /// <summary>
        /// 생성자 입니다.
        /// </summary>
        public ScrapingLauncherWorkCompletedEventArgs(bool abort)
        {
            this.IsAbort = abort;
        }

        /// <summary>
        /// 작업이 중단되었는지 여부를 가져 옵니다. 
        /// </summary>
        public bool IsAbort { get; private set; }
    }


    /// <summary>
    /// 스크래핑 런처의 작업 완료를 알리는 이벤트 핸들러 입니다.
    /// </summary>
    /// <param name="s">이벤트가 발생한 소스 입니다.</param>
    /// <param name="e">스크래핑 런처 작업 완료 이벤트 아규먼트 클래스 입니다.</param>
    public delegate void ScrapingLauncherWorkCompletedEventHandler(object s, ScrapingLauncherWorkCompletedEventArgs e);
}
