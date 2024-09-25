using DIRS21.DataModel;

namespace DIRS21.Unittests.Dirs21Models
{
    public class RoomTests
    {

        private static IEnumerable<TestCaseData> RoomUseCaseDatas => [
                    new TestCaseData(new Room()),
                    new TestCaseData(new Room{Id = "", Category = "family", NumberOfBad = 1, NumberOfBed = 3}),
                    new TestCaseData(new Room{Id = " ", Category = "family", NumberOfBad = 1, NumberOfBed = 3}),
                    new TestCaseData(new Room{Id = Guid.NewGuid().ToString(), Category = "", NumberOfBad = 1, NumberOfBed = 3 }),
                    new TestCaseData(new Room{Id = Guid.NewGuid().ToString(), Category = " ", NumberOfBad = 1, NumberOfBed = 3 }),
                    new TestCaseData(new Room{Id = Guid.NewGuid().ToString(), Category = "family", NumberOfBad = -1, NumberOfBed = 3 }),
                    new TestCaseData(new Room{Id = Guid.NewGuid().ToString(), Category = "family", NumberOfBad = 0, NumberOfBed = 0 }),
                ];

        [Test]
        public void CreateRoom_ValidateRoom_RoomIsValid()
        {
            Room room = new Room() { Id = Guid.NewGuid().ToString(), Category = "family", NumberOfBad = 1, NumberOfBed = 3 };

            Assert.That(room.IsValid, Is.True);
            Assert.That(room.Errors, Is.Empty);
        }

        [Test, TestCaseSource(nameof(RoomUseCaseDatas))]
        public void CreateRoom_ValidateRoom_RoomIsInValid(Room room)
        {
            Assert.That(room.IsValid, Is.False);
            Assert.That(room.Errors, Is.Not.Empty);
        }
    }
}