using System.Drawing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace TestNLogLayoutsForELK;

public class Worker
{
    private readonly IConfiguration configuration;
    private readonly ILogger _logger;

    public Worker(IConfiguration configuration, ILogger<Worker> logger)
    {
        this.configuration = configuration;
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.LogInformation("Hello, World!");

        var car = new Car
        {
            Color = "Red",
            Length = 5M
        };

        _logger.LogInformation("{car}", car);

        if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug("DEBUG IS ENABLED; DEBUG LOG FOR: {carDebug}", car);

        try
        {
            throw new ArgumentException("testing the custom exception!!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error block 1");
        }

        try
        {
            var a = 0;
            var b = 9 / a;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error block 2");
        }


        var t = new Timer();
        t.Interval = int.Parse(configuration["appSettings:timerIntervalSeconds"]) * 1000;
        var i = 1;
        t.Elapsed += (sender, args) =>
        {
            for (var j = 0; j < int.Parse(configuration["appSettings:batchSize"]); j++) // x per y seconds
                try
                {
                    var randomGen = new Random();
                    var names = (KnownColor[]) Enum.GetValues(typeof(KnownColor));
                    var randomColorName = names[randomGen.Next(names.Length)];
                    var randomColor = Color.FromKnownColor(randomColorName);

                    var newCar = new Car
                    {
                        Color = randomColor.Name,
                        Length = new Random().Next(0, 100),
                        Id = i
                    };

                    _logger.LogInformation("New car created: {newCar}", newCar);
                    i++;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "");
                }
        };
        t.Enabled = true;

        Console.ReadKey();
    }
}