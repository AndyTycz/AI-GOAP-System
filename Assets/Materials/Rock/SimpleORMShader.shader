// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SimpleORMShader"
{
	Properties
	{
		_Rocks_ORM("Rocks_ORM", 2D) = "white" {}
		_Rocks_Normal("Rocks_Normal", 2D) = "white" {}
		_Rocks_Base("Rocks_Base", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Rocks_Normal;
		uniform float4 _Rocks_Normal_ST;
		uniform sampler2D _Rocks_Base;
		uniform float4 _Rocks_Base_ST;
		uniform sampler2D _Rocks_ORM;
		SamplerState sampler_Rocks_ORM;
		uniform float4 _Rocks_ORM_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Rocks_Normal = i.uv_texcoord * _Rocks_Normal_ST.xy + _Rocks_Normal_ST.zw;
			o.Normal = tex2D( _Rocks_Normal, uv_Rocks_Normal ).rgb;
			float2 uv_Rocks_Base = i.uv_texcoord * _Rocks_Base_ST.xy + _Rocks_Base_ST.zw;
			o.Albedo = tex2D( _Rocks_Base, uv_Rocks_Base ).rgb;
			float2 uv_Rocks_ORM = i.uv_texcoord * _Rocks_ORM_ST.xy + _Rocks_ORM_ST.zw;
			float4 tex2DNode1 = tex2D( _Rocks_ORM, uv_Rocks_ORM );
			o.Metallic = tex2DNode1.r;
			o.Occlusion = tex2DNode1.g;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
236;75;1080;888;1731.594;836.4408;2.700142;True;False
Node;AmplifyShaderEditor.SamplerNode;3;-704.0321,-171.8988;Inherit;True;Property;_Rocks_Base;Rocks_Base;2;0;Create;True;0;0;False;0;False;-1;ad83d640fd4d39246b23f815f85bd2a8;ad83d640fd4d39246b23f815f85bd2a8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-727.3732,59.3372;Inherit;True;Property;_Rocks_Normal;Rocks_Normal;1;0;Create;True;0;0;False;0;False;-1;8632ff75c26c1464194b5e29e4c03e97;8632ff75c26c1464194b5e29e4c03e97;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-587.8555,311.716;Inherit;True;Property;_Rocks_ORM;Rocks_ORM;0;0;Create;True;0;0;False;0;False;-1;ce35c5a8d8d920c49ac0c7f50b48e3ea;ce35c5a8d8d920c49ac0c7f50b48e3ea;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;SimpleORMShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;3;0
WireConnection;0;1;2;0
WireConnection;0;3;1;1
WireConnection;0;5;1;2
ASEEND*/
//CHKSM=592426DB73C6A013993D714AA9A29CF526C6A8B0