sampler TextureSampler : register(s0);

struct VertexShaderOutput
{
    float4 color : COLOR0;
    float2 texCoord : TEXCOORD0;
};

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{    
    float4 col = tex2D(TextureSampler,input.texCoord);
    
    float orgA = col.a;
    
    col = (float4)1.0f - col;
    
    col.a = orgA;
    
    return col;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();        
    }
}
