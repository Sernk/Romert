sampler2D TextureSampler : register(s0);

float Time;
float Strength;

float4 MainPS(float2 texCoord : TEXCOORD0) : COLOR
{
    float2 offset;

    offset.x = sin(Time * 40 + texCoord.y * 20) * Strength;
    offset.y = cos(Time * 35 + texCoord.x * 25) * Strength;

    float2 shakenUV = texCoord + offset;

    return tex2D(TextureSampler, shakenUV);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 MainPS();
    }
}