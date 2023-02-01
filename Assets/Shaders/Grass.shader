Shader "Custom/Grass"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _RED("Red", Range(0, 255)) = 0
        _GREEN("Green", Range(0, 255)) = 0
        _BLUE("Blue", Range(0, 255)) = 0

        _BendRotationRandom("Bend Rotation Random", Range(0, 1)) = 0.2
        _BladeWidth("Blade Width", Float) = 0.05
        _BladeWidthRandom("Blade Width Random", Float) = 0.02
        _BladeHeight("Blade Height", Float) = 0.5
        _BladeHeightRandom("Blade Height Random", Float) = 0.3

        _WindDistortionMap("Wind Distortion Map", 2D) = "white" {}
        _WindFrequency("Wind Frequency", Vector) = (0.05, 0.05, 0, 0)
        _WindStrength("Wind Strength", Float) = 1

        _TessellationUniform("Tessellation Uniform", Range(1, 64)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off

        CGINCLUDE
        #include "UnityCG.cginc"
        #include "./CustomTessellation.cginc"

        sampler2D _MainTex;

        float _RED, _GREEN, _BLUE;

        float _BendRotationRandom;
        float _BladeHeight;
        float _BladeHeightRandom;	
        float _BladeWidth;
        float _BladeWidthRandom;

        sampler2D _WindDistortionMap;
        float4 _WindDistortionMap_ST;
        float2 _WindFrequency;
        float _WindStrength;

        struct geometryOutput
        {
            float4 pos : SV_POSITION;
        };

        // Simple noise function, sourced from http://answers.unity.com/answers/624136/view.html
        // Returns a number in the 0...1 range.
        float rand(float3 co)
        {
            return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
        }

        // Construct a rotation matrix that rotates around the provided axis, sourced from:
        // https://gist.github.com/keijiro/ee439d5e7388f3aafc5296005c8c3f33
        float3x3 AngleAxis3x3(float angle, float3 axis)
        {
            float c, s;
            sincos(angle, s, c);

            float t = 1 - c;
            float x = axis.x;
            float y = axis.y;
            float z = axis.z;

            return float3x3(
                t * x * x + c, t * x * y - s * z, t * x * z + s * y,
                t * x * y + s * z, t * y * y + c, t * y * z - s * x,
                t * x * z - s * y, t * y * z + s * x, t * z * z + c
                );
        }

        float4 frag (vertexOutput IN) : COLOR 
        {   
            return float4(_RED, _GREEN, _BLUE, 0) / 255;
        }

        [maxvertexcount(3)]
        void geo(triangle vertexOutput IN[3] : SV_POSITION, inout TriangleStream<geometryOutput> triStream)
        {
            float3 pos = IN[0].vertex;
            float height = (rand(pos.zyx) * 2 - 1) * _BladeHeightRandom + _BladeHeight;
            float width = (rand(pos.xzy) * 2 - 1) * _BladeWidthRandom + _BladeWidth;

            float2 uv = pos.xz * _WindDistortionMap_ST.xy + _WindDistortionMap_ST.zw + _WindFrequency * _Time.y;
            float2 windSample = (tex2Dlod(_WindDistortionMap, float4(uv, 0, 0)).xy * 2 - 1) * _WindStrength;
            float3 wind = normalize(float3(windSample.x, windSample.y, 0));

            float3x3 windRotationMatrix = AngleAxis3x3(UNITY_PI * windSample, wind);
            float3x3 facingRotationMatrix = AngleAxis3x3(rand(pos) * UNITY_TWO_PI, float3(0, 0, 1));
            float3x3 bendRotationMatrix = AngleAxis3x3(rand(pos.zzx) * _BendRotationRandom * UNITY_PI * 0.5, float3(-1, 0, 0));
            float3x3 transformationMatrix = mul(windRotationMatrix, mul(facingRotationMatrix, bendRotationMatrix));

            geometryOutput o;

            o.pos = UnityObjectToClipPos(pos + mul(transformationMatrix, float3(width, 0, 0)));
            triStream.Append(o);

            o.pos = UnityObjectToClipPos(pos + mul(transformationMatrix, float3(-width, 0, 0)));
            triStream.Append(o);

            o.pos = UnityObjectToClipPos(pos + mul(transformationMatrix, float3(0, height, 0)));
            triStream.Append(o);
        }

        ENDCG

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geo
            #pragma hull hull
            #pragma domain domain

            ENDCG
        }
    }
}
