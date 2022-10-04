using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 이름 값 컬렉션을 직렬화하는 관리 클래스 입니다. 
    /// 웹 요청시 QueryString 또는 Post 데이터 위한 직렬화된 문자열을 생성 합니다. 
    /// </summary>
    public class PostingCollectionManager
    {
        private List<NameValue> _internalList;
        private ScrapingHelper _helper = new ScrapingHelper();

        /// <summary>
        /// 기본 생성자 입니다.
        /// </summary>
        public PostingCollectionManager()
        {
            _internalList = new List<NameValue>(50);
        }

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="capacity">처음에 저장할 수 있는 요소의 수 입니다.</param>
        public PostingCollectionManager(int capacity)
        {
            _internalList = new List<NameValue>(capacity);
        }

        /// <summary>
        /// 컬렉션을 초기화 합니다. 
        /// </summary>
        public void Clear()
        {
            _internalList.Clear();
        }

        /// <summary>
        /// 컬렉션을 추가 합니다.
        /// </summary>
        /// <param name="name">이름</param>
        /// <param name="value">값</param>
        public void Add(string name, string value)
        {
            NameValue nv = new NameValue();
            nv.Name = name;
            nv.Value = value;
            _internalList.Add(nv);
        }

        /// <summary>
        /// 컬렉션을 삽입 합니다. 
        /// </summary>
        /// <param name="index">삽입할 인덱스 번호</param>
        /// <param name="name">이름</param>
        /// <param name="value">값</param>
        public void Insert(int index, string name, string value)
        {
            NameValue nv = new NameValue();
            nv.Name = name;
            nv.Value = value;
            _internalList.Insert(index, nv);
        }

        /// <summary>
        /// 지정된 이름의 값을 가져 옵니다. 동일한 이름이 여러개 있을 경우 첫번째 이름의 값을 가져 옵니다. 
        /// </summary>
        /// <param name="name">값을 가져올 이름</param>
        /// <returns>값</returns>
        public string GetValue(string name)
        {
            
            foreach (NameValue item in _internalList)
            {
                if (item.Name == name) return item.Value;
            }
            return "";
        }

        /// <summary>
        /// 항목의 수를 리턴 합니다. 
        /// </summary>
        public int Count
        {
            get
            {
                return _internalList.Count;
            }
        }

        /// <summary>
        /// QueryString 형태로 직렬화된 문자열을 리턴 합니다.
        /// </summary>
        public string SerializeToString()
        {
            return this.SerializeToString(null);
        }

        /// <summary>
        /// 지정된 인코딩을 사용해 QueryString 형태로 직렬화된 문자열을 리턴 합니다.
        /// </summary>
        /// <param name="enc">직렬화에 사용하는 인코딩</param>
        public string SerializeToString(Encoding enc)
        {
            if (this._internalList.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            int num = 0;
            foreach (NameValue value2 in _internalList)
            {
                string name = value2.Name;
                string str = value2.Value;
                if (enc != null)
                {
                    name = _helper.UrlEncode(name, enc);
                    str = _helper.UrlEncode(str, enc);
                }
                else
                {
                    name = _helper.EscapeString(name);
                    str = _helper.EscapeString(str);
                }
                if (num == 0)
                {
                    builder.AppendFormat("{0}={1}", name, str);
                }
                else
                {
                    builder.AppendFormat("&{0}={1}", name, str);
                }
                num++;
            }
            return builder.ToString();

        }

        /// <summary>
        /// 직렬화된 값을 반환 합니다. 
        /// </summary>
        public override string ToString()
        {
            return this.SerializeToString();
        }
    }

    class NameValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
