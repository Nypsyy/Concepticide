Shader "Custom/OutlineBlur"
{
    Properties
    {
        _BlurRadius("Blur Radius", Range(0.0, 20.0)) = 1
        _Intensity("Blur Intensity", Range(0.0, 1.0)) = 0.1
        _OutlineWidth("Outline Width", Range(1.0, 10.0)) = 1.1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
        }

        GrabPass
        {
        }

        Pass
        {
            Name "OUTLINEHORIZONTALBLUR"

            ZWrite Off

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
            struct v2f {
                float4 vertex : SV_POSITION;
                float4 uvgrab : TEXCOORD0;
            };

            // =================================================
            // Imports - Re-import property from shader lab to Nvidia CG
            // =================================================

            float _BlurRadius;
            float _Intensity;
            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
            float _OutlineWidth;

            // =================================================
            // Vertex Functions - Build the object
            // =================================================

            v2f vert(appdata_base IN) {
                IN.vertex.xyz *= _OutlineWidth + 0.1;
                v2f OUT;

                OUT.vertex = UnityObjectToClipPos(IN.vertex);

                #if UNITY_UV_STARTS_AT_TOP
                float scale = -1.0;
                #else
                float scale = 1.0;
                #endif

                OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * scale) + OUT.vertex.w) * 0.5;
                OUT.uvgrab.zw = OUT.vertex.zw;

                return OUT;
            }

            // =================================================
            // Fragment Functions - Color it in
            // =================================================

            half4 frag(v2f IN) : COLOR {
                half4 texcol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab));
                half4 texsum = half4(0, 0, 0, 0);

                #define GRABPIXEL(weigth, kernelX) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(IN.uvgrab.x + _GrabTexture_TexelSize.x * kernelX * _BlurRadius, IN.uvgrab.y, IN.uvgrab.z, IN.uvgrab.w))) * weigth

                texsum += GRABPIXEL(0.05, -4.0);
                texsum += GRABPIXEL(0.09, -3.0);
                texsum += GRABPIXEL(0.12, -2.0);
                texsum += GRABPIXEL(0.15, -1.0);
                texsum += GRABPIXEL(0.18, -0.0);
                texsum += GRABPIXEL(0.15, 1.0);
                texsum += GRABPIXEL(0.12, 2.0);
                texsum += GRABPIXEL(0.09, 3.0);
                texsum += GRABPIXEL(0.05, 4.0);

                texcol = lerp(texcol, texsum, _Intensity);
                return texcol;
            }
            ENDCG
        }

        GrabPass
        {
        }

        Pass
        {
            Name "OUTLINEVERTICALBLUR"
            
            ZWrite Off

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

            struct v2f {
                float4 vertex : SV_POSITION;
                float4 uvgrab : TEXCOORD0;
            };

            // =================================================
            // Imports - Re-import property from shader lab to Nvidia CG
            // =================================================

            float _BlurRadius;
            float _Intensity;
            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
            float _OutlineWidth;

            // =================================================
            // Vertex Functions - Build the object
            // =================================================

            v2f vert(appdata_base IN) {
                IN.vertex.xyz *= _OutlineWidth + 0.1;
                v2f OUT;

                OUT.vertex = UnityObjectToClipPos(IN.vertex);

                #if UNITY_UV_STARTS_AT_TOP
                float scale = -1.0;
                #else
                float scale = 1.0;
                #endif

                OUT.uvgrab.xy = (float2(OUT.vertex.x, OUT.vertex.y * scale) + OUT.vertex.w) * 0.5;
                OUT.uvgrab.zw = OUT.vertex.zw;

                return OUT;
            }

            // =================================================
            // Fragment Functions - Color it in
            // =================================================

            half4 frag(v2f IN) : COLOR {
                half4 texcol = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.uvgrab));
                half4 texsum = half4(0, 0, 0, 0);

                #define GRABPIXEL(weigth, kernelY) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(IN.uvgrab.x, IN.uvgrab.y + _GrabTexture_TexelSize.y * kernelY * _BlurRadius, IN.uvgrab.z, IN.uvgrab.w))) * weigth

                texsum += GRABPIXEL(0.05, -4.0);
                texsum += GRABPIXEL(0.09, -3.0);
                texsum += GRABPIXEL(0.12, -2.0);
                texsum += GRABPIXEL(0.15, -1.0);
                texsum += GRABPIXEL(0.18, -0.0);
                texsum += GRABPIXEL(0.15, 1.0);
                texsum += GRABPIXEL(0.12, 2.0);
                texsum += GRABPIXEL(0.09, 3.0);
                texsum += GRABPIXEL(0.05, 4.0);

                texcol = lerp(texcol, texsum, _Intensity);
                return texcol;
            }
            ENDCG
        }
    }
}