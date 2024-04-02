
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Configs;
using Demos;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins;

namespace Demos;

public class Demo07 : BaseDemo
{
    private readonly ChatHistory history = new ChatHistory();

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
        
        KernelPlugin[] Plugins = new KernelPlugin[]{kernel.CreatePluginFromPromptDirectory("/Users/yangchengru/Documents/C#training/SKConsole/Prompts/")};
        
        return Task.FromResult(Plugins);
    }

    protected override async Task<string?> HandlePrompt(Kernel kernel, string userPrompt){

        var setting = new OpenAIPromptExecutionSettings{
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        history.AddUserMessage(userPrompt);

        var chatCompletionServer = kernel.GetRequiredService<IChatCompletionService>();

        var responseList = await chatCompletionServer.GetChatMessageContentsAsync(userPrompt, executionSettings : setting ,kernel :  kernel);

        var response = responseList.FirstOrDefault(new ChatMessageContent());

        history.AddAssistantMessage(response.Content??"");

        return response.Content;

    }
    public override string? ScreemPrompt => "How can i help you?";
    
}
