Shader "Hidden/Distortion"
{
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_DisplacementTex("DisplacementTex", 2D) = "white" {}
		_Strength("Strength", Float) = 0.5
	}
	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _DisplacementTex;

			fixed _Strength;

			
			float4 frag(v2f_img i) : COLOR{
				/*half2 n = tex2D(_DisplacementTex, i.uv);
				half2 d = n * 2 - 1;
				i.uv += d * _Strength;
				i.uv = saturate(i.uv);*/

				float2 signs = float2(sign(i.uv.x-0.5), sign(i.uv.y - 0.5));
				//i.uv = (pow((i.uv - 0.5) * 2, 6) / 2)*signs + 0.5;

				i.uv += signs*_Strength* pow(tex2D(_DisplacementTex, i.uv), 4).r/ _ScreenParams.xy;

				float4 c = tex2D(_MainTex, i.uv);
				return c;// pow(tex2D(_DisplacementTex, i.uv), 4);
			}
			ENDCG
		}
	}
}
