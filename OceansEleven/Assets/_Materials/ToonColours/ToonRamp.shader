Shader "Test/ToonRamp" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_RampTex("Ramp Texure", 2D) = "white"{}

	}

		SubShader{
		Tags{
		"Queue" = "Geometry"
}

		CGPROGRAM
		#pragma surface surf ToonRamp

		fixed4 _Color;
		sampler2D _RampTex;

		float4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			//diffuse lighting with dot product
			float diff = dot(s.Normal, lightDir);

			//not typical halfway - used to give uv values against ramp texture
			float h = diff * 0.5 + 0.5;
			float2 rh = h;
			float3 ramp = tex2D(_RampTex, rh).rgb;

			//work out color values
			float4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (ramp);
			c.a = s.Alpha;
			return c;
		}

		

	struct Input {
		float2 uv_MainTex;
	};




	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = _Color.rgb;


	}
	ENDCG
	}
		FallBack "Diffuse"
}
