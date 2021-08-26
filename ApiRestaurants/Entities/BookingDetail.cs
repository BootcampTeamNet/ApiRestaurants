namespace Entities
{
    public class BookingDetail : BaseClass
    {
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
    }
}
