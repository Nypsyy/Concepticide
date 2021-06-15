Shader "Custom/FinalOutline"
{
    Properties
    {
        _MainTex("Main Texture (RGB)", 2D) = "white" {} // Allows for a texture property
        _Color("Color", Color) = (1, 1, 1, 1) // Allows for a color property

        _OutlineWidth("Outline Width", Range(0.5, 10.0)) = 1.1

        _BlurRadius("Blur Radius", Range(0.0, 20.0)) = 1
        _Intensity("Blur Intensity", Range(0.0, 1.0)) = 0.1

        _DistortColor("Distord Color", Color) = (1, 1, 1, 1)
        _BumpAmt("Distortion", Range(0, 128)) = 10
        _DistortTex("Distort Texture (RGB)", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
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
        UsePass "Custom/OutlineDistort/OUTLINEDISTORT"
        GrabPass
        {
        }
        UsePass "Custom/OutlineBlur/OUTLINEHORIZONTALBLUR"
        GrabPass
        {
        }
        UsePass "Custom/OutlineBlur/OUTLINEVERTICALBLUR"

        UsePass "Custom/Outline/OBJECT"
    }
}