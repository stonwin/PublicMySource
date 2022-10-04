using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Enums
{
    /// <summary>
    /// 로그인 이후 다음 프로세스에 대한 열거형 입니다. 로그인 테스트인 경우 로그아웃을, 스크래핑 조회인 경우 조회 프로세스를 호출하도록 지정 합니다.
    /// </summary>
    public enum LoginNextProcess
    {
        /// <summary>
        /// 다음 처리 과정을 데이터 처리로 지정 합니다.
        /// </summary>
        DataProcess = 0,
        /// <summary>
        /// 다음 처리 과정을 로그아웃으로 지정 합니다.
        /// </summary>
        Logout = 1,
        /// <summary>
        /// 다음과정의 진행 없이 대기 한다. 
        /// </summary>
        StandBy

    }
}
