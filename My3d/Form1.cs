using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using My3d;

namespace My3d
{
    public partial class Form1 : Form
    {
        //double x, y;
        MyDEM mydem = new MyDEM();
        public Vector3d vEye = new Vector3d(0, 0, 5);
        public Vector3d vLookAt = new Vector3d(1, 10, 1.6);
        public Vector3d vUp = new Vector3d(0, 0, 1);
        public double dXY = 90;
        public double dAngleXY = 90;
        public double[,] terr=null;
        public List<Vector3d> treePos = new List<Vector3d>();
        public List<int> treeId = new List<int>();
        public double dStep = 1;
        // static int ia = 0;
        //纹理
        float[] fLightPosition = new float[4] { 0f, 0f, 10f, 0f };// 光源位置 
        float[] fLightAmbient = new float[4] { 1f, 1f, 1f, 0f };// 环境光参数 
        float[] fLightDiffuse = new float[4] { 1f, 1f, 1f, 0f };// 漫射光参数

        float[] fLightPosition2 = new float[4] { 0, 0,10f, 0f };// 光源位置 
        float[] fLightAmbient2 = new float[4] { 0f, 0f, 0f, 0f };// 环境光参数 
        float[] fLightDiffuse2 = new float[4] { 0f, 0f, 0f, 0f };// 漫射光参数


        public SharpGL.SceneGraph.Assets.Texture[] texture;
        private double a=0;

        public void intTextures(SharpGL.OpenGL gl)
        {
            texture = new SharpGL.SceneGraph.Assets.Texture[9];
            for (int i = 0; i < 9; i++)
            {
                texture[i] = new SharpGL.SceneGraph.Assets.Texture();
            }
            string szPath = Application.StartupPath;
            string texPath = szPath + "\\data";

            Bitmap bitmap = new Bitmap(texPath + "\\tree.BMP");
            bitmap.MakeTransparent();
            texture[0].Create(gl, bitmap);

            bitmap = new Bitmap(texPath + "\\梯田桃树1.png");
            bitmap.MakeTransparent();
            texture[1].Create(gl, bitmap);

            bitmap = new Bitmap(texPath + "\\u=1685326126,4201227267&fm=26&gp=0.jpg");
            bitmap.MakeTransparent();
            texture[2].Create(gl, bitmap);

            bitmap = new Bitmap(texPath + "\\下载 (1).jpg");//水面
            bitmap.MakeTransparent();
            texture[3].Create(gl, bitmap);

            bitmap = new Bitmap(texPath + "\\u=1685326126,4201227267&fm=26&gp=0.jpg");
            bitmap.MakeTransparent();
            texture[4].Create(gl, bitmap);

            bitmap = new Bitmap(texPath + "\\u=1685326126,4201227267&fm=26&gp=0.jpg");
            bitmap.MakeTransparent();
            texture[5].Create(gl, bitmap);

            bitmap = new Bitmap(texPath + "\\u=1685326126,4201227267&fm=26&gp=0.jpg");
            bitmap.MakeTransparent();
            texture[6].Create(gl, bitmap);

            bitmap = new Bitmap(texPath + "\\u=1685326126,4201227267&fm=26&gp=0.jpg");
            bitmap.MakeTransparent();
            texture[7].Create(gl, bitmap);

            bitmap = new Bitmap(texPath + "\\sand.png");
            bitmap.MakeTransparent();
            texture[8].Create(gl, bitmap);
        }


        public Form1()
        {

            InitializeComponent();
            Random rand = new Random();
            //zhongshu
            for (int i = 0; i < 50; i++)
            {
                int IndexX = rand.Next(-50, 50);
                int IndexY = rand.Next(-50, 50);
                int Index = rand.Next(0, 2);
                treeId.Add(Index);
                Vector3d v = new Vector3d(IndexX, IndexY, 0);
                treePos.Add(v);


            }
            ////wenli
            //texture = new SharpGL.SceneGraph.Assets.Texture[2];
            //for(int i=0;i<2;i++)
            //{
            //    texture[i] = new SharpGL.SceneGraph.Assets.Texture();

            //    string szPath = Application.StartupPath;
            //    string texPath = szPath + "\\data\\image";
            //}
        }

        void drawTree(SharpGL.OpenGL gl, double dx, double dy, double dz,int index)//树的函数
        {
            gl.Enable(OpenGL.GL_BLEND);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.Enable(OpenGL.GL_ALPHA_TEST);
            gl.Enable(OpenGL.GL_TEXTURE_2D);

            gl.PushMatrix();   //强制刷新

            gl.Translate(dx, dy, mydem.high[Convert.ToInt32(mydem.m - (dy + 50) / 0.5 - 1), Convert.ToInt32((dx + 50) / 0.5)]);
            texture[treeId[index]].Bind(gl);

            gl.Color(1f, 1f, 1f, 1f);

            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.TexCoord(0, 1);
                gl.Vertex(-0.5f, 0f, 0f);
                gl.TexCoord(1, 1);
                gl.Vertex(0.5f, 0f, 0f);
                gl.TexCoord(1, 0);
                gl.Vertex(0.5f, 0f, 3.0f);
                gl.TexCoord(0, 0);
                gl.Vertex(-0.5f, 0.0f, 3.0f);
            }
            gl.End();

            gl.Rotate(0f, 0f, 90);

            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.TexCoord(0, 1);
                gl.Vertex(-0.5f, 0f, 0f);
                gl.TexCoord(1, 1);
                gl.Vertex(0.5f, 0f, 0f);
                gl.TexCoord(1, 0);
                gl.Vertex(0.5f, 0f, 3.0f);
                gl.TexCoord(0, 0);
                gl.Vertex(-0.5f, 0.0f, 3.0f);
            }
            gl.End();

            gl.PopMatrix();

            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.Disable(OpenGL.GL_BLEND);
            gl.Disable(OpenGL.GL_ALPHA);




        }
        void setCamera(SharpGL.OpenGL gl)
        {
            //OpenGL gl = openGLControl.OpenGL;

            //  设置当前矩阵模式,对投影矩阵应用随后的矩阵操作
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            // 重置当前指定的矩阵为单位矩阵,将当前的用户坐标系的原点移到了屏幕中心
            gl.LoadIdentity();

            // 创建透视投影变换
            gl.Perspective(30.0f, (double)Width / (double)Height, 0.05, 1000.0);

            //增加移动代码
            // double dRad = dAngleXY / 180 * 3.1415926;
            double dRad = dAngleXY / 180 * 3.1415926;
            double dx = vEye.dx + 100 * Math.Cos(dRad);
            double dy = vEye.dy + 100 * Math.Sin(dRad);

            vLookAt.dx = dx;
            vLookAt.dy = dy;

            // 视点变换
            gl.LookAt(vEye.dx, vEye.dy, vEye.dz, vLookAt.dx, vLookAt.dy, vLookAt.dz, vUp.dx, vUp.dy, vUp.dz);

            // 设置当前矩阵为模型视图矩阵
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        private void openGLControl1_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            SharpGL.OpenGL gl = this.openGLControl.OpenGL;
            setCamera(gl);

            //清除深度缓存 
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //重置当前指定的矩阵为单位矩阵,将当前的用户坐标系的原点移到了屏幕中心
            gl.LoadIdentity();

            // 光照
          //  gl.Enable(OpenGL.GL_LIGHTING);
           
            
            //地形
            mydem.DrawLand(gl, texture[8]);



            //把当前坐标系右移3个单位，注意此时是相对上面(-1.5, 0, -6)点定位
            gl.Translate(0f, 0f, 0f);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture[1].Bind(gl);

            gl.Color(1f, 1f, 1f, 1f);
            gl.Begin(OpenGL.GL_QUADS);
            {    //地形
                //gl.TexCoord(0, 0);
                //gl.Vertex(-50.0f, 50.0f, 0.0f);
                //gl.TexCoord(0, 1);
                //gl.Vertex(-50.0f, -50.0f, 0.0f);
                //gl.TexCoord(1, 1);
                //gl.Vertex(50.0f, -50.0f, 0.0f);
                //gl.TexCoord(1, 0);
                //gl.Vertex(50.0f, 50.0f, 0.0f);

            }


            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);




            //水面

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture[3].Bind(gl);

            gl.Color(1f, 1f, 1f, 1f);
            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.TexCoord(0, 0);
                gl.Vertex(-50.0f, 50.0f, a);
                gl.TexCoord(0, 1);
                gl.Vertex(-50.0f, -50.0f, a);
                gl.TexCoord(1, 1);
                gl.Vertex(50.0f, -50.0f, a);
                gl.TexCoord(1, 0);
                gl.Vertex(50.0f, 50.0f, a);

            }

            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);




            //天空

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture[2].Bind(gl);

            gl.Color(1f, 1f, 1f, 1f);
            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.TexCoord(0, 0);
                gl.Vertex(-50.0f, 50.0f, 40.00f);
                gl.TexCoord(0, 1);
                gl.Vertex(-50.0f, -50.0f, 40f);
                gl.TexCoord(1, 1);
                gl.Vertex(50.0f, -50.0f, 40f);
                gl.TexCoord(1, 0);
                gl.Vertex(50.0f, 50.0f, 40f);

            }

            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            //01
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture[4].Bind(gl);
            gl.Color(1f, 1f, 1f, 1f);
            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.TexCoord(0, 0);
                gl.Vertex(-50.0f, 50.0f, -1f);
                gl.TexCoord(0, 1);
                gl.Vertex(-50.0f, 50.0f, 40f);
                gl.TexCoord(1, 1);
                gl.Vertex(-50.0f, -50.0f, 40f);
                gl.TexCoord(1, 0);
                gl.Vertex(-50.0f, -50.0f, -1f);
            }
            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);

            //02

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture[5].Bind(gl);
            gl.Color(1f, 1f, 1f, 1f);
            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.TexCoord(0, 0);
                gl.Vertex(50.0f, 50.0f, -1f);
                gl.TexCoord(0, 1);
                gl.Vertex(50.0f, 50.0f, 40f);
                gl.TexCoord(1, 1);
                gl.Vertex(50.0f, -50.0f, 40f);
                gl.TexCoord(1, 0);
                gl.Vertex(50.0f, -50.0f, -1f);
            }
            //03
            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture[6].Bind(gl);
            gl.Color(1f, 1f, 1f, 1f);
            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.TexCoord(0, 0);
                gl.Vertex(-50.0f, 50.0f, -1f);
                gl.TexCoord(0, 1);
                gl.Vertex(-50.0f, 50.0f, 40f);
                gl.TexCoord(1, 1);
                gl.Vertex(50.0f, 50.0f, 40f);
                gl.TexCoord(1, 0);
                gl.Vertex(50.0f, 50.0f, -1f);
            }
            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            texture[7].Bind(gl);
            gl.Color(1f, 1f, 1f, 1f);
            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.TexCoord(0, 0);
                gl.Vertex(-50.0f, -50.0f, -1f);
                gl.TexCoord(0, 1);
                gl.Vertex(-50.0f, -50.0f, 40f);
                gl.TexCoord(1, 1);
                gl.Vertex(50.0f, -50.0f, 40f);
                gl.TexCoord(1, 0);
                gl.Vertex(50.0f, -50.0f, -1f);
            }
            gl.End();
            gl.Disable(OpenGL.GL_TEXTURE_2D);



            //画树
            for (int i = 0; i < treePos.Count; i++)
            {

                Vector3d vPos = treePos[i];
                drawTree(gl, vPos.dx, vPos.dy, vPos.dz,i);
            }

            gl.Flush();   //强制刷新
        }

        public bool pengzhuang(double dx,double dy)
      {
            int a = 0;
            for (int i = 0; i < treePos.Count; i++)
            {

                Vector3d vPos = treePos[i];
                if ((dx - vPos.dx) * (dx - vPos.dx) + (dy - vPos.dy) * (dy - vPos.dy) < 2||dx>49||dx<-49||dy>49||dy<-49)
                { a = 1;break; }
            }
            if (a==0)
                return true;
            else
                return false;
       }  

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.ClearColor(1f, 1f, 1f, 0f);


            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, fLightAmbient);//环境光源 
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, fLightDiffuse);//漫射光源 
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, fLightPosition);//光源位置 

            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_AMBIENT, fLightAmbient2);//环境光源 
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_DIFFUSE, fLightDiffuse2);//漫射光源 
            gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_POSITION, fLightPosition2);//光源位置 
            //gl.Enable(OpenGL.GL_LIGHTING);//开启光照 
            //gl.Enable(OpenGL.GL_LIGHT0);
            //gl.Enable(OpenGL.GL_LIGHT1);

          //  gl.Enable(OpenGL.GL_NORMALIZE);
           // gl.ClearColor(0, 0, 0, 0);
            //纹理

            intTextures(gl);
        }

        private void openGLControl1_Resize(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            setCamera(gl);

        }

        private void openGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            
            OpenGL gl = openGLControl.OpenGL;
            switch (e.KeyCode)
            {
                
                case Keys.Space:
                    vEye.dz += 2;
                    break;
                case Keys.A:
                    dAngleXY = dAngleXY + 10;
                    break;
                case Keys.D:
                    dAngleXY = dAngleXY - 10;
                    break;
                case Keys.W:
                    double dRad = dAngleXY / 180 * 3.1415926;
                    double dX = dStep * Math.Cos(dRad);
                    double dY = dStep * Math.Sin(dRad);
                    if (pengzhuang(vEye.dx + dX, vEye.dy + dY))
                    {
                        vEye.dx += dX;
                        vEye.dy += dY;
                        vEye.dz = mydem.high[Convert.ToInt32(mydem.m - (vEye.dy + 50) / 0.5 - 1), Convert.ToInt32((vEye.dx + 50) / 0.5)] + 2;
                    }
                    break;
                    
                case Keys.S:
                    dRad = dAngleXY / 180 * 3.1415926;
                    dX = dStep * Math.Cos(dRad);
                    dY = dStep * Math.Sin(dRad);
                    int a = 0;
                    if (pengzhuang(vEye.dx - dX, vEye.dy - dY))
                    {
                        vEye.dx -= dX;
                        vEye.dy -= dY;
                        vEye.dz = mydem.high[Convert.ToInt32(mydem.m - (vEye.dy + 50) / 0.5 - 1), Convert.ToInt32((vEye.dx + 50) / 0.5)] + 2;
                    }
                    break;
                case Keys.PageDown:
                    vEye.dz -= 2;
                    break;
                case Keys.Q:
                
                    vLookAt.dz -= 10;
                    break;
                case Keys.E:
              
                    vLookAt.dz += 10;
                    break;
                case Keys.F:
                    timer1.Start();
                    break;
                case Keys.G:
                  
                     gl.Enable(OpenGL.GL_LIGHTING);//开启光照 
                    gl.Enable(OpenGL.GL_LIGHT0);
                    gl.Enable(OpenGL.GL_LIGHT1);

                    gl.Enable(OpenGL.GL_NORMALIZE); 
                        // gl.ClearColor(0, 0, 0, 0);
                    break;
                case Keys.H:
                    gl.Disable(OpenGL.GL_LIGHTING);//开启光照 
                    gl.Disable(OpenGL.GL_LIGHT0);
                    gl.Disable(OpenGL.GL_LIGHT1);

                    gl.Disable(OpenGL.GL_NORMALIZE);
                    // gl.ClearColor(0, 0, 0, 0);
                    break;
                case Keys.V:
                    timer1.Stop();
                    break;
                case Keys.Z:
                    timer2.Start();
                    break;
                case Keys.X:
                    timer2.Stop();
                    break;

            }

        }
    
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double dRad = dAngleXY / 180 * 3.1415926;
            double dX = dStep * Math.Cos(dRad);
            double dY = dStep * Math.Sin(dRad);
            if (pengzhuang(vEye.dx + dX, vEye.dy + dY))
                {
                vEye.dx += dX;
                vEye.dy += dY;
                vEye.dz = mydem.high[Convert.ToInt32(mydem.m - (vEye.dy + 50) / 0.5 - 1), Convert.ToInt32((vEye.dx + 50) / 0.5)] + 2;
                }
            else
            { SendKeys.Send("{D}"); }
            //int count = 0;
            //while (count < 1)
            //{
            //    Random rand = new Random();
            //    int A = rand.Next(0, 3);
            //    if (A == 0)
            //    {
            //        SendKeys.Send("{W}"); 
            //    }
            //    else if (A == 1)
            //    { SendKeys.Send("{A}");  }
            //    else if (A == 2)
            //    {
            //        SendKeys.Send("{D}");
            //    }
            //    count++;
            //}
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            a += 0.1;
            if(a>4)
            { a = 0; }
        }
    }
}
