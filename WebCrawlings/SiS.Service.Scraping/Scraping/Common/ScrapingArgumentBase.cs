using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 아규먼트를 위한 베이스 클래스 입니다. 
    /// </summary>
    public abstract class ScrapingArgumentBase: PropertyContainerBase

    {
        #region 생성자

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        protected ScrapingArgumentBase()
        {
            _ScrapingItems = new ScrapingItemCollection(this);
            this.ServiceType = Enums.ScrapingType.None;
            this.LoginType = Enums.LoginType.None;
        }

        #endregion  

        #region 기본 공통 속성 정의

        /// <summary>
        /// 서비스 기관 타입을 설정하거나 가져 옵니다.
        /// </summary>
        public Enums.ScrapingType ServiceType
        {
            get { return (Enums.ScrapingType)this.GetProperty("ServiceType"); }
            set { this.SetProperty("ServiceType", value); }
        }

        /// <summary>
        /// 인증에 필요한 ID( 인증서 이름, 또는 로그인 ID 등)를 가져오거나 저장 합니다.
        /// </summary>
        public string AuthenticationID
        {
            get { return this.GetProperty("AuthenticationID") as string; }
            set { this.SetProperty("AuthenticationID", value); }
        }

        /// <summary>
        /// 인증에 사용되는 암호를 가져오거나 설정 합니다.
        /// </summary>
        public string AuthenticationPassword
        {
            get { return this.GetProperty("AuthenticationPassword") as string; }
            set { this.SetProperty("AuthenticationPassword", value); }
        }

        /// <summary>
        /// 주민등록번호를 가져오거나 설정 합니다. 
        /// </summary>
        public string PersonalNo
        {
            get { return this.GetProperty("PersonalNo") as string; }
            set { this.SetProperty("PersonalNo", value.Replace("-", "")); }
        }

        /// <summary>
        /// 사업자 번호를 가져오거나 설정 합니다.
        /// </summary>
        public string BusinessNo
        {
            get { return this.GetProperty("BusinessNo") as string; }
            set { this.SetProperty("BusinessNo", value.Replace("-", "")); }
        }


        /// <summary>
        /// 로그인 방식을 가져오거나 설정 합니다.
        /// </summary>
        public Enums.LoginType LoginType
        {
            get { return (Enums.LoginType)this.GetProperty("LoginType"); }
            set { this.SetProperty("LoginType", value); }
        }

        #endregion

        #region 하위 아이템 항목 리스트
        private ScrapingItemCollection _ScrapingItems ;
        /// <summary>
        /// 하위 스크래핑 항목들을 가져 옵니다.
        /// </summary>
        public ScrapingItemCollection ScrapingItems
        {
            get
            {
                return _ScrapingItems;
            }
        }

        #endregion

        #region 이 스크래핑 아규먼트를 사용하는 서비스, ScrapingItem의 인스턴스를 생성해서 리턴하는 메서드 
        
        #region 서비스 인스턴스
        /// <summary>
        /// 스크래핑 서비스의 인스턴스를 생성해서 리턴 합니다. 파생클래스에서 반드시 구현해야 합니다.
        /// </summary>
        /// <returns>ScrapingServiceBase 타입의 객체를 리턴 합니다.</returns>
        public virtual ScrapingServiceBase CreateScrapingServiceInstance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 스크래핑 서비스의 인스턴스를 생성해서 리턴 합니다. 
        /// </summary>
        /// <typeparam name="T">인스턴스가 생성되어 리턴될 타입</typeparam>
        /// <returns>서비스 인스턴스</returns>
        protected T CreateScrapingServiceInstance<T>() where T : ScrapingServiceBase
        {
            Type serviceT = typeof(T);
            ScrapingServiceBase service = (ScrapingServiceBase)Activator.CreateInstance(serviceT);
            
            // 여기에 라이센스 관련 어떤 작업 필요.
            //service.IsValidLicence = LicenceManager.ValidationLicence(this.ServiceType);
            service.IsValidLicence = true;
            return service as T;
        }
        #endregion

        #region 아이템 인스턴스
        /// <summary>
        /// 스크래핑 항목의 인스턴스를 생성 합니다. 파생클래스에서 이 메서드는 구현을 해야 합니다. 
        /// </summary>
        public virtual ScrapingItemBase CreateScrapingItemInstance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ScrapingItem의 인스턴스를 생성해서 리턴 합니다. 생성된 ScrapingItem은 ScrapingItems의 컬렉션 항목으로 자동 추가 됩니다. 
        /// </summary>
        /// <typeparam name="T">생성할 ScrapingItem의 타입을 지정 합니다.</typeparam>
        /// <returns>생성된 ScrapingItem 개체 인스턴스</returns>
        protected T CreateScrapingItemInstance<T>() where T : ScrapingItemBase
        {
            Type itemT = typeof(T);
            ScrapingItemBase scrapItem = (ScrapingItemBase)Activator.CreateInstance(itemT);
            this.ScrapingItems.Add(scrapItem);
            return scrapItem as T;
        } 
        #endregion

        #endregion

        /// <summary>
        /// 이 아규먼트의 작업 이름을 리턴 합니다. 재정의하지 않으면 빈문자열을 리턴 합니다. 
        /// </summary>
        /// <returns></returns>
        public virtual string WorkName()
        {
            return "";
        }
    }
}
