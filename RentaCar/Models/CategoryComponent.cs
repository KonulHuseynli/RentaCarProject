namespace RentaCar.Models
{
    public class CategoryComponent
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string DailyPrice { get; set; }
        public string WeeklyPrice { get; set; }

        public int Date { get; set; }
        public string Seat { get; set; }
        public string FuelType { get; set; }
        public string Auto { get; set; }
        public string PhotoPath { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
