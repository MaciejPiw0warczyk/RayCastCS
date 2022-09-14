using GLFW;
using static OpenGL.GL;
namespace MapCreator.Rendering.Shaders
{
     unsafe class VAO
    {
        public uint VAOID;
        public VAO()
        {
            VAOID=glGenVertexArray();
        }

        public void LinkVBO(VBO VBO, int layout)
        {
            VBO.Bind();

            glVertexAttribPointer(0, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
            glEnableVertexAttribArray(0);

            glVertexAttribPointer(1, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)(2 * sizeof(float)));
            glEnableVertexAttribArray(1);

            VBO.Unbind();
        }
        public void Bind() { glBindVertexArray(VAOID); }
        public void Unbind() { glBindVertexArray(0); }
        public void Delete() { glDeleteVertexArray(VAOID); }
    }
}
