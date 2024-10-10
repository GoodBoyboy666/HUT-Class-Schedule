using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Security.Principal;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.IO;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Data;
using System.Xml.Linq;

namespace HUT_Class_Schedule
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        struct Course
        {
            public string courseName;
            public string courseInfo;
        }

        //初始化全局变量
        HttpClient client = new HttpClient();
        bool FINISH = false;//成功请求标识位
        string kbjcmsid = "";//课表ID（？）
        string xnxq01id = "";//学年

        private async void Get_Schedule_Click(object sender, EventArgs e)
        {

            statusLabel.Text = "开始请求";
            statusBar.Visible = true;
            statusBar.Value = 0;

            bool isError = false;//身份验证通过标识位
            if (FINISH == false)
            {
                //身份验证
                isError = await Authentication();
            }
            else
            {
                statusBar.Value += 20;
            }

            //Get请求课表参数
            string kbURL = "";
            if (isError != true)
                kbURL = await GetAllkbParam();


            //请求与解析课表
            if (kbURL != "")
            {
                if (await GetSchedule(kbURL))
                {
                    FINISH = true;
                    textBox_Account.Enabled = false;
                    textBox_Password.Enabled = false;
                    logout.Visible = true;
                }
            }

            //保存配置
            SaveSettings();

            statusLabel.Text = "完成";

            statusBar.Value += 10;

        }

        /// <summary>
        /// 获取课表
        /// </summary>
        /// <param name="kbURL">课表URL</param>
        /// <returns>是否成功获取</returns>
        private async Task<bool> GetSchedule(string kbURL)
        {
            statusLabel.Text = "请求课表中……";

            HttpResponseMessage response = await client.GetAsync(kbURL);
            string result = await response.Content.ReadAsStringAsync();

            statusBar.Value += 20;

            //解析课表HTML
            if (ParseSchedule(result))
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 获取所有课表请求参数
        /// </summary>
        /// <returns>课表链接</returns>
        private async Task<string> GetAllkbParam()
        {
            string kbURL = "";

            statusLabel.Text = "获取课表请求参数中……";
            if (FINISH == false)
            {
                HttpResponseMessage reKbParam = await client.GetAsync("http://jwxt.hut.edu.cn/jsxsd/framework/xsMainV_new.htmlx?t1=1");
                string htmlKbParam = await reKbParam.Content.ReadAsStringAsync();

                //Post请求课表HTML（不要问我变量名为什么这么奇怪，问学校）
                KbParam kbParam = GetKbParam(htmlKbParam);
                string zhouciSelected = kbParam.zhouciSelected;
                kbjcmsid = kbParam.kbjcmsid;
                xnxq01id = kbParam.xnxq01id;
                kbURL = "http://jwxt.hut.edu.cn/jsxsd/framework/mainV_index_loadkb.htmlx?zc=" + zhouciSelected + "&kbjcmsid=" + kbjcmsid + "&xnxq01id=" + xnxq01id + "&xswk=false";

                //将周次写入combox
                ZhouCicomboBox.Items.Clear();
                foreach (string zhouci in kbParam.zhouci)
                    ZhouCicomboBox.Items.Add(zhouci);
                ZhouCicomboBox.SelectedItem = zhouciSelected;
            }
            else
            {
                kbURL = "http://jwxt.hut.edu.cn/jsxsd/framework/mainV_index_loadkb.htmlx?zc=" + ZhouCicomboBox.SelectedItem + "&kbjcmsid=" + kbjcmsid + "&xnxq01id=" + xnxq01id + "&xswk=false";
            }


            if (ZhouCicomboBox?.SelectedItem?.ToString() == "" || kbjcmsid == "" || xnxq01id == "")
            {
                MessageBox.Show("解析课表参数失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                kbURL = "";
            }
            else
            {
                statusBar.Value += 20;
            }
            return kbURL;
        }

        #region 2.0.2版认证接口

        /// <summary>
        /// 身份认证
        /// </summary>
        /// <returns><是否完成身份认证/returns>
        //private async Task<bool> Authentication()
        //{
        //    //获取SESS
        //    await client.GetAsync("http://218.75.197.123:83/");

        //    statusLabel.Text = "获取SESS中……";
        //    statusBar.Value += 10;


        //    HttpContent empty = new StringContent("");
        //    HttpResponseMessage SESS = await client.PostAsync("http://218.75.197.123:83/Logon.do?method=logon&flag=sess", empty);
        //    string SESStext = "";
        //    for (int i = 0; i < 3; i++)
        //    {
        //        SESStext = await SESS.Content.ReadAsStringAsync();
        //        if (!(SESStext.Length > 100 || SESStext == "" || SESStext == null))
        //            break;
        //        SESStext = "";
        //    }

        //    if (SESStext == "")
        //    {
        //        MessageBox.Show("获取SESS失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    statusBar.Value += 20;

        //    //处理账户密码加密
        //    string encodedString = "";
        //    if (SESStext != "")
        //    {
        //        statusLabel.Text = "处理账号密码中……";
        //        encodedString = EncodeString(SESStext, textBox_Account.Text, textBox_Password.Text);
        //        statusBar.Value += 10;
        //    }


        //    //组装Post请求Body
        //    string authReslutString = "";
        //    bool isError = false;
        //    if (encodedString != "")
        //    {
        //        statusLabel.Text = "身份验证中……";
        //        string postData = "loginMethod=logon&userlanguage=0&userAccount=" + textBox_Account.Text + "&userPassword=&encoded=" + WebUtility.UrlEncode(encodedString);

        //        //Post身份验证
        //        HttpContent content = new StringContent(postData);
        //        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        //        HttpResponseMessage authReslut = await client.PostAsync("http://218.75.197.123:83/Logon.do?method=logon", content);
        //        authReslutString = await authReslut.Content.ReadAsStringAsync();

        //        //检测身份验证结果
        //        HtmlDocument authReslutHTML = new HtmlDocument();
        //        authReslutHTML.LoadHtml(authReslutString);
        //        var authReslutNode = authReslutHTML.DocumentNode.SelectSingleNode("//font[@id='showMsg']");
        //        if (authReslutNode != null)
        //        {
        //            MessageBox.Show("身份验证失败！\n" + authReslutNode.InnerText.Trim(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            isError = true;
        //        }
        //        else
        //        {
        //            statusBar.Value += 20;
        //        }
        //    }
        //    else
        //    {
        //        isError = true;
        //    }

        //    return isError;
        //}

        #endregion

        private async Task<bool> Authentication()
        {
            await client.GetAsync("http://jwxt.hut.edu.cn/jsxsd/");

            //处理账户密码加密
            string encodingAccount = EncodeString(textBox_Account.Text);
            string encodingPassword = EncodeString(textBox_Password.Text);
            //组装Post请求Body
            string postData = "loginMethod=LoginToXk&userlanguage=0&userAccount=" + textBox_Account.Text + "&userPassword=&encoded=" + WebUtility.UrlEncode(encodingAccount + "%%%" + encodingPassword);

            //Post身份验证
            HttpContent content = new StringContent(postData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage authReslut = await client.PostAsync("http://jwxt.hut.edu.cn/jsxsd/xk/LoginToXk", content);

            //检测身份验证结果
            bool isError = false;
            HtmlDocument authReslutHTML = new HtmlDocument();
            string authReslutString = await authReslut.Content.ReadAsStringAsync();
            authReslutHTML.LoadHtml(authReslutString);
            var authReslutNode = authReslutHTML.DocumentNode.SelectSingleNode("//font[@id='showMsg']");
            if (authReslutNode != null)
            {
                MessageBox.Show("身份验证失败！\n" + authReslutNode.InnerText.Trim(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isError = true;
            }
            else
            {
                statusBar.Value += 20;
            }

            return isError;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        private void SaveSettings()
        {
            statusLabel.Text = "保存配置文件中……";

            //账户密码写入配置文件
            string path = "account.dat";
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(EncodeString(textBox_Account.Text));
                    writer.WriteLine(EncodeString(textBox_Password.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误： " + ex.Message);
            }
        }

        /// <summary>
        /// 解析课表HTML
        /// </summary>
        /// <param name="result">课表HTML</param>
        private bool ParseSchedule(string result)
        {
            statusLabel.Text = "解析课表中……";
            var html = new HtmlDocument();
            html.LoadHtml(result);
            var table = html.DocumentNode.SelectSingleNode("//table");
            if (table != null)
            {
                //定义课表数组
                var tableData = new List<List<Course>>();

                // 获取课表数据
                var tbody = table.SelectSingleNode(".//tbody");
                var rows = tbody.SelectNodes(".//tr");

                //遍历课表
                foreach (var row in rows)
                {
                    var cells = row.SelectNodes(".//td");
                    var rowData = new List<Course>();

                    for (int i = 0; i < cells.Count; i++)
                    {
                        var cell = cells[i].SelectSingleNode(".//div/ul/li");
                        var head = cells[i].SelectSingleNode(".//div/div[@class='index-title']");
                        Course course = new Course();
                        if (cell != null)
                        {

                            course.courseName = cell.SelectSingleNode(".//div[@class='qz-hasCourse-title qz-ellipse']").InnerText.Trim();

                            // 课程详细信息
                            var infonodes = cell.SelectNodes(".//div[contains(@class, 'qz-hasCourse-detaillists')]/div");
                            foreach (var info in infonodes)
                            {
                                course.courseInfo += info.InnerText.Trim() + "\n";
                            }
                            rowData.Add(course);
                        }
                        else if (head != null)
                        {
                            course.courseName = head.InnerText.Trim() + "\n";
                            course.courseInfo = head.InnerText.Trim() + "\n" + cells[i].SelectSingleNode(".//div/div[@class='index-detailtext']").InnerText.Trim() + "\n" + cells[i].SelectSingleNode(".//div/div[@class='index-detailtext qz-flex-row']/span").InnerText.Trim();
                            rowData.Add(course);
                        }
                        else
                        {
                            course.courseName = " ";
                            course.courseInfo = "N/A";
                            rowData.Add(course);
                        }
                    }

                    tableData.Add(rowData);
                }

                //清空DataGirdView
                Schedule.Rows.Clear();

                // 展示表格数据
                int rowIndex = 0;
                foreach (var data in tableData)
                {
                    Schedule.Rows.Add();
                    DataGridViewRow newRow = Schedule.Rows[rowIndex];
                    newRow.Cells["head"].Value = data[0].courseName;
                    newRow.Cells["head"].ToolTipText = data[0].courseInfo;
                    newRow.Cells["Monday"].Value = data[1].courseName;
                    newRow.Cells["Monday"].ToolTipText = data[1].courseInfo;
                    newRow.Cells["Tuesday"].Value = data[2].courseName;
                    newRow.Cells["Tuesday"].ToolTipText = data[2].courseInfo;
                    newRow.Cells["Wednesday"].Value = data[3].courseName;
                    newRow.Cells["Wednesday"].ToolTipText = data[3].courseInfo;
                    newRow.Cells["Thursday"].Value = data[4].courseName;
                    newRow.Cells["Thursday"].ToolTipText = data[4].courseInfo;
                    newRow.Cells["Friday"].Value = data[5].courseName;
                    newRow.Cells["Friday"].ToolTipText = data[5].courseInfo;
                    newRow.Cells["Saturday"].Value = data[6].courseName;
                    newRow.Cells["Saturday"].ToolTipText = data[6].courseInfo;
                    newRow.Cells["Sunday"].Value = data[7].courseName;
                    newRow.Cells["Sunday"].ToolTipText = data[7].courseInfo;
                    rowIndex++;
                }

                statusBar.Value = 90;
                return true;
            }
            else
            {
                MessageBox.Show("解析课表失败！");
                return false;
            }

        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="originalString">原始字符串</param>
        /// <returns>加密字符串</returns>
        private static string EncodeString(string originalString)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(originalString);
            string base64String = Convert.ToBase64String(bytes);
            return base64String;
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encodedString">加密字符串</param>
        /// <returns>原始字符串</returns>
        private static string DecodeSrting(string encodedString)
        {
            byte[] decodedBytes = Convert.FromBase64String(encodedString);
            string decodedString = System.Text.Encoding.UTF8.GetString(decodedBytes);
            return decodedString;
        }

        /// <summary>
        /// 密码加密算法
        /// </summary>
        /// <param name="dataStr">SEES</param>
        /// <param name="userAccount">学号</param>
        /// <param name="userPassword">密码</param>
        /// <returns>加密的字符</returns>
        private static string EncodeString(string dataStr, string userAccount, string userPassword)
        {
            if (dataStr == "")
            {
                return "";
            }
            else
            {
                string[] parts = dataStr.Split('#');
                string scode = parts[0];
                string sxh = parts[1];

                string code = userAccount + "%%%" + userPassword;
                string encoded = "";

                for (int i = 0; i < code.Length; i++)
                {
                    if (i < 20)
                    {
                        encoded += code[i] + scode.Substring(0, int.Parse(sxh[i].ToString()));
                        scode = scode.Substring(int.Parse(sxh[i].ToString()), scode.Length - int.Parse(sxh[i].ToString()));
                    }
                    else
                    {
                        encoded += code.Substring(i);
                        break;
                    }
                }

                return encoded;
            }
        }

        /// <summary>
        /// 组装课表请求参数
        /// </summary>
        /// <param name="htmlData"></param>
        /// <returns></returns>
        private static KbParam GetKbParam(string htmlData)
        {
            //获得周次
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlData);
            var weeks = doc.DocumentNode.SelectNodes("//select[@id='week']/option");
            List<string> Stringweeks = new List<string>();
            foreach (var weeknode in weeks)
            {
                Stringweeks.Add(weeknode.GetAttributeValue("value", ""));
            }

            HtmlNode selected = doc.DocumentNode.SelectSingleNode("//select[@id='week']/option[@selected]");

            //获得kbjcmsid
            HtmlNode kbjcmsid = doc.DocumentNode.SelectSingleNode("//ul[@class='layui-tab-title']/li");

            //获取学期
            HtmlNodeCollection xnxq01id = doc.DocumentNode.SelectNodes("//select[@lay-filter='xnxq']/option");
            string xnxq01idValue = "";
            if (xnxq01id != null)
            {
                foreach (HtmlNode xnxq01idchild in xnxq01id)
                {
                    xnxq01idValue = xnxq01idchild.InnerHtml.Trim();
                }
            }

            KbParam kbParam = new KbParam(Stringweeks, kbjcmsid?.GetAttributeValue("data-value", "") ?? "", xnxq01idValue ?? "", selected?.GetAttributeValue("value", "") ?? "");
            return kbParam;
        }



        /// <summary>
        /// DataGirdView控件选中改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Schedule_SelectionChanged(object sender, EventArgs e)
        {
            if (Schedule.SelectedCells.Count > 0)
            {
                int rowIndex = Schedule.SelectedCells[0].RowIndex;
                int columnIndex = Schedule.SelectedCells[0].ColumnIndex;
                More_Info.Text = Schedule.Rows[rowIndex].Cells[columnIndex].ToolTipText;
            }
        }

        /// <summary>
        /// 显示密码按钮按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Passwd_MouseDown(object sender, MouseEventArgs e)
        {
            textBox_Password.PasswordChar = '\0';
        }

        /// <summary>
        /// 显示密码按钮松开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Passwd_MouseUp(object sender, MouseEventArgs e)
        {
            textBox_Password.PasswordChar = '*';
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            //读取配置文件中的账户密码
            string path = "account.dat";
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    textBox_Account.Text = DecodeSrting(reader.ReadLine() ?? "");
                    textBox_Password.Text = DecodeSrting(reader.ReadLine() ?? "");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生错误： " + ex.Message);
            }
        }

        private void 主页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("explorer.exe", "https://www.goodboyboy.top"));
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            client.Dispose();
            client = new HttpClient();
            kbjcmsid = "";
            xnxq01id = "";
            FINISH = false;
            textBox_Account.Enabled = true;
            textBox_Password.Enabled = true;
            logout.Visible = false;
            MessageBox.Show("您的账户已登出！", "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 检查更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://git.goodboyboy.top/goodboyboy/HUT_Class_Schedule/releases");
        }
    }

    /// <summary>
    /// 课表请求参数实体类
    /// </summary>

    public class KbParam
    {
        public List<string> zhouci;
        public string kbjcmsid;
        public string xnxq01id;
        public string zhouciSelected;

        /// <summary>
        /// 实体类构造函数
        /// </summary>
        /// <param name="zhouci">课表周次集合</param>
        /// <param name="kbjcmsid">课表ID</param>
        /// <param name="xnxq01id">学期</param>
        /// /// <param name="zhouciSelected">已选中的周次</param>
        public KbParam(List<string> zhouci, string kbjcmsid, string xnxq01id, string zhouciSelected)
        {
            this.zhouci = zhouci;
            this.kbjcmsid = kbjcmsid;
            this.xnxq01id = xnxq01id;
            this.zhouciSelected = zhouciSelected;
        }
    }
}
