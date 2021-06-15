Shader "Custom/ApplyTexture"
{
    Properties
    {
        _MainTex("Main Texture (RGB)", 2D) = "White" {} // Allows for a texture property

        _Color("Color", Color) = (1,1,1,1) // Allows for a color property
    }

    SubShader
    {
        Pass
        {
            CGPROGRAM
            // Allows talk between 2 languages : shader lab & Nvidia C for graphics

            // =================================================
            // Function Defines - Defines the name of the vertex & fragment functions
            // =================================================

            #pragma vertex vert // Define for building function
            #pragma fragment frag // Define for coloring function

            // =================================================
            // Includes
            // =================================================

            #include "UnityCG.cginc" // Built-in shader functions

            // =================================================
            // Structures - Can get data like - vertices', normal, color, uv
            // =================================================

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // =================================================
            // Imports - Re-import property from shader lab to Nvidia CG
            // =================================================

            float4 _Color;
            sampler2D _MainTex;

            // =================================================
            // Vertex Functions - Build the object
            // =================================================

            v2f vert(appdata IN) {
                v2f OUT;

                OUT.pos = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;

                return OUT;
            }

            // =================================================
            // Fragment Functions - Color it in
            // =================================================

            fixed4 frag(v2f IN) : SV_Target {
                float4 texColor = tex2D(_MainTex, IN.uv);

                return texColor * _Color;
            }
            ENDCG
        }
    }
}