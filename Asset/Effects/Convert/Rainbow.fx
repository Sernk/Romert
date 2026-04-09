sampler2D TextureSampler : register(s0);

float2 uTextureSize;
float Time;

float4 MainPS(float2 uv : TEXCOORD0) : COLOR
{
    float4 center = tex2D(TextureSampler, uv);

    // якщо це сам предмет — малюємо як є
    if (center.a > 0.1)
        return center;

    float2 px = 1.0 / uTextureSize;

    float a1 = tex2D(TextureSampler, uv + float2(px.x, 0)).a;
    float a2 = tex2D(TextureSampler, uv - float2(px.x, 0)).a;
    float a3 = tex2D(TextureSampler, uv + float2(0, px.y)).a;
    float a4 = tex2D(TextureSampler, uv - float2(0, px.y)).a;

    // якщо поруч є текстура — малюємо outline
    if (a1 > 0.1 || a2 > 0.1 || a3 > 0.1 || a4 > 0.1)
    {
        float r = 0.5 + 0.5 * sin(Time);
        float g = 0.5 + 0.5 * sin(Time + 2.094);
        float b = 0.5 + 0.5 * sin(Time + 4.188);

        return float4(r, g, b, 1);
    }

    return float4(0, 0, 0, 0);
}

technique BasicEffect
{
    pass P0
    {
        PixelShader = compile ps_2_0 MainPS();
    }
}