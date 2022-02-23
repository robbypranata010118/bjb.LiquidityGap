using AutoMapper;
using Bjb.LiquidityGap.Base.Dtos.AuditTrails;
using Bjb.LiquidityGap.Base.Dtos.Categories;
using Bjb.LiquidityGap.Base.Dtos.Characteristics;
using Bjb.LiquidityGap.Base.Dtos.Currency;
using Bjb.LiquidityGap.Base.Dtos.DataSources;
using Bjb.LiquidityGap.Base.Dtos.SheetItemCharacteriastic;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
using Bjb.LiquidityGap.Base.Dtos.SubCategories;
using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
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
            //CreateMap<AddSheetItemRequest, SheetItem>(); //commands
            //CreateMap<UpdateSheetItemRequest, SheetItem>(); //commands
            CreateMap<SheetItem, SheetItemResponse>() //Query
            .ForMember(dto => dto.SubCategory, opt => opt.MapFrom(x => x.SubCategory))
            .ForMember(dto => dto.DataSource, opt => opt.MapFrom(x => x.DataSource))
            .ForMember(dto => dto.SheetChildItems, opt => opt.MapFrom(x => x.SheetChildItems))
            .ForMember(dto => dto.SheetItemCharacteristic, opt => opt.MapFrom(x => x.SheetItemCharacteristics));


            CreateMap<SheetItemCharacteristic, SheetItemCharacteristicResponse>()
            .ForMember(dto => dto.Characteristic, opt => opt.MapFrom(x => x.Characteristic));
            #endregion

            #region Characteristic
            CreateMap<AddCharacteristicRequest, Characteristic>() //commands
                .ForMember(x => x.CharacteristicFormulas, m => m.MapFrom(x => x.Formula));
            CreateMap<UpdateCharacteristicRequest, Characteristic>() //commands
                 .ForMember(x => x.CharacteristicFormulas, m => m.MapFrom(x => x.Formula));
            CreateMap<Characteristic, CharacteristicResponse>() //Query
                 .ForMember(x => x.Formulas, m => m.MapFrom(x => x.CharacteristicFormulas));

            #endregion

            #region Characteristic Formula
            CreateMap<AddCharacteristicFormula, CharacteristicFormula>(); //commands
            CreateMap<CharacteristicFormula, CharacteristicFormulaResponse>(); //commands
            #endregion

            #region CharacteristicTimebucket
            CreateMap<CharacteristicTimebucket, ChacteristicTimeBucketResponse>() //Query
                .ForMember(x => x.Characteristic, m => m.MapFrom(x => x.Characteristic));
            #endregion

            #region Timebucket
            CreateMap<Timebucket, TimeBucketResponse>() //Query
            .ForMember(dto => dto.CharacteristicTimebuckets, opt => opt.MapFrom(x => x.CharacteristicTimebuckets));
            #endregion

            #region AuditTrail
            CreateMap<AuditTrail, AuditTrailResponse>(); //Query
            #endregion

            #region Currency
            CreateMap<Currency, CurrencyResponse>(); //Query
            #endregion
        }
    }
}
