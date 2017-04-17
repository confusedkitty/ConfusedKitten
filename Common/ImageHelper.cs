using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace ConfusedKitten.Common
{
    public static class ImageHelper
    {
        /// <summary>
        ///  更改图片尺寸并保存
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <param name="savePath">保存路径</param>
        public static void ChangeImage(string filePath, string savePath)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    // 下载图片
                    var bytes = wc.DownloadData(filePath);
                    using (MemoryStream ms1 = new MemoryStream(bytes))
                    {
                        using (Bitmap bm = (Bitmap)Image.FromStream(ms1))
                        {
                            Rectangle rg = new Rectangle(0, 0, bm.Width, bm.Height - 30);
                            var newbm = new Bitmap(bm.Width, bm.Height - 30);
                            var g = Graphics.FromImage(newbm);
                            g.DrawImage(bm, new Rectangle(0, 0, bm.Width, bm.Height), rg, GraphicsUnit.Pixel);
                            newbm.Save(savePath, ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///  图片 转为    base64编码的文本
        /// </summary>
        /// <param name="imagefilename">图片名称</param>
        private static void ImgToBase64String(string imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(imagefilename);
                FileStream fs = new FileStream(imagefilename + ".txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                sw.Write(strbaser64);

                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///  base64编码的文本 转为图片
        /// </summary>
        /// <param name="txtFileName">文件名称</param>
        private static void Base64StringToImage(string txtFileName)
        {
            try
            {
                FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(ifs);

                String inputStr = sr.ReadToEnd();
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                ms.Close();
                sr.Close();
                ifs.Close();
                //this.pictureBox2.Image = bmp;
                if (File.Exists(txtFileName))
                {
                    File.Delete(txtFileName);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
