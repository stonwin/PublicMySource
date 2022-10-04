using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 기간별 조회에 대한 분할 조회 프로세스를 위임하기 위한 위임자 입니다. 조회된 데이터의 수를 리턴하도록 합니다. 
    /// </summary>
    /// <param name="item">스크래핑 항목 객체 입니다.</param>
    /// <param name="itemSource">항목단위의 데이터 소스 입니다.</param>
    /// <param name="startDate">조회 시작일자 입니다.</param>
    /// <param name="endDate">조회 종료일자 입니다.</param>
    /// <returns>조회된 데이터의 수를 리턴 합니다.</returns>
    public delegate int PeriodDataRetrieveInvoker(ScrapingItemBase item, DataSet itemSource, DateTime startDate, DateTime endDate);

    /// <summary>
    /// 기간별 조회에 대한 분할 조회 프로세스를 위임하기 위한 위임자 입니다. 리턴 값은 없습니다.
    /// </summary>
    /// <param name="item">스크래핑 항목 객체 입니다.</param>
    /// <param name="itemSource">항목단위의 데이터 소스 입니다.</param>
    /// <param name="startDate">조회 시작일자 입니다.</param>
    /// <param name="endDate">조회 종료일자 입니다.</param>
    public delegate void PeriodDataFillInvoker(ScrapingItemBase item, DataSet itemSource, DateTime startDate, DateTime endDate);

    // 사용안함. 추후 제거.
    internal delegate int PeriodDataRetrieveInvoker<T, S>(T item, S itemSource, DateTime startDate, DateTime endDate)
        where T : ScrapingItemBase
        where S : DataSet;

}