using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using mshtml;
using MSXML;

using SiS.Service.Scraping.Enums;
using SiS.Service.Scraping.Log;
//using SiS.Framework;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// 스크래핑에 필요한 메서드를 제공 합니다. 
    /// </summary>
    public class ScrapingHelper
    {

        #region 핼퍼 인스턴스 생성
        /// <summary>
        /// ScrapingHelper의 새로운 인스턴스를 생성해서 가져 옵니다.
        /// </summary>
        public static ScrapingHelper GetHelperInstance()
        {
            return new ScrapingHelper();
        }

        private static ScrapingHelper _helper = null;
        /// <summary>
        /// ScrapingHelper의 인스턴스를 가져 옵니다. 어플리케이션 레벨에서 오직 하나의 객체만을 생성 합니다.
        /// </summary>
        public static ScrapingHelper GetHelperSingleInstance()
        {
            if (_helper == null)
                _helper = new ScrapingHelper();
            return _helper;
        }
        #endregion

        private string _UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";
        // "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";

        /// <summary>
        /// 웹브라우저의 UserAgent값을 가져오거나 설정 합니다. 
        /// </summary>
        public string UserAgent
        {
            get
            {
                return _UserAgent;
            }
            set
            {
                if (_UserAgent == value) return;
                _UserAgent = value;
            }
        }

        /// <summary>
        /// 쿠키 컨테이너를 가져오거나 설정 합니다.
        /// </summary>
        public System.Net.CookieContainer Cookie { get; set; }

        #region 로그 기록
        /// <summary>
        /// 로그를 기록 합니다.
        /// </summary>
        /// <param name="categoryName">카테고리 입니다.</param>
        /// <param name="logMessage">기록할 로그 내용</param>
        /// <param name="logLevel">로그 레벨 기본 값은 Verbose 입니다.</param>
        /// <param name="traceInfo">트레이스 정보 입니다.</param>
        // 로그를 기록 한다. 
        public virtual void WriteLog(string categoryName, Enums.LogLevel logLevel, string logMessage, string traceInfo)
        {
            // todo: 로그 기록과 관련한 구체적인 코드를 구현 해야 함.
            //Debug.WriteLine(string.Format("[{0}] || 서비스/클래스명: {1} || 로그구분 : {2} \n\r {3} \n\r {4}", loggingTime.ToString("yyyyMMdd HH:mm:ss"), categoryName, logLevel.ToString(), logMessage, traceInfo));
            LogWriter.WriteLog(categoryName, logLevel, logMessage, traceInfo);
        }

 
        #endregion

        #region 매크로 형태로 Key In 시켜주는 메서드
        /// <summary>
        /// 포커스가 있는 컨트롤에 문자열을 Kye In 합니다.
        /// </summary>
        /// <param name="strInputData">Key In할 데이터</param>
        public void InputKeyData(string strInputData)
        {

            int cnt = 0;
            char[] chrKeys = strInputData.ToCharArray();
            foreach (char ch in chrKeys)
            {
                //WriteLog(this.GetType().ToString(), LogLevel.Verbose, "키 인: " + ch.ToString(), Log.LogWriter.GetTraceData());
                cnt++;
                if (chrKeys.Length == cnt)
                {                    
                    SendKeys.SendWait(ch.ToString());
                }
                else
                {
                    SendKeys.Send(ch.ToString());
                }
            }
        }

        /// <summary>
        /// 지정된 컨트롤에 문자열을 Key In 합니다.
        /// </summary>
        /// <param name="obj">문자를 입력할 Html 입력 컨트롤</param>
        /// <param name="strInputData">입력할 문자열</param>
        public void InputKeyData(mshtml.HTMLInputElement obj, string strInputData)
        {
            int cnt = 0;
            char[] chrKeys = strInputData.ToCharArray();
            //obj.value = string.Empty;
            obj.setAttribute("value", string.Empty);
            obj.focus();
            foreach (char ch in chrKeys)
            {
                //WriteLog(this.GetType().ToString(), LogLevel.Verbose, "키 인: " + ch.ToString(), Log.LogWriter.GetTraceData());
                cnt++;
                if (chrKeys.Length == cnt)
                {
                    //obj.focus();                    
                    SendKeys.SendWait(ch.ToString());
                }
                else
                {
                    //obj.focus();
                    SendKeys.Send(ch.ToString());
                }
            }
        }

        /// <summary>
        /// 지정된 Html 컨트롤에 문자열을 Key In 합니다.
        /// </summary>
        /// <param name="obj">Key In할 Html 컨트롤</param>
        /// <param name="strInputData">입력할 문자열</param>
        public void InputKeyData(HtmlElement obj, string strInputData)
        {
            int cnt = 0;
            char[] chrKeys = strInputData.ToCharArray();
            obj.SetAttribute("value", string.Empty);
            obj.Focus();
            foreach (char ch in chrKeys)
            {
                //WriteLog(this.GetType().ToString(), LogLevel.Verbose, "키 인: " + ch.ToString(), Log.LogWriter.GetTraceData());
                cnt++;
                //SendKeys.Send(ch.ToString());
                //System.Threading.Thread.Sleep(10);

                if (chrKeys.Length == cnt)
                {
                    //obj.Focus();
                    SendKeys.SendWait(ch.ToString());
                    //SendKeys.Send(ch.ToString());
                }
                else
                {
                    //obj.Focus();                     
                    SendKeys.Send(ch.ToString());
                    System.Threading.Thread.Sleep(10);
                }
                //System.Windows.Forms.Application.DoEvents();
            }

            //System.Threading.Thread.Sleep(1000);
        } 
        #endregion

        #region 쿠키 관련 메서드
        /// <summary>
        /// 쿠키 문자열로 부터 쿠키 컬렉션을 만든다. 
        /// </summary>
        /// <param name="cookieString">쿠키 문자열</param>
        /// <returns>CookieCollection을 리턴 합니다.</returns>
        public CookieCollection CreateCookieCollection(string cookieString)
        {
            CookieCollection cookies = new CookieCollection();
            foreach (string strCookie in cookieString.Split(';'))
            {
                if (strCookie.Trim().Length != 0)
                {
                    string cName = strCookie.Split('=')[0].Trim();
                    string cValue = strCookie.Split('=')[1].Trim();
                    Cookie objCookie = new Cookie(cName, cValue);
                    cookies.Add(objCookie);
                }
            }
            return cookies;
        }

        /// <summary>
        /// 쿠키 컨테이너를 만들어서 리턴 합니다.
        /// </summary>
        /// <param name="uriString">쿠키의 Uri 주소</param>
        /// <param name="cookieString">쿠키문자열</param>
        /// <returns>CookieContainer 객체를 리턴 합니다.</returns>
        public CookieContainer CreateCookieContainer(string uriString, string cookieString)
        {
            CookieContainer objCookies = new CookieContainer();
            Uri cookieUrl = new Uri(uriString);
            CookieCollection cookies = this.CreateCookieCollection(cookieString);
            objCookies.Add(cookieUrl, cookies);
            return objCookies;
        } 
        #endregion

        #region 카드 번호 비교 메서드

        /// <summary>
        /// 첫번째 카드와 두번째 카드의 동일한 위치의 숫자가 일치하는 갯수를 카운트해서 리턴한다. 
        /// </summary>
        /// <param name="cardNo1">첫번째 카드 번호</param>
        /// <param name="cardNo2">두번째 카드 번호</param>
        /// <returns>일치하는 번호의 갯수를 Integer( int 형) 타입으로 리턴 한다. </returns>
        public int CardMatchNumCount(string cardNo1, string cardNo2)
        {
            string source1 = cardNo1.Replace("-", "");
            string source2 = cardNo2.Replace("-", "");
            if (source1.Length != source2.Length)
            {
                return -1;
            }
            int matchCount = 0;
            for (int i = 0; i < source1.Length; i++)
            {
                if (!Information.IsNumeric(source1[i]) || !Information.IsNumeric(source2[i])) continue;
                if (source1[i] == source2[i])
                {
                    matchCount++;
                }
            }
            return matchCount;
        }

        /// <summary>
        /// 두 카드번호가 동일한지를 비교 한다. '*'와 같은 마스크 문자열을 가지고 있는 경우 해당 위치의 번호는 스킵 하고 비교 한다. 
        /// 마스크 문자는 디폴크로 '*'를 사용한다. 
        /// </summary>
        /// <param name="cardNo1">첫 번째 카드 번호</param>
        /// <param name="cardNo2">두 번째 카드 번호</param>
        /// <returns>동일한지 여부를 bool 값으로 리턴 한다.</returns>
        public bool IsCardNoMatch(string cardNo1, string cardNo2)
        {
            return IsCardNoMatch(cardNo1, cardNo2, "*");
        }

        /// <summary>
        /// 두 카드번호가 동일한지를 비교 한다. '*'와 같은 마스크 문자열을 가지고 있는 경우 해당 위치의 번호는 스킵 하고 비교 한다. 
        /// </summary>
        /// <param name="cardNo1">첫 번째 카드 번호</param>
        /// <param name="cardNo2">두 번째 카드 번호</param>
        /// 
        /// <param name="starChar">스킵할 마스크 문자</param>
        /// <returns>동일한지 여부를 bool 값으로 리턴 한다.</returns>
        public bool IsCardNoMatch(string cardNo1, string cardNo2, string starChar)
        {
            int matchCount = this.CardMatchNumCount(cardNo1, cardNo2);
            int starCount = 0;
            if (cardNo2.Contains(starChar))
            {
                foreach (char ch in cardNo2.ToCharArray())
                {
                    if (ch.ToString() == starChar)
                    {
                        starCount++;
                    }
                }
            }
            else
            {
                foreach (char ch in cardNo1.ToCharArray())
                {
                    if (ch.ToString() == starChar)
                    {
                        starCount++;
                    }
                }
            }
            return ((cardNo1.Replace("-", "").Length - starCount) == matchCount);
        }

        #endregion

        #region 문자열 처리를 위한 메서드
        /// <summary>
        /// 에스케이프 문자열을 반환합니다. (공백, 퍼센트, 엔드, 플러스, 등호, 물음표를 치환한다.)
        /// </summary>
        /// <param name="data">원본 문자열</param>
        /// <returns>문자열을 반환 합니다.</returns>
        public string EscapeString(string data)
        {
            if (string.IsNullOrEmpty(data)) return "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                string ch = data.Substring(i, 1);
                if (ch == " ")
                {
                    sb.Append("%20");
                }
                else if (ch == "%")
                {
                    sb.Append("%25");
                }
                else if (ch == "&")
                {
                    sb.Append("%26");
                }
                else if (ch == "+")
                {
                    sb.Append("%2B");
                }
                else if (ch == "=")
                {
                    sb.Append("%3D");
                }
                else if (ch == "?")
                {
                    sb.Append("%3F");
                }
                else
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 문자열을 URL 인코딩 합니다. 
        /// </summary>
        /// <param name="str">인코딩할 문자열</param>
        /// <returns>URL 인코딩된 문자열을 반환 합니다.</returns>
        public string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 문자열을 URL 인코딩 합니다. 
        /// </summary>
        /// <param name="str">인코딩할 문자열</param>
        /// <param name="enc">인코딩 타입</param>
        /// <returns>URL 인코딩된 문자열을 반환 합니다.</returns>
        public string UrlEncode(string str, Encoding enc)
        {
            if (enc == null)
            {
                return this.EscapeString(str);
            }
            return HttpUtility.UrlEncode(str, enc);
        }

        /// <summary>
        /// 문자열을 URL 디코딩 합니다. 
        /// </summary>
        /// <param name="str">디코딩할 문자열</param>
        /// <returns>URL 디코딩된 문자열을 반환 합니다.</returns>
        public string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 문자열을 URL 디코딩 합니다. 
        /// </summary>
        /// <param name="str">디코딩할 문자열</param>
        /// <param name="dec">디코딩</param>
        /// <returns>URL 디코딩된 문자열을 반환 합니다.</returns>
        public string UrlDecode(string str, Encoding dec)
        {
            return HttpUtility.UrlDecode(str, dec);
        }

        /// <summary>
        /// html 디코딩을 합니다. 
        /// </summary>
        /// <param name="s">문자열</param>
        public string HtmlDecode(string s)
        {
            return HttpUtility.HtmlDecode(s);
        }

        /// <summary>
        /// html 인코딩을 합니다.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="output"></param>
        public void HtmlDecode(string s, TextWriter output)
        {
            HttpUtility.HtmlDecode(s, output);
        }

        /// <summary>
        /// NameValue 컬렉션의 데이터를 쿼리스크링 형태로 직렬화 한다. 
        /// </summary>
        /// <param name="cols">컬렉션</param>
        /// <param name="enc">인코딩 타입을 지정 합니다.</param>
        /// <returns>직렬화된 문자열</returns>
        public string SerializeToString(NameValueCollection cols, Encoding enc)
        {
            StringBuilder postingData = new StringBuilder();
            for (int i = 0; i < cols.Count; i++)
            {
                string strKey = cols.Keys[i];
                string strValue = cols[i];
                if (enc != null)
                {
                    strKey = HttpUtility.UrlEncode(strKey, enc);
                    strValue = HttpUtility.UrlEncode(strValue, enc);
                }
                else
                {
                    strKey = this.EscapeString(strKey);
                    strValue = this.EscapeString(strValue);
                }
                string[] values = strValue.Split(',');
                if (i == 0)
                {
                    postingData.AppendFormat("{0}={1}", strKey, strValue);
                }
                else if (values.Length > 1)
                {
                    foreach (string value in values)
                    {
                        postingData.AppendFormat("&{0}={1}", strKey, value);
                    }
                }
                else
                {
                    postingData.AppendFormat("&{0}={1}", strKey, strValue);
                }
            }
            return postingData.ToString();
        }

        /// <summary>
        /// 문자열 컬렉션을 쿼리스트링 형태로 직렬화 한다. 
        /// </summary>
        /// <param name="cols">문자열 컬렉션</param>
        /// <param name="enc">엔코딩 타입</param>
        /// <returns>직렬화된 문자열</returns>
        public string SerializeToString(StringCollection cols, Encoding enc)
        {
            StringBuilder postingData = new StringBuilder();
            for (int i = 0; i < cols.Count; i++)
            {
                string strKey = cols[i].Split('=')[0];
                string strValue = cols[i].Split('=')[1];
                if (enc != null)
                {
                    strKey = HttpUtility.UrlEncode(strKey, enc);
                    strValue = HttpUtility.UrlEncode(strValue, enc);
                }
                else
                {
                    strKey = this.EscapeString(strKey);
                    strValue = this.EscapeString(strValue);
                }
                if (i == 0)
                {
                    postingData.AppendFormat("{0}={1}", strKey, strValue);
                }
                else
                {
                    postingData.AppendFormat("&{0}={1}", strKey, strValue);
                }
            }
            return postingData.ToString();
        }

        /// <summary>
        /// 금액으로 사용되는 문자열에서 숫자만 발췌해서 리턴 합니다.
        /// </summary>
        /// <param name="strAmt">금액을 표현하는 문자열</param>
        /// <returns>금액 Decimal</returns>
        public decimal GetNumericFromString(string strAmt)
        {
            string rtnVal = this.RemovePattern(@"[^\d+-.]+", strAmt);
            if (string.IsNullOrEmpty(rtnVal))
            {
                return decimal.Zero;
            }
            return Conversions.ToDecimal(rtnVal);
        }

        /// <summary>
        /// 문자열에서 HTML 주석만 제거 합니다. 
        /// </summary>
        /// <param name="strData">문자열</param>
        /// <returns>HTML 주석이 제거된 문자열</returns>
        public string RemoveHtmlRemark(string strData)
        {
            string pattern = "(<!--.*[^-->]-->)|(<!--//.*[^-->]//-->)";
            return this.RemovePattern(pattern, strData);
        }

        /// <summary>
        /// HTML 태그를 제거 합니다.
        /// </summary>
        /// <param name="data">문자열</param>
        /// <returns>HTML 태그가 제거된 문자열</returns>
        public string RemoveHTMLTags(string data)
        {
            Regex RegexObj = new Regex("</?\\w+((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?>");
            while (RegexObj.IsMatch(data))
            {
                data = RegexObj.Replace(data, "");
            }
            string pattern = @"<\!\-\-.*?\-\->";
            data = Regex.Replace(data, pattern, "");
            data = data.Replace("&nbsp;", "");
            return data;
        }

        /// <summary>
        /// 문자열에서 정규식 패턴을 가지는 문자를 제거 합니다.
        /// </summary>
        /// <param name="strPattern">정규식 패턴</param>
        /// <param name="strData">문자열</param>
        /// <returns>지정된 패턴의 문자열이 제거된 문자열</returns>
        private string RemovePattern(string strPattern, string strData)
        {
            //' html 주석 제거 패턴
            //' Dim pattern As String = "(<!--.*[^-->]-->)|(<!--//.*[^-->]//-->)"
            //' 숫자외 문자 제거 패턴
            //' Dim pattern As String = "[^\d]+"
            return Regex.Replace(strData, strPattern, "");
        }

        /// <summary>
        /// 문자열에서 지정된 정규식 패턴과 일치하는 첫 번째 문자열을 리턴 한다. 
        /// </summary>
        /// <param name="regxExpression">정규식 패턴</param>
        /// <param name="data">문자열</param>
        /// <returns>결과 문자열</returns>
        public string RegxMatchText(string regxExpression, string data)
        {
            DataTable dt = this.RegxMatchList(regxExpression, data);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Match"].ToString();
            }
            return "";
        }

        /// <summary>
        /// 문자열에서 지정된 정규식 패턴과 일치하는 문자열을 모두 찿아서 DataTable 객체를 만들어서 리턴 한다. 
        /// DataTable은 Match, Position, Length 칼럼으로 되어 있다. 정규식은 옵션은 IgnorePatternWhitespace와 Multiline이 기본 적용된다.
        /// </summary>
        /// <param name="regxExpression">정규식 패턴</param>
        /// <param name="data">문자열</param>
        /// <returns>결과 DataTable</returns>
        public DataTable RegxMatchList(string regxExpression, string data)
        {
            MatchCollection matches = new Regex(regxExpression, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline).Matches(data);
            DataTable dt = new DataTable();
            dt.Columns.Add("Match");
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Length", typeof(int));

            foreach (Match match in matches)
            {
                DataRow nrow = dt.NewRow();
                nrow["Match"] = match.ToString();
                nrow["Position"] = match.Index.ToString();
                nrow["Length"] = match.Length.ToString();
                dt.Rows.Add(nrow);
            }

            return dt;
        }

        /// <summary>
        /// 문자열에서 지정된 정규식 패턴과 일치하는 첫 번째 문자열을 리턴 한다. 
        /// </summary>
        /// <param name="regxExpression">정규식 패턴</param>
        /// <param name="data">문자열</param>
        /// <param name="options">정규식에 사용될 옵션의 열거형 값을 제공 합니다.</param>
        /// <returns>결과 문자열</returns>
        public string RegxMatchText(string regxExpression, string data, RegexOptions options)
        {
            DataTable dt = this.RegxMatchList(regxExpression, data, options);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Match"].ToString();
            }
            return "";
        }

        /// <summary>
        /// 문자열에서 지정된 정규식 패턴과 일치하는 문자열을 모두 찿아서 DataTable 객체를 만들어서 리턴 한다. 
        /// DataTable은 Match, Position, Length 칼럼으로 되어 있다.
        /// </summary>
        /// <param name="regxExpression">정규식 패턴</param>
        /// <param name="data">문자열</param>
        /// <param name="options">정규식에 사용될 옵션의 열거형 값을 제공 합니다.</param>
        /// <returns>결과 DataTable</returns>
        public DataTable RegxMatchList(string regxExpression, string data, RegexOptions options)
        {
            MatchCollection matches = new Regex(regxExpression, options).Matches(data);
            DataTable dt = new DataTable();
            dt.Columns.Add("Match");
            dt.Columns.Add("Position", typeof(int));
            dt.Columns.Add("Length", typeof(int));

            foreach (Match match in matches)
            {
                DataRow nrow = dt.NewRow();
                nrow["Match"] = match.ToString();
                nrow["Position"] = match.Index.ToString();
                nrow["Length"] = match.Length.ToString();
                dt.Rows.Add(nrow);
            }

            return dt;
        }

        /// <summary>
        /// 문자열에서 암호화된 CDATA 블럭을 찾아서 암호화 문자열을 리턴 한다. 
        /// </summary>
        /// <param name="data">암호화된 문자열을 가지고 있는 문자열</param>
        /// <param name="index">암호화 문자열을 추출할 CDATA 블럭의 인덱스 번호</param>
        public string GetCDATAEncBlock(string data, int index)
        {
            string regExpress = @"//<!\[CDATA\[.[^>]*\]\]>";
            DataTable dt = this.RegxMatchList(regExpress, data, System.Text.RegularExpressions.RegexOptions.Singleline);
            if (dt.Rows.Count > 0 && dt.Rows.Count >= index + 1)
            {
                string strEnc = dt.Rows[index]["Match"].ToString();
                strEnc = strEnc.Substring(11, strEnc.Length - 16);
                strEnc = strEnc.Replace("var s = '';", "");
                strEnc = strEnc.Replace("s += '", "");
                strEnc = strEnc.Replace("';", "");
                strEnc = strEnc.Replace("\n", "");
                return strEnc;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 날짜 형식의 문자열을 날짜 타입으로 파싱 한다. 
        /// </summary>
        /// <param name="strDate">날짜로 파싱할 문자열</param>
        /// <returns></returns>
        public DateTime ParsingDateString(string strDate)
        {
            strDate = strDate.Trim();
            try
            {
                string pattern = @"(\d{2}|\d{4})(\.|-|/)\d{2}(\.|-|/)\d{2}";
                string str2 = @"\d{1,2}:\d{1,2}:\d{1,2}";
                Match match = Regex.Match(strDate, pattern);
                Match match2 = Regex.Match(strDate, str2);
                if (match != null)
                {
                    if (match.Value.Length == 0)
                    {
                        if (strDate.Length == 8)
                        {
                            // 8자리 숫자로된 문자열
                            return new DateTime(int.Parse(strDate.Substring(0, 4)), int.Parse(strDate.Substring(4, 2)), int.Parse(strDate.Substring(6, 2)));
                        }
                    }
                    else
                    {
                        if (match2 != null)
                        {
                            return Convert.ToDateTime(match.Value + " " + match2.Value);
                        }
                    
                        if (strDate.Length == 8)
                        {
                            // 8자리 숫자로된 문자열
                            return new DateTime(int.Parse(strDate.Substring(0, 4)), int.Parse(strDate.Substring(4, 2)), int.Parse(strDate.Substring(6, 2)));
                        }

                        return Convert.ToDateTime(match.Value);
                    }
                }
                return DateTime.MinValue;
                //throw new Exception("잘못된 포멧 데이터 : " + strDate + " ---> 날짜 포멧을 알 수 없습니다.");
            }
            catch (Exception ex)
            {
                throw new Exception("잘못된 포멧 데이터 : " + strDate + " ---> " + ex.Message);
            }
        }

        #endregion

        #region 숫자처리 메서드 

        /// <summary>
        /// 페이지 카운드를 계산해서 가져옵니다.
        /// </summary>
        /// <param name="itemCount">페이지를 계산할 항목의 갯수</param>
        /// <param name="pagePerCount">페이지당 항목 수</param>
        /// <returns>페이지 카운드</returns>
        public int ComputePageCount(int itemCount, int pagePerCount)
        {
            int pageCount = 0;
            pageCount = (int)Math.Floor((double)itemCount / (double)pagePerCount);
            if ((itemCount % 1000) > 0)
            {
                pageCount += 1;
            }
            return pageCount;
        }

        #endregion

        #region 요청 응답 관련 처리 메서드

        #region 요청 객체 생성

        private int mRequestCount = 0;
        /// <summary>
        /// 지정된 Uri 주소를 이용해 HttpWebRequest 객체의 인스턴스를 생성해서 받는다. 
        /// </summary>
        /// <param name="reqUriString">Uri 주소</param>
        /// <returns>HttpWebRequest 객체</returns>
        public HttpWebRequest CreateHttpWebRequest(string reqUriString)
        {
            // 스크래핑 작업 중단 체크
            if (ScrapingLauncherManager.ScrapingAbort)
            {
                throw new ScrapingAbortException("스크래핑 작업 중단 요청으로 중단되었습니다.");
            }
            mRequestCount += 1;
            if (mRequestCount > 50)
            {
                //// 요청 횟수가 50이 넘으면 무조건 3초간 슬립
                //mRequestCount = 0;
                //WriteLog(this.GetType().ToString(), LogLevel.Verbose, "요청횟수 50회 누적으로 3초간 대기", LogWriter.GetTraceData());
                //System.Threading.Thread.Sleep(3000);
                
            }
            HttpWebRequest req = (WebRequest.Create(reqUriString) as HttpWebRequest);
            req.CookieContainer = this.Cookie;

            System.Net.ServicePointManager.Expect100Continue = false;
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";  // this.UserAgent;
            req.KeepAlive = true;
            return req;
        }

        ///// <summary>
        ///// 지정된 Uri 주소를 이용해 HttpWebRequest 객체의 인스턴스를 생성해서 받는다. 
        ///// </summary>
        ///// <param name="reqUriString">Uri 주소</param>
        ///// <param name="bankType">은행 타입을 입력 합니다.</param>
        ///// <returns>HttpWebRequest 객체</returns>
        //public HttpWebRequest CreateHttpWebRequest(string reqUriString, BankType bankType)
        //{
        //    HttpWebRequest req = this.CreateHttpWebRequest(reqUriString);
        //    System.Net.ServicePointManager.Expect100Continue = false;
        //    req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/6.0; .NET4.0E; .NET4.0C; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";  // this.UserAgent;
        //    req.KeepAlive = true;
        //    switch (bankType)
        //    {
        //        case BankType.KBBank:
        //            // 국민은행
        //            req.Accept = "text/html, */*; q=0.01";
        //            req.KeepAlive = true;
        //            this.SetRequestHeader(req, "X-Requested-With: XMLHttpRequest");
        //            this.SetRequestHeader(req, "Accept-Language: ko");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            //this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "DNT: 1");
        //            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

        //            break;
                    
        //        case BankType.WOORIBank:
        //            // 우리은행
        //            // System.Threading.Thread.Sleep(2000);
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            //this.SetRequestHeader(req, "User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");

        //            req.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
                    
        //            break;

        //        case BankType.IBKBank:
        //            // 기업은행
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            req.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
        //            this.SetRequestHeader(req, "Accept-Encoding", "gzip, deflate");
        //            this.SetRequestHeader(req, "Accept-Language", "ko-KR");

        //            break;
                    
        //        case BankType.KEBBank:
        //            // 외환은행
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            req.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        //            SetRequestHeader(req, "Accept-Encoding", "gzip, deflate");
        //            SetRequestHeader(req, "Accept-Language", "ko-KR");

        //            break;

        //        case BankType.JBBank:
        //            //전북은행
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3; moasigns=1.0.23;; moasigns=1.0.23;)";
        //            SetRequestHeader(req, "Accept-Encoding", "gzip, deflate");
        //            SetRequestHeader(req, "Accept-Language", "ko-KR");

        //            break;
                
        //        default:
        //            // 지정되지 않은 경우 표준 해더 타입 지정 후 실행
        //            // 대구 은행
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            req.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";
        //            this.SetRequestHeader(req, "Accept-Encoding", "gzip, deflate");
        //            this.SetRequestHeader(req, "Accept-Language", "ko-KR");

        //            break;
        //    }

            
        //    return req;
        //}

        ///// <summary>
        ///// 지정된 Uri 주소를 이용해 HttpWebRequest 객체의 인스턴스를 생성해서 받는다. 
        ///// </summary>
        ///// <param name="reqUriString">Uri 주소</param>
        ///// <param name="cardType">카드 타입을 입력 합니다.</param>
        ///// <returns>HttpWebRequest 객체</returns>
        //public HttpWebRequest CreateHttpWebRequest(string reqUriString, CardType cardType)
        //{
        //    HttpWebRequest req = this.CreateHttpWebRequest(reqUriString);
        //    System.Net.ServicePointManager.Expect100Continue = false;
        //    // Accept : image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */* 

        //    switch (cardType)
        //    {
        //        case CardType.None:
        //            break;
        //        case CardType.BCCard:
        //            // 비씨 카드
        //            req.Accept = "text/html, application/xhtml+xml, *";
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "DNT: 1");
        //            break;

        //        case CardType.KBCard:
        //            // 국민카드
        //            req.Accept = "text/html, application/xhtml+xml, *";
        //            //this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, de;late");
        //            this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "DNT: 1");

        //            this.SetRequestHeader(req, "X-Requested-With: XMLHttpRequest");
        //            this.SetRequestHeader(req, "Accept-Language: ko");
        //            break;

        //        case CardType.SHINHANCard:
        //            // 신한 카드 
        //            req.Accept = "text/html, application/xhtml+xml, *";
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, de;late");
        //            this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "DNT: 1");
        //            break;

        //        case CardType.SAMSUNGCard:
        //            // 삼성 카드
        //            req.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            //this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            this.SetRequestHeader(req, "DNT: 1");
        //            break;

        //        case CardType.LOTTECard:
        //            // 롯데 카드
        //            req.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            break;

        //        case CardType.HYUNDAICard:
        //            // 현대카드 
        //            req.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "DNT: 1");
        //            break;

        //        case CardType.KEBCard:
        //            // 외환 카드
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            this.SetRequestHeader(req, "DNT: 1");
        //            break;
        //        default:
        //            throw new NotSupportedException("지원하지 않는 카드 타입 입니다.");

        //    }

        //    req.UserAgent = this.UserAgent;
        //    req.KeepAlive = true;
        //    return req;
        //}

        ///// <summary>
        ///// HttpWebRequest 객체를 생성해서 반환 합니다. 
        ///// </summary>
        ///// <param name="reqUriString">URL 문자열</param>
        ///// <param name="scrapinngType">스크래핑 타입</param>
        ///// <returns></returns>
        //public HttpWebRequest CreateHttpWebRequest(string reqUriString, Enums.ScrapingType scrapinngType)
        //{
        //    HttpWebRequest req = this.CreateHttpWebRequest(reqUriString);
        //    System.Net.ServicePointManager.Expect100Continue = false;
        //    req.UserAgent = this.UserAgent;
        //    req.KeepAlive = true;
        //    switch (scrapinngType)
        //    {
        //        case Enums.ScrapingType.Esero:
        //            // 이세로 (세금계산서 관련 조회)
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            //this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            //this.SetRequestHeader(req, "DNT: 1");
        //            break;

        //        case ScrapingType.Crefia:
        //            // 여신금융협회 (카드 매출[승인,입금 포함] 관련 조회)
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            //this.SetRequestHeader(req, "Cache-Control: no-cache");
        //            //this.SetRequestHeader(req, "DNT: 1");
        //            break;

        //        case ScrapingType.TaxSave:
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            break;
                
        //        case ScrapingType.NTS:
        //            req.Accept = "text/html, application/xhtml+xml, */*";
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            break;
                
        //        case ScrapingType.HomeTax:
        //         req.Accept = "text/html, application/xhtml+xml, */*";
        //            this.SetRequestHeader(req, "Pragma: no-cache");
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            break;

        //        default:
        //            throw new NotSupportedException("지원되지 않는 서비스 타입 입니다.");
        //    }

        //    return req;
        //}
        /// <summary>
        ///  XMLHTTPRequest 객체의 인스턴스를 생성해서 가져 온다.
        /// </summary>
        /// <param name="method">요청 메서드(GET, 또는 POST 방식 중 하나를 사용 한다.)</param>
        /// <param name="url">요청 Uri 주소</param>
        /// <returns>XMLHTTPRequest 객체를 받습니다.</returns>
        public XMLHTTPRequest CreateXMLHTTPRequest(string method, string url)
        {
            // 스크래핑 작업 중단 체크
            if (ScrapingLauncherManager.ScrapingAbort)
            {
                throw new ScrapingAbortException("스크래핑 작업 중단 요청으로 중단되었습니다.");
            }
            XMLHTTPRequestClass req = new XMLHTTPRequestClass();
            req.open(method, url, false, null, null);
            return req;
        }

        /// <summary>
        ///  XMLHTTPRequest 객체의 인스턴스를 생성해서 가져 온다.
        /// </summary>
        /// <param name="method">요청 메서드</param>
        /// <param name="url">요청 Uri 주소</param>
        /// <returns>XMLHTTPRequest 객체를 받습니다.</returns>
        public XMLHTTPRequest CreateXMLHTTPRequest(HttpRequestMethodType method, string url)
        {
            switch (method)
            {
                case HttpRequestMethodType.GET:
                    return CreateXMLHTTPRequest("GET", url);
                //break;
                case HttpRequestMethodType.POST:
                    return CreateXMLHTTPRequest("POST", url);
                //break;
                default:
                    throw new NotSupportedException();
            }
        }

        ///// <summary>
        ///// XMLHTTPRequest 객체를 생성해서 반환 합니다. 
        ///// </summary>
        ///// <param name="method">get 또는 post 중 선택합니다.</param>
        ///// <param name="url">객체 생성을 위한 url</param>
        ///// <param name="bankType">은행 타입</param>
        ///// <param name="isPing">ping용으로 사용되는지 여부를 리턴 합니다.</param>
        ///// <returns></returns>
        //public XMLHTTPRequest CreateXMLHTTPRequest(Enums.HttpRequestMethodType method, string url, Enums.BankType bankType, bool isPing = false)
        //{

        //    XMLHTTPRequest request = this.CreateXMLHTTPRequest(method, url);

        //    switch (bankType)
        //    {
        //        //case BankType.HANABank:
        //        //    // 하나
        //        //    if (isPing)
        //        //    {
        //        //        request.setRequestHeader("Accept-Language", "ko");
        //        //    }
        //        //    else
        //        //    {
        //        //        request.setRequestHeader("Accept", "text/html, application/xhtml+xml, */*");
        //        //        request.setRequestHeader("Accept-Language", "ko-KR");
        //        //    }

        //        //    request.setRequestHeader("Accept-Encoding", "securemsg");
        //        //    request.setRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0);");
        //        //    request.setRequestHeader("Connection", "Keep-Alive");
        //        //    request.setRequestHeader("Pragma", "no-cache");

        //        //    break;
                    
        //            case BankType.SHINHANBank:
        //            // 신한
        //            request.setRequestHeader("Accept-Encoding", "securemsg");
        //            request.setRequestHeader("Accept-Language", "ko");
        //            request.setRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
        //            request.setRequestHeader("Connection", "Keep-Alive");
        //            request.setRequestHeader("Pragma", "no-cache");

        //            if (!isPing)
        //            {
        //                request.setRequestHeader("Cookie", "loginURL=/rib/easy_integ/easy_index.jsp");
        //                request.setRequestHeader("Cookie", "Action=");
        //                request.setRequestHeader("Cookie", "Task=");

        //                //_XmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
        //                request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
        //            }

        //            break;
                    
        //            case BankType.JBBank:
        //            // 전북 - 사용 안됨.??
        //            //request.setRequestHeader("Accept", "*/*");
        //            //request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
        //            //request.setRequestHeader("Pragma", "no-cache");
        //            //request.setRequestHeader("Cache-Control", "no-store, no-cache, must-revalidate");
        //            //request.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        //            //request.setRequestHeader("Accept-Encoding", "gzip, deflate");
        //            //request.setRequestHeader("Accept-Language", "ko");
        //            //request.setRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
        //            //request.setRequestHeader("Connection", "Keep-Alive");
                    
        //            break;

        //        //case BankType.BUSANBank:

        //            ////부산 
        //            //if (isPing)
        //            //{                      
        //            //    request.setRequestHeader("Accept-Language", "ko-KR");
        //            //}
        //            //else
        //            //{
        //            //    request.setRequestHeader("Accept", "text/html, application/xhtml+xml, */*");
        //            //    request.setRequestHeader("Accept-Language", "ko");
        //            //}
        //            //request.setRequestHeader("Accept-Encoding", "securemsg");                    
        //            //request.setRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0);");
        //            //request.setRequestHeader("Connection", "Keep-Alive");
        //            //request.setRequestHeader("Pragma", "no-cache");

        //            //break;
                    
        //        case BankType.BUSANBank:
        //        case BankType.HANABank:
        //        case BankType.NHBank:
        //            // 부산, 하나, 농협
        //             if (isPing)
        //            {                      
        //                request.setRequestHeader("Accept-Language", "ko-KR");
        //            }
        //            else
        //            {
        //                request.setRequestHeader("Accept", "text/html, application/xhtml+xml, */*");
        //                request.setRequestHeader("Accept-Language", "ko");
        //            }
        //            request.setRequestHeader("Accept-Encoding", "securemsg");                    
        //            request.setRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0);");
        //            request.setRequestHeader("Connection", "Keep-Alive");
        //            request.setRequestHeader("Pragma", "no-cache");
        //            break;

        //        default:

        //            break;
        //    }

        //    return request;
        //}


        ///// <summary>
        /////  XMLHTTPRequest 객체의 인스턴스를 생성해서 가져 온다. 생성시 카드 타입에 따른 해더 정보를 추가해서 리턴 한다. 
        ///// </summary>
        ///// <param name="method">요청 메서드</param>
        ///// <param name="url">요청 Uri 주소</param>
        ///// <param name="cardType">요청 객체의 해더 정보를 추가하기 위한 카드 타입</param>
        ///// <returns>XMLHTTPRequest 객체를 받습니다.</returns>
        //public XMLHTTPRequest CreateXMLHTTPRequest(Enums.HttpRequestMethodType method, string url, Enums.CardType cardType)
        //{
        //    //this.SetRequestHeader(req, "Accept: image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*");
        //    XMLHTTPRequest req = null;
        //    switch (cardType)
        //    {
        //        case Enums.CardType.None:

        //            break;
        //        case Enums.CardType.BCCard:
        //            // 비씨 카드 
        //            req = this.CreateXMLHTTPRequest(method, url);
        //            this.SetRequestHeader(req, "Accept: text/html, application/xhtml+xml, */*");
                    
        //            this.SetRequestHeader(req, "Connection: Keep-Alive");
        //            this.SetRequestHeader(req, "User-Agent:" + this.UserAgent);
        //            this.SetRequestHeader(req, "Pragma: no-cache");

        //            // ==================
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: gzip, deflate");
        //            this.SetRequestHeader(req, "Content-Type: application/x-www-form-urlencoded");
        //            break;

        //        case Enums.CardType.KBCard:

        //            break;
        //        case Enums.CardType.SHINHANCard:

        //            break;
        //        case Enums.CardType.SAMSUNGCard:

        //            break;
        //        case Enums.CardType.LOTTECard:

        //            break;
        //        case Enums.CardType.HYUNDAICard:

        //            break;
        //        case Enums.CardType.HANASKCard:
        //            // 하나SK카드
        //            req = this.CreateXMLHTTPRequest(method, url);
        //            this.SetRequestHeader(req, "Accept: text/html, application/xhtml+xml, */*");

        //            this.SetRequestHeader(req, "Connection: Keep-Alive");
        //            this.SetRequestHeader(req, "User-Agent:" + this.UserAgent);
        //            this.SetRequestHeader(req, "Pragma: no-cache");

        //            // ==============
        //            this.SetRequestHeader(req, "Accept-Language: ko-KR");
        //            this.SetRequestHeader(req, "Accept-Encoding: securemsg");
        //            break;

        //        case Enums.CardType.WOORICard:

        //            break;

        //        case Enums.CardType.CITYCard:
        //            // 씨티카드
        //            req = this.CreateXMLHTTPRequest(method, url);
        //            this.SetRequestHeader(req, "Accept: text/html, application/xhtml+xml, */*");

        //            this.SetRequestHeader(req, "Connection: Keep-Alive");
        //            this.SetRequestHeader(req, "User-Agent:" + _helper.UserAgent);
        //            this.SetRequestHeader(req, "Pragma: no-cache");

        //            this.SetRequestHeader(req, "Accept-Encoding: securemsg");
        //            this.SetRequestHeader(req, "Accept-Language: ko");
                    


        //break;


        //        case Enums.CardType.KEBCard:

        //            break;
        //        case Enums.CardType.NHCard:

        //            break;
        //        case Enums.CardType.KWANGJUCard:

        //            break;
        //        case Enums.CardType.JBCard:

        //            break;
        //        case Enums.CardType.SUHYUPCard:

        //            break;
        //        case Enums.CardType.JEJUCard:

        //            break;
        //    }
        //    return req;
        //}
        #endregion

        #region 요청 객체의 해더를 추가하는 메서드
        /// <summary>
        /// HttpWebRequest 객체의 키-값 쌍으로 해더에 항목을 추가 한다. 
        /// </summary>
        /// <param name="req">HttpWebRequest 객체</param>
        /// <param name="name">해더 이름</param>
        /// <param name="value">해더 값</param>
        public void SetRequestHeader(HttpWebRequest req, string name, string value)
        {
            if (req.Headers[name] == null)
            {
                req.Headers.Add(name, value);
            }
            else
            {
                req.Headers[name] = value;
            }
        }

        /// <summary>
        /// HttpWebRequest 객체의 키-값 쌍으로 해더에 항목을 추가 한다. 
        /// </summary>
        /// <param name="req">HttpWebRequest 객체</param>
        /// <param name="strHeader">해더 이름:값으로 셋팅된 문자열</param>
        public void SetRequestHeader(HttpWebRequest req, string strHeader)
        {
            string name = strHeader.Split(':')[0].Trim();
            string value = strHeader.Split(':')[1].Trim();
            if (req.Headers[name] == null)
            {
                req.Headers.Add(name, value);
            }
            else
            {
                req.Headers[name] = value;
            }
        }

        /// <summary>
        /// HttpWebRequest 객체의 키-값 쌍으로 해더에 항목을 추가 한다. 
        /// </summary>
        /// <param name="req">HttpWebRequest 객체</param>
        /// <param name="strHeaders">해더 이름:값으로 셋팅된 문자열을 배열로 나열 한다.</param>
        public void SetRequestHeader(HttpWebRequest req, params string[] strHeaders)
        {
            foreach (string strHeader in strHeaders)
            {
                SetRequestHeader(req, strHeader);
            }
        }

        /// <summary>
        /// XMLHTTPRequest 객체의 키-값 쌍으로 해더에 항목을 추가 한다. 
        /// </summary>
        /// <param name="req">XMLHTTPRequest 객체</param>
        /// <param name="name">해더 이름</param>
        /// <param name="value">해더 값</param>
        public void SetRequestHeader(XMLHTTPRequest req, string name, string value)
        {
            req.setRequestHeader(name, value);
        }

        /// <summary>
        /// XMLHTTPRequest 객체의 키-값 쌍으로 해더에 항목을 추가 한다. 
        /// </summary>
        /// <param name="req">XMLHTTPRequest 객체</param>
        /// <param name="strHeader">해더 이름:값으로 셋팅된 문자열</param>
        public void SetRequestHeader(XMLHTTPRequest req, string strHeader)
        {
            string name = strHeader.Split(':')[0].Trim();
            string value = strHeader.Split(':')[1].Trim();
            req.setRequestHeader(name, value);
        }


        /// <summary>
        /// XMLHTTPRequest 객체의 키-값 쌍으로 해더에 항목을 추가 한다. 
        /// </summary>
        /// <param name="req">XMLHTTPRequest 객체</param>
        /// <param name="strHeaders">해더 이름:값으로 셋팅된 문자열을 배열로 나열 한다.</param>
        public void SetRequestHeader(XMLHTTPRequest req, params string[] strHeaders)
        {
            foreach (string strHeader in strHeaders)
            {
                SetRequestHeader(req, strHeader);
            }
        } 
        #endregion

        #region 요청객체에 Post 데이터를 추가하는 메서드
        /// <summary>
        /// HttpWebRequest 객체에 서버로 Posting할 데이터를 셋팅 합니다. 포스팅할 데이터는 Default 엔코딩(949)을 사용 합니다.
        /// </summary>
        /// <param name="request">HttpWebRequest 객체 입니다.</param>
        /// <param name="postData">서버로 포스팅할 데이터 입니다.</param>
        public void SetRequestPostData(HttpWebRequest request, string postData)
        {
            //request.Method = "POST";
            //byte[] bytes = Encoding.Default.GetBytes(postData);
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = bytes.Length;
            //using (Stream stream = request.GetRequestStream())
            //{
            //    stream.Write(bytes, 0, bytes.Length);
            //    stream.Close();
            //}
            SetRequestPostData(request, postData, Encoding.Default);
        }

        // 응답 대기를 위한 타임 아웃
        private int _responseTimeOout = 120 * 1000;

        /// <summary>
        /// HttpWebRequest 객체에 서버로 Posting할 데이터를 셋팅 합니다.
        /// </summary>
        /// <param name="request">HttpWebRequest 객체 입니다.</param>
        /// <param name="postData">서버로 포스팅할 데이터</param>
        /// <param name="reqPostEncoding">서버로 포스팅할 데이터의 Encoding 방식을 지정 합니다. </param>
        public void SetRequestPostData(HttpWebRequest request, string postData, Encoding reqPostEncoding)
        {
            request.Method = "POST";
            byte[] bytes = reqPostEncoding.GetBytes(postData);
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
        } 
        #endregion

        #region 응답 객체 생성(요청 객체로 부터 받기)
        /// <summary>
        /// 포스트 데이터를 설정하고 HttpWebRequest 객체를 가져 옵니다. 
        /// </summary>
        /// <param name="request">HttpWebRequest 요청 객체</param>
        /// <param name="strPost">서버로 포스팅할 데이터</param>
        /// <returns>HttpWebResponse 객체를 리턴 합니다.</returns>
        public HttpWebResponse GetResponse(HttpWebRequest request, string strPost)
        {
            //request.Timeout = _responseTimeOout;
            //if (strPost.Length > 0)
            //{
            //    this.SetRequestPostData(request, strPost);
            //}
            //else
            //{
            //    request.Method = "GET";
            //}
            //return (request.GetResponse() as HttpWebResponse);
            return GetResponse(request, strPost, Encoding.Default);
        }

        /// <summary>
        /// 포스트 데이터를 설정하고 HttpWebRequest 객체를 가져 옵니다. 
        /// </summary>
        /// <param name="request">HttpWebRequest 요청 객체</param>
        /// <param name="strPost">서버로 포스팅할 데이터</param>
        /// <param name="reqPostEncodingName">포스팅할 데이터의 인코딩 이름 입니다.</param>
        /// <returns>HttpWebResponse 객체를 리턴 합니다.</returns>
        public HttpWebResponse GetResponse(HttpWebRequest request, string strPost, string reqPostEncodingName)
        {
            //request.Timeout = _responseTimeOout;
            //if (strPost.Length > 0)
            //{
            //    this.SetRequestPostData(request, strPost, Encoding.GetEncoding(reqPostEncoding));
            //}
            //else
            //{
            //    request.Method = "GET";
            //}
            //return (request.GetResponse() as HttpWebResponse);

            Encoding postDataEncoding = Encoding.GetEncoding(reqPostEncodingName);

            return GetResponse(request, strPost, postDataEncoding);
        }


        /// <summary>
        /// 포스트 데이터를 설정하고 HttpWebRequest 객체를 가져 옵니다. 
        /// </summary>
        /// <param name="request">HttpWebRequest 요청 객체</param>
        /// <param name="strPost">서버로 포스팅할 데이터</param>
        /// <param name="postDataEncoding">포스팅할 데이터의 인코딩 입니다.</param>
        /// <returns>HttpWebResponse 객체를 리턴 합니다.</returns>
        public HttpWebResponse GetResponse(HttpWebRequest request, string strPost, Encoding postDataEncoding)
        {
            request.Timeout = _responseTimeOout;
            if (strPost.Length > 0)
            {
                this.SetRequestPostData(request, strPost, postDataEncoding);
            }
            else
            {
                request.Method = "GET";
            }
            return (request.GetResponse() as HttpWebResponse);
        } 
        #endregion

        #region 응답 컨텐츠를 지정된 인코딩 방식으로 가져 온다.
        /// <summary>
        /// 응답 객체로부터 인코딩 방식을 가져 온다. 
        /// </summary>
        /// <param name="res">HttpWebResponse</param>
        /// <returns>인코딩을 리턴한다.</returns>
        public Encoding GetResponseEncoding(HttpWebResponse res)
        {
            string input = res.ContentType.ToUpper();
            if (Regex.IsMatch(input, "UTF-8"))
            {
                return Encoding.UTF8;
            }
            if (Regex.IsMatch(input, "EUC-KR"))
            {
                return Encoding.GetEncoding("EUC-KR");
            }
            return Encoding.Default;
        }

        /// <summary>
        /// 응답객체의 응답 스트림을 가져 옵니다.
        /// </summary>
        /// <param name="response">응답객체</param>
        public Stream GetResponseStream(HttpWebResponse response)
        {
            Stream responseStream = response.GetResponseStream();

            if (response.ContentEncoding.ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }

            else if (response.ContentEncoding.ToLower().Contains("deflate"))
            {
                responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);
            }

            return responseStream;
        }

        /// <summary>
        /// 응답 객체로부터 응답 내용을 읽어 온다.
        /// </summary>
        /// <param name="response">응답객체</param>        
        public string GetResponseContents(HttpWebResponse response)
        {
            return GetResponseContents(response, null);
        }

        /// <summary>
        /// response 객체로부터 응답 내용을 읽어 온다. 
        /// </summary>
        /// <param name="response">HttpWebResponse 객체</param>
        /// <param name="encType">인코딩 타입</param>
        /// <param name="autoResponseClose">응답객체의 자동 Close 여부, 기본 값은 tru로 설정된다.</param>
        /// <returns>응답객체로 부터 읽은 응답 내용</returns>
        public string GetResponseContents(HttpWebResponse response, Encoding encType, bool autoResponseClose = true)
        {
            if (encType == null)
            {
                encType = this.GetResponseEncoding(response);
            }
            Stream responseStream = response.GetResponseStream();
            string strContents = string.Empty;
            if (response.ContentEncoding.ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }
            else if (response.ContentEncoding.ToLower().Contains("deflate"))
            {
                responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);
            }
            using (Stream stream2 = responseStream)
            {
                StreamReader reader = new StreamReader(stream2, encType);
                strContents = reader.ReadToEnd();
                reader.Close();
            }
            if (autoResponseClose)
            {
                response.Close();
                response = null;
            }
            return strContents;
        } 
        #endregion

        #region 응답스트림을 메모리스트림으로 변환 리턴 

        /// <summary>
        /// 응답 객체(HttpWebResponse)의 응답스트림(ResponseStream)을 메모리스트림으로 변환해서 리턴 합니다. 
        /// </summary>
        /// <param name="res">응답객체</param>
        public MemoryStream GetResponseToMemoryStream(HttpWebResponse res)
        {
            MemoryStream ms = new MemoryStream(1024 * 50);
            using (Stream stm = GetResponseStream(res))
            {
                byte[] buffer = new byte[4095];
                int readCount = 0;
                do
                {
                    readCount = stm.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, readCount);
                } while (readCount > 0);

            }
            return ms;
        }

        #endregion

        #endregion

        #region Win32 API 사용

        [DllImport("urlmon.dll", CharSet = CharSet.Auto)]
        private static extern uint FindMimeFromData(uint pBC, [MarshalAs(UnmanagedType.LPStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, uint cbSize, [MarshalAs(UnmanagedType.LPStr)] string pwzMimeProposed, uint dwMimeFlags, out uint ppwzMimeOut, uint dwReserverd);

        /// <summary>
        /// 바이트 배열로부터 마임 타입을 읽어 온다. 
        /// </summary>
        /// <param name="data">바이트 배열</param>
        /// <param name="checkingMimeType">비교할 마임 타입 기본 값은 application/octet-stream을 사용</param>
        /// <param name="mimeSampleSize">마임 샘플 크기</param>
        public string GetMimeFromBytes(byte[] data, string checkingMimeType = "application/octet-stream", uint mimeSampleSize = 256)
        {
            string readMimeValue = "";
            try
            {
                uint num;
                FindMimeFromData(0, null, data, mimeSampleSize, null, 0, out num, 0);
                IntPtr mimePointer = new IntPtr((long)num);
                string mime = Marshal.PtrToStringUni(mimePointer);
                Marshal.FreeCoTaskMem(mimePointer);
                //readMimeValue = Conversions.ToString(Func.NVL(mime, checkingMimeType));
                if (string.IsNullOrEmpty(mime))
                {
                    readMimeValue = checkingMimeType;
                }
                else
                {
                    readMimeValue = mime;
                }
            }
            catch 
            {
                readMimeValue = checkingMimeType;
            }
            return readMimeValue;
        }

 


 


        #endregion

        #region 열거형 값 변환 관련 함수 

        ///// <summary>
        ///// 지정된 분기에 대한 분기 값을 문자열(배열)로 가져 온다.  
        ///// </summary>
        ///// <returns></returns>
        //public string[] GetQuarterValues(Enums.RetrieveQuarterType quarter)
        //{
        //    switch (quarter)
        //    {
        //        case Enums.RetrieveQuarterType.Q11:
        //            return new string[] { "1" };
        //        case Enums.RetrieveQuarterType.Q12:
        //            return new string[] { "2" };
        //        case Enums.RetrieveQuarterType.Q13:
        //            return new string[] { "1", "2" };
        //        case Enums.RetrieveQuarterType.Q21:
        //            return new string[] { "3" };
        //        case Enums.RetrieveQuarterType.Q22:
        //            return new string[] { "4" };
        //        case Enums.RetrieveQuarterType.Q23:
        //            return new string[] { "3", "4" };

        //    }
        //    throw new NotSupportedException("지원하지 않는 케이스 입니다.");

        //}

        ///// <summary>
        ///// 분기에 해당하는 월을 문자열 배열로 가져온다. 
        ///// </summary>
        ///// <param name="quarter"></param>
        //public string[] GetMonthFromQuarter(Enums.RetrieveQuarterType quarter)
        //{
        //    switch (quarter)
        //    {
        //        case Enums.RetrieveQuarterType.Q11:
        //            return new string[] { "1", "2", "3"};
        //        case Enums.RetrieveQuarterType.Q12:
        //            return new string[] { "4", "5", "6" };
        //        case Enums.RetrieveQuarterType.Q13:
        //            return new string[] { "1", "2", "3", "4", "5", "6" };
        //        case Enums.RetrieveQuarterType.Q21:
        //            return new string[] { "7", "8", "9" };
        //        case Enums.RetrieveQuarterType.Q22:
        //            return new string[] { "10", "11", "12" };
        //        case Enums.RetrieveQuarterType.Q23:
        //            return new string[] { "7", "8", "9", "10", "11", "12" };
        //    }
        //    throw new NotSupportedException("지원하지 않는 케이스 입니다.");
        //}

        ///// <summary>
        ///// 월을 이용해 분기 셋팅(1,2,3,4 분기중)
        ///// </summary>
        ///// <param name="MonthData">월(정수)</param>
        //public Enums.RetrieveQuarterType GetQuarterValue(int MonthData)
        //{
        //    switch (MonthData)
        //    {
        //        case 1:
        //        case 2:
        //        case 3:
        //            return Enums.RetrieveQuarterType.Q11;
        //        case 4:
        //        case 5:
        //        case 6:
        //            return Enums.RetrieveQuarterType.Q12;
        //        case 7:
        //        case 8:
        //        case 9:
        //            return Enums.RetrieveQuarterType.Q21;
        //        case 10:
        //        case 11:
        //        case 12:
        //            return Enums.RetrieveQuarterType.Q22;
        //    }
        //    throw new NotSupportedException("1~12사이의 숫자를 입력해야 합니다.");
        //}

        /// <summary>
        /// 월별 조건에서 해당월에 대한 숫자를 가져 온다.
        /// </summary>
        public int GetMonthValue(RetrieveMonthType enumMonth)
        {
            return (int)enumMonth;
        }

        #endregion
    }
}
