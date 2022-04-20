using MarsRover.Core.Enums;
using MarsRover.Core.Models;
using MarsRover.Domain;
using MarsRover.Domain.States;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MarsRover.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IMarsRoverService, MarsRoverService>();
            services.AddSingleton<IBaseRoverState, RoverNorthState>();
            services.AddSingleton<IBaseRoverState, RoverSouthState>();
            services.AddSingleton<IBaseRoverState, RoverWestState>();
            services.AddSingleton<IBaseRoverState, RoverEastState>();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the Plateau limits :");
                    var plateauLimits = Console.ReadLine();
                    var limits = plateauLimits.Split(" ");

                    if (limits.Length != 2)
                        throw new BusinessException("The Plateau coordinates must be 2 dimensions.");

                    Console.WriteLine("Locate Rover on the plateau");
                    var positionAndDirection = Console.ReadLine();
                    var pieces = positionAndDirection.Split(" ");

                    if (pieces.Length != 3)
                        throw new BusinessException("Rover starting coordinates and direction must be specified.");

                    using var serviceProvider = services.BuildServiceProvider(true);
                    var marsRoverService = serviceProvider.GetService<IMarsRoverService>();

                    var plateau = marsRoverService.SetInitialRoverPosition(Convert.ToInt32(pieces[0]), Convert.ToInt32(pieces[1]), Convert.ToInt32(limits[0]),
                                                                           Convert.ToInt32(limits[1]), Enum.Parse<Direction>(pieces[2], true));

                    Console.WriteLine("Enter Rover movoment commands (L-M-R) : ");
                    var commands = Console.ReadLine();

                    marsRoverService.MoveRover(plateau, commands.ToList());
                    var response = new ResponseModel<object>(true, new
                    {
                        X_Axis = plateau.Rover.RoverState.Coordinate.X_Axis,
                        Y_Axis = plateau.Rover.RoverState.Coordinate.Y_Axis,
                        Direction = plateau.Rover.RoverState.Direction.ToString()
                    });

                    Console.WriteLine(JsonConvert.SerializeObject(response, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (BusinessException bEx)
                {
                    var errorModel = new ErrorModel()
                    {
                        ErrorCode = bEx.ErrorCode,
                        ErrorMessage = bEx.Message
                    };

                    var responseModel = new ResponseModel<ErrorModel>(bEx.Message, false, errorModel);

                    Console.WriteLine(JsonConvert.SerializeObject(responseModel, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                catch (System.Exception ex)
                {
                    var errorModel = new ErrorModel()
                    {
                        ErrorCode = 1000,
                        ErrorMessage = ex.StackTrace
                    };

                    var responseModel = new ResponseModel<ErrorModel>(ex.Message, false, errorModel);

                    Console.WriteLine(JsonConvert.SerializeObject(responseModel, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                Console.WriteLine("Do you want to play again? [Y]/[N]?");
                if (Console.ReadLine() == "N")
                    break;
            }

            Console.ReadKey();
        }
    }
}
