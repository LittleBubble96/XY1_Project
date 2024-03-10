// See https://aka.ms/new-console-template for more information
ServeEntry serveEntry = ServeEntry.Instance;
serveEntry.Start("192.168.0.106", 20888);
Console.ReadKey();
