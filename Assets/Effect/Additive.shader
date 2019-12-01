Shader "Effect/Additive"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Enable("Enable", FLOAT) = 0
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One Zero

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex, _ScreenTexture, _PreviousTexture;
			float _Enable;

			fixed4 frag (v2f i) : SV_Target
			{
				float space = 0.9;

				fixed4 screen = tex2D(_ScreenTexture, i.uv)*_Enable;
				//fixed4 prev = tex2D(_PreviousTexture, (i.uv - 0.5) * 1space1 + 0.5) * sign(max(0, 1 - (abs(i.uv.x - 0.5) + abs(i.uv.y - 0.5)) * space));
				fixed4 prev = tex2D(_PreviousTexture, (i.uv - 0.5) * space + 0.5) * max(0, sign(abs(i.uv.x-0.5)+abs(i.uv.y-0.5)-0.1));/* *
					min(sign(max(0, 0.5 - abs(i.uv.x - 0.5)*space)),
						sign(max(0, 0.5 - abs(i.uv.y - 0.5)*space)));*/

				//screen.a *= _Enable;
				//prev.rgb *= prev.a - screen.a;
				screen.a -= prev.a;
				screen.rgb *= screen.a;

				return screen + prev;
			}
			ENDCG
		}
	}
}
