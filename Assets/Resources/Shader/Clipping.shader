Shader "Sprites/Clipping"
{
Properties
{
    _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
    _Length ("Length", Range(0.0, 1.0)) = 1.0
    _Width ("Width", Range(0.0, 1.0)) = 1.0
    _LocalPos1 ("LocalPos1", Vector) = (0,0,0,0)
    _LocalPos2 ("LocalPos2", Vector) = (0,0,0,0)
 }

SubShader
{
    LOD 200

    Tags
    {
        "Queue" = "Transparent"
        "IgnoreProjector" = "True"
        "RenderType" = "Transparent"
    }

    Pass
    {
        Cull Off
        Lighting Off
        ZWrite off
        Offset -1, -1
        Fog { Mode Off }
        ColorMask RGB
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float _Length;
        float _Width;
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

        v2f vert (appdata_t v)
	        {
            v2f o;
			_WorldPos = mul (_Object2World, v.vertex).xyz;
            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
            o.texcoord = v.texcoord;
            o.wpos = _WorldPos;
            return o;
        }

        half4 frag (v2f IN) : COLOR
        {
            if ((IN.texcoord.x<0) || (IN.texcoord.x>_Width) || (IN.texcoord.y<0) || (IN.texcoord.y>_Length) || ((IN.texcoord.y<IN.wpos.y-_LocalPos1.y+IN.texcoord.y) && _LocalPos1.y!=0)
            	|| ((IN.texcoord.y>IN.wpos.y+_LocalPos2.y+IN.texcoord.y)&& _LocalPos2.y!=0) || ((IN.texcoord.x<IN.wpos.x-_LocalPos1.x+IN.texcoord.x) && _LocalPos1.x!=0)
            	|| ((IN.texcoord.x>IN.wpos.x+_LocalPos2.x+IN.texcoord.x)&& _LocalPos2.x!=0) ){
                half4 colorTransparent = half4(0,0,0,0) ;
                return  colorTransparent ;
            }else{
         	   return tex2D(_MainTex, IN.texcoord);
         	}
        }
        ENDCG
    }
}
}