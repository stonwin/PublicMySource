using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Enums
{
    // 기존 ELoginType에 대응 아이디 --> UserIdentity, 인증서 --> Certificate
    /// <summary>
    /// 로그인 방식에 대한 열거형 입니다. 인증서를 사용하는것과 그렇지 않은 것으로 나눕니다.
    /// </summary>
    public enum LoginType
    {
        /// <summary>
        /// 인증 없음.
        /// </summary>
        None = 0,
        /// <summary>
        /// 계정 또는 고유 키 값으로 로그인
        /// </summary>
        UserIdentity = 1,
        /// <summary>
        /// 인증서 기반으로 로그인 
        /// </summary>
        Certificate = 2
    }
}
