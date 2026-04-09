sampler2D TextureSampler : register(s0);

float2 uTextureSize;

float Brightness(float4 c)
{
    return dot(c.rgb, float3(0.299, 0.587, 0.114));
}

float4 PixelShaderFunction(float2 uv : TEXCOORD0) : COLOR0
{
    float4 center = tex2D(TextureSampler, uv);

    if (center.a > 0.1)
        return center;

    float2 px = 1.0 / uTextureSize;

    float4 c1 = tex2D(TextureSampler, uv + float2(px.x, 0));
    float4 c2 = tex2D(TextureSampler, uv - float2(px.x, 0));
    float4 c3 = tex2D(TextureSampler, uv + float2(0, px.y));
    float4 c4 = tex2D(TextureSampler, uv - float2(0, px.y));

    float minB = 1.0;
    float4 darkest = float4(0, 0, 0, 1);

    if (c1.a > 0.1 && Brightness(c1) < minB)
    {
        minB = Brightness(c1);
        darkest = c1;
    }

    if (c2.a > 0.1 && Brightness(c2) < minB)
    {
        minB = Brightness(c2);
        darkest = c2;
    }

    if (c3.a > 0.1 && Brightness(c3) < minB)
    {
        minB = Brightness(c3);
        darkest = c3;
    }

    if (c4.a > 0.1 && Brightness(c4) < minB)
    {
        minB = Brightness(c4);
        darkest = c4;
    }

    if (minB < 1.0)
        return darkest;

    return float4(0, 0, 0, 0);
}

technique Outline
{
    pass P0
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}