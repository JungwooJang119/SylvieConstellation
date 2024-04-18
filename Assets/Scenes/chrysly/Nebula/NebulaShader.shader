Shader "Unlit/NebulaShader"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    	_NoiseTex ("NoiseTex", 2D) = "white" {}
    	_SpaceColor ("SpaceColor", Color) = (0.05, 0.05, 0.2, 1.)
    	_Layer1Color ("Layer1Color", Color) = (0.2, 0.3, 0.5, 1.)
    	_Layer2Color ("Layer2Color", Color) = (0.5, 0.3, 0.2, 1.)
    	_Scale ("Scale", Float) = 1.
    }
    SubShader {
        Tags { "RenderType"="Transparent" }

        Pass {
        	ZTest Off
            ZWrite Off
            Blend One OneMinusSrcAlpha
            
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct MeshData {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            	float4 worldPos : SV_Target0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _NoiseTex;
            float4 _NoiseTex_ST;

            float4 _SpaceColor;
            float4 _Layer1Color;
            float4 _Layer2Color;
            float _Scale;

            float field(in float3 p, float s) {
	            float strength = 7. + .03 * log(1.e-6 + frac(sin(_Time.x) * 4373.11));
	            float accum = s/4.;
	            float prev = 0.;
	            float tw = 0.;
	            for (int i = 0; i < 64; ++i) {
		            float mag = dot(p, p);
		            p = abs(p) / mag + float3(-.5, -.4, -1.5);
		            float w = exp(-float(i) / 7.);
		            accum += w * exp(-strength * pow(abs(mag - prev), 2.2));
		            tw += w;
		            prev = mag;
	            }
	            return max(0., 5. * accum / tw - .7);
            }

            float field2(in float3 p, float s) {
            	p /= _Scale;
				float strength = 7. + .03 * log(1.e-6 + frac(sin(_Time.x) * 4373.11));
				float accum = s/4.;
				float prev = 0.;
				float tw = 0.;
				for (int i = 0; i < 24; ++i) {
					float mag = dot(p, p);
					p = abs(p) / mag + float3(-.5, -.4, -1.5);
					float w = exp(-float(i) / 7.);
					accum += w * exp(-strength * pow(abs(mag - prev), 2.2));
					tw += w;
					prev = mag;
				}
				return max(0., 5. * accum / tw - .7);
			}

            float field3(in float3 p, float s) {
            	p /= _Scale;
				float strength = 7. + .03 * log(1.e-6 + frac(sin(_Time.x) * 4373.11));
				float accum = s/4.;
				float prev = 0.;
				float tw = 0.;
				for (int i = 0; i < 32; ++i) {
					float mag = dot(p, p);
					p = abs(p) / mag + float3(-.5, -.4, -1.5);
					float w = exp(-float(i) / 7.);
					accum += w * exp(-strength * pow(abs(mag - prev), 2.2));
					tw += w;
					prev = mag;
				}
				return max(0., 5. * accum / tw - .7);
			}

            float3 hash3( float2 co ) {
				float3 a = frac(cos(co.x * 8.3e-3 + co.y) * float3(1.3e5, 4.7e5, 2.9e5));
				float3 b = frac(sin(co.x * 0.3e-3 + co.y) * float3(8.1e5, 1.0e5, 0.1e5));
				float3 c = lerp(a, b, 0.5);
				return c;
			}
            
            Interpolators vert (MeshData v) {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            	o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target {
            	float2 uv = i.uv;
            	
            	//= clamp(i.uv * _Scale, 0., 1.0);
				float3 p = float3(uv / 4., 0) + float3(1., -1.3, 0.);
				p += .2 * float3(sin(_Time.x / 16.), sin(_Time.x / 12.),  sin(_Time.x / 128.));
				
				float freqs[5];
				freqs[0] = tex2D(_NoiseTex, float2(0.01, 0.25)).x;
				freqs[1] = tex2D(_NoiseTex, float2(0.07, 0.25)).x;
				freqs[2] = tex2D(_NoiseTex, float2(0.15, 0.25)).x;
				freqs[3] = tex2D(_NoiseTex, float2(0.30, 0.25)).x;
            	freqs[4] = tex2D(_NoiseTex, float2(0.45, 0.25)).x;
            	
				float t = field(p,freqs[2]);
				float v = (1. - exp((abs(uv.x) - 1.) * 6.)) * (1. - exp((abs(uv.y) - 1.) * 6.));
				
			    //Second Layer
				float3 p2 = float3(uv / (4. + sin(_Time.x * 0.11) * 0.2 + 0.2 + sin(_Time * 0.15) * 0.3 + 0.4), 1.5) + float3(2., -1.3, -1.);
				p2 += 0.25 * float3(sin(_Time.x / 16.), sin(_Time.x / 12.),  sin(_Time.x / 128.));
				float t2 = field2(p2,freqs[3]);
				float4 c2 = lerp(.4, 1., v) * float4(1.3 * t2 * t2 * t2 ,1.8  * t2 * t2 , t2 * freqs[0], t2) * _Layer1Color;

            	//Third Layer
            	float3 p3 = float3(uv / (3. + cos(_Time.x * 0.34) * 0.2 + 0.2 + sin(_Time * 0.15) * 0.3 + 0.4), 1.5) + float3(2., -1.3, -1.);
				p3 += 0.25 * float3(sin(_Time.x / 16.), sin(_Time.x / 12.),  sin(_Time.x / 128.));
				float t3 = field2(p3,freqs[4]);
            	//float4 topColor = float4(_Layer2Color.r)
				float4 c3 = lerp(.4, 1., v) * float4(1.3 * t3 * t3 * t3 ,1.8  * t3 * t3 , t3 * freqs[0], t3) * _Layer2Color;
            	
				float4 fragColor = (lerp(freqs[3]-.3, 1., v) * float4(1.5 * freqs[2] * t * t * t, 1.2 * freqs[1] * t * t, freqs[3]*t, 1.0) * _SpaceColor) + c2 + c3;
				return float4(fragColor.xyz, 1.);
            }
            ENDCG
        }
    }
}