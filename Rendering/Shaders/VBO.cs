using GLFW;
using static OpenGL.GL;
namespace MapCreator.Rendering.Shaders
{
     unsafe class VBO
    {
        public uint VBOID;

        float* verteces;
        int size;

        public VBO(float* verteces, int size)
        {
            this.verteces = verteces;
            this.size = size;


            VBOID=glGenBuffer();
            glBindBuffer(GL_ARRAY_BUFFER, VBOID);
            glBufferData(GL_ARRAY_BUFFER, size, verteces, GL_DYNAMIC_DRAW);
        }


        public void Bind() { glBindBuffer(GL_ARRAY_BUFFER, VBOID); }
        public void Unbind() { glBindBuffer(GL_ARRAY_BUFFER, 0); }
        public void Delete() { glDeleteBuffer(VBOID); }
    }
}
