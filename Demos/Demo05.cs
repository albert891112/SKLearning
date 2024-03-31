
using System.Runtime.CompilerServices;
using Configs;
using Demos;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

namespace Demos;

public class Demo05 : BaseDemo
{
    protected override Kernel CreateKernel(SkConfig config)
    {
        var kernelBuilder  = Kernel.CreateBuilder()
        .AddAzureOpenAIChatCompletion
        (
            deploymentName : config.deploymentName,
            endpoint : config.endpoint,
            apiKey : config.apiKey    
        );

        kernelBuilder.Plugins.AddFromType<TimePlugin>();


        var kernel = kernelBuilder.Build();

        return kernel;
    }


    public override Task<KernelPlugin[]> CreatePluginsAsync(Kernel kernel){
        
        System.Console.WriteLine("CreatePluginsAsync");
        KernelPlugin[] Plugins = new KernelPlugin[]{kernel.CreatePluginFromPromptDirectory("/Users/yangchengru/Documents/C#training/SKConsole/Prompts/")};
        
        System.Console.WriteLine($"Plugins Lenth = {Plugins.Length}");
        foreach(var i in Plugins){
            var k = 0;
            Console.WriteLine($"{k + 1}{i.Name}");
        }
        return Task.FromResult(Plugins);
    }

    protected override async Task<string?> HandlePrompt(Kernel kernel, string userPrompt){

        var args = new KernelArguments
        {
            {"input", userPrompt}
        };

        var Plugin = Plugins["Prompts"]["Time"];

        var response = await kernel.InvokeAsync<string>(Plugin , args);

        return response;

    }
    public override string? ScreemPrompt => "How can i help you?";
    
}
