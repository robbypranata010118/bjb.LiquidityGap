using Bjb.LiquidityGap.Application;
using Bjb.LiquidityGap.Application.Exceptions;
using Bjb.LiquidityGap.Base.Dtos.TimeBuckets;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Services
{
    public class TimeBucketService : ITimeBucket
    {
        private readonly AppDBContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogService _logService;

        public TimeBucketService(ILogService logService, AppDBContext appDbContext, ICurrentUserService currentUserService)
        {
            _logService = logService;
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
        }
        public async Task<int> CreateTimeBucket(AddTimeBucketRequest request)
        {
            try
            {
                await _appDbContext.Database.BeginTransactionAsync();
                foreach (var checkCharacteristic in request.CharacteristicTimebuckets)
                {
                    var checkCharacteriticId = await _appDbContext.Characteristics.Where(x => checkCharacteristic.CharacteristicId == x.Id).FirstOrDefaultAsync();
                    if (checkCharacteriticId == null)
                        throw new Exception(string.Format(Constant.MessageDataNotFound, Constant.Characteristic, checkCharacteristic.CharacteristicId));
                }


                var listCharTimeBucket = new List<CharacteristicTimebucket>();
                foreach (var dataChar in request.CharacteristicTimebuckets)
                {
                    listCharTimeBucket.Add(new CharacteristicTimebucket
                    {
                        CharacteristicId = dataChar.CharacteristicId,
                        UsePercentage = dataChar.UsePercentage,
                        DayRange = dataChar.DayRange,
                        Percentage = dataChar.Percentage,
                        UserIn = _currentUserService.UserId,
                        DateIn = DateTime.Now,
                        IsActive = true,
                    });
                }

                Timebucket timeBucket = new Timebucket
                {
                    Code = request.Code,
                    Label = request.Label,
                    Sequence = request.Sequence,
                    UserIn = _currentUserService.UserId,
                    DateIn = DateTime.Now,
                    IsActive = true,
                    CharacteristicTimebuckets = listCharTimeBucket
                };
                await _appDbContext.Timebuckets.AddAsync(timeBucket);
                await _appDbContext.SaveChangesAsync();
                await _appDbContext.Database.CommitTransactionAsync();
                return timeBucket.Id;
            }
            catch (Exception ex)
            {

                await _appDbContext.Database.RollbackTransactionAsync();
                throw new ApiException(ex.Message.ToString());
            }
        }
        public async Task<int> EditTimeBucket(UpdateTimeBucketRequest timeBucket)
        {
            try
            {
                await _appDbContext.Database.BeginTransactionAsync();

                var timeBucketCharacteristics = await _appDbContext.CharacteristicTimebuckets.Where(x => x.TimebucketId == timeBucket.Id).ToListAsync();
                //should be 3 in here
                var characteristicInRequest = timeBucket.CharacteristicTimebuckets.ToList();

                #region Delete
                foreach (var item in timeBucketCharacteristics)
                {
                    item.IsActive = false;
                    item.UserUp = _currentUserService.UserId;
                    item.DateUp = DateTime.Now;
                    _appDbContext.CharacteristicTimebuckets.Update(item);
                    await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                    {
                        Id = Guid.NewGuid(),
                        Action = Constant.ACTION_UPDATE,
                        ApplicationName = Constant.NAMA_APLIKASI,
                        Detail = "",
                        Feature = "Time Bucket Characteristic",
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
                foreach (var sub in timeBucket.CharacteristicTimebuckets)
                {
                    var checkDataWillBeAdded = await _appDbContext.Characteristics.Where(x => x.Id == sub.CharacteristicId && x.IsActive).FirstOrDefaultAsync();
                    if (checkDataWillBeAdded == null)
                        throw new Exception(string.Format(Constant.MessageDataNotFound, Constant.Characteristic, sub.CharacteristicId));
                }

                foreach (var dataChar in timeBucket.CharacteristicTimebuckets)
                {
                    CharacteristicTimebucket timeBucketCharacteristic = new CharacteristicTimebucket
                    {
                        CharacteristicId = dataChar.CharacteristicId,
                        TimebucketId = timeBucket.Id,
                        UsePercentage = dataChar.UsePercentage,
                        DayRange = dataChar.DayRange,
                        Percentage = dataChar.Percentage,
                        UserIn = _currentUserService.UserId,
                        DateIn = DateTime.Now,
                        IsActive = true
                    };
                    _appDbContext.CharacteristicTimebuckets.Add(timeBucketCharacteristic);
                    await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                    {
                        Id = Guid.NewGuid(),
                        Action = Constant.ACTION_INSERT,
                        ApplicationName = Constant.NAMA_APLIKASI,
                        Detail = "",
                        Feature = "Time Bucket Characteristic",
                        LogDate = DateTime.Now,
                        Message = "Success",
                        Module = "Master Data",
                        ReferenceId = timeBucketCharacteristic.Id.ToString(),
                        RoleId = _currentUserService.IdFungsi,
                        RoleName = _currentUserService.IdFungsi,
                        UserId = _currentUserService.UserId,
                        UserName = _currentUserService.UserName,
                    });
                }




                #endregion

                var dataTimeBucket = await _appDbContext.Timebuckets.Where(x => x.Id == timeBucket.Id).FirstOrDefaultAsync();
                dataTimeBucket.Code = timeBucket.Code;
                dataTimeBucket.Label = timeBucket.Label;
                dataTimeBucket.Sequence = timeBucket.Sequence;
                _appDbContext.Timebuckets.Update(dataTimeBucket);
                await _logService.InsertLog(new Base.Dtos.AuditTrails.AuditTrailRequest
                {
                    Id = Guid.NewGuid(),
                    Action = Constant.ACTION_INSERT,
                    ApplicationName = Constant.NAMA_APLIKASI,
                    Detail = "",
                    Feature = "Time Bucket",
                    LogDate = DateTime.Now,
                    Message = "Success",
                    Module = "Master",
                    ReferenceId = dataTimeBucket.Id.ToString(),
                    RoleId = _currentUserService.IdFungsi,
                    RoleName = _currentUserService.IdFungsi,
                    UserId = _currentUserService.UserId,
                    UserName = _currentUserService.UserName,
                });
                await _appDbContext.SaveChangesAsync();
                await _appDbContext.Database.CommitTransactionAsync();
                return timeBucket.Id;
            }
            catch (Exception ex)
            {

                await _appDbContext.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}
