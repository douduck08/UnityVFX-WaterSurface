Shader "Custom/FlowMap" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_FlowSpeed ("Flow Speed", Float) = 1
      	_FlowIntensity ("Flow Intensity", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input {
			float2 uv_MainTex;
			fixed4 color : Color;
		};

		sampler2D _MainTex;
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
			Flow(IN.uv_MainTex, IN.color.rg, _FlowSpeed, _FlowIntensity, uv1, uv2, interp);

			fixed4 c1 = tex2D (_MainTex, uv1);
			fixed4 c2 = tex2D (_MainTex, uv2);
			fixed4 cf = lerp(c1, c2, interp);
			o.Albedo = cf.rgb;
			// o.Albedo = fixed3(uv1.x, uv1.y, 0);
			// o.Albedo = IN.color.rgb;
            o.Alpha = cf.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
