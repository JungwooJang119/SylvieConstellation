#ifndef NEBULAFIELD_INCLUDED
#define NEBULAFIELD_INCLUDED

void nebulafield_float(float3 p, float s, float time, out float output)
{
    float strength = 7. + .03 * log(1.e-6 + frac(sin(time) * 4373.11));
    float accum = s / 4.;
    float prev = 0.;
    float tw = 0.;
    for (int i = 0; i < 26; ++i) {
        float mag = dot(p, p);
        p = abs(p) / mag + float3(-.5, -.4, -1.5);
        float w = exp(-float(i) / 7.);
        accum += w * exp(-strength * pow(abs(mag - prev), 2.3));
        tw += w;
        prev = mag;
    }
    output = max(0., 5. * accum / tw - .7);
}

#endif