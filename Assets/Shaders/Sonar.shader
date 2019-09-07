Shader "Posteffects/Sonar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SonarRadius ("Radius", Float) = 0.0
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

            float3 _SonarCenter;
            float _SonarRadius;
            float _SonarWidth;
            half4 _SonarColor;
            float _SonarEffectiveRadius;

            float4x4 _ViewToWorld;

            const static float Pi = 3.1415926536;

            half4 frag(vertex_out v) : SV_Target
            {
                // Reconstruct World Position
                float4 projection_position = float4(v.uv * 2 - 1, 1.0, 1.0) * _ProjectionParams.z;
                float4 vpos = mul(unity_CameraInvProjection, projection_position);
                float depth = tex2D(_CameraDepthTexture, v.uv).r;
                float3 view_position = vpos.xyz * Linear01Depth(depth);
                float3 world_position = mul(_ViewToWorld, float4(view_position.xyz, 1)).xyz;

                float radius_from_sonar = length(world_position - _SonarCenter);
                float sonar_intensity = 1.0 - (cos(saturate((radius_from_sonar - _SonarRadius + _SonarWidth) / _SonarWidth) * Pi * 2) * 0.5 + 0.5);
                float effective_radius_attenuation = 1.0 - smoothstep(_SonarEffectiveRadius - _SonarWidth, _SonarEffectiveRadius, radius_from_sonar);
                half3 sonar = sonar_intensity * effective_radius_attenuation * _SonarColor.rgb * _SonarColor.a;

                half4 result = tex2D(_MainTex, v.uv);
                result.rgb += sonar;

                return result;
            }

            ENDCG
        }
    }
}
