#ifndef HASH3_INCLUDED
#define HASH3_INCLUDED

void hash3_float(float2 v, out float3 output)
{
    float3 a = frac(cos(v.x * 8.3e-3 + v.y) * float3(1.3e5, 4.7e5, 2.9e5));
    float3 b = frac(sin(v.x * 0.3e-3 + v.y) * float3(8.1e5, 1.0e5, 0.1e5));
    output = lerp(a, b, 0.5);
}

#endif