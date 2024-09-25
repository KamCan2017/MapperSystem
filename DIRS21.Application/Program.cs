// See https://aka.ms/new-console-template for more information

using ModelMapper.Core.Converters;
using ModelMapper.Core.Handlers;
using ModelMapper.Core.Interfaces;
using ModelMapper.Core.Profiles;
using Newtonsoft.Json;
using NLog;

IModelProfiler modelProfiler = new ModelProfiler();
IConverterProvider converterProvider = new ConverterProvider();
ILogger logger = LogManager.GetCurrentClassLogger();

IMapHandler mapHandler = new MapHandler(modelProfiler, converterProvider, logger);

//1. Map the room model
//DIRS21.DataModel.Room room = new DIRS21.DataModel.Room() { Id = "1587-1457-p90", NumberOfBad = 1, NumberOfBed = 3, Category = "family", Details = "With animals themes..." };
//Console.WriteLine($"Source:\n{JsonConvert.SerializeObject(room)}");

//object targetRoomModel = mapHandler.Map(room, typeof(DIRS21.DataModel.Room).FullName!, typeof(ExternalClient.DataModel.Google.Room).FullName!);
//Console.WriteLine($"\nTarget:\n{JsonConvert.SerializeObject(targetRoomModel)}");

//2. Map the reservation model
ExternalClient.DataModel.Google.Reservation reservation = new() { Id = Guid.NewGuid().ToString(), CustomerId = "345-678", Start = DateTime.Now, End = DateTime.Now.AddDays(2) };
Console.WriteLine($"Source:\n{JsonConvert.SerializeObject(reservation)}");

object targetReservationModel = mapHandler.Map(reservation, typeof(ExternalClient.DataModel.Google.Reservation).FullName!, typeof(DIRS21.DataModel.Reservation).FullName!);
Console.WriteLine($"\nTarget:\n{JsonConvert.SerializeObject(targetReservationModel)}");

//3. Map a collection of reservation
//ExternalClient.DataModel.Google.Reservation reservation = new() { Id = Guid.NewGuid().ToString(), CustomerId = "345-678", Start = DateTime.Now, End = DateTime.Now.AddDays(2) };
//var sourceObject = new List<ExternalClient.DataModel.Google.Reservation> { reservation };
//Console.WriteLine($"Source:\n{JsonConvert.SerializeObject(sourceObject)}");

//var targetObjects = mapHandler.MapCollection(sourceObject, typeof(List<ExternalClient.DataModel.Google.Reservation>).FullName!, typeof(List<Reservation>).FullName!);

//Console.WriteLine($"\nTarget:\n{JsonConvert.SerializeObject(targetObjects)}");

Console.WriteLine("\n\nMapping is done. Press any key to close the console application.");
Console.ReadLine();
