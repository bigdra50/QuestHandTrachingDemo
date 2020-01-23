﻿Shader "Custom/Glass" {
	Properties {
		_Color      ("Color"             , Color      ) = (1, 1, 1, 1)
		_Smoothness ("Smoothness"        , Range(0, 1)) = 1
		_AlphaF     ("Alpha (Face)"      , Range(0, 1)) = 0
		_AlphaE     ("Alpha (Edge)"      , Range(0, 1)) = 0
		_AlphaR     ("Alpha (Rim)"       , Range(0, 1)) = 0
		_InRefl     ("Inner Reflectivity", Range(0, 1)) = 1

		[HDR] _EmissionColor("Emission Color", Color) = (0, 0, 0)
	}

	SubShader {
		Tags {
			"Queue"      = "Transparent"
			"RenderType" = "Transparent"
		}

		// 背景とのブレンド法を「乗算」に指定
		Blend DstColor Zero

		Cull Front

		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				half3 _Color;
				half _AlphaF;
				half _AlphaE;

				struct appdata {
					float4 vertex : POSITION;
					half   color  : COLOR;
				};

				struct v2f {
					float4 vertex  : SV_POSITION;
					half   color   : COLOR;
				};
				
				v2f vert (appdata v) {
					v2f o;

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color  = v.color;

					return o;
				}
				
				fixed4 frag (v2f i) : SV_Target {
					return fixed4(lerp(lerp(_Color, 0, _AlphaF), lerp(_Color, 0, _AlphaE), i.color), 1);
				}
			ENDCG
		}
		
		// V/FシェーダーはReflection Probeに反応しないので
		// 反射だけを描画するSurface Shaderを追記する
		CGPROGRAM
			#pragma target 3.0
			#pragma surface surf Standard alpha

			half _Smoothness;
			half _InRefl;
			
			struct Input {
				fixed null;
			};

			void surf (Input IN, inout SurfaceOutputStandard o) {
				o.Smoothness = _Smoothness;

				// 反射強度は o.Occlusion で調整できる
				o.Occlusion  = _InRefl;
			}
		ENDCG

		Cull Back

		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				half3 _Color;
				half _AlphaF;
				half _AlphaE;
				half _AlphaR;
				float4 _EmissionColor;

				struct appdata {
					float4 vertex : POSITION;
					half   color  : COLOR;
					float3 normal : NORMAL;
				};

				struct v2f {
					float4 vertex  : SV_POSITION;
					half   color   : COLOR;
					float3 normal  : NORMAL;
					float3 viewDir : TEXCOORD0;
				};
				
				v2f vert (appdata v) {
					v2f o;

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color  = v.color;
					o.normal = v.normal;
					o.viewDir = normalize(ObjSpaceViewDir(v.vertex));

					return o;
				}
				
				fixed4 frag (v2f i) : SV_Target {
					float rimFallOff = lerp(1, dot(i.viewDir, i.normal), _AlphaR);

					return (fixed4(lerp(lerp(_Color, 0, _AlphaF), lerp(_Color, 0, _AlphaE), i.color) * rimFallOff, 1) + _EmissionColor);
				}
			ENDCG
		}

		// V/FシェーダーはReflection Probeに反応しないので
		// 反射だけを描画するSurface Shaderを追記する
		CGPROGRAM
			#pragma target 3.0
			#pragma surface surf Standard alpha

			half _Smoothness;
			
			struct Input {
				fixed null;
			};

			void surf (Input IN, inout SurfaceOutputStandard o) {
				o.Smoothness = _Smoothness;
			}
		ENDCG
	}

	FallBack "Standard"
}