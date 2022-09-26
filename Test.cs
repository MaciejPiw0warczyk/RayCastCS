using GLFW;
using System;
using NAudio;
using System.Numerics;
using System.Collections.Generic;
using MapCreator.Game;
using MapCreator.Wolf3D;
using MapCreator.Rendering.Display;
using MapCreator.Rendering.Shaders;
using MapCreator.Rendering.Cameras;
using static OpenGL.GL;

namespace MapCreator
{
    internal class Test : Game.Game
    {
        Shader shader;
        Camera2D cam;
        VAO VAO, UpdateVAO, LineVAO,SWFVAO;
        VBO VBO, UpdateVBO, LineVBO,SWFVBO;
        float[] playervert;
        float[] RayVert;
        float[] SWFVert;
        float[] tri;
        public enum Color
        {
            black = 0,
            red = 1,
            green = 2,
            blue = 3,
            teal = 4,
            white = 5,
            yellow = 6,
            gray = 7,
            mint = 8,
        }

        static Vector4[] col =
        {
            new Vector4(0,0,0,1f),
            new Vector4(1, 0, 0, 1f),
            new Vector4(0, 1, 0, 1f),
            new Vector4(0, 0, 1, 1f),
            new Vector4(0, .5f, .5f, 1f),
            new Vector4(1f,1f,1f,1f),
            new Vector4(1f,1f,0f,1f),
            new Vector4(.7f,.7f,.7f,1f),
            new Vector4(0.006f,0.004f,.005f,1f),
        };

        protected unsafe override void LoadContent()
        {

            tri = DrawMap().ToArray();

            shader = new Shader(ShaderCode.vertexShader, ShaderCode.fragmentShader);
            shader.Load();

            VAO = new();
            SWFVAO = new();
            LineVAO = new();
            UpdateVAO = new();

            VAO.Bind();

            fixed (float* v = &tri[0])
            {
                VBO = new(v, sizeof(float) * tri.Length);
            }
            VAO.LinkVBO(VBO, 0);

            VAO.Unbind();
            VBO.Unbind();



            cam = new Camera2D(DisplayManager.WindowSize / 2f, 1f);

            Vector2 position = new(400, 300);
            Vector2 scale = new(150, 100);
            float rotation = MathF.Sin(GameTime.TotalElapsedSec) * MathF.PI * 2;

            Matrix4x4 tran = Matrix4x4.CreateTranslation(position.X, position.Y, 0);
            Matrix4x4 sca = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
            Matrix4x4 rot = Matrix4x4.CreateRotationZ(rotation);

            shader.Use();
            shader.SetMatrix4x4("model", sca * 1 * tran);
            shader.SetMatrix4x4("projection", cam.GetProjectionMatrix());
        }


        protected override void Render()
        {
            MpClearColor((int)Color.gray);
            glClear(GL_COLOR_BUFFER_BIT);


            VAO.Bind();
            glDrawArrays(GL_TRIANGLES, 0, tri.Length);
            VAO.Unbind();

            UpdateVAO.Bind();
            glDrawArrays(GL_TRIANGLES, 0, playervert.Length);
            UpdateVAO.Unbind();

            SWFVAO.Bind();
            glDrawArrays(GL_TRIANGLES, 0, SWFVert.Length);
            SWFVAO.Unbind();

            LineVAO.Bind();
            glDrawArrays(GL_LINES,0,RayVert.Length);
            LineVAO.Unbind();


            VBO.Delete();
            LineVBO.Delete();
            UpdateVBO.Delete();
            SWFVBO.Delete();

            Glfw.SwapBuffers(DisplayManager.Window);
        }


        protected unsafe override void Update()
        {
            Movment.MovePlayer();

            Player.pos = new Vector2(Player.X, Player.Y);
            Colision.ColisionPoint = new Vector2((int)(Player.X + (Player.deltaX) * 5), (int)(Player.Y + (Player.deltaY) * 5));
            Colision.NegativeColisionPoint = new Vector2((int)(Player.X - (Player.deltaX) * 5), (int)(Player.Y - (Player.deltaY) * 5));


            VAO.Bind();

            fixed (float* v = &tri[0])
            {
                VBO = new(v, sizeof(float) * tri.Length);
            }
            VAO.LinkVBO(VBO, 0);

            VAO.Unbind();
            VBO.Unbind();

            playervert = DrawPlayer();
            UpdateVAO.Bind();

            fixed (float* v = &playervert[0])
            {
                UpdateVBO = new(v, sizeof(float) * playervert.Length);
            }
            UpdateVAO.LinkVBO(UpdateVBO, 0);

            UpdateVAO.Unbind();
            UpdateVBO.Unbind();

            RayVert = Rays.DrawWalls().ToArray();
            LineVAO.Bind();

            fixed (float* v = &RayVert[0])
            {
                LineVBO = new(v, sizeof(float) * RayVert.Length);
            }
            LineVAO.LinkVBO(LineVBO, 0);
            LineVAO.Unbind();

            SWFVert= Rays.SWF.ToArray();
            SWFVAO.Bind();

            fixed (float* v = &SWFVert[0])
            {
                SWFVBO = new(v, sizeof(float) * SWFVert.Length);
            }
            SWFVAO.LinkVBO(SWFVBO, 0);
            SWFVAO.Unbind();

            ShowFPS(DisplayManager.Window);
            
            /*            
                { 
                Console.WriteLine(Player.X);
                Console.WriteLine(Player.Y);
                Console.WriteLine(Player.angle);
                Console.WriteLine(Movment.W);
                Console.WriteLine(Movment.A);
                Console.WriteLine(Movment.S);
                Console.WriteLine(Movment.D);
                Console.SetCursorPosition(0, 0);
                }
            */
        }




        static public List<float> DrawMap()
        {
            List<float> trimap = new();
            int xo, yo;
            Vector3 pixelCol;

            for (int y = 0; y < Maps.mapY; y++)
                for (int x = 0; x < Maps.mapX; x++)
                {
                    if (Maps.ammoMap[y * Maps.mapX + x] == 1) { pixelCol = new Vector3(0f, 1f, 0f); }
                    else if (Maps.wallMap[y * Maps.mapX + x] == 1) { pixelCol = Vector3.One; }
                    else { pixelCol = Vector3.Zero; }
                    xo = x * Maps.mapS; yo = y * Maps.mapS;

                    float[] temp = {
                        (float)xo + 1, yo + 1, pixelCol.X, pixelCol.Y, pixelCol.Z,
                        (float)xo + 1, yo + Maps.mapS - 1, pixelCol.X, pixelCol.Y, pixelCol.Z,
                        (float)xo + Maps.mapS - 1, yo + Maps.mapS - 1, pixelCol.X, pixelCol.Y, pixelCol.Z,

                        (float)xo + Maps.mapS - 1, yo + Maps.mapS - 1, pixelCol.X, pixelCol.Y, pixelCol.Z,
                        (float)xo + Maps.mapS - 1, yo + 1, pixelCol.X, pixelCol.Y, pixelCol.Z,
                        (float)xo + 1, yo + 1, pixelCol.X, pixelCol.Y, pixelCol.Z 
                    };
                    trimap.AddRange(temp);
                }
            return trimap;
        }

        static private float[] DrawPlayer()
        {
            float[] playertri =
            {
                Player.X-5, Player.Y-5, 1f, 1f, 0f,
                Player.X-5, Player.Y+5, 1f, 1f, 0f,
                Player.X+5, Player.Y-5, 1f, 1f, 0f,

                Player.X+5, Player.Y-5, 1f, 1f, 0f,
                Player.X+5, Player.Y+5, 1f, 1f, 0f,
                Player.X-5, Player.Y+5, 1f, 1f, 0f,

            };
            return playertri;
        }
        static private float[] DrawColision()
        {
            float[] colvert =
            {
                Player.X,Player.Y,0f,0f,1f,
                Colision.ColisionPoint.X, Colision.ColisionPoint.Y,0f, 0f, 1f,

                Player.X,Player.Y,1f,0f,0f,
                Colision.NegativeColisionPoint.X, Colision.NegativeColisionPoint.Y,1f, 0f, 0f,
            };
            return colvert;
        }
        static void MpClearColor(int index)
        {
            glClearColor(col[index].X, col[index].Y, col[index].Z, col[index].W);
        }
        static void ShowFPS(GLFW.Window w)
        {   
            var fps = (1/Game.GameTime.DeltaTime);
                Glfw.SetWindowTitle(DisplayManager.Window, "Test | FPS: " + ((int)fps));
        }

        /*
        protected override void KeyCallback(Window window, Keys key, int scanCode, InputState state, ModifierKeys mods)
        {
            Wolf3D.Movment.Callback(window, key, scanCode, state, mods);
        }
        */
        public Test(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle) { }
        protected override void Initialize() 
        {
            Player.X = 300;
            Player.Y = 300;
            Player.pos = new Vector2(300, 300);

            audio1 = new NAudio.Wave.WaveFileReader("src/snd/audio1.wav");
            gunshot = new NAudio.Wave.WaveFileReader("src/snd/gunshot.wav");

            soundFx =      new NAudio.Wave.DirectSoundOut();
            musicChannel = new NAudio.Wave.DirectSoundOut();

            fxChannel = new NAudio.Wave.WaveChannel32(gunshot, .1f, 0);
            musicChannel.Init(new NAudio.Wave.WaveChannel32(audio1, .1f, 0));
        }

        public static NAudio.Wave.DirectSoundOut soundFx = null;
        public static NAudio.Wave.DirectSoundOut musicChannel = null;
        public static NAudio.Wave.WaveChannel32 fxChannel = null;
        public static NAudio.Wave.WaveFileReader audio1 = null;
        public static NAudio.Wave.WaveFileReader gunshot = null;
    }
}
