Shader "Shader/BareBone" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
	}

	SubShader {
		Pass{
			CGPROGRAM
		#pragma vertex VertexMain
		#pragma fragment FragmentMain
			struct VertexInput
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
			};
			struct VertexOutput
			{
				float4 pos : SV_POSITION;
				float3 pass_v : TEXCOORD0;
				float3 pass_n : TEXCOORD1;
			};

			uniform float4 _Color;
			
			VertexOutput VertexMain( VertexInput v )
			{
				VertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.pass_v = normalize( _WorldSpaceCameraPos - v.vertex.xyz );
				o.pass_n = normalize( v.normal );
				return o;
			}

			half4 FragmentMain( VertexOutput f ) : COLOR
			{
				return _Color;
			}

			ENDCG
		}
	}
}