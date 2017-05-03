Shader "Unlit/ExampleShader"
{
	Properties
	{
		_Tint("Color", Color) = (1,1,1,1)
		_Diffuse("Diffuse", float) = 1
		_Specular("Specular", float) = 128.0
		_Ambient("Ambient", float) = 1.0
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vertex
			#pragma fragment fragment
			#include "UnityCG.cginc"


			float4 _Tint;
			float _Diffuse;
			float _Specular;
			float _Ambient;
			float3 lightpos = float3(0.0, 0.0, 0.0);

			struct toVert {
				float4 position : POSITION;
				float3 normal : NORMAL;
			};

			struct toFrag {
				float4 gl_Position : SV_POSITION;
				float3 ex_color : TEXCOORD0;
				float3 ex_normal : TEXCOORD1;
			};


			toFrag vertex(toVert i) 
			{
				toFrag f;
				f.ex_color = _Tint; 
				f.ex_normal = mul(unity_ObjectToWorld, float4(i.normal, 0.0));
				f.gl_Position = mul(UNITY_MATRIX_MVP, i.position);
				return f;
			}

			float4 fragment(toFrag i) : SV_TARGET 
			{
				float4 pos = i.gl_Position;
				float3 v = normalize(-pos.rgb);
				float3 n = float4(i.ex_normal, 0.0);
				float3 l = normalize(lightpos - pos.xyz);
				float3 h = normalize(l+v);
				float3 spec = pow(max(dot(h, n), 0.0), _Specular);
				float3 diffuse = max(dot(l, n), 0.0) * _Diffuse;	
				return float4(_Tint*_Ambient + i.ex_color.xyz * diffuse + spec, 1.0f);
			}

			ENDCG
		}
	}
}
