#version 330 core
layout (location = 0) in vec3  aPos;
layout (location = 1) in vec3  aNormal;
layout (location = 2) in vec2  aTexCoords;
layout (location = 3) in vec3  tangent;
layout (location = 4) in vec3  bitangent;

out vec2 TexCoords;
out vec3 ex_N;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform vec3 lightPosition;
uniform vec3 lightDirection;
uniform vec3 eye;

uniform vec3 lightPosition2;
uniform vec3 lightDirection2;

out vec3 EyeDirection_cameraspace;
out vec3 LightDirection_cameraspace;
out vec3 Normal_cameraspace;

out vec3 EyeDirection_cameraspace2;
out vec3 LightDirection_cameraspace2;
out vec3 Normal_cameraspace2;

uniform float tradius;
uniform float theta;
uniform float alpha;

void main()
{

    vec4 PosL = vec4(aPos, 1.0f);

    //PosL.x += tradius * cos(theta);
    //PosL.y += tradius * sin(theta);
    //PosL.z += 0.0f;

    //PosL.x += (tradius + 2.0f * cos(alpha)) * cos (theta);
    //PosL.y += (tradius + 2.0f * cos(alpha)) * sin (theta);
    //PosL.z += 2.0f * sin (alpha);

    PosL.x += theta;
    PosL.y += 0.0f;
    PosL.z += tradius * abs(sin(theta)) / theta;

    gl_Position = projection * view * model * PosL;

    TexCoords = aTexCoords;  

    vec3 vertexPosition_cameraspace = ( view * model * vec4(aPos,1)).xyz;
    EyeDirection_cameraspace = vec3(0,0,0) - vertexPosition_cameraspace;
    vec3 LightPosition_cameraspace = ( view * vec4(lightPosition,1)).xyz;
    LightDirection_cameraspace = LightPosition_cameraspace + EyeDirection_cameraspace;
    Normal_cameraspace = ( view * model * vec4(aNormal,0)).xyz;

    vec3 vertexPosition_cameraspace2 = ( view * model * vec4(aPos,1)).xyz;
    EyeDirection_cameraspace2 = vec3(0,0,0) - vertexPosition_cameraspace2;
    vec3 LightPosition_cameraspace2 = ( view * vec4(lightPosition2,1)).xyz;
    LightDirection_cameraspace2 = LightPosition_cameraspace2 + EyeDirection_cameraspace2;
    Normal_cameraspace2 = ( view * model * vec4(aNormal,0)).xyz;

    ex_N = aNormal;
}