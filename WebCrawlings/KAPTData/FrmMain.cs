using MSXML;
using Newtonsoft.Json.Linq;
using SiS.Service.Scraping.Common;
using SiS.Service.Scraping.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using SiS.Framework;
using DevExpress.XtraGrid.Columns;

namespace KAPTData
{
    public partial class FrmMain : SiS.Framework.Extension.Xtra.SiSXtraForm
    {
        Dictionary<string, string> colMatch = new Dictionary<string, string>();
        string cols = "단지코드,법정동주소,도로명주소,분양형태,사용승인일,동수,세대수,아파트명,관리사무소전화,관리사무소팩스,평형60이하,평형60-85,평형85-135,평형135이상,연면적,복도유형,교육시설";

        ScrapingHelper _ScrapingHelper = new ScrapingHelper();
        string reqUrl = @"http://www.k-apt.go.kr/kaptinfo/getKaptInfo_detail.do";

        public FrmMain()
        {
            InitializeComponent();

            for (int i = 2012; i <= DateTime.Today.Year; i++)
            {
                cbYear.Properties.Items.Add(i);
            }

            cbeMonth.EditValue = DateTime.Today.Month - 1;
            cbYear.EditValue = DateTime.Today.Year;
            
            MonthListBinding(DateTime.Today.Year);
                       
            foreach (string col in cols.Split(','))
            {
                aptDT.Columns.Add(col);
            }

            colMatch.Add("CODE_SALE", "분양형태");
            colMatch.Add("KAPT_USEDATE", "사용승인일");
            colMatch.Add("KAPT_DONG_CNT", "동수");
            colMatch.Add("KAPT_NAME", "아파트명");
            colMatch.Add("KAPT_TEL", "관리사무소전화");
            colMatch.Add("KAPT_FAX", "관리사무소팩스");

            colMatch.Add("KAPT_TAREA", "연면적");
            colMatch.Add("CODE_HALL", "복도유형");
            colMatch.Add("EDUCATION_FACILITY", "교육시설");
            this.CenterToScreen();

            xgList.DataSource = aptDT;

            //foreach (GridColumn col in xgListView.Columns)
            //{
            //    if (col.FieldName.StartsWith("관리사무소"))
            //    {
            //        col.ColumnEdit = rtxtTelNo;
            //    }
            //}
        }

        private void MonthListBinding(int yearValue)
        {
            int maxMonth = 12;
            int currentValue = (int)cbeMonth.EditValue;
            cbeMonth.Properties.Items.Clear();
            if (DateTime.Today.Year == yearValue)
            {
                maxMonth = DateTime.Today.Month-1;
            }
            if (maxMonth < currentValue) currentValue = maxMonth;
            for(int i = 0; i <= maxMonth; i++)
            {
                cbeMonth.Properties.Items.Add(i);
            }
            cbeMonth.EditValue = currentValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            foreach (string col in cols.Split(','))
            {
                dt.Columns.Add(col);
            }
            Dictionary<string, string> mc = new Dictionary<string, string>();
            mc.Add("CODE_SALE", "분양형태");
            mc.Add("KAPT_USEDATE", "사용승인일");
            mc.Add("KAPT_DONG_CNT", "동수");
            mc.Add("KAPT_NAME", "아파트명");
            mc.Add("KAPT_TEL", "관리사무소전화");
            mc.Add("KAPT_FAX", "관리사무소팩스");

            mc.Add("KAPT_TAREA", "연면적");
            mc.Add("CODE_HALL", "복도유형");
            mc.Add("EDUCATION_FACILITY", "교육시설");


            var row = dt.NewRow();
            // 소하2단지: A42370103
            // 소하3단지: A42370102
            var contents = GetResponseContentsByXMLHttp(reqUrl, "kapt_code=A42370102");

            var json2 = JObject.Parse(contents);

            //this.richTextBox1.Text = contents;

            var baseInfo = json2.SelectToken("resultMap_kapt");
            var addresInfo = json2.SelectToken("resultMap_kapt_addrList");
            var areaInfo = json2.SelectToken("resultMap_kapt_areacnt");


            //Dictionary<string, string> dataList = new Dictionary<string, string>();
            //foreach (JProperty item in baseInfo.AsQueryable())
            //{
            //    dataList.Add(item.Name, item.Value.ToString());
            //    if (mc.ContainsKey(item.Name))
            //    {
            //        var match = mc[item.Name];
            //        row[mc[item.Name]] = item.Value;
            //    }
            //}

            //this.richTextBox1.ResetText();
            //foreach (var itm in dataList)
            //{
            //    this.richTextBox1.AppendText(itm.Key + ":" + itm.Value + "\r\n");
            //}

            string addr1 = addresInfo.First.SelectToken("ADDR").ToString();
            string addr2 = addresInfo.Last.SelectToken("ADDR").ToString();
            //this.richTextBox1.AppendText("법정동주소:" + addr1 + "\r\n");
            //this.richTextBox1.AppendText("도로명주소:" + addr2 + "\r\n");



            // 기본정보
            foreach (JProperty item in baseInfo.AsQueryable())
            {
                if (mc.ContainsKey(item.Name))
                {
                    var match = mc[item.Name];
                    row[mc[item.Name]] = item.Value;
                }
            }
            // 주소
            row["법정동주소"] = addr1;
            row["도로명주소"] = addr2;

            // 면적


            dt.Rows.Add(row);

            int totcnt = 0;
            foreach (var item in areaInfo.AsQueryable())
            {
                string areaGbn = item.SelectToken("AREA_GBN").ToString();
                int cnt = 0;
                if (int.TryParse(item.SelectToken("KAPTDA_CNT").ToString(), out cnt))
                {
                    totcnt += cnt;
                }
                switch (areaGbn)
                {
                    case "1":
                        row["평형60이하"] = cnt.ToString();
                        break;
                    case "2":
                        row["평형60-85"] = cnt.ToString();
                        break;
                    case "3":
                        row["평형85-135"] = cnt.ToString();
                        break;
                    case "4":
                        row["평형135이상"] = cnt.ToString();
                        break;
                    default:
                        break;
                }
                //this.richTextBox1.AppendText(item.ToString() + "\r\n");
            }

            row["세대수"] = totcnt;
            xgList.DataSource = dt;

        }

        private string GetResponseContentsByXMLHttp(string requestURL, string postData)
        {
            var req = _ScrapingHelper.CreateXMLHTTPRequest(HttpRequestMethodType.POST, requestURL);

            _ScrapingHelper.SetRequestHeader(req, "Accept: application/json, text/javascript, */*; q=0.01");

            _ScrapingHelper.SetRequestHeader(req, @"Connection: Keep-Alive");
            _ScrapingHelper.SetRequestHeader(req, @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.106 Whale/2.8.107.16 Safari/537.36");
            _ScrapingHelper.SetRequestHeader(req, @"Pragma: no-cache");

            // ==================
            _ScrapingHelper.SetRequestHeader(req, @"Accept-Language: ko-KR,ko;q=0.9,en-US;q=0.8,en;q=0.7");
            _ScrapingHelper.SetRequestHeader(req, @"Accept-Encoding: gzip, deflate");
            _ScrapingHelper.SetRequestHeader(req, @"Content-Type: application/x-www-form-urlencoded; charset=UTF-8");
            //Helper.SetRequestHeader(req, "DNT: 1");

            //Helper.SetRequestHeader(req, "Referer: https://www.kebhana.com/foreign/index.do?contentUrl=/foreign/rate/wpfxd651_01i.do");
            //Referer: https://www.kebhana.com/foreign/index.do?contentUrl=/foreign/rate/wpfxd651_01i.do
            return XmlHttpSend(req, postData);
            //CreateXMLHTTPRequest

        }

        private string XmlHttpSend(XMLHTTPRequest request, string data)
        {
            if (string.IsNullOrEmpty(data))
                request.send();
            else
                request.send(data);

            int responsedStatus = request.status;

            if (200 != responsedStatus)
                throw new ScrapingException("Error: " + request.statusText);

            return request.responseText;
        }

        DataTable aptDT = new DataTable();

        private void button2_Click(object sender, EventArgs e)
        {
            GetDongDangiInformation();

        }

        // 조회된 단지리스트의 단지 정보를 가져 온다. 
        private void GetDongDangiInformation()
        {
            List<string> codes = new List<string>();
            var kaptCodeDT = xgKAPT.DataSource as DataTable;
            int term = 1000;
            int.TryParse(textEdit1.Text, out term);
            if (kaptCodeDT.Rows.Count == 0)
            {
                MessageBox.Show("조회할 아파트 코드가 없습니다.");
                return;
            }
            foreach (DataRow codeRow in kaptCodeDT.Select())
            {
                string code = codeRow["KAPT_CODE"].ToString();
                if (string.IsNullOrWhiteSpace(code)) continue;
                codes.Add(code);
            }
            using (FrmProgress dlogPrgs = new FrmProgress())
            {
                int totCnt = codes.Count;
                int workCnt = 0;
                dlogPrgs.DisableCancel();
                dlogPrgs.ShowCenter(this);
                this.richTextBox1.Clear();
                foreach (string code in codes)
                {                    
                    workCnt++;
                    if ( workCnt > 1 ) System.Threading.Thread.Sleep(term);
                    int percent = (workCnt * 100 / totCnt);
                    dlogPrgs.Text = percent.ToString() + "% 완료";
                    dlogPrgs.SetProgress(percent);
                    GetAptData(code);                    
                }
            }


            xgListView.BestFitColumns();
        }

        private string TelFormat(string telNo)
        {
            if (telNo.StartsWith("02"))
            {
                if (telNo.Length == 9)
                {
                    telNo = $"{telNo.Substring(0, 2)}-{telNo.Substring(2, 3)}-{telNo.Substring(5)}";
                }

                if (telNo.Length == 10)
                {
                    telNo = $"{telNo.Substring(0, 2)}-{telNo.Substring(2, 4)}-{telNo.Substring(6)}";
                }
            }
            else
            {
                if (telNo.Length == 10)
                {
                    telNo = $"{telNo.Substring(0, 3)}-{telNo.Substring(3, 3)}-{telNo.Substring(6)}";
                }

                if (telNo.Length == 11)
                {
                    telNo = $"{telNo.Substring(0, 3)}-{telNo.Substring(3, 4)}-{telNo.Substring(7)}";
                }
            }
            return telNo;
        }

        private void GetAptData(string aptCode)
        {
            var row = aptDT.NewRow();
            row["단지코드"] = aptCode;
            var contents = GetResponseContentsByXMLHttp(reqUrl, "kapt_code=" + aptCode);
            //this.richTextBox1.AppendText(contents);
            //this.richTextBox1.AppendText("==================================================== \r\n");

            var json2 = JObject.Parse(contents);
            var baseInfo = json2.SelectToken("resultMap_kapt");
            var addresInfo = json2.SelectToken("resultMap_kapt_addrList");
            var areaInfo = json2.SelectToken("resultMap_kapt_areacnt");

            string addr1 = addresInfo.First.SelectToken("ADDR").ToString();
            string addr2 = addresInfo.Last.SelectToken("ADDR").ToString();
            
            // 기본정보
            foreach (JProperty item in baseInfo.AsQueryable())
            {
                if (colMatch.ContainsKey(item.Name))
                {
                    var match = colMatch[item.Name];
                    row[colMatch[item.Name]] = item.Value;
                }
            }
            
            row["관리사무소전화"] = TelFormat(row["관리사무소전화"].ToString().Replace("-", ""));
            row["관리사무소팩스"] = TelFormat(row["관리사무소팩스"].ToString().Replace("-", ""));
            
            // 주소
            row["법정동주소"] = addr1;
            row["도로명주소"] = addr2;

            // 면적

            int totcnt = 0;
            foreach (var item in areaInfo.AsQueryable())
            {
                string areaGbn = item.SelectToken("AREA_GBN").ToString();
                int cnt = 0;
                if (int.TryParse(item.SelectToken("KAPTDA_CNT").ToString(), out cnt))
                {
                    totcnt += cnt;
                }
                switch (areaGbn)
                {
                    case "1":
                        row["평형60이하"] = cnt.ToString();
                        break;
                    case "2":
                        row["평형60-85"] = cnt.ToString();
                        break;
                    case "3":
                        row["평형85-135"] = cnt.ToString();
                        break;
                    case "4":
                        row["평형135이상"] = cnt.ToString();
                        break;
                    default:
                        break;
                }
                //this.richTextBox1.AppendText(item.ToString() + "\r\n");
            }

            row["세대수"] = totcnt;


            aptDT.Rows.Add(row);
        }

        private void FillSido()
        {
            
            var dt = GetJuSoList("", "SIDO");
            lueSiDo.Properties.DataSource = dt;
            lueSiDo.Properties.ValueMember = "CODE";
            lueSiDo.Properties.DisplayMember = "CODE_VALUE";
        }

        private void BindingGuGun(string code)
        {
            //bjd_code=41&bjd_gbn=SGG
            var dt = GetJuSoList(code, "SGG");
            lueGuGun.Properties.DataSource = dt;
            lueGuGun.Properties.ValueMember = "CODE";
            lueGuGun.Properties.DisplayMember = "CODE_VALUE";
        }

        private void BindgingDong(string code)
        {
            //EMD
            var dt = GetJuSoList(code, "EMD");
            lueDong.Properties.DataSource = dt;
            lueDong.Properties.ValueMember = "CODE";
            lueDong.Properties.DisplayMember = "CODE_VALUE";
        }

        
        private DataTable GetJuSoList(string bjd_code, string bjd_gbn)
        {

            string bjdurl = @"http://www.k-apt.go.kr/cmmn/bjd/getBjdList.do";
            string postData = $"bjd_code={bjd_code}&bjd_gbn={bjd_gbn}";
            var contents = GetResponseContentsByXMLHttp(bjdurl, postData);
            //resultList
            //this.richTextBox1.Text = contents;
            var sidoJson = JObject.Parse(contents);
            var sidoList = sidoJson.SelectToken("resultList");
            DataTable dt = new DataTable();
            dt.Columns.Add("CODE");
            dt.Columns.Add("CODE_VALUE");
            foreach (var item in sidoList.AsQueryable())
            {
                var row = dt.NewRow();
                string sidoCode = item.SelectToken("CODE").ToString();
                string sidoName = item.SelectToken("CODE_VALUE").ToString();
                row["CODE"] = sidoCode;
                row["CODE_VALUE"] = sidoName;
                dt.Rows.Add(row);

            }
            return dt;
        }

        private DataTable GetDanjiList(string bjd_code, string search_date)
        {
            string reqUrl = "http://www.k-apt.go.kr/kaptinfo/getKaptList.do";
            string postData = $"bjd_code={bjd_code}&search_date={search_date}";
            var contents = GetResponseContentsByXMLHttp(reqUrl, postData);
            //resultList
            //this.richTextBox1.Text = contents;
            var sidoJson = JObject.Parse(contents);
            var sidoList = sidoJson.SelectToken("resultList");
            DataTable dt = new DataTable();
            dt.Columns.Add("KAPT_CODE"); // =A42981401");
            dt.Columns.Add("KAPT_NAME"); // =도창에이스1차");
            //dt.Columns.Add("BJD_CODE"); // =4139010900");
            dt.Columns.Add("BJD_NAME"); // =경기도 시흥시 도창동");
            foreach (var item in sidoList.AsQueryable())
            {
                var row = dt.NewRow();                
                row["KAPT_CODE"] = item.SelectToken("KAPT_CODE").ToString();
                row["KAPT_NAME"] = item.SelectToken("KAPT_NAME").ToString();
                row["BJD_NAME"] = item.SelectToken("BJD_NAME").ToString();
                dt.Rows.Add(row);

            }
            return dt;
        }

        private void lueSiDo_EditValueChanged(object sender, EventArgs e)
        {
            // 시도 변경 
            lueDong.EditValue = null;
            lueGuGun.EditValue = null;
            lueDong.Properties.DataSource = null;

            string sido = Func.NVL(lueSiDo.EditValue, "").ToString();
            if (string.IsNullOrEmpty(sido) == false)
            {
                BindingGuGun(sido);
            }
        }

        private void lueGuGun_EditValueChanged(object sender, EventArgs e)
        {
            // 구군 변경
            lueDong.EditValue = null;
            string sido = Func.NVL(lueSiDo.EditValue, "").ToString();
            string gugun = Func.NVL(lueGuGun.EditValue, "").ToString();
            string code = sido + gugun;
            if (string.IsNullOrEmpty(code) == false)
            {
                BindgingDong(code);

            }
        }

        private void lueDong_EditValueChanged(object sender, EventArgs e)
        {
            // 동 변경
            // 아파트 단지 가져오기 
            string sidoCode = Func.NVL(lueSiDo.EditValue,"").ToString();
            string gugunCode = Func.NVL( lueGuGun.EditValue,"").ToString();
            string dongCode = Func.NVL(lueDong.EditValue, "").ToString();

            string bjdCode = sidoCode + gugunCode + dongCode;
            if (bjdCode.Length != 8) return;

            string searchDate = cbYear.EditValue.ToString() + ((int)cbeMonth.EditValue).ToString("00");

            var listDT = GetDanjiList(bjdCode, searchDate);
            xgKAPT.DataSource = listDT;
            xgKAPTView.BestFitColumns();
        }

        private void cbYear_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 발생년월 기준 년도 선택 변경
            int yearValue = (int)cbYear.EditValue;
            MonthListBinding(yearValue);

        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            this.FillSido();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            aptDT.Clear();
        }

        private void chkCopyOption_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCopyOption.Checked)
            {
                xgListView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                xgListView.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        private void btnGugunAll_Click(object sender, EventArgs e)
        {
            GetCurrentGugunAllDangi();

        }

        // 현재 구군의 모든 단지 조회
        private void GetCurrentGugunAllDangi()
        {
            int term = 1000;
            int.TryParse(textEdit1.Text, out term);

            // 현재 구군 아파트 단지 가져오기 
            string sidoCode = Func.NVL(lueSiDo.EditValue, "").ToString();
            string gugunCode = Func.NVL(lueGuGun.EditValue, "").ToString();

            if (string.IsNullOrEmpty(sidoCode) || string.IsNullOrEmpty(gugunCode))
            {
                MessageBox.Show("시도 및 구군을 선택해야 합니다.");
                return;
            }

            // 조회된 모든 동
            DataTable dongDT = lueDong.Properties.DataSource as DataTable;
            DataTable dangiDT = new DataTable();
            using (FrmProgress dlogPrgs = new FrmProgress())
            {
                int totCnt = dongDT.Rows.Count;
                int workCnt = 0;
                dlogPrgs.DisableCancel();
                dlogPrgs.ShowCenter(this);

                foreach (DataRow row in dongDT.Select())
                {
                    workCnt++;
                    int percent = (workCnt * 100 / totCnt);
                    dlogPrgs.Text = percent.ToString() + "% 완료";
                    dlogPrgs.SetProgress(percent);

                    System.Threading.Thread.Sleep(term);
                    //string dongCode = Func.NVL(lueDong.EditValue, "").ToString();
                    string dongCode = row["CODE"].ToString();
                    string bjdCode = sidoCode + gugunCode + dongCode;
                    if (bjdCode.Length != 8) continue;
                    string searchDate = cbYear.EditValue.ToString() + ((int)cbeMonth.EditValue).ToString("00");
                    var listDT = GetDanjiList(bjdCode, searchDate);

                    dangiDT.Load(listDT.CreateDataReader());
                }
            }
           
            xgKAPT.DataSource = dangiDT;
            xgKAPTView.BestFitColumns();
        }
    }
}
