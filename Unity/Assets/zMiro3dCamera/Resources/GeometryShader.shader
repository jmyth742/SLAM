// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GeometryShader" {
	
		Properties{
			_Size("Size", Range(0, 3)) = 0.05
			_Color("Color", Color) = (1,1,1,1)
			_Amount("Height Adjustment", Float) = 0.0
		}
	
		SubShader{
			Pass{
				Tags{ "RenderType" = "Opaque" }
				LOD 200

		CGPROGRAM
#pragma target 5.0
		//#pragma addshadow
#pragma vertex vert
#pragma geometry GS_Main
#pragma fragment frag
#include "UnityCG.cginc"

			float _Size;

		// Vert to geo
		struct v2g
	{
		float4 pos: POSITION;
	};

	// geo to frag
	struct g2f
	{
		float4 pos: POSITION;
	};


	// Vars
	float4 _Color;
	float _Amount;


	// Vertex modifier function
	v2g vert(appdata_base v) {

		v2g output = (v2g)0;
		output.pos = mul(unity_ObjectToWorld, v.vertex);		
		return output;
	}

	// GS_Main(point v2g p[1], inout TriangleStream<g2f> triStream)
	// GS_Main(line v2g p[2], inout TriangleStream<g2f> triStream)
	// GS_Main(triangle v2g p[3], inout TriangleStream<g2f> triStream)
	[maxvertexcount(4)]
	void GS_Main(point v2g p[1], inout TriangleStream<g2f> triStream)
	{
		float4 v[4];

		float3 up = float3(0, 1, 0);
		float3 look = _WorldSpaceCameraPos - p[0].pos;
		look.y = 0; // activate this line to freeze the orientation up-down 
		look = normalize(look);
		float3 right = cross(up, look);

		float dist = distance(_WorldSpaceCameraPos, p[0].pos);
		float halfS = 0.5f * _Size;// *dist;

		v[0] = float4(p[0].pos + halfS * right - halfS * up, 1.0f);
		v[1] = float4(p[0].pos + halfS * right + halfS * up, 1.0f);
		v[2] = float4(p[0].pos - halfS * right - halfS * up, 1.0f);
		v[3] = float4(p[0].pos - halfS * right + halfS * up, 1.0f);

		g2f pIn;


		for (int i = 0; i < 4; i++) {
			pIn.pos = UnityObjectToClipPos(v[i]);
			triStream.Append(pIn);
		}

		/*float4x4 vp = UnityObjectToClipPos(unity_WorldToObject);
		g2f pIn;
		pIn.pos = mul(vp, v[0]);
		triStream.Append(pIn);

		pIn.pos = mul(vp, v[1]);
		triStream.Append(pIn);

		pIn.pos = mul(vp, v[2]);
		triStream.Append(pIn);

		pIn.pos = mul(vp, v[3]);
		triStream.Append(pIn);*/
	}

	fixed4 frag(g2f input) : COLOR{
		return _Color;
	}

		ENDCG
	}


	}

		FallBack "Diffuse"
}