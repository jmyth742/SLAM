Shader "Custom/PitShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_CliffMap1 ("Cliff map", 2D) = "white" {}
		_DirtNormal ("Dirt Normal", 2D) = "white" {}

		_DirtMask  ("Dirt mask", 2D) = "white" {}
		_LightMap  ("Lightmap", 2D) = "white" {}


		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0


		_tile1 ("Tiling", Vector) = (100, 100, 100, 100)

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard
		#pragma debug

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _DirtNormal;

		sampler2D _CliffMap1;


		sampler2D _DirtMask;
		sampler2D _LightMap;


		float4 _tile1;

		struct Input {
			float2 uv_MainTex;
			float2 uv2_DirtMask;
			float3 worldPos;
			float3 worldNormal;

		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		float4 triPlanar(float3 worldPos, float3 normal, float tile) {
			float4 col1 = tex2D(_CliffMap1, float2((worldPos.zy*tile)/200.0));
			float4 col2 = tex2D(_CliffMap1, float2((worldPos.xz*tile)/200.0));
			float4 col3 = tex2D(_CliffMap1, float2((worldPos.xy*tile)/200.0));

			float3 res = normalize(saturate(abs(normal) - 0.4));
			return col1*res.x + col2*res.y + col3*res.z;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {

			// Texture coordinates for the aerial image



			// The dynamic road map

			float4 maskData = tex2D(_DirtMask, IN.uv2_DirtMask);
			float4 ao = tex2D(_LightMap, IN.uv2_DirtMask);

			float3 norm = normalize(IN.worldNormal);
			float cliffAmt = 1 - abs(norm.y);
			float realCliffAmt = (1 - abs(norm.y))*4 - 0.25;
			cliffAmt = lerp(cliffAmt, realCliffAmt, maskData.g);

			float4 texCol = tex2D(_MainTex, float2(_tile1.xy*IN.uv_MainTex));

			float4 cliffCol = triPlanar(IN.worldPos, norm, 2);
			texCol.rgb = lerp(texCol.rgb, cliffCol, cliffAmt);

			texCol.rgb = texCol.rgb*ao.rgb;

			o.Albedo = texCol;

			// Metallic and smoothness come from slider variables
			o.Metallic = 0;
			o.Smoothness = 0;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
