Shader "ChromaticAberration"
{
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _Size("Size", Range(0.0, 1.0)) = 0.025
    }
        SubShader
        {
            Cull Off
            ZTest Always
            ZWrite Off

            Tags { "RenderType" = "Opaque" }

            Pass
            {
               CGPROGRAM
               #pragma vertex vert
               #pragma fragment frag

               #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                half _Size;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    half4 col = tex2D(_MainTex, i.uv);

                    half2 uvBase = i.uv - 0.5h;
                    // R値を拡大したものに置き換える
                    half2 uvR = uvBase * (1.0h - _Size * 2.0h) + 0.5h;
                    col.r = tex2D(_MainTex, uvR).r;
                    // G値を拡大したものに置き換える
                    half2 uvG = uvBase * (1.0h - _Size) + 0.5h;
                    col.g = tex2D(_MainTex, uvG).g;

                    return col;
                }
                ENDCG
            }
        }
}