using System.Reflection;
using Demos;

public interface IDemo{

}

public class DemoUtility{
    public Dictionary<string , BaseDemo?> demos = new Dictionary<string, BaseDemo?>();

    public DemoUtility(){
       // Get the assembly that contains the classes
        var assembly = Assembly.GetExecutingAssembly();

        // Get all types in the assembly
        var types = assembly.GetTypes();

        // Filter the types to only include classes that implement IDemo
        var demoTypes = types.Where(t => t.IsClass && !t.IsAbstract && typeof(IDemo).IsAssignableFrom(t));

        // Create an instance of each type and add it to the dictionary
        foreach (var type in demoTypes)
        {
            var instance = Activator.CreateInstance(type) as BaseDemo;
            demos.Add(type.Name, instance);
        }
    }
}