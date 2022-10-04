using System;
using System.Collections;
using System.Collections.Generic;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 매개 변수의 속성값들을 관리하기 위한 컨테이너에 대한 베이스 클래스 입니다.
    /// </summary>
    [Serializable]
    public abstract class PropertyContainerBase : IPropertyContainer
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        internal protected PropertyContainerBase()
        {
        }

        /// <summary>
        /// 속성의 값을 가져오거나 설정 합니다.
        /// </summary>
        /// <param name="propertyName">속성의 값을 가져올 속성의 이름 입니다.</param>
        /// <returns></returns>
        public object this[string propertyName]
        {
            get
            {
                return GetProperty(propertyName);
            }
            set
            {
                SetProperty(propertyName, value);
            }
        }

        /// <summary>
        /// 동적 속성 이름을 배열로 리턴 합니다. 
        /// </summary>
        public string[] PropertyNames()
        {
            string[] propKeys = new string[this.PropertyCount];
            this._properties.Keys.CopyTo(propKeys, 0);
            return propKeys;
        }

        /// <summary>
        /// 속성을 셋팅 합니다.
        /// </summary>
        /// <param name="propertyName">셋팅할 속성의 이름 입니다.</param>
        /// <param name="value">셋팅할 속성의 값 입니다.</param>
        public void SetProperty(string propertyName, object value)
        {
            if (this.ContainsProperty(propertyName))
            {
                this._properties[propertyName] = value;
            }
            else
            {
                this._properties.Add(propertyName, value);
            }
        }
        /// <summary>
        /// 속성의 값을 가져 옵니다.
        /// </summary>
        /// <param name="propertyName">속성의 이름 입니다.</param>
        /// <returns>지정된 이름의 속성이 가지는 값 입니다.</returns>
        public object GetProperty(string propertyName)
        {
            if (this.ContainsProperty(propertyName))
            {
                return this._properties[propertyName];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 지정된 이름의 속성을 값과 함께 제거 합니다.
        /// </summary>
        /// <param name="propertyName">제거할 속성의 이름 입니다.</param>
        public void RemoveProperty(string propertyName)
        {
            if (this.ContainsProperty(propertyName))
            {
                this._properties.Remove(propertyName);
            }
        }
        /// <summary>
        /// 지정된 이름의 속성이 존재하는지 여부를 리턴 합니다.
        /// </summary>
        /// <param name="propertyName">검색할 속성의 이름 입니다.</param>
        /// <returns>존재 여부를 리턴 합니다.</returns>
        public bool ContainsProperty(string propertyName)
        {
            return this._properties.ContainsKey(propertyName);
        }

        /// <summary>
        /// 셋팅된 동적 속성의 갯수를 리턴 합니다.
        /// </summary>
        public int PropertyCount
        {
            get
            {
                return _properties.Count;
            }
        }
    }
}
