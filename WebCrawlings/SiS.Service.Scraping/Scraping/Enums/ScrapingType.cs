using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Enums
{
    // 기존 EScrapingType에 대응

    /// <summary>
    /// Scraping 기관 타입에 대한 열거형 입니다.
    /// </summary>
    public enum ScrapingType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,
        /// <summary>
        /// 이세로
        /// </summary>
        Esero = 1,
        /// <summary>
        /// 현금영수증
        /// </summary>
        TaxSave = 2,
        /// <summary>
        /// 여신금유협회
        /// </summary>
        Crefia = 3,
        /// <summary>
        /// 홈텍스
        /// </summary>
        HomeTax = 4,
        /// <summary>
        /// 은행
        /// </summary>
        Bank = 5,
        /// <summary>
        /// 카드
        /// </summary>
        Card = 6,
        /// <summary>
        /// 국세청
        /// </summary>
        NTS = 7,
        /// <summary>
        /// 환율
        /// </summary>
        ExchangeRate = 8
        
    }


}