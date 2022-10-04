using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Enums
{
    /// <summary>
    /// 요청 메서드 형식에 대한 열거형 입니다.
    /// </summary>
    public enum HttpRequestMethodType
    {
        /// <summary>
        /// GET 방식으로 호출 합니다.
        /// </summary>
        GET = 0,
        /// <summary>
        /// POST 방식으로 호출 합니다.
        /// </summary>
        POST = 1
    }
}
