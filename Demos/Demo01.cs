using Configs;
using Demos;
using Microsoft.SemanticKernel;

public class Demo01 : BaseDemo
{
    protected override Kernel CreateKernel(SkConfig config)
    {
        var kernel  = Kernel.CreateBuilder()
        .AddAzureOpenAIChatCompletion
        (
            deploymentName : config.deploymentName,
            endpoint : config.endpoint,
            apiKey : config.apiKey    
        ).Build();

        return kernel;
    }

    protected override async Task<string?> HandlePrompt(Kernel kernel, string userPrompt)
     => await kernel.InvokePromptAsync<string>(userPrompt);
}