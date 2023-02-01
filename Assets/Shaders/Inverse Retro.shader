Shader "Custom/Inverse Retro"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Delta ("Delta", float) = 0
        _Threshold ("Threshold", float) = 0
        _RED ("Red", float) = 0
        _GREEN ("Green", float) = 0
        _BLUE ("Blue", float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGINCLUDE
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float _Delta;
        float _Threshold;
        float _RED, _GREEN, _BLUE;

        float toGray(float4 pixel)
        {
            return (pixel.r + pixel.g + pixel.b) / 3;
        }

        float sobel (sampler2D tex, float2 uv, float delta, float threshold)
        {
            float4 z1 = tex2D(tex, uv + float2(-1, -1) * delta) * 255;
            float4 z3 = tex2D(tex, uv + float2(1, -1) * delta) * 255;
            float4 z2 = tex2D(tex, uv + float2(0, -1) * delta) * 255;
            float4 z4 = tex2D(tex, uv + float2(-1, 0) * delta) * 255;
            float4 z5 = tex2D(tex, uv + float2(0, 0) * delta) * 255;
            float4 z6 = tex2D(tex, uv + float2(1, 0) * delta) * 255;
            float4 z7 = tex2D(tex, uv + float2(-1, 1) * delta) * 255;
            float4 z8 = tex2D(tex, uv + float2(0, 1) * delta) * 255;
            float4 z9 = tex2D(tex, uv + float2(1, 1) * delta) * 255;

            float4 deltaX = (z7 + 2 * z8 + z9) - (z1 + 2 * z2 + z3);
            float4 deltaY = (z3 + 2 * z6 + z9) - (z1 + 2 * z4 + z7);
            float mag = sqrt(deltaX * deltaX + deltaY * deltaY);

            mag = ceil((mag / 255) - (threshold / 255));
            return mag;
        }

        float bayer( sampler2D tex, v2f_img IN, float delta, float level)
        {
            float4x4 bayer = float4x4(0, 8, 2, 10,
                                      12, 4, 14, 6,
                                      3, 11, 1, 9,
                                      15, 7, 13, 5);

            
            float2 cords = IN.uv *_ScreenParams.xy;
            float4 pixel = tex2D(tex, IN.uv) * 255;

            float brightness = toGray(pixel) / 255;
            float threshold = bayer[fmod(cords.x, 4)][fmod(cords.y, 4)] / 16 - 0.5;

            return ceil(brightness - threshold);
        }

        float4 frag (v2f_img IN) : COLOR 
        {
            float4 COLOR = float4(_RED, _GREEN, _BLUE, 0);

            float edge = sobel(_MainTex, IN.uv, _Delta, _Threshold);
            float dither = bayer(_MainTex, IN, _Delta, 3);
            float4 ret = (1 - edge) * (dither) * (COLOR / 255);
            
            return ret;
        }

        ENDCG

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag nolightmap
            ENDCG
        }
    }
}
