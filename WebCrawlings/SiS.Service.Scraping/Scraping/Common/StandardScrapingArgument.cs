using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 표준 스크래핑 아규먼트 클래스 입니다.
    /// </summary>
    public class StandardScrapingArgument : ScrapingArgumentBase
    {

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        protected StandardScrapingArgument() : base()
        {
            this.ServiceType = Enums.ScrapingType.None;
        }

        /// <summary>
        /// StandardScrapingService를 생성해서 리턴 합니다.
        /// 구현되어 있지 않습니다. 베이스를 호출하지 마세요.
        /// </summary>
        public override ScrapingServiceBase CreateScrapingServiceInstance()
        {
            throw new NotSupportedException();
            //return base.CreateScrapingServiceInstance<ScrapingStandardService>();
        }
        /// <summary>
        /// StandardScrapingItem을 생성해서 리턴 합니다. 
        /// 구현되어 있지 않습니다. 베이스를 호출하지 마세요.
        /// </summary>
        public override ScrapingItemBase CreateScrapingItemInstance()
        {
            //return base.CreateScrapingItemInstance<ScrapingStandardItem>();
            throw new NotSupportedException();
        }
    }
}
