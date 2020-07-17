namespace CarDealer
{
    using System.Collections.Generic;

    using Models;

    public class ImportCarDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public IEnumerable<int> PartsId { get; set; }
    }
}