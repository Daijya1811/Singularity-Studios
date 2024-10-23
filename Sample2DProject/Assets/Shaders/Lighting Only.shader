Shader "Custom/Lighting Only" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }

    SubShader {
        Tags { "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ColorMask RGB

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            // Sample the main texture and apply color tint
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // Set base color and alpha directly from texture
            o.Albedo = texColor.rgb;
            o.Alpha = saturate(dot(_LightColor0.rgb, texColor.rgb));

            // Use clip() to discard pixels with 0 alpha
            clip(o.Alpha - 0.01); 
        }
        ENDCG
    }

    Fallback "Diffuse"
}