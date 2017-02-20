using System;
using System.Windows.Forms;
using ConfusedKitten.Common;

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
            ////Download.ResumeDownloadFile(@"E:\项目文档\酒店文档\艺龙\酒店静态文件\", @"http://api.elongstatic.com/xml/v2.0/hotel/hotellist.xml");
            ////Download.DownloadFile(@"E:\项目文档\酒店文档\艺龙\酒店静态文件\", @"http://api.elongstatic.com/xml/v2.0/hotel/hotellist.xml");
            ////Download.DownloadFile(@"E:\项目文档\酒店文档\艺龙\酒店静态文件\", @"http://api.elongstatic.com/xml/v2.0/hotel/cn/15/90594615.xml");
            ////       Download.DownloadGip(@"E:\项目文档\酒店文档\艺龙\酒店静态文件\", @"http://api.elongstatic.com/xml/v2.0/hotel/geo_cn.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string test = "02/13/2017 10:56:00";
            DateTime dt = TimeHelper.MdyConvert(test);
        }
    }
}
