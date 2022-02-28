using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Application;

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
    }
}
