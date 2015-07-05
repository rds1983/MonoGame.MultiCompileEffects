#include "Macros.fxh"

#pragma shader_feature LIGHTNING

BEGIN_CONSTANTS

#ifdef LIGHTNING
float3 dirLight0Direction;
float4 dirLight0DiffuseColor;
#endif

MATRIX_CONSTANTS

float4x4 worldViewProj;
float3x3 worldInverseTranspose;
float4 diffuseColor;

END_CONSTANTS

struct VSInput
{
    float4 Position : SV_Position;
#ifdef LIGHTNING
    float3 Normal   : NORMAL;
#endif
};

struct VSOutput
{
    float4 PositionPS : SV_Position;
	float4 Color: COLOR;
};

VSOutput VertexShaderFunction(VSInput input)
{
    VSOutput output;

    output.PositionPS = mul(input.Position, worldViewProj);

#ifdef LIGHTNING	
    float3 normal = mul(input.Normal, worldInverseTranspose);
    float lightIntensity = dot(-normal, dirLight0Direction);
    output.Color = float4(saturate(dirLight0DiffuseColor * lightIntensity));
#endif

    return output;
}

float4 PixelShaderFunction(VSOutput input) : SV_Target0
{
#ifdef LIGHTNING	
	return diffuseColor * input.Color;
#else
    return diffuseColor;
#endif
}

TECHNIQUE(Default, VertexShaderFunction, PixelShaderFunction);