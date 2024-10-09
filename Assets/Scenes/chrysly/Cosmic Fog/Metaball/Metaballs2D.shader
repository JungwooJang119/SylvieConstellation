Shader "Hidden/Metaballs2D"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _GradientTex;
            sampler2D _GradientNoise;
            sampler2D _VoronoiNoise;

			int _MetaballCount;
			float4 _MetaballData[1000];
			float _OutlineSize;
			float4 _InnerColor;
			float4 _OutlineColor;
			float _CameraSize;

            float _Wavelength;
            float _Scale;
            float _Intensity;

            float _ScrollSpeed;
            float _SimpleScale;

            float4 playerPos;

            float Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax)
			{
				return OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
			}

            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
            
            float2 Unity_GradientNoise_Deterministic_Dir_float(float2 p)
			{
				float x; Hash_Tchou_2_1_float(p, x);
				return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
			}

            void Unity_GradientNoise_Deterministic_float (float2 UV, float3 Scale, out float Out)
			{
				float2 p = UV * Scale.xy;
				float2 ip = floor(p);
				float2 fp = frac(p);
				float d00 = dot(Unity_GradientNoise_Deterministic_Dir_float(ip), fp);
				float d01 = dot(Unity_GradientNoise_Deterministic_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
				float d10 = dot(Unity_GradientNoise_Deterministic_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
				float d11 = dot(Unity_GradientNoise_Deterministic_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
				fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
				Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
			}

            float2 Unity_Voronoi_RandomVector_Deterministic_float (float2 UV, float offset)
			{
				Hash_Tchou_2_2_float(UV, UV);
				return float2(sin(UV.y * offset), cos(UV.x * offset)) * 0.5 + 0.5;
			}

			void Unity_Voronoi_Deterministic_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
			{
				float2 g = floor(UV * CellDensity);
				float2 f = frac(UV * CellDensity);
				float t = 8.0;
				float3 res = float3(8.0, 0.0, 0.0);
				for (int y = -1; y <= 1; y++)
				{
					for (int x = -1; x <= 1; x++)
					{
						float2 lattice = float2(x, y);
						float2 offset = Unity_Voronoi_RandomVector_Deterministic_float(lattice + g, AngleOffset);
						float d = distance(lattice + offset, f);
						if (d < res.x)
						{
						res = float3(d, offset.x, offset.y);
						Out = res.x;
						Cells = res.y;
						}
					}
				}
			}

            float3 ClipToWorldPos(float4 clipPos)
			{
			#ifdef UNITY_REVERSED_Z
			    // unity_CameraInvProjection always in OpenGL matrix form
			    // that doesn't match the current view matrix used to calculate the clip space

			    // transform clip space into normalized device coordinates
			    float3 ndc = clipPos.xyz / clipPos.w;

			    // convert ndc's depth from 1.0 near to 0.0 far to OpenGL style -1.0 near to 1.0 far 
			    ndc = float3(ndc.x, ndc.y * _ProjectionParams.x, (1.0 - ndc.z) * 2.0 - 1.0);

			    // transform back into clip space and apply inverse projection matrix
			    float3 viewPos =  mul(unity_CameraInvProjection, float4(ndc * clipPos.w, clipPos.w));
			#else
			    // using OpenGL, unity_CameraInvProjection matches view matrix
			    float3 viewPos = mul(unity_CameraInvProjection, clipPos);
			#endif

			    // transform from view to world space
			    return mul(unity_MatrixInvV, float4(viewPos, 1.0)).xyz;
			}

            float4 frag (v2f_img i) : SV_Target
            {
				float4 tex = tex2D(_MainTex, i.uv);

            	//Gradient
            	float3 viewPos = UnityWorldToViewPos(ClipToWorldPos(i.pos));
            	float2 uv = viewPos * 0.0003 + _Time;
            	float4 gradTex = tex2D(_GradientTex, uv);
            	gradTex = clamp(gradTex, float4(0.2, 0.2, 0.2, 0.2), float4(1., 1., 1., 1.));

            	//Wavy distance field
            	float4 noiseTex = tex2D(_GradientNoise, i.uv * _Scale + (_Time * _Wavelength));
            	float remap_noiseTex = Unity_Remap_float(noiseTex.x, float2(0, 1), float2(-1, 1));
            	float noiseVec = remap_noiseTex * _Intensity;

				float dist = 1.0f;
            	
            	float threshold = 0.3f;
            	float outlineOffset = 0.;
            	
				for (int m = 0; m < _MetaballCount; ++m)
				{
					float2 metaballPos = _MetaballData[m].xy;

					float distFromMetaball = distance(metaballPos, i.uv * _ScreenParams.xy);
					float radiusSize = _MetaballData[m].z * _ScreenParams.y / _CameraSize;

					if (_MetaballData[m].w == 1)
					{
						dist /= saturate(distFromMetaball / radiusSize);
					} else
					{
						dist *= saturate(distFromMetaball / radiusSize);
					}
					dist += noiseVec;
				}

            	float outlineThreshold = threshold * saturate(1.0f - _OutlineSize - outlineOffset);

            	//Galaxy shader
            	float4 color = saturate(pow(saturate(dist * 3.5), 6)) * gradTex;
            	color.w = 0.5;
            	float scroll = _ScrollSpeed * (_Time + 3);
            	float2 v_UV = viewPos * _SimpleScale + scroll;
            	float2 s_UV = viewPos * _SimpleScale - scroll;
            	float voronoi;
            	float simple;
            	float cells;

            	Unity_GradientNoise_Deterministic_float(s_UV, _SimpleScale, simple);
            	Unity_Voronoi_Deterministic_float(v_UV, 2., _SimpleScale, voronoi, cells);

            	simple = clamp(pow(saturate(simple) + 0.2, 20), 0, 4.);
            	voronoi = clamp(pow(saturate(voronoi) + 0.2, 20), 0., 4.);
            	float4 star_color = simple * voronoi * _InnerColor;
            	

				return (dist > threshold) ? tex :
					((dist > outlineThreshold) ? gradTex : color + star_color);
            }
            ENDCG
        }
    }
}
