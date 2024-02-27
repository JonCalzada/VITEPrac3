#version 330 core
out vec4 FragColor;

in vec2 TexCoords;
in vec3 ex_N; 

in vec3 EyeDirection_cameraspace;
in vec3 LightDirection_cameraspace;
in vec3 Normal_cameraspace;

in vec3 EyeDirection_cameraspace2;
in vec3 LightDirection_cameraspace2;
in vec3 Normal_cameraspace2;

uniform sampler2D texture_diffuse1;

uniform vec4 LightColor;
uniform vec4 LightPower;
uniform int  alphaIndex;
uniform float distance;

uniform vec4 LightColor2;
uniform vec4 LightPower2;
uniform int  alphaIndex2;
uniform float distance2;

uniform vec4 MaterialAmbientColor;
uniform vec4 MaterialDiffuseColor;
uniform vec4 MaterialSpecularColor;
uniform float transparency;

void main()
{    

    // Cálculo de componente difusa
    vec3 n = normalize( Normal_cameraspace );
    vec3 l = normalize( LightDirection_cameraspace );

    vec3 n2 = normalize( Normal_cameraspace2 );
    vec3 l2 = normalize( LightDirection_cameraspace2 );

    vec4 texel = texture(texture_diffuse1, TexCoords);
    
    float cosTheta = clamp( dot( n,l ), 0,1 );
    float cosTheta2 = clamp( dot( n2,l2 ), 0,1 );

    // Cálculo de componente difusa
    vec4 MaterialAmbColor = MaterialAmbientColor * LightColor;
    vec4 MaterialAmbColor2 = MaterialAmbientColor * LightColor2;

    // Cálculo de componente especular
    vec3 E = normalize(EyeDirection_cameraspace);
    vec3 R = reflect(-l,n);

    vec3 E2 = normalize(EyeDirection_cameraspace2);
    vec3 R2 = reflect(-l2,n2);

    float cosAlpha = clamp( dot( E,R ), 0,1 );
    float cosAlpha2 = clamp( dot( E2,R2 ), 0,1 );

    vec4 ex_color = MaterialAmbColor  +  //* LightPower / (distance*distance)
                    MaterialDiffuseColor * LightColor * LightPower * cosTheta / (distance*distance) +
                    MaterialSpecularColor * LightColor * LightPower * pow(cosAlpha,alphaIndex) / (distance*distance) +
                    MaterialAmbColor2 +
                    MaterialDiffuseColor * LightColor2 * LightPower2 * cosTheta2 / (distance2*distance2) +
                    MaterialSpecularColor * LightColor2 * LightPower2 * pow(cosAlpha2,alphaIndex2) / (distance2*distance2);
    ex_color.a = transparency;

    FragColor = texel * ex_color;
}