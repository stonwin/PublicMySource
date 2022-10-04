using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Enums
{
    /// <summary>
    /// 로그를 기록할 레벨 구분에 대한 열거형 입니다. 
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 에러 레벨, 이 레벨로 셋팅된 경우 항상 로그를 남긴다. 
        /// </summary>
        Error = 0,
        /// <summary>
        /// 표준 레벨, 로그를 기록하기로 설정한 경우 남긴다. 
        /// </summary>
        Information = 1,
        /// <summary>
        /// 상세 레벨, 상세 정보로 기록된 로그를 남긴다. 
        /// </summary>
        Verbose = 2,
        /// <summary>
        /// 매우 상세 레벨, HTTP 응답 메세지에 대한 내용까지 로그로 남긴다. 
        /// </summary>
        VeryDetail = 3
    }
}
