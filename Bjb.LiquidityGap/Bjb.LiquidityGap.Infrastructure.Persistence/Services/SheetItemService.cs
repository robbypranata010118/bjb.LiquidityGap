using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Bjb.LiquidityGap.Base.Dtos.SheetItems;

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Services
{
    public class SheetItemService : ISheetItem
    {
        private readonly AppDBContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        public SheetItemService(AppDBContext appDbContext, ICurrentUserService currentUserService)
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<int> CreateSheetItem(AddSheetItemRequest request)
        {
            try
            {
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
                    SheetItemCharacteristics = request.SheetItemCharacteristics.Select(x => new SheetItemCharacteristic
                    {
                        UserIn = _currentUserService.UserId,
                        DateIn = DateTime.Now,
                        IsActive = true,
                        CharacteristicId = x
                    }).ToList()
                };
                await _appDbContext.Database.BeginTransactionAsync();
                await _appDbContext.SheetItems.AddAsync(sheetItem);
                await _appDbContext.SaveChangesAsync();
                await _appDbContext.Database.CommitTransactionAsync();
                return sheetItem.Id;
            }
            catch (Exception ex)
            {

                await _appDbContext.Database.RollbackTransactionAsync();
                return 0;
            }
        }

        public async Task<int> EditSheetItem(UpdateSheetItemRequest sheetItem)
        {
            try
            {
                var sheetItemCharacteristics = await _appDbContext.SheetItemCharacteristics.Where(x => x.SheetItemId == sheetItem.Id).ToListAsync();
                //should be 3 in here
                var characteristicInRequest = sheetItem.SheetItemCharacteristics;
                var sheeItemNotInRequests = sheetItemCharacteristics.Where(x => !characteristicInRequest.Any(y => y == x.CharacteristicId)).ToList();

                List<SheetItemCharacteristic> newSheetItemCharacteristics = new List<SheetItemCharacteristic>();
                foreach (var item in sheeItemNotInRequests)
                {
                    var isExist = sheetItemCharacteristics.Where(x => x.CharacteristicId == item.CharacteristicId).FirstOrDefault();
                    if (isExist != null)
                    {
                        //case data lama yang di delete
                        isExist.IsActive = false;
                        isExist.DateUp = DateTime.Now;
                        isExist.UserUp = _currentUserService.UserId;
                        _appDbContext.SheetItemCharacteristics.Update(isExist);
                        
                    }
                    else
                    {
                        //case untuk data baru
                        SheetItemCharacteristic newSheetItemCharacteristic = new()
                        {
                            CharacteristicId = item.CharacteristicId,
                            SheetItemId = sheetItem.Id,
                            DateIn = DateTime.Now,
                            UserIn = _currentUserService.UserId
                        };
                        await _appDbContext.SheetItemCharacteristics.AddAsync(newSheetItemCharacteristic);
                    }
                }
                var dataSheetItem = await _appDbContext.SheetItems.Where(x => x.Id == sheetItem.Id).FirstOrDefaultAsync();
                dataSheetItem.IsManualInput = sheetItem.IsManualInput;
                dataSheetItem.MarkToCalculate = sheetItem.MarkToCalculate;
                dataSheetItem.SheetItemParentId = sheetItem.SheetItemParentId;
                dataSheetItem.SubCategoryId = sheetItem.SubCategoryId;
                dataSheetItem.DataSourceId = sheetItem.DataSourceId;
                dataSheetItem.Code = sheetItem.Code;
                dataSheetItem.Name = sheetItem.Name;
                dataSheetItem.Statement = sheetItem.Statement;
                //dataSheetItem.SheetItemCharacteristics = newSheetItemCharacteristics;
                await _appDbContext.Database.BeginTransactionAsync();
                _appDbContext.SheetItems.Update(dataSheetItem);
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
