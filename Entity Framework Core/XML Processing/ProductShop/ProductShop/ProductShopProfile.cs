namespace ProductShop
{
    using Dtos.Export;
    using Dtos.Import;
    using Models;

    using AutoMapper;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<ImportUserDto, User>();
            this.CreateMap<ImportProductDto, Product>();
            this.CreateMap<ImportCategorieDto, Category>();
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
            this.CreateMap<Product, ExportProductInRangeDto>()
                .ForMember(x => x.Buyer, y => y.MapFrom(x => x.Buyer.FirstName + " " + x.Buyer.LastName));

            this.CreateMap<Product, ExportProductDto>();
            this.CreateMap<User, ExportSoldProductDto>()
                .ForMember(x => x.Products, y => y.MapFrom(x => x.ProductsSold));
        }
    }
}
