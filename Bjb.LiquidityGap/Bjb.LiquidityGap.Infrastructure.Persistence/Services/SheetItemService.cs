using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Application;
using System.Collections.Generic;
using Bjb.LiquidityGap.Base.Parameters;
using Bjb.LiquidityGap.Infrastructure.Persistence.Extensions;
using Bjb.LiquidityGap.Base.Dtos.DataSources;
using Bjb.LiquidityGap.Base.Dtos.SheetItemCharacteriastic;
using Bjb.LiquidityGap.Base.Dtos.Characteristics;
using Bjb.LiquidityGap.Base.Dtos.SubCategories;
using Bjb.LiquidityGap.Base.Dtos.Categories;
using Bjb.LiquidityGap.Base.Wrappers;
using System.Linq.Dynamic.Core;
using Bjb.LiquidityGap.Base.Extensions;

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Services
{
    public class SheetItemService : ISheetItem
    {
        private readonly AppDBContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogService _logService;
        public SheetItemService(AppDBContext appDbContext, ICurrentUserService currentUserService, ILogService logService)
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _logService = logService;
        }

        public async Task<int> CreateSheetItem(AddSheetItemRequest request)
        {
            try
            {
                await _appDbContext.Database.BeginTransactionAsync();
                var checkCharacteriticId = await _appDbContext.Characteristics.Where(x => request.SheetItemCharacteristics.Contains(x.Id)).Select(x => x.Id).ToListAsync();
                var idNotExist = request.SheetItemCharacteristics.Except(checkCharacteriticId).ToList();
                if (checkCharacteriticId.Count == 0)
                    throw new Exception(String.Format(Constant.MessageDataNotFound, Constant.Characteristic, $"{String.Join(" dan ", idNotExist.Select(x => x.ToString()))}"));
                var checkTimebucketId = await _appDbContext.Timebuckets.Where(x => request.SheetItemTimebuckets.Contains(x.Id)).Select(x => x.Id).ToListAsync();
                var timeBucketIdNotExist = request.SheetItemTimebuckets.Except(checkCharacteriticId).ToList();
                if (checkTimebucketId.Count == 0)
                    throw new Exception(String.Format(Constant.MessageDataNotFound, Constant.TimeBucket, $"{String.Join(" dan ", timeBucketIdNotExist.Select(x => x.ToString()))}"));
                SheetItem sheetItem = new SheetItem
                {
                    SubCategoryId = request.SubCategoryId,
                    Code = request.Code,
                    DataSourceId = request.DataSourceId,
                    IsManualInput = request.IsManualInput,
                    MarkToCalculate = request.MarkToCalculate,
                    Name = request.Name,
                    Statement = request.Statement,
                    SheetItemParentId = request.SheetItemParentId,
                    UserIn = _currentUserService.UserId,
                    DateIn = DateTime.Now,
                    IsActive = true,
                    SheetItemCharacteristics = checkCharacteriticId.Select(x => new SheetItemCharacteristic
                    {
                        UserIn = _currentUserService.UserId,
                        DateIn = DateTime.Now,
                        IsActive = true,
                        CharacteristicId = x
                    }).ToList(),
                    SheetItemTimebuckets = checkTimebucketId.Select(x => new SheetItemTimebucket
                    {
                        UserIn = _currentUserService.UserId,
                        DateIn = DateTime.Now,
                        IsActive = true,
                        TimebucketId = x
                    }).ToList()
                };
                await _appDbContext.SheetItems.AddAsync(sheetItem);
                await _appDbContext.SaveChangesAsync();
                await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                {
                    Id = Guid.NewGuid(),
                    Action = Constant.ACTION_UPDATE,
                    ApplicationName = Constant.NAMA_APLIKASI,
                    Detail = "",
                    Feature = "Sheet Item Characteristic",
                    LogDate = DateTime.Now,
                    Message = "Success",
                    Module = "Master Data",
                    ReferenceId = sheetItem.Id.ToString(),
                    RoleId = _currentUserService.IdFungsi,
                    RoleName = _currentUserService.IdFungsi,
                    UserId = _currentUserService.UserId,
                    UserName = _currentUserService.UserName,
                });
                await _appDbContext.Database.CommitTransactionAsync();
                return sheetItem.Id;
            }
            catch (Exception ex)
            {

                await _appDbContext.Database.RollbackTransactionAsync();
                throw new ApiException(ex.Message.ToString());
            }
        }

        public async Task<int> EditSheetItem(UpdateSheetItemRequest sheetItem)
        {
            try
            {
                await _appDbContext.Database.BeginTransactionAsync();
                var sheetItemCharacteristics = await _appDbContext.SheetItemCharacteristics.Where(x => x.SheetItemId == sheetItem.Id).ToListAsync();
                //should be 3 in here
                var characteristicInRequest = sheetItem.SheetItemCharacteristics;

                var sheetItemTimebuckets = await _appDbContext.SheetItemTimebuckets.Where(x => x.SheetItemId == sheetItem.Id).ToListAsync();
                //should be 3 in here
                var timebucketInRequest = sheetItem.SheetItemTimebuckets;
                #region Delete
                var dataWillBeDeleted = sheetItemCharacteristics.Select(x => x.CharacteristicId).Except(characteristicInRequest).ToList();
                foreach (var item in sheetItemCharacteristics.Where(x => dataWillBeDeleted.Contains(x.CharacteristicId)))
                {
                    item.IsActive = false;
                    item.UserUp = _currentUserService.UserId;
                    item.DateUp = DateTime.Now;
                    _appDbContext.SheetItemCharacteristics.Update(item);
                    await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                    {
                        Id = Guid.NewGuid(),
                        Action = Constant.ACTION_UPDATE,
                        ApplicationName = Constant.NAMA_APLIKASI,
                        Detail = "",
                        Feature = "Sheet Item Characteristic",
                        LogDate = DateTime.Now,
                        Message = "Success",
                        Module = "Master Data",
                        ReferenceId = item.Id.ToString(),
                        RoleId = _currentUserService.IdFungsi,
                        RoleName = _currentUserService.IdFungsi,
                        UserId = _currentUserService.UserId,
                        UserName = _currentUserService.UserName,
                    });
                }

                var dataWillBeDeletedTimebucket = sheetItemTimebuckets.Select(x => x.TimebucketId).Except(timebucketInRequest).ToList();
                foreach (var item in sheetItemTimebuckets.Where(x => dataWillBeDeletedTimebucket.Contains(x.TimebucketId)))
                {
                    item.IsActive = false;
                    item.UserUp = _currentUserService.UserId;
                    item.DateUp = DateTime.Now;
                    _appDbContext.SheetItemTimebuckets.Update(item);
                    await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                    {
                        Id = Guid.NewGuid(),
                        Action = Constant.ACTION_UPDATE,
                        ApplicationName = Constant.NAMA_APLIKASI,
                        Detail = "",
                        Feature = "Sheet Item Timebucket",
                        LogDate = DateTime.Now,
                        Message = "Success",
                        Module = "Master Data",
                        ReferenceId = item.Id.ToString(),
                        RoleId = _currentUserService.IdFungsi,
                        RoleName = _currentUserService.IdFungsi,
                        UserId = _currentUserService.UserId,
                        UserName = _currentUserService.UserName,
                    });
                }
                #endregion
                #region Added
                var dataWillBeAdded = characteristicInRequest.Except(sheetItemCharacteristics.Select(x => x.CharacteristicId).ToList()).ToList();
                foreach (var item in dataWillBeAdded)
                {
                    var checkDataWillBeAdded = await _appDbContext.Characteristics.Where(x => x.Id == item && x.IsActive).FirstOrDefaultAsync();
                    if (checkDataWillBeAdded == null)
                        throw new Exception(String.Format(Constant.MessageDataNotFound, Constant.Characteristic, item));
                    SheetItemCharacteristic sheetItemCharacteristic = new SheetItemCharacteristic
                    {
                        CharacteristicId = item,
                        SheetItemId = sheetItem.Id,
                        UserIn = _currentUserService.UserId,
                        DateIn = DateTime.Now,
                        IsActive = true
                    };
                    _appDbContext.SheetItemCharacteristics.Add(sheetItemCharacteristic);
                    await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                    {
                        Id = Guid.NewGuid(),
                        Action = Constant.ACTION_INSERT,
                        ApplicationName = Constant.NAMA_APLIKASI,
                        Detail = "",
                        Feature = "Sheet Item Characteristic",
                        LogDate = DateTime.Now,
                        Message = "Success",
                        Module = "Master Data",
                        ReferenceId = sheetItemCharacteristic.Id.ToString(),
                        RoleId = _currentUserService.IdFungsi,
                        RoleName = _currentUserService.IdFungsi,
                        UserId = _currentUserService.UserId,
                        UserName = _currentUserService.UserName,
                    });
                }

                var dataWillBeAddedTimeBucket = timebucketInRequest.Except(sheetItemTimebuckets.Select(x => x.TimebucketId).ToList()).ToList();
                foreach (var item in dataWillBeAddedTimeBucket)
                {
                    var checkDataWillBeAdded = await _appDbContext.Timebuckets.Where(x => x.Id == item && x.IsActive).FirstOrDefaultAsync();
                    if (checkDataWillBeAdded == null)
                        throw new Exception(String.Format(Constant.MessageDataNotFound, Constant.TimeBucket, item));
                    SheetItemTimebucket sheetItemTimebucket = new SheetItemTimebucket
                    {
                        TimebucketId = item,
                        SheetItemId = sheetItem.Id,
                        UserIn = _currentUserService.UserId,
                        DateIn = DateTime.Now,
                        IsActive = true
                    };
                    _appDbContext.SheetItemTimebuckets.Add(sheetItemTimebucket);
                    await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                    {
                        Id = Guid.NewGuid(),
                        Action = Constant.ACTION_INSERT,
                        ApplicationName = Constant.NAMA_APLIKASI,
                        Detail = "",
                        Feature = "Sheet Item Timebucket",
                        LogDate = DateTime.Now,
                        Message = "Success",
                        Module = "Master Data",
                        ReferenceId = sheetItemTimebucket.Id.ToString(),
                        RoleId = _currentUserService.IdFungsi,
                        RoleName = _currentUserService.IdFungsi,
                        UserId = _currentUserService.UserId,
                        UserName = _currentUserService.UserName,
                    });
                }
                #endregion

                var dataSheetItem = await _appDbContext.SheetItems.Where(x => x.Id == sheetItem.Id).FirstOrDefaultAsync();
                dataSheetItem.IsManualInput = sheetItem.IsManualInput;
                dataSheetItem.MarkToCalculate = sheetItem.MarkToCalculate;
                dataSheetItem.SheetItemParentId = sheetItem.SheetItemParentId;
                dataSheetItem.SubCategoryId = sheetItem.SubCategoryId;
                dataSheetItem.DataSourceId = sheetItem.DataSourceId;
                dataSheetItem.Code = sheetItem.Code;
                dataSheetItem.Name = sheetItem.Name;
                dataSheetItem.Statement = sheetItem.Statement;
                dataSheetItem.UserUp = _currentUserService.UserId;
                dataSheetItem.DateUp = DateTime.Now;
                _appDbContext.SheetItems.Update(dataSheetItem);
                await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                {
                    Id = Guid.NewGuid(),
                    Action = Constant.ACTION_INSERT,
                    ApplicationName = Constant.NAMA_APLIKASI,
                    Detail = "",
                    Feature = "Sheet Item",
                    LogDate = DateTime.Now,
                    Message = "Success",
                    Module = "Master",
                    ReferenceId = dataSheetItem.Id.ToString(),
                    RoleId = _currentUserService.IdFungsi,
                    RoleName = _currentUserService.IdFungsi,
                    UserId = _currentUserService.UserId,
                    UserName = _currentUserService.UserName,
                });
                await _appDbContext.SaveChangesAsync();
                await _appDbContext.Database.CommitTransactionAsync();
                return sheetItem.Id;
            }
            catch (Exception ex)
            {

                await _appDbContext.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<SheetItemVm> GetSheetItem(RequestParameter request)
        {
            var predicate = PredicateBuilder.Create<SheetItem>(x => 1 == 1);
            if (request.Filters.Count > 0)
            {
                foreach (var f in request.Filters)
                {
                    switch (f.Field)
                    {
                        case "sheetItemCharacteristics.characteristic.name":
                            predicate = predicate.And(x => x.SheetItemCharacteristics.Any(x => x.Characteristic.Name.Contains(f.Value)));
                            break;
                        case "code":
                            predicate = predicate.And(x => x.Code == f.Value);
                            break;
                        case "name":
                            predicate = predicate.And(x => x.Name == f.Value);
                            break;
                        case "markToCalculated":
                            predicate = predicate = predicate.And(x => x.MarkToCalculate == Convert.ToBoolean(f.Value));
                            break;
                        case "isManualInput":
                            predicate = predicate.And(x => x.IsManualInput == Convert.ToBoolean(f.Value));
                            break;
                        default:
                            predicate = predicate.And(x => 1 == 1);
                            break;
                    }
                }
            }

            var datas = _appDbContext.SheetItems
                .Include(x => x.SheetChildItems)
                .Include(x => x.SheetItemCharacteristics)
                    .ThenInclude(x => x.Characteristic)
                        .ThenInclude(x => x.CharacteristicFormulas)
                .Include(x => x.SubCategory)
                    .ThenInclude(x => x.Category)
                .Include(x => x.DataSource)
                .Include(x => x.SheetItemParent)
                .Where(predicate)
                .Select(x => new SheetItemResponse
                {
                    Id = x.Id,
                    Code = x.Code,
                    IsActive = x.IsActive,
                    IsManualInput = x.IsManualInput,
                    MarkToCalculate = x.MarkToCalculate,
                    DateIn = x.DateIn,
                    UserIn = x.UserIn,
                    DateUp = x.DateUp,
                    UserUp = x.UserUp,
                    Name = x.Name,
                    Statement = x.Statement,
                    SheetItemCharacteristics = x.SheetItemCharacteristics.Select(a => new SheetItemCharacteristicResponse
                    {
                        Characteristic = new CharacteristicResponse
                        {
                            Id = a.Characteristic.Id,
                            Code = a.Characteristic.Code,
                            CalcDay = a.Characteristic.CalcDay,
                            DateIn = a.Characteristic.DateIn,
                            DateUp = a.Characteristic.DateUp,
                            Description = a.Characteristic.Description,
                            IsActive = a.Characteristic.IsActive,
                            Name = a.Characteristic.Name,
                            UserIn = a.UserIn,
                            UserUp = a.UserUp,
                            Formulas = a.Characteristic.CharacteristicFormulas.Select(b => new CharacteristicFormulaResponse
                            {
                                Id = b.Id,
                                Formula = b.Formula,
                                Name = b.Name,
                                Sequence = b.Sequence
                            }).OrderBy(b => b.Sequence).ToList()
                        }
                    }).ToList(),
                    Characteristics = String.Join(",", x.SheetItemCharacteristics.Select(x => x.Characteristic.Name)),
                    DataSource = x.DataSource == null ? null : new DataSourceResponse
                    {
                        Id = x.DataSource.Id,
                        Name = x.DataSource.Name,
                        ConnString = x.DataSource.ConnString,
                        UseEtl = x.DataSource.UseETL,
                        IsActive = x.DataSource.IsActive,
                        UserIn = x.DataSource.UserIn,
                        DateIn = x.DataSource.DateIn,
                        UserUp = x.DataSource.UserUp,
                        DateUp = x.DataSource.DateUp
                    },
                    SheetChildItems = x.SheetChildItems.Select(c => new SheetItemSimpleResponse
                    {
                        Code = c.Code,
                        Id = c.Id,
                        Name = c.Name
                    }).ToList(),
                    SheetItemParent = x.SheetItemParent == null ? null : new SheetItemSimpleResponse
                    {
                        Name = x.SheetItemParent.Name,
                        Id = x.SheetItemParent.Id,
                        Code = x.SheetItemParent.Code,
                    },
                    SubCategory = new SubCategoryResponse
                    {
                        Id = x.SubCategory.Id,
                        Code = x.SubCategory.Code,
                        Name = x.SubCategory.Name,
                        Category = new CategoryResponse
                        {
                            Id = x.SubCategory.Category.Id,
                            Code = x.SubCategory.Category.Name,
                            Name = x.SubCategory.Category.Name,
                            IsActive = x.SubCategory.Category.IsActive,
                            UserIn = x.SubCategory.Category.UserIn,
                            DateIn = x.SubCategory.Category.DateIn,
                            UserUp = x.SubCategory.Category.UserUp,
                            DateUp = x.SubCategory.Category.DateUp
                        },
                        IsActive = x.SubCategory.IsActive,
                        UserIn = x.SubCategory.UserIn,
                        DateIn = x.SubCategory.DateIn,
                        UserUp = x.SubCategory.UserUp,
                        DateUp = x.SubCategory.DateUp
                    }
                })
            .AsQueryable();
            if (request.Orders != null && request.Orders.Count > 0)
            {
                foreach (var item in request.Orders)
                {
                    datas = datas.OrderBy(item + " " + request.SortType);
                }
            };
            return new SheetItemVm
            {
                Results = await datas.Skip(request.Page).Take(request.Length).ToListAsync(),
                Info = new PagedInfoRepositoryResponse
                {
                    CurrentPage = request.Page,
                    TotalPage = await datas.CountAsync(),
                    Length = request.Length
                }
            };
        }
    }
}
