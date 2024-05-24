﻿namespace RentaCar.Areas.Admin.ViewModels.AirPortTransfer
{
    public class AirPortTransferCreateViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }

        public string Price { get; set; }
        public DateTime? CreateDate { get; set; }

        public IFormFile MainPhoto { get; set; }
    }
}