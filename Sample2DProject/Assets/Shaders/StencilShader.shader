Shader "Unlit/StencilOnlyShader"
{
    SubShader
    {
        LOD 100
        ZWrite Off
        ColorMask 0   // Disables color writing

        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
        }
    }
}

