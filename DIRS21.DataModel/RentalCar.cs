using DIRS21.DataModel.Validators;

namespace DIRS21.DataModel
{
    public class RentalCar: BaseMirrorModel
    {
        public RentalCar() : base(new RentalCarValidator())
        {

        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string CarId { get; set; } = string.Empty;
    }
}