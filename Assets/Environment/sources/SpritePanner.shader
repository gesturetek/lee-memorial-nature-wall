// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:0,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1873,x:33103,y:32649,varname:node_1873,prsc:2|emission-7056-OUT,alpha-8129-A;n:type:ShaderForge.SFN_ValueProperty,id:1020,x:31936,y:32867,ptovrint:False,ptlb:PanSpeed,ptin:_PanSpeed,varname:node_1020,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Panner,id:864,x:32353,y:32668,varname:node_864,prsc:2,spu:0.1,spv:0|UVIN-6339-UVOUT,DIST-8518-OUT;n:type:ShaderForge.SFN_TexCoord,id:6339,x:32115,y:32623,varname:node_6339,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:711,x:31926,y:32726,varname:node_711,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8518,x:32115,y:32796,varname:node_8518,prsc:2|A-711-T,B-1020-OUT;n:type:ShaderForge.SFN_Multiply,id:7056,x:32869,y:32736,varname:node_7056,prsc:2|A-8129-R,B-8129-A;n:type:ShaderForge.SFN_Tex2d,id:8129,x:32559,y:32785,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_8129,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-864-UVOUT;proporder:1020-8129;pass:END;sub:END;*/

Shader "Shader Forge/SpritePanner" {
    Properties {
        _PanSpeed ("PanSpeed", Float ) = 1
        _MainTex ("MainTex", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
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
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _PanSpeed;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_711 = _Time + _TimeEditor;
                float2 node_864 = (i.uv0+(node_711.g*_PanSpeed)*float2(0.1,0));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_864, _MainTex));
                float node_7056 = (_MainTex_var.r*_MainTex_var.a);
                float3 emissive = float3(node_7056,node_7056,node_7056);
                float3 finalColor = emissive;
                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
