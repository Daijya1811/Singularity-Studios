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
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade nofog
        #pragma target 2.0 // Ensure compatibility with WebGL

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            // Sample the main texture and apply color tint
            fixed4 texColor = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // Set base color
            o.Albedo = texColor.rgb;

            // Use the main light color for alpha control
            fixed3 mainLightColor = _LightColor0.rgb;
            o.Alpha = saturate(dot(mainLightColor, texColor.rgb));
        }
        ENDCG
    }

    Fallback "Diffuse"
}