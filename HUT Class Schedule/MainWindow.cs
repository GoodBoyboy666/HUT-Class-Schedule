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

        private async void Get_Schedule_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "��ʼ����";
            statusBar.Visible = true;
            statusBar.Value = 0;

            //����HttpClientʵ��
            HttpClient client = new HttpClient();

            await client.GetAsync("http://218.75.197.123:83/");

            statusLabel.Text = "��ȡSESS�С���";
            statusBar.Value += 10;

            //��ȡSESS
            HttpContent empty = new StringContent("");
            HttpResponseMessage SESS = await client.PostAsync("http://218.75.197.123:83/Logon.do?method=logon&flag=sess", empty);
            string SESStext = "";
            for (int i = 0; i < 3; i++)
            {
                SESStext = await SESS.Content.ReadAsStringAsync();
                if (!(SESStext.Length > 100 || SESStext == "" || SESStext == null))
                    break;
                SESStext = "";
            }

            if (SESStext == "")
            {
                MessageBox.Show("��ȡSESSʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            statusBar.Value += 20;

            //�����˻��������
            string encodedString = "";
            if (SESStext != "")
            {
                statusLabel.Text = "�����˺������С���";
                encodedString = EncodeString(SESStext, textBox_Account.Text, textBox_Password.Text);
                statusBar.Value += 10;
            }


            //��װPost����Body
            string authReslutString = "";
            bool isError = false;
            if (encodedString != "")
            {
                statusLabel.Text = "�����֤�С���";
                string postData = "loginMethod=logon&userlanguage=0&userAccount=" + textBox_Account.Text + "&userPassword=&encoded=" + WebUtility.UrlEncode(encodedString);

                //Post�����֤
                HttpContent content = new StringContent(postData);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage authReslut = await client.PostAsync("http://218.75.197.123:83/Logon.do?method=logon", content);
                authReslutString = await authReslut.Content.ReadAsStringAsync();

                //��������֤���
                HtmlDocument authReslutHTML = new HtmlDocument();
                authReslutHTML.LoadHtml(authReslutString);
                var authReslutNode = authReslutHTML.DocumentNode.SelectSingleNode("//font[@id='showMsg']");
                if (authReslutNode !=null)
                {
                    MessageBox.Show("�����֤ʧ�ܣ�\n" + authReslutNode.InnerText.Trim(), "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isError = true;
                }
                else
                {
                    statusBar.Value += 20;
                }
            }
            else
            {
                isError = true;
            }


            //Get����α����
            string kbURL = "";
            if (isError !=true)
            {
                statusLabel.Text = "��ȡ�α���������С���";
                HttpResponseMessage reKbParam = await client.GetAsync("http://jwxt.hut.edu.cn/jsxsd/framework/xsMainV_new.htmlx?t1=1");
                string htmlKbParam = await reKbParam.Content.ReadAsStringAsync();

                //Post����α�HTML����Ҫ���ұ�����Ϊʲô��ô��֣���ѧУ��
                KbParam kbParam = GetKbParam(htmlKbParam);
                string zhouci = kbParam.zhouci;
                string kbjcmsid = kbParam.kbjcmsid;
                string xnxq01id = kbParam.xnxq01id;
                kbURL = "http://jwxt.hut.edu.cn/jsxsd/framework/mainV_index_loadkb.htmlx?zc=" + zhouci + "&kbjcmsid=" + kbjcmsid + "&xnxq01id=" + xnxq01id + "&xswk=false";

                if (zhouci == "" || kbjcmsid == "" || xnxq01id == "")
                {
                    MessageBox.Show("�����α����ʧ�ܣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isError = true;
                }
                else
                {
                    statusBar.Value += 20;
                }
            }
            else
            {
                isError = true;
            }

            if (isError!=true)
            {
                statusLabel.Text = "����α��С���";

                HttpResponseMessage response = await client.GetAsync(kbURL);
                string result = await response.Content.ReadAsStringAsync();

                client.Dispose();

                statusBar.Value += 10;

                //�����α�HTML
                ParseSchedule(result);

                //��������
                SaveSettings();

                statusLabel.Text = "���";

                statusBar.Value += 10;
            }

        }

        /// <summary>
        /// ���������ļ�
        /// </summary>
        private void SaveSettings()
        {
            statusLabel.Text = "���������ļ��С���";

            //�˻�����д�������ļ�
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
                MessageBox.Show("�������� " + ex.Message);
            }
        }

        /// <summary>
        /// �����α�HTML
        /// </summary>
        /// <param name="result">�α�HTML</param>
        private void ParseSchedule(string result)
        {
            statusLabel.Text = "�����α��С���";
            var html = new HtmlDocument();
            html.LoadHtml(result);
            var table = html.DocumentNode.SelectSingleNode("//table");
            if (table != null)
            {
                //����α�����
                var tableData = new List<List<Course>>();

                // ��ȡ�α�����
                var tbody = table.SelectSingleNode(".//tbody");
                var rows = tbody.SelectNodes(".//tr");

                //�����α�
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

                            // �γ���ϸ��Ϣ
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

                //���DataGirdView
                Schedule.Rows.Clear();

                // չʾ�������
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
            }
            else
            {
                MessageBox.Show("�����α�ʧ�ܣ�");
            }
            statusBar.Value = 90;
        }

        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="originalString">ԭʼ�ַ���</param>
        /// <returns>�����ַ���</returns>
        public static string EncodeString(string originalString)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(originalString);
            string base64String = Convert.ToBase64String(bytes);
            return base64String;
        }

        /// <summary>
        /// Base64����
        /// </summary>
        /// <param name="encodedString">�����ַ���</param>
        /// <returns>ԭʼ�ַ���</returns>
        public static string DecodeSrting(string encodedString)
        {
            byte[] decodedBytes = Convert.FromBase64String(encodedString);
            string decodedString = System.Text.Encoding.UTF8.GetString(decodedBytes);
            return decodedString;
        }

        /// <summary>
        /// ��������㷨
        /// </summary>
        /// <param name="dataStr">SEES</param>
        /// <param name="userAccount">ѧ��</param>
        /// <param name="userPassword">����</param>
        /// <returns>���ܵ��ַ�</returns>
        public static string EncodeString(string dataStr, string userAccount, string userPassword)
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
        /// ��װ�α��������
        /// </summary>
        /// <param name="htmlData"></param>
        /// <returns></returns>
        public static KbParam GetKbParam(string htmlData)
        {
            //����ܴ�
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlData);
            HtmlNode week = doc.DocumentNode.SelectSingleNode("//select[@id='week']/option[@selected]");

            //���kbjcmsid
            HtmlNode kbjcmsid = doc.DocumentNode.SelectSingleNode("//ul[@class='layui-tab-title']/li");

            //��ȡѧ��
            HtmlNodeCollection xnxq01id = doc.DocumentNode.SelectNodes("//select[@lay-filter='xnxq']/option");
            string xnxq01idValue = "";
            if (xnxq01id != null)
            {
                foreach (HtmlNode xnxq01idchild in xnxq01id)
                {
                    xnxq01idValue = xnxq01idchild.InnerHtml.Trim();
                }
            }

            KbParam kbParam = new KbParam(week?.GetAttributeValue("value", "") ?? "", kbjcmsid?.GetAttributeValue("data-value", "") ?? "", xnxq01idValue ?? "");
            return kbParam;
        }



        /// <summary>
        /// DataGirdView�ؼ�ѡ�иı��¼�
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
        /// ��ʾ���밴ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Passwd_MouseDown(object sender, MouseEventArgs e)
        {
            textBox_Password.PasswordChar = '\0';
        }

        /// <summary>
        /// ��ʾ���밴ť�ɿ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Passwd_MouseUp(object sender, MouseEventArgs e)
        {
            textBox_Password.PasswordChar = '*';
        }

        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            //��ȡ�����ļ��е��˻�����
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
                Console.WriteLine("�������� " + ex.Message);
            }
        }

        private void ��ҳToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("explorer.exe", "https://www.goodboyboy.top"));
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }

    /// <summary>
    /// �α��������ʵ����
    /// </summary>

    public class KbParam
    {
        public string zhouci;
        public string kbjcmsid;
        public string xnxq01id;

        /// <summary>
        /// ʵ���๹�캯��
        /// </summary>
        /// <param name="zhouci">�α��ܴ�</param>
        /// <param name="kbjcmsid">�α�ID</param>
        /// <param name="xnxq01id">ѧ��</param>
        public KbParam(string zhouci, string kbjcmsid, string xnxq01id)
        {
            this.zhouci = zhouci;
            this.kbjcmsid = kbjcmsid;
            this.xnxq01id = xnxq01id;
        }
    }
}
