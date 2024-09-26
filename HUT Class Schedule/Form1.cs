using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Security.Principal;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace HUT_Class_Schedule
{
    public partial class Form1 : Form
    {
        public Form1()
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
            HttpClient client = new HttpClient();

            string encodingAccount = EncodeString(textBox_Account.Text);
            string encodingPassword = EncodeString(textBox_Password.Text);
            string postData = "userAccount=" + encodingAccount + "&userPassword=&encoded=" + WebUtility.UrlEncode(encodingAccount + "%%%" + encodingPassword);


            //Post身份验证
            HttpContent content = new StringContent(postData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            await client.PostAsync("http://218.75.197.123:83/jsxsd/xk/LoginToXk", content);


            //Post请求课表HTML
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            string kbData = "rq=" + today;

            HttpContent kbContent = new StringContent(kbData);
            kbContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpResponseMessage response = await client.PostAsync("http://218.75.197.123:83/jsxsd/framework/main_index_loadkb.jsp", kbContent);
            string result = await response.Content.ReadAsStringAsync();


            //解析课表HTML
            var html = new HtmlDocument();
            html.LoadHtml(result);
            var table = html.DocumentNode.SelectSingleNode("//table");
            if (table != null)
            {

                var tableData = new List<List<Course>>();

                // 获取课表数据
                var tbody = table.SelectSingleNode(".//tbody");
                var rows = tbody.SelectNodes(".//tr");

                foreach (var row in rows)
                {
                    var cells = row.SelectNodes(".//td");
                    var rowData = new List<Course>();

                    for (int i = 0; i < cells.Count; i++)
                    {
                        var cell = cells[i];
                        if (cell.SelectSingleNode(".//p") != null)
                        {
                            Course course = new Course();
                            course.courseName = cell.InnerText.Trim();
                            // 获取p标签的 title 属性
                            var title = cell.SelectSingleNode(".//p").GetAttributeValue("title", string.Empty);
                            course.courseInfo = title.Replace("<br/>", "\n");
                            rowData.Add(course);
                        }
                        else
                        {
                            Course course = new Course();
                            course.courseName = cell.InnerText.Trim();
                            course.courseInfo = "N/A";
                            rowData.Add(course);
                        }
                    }

                    tableData.Add(rowData);
                }

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
            }
            else
            {
                MessageBox.Show("解析课表失败！");
            }

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

        public static string EncodeString(string originalString)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(originalString);
            string base64String = Convert.ToBase64String(bytes);
            return base64String;
        }

        public static string DecodeSrting(string encodedString)
        {
            byte[] decodedBytes = Convert.FromBase64String(encodedString);
            string decodedString = System.Text.Encoding.UTF8.GetString(decodedBytes);
            return decodedString;
        }

        private void Schedule_SelectionChanged(object sender, EventArgs e)
        {
            if (Schedule.SelectedCells.Count > 0)
            {
                int rowIndex = Schedule.SelectedCells[0].RowIndex;
                int columnIndex = Schedule.SelectedCells[0].ColumnIndex;
                More_Info.Text = Schedule.Rows[rowIndex].Cells[columnIndex].ToolTipText;
            }
        }

        private void Show_Passwd_MouseDown(object sender, MouseEventArgs e)
        {
            textBox_Password.PasswordChar = '\0';
        }

        private void Show_Passwd_MouseUp(object sender, MouseEventArgs e)
        {
            textBox_Password.PasswordChar = '*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = "account.dat";
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    textBox_Account.Text = DecodeSrting(reader.ReadLine()??"");
                    textBox_Password.Text = DecodeSrting(reader.ReadLine()??"");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("发生错误： " + ex.Message);
            }
        }


    }
}
