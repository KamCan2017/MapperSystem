using DIRS21.Core.Result;
using DIRS21.DataModel;
using ModelMapper.Core.Converters;
using ModelMapper.Core.Handlers;
using ModelMapper.Core.Interfaces;
using ModelMapper.Core.Profiles;
using NLog;
using System.Collections;

namespace DIRS21.IntegrationTests
{
    public class Tests
    {

        private IModelProfiler _modelProfiler;
        private IConverterProvider _converterProvider;


        private MapHandler _mapHandler;

        [SetUp]
        public void Setup()
        {
            _modelProfiler = new ModelProfiler();
            _converterProvider = new ConverterProvider();
            _mapHandler = new MapHandler(_modelProfiler, _converterProvider, LogManager.GetCurrentClassLogger());
        }

        [Test]
        public void Map_GoogleRoomToCompanyRoomModel_MappingIsSuccessful()
        {
            DataModel.Room room = new() { Id = "1587-1457-p90", NumberOfBad = 1, NumberOfBed = 3, Category = "family", Details = "With animals themes..." };

            object targetRoomModel = _mapHandler.Map(room, typeof(DataModel.Room).FullName!, typeof(ExternalClient.DataModel.Google.Room).FullName!);
            var targetModel = targetRoomModel as ExternalClient.DataModel.Google.Room;

            Assert.Multiple(() =>
            {
                Assert.That(targetRoomModel, Is.Not.Null);
                Assert.That(targetRoomModel, Is.TypeOf<ExternalClient.DataModel.Google.Room>());
                Assert.That(targetModel!.IsValid, Is.True);
            });
        }

        [Test]
        public void Map_CompanyRoomModelToGoogleRoom_MappingIsSuccessful()
        {
            var room = new ExternalClient.DataModel.Google.Room() { Id = "1587-1457-p90", NumberOfBad = 1, NumberOfBed = 3, Category = "family", Details = "With animals themes..." };

            object targetRoomModel = _mapHandler.Map(room, typeof(ExternalClient.DataModel.Google.Room).FullName!, typeof(DataModel.Room).FullName!);
            var targetModel = targetRoomModel as DataModel.Room;

            Assert.Multiple(() =>
            {
                Assert.That(targetRoomModel, Is.Not.Null);
                Assert.That(targetRoomModel, Is.TypeOf<DataModel.Room>());
                Assert.That(targetModel!.IsValid, Is.True);
            });
        }

        [Test]
        public void Map_GoogleReservationToCompanyReservationModel_MappingIsSuccessful()
        {
            ExternalClient.DataModel.Google.Reservation reservation = new() { Id = Guid.NewGuid().ToString(), CustomerId = "345-678", Start = DateTime.Now, End = DateTime.Now.AddDays(2),
            RentalCar = new ExternalClient.DataModel.Google.RentalCar() { Id = "2548", Name = "byd", Description = "Combi car" }
            };

            object targetReservationModel = _mapHandler.Map(reservation, typeof(ExternalClient.DataModel.Google.Reservation).FullName!, typeof(DIRS21.DataModel.Reservation).FullName!);
            var targetModel = targetReservationModel as DataModel.Reservation;

            Assert.Multiple(() =>
            {
                Assert.That(targetReservationModel, Is.Not.Null);
                Assert.That(targetReservationModel, Is.TypeOf<DataModel.Reservation>());
                Assert.That(targetModel!.IsValid, Is.True);
            });
        }

        [Test]
        public void Map_CompanyReservationModelToGoogleReservation_MappingIsSuccessful()
        {
            var reservation = new DataModel.Reservation() { ReservationId = Guid.NewGuid(), CustomerId = "345-678", Start = DateTime.Now, End = DateTime.Now.AddDays(2) };

            object targetReservationModel = _mapHandler.Map(reservation, typeof(DataModel.Reservation).FullName!, typeof(ExternalClient.DataModel.Google.Reservation).FullName!);
            var targetModel = targetReservationModel as ExternalClient.DataModel.Google.Reservation;

            Assert.Multiple(() =>
            {
                Assert.That(targetReservationModel, Is.Not.Null);
                Assert.That(targetReservationModel, Is.TypeOf<ExternalClient.DataModel.Google.Reservation>());
                Assert.That(targetModel!.IsValid, Is.True);
            });
        }


        [Test]
        public void MapWithResult_CompanyReservationModelToGoogleReservation_MappingIsSuccessful()
        {
            var reservation = new DataModel.Reservation() { ReservationId = Guid.NewGuid(), CustomerId = "345-678", Start = DateTime.Now, End = DateTime.Now.AddDays(2) };

            OperationResult result = _mapHandler.MapWithResult(reservation, typeof(DataModel.Reservation).FullName!, typeof(ExternalClient.DataModel.Google.Reservation).FullName!);
            var targetModel = result.Output as ExternalClient.DataModel.Google.Reservation;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ResultType, Is.EqualTo(ResultType.Success));
                Assert.That(result.Output, Is.TypeOf<ExternalClient.DataModel.Google.Reservation>());
                Assert.That(targetModel!.IsValid, Is.True);
            });
        }

        [Test]
        public void MapWithResult_CompanyReservationModelToGoogleReservation_MappingFailed()
        {
            var reservation = new DataModel.Reservation() { ReservationId = Guid.Empty, CustomerId = "345-678", Start = DateTime.Now, End = DateTime.Now.AddDays(2) };

            OperationResult result = _mapHandler.MapWithResult(reservation, typeof(DataModel.Reservation).FullName!, typeof(ExternalClient.DataModel.Google.Reservation).FullName!);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ResultType, Is.EqualTo(ResultType.Failed));
                Assert.That(result.Output, Is.Null);
                Assert.That(result.Resume, Is.Not.Empty);
            });
        }

        [Test]
        public void Map_ListOfCompanyReservationModelToListofGoogleReservation_MappingIsSuccessful()
        {
            var reservation1 = new Reservation() { ReservationId = Guid.NewGuid(), CustomerId = "345-678", Start = DateTime.Now, End = DateTime.Now.AddDays(2) };
            var reservation2 = new Reservation() { ReservationId = Guid.NewGuid(), CustomerId = "375-678", Start = DateTime.Now, End = DateTime.Now.AddDays(12) };
            var data = new List<Reservation>() { reservation1, reservation2 };

            object targetReservationModel = _mapHandler.MapCollection(data, typeof(List<Reservation>).FullName!, typeof(List<ExternalClient.DataModel.Google.Reservation>).FullName!);
            IList? list = targetReservationModel as IList;
            Assert.Multiple(() =>
            {
                Assert.That(targetReservationModel, Is.Not.Null);
                CollectionAssert.AllItemsAreNotNull(list);
                CollectionAssert.AllItemsAreInstancesOfType(list, typeof(ExternalClient.DataModel.Google.Reservation));
            });
        }
    }
}