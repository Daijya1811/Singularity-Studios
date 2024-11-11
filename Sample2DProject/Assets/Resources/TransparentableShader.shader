Shader "Custom/StencilReaderStandard"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color Tint", Color) = (1,1,1,1)
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _MetallicMap ("Metallic Map", 2D) = "black" {}
        _AOMap ("Ambient Occlusion Map", 2D) = "white" {} // New AO map property
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // Stencil settings to control visibility
        Stencil
        {
            Ref 1
            Comp NotEqual   // Render only where stencil is NOT 1
        }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _NormalMap;
        sampler2D _MetallicMap;
        sampler2D _AOMap; // Declare the AO map texture
        fixed4 _Color;
        half _Glossiness;
        half _Metallic;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NormalMap;
            float2 uv_MetallicMap;
            float2 uv_AOMap; // UV for AO map
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Apply texture and color tint
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // Apply ambient occlusion by darkening the albedo based on the AO map
            fixed ao = tex2D(_AOMap, IN.uv_AOMap).r; // AO map's red channel
            o.Albedo *= ao; // Modulate albedo color by AO

            // Blend the metallic slider value with the metallic map
            o.Metallic = lerp(_Metallic, tex2D(_MetallicMap, IN.uv_MetallicMap).r, 1.0);

            // Apply smoothness
            o.Smoothness = _Glossiness;

            // Apply normal map if provided
            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
