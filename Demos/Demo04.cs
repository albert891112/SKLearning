using Configs;
using Demos;
using Microsoft.SemanticKernel;


namespace Demos;

public class Demo04 : BaseDemo
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


    public override Task<KernelPlugin[]> CreatePluginsAsync(Kernel kernel){
        KernelPlugin[] Plugins = new KernelPlugin[]{kernel.CreatePluginFromPromptDirectory("/Users/yangchengru/Documents/C#training/SKConsole/Prompts")};
        
        return Task.FromResult(Plugins);
    }

    protected override async Task<string?> HandlePrompt(Kernel kernel, string userPrompt){
        
        var arg =  new KernelArguments{
            ["order"] = userPrompt
        };

        var plugin = Plugins["Prompts"]["Order"];

        var response = await kernel.InvokeAsync<string>(plugin , arg);

        return response;
    }
     

    public override string? ScreemPrompt => "what do you want to order?";
    
}
