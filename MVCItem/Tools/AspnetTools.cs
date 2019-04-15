using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCItem.Tools
{
    public class AspnetTools
    {

        public static string CreateRandomNum(int NumCount)
        {

            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');//拆分成数组
            string randomNum = string.Empty;
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < NumCount; i++)
            {
                if (temp !=-1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(35);
                if (temp == t)
                {
                    return CreateRandomNum(NumCount);
                }
                temp = t;
                randomNum += allCharArray[t];
            }
            return randomNum;
        }
        public static byte[] CreateImage (string validateNum)
        {
            
            if (validateNum ==null || validateNum.Trim() ==string.Empty)
            {
                return null;
            }
            Bitmap image = new Bitmap(validateNum.Length * 12 + 10, 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.Wheat);
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);

                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateNum, font, brush, 2, 2);
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, 2, Color.FromArgb(random.Next()));

                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                MemoryStream ms = new MemoryStream();
                
                image.Save(ms, ImageFormat.Gif);
                ms.Seek(0, SeekOrigin.Begin);               

                byte[] result = new byte[ms.Length];
                 ms.Read(result,0,result.Length);
                return result;

            }
           finally
            {

                g.Dispose();
                image.Dispose();
            }

        }

    }
}