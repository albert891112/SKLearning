using Microsoft.SemanticKernel;
using Configs;

namespace Demos;
public  abstract class BaseDemo : IDemo{

    private Kernel _kernel = new Kernel();

    private Dictionary<string , KernelPlugin> _plugins = new Dictionary<string, KernelPlugin>();
    public Dictionary<string , KernelPlugin> Plugins => _plugins;

    public async Task InitializeKernel(SkConfig config){
        _kernel = CreateKernel(config);
        _plugins = (await CreatePluginsAsync(_kernel)).ToDictionary(x=>x.Name);

    }

    public async Task RunAsync(){
        while(true){

            Console.WriteLine(ScreemPrompt);

            var userPrompt = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(userPrompt)){
                continue;
            }

            var response = await HandlePrompt(_kernel , userPrompt);

            if(string.IsNullOrWhiteSpace(response)){

                System.Console.WriteLine(ScreemPrompt1);
                continue;
            }
            else{
                System.Console.WriteLine(response);
            }
      }
    }

    public virtual string? ScreemPrompt => "How can I help you?";
    public virtual string? ScreemPrompt1 => "Sorry I did not get that, can you please rephrase?";

    protected abstract Kernel CreateKernel(SkConfig config);
    
    protected abstract Task<string?> HandlePrompt(Kernel kernel , string userPrompt);

    public virtual Task<KernelPlugin[]> CreatePluginsAsync(Kernel kernel){
        return  Task.FromResult(new KernelPlugin[0]);
    }


}