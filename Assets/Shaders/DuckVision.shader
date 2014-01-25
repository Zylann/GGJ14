Shader "Duck/Duck vision"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_HiddenTexture ("Duck world texture", 2D) = "white" {}
		_Mask ("Mask (greyscale)", 2D) = "white" {}
		_Fade ("Fading value", Range(0.0,1.0)) = 0.0
	}
	
	SubShader
	{
		Tags { "Queue"="transparent+10" "RenderType"="Transparent" }
		LOD 200
		
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		Lighting Off
		ZWrite Off
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
	
			sampler2D _MainTex;
			sampler2D _HiddenTexture;
			sampler2D _Mask;
			
			float4 _MainTex_ST; // Needed by the TRANSFORM_TEX macro
			float  _Fade;
			
			struct vertexInput
			{
				float4 vertex : POSITION;
				float2 texcoord0 : TEXCOORD0;
			};
	
			struct fragmentInput
			{
				float4 position : SV_POSITION;
				float2 texcoord0 : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				float2 texcoord2 : TEXCOORD2;
			};
	
			fragmentInput vert(vertexInput i)
			{
				fragmentInput o;
				o.texcoord0 = TRANSFORM_TEX(i.texcoord0, _MainTex); // This handles the "tiling" and "offset" texture parameters
				o.position = mul (UNITY_MATRIX_MVP, i.vertex);
				o.texcoord1 = i.vertex.xy;
				o.texcoord2 = 0.5*o.position.xy+0.5;
				return o;
			}
			
			float4 frag(fragmentInput i) : COLOR
			{
				float4 realColor = tex2D(_MainTex, i.texcoord0.xy);
				float m = tex2D(_Mask, i.texcoord2.xy).r * _Fade;
				float4 hiddenColor = tex2D(_HiddenTexture, i.texcoord1.xy);
				float4 finalColor = realColor * (1.0-m) + hiddenColor * m;
				return finalColor;
			}
			
			ENDCG
		}
	}
	FallBack "Diffuse"
}


