using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using mshtml;

namespace SiS.Service.Scraping.Common
{
    /// <summary>
    /// HTML 문서를 계층형 구조로 분해해서 관리할 수 있도록 하는 클래스 입니다. 
    /// </summary>
    public class HtmlDocumentParsingManager
    {

        //private TaxSaver.Scraping.Common.ScrapingHelper _helper = new TaxSaver.Scraping.Common.ScrapingHelper();

        private HTMLDocumentClass _CurrentDocumentObj = null;

        private System.Collections.ObjectModel.Collection<HtmlParsingNode> _HtmlNodes = new System.Collections.ObjectModel.Collection<HtmlParsingNode>();

        #region 생성자

        /// <summary>
        /// 생성자 입니다. 
        /// </summary>
        public HtmlDocumentParsingManager()
        {

        }
        /// <summary>
        /// 생성자 입니다. 파싱할 Html 문자열을 매개변수로 받습니다. 
        /// </summary>
        /// <param name="htmlContents">html 문서를 나타내는 문자열</param>
        public HtmlDocumentParsingManager(string htmlContents)
        {
            this.HtmlParsing(htmlContents);
        } 
        #endregion

        #region 속성
        /// <summary>
        /// 파싱된 Html 노드 컬렉션 입니다. 
        /// </summary>
        public System.Collections.ObjectModel.Collection<HtmlParsingNode> HtmlNodes
        {
            get
            {
                return _HtmlNodes;
            }
        }

        /// <summary>
        /// 파싱에 사용된 현재 HtmlDocument 클래스를 가져 옵니다.
        /// </summary>
        public HTMLDocumentClass CurrentDomDocument
        {
            get
            {
                return _CurrentDocumentObj;
            }
        }
        #endregion

        #region HTML 파싱

        /// <summary>
        /// HTMLDocumentClass 객체의 인스턴스를 생성 합니다. 
        /// </summary>
        /// <param name="htmlDocumentText">HTMLDocumentClass 객체를 생성하기 위한 html 문자열</param>
        public HTMLDocumentClass CreateHtmlDocument(string htmlDocumentText)
        {
            HTMLDocumentClass pDoc3 = new HTMLDocumentClass();
            if (pDoc3 == null)
            {
                new Exception("HTMLDocumentClass 객체 생성 실패");
            }
            // 다음 코드 정상 작동 안함.
            //pDoc3.write(new object[] { htmlDocumentText });
            //pDoc3.close();


            // pDoc3.designMode = "on";
            object[] oPageText = { htmlDocumentText };
            IHTMLDocument2 oMyDoc = (IHTMLDocument2)pDoc3;
            //try
            //{
            //Console.WriteLine("Check2-{0}", oMyDoc.readyState);
            oMyDoc.designMode = "on";
            oMyDoc.expando = true;
            oMyDoc.write(oPageText);
            //Console.WriteLine("Check2-{0}", oMyDoc.readyState);
            oMyDoc.close();
            
            return pDoc3;

        }
        /// <summary>
        /// html 문서를 파싱 합니다. 
        /// </summary>
        /// <param name="htmlText">html문서(문자열)</param>
        public void HtmlParsing(string htmlText)
        {
            _HtmlNodes.Clear();
            mshtml.HTMLDocumentClass htmldoc = CreateHtmlDocument(htmlText);
            _CurrentDocumentObj = htmldoc;
            if (htmldoc.hasChildNodes())
            {

                IHTMLDOMNode hnode = htmldoc.firstChild;
                if (hnode != null)
                {
                    // 재귀 호출 파싱
                    HtmlParsing(hnode, null, _HtmlNodes);
                }

            }
        }

        /// <summary>
        /// html문서를 파싱합니다. 매개변수로 HTMLDocumentClass 객체가 사용됩니다. 
        /// </summary>
        /// <param name="htmldoc">HTMLDocumentClass 타입의 객체</param>
        public void HtmlParsing(mshtml.HTMLDocumentClass htmldoc)
        {
            _HtmlNodes.Clear();
            _CurrentDocumentObj = htmldoc;
            if (htmldoc.hasChildNodes())
            {

                IHTMLDOMNode hnode = htmldoc.firstChild;
                if (hnode != null)
                {
                    // 재귀 호출 파싱
                    HtmlParsing(hnode, null, _HtmlNodes);
                }

            }
        }

        // html dom 객체 파싱을 위한 메서드 
        private void HtmlParsing(mshtml.IHTMLDOMNode hnode, HtmlParsingNode parentNode, Collection<HtmlParsingNode> nodes)
        {
            while (hnode != null)
            {
                //IHTMLElement htmlEle = hnode as IHTMLElement;
                HtmlParsingNode node = new HtmlParsingNode();
                node.TagName = hnode.nodeName;
                node.HtmlDOMElement = hnode;
                node.ParentNode = parentNode;

                if (hnode.nodeName == "#text")
                {
                    try
                    {
                        node.ID = "";
                        node.Name = "";
                        node.OuterHtml = hnode.nodeValue.ToString();
                        node.OuterText = hnode.nodeValue.ToString();
                        node.InnerText = hnode.nodeValue.ToString();
                        node.InnerHtml = hnode.nodeValue.ToString();
                    }
                    catch (Exception ex)
                    {
                        node.OuterHtml = ex.Message;
                    }
                }
                else
                {
                    var htmlEle = hnode as IHTMLElement;
                    if (htmlEle != null)
                    {
                        node.ID = htmlEle.id == null ? "" : htmlEle.id;

                        var name = htmlEle.getAttribute("name");

                        if (name == DBNull.Value || name == null)
                        {
                            node.Name = "";
                        }
                        else 
                        {
                            node.Name = name.ToString();
                        }
                        node.OuterHtml = htmlEle.outerHTML;
                        node.InnerText = htmlEle.innerText;
                        node.OuterText = htmlEle.outerText;
                        node.InnerHtml = htmlEle.innerHTML;
                    }
                }
                nodes.Add(node);

                if (hnode.hasChildNodes())
                {
                    // 재귀 호출 파싱
                    HtmlParsing(hnode.firstChild, node, node.ChildNodes);
                }

                hnode = hnode.nextSibling;
            }
        } 
        #endregion

        #region 노드 경로 가져오기
        /// <summary>
        /// 지정된 노드의 태그 이름 기준 인덱스 이름을 가져 옵니다. 
        /// </summary>
        /// <param name="node">이름을 가져올 노드</param>
        public string GetTagIndexName(HtmlParsingNode node)
        {
            string tagIndex = node.TagName;
            Collection<HtmlParsingNode> nodes = null;
            if (node.ParentNode == null)
            {
                nodes = _HtmlNodes;
                tagIndex = "/" + tagIndex;
            }
            else
            {
                nodes = node.ParentNode.ChildNodes;
            }
            int idx = 0;
            foreach (HtmlParsingNode bNode in nodes)
            {
                if (bNode.TagName == node.TagName)
                {
                    if (bNode == node)
                    {
                        return tagIndex + "." + idx.ToString();
                    }
                    idx += 1;
                }
            }

            return tagIndex;
        }

        /// <summary>
        /// 지정된 노드의 루트로부터 전체 경로 이름을 리턴 합니다. 
        /// </summary>
        /// <param name="node"></param>
        public string GetTagIndexFullName(HtmlParsingNode node)
        {
            string tagIndexName = GetTagIndexName(node);
            HtmlParsingNode parentNode = node.ParentNode;
            while (parentNode != null)
            {
                tagIndexName = GetTagIndexName(parentNode) + "/" + tagIndexName;
                parentNode = parentNode.ParentNode;
            }

            return tagIndexName;
        }

        /// <summary>
        /// 지정된 노드의 시작노드로부터 전체 경로 이름을 리턴 합니다. 
        /// </summary>
        /// <param name="startNode">시작노드</param>
        /// <param name="node">시작노드에서 경로를 구할 노드</param>
        public string GetTagIndexFullName(HtmlParsingNode startNode, HtmlParsingNode node)
        {
            string tagIndexName = GetTagIndexName(node);
            HtmlParsingNode parentNode = node.ParentNode;
            while (parentNode != null)
            {
                tagIndexName = GetTagIndexName(parentNode) + "/" + tagIndexName;
                if (parentNode == startNode)
                {
                    break;
                }
                parentNode = parentNode.ParentNode;
            }

            return tagIndexName;
        }
        #endregion

        #region 노드 탐색

        /// <summary>
        /// Name를 이용해 노드를 검색해 옵니다. 
        /// </summary>
        /// <param name="name">검색할 노드의 Name</param>
        public List<HtmlParsingNode> SelectNodesByName(string name)
        {
            List<HtmlParsingNode> findNodes = new List<HtmlParsingNode>();
            if (name.Length == 0) return findNodes;
            SelectNodesByName(findNodes, _HtmlNodes, name);
            return findNodes;
        }
        private void SelectNodesByName(List<HtmlParsingNode> findNodes, Collection<HtmlParsingNode> nodes, string name)
        {
            foreach (HtmlParsingNode node in nodes)
            {
                if (name.ToUpper() == node.Name.ToUpper()) findNodes.Add(node);
                if (node.ChildNodes.Count > 0)
                {
                    SelectNodesByName(findNodes, node.ChildNodes, name);
                }
            }
        }

        /// <summary>
        /// ID를 이용해 노드를 검색해 옵니다. 
        /// </summary>
        /// <param name="id">검색할 노드의 ID</param>
        public HtmlParsingNode SelectNodeById(string id)
        {
            if (id.Length == 0) return null;
            return SelectNodeById(_HtmlNodes, id);
        }

        private HtmlParsingNode SelectNodeById(Collection<HtmlParsingNode> nodes, string id)
        {
            foreach (HtmlParsingNode node in nodes)
            {
                if (id.ToUpper() == node.ID.ToUpper()) return node;
                if (node.ChildNodes.Count > 0)
                {
                    var findNode = SelectNodeById(node.ChildNodes, id);
                    if (findNode != null) return findNode;
                }
            }
            return null;
        }
        /// <summary>
        /// 지정된 경로의 노드를 찾습니다. 
        /// </summary>
        /// <param name="strXPath">노드를 찾을 경로</param>
        /// <returns>탐색된 HtmlParsingNode 를 리턴 합니다. </returns>
        public HtmlParsingNode SelectNode(string strXPath)
        {
            return SelectNode(_HtmlNodes, strXPath);
        }

        /// <summary>
        /// 지정된 노드 아래 경로의 노드를 찾습니다. 
        /// </summary>
        /// <param name="startNode">탐색을 시작할 노드</param>
        /// <param name="strXPath">노드를 찾을 경로</param>
        /// <returns>탐색된 HtmlParsingNode 를 리턴 합니다. </returns>
        public HtmlParsingNode SelectNode(HtmlParsingNode startNode, string strXPath)
        {
            return SelectNode(startNode.ChildNodes, strXPath);
        }

        /// <summary>
        /// 지정된 노드 아래 경로의 노드를 찾습니다. 
        /// </summary>
        /// <param name="nodes">탐색을 시작할 노드</param>
        /// <param name="strXPath">노드를 찾을 경로</param>
        /// <returns>탐색된 HtmlParsingNode 를 리턴 합니다. </returns>
        internal static HtmlParsingNode SelectNode(Collection<HtmlParsingNode> nodes, string strXPath)
        {
            strXPath = strXPath.ToUpper();
            HtmlParsingNode findNode = null;
            string[] levelPath = strXPath.Split('/');
            foreach (string nodeTagIndex in levelPath)
            {
                if (nodeTagIndex.Length == 0) continue;
                string tagName = nodeTagIndex.Split('.')[0];
                string strIndex = nodeTagIndex.Split('.')[1];

                findNode = SelectNode(nodes, tagName, int.Parse(strIndex));
                if (findNode == null)
                    return null;
                nodes = findNode.ChildNodes;
            }
            return findNode;
        }

        internal static HtmlParsingNode SelectNode(Collection<HtmlParsingNode> nodes, string tagName, int index)
        {
            int chkIndex = 0;
            foreach (var node in nodes)
            {
                if (node.TagName.ToUpper() == tagName.ToUpper())
                {
                    if (chkIndex == index)
                    {
                        return node;
                    }
                    chkIndex += 1;
                }
            }
            return null;
        }

        /// <summary>
        /// 지정된 태그 이름을 가지는 노드 리스트를 가져 옵니다. 자식 노드까지 탐색 합니다.
        /// </summary>
        /// <param name="tagName">태그 이름</param>
        /// <returns></returns>
        public List<HtmlParsingNode> SelectNodesByTagName(string tagName)
        {
            return SelectNodesByTagName(_HtmlNodes, tagName, true);
        }

        /// <summary>
        /// 지정된 태그 이름을 가지는 노드 리스트를 가져 옵니다. 
        /// </summary>
        /// <param name="tagName">태그 이름</param>
        /// <param name="includeChildNode">탐색시 자식노드를 포함할 지 여부 입니다. </param>
        /// <returns></returns>
        public List<HtmlParsingNode> SelectNodesByTagName(string tagName, bool includeChildNode)
        {
            return SelectNodesByTagName(_HtmlNodes, tagName, includeChildNode);
        }

        internal static List<HtmlParsingNode> SelectNodesByTagName(Collection<HtmlParsingNode> nodes, string tagName, bool includeChildNode)
        {
            List<HtmlParsingNode> findNodes = new List<HtmlParsingNode>();

            SelectNodesByTagName(findNodes, nodes, tagName, includeChildNode);

            return findNodes;
        }

        internal static void SelectNodesByTagName(List<HtmlParsingNode> findNodes, Collection<HtmlParsingNode> nodes, string tagName, bool includeChildNode)
        {
            foreach (var node in nodes)
            {
                if (node.TagName.ToUpper() == tagName.ToUpper())
                {
                    findNodes.Add(node);
                }
                if (node.ChildNodes.Count > 0 && includeChildNode)
                {
                    SelectNodesByTagName(findNodes, node.ChildNodes, tagName, includeChildNode);
                }
            }

        }

        #endregion
    }

    /// <summary>
    /// HTML 파싱 노드 입니다. 
    /// </summary>
    public class HtmlParsingNode
    {
        /// <summary>
        /// 태그의 name 속성의 값 입니다.
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// 태그의 id 속성의 값 입니다.
        /// </summary>
        public string ID { get; internal set; }
        private string _tagName = "";
        /// <summary>
        /// 태그이름 입니다.
        /// </summary>
        public string TagName
        {
            get { return _tagName; }
            internal set { _tagName = value.ToUpper(); }
        }
        /// <summary>
        /// HtmlDOMElement 객체 입니다.
        /// </summary>
        public mshtml.IHTMLDOMNode HtmlDOMElement { get; internal set; }
        /// <summary>
        /// 이 노드의 부모 노드 입니다.
        /// </summary>
        public HtmlParsingNode ParentNode { get; internal set; }
        private string _OuterHtml = "";
        private string _InnerText = "";
        private string _OuterText = "";
        private string _InnerHtml = "";
        /// <summary>
        /// OuterHtml을 가져 옵니다. 
        /// </summary>
        public string OuterHtml
        {
            get { return string.IsNullOrEmpty(_OuterHtml) ? "" : _OuterHtml; }
            internal set { _OuterHtml = value; }
        }
        /// <summary>
        /// OuterText를 가져 옵니다. 
        /// </summary>
        public string OuterText
        {
            get { return string.IsNullOrEmpty(_OuterText) ? "" : _OuterText; }
            internal set { _OuterText = value; }
        }
        /// <summary>
        /// InnerText를 가져 옵니다. 
        /// </summary>
        public string InnerText
        {
            get { return string.IsNullOrEmpty(_InnerText) ? "" : _InnerText; }
            internal set { _InnerText = value; }
        }
        /// <summary>
        /// InnerHtml을 가져 옵니다. 
        /// </summary>
        public string InnerHtml
        {
            get { return string.IsNullOrEmpty(_InnerHtml) ? "" : _InnerHtml; }
            set { _InnerHtml = value; }
        }

        private System.Collections.ObjectModel.Collection<HtmlParsingNode> _HtmlNodes = new System.Collections.ObjectModel.Collection<HtmlParsingNode>();
        /// <summary>
        /// 자식 노드 입니다. 
        /// </summary>
        public System.Collections.ObjectModel.Collection<HtmlParsingNode> ChildNodes
        {
            get
            {
                return _HtmlNodes;
            }
        }

        #region 노드 탐색 

        /// <summary>
        /// 지정된 경로의 노드를 찾습니다. 
        /// </summary>
        /// <param name="strXPath">노드를 찾을 경로</param>
        /// <returns>탐색된 HtmlParsingNode 를 리턴 합니다. </returns>
        public HtmlParsingNode SelectNode(string strXPath)
        {
            return HtmlDocumentParsingManager.SelectNode(_HtmlNodes, strXPath);
        }

        /// <summary>
        /// 지정된 태그 이름을 가지는 노드 리스트를 가져 옵니다. 자식 노드는 탐색하지 않습니다.
        /// </summary>
        /// <param name="tagName">태그 이름</param>
        /// <returns></returns>
        public List<HtmlParsingNode> SelectNodesByTagName(string tagName)
        {
            return HtmlDocumentParsingManager.SelectNodesByTagName(_HtmlNodes, tagName, true);
        }

        /// <summary>
        /// 지정된 태그 이름을 가지는 노드 리스트를 가져 옵니다. 
        /// </summary>
        /// <param name="tagName">태그 이름</param>
        /// <param name="includeChildNode">탐색시 자식노드를 포함할 지 여부 입니다. </param>
        /// <returns></returns>
        public List<HtmlParsingNode> SelectNodesByTagName(string tagName, bool includeChildNode)
        {
            return HtmlDocumentParsingManager.SelectNodesByTagName(_HtmlNodes, tagName, includeChildNode);
        }
        #endregion

    }



}
