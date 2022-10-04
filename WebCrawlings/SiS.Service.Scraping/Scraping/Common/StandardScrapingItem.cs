using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 표준 스크래핑 항목 클래스 입니다. 
    /// </summary>
    public class StandardScrapingItem : ScrapingItemBase
    {
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        protected internal StandardScrapingItem() : base()
        {

        }

        #region 기본적으로 재정의 되거나 숨김 속성
        /// <summary>
        /// 부모 스크래핑 아규먼트 입니다. 
        /// </summary>
        public new StandardScrapingArgument ParentArgument
        {
            get
            {
                return (StandardScrapingArgument) base.ParentArgument;
            }
            internal set
            {
                base.ParentArgument = value;
            }
        }
        #endregion

    }
}
