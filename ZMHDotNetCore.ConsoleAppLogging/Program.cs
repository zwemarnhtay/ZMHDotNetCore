using Serilog;

Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.Console()
			.WriteTo.File("logs/ZMHDotNetCore.ConsoleAppLogging.txt", rollingInterval: RollingInterval.Hour)
			.CreateLogger();

Log.Fatal("Hi Hi");
Log.Error("err..");

int a = 10, b = 0;
try
{
	Log.Debug("Dividing {A} by {B}", a, b);
	Console.WriteLine(a / b);
}
catch (Exception ex)
{
	Log.Error(ex, "Something went wrong");
}
finally
{
	await Log.CloseAndFlushAsync();
}
