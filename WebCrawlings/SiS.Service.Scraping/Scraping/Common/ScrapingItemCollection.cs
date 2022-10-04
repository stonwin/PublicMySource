using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 아이템 컬렉션 입니다. 
    /// </summary>
    public class ScrapingItemCollection:System.Collections.ObjectModel.Collection<ScrapingItemBase>
    {
        // 컬렉션 항목의 부모 아규먼트클래스 셋팅용 멤버 변수
        private ScrapingArgumentBase _parentArg = null;
        
        #region 생성자
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="parentArg"></param>
        public ScrapingItemCollection(ScrapingArgumentBase parentArg)
        {
            this._parentArg = parentArg;
        }
        #endregion


        #region 항목 설정 또는 제거시 부모 Argument 객체를 설정하거나 해제하도록 재정의
        /// <summary>
        /// 항목을 셋팅 합니다.
        /// </summary>
        /// <param name="index">셋팅할 인덱스 번호</param>
        /// <param name="item">셋팅할 아이템</param>
        protected override void SetItem(int index, ScrapingItemBase item)
        {
            item.ParentArgument = _parentArg;
            base.SetItem(index, item);
        }
        /// <summary>
        /// 지정된 인덱스 요소에 항목을 삽입 합니다. 
        /// </summary>
        /// <param name="index">삽입할 인덱스 번호</param>
        /// <param name="item">항목</param>
        protected override void InsertItem(int index, ScrapingItemBase item)
        {
            item.ParentArgument = _parentArg;
            base.InsertItem(index, item);
        }

        /// <summary>
        /// 항목을 제거 합니다.
        /// </summary>
        /// <param name="index">제거할 인덱스 번호</param>
        protected override void RemoveItem(int index)
        {
            this.Items[index].ParentArgument = null;
            base.RemoveItem(index);
        }
        /// <summary>
        /// 모든 항목을 클리어 합니다. 
        /// </summary>
        protected override void ClearItems()
        {
            foreach (var item in this.Items)
            {
                item.ParentArgument = null;
            }
            base.ClearItems();
        }
        #endregion


    }

}
