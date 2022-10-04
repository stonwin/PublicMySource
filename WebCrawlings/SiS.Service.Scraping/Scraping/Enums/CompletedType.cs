using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Enums
{
    // 기존 EScrapingResult에 대응 미실행 및 자료 없음은 더이상 구분하지 않음.
    /// <summary>
    /// 작업의 완료 타입에 대한 열거형 입니다.
    /// </summary>
    public enum CompletedType
    {
        /// <summary>
        /// 정상적으로 완료된 경우
        /// </summary>
        Valid = 0,
        /// <summary>
        /// 비정상적으로 완료된 경우
        /// </summary>
        Invalid = 1
        
    }
}
