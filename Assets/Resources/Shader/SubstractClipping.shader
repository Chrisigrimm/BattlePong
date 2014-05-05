Shader "Sprites/SubstractClipping"{
	Properties{
	    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	 }

	SubShader{
	    
	    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		
		 ZWrite on
	     Offset -1, -1
	     ColorMask 0
	     Blend SrcAlpha OneMinusSrcAlpha
		
	    Pass{
	       ZWrite on
	     	Offset -1, -1
	     	ColorMask 0
	   	 	Blend SrcAlpha OneMinusSrcAlpha
	    }	
	}
}