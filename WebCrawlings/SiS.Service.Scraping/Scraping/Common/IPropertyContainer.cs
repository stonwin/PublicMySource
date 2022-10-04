using System;
using System.Collections.Generic;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 매개 변수의 속성값들을 관리하기 위한 컨테이너에 대한 인터페이스 입니다.
    /// </summary>
    internal interface IPropertyContainer
    {
        /// <summary>
        /// 속성을 셋팅 합니다.
        /// </summary>
        /// <param name="propertyName">셋팅할 속성의 이름 입니다.</param>
        /// <param name="value">셋팅할 속성의 값 입니다.</param>
        void SetProperty(string propertyName, object value);
        /// <summary>
        /// 속성의 값을 가져 옵니다.
        /// </summary>
        /// <param name="propertyName">속성의 이름 입니다.</param>
        /// <returns>지정된 이름의 속성이 가지는 값 입니다.</returns>
        object GetProperty(string propertyName);
        /// <summary>
        /// 지정된 이름의 속성을 값과 함께 제거 합니다.
        /// </summary>
        /// <param name="propertyName">제거할 속성의 이름 입니다.</param>
        void RemoveProperty(string propertyName);
        /// <summary>
        /// 지정된 이름의 속성이 존재하는지 여부를 리턴 합니다.
        /// </summary>
        /// <param name="propertyName">검색할 속성의 이름 입니다.</param>
        /// <returns>존재 여부를 리턴 합니다.</returns>
        bool ContainsProperty(string propertyName);
    }
}
