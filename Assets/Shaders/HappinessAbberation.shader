Shader "Posteffects/Happiness Aberration"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Cull Off
        ZTest Always
        ZWrite Off

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct vertex_in
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertex_out
            {
                float4 position : SV_POSITION;
                float2 uv : Texcoord;
            };

            vertex_out vert(vertex_in v)
            {
                vertex_out o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Happiness;

            const static float AberrationGamma = 0.75;
            const static float MaxAberration = 0.2;

            half4 frag(vertex_out v) : SV_Target
            {
                float2 outer_dir = v.uv * 2 - 1;
                float2 offset = -outer_dir * pow(length(outer_dir), AberrationGamma) * MaxAberration * _Happiness / 3;
                half red = tex2D(_MainTex, v.uv + offset * 1).r;
                half green = tex2D(_MainTex, v.uv + offset * 2).g;
                half blue = tex2D(_MainTex, v.uv + offset * 3).b;
                return half4(red, green, blue, 1);
            }

            ENDCG
        }
    }
}
