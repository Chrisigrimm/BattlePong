Shader "test/test"
{
	Properties
	{
	    _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
	    _LocalPos1 ("LocalPos1", Vector) = (0,0,0,0)
	    _LocalPos2 ("LocalPos2", Vector) = (0,0,0,0)
	 }
	Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater .01
		ColorMask 0
		Cull Off Lighting Off ZWrite Off

		SubShader
		{
		    Tags
		    {
		        "Queue" = "Transparent"
		        "IgnoreProjector" = "True"
		        "RenderType" = "Transparent"
		    }

		    Pass
		    {
		        Blend SrcAlpha OneMinusSrcAlpha
				AlphaTest Greater .01
				ColorMask RGB
				Cull Off Lighting Off ZWrite On

		        CGPROGRAM
		        #pragma vertex vert
		        #pragma fragment frag
		        #include "UnityCG.cginc"
				
		        sampler2D _MainTex;
		        float4 _MainTex_ST;
		        float3 _WorldPos;
		        float4 _LocalPos1;
		        float4 _LocalPos2;
				
		        struct appdata_t
		        {
		            float4 vertex : POSITION;
		            float2 texcoord : TEXCOORD0;
		        };

		        struct v2f
		        {
		            float4 vertex : POSITION;
		            float2 texcoord : TEXCOORD2;
		            float3 wpos : TEXCOORD0;
		        };

		        v2f vert (appdata_t v){
		            v2f o;
					_WorldPos = mul (_Object2World, v.vertex).xyz;
		            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
		            o.texcoord = v.texcoord;
		            o.wpos = _WorldPos;
		            return o;
		        }

		        fixed4 frag (v2f IN) : COLOR
		        {
		             if (((IN.texcoord.x>_LocalPos1.x)&& _LocalPos1.x!=0) || ((IN.texcoord.x<_LocalPos2.x)&& _LocalPos2.x!=0) ||
		             	 ((IN.texcoord.y>_LocalPos1.y)&& _LocalPos1.y!=0) || ((IN.texcoord.y<_LocalPos2.y)&& _LocalPos2.y!=0)
		             	){
		                fixed4 colorTransparent = fixed4(0,0,0,0) ;
		                return  colorTransparent * tex2D(_MainTex, IN.texcoord) ;
		            }else{
		         	   return tex2D(_MainTex, IN.texcoord);
		         	}
		        }
		        ENDCG
		    }
		}
	}
}