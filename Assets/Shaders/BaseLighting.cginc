#ifndef BASE_LIGHTING_INCLUDE_GUARD
#define BASE_LIGHTING_INCLUDE_GUARD

#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

half3 get_diffuse(float3 world_normal, float3 world_lightdir)
{
    half diffuse_intensity = dot(world_normal, world_lightdir) * 0.5 + 0.5;
    return diffuse_intensity * _LightColor0;
}

half3 get_ambient(float3 world_normal)
{
    return ShadeSH9(float4(world_normal, 1.0));
}

half3 get_fresnel(float3 world_normal, float3 world_lightdir, float3 world_viewdir)
{
    half rim_intensity = 1.0 - dot(world_normal, world_viewdir);
    half light_intensity = dot(world_normal, -world_lightdir) * 0.5 + 0.5;
    return pow(rim_intensity * light_intensity, 5);
}

half3 get_specular(float3 world_normal, float3 world_lightdir, float3 world_viewdir, half intensity)
{
    half3 half_vector = normalize(world_lightdir + world_viewdir);
    float specular_base = saturate(dot(world_normal, half_vector));
    float specular_base_rough = specular_base * specular_base;
    float specular_base_flat = pow(specular_base, 8);
    float specular_base_peak = pow(specular_base, 512);
    half rough = specular_base_rough * 0.5;
    half smooth = specular_base_flat * 0.25 + specular_base_peak;
    return lerp(rough, smooth, intensity) * _LightColor0;
}

#endif
