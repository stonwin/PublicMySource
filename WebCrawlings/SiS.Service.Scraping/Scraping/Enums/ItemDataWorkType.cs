using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Enums
{
    /// <summary>
    /// 항목 작업 구분에 대한 열거형 입니다. 조회, 푸쉬로 나뉩니다. 
    /// </summary>
    public enum ItemDataWorkType
    {
        /// <summary>
        /// 조회, 데이터 수집용으로 동작 합니다. 
        /// </summary>
        Retrieve = 0,
        /// <summary>
        /// 데이터 전송 업로드 용으로 동작 합니다.
        /// </summary>
        Push = 1
    }
}
