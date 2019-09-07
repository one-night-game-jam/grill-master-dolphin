Shader "Custom/Skin"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _SpecularIntensity ("Specular Intensity", Range(0.0, 1.0)) = 0.5
    }

    SubShader
    {
        Tags { "Queue"="Geometry" "RenderType"="Opaque" }

        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            Blend One Zero
            Cull Back
            ZTest LEqual
            ZWrite On

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #pragma target 3.0
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Assets/Shaders/BaseLighting.cginc"

            struct vertex_in
            {
                float4 vertex : POSITION;
                float2 uv_main : TEXCOORD0;
                float2 uv_lightmap : TEXCOORD1;
                float3 normal : NORMAL;
            };

            struct vertex_out
            {
                float4 position : SV_POSITION;
                float2 uv_main : Texcoord0;
                float2 uv_lightmap : Texcoord1;
                float3 world_position : WorldPosition;
                float3 world_normal : WorldNormal;

                float4 pos : Texcoord2;
                UNITY_SHADOW_COORDS(3)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Color;

            half _SpecularIntensity;

            vertex_out vert(vertex_in v)
            {
                vertex_out o = (vertex_out)0;
                o.world_position = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.position = mul(UNITY_MATRIX_VP, float4(o.world_position, 1));
                o.uv_main = TRANSFORM_TEX(v.uv_main, _MainTex);
                o.world_normal = UnityObjectToWorldNormal(v.normal);

                o.pos = o.position;
                UNITY_TRANSFER_SHADOW(o, v.uv_lightmap);

                return o;
            }

            half4 frag(vertex_out v) : SV_Target
            {
                half3 albedo = tex2D(_MainTex, v.uv_main).rgb * _Color.rgb;

                float3 world_normal = normalize(v.world_normal);
                float3 world_lightdir = normalize(UnityWorldSpaceLightDir(v.world_position));
                float3 world_viewdir = normalize(UnityWorldSpaceViewDir(v.world_position));

                half3 diffuse = get_diffuse(world_normal, world_lightdir);
                half3 ambient = get_ambient(world_normal);
                half3 fresnel = get_fresnel(world_normal, world_lightdir, world_viewdir);
                half3 specular = get_specular(world_normal, world_lightdir, world_viewdir, _SpecularIntensity);
                UNITY_LIGHT_ATTENUATION(shadow, v, v.world_position);

                half3 result = albedo * ((diffuse + specular) * shadow + ambient + fresnel);
                return half4(result, 1.0);
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
