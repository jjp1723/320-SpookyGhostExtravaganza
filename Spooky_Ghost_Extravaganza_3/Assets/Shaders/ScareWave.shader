Shader "Unlit/ScareWave"
{
    Properties
    {
        _Radius ("Radius", Float) = 1.0
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Additive" }
        //LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define TAU 6.2831853071
            float _Radius;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float getWave( float2 uv)
            {
                float2 uvsCentered = uv * 2 - 1;
                float radialDistance = length(uvsCentered);

                float wave = cos( (radialDistance - _Time.y * 0.1) * TAU * 5) * 0.5 + 0.5;
                wave *= 1 - radialDistance;

                return wave;
            }

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (MeshData v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float wave = getWave( i.uv );
                return float4(wave, wave, wave, wave);
            }
            ENDCG
        }
    }
}
