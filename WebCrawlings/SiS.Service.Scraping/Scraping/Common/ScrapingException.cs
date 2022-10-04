using System;
using System.Collections.Generic;
using System.Text;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑 관련 예외를 처리하기 위한 객체 입니다.
    /// </summary>
    public class ScrapingException : System.Exception
    {
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        public ScrapingException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ScrapingException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        public ScrapingException() : base()
        {

        }
    }

    /// <summary>
    /// 스크래핑 초기화 관련 예외를 처리하기 위한 객체 입니다. 
    /// </summary>
    public class ScrapingInitException : ScrapingException
    {
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        public ScrapingInitException()
        {

        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        public ScrapingInitException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ScrapingInitException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    /// <summary>
    /// 스크래핑 로그인 관련 예외를 처리하기 위한 객체 입니다. 
    /// </summary>
    public class ScrapingLoginException : ScrapingException
    {
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        public ScrapingLoginException()
        {

        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        public ScrapingLoginException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ScrapingLoginException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

    /// <summary>
    /// 스크래핑 보안 프로그램 관련 오류를 처리하기 위한 객체 입니다.
    /// </summary>
    public class ScrapingSecurityException : ScrapingException
    {
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        public ScrapingSecurityException()
        {

        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        public ScrapingSecurityException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ScrapingSecurityException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        ///// <summary>
        ///// 보안 객체 종류에 대한 열거형 타입 입니다. 
        ///// </summary>
        //public Enums.SecurityObjectKind SecurityObjectKind { get; set; }
       
    }

    /// <summary>
    /// 스크래핑 조회 관련 예외를 처리하기 위한 객체 입니다.
    /// </summary>
    public class ScrapingRetrieveException : ScrapingException
    {
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        public ScrapingRetrieveException()
        {
            
        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        public ScrapingRetrieveException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ScrapingRetrieveException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }


    /// <summary>
    /// 스크래핑 작업이 강제로 중단되었을때의 예외를 처리하기 위한 객체 입니다. 
    /// </summary>
    public class ScrapingAbortException : ScrapingException
    {
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        public ScrapingAbortException() : base()
        {
            
        }

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message">메세지 입니다.</param>
        public ScrapingAbortException(string message) : base(message)
        {

        }
        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        /// <param name="message">예외 메세지 입니다.</param>
        /// <param name="innerException">내부 예외 입니다.</param>
        public ScrapingAbortException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

}
