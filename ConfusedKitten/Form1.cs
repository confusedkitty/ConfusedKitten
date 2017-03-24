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
            BaseEntity baseEntity = new BaseEntity
            {
                ClassName = "MainorderCommon",
                ClassAuthor = "王越",
                ClassExplain = "主订单_综合订单",
                NameSpace = "test"
            };
            string path = ProjectHelper.ProjectDirectory() + "\\MainorderCommon.cs";
            DbToEntity.CreateEntity(path, baseEntity, "tmc_mainorder_common");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var a=     Dao.Create("Database=test_hotel;Data Source=localhost;port=3306;User Id=sa;Password=123456;Charset=utf8;Connect Timeout=6000;");
            var a = Dao.Create("Database=tmc3db;Data Source=192.168.2.15;port=3306;User Id=tmc3;Password=88691111;Charset=utf8;Connect Timeout=6000;");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string data = "TimeStamp=1489634122&Sign=FE29D133-468D-403B-8428-0168C968CAC1&PlatformId=24&ProductId=23&username=cstest001&NewKey=5017faddc612d036fe55e099a9d7ccbf";
            string url = "http://localhost:22421/User/useraccountdetail?" + data;
            string a = RequestMethod.PostMethod(url, "");
            RequestMethod.GetMethod(url);
        }
    }
}
