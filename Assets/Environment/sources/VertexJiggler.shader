// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-2616-OUT,alpha-9689-A,voffset-2085-OUT;n:type:ShaderForge.SFN_Tex2d,id:2103,x:32010,y:32873,ptovrint:False,ptlb:Noise1,ptin:_Noise1,varname:node_2103,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ec5f05bd0450b4f4a913218e00118cec,ntxv:2,isnm:False|UVIN-5509-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1380,x:31580,y:32790,varname:node_1380,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:5509,x:31828,y:32790,varname:node_5509,prsc:2,spu:-0.5,spv:0|UVIN-1380-UVOUT,DIST-2066-OUT;n:type:ShaderForge.SFN_Time,id:668,x:31580,y:32932,varname:node_668,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8714,x:31580,y:33078,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_8714,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2066,x:31828,y:32958,varname:node_2066,prsc:2|A-668-T,B-8714-OUT;n:type:ShaderForge.SFN_Multiply,id:1750,x:32262,y:33134,varname:node_1750,prsc:2|A-2103-RGB,B-4675-XYZ;n:type:ShaderForge.SFN_Vector4Property,id:4675,x:31990,y:33286,ptovrint:False,ptlb:Magnitude,ptin:_Magnitude,varname:node_4675,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:1,v3:0,v4:0;n:type:ShaderForge.SFN_Tex2d,id:9689,x:32005,y:32544,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_9689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2616,x:32387,y:32555,varname:node_2616,prsc:2|A-9689-RGB,B-9689-A;n:type:ShaderForge.SFN_ChannelBlend,id:2085,x:32514,y:33256,varname:node_2085,prsc:2,chbt:1|M-1158-R,R-1750-OUT,G-8238-OUT,BTM-8238-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8238,x:32206,y:33344,ptovrint:False,ptlb:node_8238,ptin:_node_8238,varname:node_8238,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:1158,x:32091,y:33455,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_1158,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:de1832d87e68b974baf0bdd9c5cd4277,ntxv:0,isnm:False;proporder:2103-8714-4675-9689-8238-1158;pass:END;sub:END;*/

Shader "Shader Forge/VertexJiggler" {
    Properties {
        _Noise1 ("Noise1", 2D) = "black" {}
        _Speed ("Speed", Float ) = 1
        _Magnitude ("Magnitude", Vector) = (0,1,0,0)
        _Texture ("Texture", 2D) = "white" {}
        _node_8238 ("node_8238", Float ) = 0.5
        _Mask ("Mask", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _Noise1; uniform float4 _Noise1_ST;
            uniform float _Speed;
            uniform float4 _Magnitude;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _node_8238;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 _Mask_var = tex2Dlod(_Mask,float4(TRANSFORM_TEX(o.uv0, _Mask),0.0,0));
                float4 node_668 = _Time + _TimeEditor;
                float2 node_5509 = (o.uv0+(node_668.g*_Speed)*float2(-0.5,0));
                float4 _Noise1_var = tex2Dlod(_Noise1,float4(TRANSFORM_TEX(node_5509, _Noise1),0.0,0));
                v.vertex.xyz += (lerp( float3(_node_8238,_node_8238,_node_8238), (_Noise1_var.rgb*_Magnitude.rgb), _Mask_var.r.r ));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 emissive = (_Texture_var.rgb*_Texture_var.a);
                float3 finalColor = emissive;
                return fixed4(finalColor,_Texture_var.a);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            #pragma glsl
            uniform float4 _TimeEditor;
            uniform sampler2D _Noise1; uniform float4 _Noise1_ST;
            uniform float _Speed;
            uniform float4 _Magnitude;
            uniform float _node_8238;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 _Mask_var = tex2Dlod(_Mask,float4(TRANSFORM_TEX(o.uv0, _Mask),0.0,0));
                float4 node_668 = _Time + _TimeEditor;
                float2 node_5509 = (o.uv0+(node_668.g*_Speed)*float2(-0.5,0));
                float4 _Noise1_var = tex2Dlod(_Noise1,float4(TRANSFORM_TEX(node_5509, _Noise1),0.0,0));
                v.vertex.xyz += (lerp( float3(_node_8238,_node_8238,_node_8238), (_Noise1_var.rgb*_Magnitude.rgb), _Mask_var.r.r ));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
