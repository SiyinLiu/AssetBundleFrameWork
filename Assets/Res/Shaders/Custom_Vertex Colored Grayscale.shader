Shader "Custom/Vertex Colored Grayscale" {
    Properties {
            _Color ("Main Color", Color) = (1,1,1,1)
            _Alpha("Alpha",Range(0,1)) = 1
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _Ratio ("Grayscale ratio", Range(0, 1)) = 0
            _GrayVector ("Gray-ize Vector", Color) = (0.33,0.34,0.33,1)
    }
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 200
        Pass {
            ZWrite On
            ColorMask 0
        }
    CGPROGRAM
    #pragma surface surf Lambert alpha
        fixed4 _Color;
        sampler2D _MainTex;
        float _Alpha;
        float _Ratio;
        fixed4 _GrayVector;
        struct Input {
            float2 uv_MainTex;
            float4 color: Color; // Vertex color
        };
        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = dot(tex2D (_MainTex, IN.uv_MainTex) * _Color,_GrayVector * _Ratio);
            o.Albedo = c.rgb * IN.color.rgb; // vertex RGB
            o.Alpha = _Alpha; // vertex Alpha
        }
        ENDCG
    }
    FallBack "Diffuse"
}