using System;
using System.Windows.Forms;
using ConfusedKitten.Common;
using ConfusedKitten.DataBase;
using ConfusedKitten.Model;
using ConfusedKitten.MyWebRequest;

namespace ConfusedKitten
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //BaseEntity baseEntity = new BaseEntity
            //{
            //    ClassName = "MainorderCommon",
            //    ClassAuthor = "王越",
            //    ClassExplain = "主订单_综合订单",
            //    NameSpace = "test"
            //};
            //string path = ProjectHelper.ProjectDirectory() + "\\1.xml";
            //DbToEntity.CreateEntity(path, baseEntity, "tmc_mainorder_common");
            //string test = XmlHelper.FetchNode(path, "test", "name", "a");
            LogHelper.Write("test\\test.txt", "测试", "内容");
            LogHelper.Write("test\\test.txt", "测试", "内容");
            LogHelper.Write("test\\test.txt", "测试", "内容");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var a=     Dao.Create("Database=test_hotel;Data Source=localhost;port=3306;User Id=sa;Password=123456;Charset=utf8;Connect Timeout=6000;");
            //var a = Dao.Create("Database=tmc3db;Data Source=192.168.2.15;port=3306;User Id=tmc3;Password=88691111;Charset=utf8;Connect Timeout=6000;");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string data = "ProductId=12&VipCode=cstest001&PlatformId=14&SubOrderSerialNumber=JD12170328000002&Sign=FE29D133-468D-403B-8428-0168C968CAC1&NewKey=f5508967f91cad89bee5a1d4b8857dba";
            string url = "http://test.api.tripg.com/HotelQunar/GetOrderDetail?" + data;
            string a = RequestMethod.PostMethod(url, "");
            RequestMethod.GetMethod(url);
        }
    }
}
