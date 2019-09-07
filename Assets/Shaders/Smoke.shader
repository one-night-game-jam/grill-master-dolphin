Shader "Custom/Smoke"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZTest LEqual
            ZWrite Off

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #pragma target 3.0

            #include "UnityCG.cginc"

            struct vertex_in
            {
                float4 vertex : POSITION;
                half4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct vertex_out
            {
                float4 position : SV_POSITION;
                half4 color : Color;
                float2 uv : Texcoord0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            vertex_out vert(vertex_in v)
            {
                vertex_out o = (vertex_out)0;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;

                return o;
            }

            half4 frag(vertex_out v) : SV_Target
            {
                float noise = tex2D(_MainTex, v.uv).r;
                float atten = 1.0 - saturate(length(v.uv * 2 - 1));
                float val = noise * atten;
                half4 result = half4(1, 1, 1, val) * v.color;
                return result;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
