Shader "Unlit/BgShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _NormalMap("NormalMap", 2D) = "white" {}
        _Color("Color" , Color) = (1,0,0,1)
        _Speed("Speed", Range(0,110)) = 20
    }
        SubShader
        {
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                sampler2D _NormalMap;
                float4 _NormalMap_ST;


                float _Speed;
                fixed4 _Color;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float SinY = sin((i.uv.y * i.uv.y * i.uv + _Time.y / _Speed) * 10) / 20;
                    float2 NewPos = float2(i.uv.x * i.uv.x + SinY, i.uv.y * i.uv.y - SinY);
                    fixed4 norm = tex2D(_NormalMap, NewPos);
                    fixed4 col = tex2D(_MainTex, NewPos) * (norm + 0.4) * _Color;
                    return col;
                }
                ENDCG
            }
        }
}
