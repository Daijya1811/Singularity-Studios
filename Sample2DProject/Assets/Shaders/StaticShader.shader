Shader "Custom/StaticNoiseShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _NoiseStrength ("Noise Strength", Range(0, 1)) = 0.5
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _NoiseStrength;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Simple random function using uv and time
            float randomNoise(float2 uv, float time)
            {
                return frac(sin(dot(uv.xy + time, float2(12.9898, 78.233))) * 43758.5453);
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Sample the main texture
                float4 texColor = tex2D(_MainTex, uv);

                // Generate static noise
                float noise = randomNoise(uv, _Time);

                // Blend noise with the texture based on strength
                texColor.rgb = lerp(texColor.rgb, noise.xxx, _NoiseStrength);

                return texColor * _Color;
            }
            ENDCG
        }
    }
}