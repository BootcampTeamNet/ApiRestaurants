namespace Entities
{
    public class BookingStatus : BaseClass
    {
        public BookingStatus()
        {
            IsActive = true;
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
