namespace CarDealer
{
    using Dtos.Export;
    using Dtos.Import;
    using Models;

    using AutoMapper;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDto, Supplier>();

            this.CreateMap<ImportPartDto, Part>();

            this.CreateMap<ImportCustomerDto, Customer>();

            this.CreateMap<ImportSaleDto, Sale>();

            this.CreateMap<Car, ExportCarDto>();

            this.CreateMap<Car, ExportCarFromMakeBmwDto>();
        }
    }
}
