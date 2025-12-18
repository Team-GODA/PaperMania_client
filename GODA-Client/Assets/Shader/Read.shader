Shader "Custom/Portal"
{
	Properties
	{
    	// 0~255 사이의 int 값을 프로퍼티로 조작 가능
		[IntRange] _StencilID("_Stencil ID", Range(0,255)) = 0
	}
	SubShader{
		Tags
		{
        	//스텐실 버퍼에 값을 먼저 기록
			"Queue" = "Geometry-1"
		}
		Pass
		{
        	// 깊이 계산 안함
			Zwrite off
            
            // 컬러를 출력하지 않음
			ColorMask 0
			
            // 뒷면만 출력
			Cull front

			Stencil
			{
				Ref [_StencilID]
				Comp always
				Pass replace
			}
		}
	}
}