using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK_Tema_lab2
{
    class TemaLab2 : GameWindow
    {

        public TemaLab2() : base(1200, 600, new GraphicsMode(32, 24, 0, 8)) {
            VSync = VSyncMode.On;

       
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            GL.ClearColor(Color.Black);
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

            Matrix4 lookat = Matrix4.LookAt(30, 30, 0, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);


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
            if (keyboard[Key.Left])
            {
                GL.Translate(0, 0, 3);
            }
            if (keyboard[Key.Right]){
                GL.Translate(0, 0, -3);
            }
            if (keyboard[Key.Up])
            {
                GL.Translate(0, 3, 0);
            }
            if (keyboard[Key.Down])
            {
                GL.Translate(0, -3, 0);
            }



            //miscare la clic de maus
            if (mouse.IsButtonDown(MouseButton.Left))
            {
                GL.Translate(mouse.X*0.01, mouse.Y*0.01, 0);
            }
            if (mouse.IsButtonDown(MouseButton.Right))
            {
                GL.Translate(-mouse.X * 0.01, -mouse.Y * 0.01, 0);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e) {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);


            DrawBox(4);
            
            SwapBuffers();
        }



        private void DrawBox(float size)
        {
            float[,] n = new float[,]{
            {-1.0f, 0.0f, 0.0f},
            {0.0f, 1.0f, 0.0f},
            {1.0f, 0.0f, 0.0f},
            {0.0f, -1.0f, 0.0f},
            {0.0f, 0.0f, 1.0f},
            {0.0f, 0.0f, -1.0f}
        };
            int[,] faces = new int[,]{
            {0, 1, 2, 3},
            {3, 2, 6, 7},
            {7, 6, 5, 4},
            {4, 5, 1, 0},
            {5, 6, 2, 1},
            {7, 4, 0, 3}
        };
            float[,] v = new float[8, 3];
            int i;

            v[0, 0] = v[1, 0] = v[2, 0] = v[3, 0] = -size / 2;
            v[4, 0] = v[5, 0] = v[6, 0] = v[7, 0] = size / 2;
            v[0, 1] = v[1, 1] = v[4, 1] = v[5, 1] = -size / 2;
            v[2, 1] = v[3, 1] = v[6, 1] = v[7, 1] = size / 2;
            v[0, 2] = v[3, 2] = v[4, 2] = v[7, 2] = -size / 2;
            v[1, 2] = v[2, 2] = v[5, 2] = v[6, 2] = size / 2;

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Green);
            for (i = 5; i >= 0; i--)
            {
                GL.Normal3(ref n[i, 0]);
                GL.Vertex3(ref v[faces[i, 0], 0]);
                GL.Vertex3(ref v[faces[i, 1], 0]);
                GL.Vertex3(ref v[faces[i, 2], 0]);
                GL.Vertex3(ref v[faces[i, 3], 0]);
            }
            
            GL.End();
        }


        [STAThread]
        static void Main(string[] args) {

            using (TemaLab2 example = new TemaLab2()) {
                example.Run(30.0, 0.0);
            }
        }
    }

}
