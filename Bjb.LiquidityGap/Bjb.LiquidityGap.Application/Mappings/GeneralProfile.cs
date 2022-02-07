﻿using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.Categories;
using Bjb.LiquidityGap.Base.Dtos.DataSources;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
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

            #region Data Source
            CreateMap<AddDataSourceRequest, DataSource>(); //commands
            CreateMap<UpdateDataSourceRequest, DataSource>(); //commands
            CreateMap<DataSource, DataSourceResponse>(); //Query
            #endregion

            #region SheetItem
            CreateMap<AddSheetItemRequest, SheetItem>(); //commands
            CreateMap<UpdateSheetItemRequest, SheetItem>(); //commands
            CreateMap<SheetItem, SheetItemResponse>() //Query
            .ForMember(dto => dto.SubCategory, opt => opt.MapFrom(x => x.SubCategory))
            .ForMember(dto => dto.DataSource, opt => opt.MapFrom(x => x.DataSource))
            .ForMember(dto => dto.SheetChildItems, opt => opt.MapFrom(x => x.SheetChildItems));


            #endregion
        }
    }
}
