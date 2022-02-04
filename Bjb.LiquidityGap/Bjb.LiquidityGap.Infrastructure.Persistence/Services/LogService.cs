using Bjb.LiquidityGap.Base.Commons;
using Bjb.LiquidityGap.Base.Dtos.AuditTrails;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Services
{
    public class LogService : ILogService
    {
        private readonly AppDBContext _appDbContext;

        public LogService(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> InsertLog(AuditTrailRequest logDto)
        {
            List<Dictionary<object, object>> listOldValue = new List<Dictionary<object, object>>();
            List<Dictionary<object, object>> listNewValue = new List<Dictionary<object, object>>();
            var modifiedEntities = _appDbContext.ChangeTracker.Entries()
               .Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Unchanged).ToList();
            if (modifiedEntities != null)
            {
                foreach (EntityEntry change in modifiedEntities)
                {
                    var entityName = change.Entity.GetType().Name;
                    foreach (var prop in change.OriginalValues.Properties)
                    {
                        var originalValue = change?.GetDatabaseValues()?.GetValue<object>(prop.Name)?.ToString();
                        var currentValue = change.CurrentValues[prop]?.ToString();
                        string ColumnName = prop.Name;
                        object OldValue = new object();
                        object NewValue = new object();
                        if (modifiedEntities[0].State == EntityState.Added)
                        {
                            OldValue = null;
                            NewValue = currentValue;
                            listOldValue.Add(new Dictionary<object, object>
                                {
                                    {ColumnName,OldValue }
                                });

                            listNewValue.Add(new Dictionary<object, object>
                                {
                                    {ColumnName,NewValue }
                                });
                        }
                        else if (modifiedEntities[0].State == EntityState.Modified)
                        {
                            if (originalValue != currentValue)
                            {
                                OldValue = originalValue;
                                NewValue = currentValue;
                                listOldValue.Add(new Dictionary<object, object>
                                {
                                    {ColumnName,OldValue }
                                });
                                listNewValue.Add(new Dictionary<object, object>
                                {
                                    {ColumnName,NewValue }
                                });
                            }
                        }
                        else if (modifiedEntities[0].State == EntityState.Deleted)
                        {
                            OldValue = currentValue;
                            NewValue = null;
                            listOldValue.Add(new Dictionary<object, object>
                                {
                                    {ColumnName,OldValue }
                                });
                            listNewValue.Add(new Dictionary<object, object>
                                {
                                    {ColumnName,NewValue }
                                });
                        }

                    }
                }
                object Detail = new
                {
                    OldValue = listOldValue,
                    NewValue = listNewValue
                };
                AuditTrail log = new AuditTrail
                {
                    Id = Guid.NewGuid(),
                    Action = logDto.Action,
                    ApplicationName = logDto.ApplicationName,
                    Detail = JsonConvert.SerializeObject(Detail).ToString(),
                    Feature = logDto.Feature,
                    LogDate = DateTime.Now,
                    Message = logDto.Message,
                    Module = logDto.Module,
                    ReferenceId = logDto.ReferenceId,
                    RoleId = logDto.RoleId,
                    RoleName = logDto.RoleName,
                    UserId = logDto.UserId,
                    UserName = logDto.UserName,
                    IsActive = true,
                    DateIn = DateTime.Now,
                    UserIn = logDto.UserId
                };
                _appDbContext.AuditTrails.Add(log);
            }
            if (logDto.Action != CommonConstants.ACTION_LOGIN)
                return await _appDbContext.SaveChangesAsync();
            return 1;
        }
    }
}
