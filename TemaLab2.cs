using System;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK_Tema_lab2
{
    class TemaLab2 : GameWindow
    {
        private int[,] objVertices = {
            {5, 10, 5,
                10, 5, 10,
                5, 10, 5,
                10, 5, 10,
                5, 5, 5,
                5, 5, 5,
                5, 10, 5,
                10, 10, 5,
                10, 10, 10,
                10, 10, 10,
                5, 10, 5,
                10, 10, 5},
            {5, 5, 12,
                5, 12, 12,
                5, 5, 5,
                5, 5, 5,
                5, 12, 5,
                12, 5, 12,
                12, 12, 12,
                12, 12, 12,
                5, 12, 5,
                12, 5, 12,
                5, 5, 12,
                5, 12, 12},
            {6, 6, 6,
                6, 6, 6,
                6, 6, 12,
                6, 12, 12,
                6, 6, 12,
                6, 12, 12,
                6, 6, 12,
                6, 12, 12,
                6, 6, 12,
                6, 12, 12,
                12, 12, 12,
                12, 12, 12}};
        private const int XYZ_SIZE = 50;
        bool DrowCub = false;
        bool Colorrrrrr = false;
        private Color[] colorVertices = { Color.White, Color.LawnGreen, Color.WhiteSmoke, Color.Tomato, Color.Turquoise, Color.OldLace, Color.Olive, Color.MidnightBlue, Color.PowderBlue, Color.PeachPuff, Color.LavenderBlush, Color.MediumAquamarine };
        private Color[] colorVerticess = { Color.Red, Color.Green, Color.Coral, Color.Violet, Color.Yellow, Color.Blue, Color.Black, Color.Maroon, Color.Green, Color.Lime, Color.Aqua, Color.Pink };

        private int transStep = 0;
        private int radStep = 0;
        private int attStep = 0;

        public TemaLab2() : base(1200, 600, new GraphicsMode(32, 24, 0, 8)) {
            VSync = VSyncMode.On;

       
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            GL.ClearColor(Color.Gray);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }


        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            DrowCub = true;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
            }


            //miscare pe sageti
                if (keyboard[Key.A])
                {
                    transStep--;
                }
                if (keyboard[Key.D])
                {
                    transStep++;
                }
                if (keyboard[Key.W])
                {
                    radStep--;
                }
                if (keyboard[Key.S])
                {
                    radStep++;
                }
                if (keyboard[Key.L])//Schimbul de culori
                {
                Colorrrrrr = true;
                }
                if (keyboard[Key.K])
                {
                Colorrrrrr = false;
                }
                if (keyboard[Key.P])
                {
                DrowCub = false;
                }




            //miscare la clic de maus(sus/jos)
            if (mouse.IsButtonDown(MouseButton.Left))
                {
                    attStep--;

                }
                if (mouse.IsButtonDown(MouseButton.Right))
                {
                    attStep++;
 
                }
            
        }


        protected override void OnRenderFrame(FrameEventArgs e) {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);



            if (DrowCub == true)
            {
                GL.PushMatrix();
                GL.Translate(transStep, attStep, radStep);
                GL.Translate(0, 0, radStep);
                GL.Translate(0, attStep, 0);
                DrawCube();
                GL.PopMatrix();
            }


            //DrawTriangle();
            //DrawAxes();
            //DrawCube();
            
            SwapBuffers();
        }

        private void DrawAxes()
        {

            // Desenează toate 3 axe intrun Gl.Begin(lab3 ex1)
            GL.Begin(PrimitiveType.Lines);
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    GL.Color3(Color.Yellow);
                    GL.Vertex3(0, 0, 0);
                    GL.Vertex3(XYZ_SIZE, 0, 0);
                }
                if (i == 1)
                {
                    GL.Color3(Color.Green);
                    GL.Vertex3(0, 0, 0);
                    GL.Vertex3(0, XYZ_SIZE, 0);
                }
                if (i == 2)
                {
                    GL.Color3(Color.Red);
                    GL.Vertex3(0, 0, 0);
                    GL.Vertex3(0, 0, XYZ_SIZE);
                }
            }
            GL.End();
        }

        private void DrawCube()
        {

            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < 35; i = i + 3)
            {
                
                if (Colorrrrrr == true)
                {
                    GL.Color3(colorVerticess[i / 3]);
                }
                else
                    GL.Color3(colorVerticess[i / 3]);
                GL.Vertex3(objVertices[0, i], objVertices[1, i], objVertices[2, i]);
                GL.Vertex3(objVertices[0, i + 1], objVertices[1, i + 1], objVertices[2, i + 1]);
                GL.Vertex3(objVertices[0, i + 2], objVertices[1, i + 2], objVertices[2, i + 2]);
            }
            GL.End();

        }

        private void DrawTriangle()
        {

            GL.LineWidth(5);

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Red);
            GL.Vertex3(10, 0, 0);
            GL.Vertex3(-10, 0, 0);
            GL.Vertex3(0, 10, 0);
            GL.End();

            //Nu reusesc sa citesc coordonatele din fisier, mai incerc pe parcurs
            /*
            using (StreamReader sr = new StreamReader("C:\\Users\\Professional\\Desktop\\LAB2\\Lab2\\coordonate.txt"))
            {
                string sLineOne = sr.ReadLine();
                string[] split = sLineOne.Split(' ');
                for (int i = 0; i < 8; i++)
                {
                    for(int j = 0;j <2; j++)
                    {
                        int x = Convert.ToInt32(split[0]);
                        int y = Convert.ToInt32(split[1]);
                        int z = Convert.ToInt32(split[2]);
                        GL.Color3(Color.Red);
                        GL.Vertex3(x, y, z);
                        

                    }
                }
        }*/


          



        }

            [STAThread]
        static void Main(string[] args) {

            using (TemaLab2 example = new TemaLab2()) {
                example.Run(30.0, 0.0);
            }
        }
    }

}
