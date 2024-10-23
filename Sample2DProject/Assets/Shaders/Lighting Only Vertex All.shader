Shader "Lighting Only All" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        // Blending for transparency
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Tags {Queue = Transparent}
        ColorMask RGB

        // Per-pixel lighting
        Pass {
            Tags {"LightMode" = "ForwardBase"}
            Lighting On
            Material {
                Diffuse [_Color]
            }

            SetTexture [_MainTex] {
                Combine texture * primary DOUBLE, texture * primary
            }

            // Adjust transparency based on lighting
            SetTexture [_MainTex] {
                constantColor [_Color]
                Combine previous, previous * primary
            }
        }
    }
    Fallback  "Diffuse", 2
}