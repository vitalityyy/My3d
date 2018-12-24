using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace My3d
{
    public class MyDEM
    {
        double leftx, lefty, cell;
    public    int m, n;//m为行数，n为列数
       public double[,] high = null;
        //double highmin = 0;
        //double highmax = 0;
        double d = 0;
        public MyDEM()
        {
            string filePath = Application.StartupPath + "\\data\\jiehuo.txt";
            //StreamReader sr = new StreamReader(filePath);
            try
            {
                if (File.Exists(filePath))
                {
                    string[] textlines = File.ReadAllLines(filePath);

                    string[] s = textlines[0].Split(' ');
                    n = Convert.ToInt32(s[s.Length - 1]);//列数
                    s = textlines[1].Split(' ');
                    m = Convert.ToInt32(s[s.Length - 1]);//行数
                    s = textlines[2].Split(' ');
                    leftx = Convert.ToDouble(s[s.Length - 1]);//左下角坐标X值
                    s = textlines[3].Split(' ');
                    lefty = Convert.ToDouble(s[s.Length - 1]);//左下角坐标y值
                    s = textlines[4].Split(' ');
                    cell = 0.5;//像素大小

                    high = new double[textlines.Length - 6, textlines[6].Split(' ').Length - 1];
                   
                    for (int i = 6; i < textlines.Length; i++)
                    {
                        s = textlines[i].Split(' ');
                        for (int j = 0; j < s.Length - 1; j++)  //最后一个是空格
                        {
                            high[i - 6, j] = Convert.ToDouble(s[j]) /250-7;

                    //        if (high[i-6,j]>highmax)
                    //        {
                    //           highmax = high[i - 6, j];
                    //        }
                    //        else if(high[i-6,j]<highmin)
                    //        {
                    //           highmin = high[i - 6, j];
                    //        }

                        }
                    }
                    //d = (highmax - highmin) / 8;
                    //MessageBox.Show(Convert.ToString(highmax));
                }
                else
                {
                    MessageBox.Show("文件不存在");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DrawLand(OpenGL gl,Texture t)
        {

           gl.Enable(OpenGL.GL_TEXTURE_2D);
           gl.Color(1f, 0.5f, 0f, 0f);
            double x, y;
            t.Bind(gl);
          
            for (int i = m - 1; i > 0; i--)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    x = -50+ cell * j;//位置
                    y = -50 + cell * (m - i - 1);

                    gl.Color(high[i, j]/8,1- high[i, j] / 8, 1, 0f);
                    //颜色
                   // if (high[i, j]>=highmin+d&&high[i,j]<highmin+d)
                   // { gl.Color(0.5f, 0f, 0.5f, 0f); }
                   //else if (high[i, j] >=highmin+d&&high[i,j]<highmin+2*d)
                   // { gl.Color(1f, 1f, 1f, 0f); }
                   // else if (high[i,j]>=highmin + 2 * d && high[i, j] < highmin + 3 * d)
                   // { gl.Color(1f, 0.5f, 0f, 0f); }
                   // else if (high[i, j] >= highmin + 3 * d && high[i, j] < highmin + 4 * d)
                   // { gl.Color(1f, 0f, 1f, 0f); }
                   // else if (high[i, j] >= highmin + 5 * d && high[i, j] < highmin + 6 * d)
                   // { gl.Color(1f, 0f, 0.6f, 0f); }
                   // else if (high[i, j] >= highmin + 6 * d && high[i, j] < highmin + 7 * d)
                   // { gl.Color(1f, 0f,0.2f, 0f); }
                   // else if (high[i, j] >= highmin + 7 * d && high[i, j] < highmin + 8 * d)
                   // { gl.Color(1f, 0f, 0f, 0f); }
                    gl.Begin(OpenGL.GL_QUADS);
                    {
                        gl.TexCoord(0, 1);
                        gl.Vertex(x, y, high[i, j]);
                        gl.TexCoord(1, 1);
                        gl.Vertex(x + cell, y, high[i, j + 1]);
                        gl.TexCoord(1, 0);
                        gl.Vertex(x + cell, y + cell, high[i - 1, j + 1]);
                        gl.TexCoord(0, 0);
                        gl.Vertex(x, y + cell, high[i - 1, j]);
                    }
                    gl.End();
                }
            }
            gl.Disable(OpenGL.GL_TEXTURE_2D);
           
        }

    }
}
