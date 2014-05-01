Shader "Particles/Alpha Blended3" {
Properties {
	_MainTex ("Particle Texture", 2D) = "white" {}
	_Length ("Length", Range(0.0, 1.0)) = 1.0
	_Width ("Width", Range(0.0, 1.0)) = 1.0
	_LocalPos1 ("LocalPos1", Vector) = (0,0,0,0)
	_LocalPos2 ("LocalPos2", Vector) = (0,0,0,0)
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest Greater .01
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off

	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float3 _WorldPos;
		    float4 _LocalPos1;
		    float4 _LocalPos2;
		    float _Length;
		    float _Width;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				#ifdef SOFTPARTICLES_ON
				float4 projPos : TEXCOORD1;
				#endif
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				#ifdef SOFTPARTICLES_ON
				o.projPos = ComputeScreenPos (o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				#endif
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
			   	_WorldPos = mul (_Object2World, v.vertex).xyz;
				return o;
			}

			sampler2D _CameraDepthTexture;
			
			fixed4 frag (v2f i) : COLOR
			{
				#ifdef SOFTPARTICLES_ON
				float sceneZ = LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
				float partZ = i.projPos.z;
				#endif
				if ((i.texcoord.x<0) || (i.texcoord.y<0)){
					return fixed4(255,0,0,1) * tex2D(_MainTex, i.texcoord);
				}else{
					return fixed4(0,255,0,1) * tex2D(_MainTex, i.texcoord);
				}
				
				//if ((i.texcoord.x<0) || (i.texcoord.y<0) || ((i.texcoord.y<_WorldPos.y-_LocalPos1.y) && _LocalPos1.y!=0)
		        //    	|| ((i.texcoord.y>_WorldPos.y+_LocalPos2.y)&& _LocalPos2.y!=0) || ((i.texcoord.x<_WorldPos.x-_LocalPos1.x) && _LocalPos1.x!=0)
		        //    	|| ((i.texcoord.x>_WorldPos.x+_LocalPos2.x)&& _LocalPos2.x!=0) ){
		        //        fixed4 colorTransparent = fixed4(125,125,125,255) ;
		        //        return colorTransparent * tex2D(_MainTex, i.texcoord) ;
		        //}else{
		        //	return fixed4(125,125,125,255) * tex2D(_MainTex, i.texcoord);
		        //}
			}
			ENDCG 
		}
	}	
}
}
