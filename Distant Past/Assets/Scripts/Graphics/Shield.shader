Shader "Custom/ScrollingDownShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _EmissionTex ("Emission Texture", 2D) = "black" {}
        _Color ("Color", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _OffsetSpeed ("Offset Speed", Range(0,5)) = 1.0
        _Transparency ("Transparency", Range(0,1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade

        sampler2D _MainTex;
        sampler2D _EmissionTex;
        fixed4 _Color;
        fixed4 _EmissionColor;
        half _Smoothness;
        half _OffsetSpeed;
        half _Transparency;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_EmissionTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Calculate a downward scrolling offset
            float2 offsetUV = IN.uv_MainTex;
            offsetUV.y -= _Time.y * _OffsetSpeed;

            float2 emissionOffsetUV = IN.uv_EmissionTex;
            emissionOffsetUV.y -= _Time.y * _OffsetSpeed;

            // Base texture with color tint and transparency
            fixed4 c = tex2D(_MainTex, offsetUV) * _Color;
            o.Albedo = c.rgb;

            // Emission texture
            fixed4 emission = tex2D(_EmissionTex, emissionOffsetUV) * _EmissionColor;
            o.Emission = emission.rgb;

            // Smoothness
            o.Smoothness = _Smoothness;

            // Set the alpha value for transparency
            o.Alpha = c.a * _Transparency;
        }
        ENDCG
    }
    FallBack "Transparent/Diffuse"
}
