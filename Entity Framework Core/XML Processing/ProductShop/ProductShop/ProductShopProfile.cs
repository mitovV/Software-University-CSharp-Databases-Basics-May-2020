namespace ProductShop
{
    using System.Linq;

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

            this.CreateMap<Category, ExportCategorieDto>()
                .ForMember(x => x.Count, y => y.MapFrom(x => x.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice, y => y.MapFrom(x => x.CategoryProducts.Average(cp => cp.Product.Price)))
                .ForMember(x => x.TotalRevenue, y => y.MapFrom(x => x.CategoryProducts.Sum(cp => cp.Product.Price)));

            this.CreateMap<User, ExportSoldProductsCountDto>()
                .ForMember(x => x.Count, y => y.MapFrom(x => x.ProductsSold.Count))
                .ForMember(x => x.Products, y => y.MapFrom(x => x.ProductsSold));


            this.CreateMap<User, ExportUserWithProductDto>()
                .ForMember(x => x.SoldProducts, y => y.MapFrom(x => x));
        }
    }
}
