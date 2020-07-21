namespace CarDealer
{
    using Models;

    using AutoMapper;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDto, Supplier>();
        }
    }
}
