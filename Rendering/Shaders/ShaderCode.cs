using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreator.Rendering.Shaders
{
    public static class ShaderCode
    {
       public static string vertexShader = 
            @"#version 330 core
            layout (location = 0) in vec2 aPosition;
            layout (location = 1) in vec3 aColor;
            out vec4 vertexColor;
            uniform mat4 projection;
            uniform mat4 model;
            void main()
            {
                vertexColor = vec4(aColor.rgb, 1.0);
                gl_Position = projection *  vec4(aPosition.xy, 0, 1.0);
            }";

        public static string fragmentShader = 
            @"#version 330 core
            out vec4 FragColor;
            in vec4 vertexColor;
                                        
            void main()
            {
                FragColor = vertexColor;
            }";
    }
}
