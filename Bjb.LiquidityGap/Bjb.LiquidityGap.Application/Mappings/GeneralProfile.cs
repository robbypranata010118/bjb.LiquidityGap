using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.Categories;
using Bjb.LiquidityGap.Base.Dtos.SubCategories;
using Bjb.LiquidityGap.Domain.Entities;

namespace Bjb.LiquidityGap.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Category
            CreateMap<AddCategoryRequest, Category>(); //commands
            CreateMap<UpdateCategoryRequest, Category>(); //commands
            CreateMap<Category, CategoryResponse>(); //Query
            #endregion

            #region SubCategory
            CreateMap<AddSubCategoryRequest, SubCategory>(); //commands
            CreateMap<UpdateSubCategoryRequest, SubCategory>(); //commands
            CreateMap<SubCategory, SubCategoryResponse>() //Query
            .ForMember(dto => dto.Category, opt => opt.MapFrom(x => x.Category));
            #endregion
        }
    }
}
