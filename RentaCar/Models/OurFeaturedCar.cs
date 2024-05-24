namespace RentaCar.Models
{
    public class OurFeaturedCar
    {
        public int Id { get; set; } 
        public string CarName { get; set; }
        public string CarType { get; set; }
        public string PriceDaily { get; set; }
        public string PriceWeekly { get; set; }

        public int Date { get; set; }
        public string Seats { get; set; }
        public string Fuel { get; set; }
        public string Type { get; set; }
        public string FilePath { get; set; }

    }
}
