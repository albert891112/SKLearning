using Configs;
using Demos;
using Microsoft.SemanticKernel;

namespace Demos;

public class Demo03 : BaseDemo
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

    protected override async Task<string?> HandlePrompt(Kernel kernel, string userPrompt){

        
        var newprompt   = GetPormpt(userPrompt);
        return await kernel.InvokePromptAsync<string>(newprompt);
    }
     

    public override string? ScreemPrompt => "what do you want to order?";

    private string GetPormpt(string userPrompt)
      => $"""
         what products is ordered in this request?  {userPrompt} ,
         product can be coffee , burger , pizza , water. 
         If orderod product is not in the list, response  "NotAvailalbe".
      """;
    
}
