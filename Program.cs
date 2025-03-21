// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata.Ecma335;
using Configs;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Demos;


var AIKey = "";
var EndPoint = "";
var model = "";

var kernelConfig = new SkConfig(endpoint : EndPoint, apiKey : AIKey, deploymentName: model);


var demoUtility = new DemoUtility();
var demo = demoUtility.demos[args[0]]??new Demo01();

await demo.InitializeKernel(kernelConfig);
await demo.RunAsync();




  



