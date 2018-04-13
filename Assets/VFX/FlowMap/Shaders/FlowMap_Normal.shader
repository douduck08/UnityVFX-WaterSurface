Shader "Custom/FlowMap_Normal" {
	Properties {
		_Albedo ("Albedo", Color) = (1.0, 1.0, 1.0, 1.0) 
		_NormalMap("Normal Map", 2D) = "bump" {}
		_FlowSpeed ("Flow Speed", Float) = 1
      	_FlowIntensity ("Flow Intensity", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input {
        	float2 uv_NormalMap;
			fixed4 color : Color;
		};

		float4 _Albedo;
		sampler2D _NormalMap;
		float _FlowSpeed;
		float _FlowIntensity;

		void Flow(float2 uv, float2 flow, float speed, float intensity, out float2 uv1, out float2 uv2, out float interp) {
			float2 flowVector = (flow * 2.0 - 1.0) * intensity;
			float timeScale = _Time.z * speed;
			float2 phase;
			
			phase.x = frac(timeScale);
			phase.y = frac(timeScale + 0.5);

			uv1 = (uv - phase.x * flowVector);
			uv2 = (uv - phase.y * flowVector);
			interp = abs(0.5 - phase.x) / 0.5;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			float2 uv1;
			float2 uv2;
			float interp;
			Flow(IN.uv_NormalMap, IN.color.rg, _FlowSpeed, _FlowIntensity, uv1, uv2, interp);

			fixed3 n1 = UnpackNormal (tex2D (_NormalMap, uv1));
			fixed3 n2 = UnpackNormal (tex2D (_NormalMap, uv2));
			fixed3 nf = lerp(n1, n2, interp);
			o.Albedo = _Albedo.rgb;
            o.Alpha = _Albedo.a;
			// o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_NormalMap));
			o.Normal = nf;
		}
		ENDCG
	}
	FallBack "Diffuse"
}