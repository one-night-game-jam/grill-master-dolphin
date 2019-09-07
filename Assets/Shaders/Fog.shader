Shader "Posteffects/Fog"
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
            sampler2D _CameraDepthTexture;

            float _FogGamma;
            float _FarDistance;

            half4 _DepthFogColor;

            float4x4 _ViewToWorld;

            half4 frag(vertex_out v) : SV_Target
            {
                // Reconstruct View Position
                float4 projection_position = float4(v.uv * 2 - 1, 1.0, 1.0) * _ProjectionParams.z;
                float4 vpos = mul(unity_CameraInvProjection, projection_position);
                float depth = tex2D(_CameraDepthTexture, v.uv).r;
                float linear_depth = Linear01Depth(depth);
                float3 view_position = vpos.xyz * linear_depth;
                float3 world_position = mul(_ViewToWorld, float4(view_position.xyz, 1)).xyz;

                half4 result = tex2D(_MainTex, v.uv);
                float fog_intensity = pow(saturate(-view_position.z / _FarDistance), _FogGamma);
                result.rgb = lerp(result.rgb, _DepthFogColor.rgb, fog_intensity);

                return result;
            }

            ENDCG
        }
    }
}
