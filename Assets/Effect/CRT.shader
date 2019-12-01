Shader "Hidden/CRT"
{
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Size(" Size", Float) = 1
	}
	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;

			fixed _Size;

			fixed4 frag(v2f_img i) : COLOR{

			i.uv = float2(i.uv.x-i.uv.x%_Size, i.uv.y - i.uv.y%_Size);

			fixed4 base = tex2D(_MainTex, i.uv);
			return  base;
			}
			ENDCG
		}
	}
}
