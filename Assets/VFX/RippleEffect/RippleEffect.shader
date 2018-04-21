Shader "Custom/RippleEffect" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
        _Ripple1("Ripple 1", Vector) = (0.5, 0.5, 0, 0)
        _Ripple2("Ripple 2", Vector) = (0.5, 0.5, 0, 0)
        _Ripple3("Ripple 3", Vector) = (0.5, 0.5, 0, 0)
		_Frequency("Frequency", Float) = 1
		_Speed("Speed", Float) = 1
		_Aamplitude("Aamplitude", Float) = 1
		_Decay("Decay", Float) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		struct Input {
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		float3 _Ripple1;
		float3 _Ripple2;
		float3 _Ripple3;
		float _Frequency;
		float _Speed;
		float _Aamplitude;
		float _Decay;

		// sin(pi*x) = 4(x-x*abs(x)), x=-1~1
		// height = sin(d-t*pi)/(d+1)
		float wave (float2 position, float2 origin, float time) {
			if (time < 0) return 0;
			float d = length (position - origin);
			// if (d * 3.1415926 < (time - 3.0 / _Frequency) * _Speed) return 0;
			float x = (d - _Speed - time) * _Frequency;
			return _Aamplitude * sin (x) * pow (0.5, d + time * _Decay);
    	}

		float allwave (float2 position) {
			return wave(position, _Ripple1.xy, _Ripple1.z) + wave(position, _Ripple2.xy, _Ripple2.z) + wave(position, _Ripple3.xy, _Ripple3.z);
		}

		void vert (inout appdata_full v) {
            v.vertex.xyz += v.normal * allwave(v.vertex.xy);
        }

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
