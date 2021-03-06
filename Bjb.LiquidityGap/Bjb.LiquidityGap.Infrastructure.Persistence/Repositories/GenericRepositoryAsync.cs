using Bjb.LiquidityGap.Application;
using Bjb.LiquidityGap.Base.Dtos.AuditTrails;
using Bjb.LiquidityGap.Base.Extensions;
using Bjb.LiquidityGap.Base.Interfaces;
using Bjb.LiquidityGap.Base.Parameters;
using Bjb.LiquidityGap.Base.Wrappers;
using Bjb.LiquidityGap.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly AppDBContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogService _logService;
        private bool _isDeactivable = typeof(IDeactivable).IsAssignableFrom(typeof(T));
        private bool _isSoftDelete = typeof(ISoftDelete).IsAssignableFrom(typeof(T));
        private bool _isAuditable = typeof(IAuditable).IsAssignableFrom(typeof(T));
        private bool _isGuidEntity = typeof(IEntity<Guid>).IsAssignableFrom(typeof(T));
        private bool _isIntEntity = typeof(IEntity<int>).IsAssignableFrom(typeof(T));
        private bool _isAudit = typeof(IAudit).IsAssignableFrom(typeof(T));

        public GenericRepositoryAsync(AppDBContext dbContext,
            ICurrentUserService currentUserService, ILogService logService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _logService = logService;
        }

        public virtual async Task<T> GetByIdAsync(int id, string idFieldName = "Id", string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(string.Format("{0}.Equals({1})", idFieldName, id));
            //query = query.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public virtual async Task<T> GetByIdAsync(Guid id, string idFieldName = "Id", string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(string.Format("{0}.ToString().Equals(\"{1}\")", idFieldName, id));
            //query = query.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public virtual async Task<T> GetByIdAsync(string id, string idFieldName = "Id", string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(string.Format("{0}.Equals(\"{1}\")", idFieldName, id));
            //query = query.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<PagedRepositoryResponse<IReadOnlyList<T>>> GetPagedReponseAsync(IRequestParameter request, string[] includes = null)
        {
            var data = _dbContext.Set<T>().AsQueryable();
            data = _Includes(data, includes);
            data = _Filters(data, request.Filters);

            PagedInfoRepositoryResponse info = new PagedInfoRepositoryResponse
            {
                CurrentPage = request.Page,
                Length = request.Length,
                TotalPage = await data.CountAsync()
            };
            data = _Orders(data, request.Orders, request.SortType);
            if (request.Page > 0 && request.Length > 0)
            {
                data = data
                .Skip((request.Page - 1) * request.Length)
                .Take(request.Length);
            }
            data = data.AsNoTracking();
            return new PagedRepositoryResponse<IReadOnlyList<T>>
            {
                Results = await data.ToListAsync(),
                Info = info
            };
        }
        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<T> GetByPredicate(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(predicate);
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }
        public IQueryable<T> GetQueryByPredicate(Expression<Func<T, bool>> predicate)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = query.Where(predicate);
            return query;
        }
        public Task<int> GetCountByPredicate(Expression<Func<T, bool>> predicate)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = query.Where(predicate);
            return query.CountAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            if (_isDeactivable)
            {
                (entity as IDeactivable).IsActive = true;
            }
            //if (_isSoftDelete)
            //{
            //    (entity as ISoftDelete).IsDeleted = false;
            //}
            (entity as IAudit).UserIn = _currentUserService.UserId;
            (entity as IAudit).DateIn = DateTime.Now;
            await _dbContext.Set<T>().AddAsync(entity);
            if (_isAudit)
            {
                AuditTrailRequest log = new AuditTrailRequest
                {
                    Id = Guid.NewGuid(),
                    Action = Constant.ACTION_INSERT,
                    ApplicationName = Constant.NAMA_APLIKASI,
                    Detail = "",
                    Feature = (entity as IAuditable).FeatureName,
                    LogDate = DateTime.Now,
                    Message = "Success",
                    Module = (entity as IAuditable).ModuleName,
                    ReferenceId = (entity as IEntity<int>)?.Id.ToString() ?? "0000000000",
                    RoleId = _currentUserService.IdFungsi,
                    RoleName = _currentUserService.IdFungsi,
                    UserId = _currentUserService.UserId,
                    UserName = _currentUserService.UserName,

                };
                await _logService.InsertLog(log);


            }

            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }
        public List<T> AddRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                if (_isDeactivable)
                {
                    (entity as IDeactivable).IsActive = true;
                }
                if (_isSoftDelete)
                {
                    (entity as ISoftDelete).IsDeleted = false;
                }
                if (_isAudit)
                {
                    (entity as IAudit).UserIn = _currentUserService.UserId;
                    (entity as IAudit).DateIn = DateTime.Now;

                }
            }
            _dbContext.Set<T>().AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }
        public async Task UpdateAsync(T entity)
        {
            (entity as IAudit).UserUp = _currentUserService.UserId;
            (entity as IAudit).DateUp = DateTime.Now;
            _dbContext.Attach(entity);
            EntityEntry entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            _dbContext.Set<T>().Update(entity);
            if (_isAudit)
            {

                AuditTrailRequest log = new AuditTrailRequest
                {
                    Id = Guid.NewGuid(),
                    Action = Constant.ACTION_UPDATE,
                    ApplicationName = Constant.NAMA_APLIKASI,
                    Detail = "",
                    Feature = (entity as IAuditable).FeatureName,
                    LogDate = DateTime.Now,
                    Message = "Success",
                    Module = (entity as IAuditable).ModuleName,
                    ReferenceId = (entity as IEntity<int>)?.Id.ToString() ?? "0000000000",
                    RoleId = _currentUserService.IdFungsi,
                    RoleName = _currentUserService.IdFungsi,
                    UserId = _currentUserService.UserId,
                    UserName = _currentUserService.UserName
                };
                await _logService.InsertLog(log);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateRangeAsync(List<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.UpdateRange(entities);
                if (_isAudit)
                {

                    AuditTrailRequest log = new AuditTrailRequest
                    {
                        Id = Guid.NewGuid(),
                        Action = Constant.ACTION_INSERT,
                        ApplicationName = Constant.NAMA_APLIKASI,
                        Detail = "",
                        Feature = (entity as IAuditable).FeatureName,
                        LogDate = DateTime.Now,
                        Message = "Success",
                        Module = (entity as IAuditable).ModuleName,
                        ReferenceId = (entity as IEntity<int>)?.Id.ToString() ?? "0000000000",
                        RoleId = _currentUserService.IdFungsi,
                        RoleName = _currentUserService.IdFungsi,
                        UserId = _currentUserService.UserId,
                        UserName = _currentUserService.UserName
                    };
                    await _logService.InsertLog(log);
                    (entity as IAudit).UserUp = _currentUserService.UserId;
                    (entity as IAudit).DateUp = DateTime.Now;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        public void DeleteRange(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(string[] includes = null)
        {
            var query = _dbContext
                 .Set<T>()
                 //.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true)
                 .AsQueryable();
            query = _Includes(query, includes);
            return await query.AsNoTracking()
                .ToListAsync();
        }
        public IReadOnlyList<T> GetAll()
        {
            return _dbContext
                 .Set<T>()
                 //.Where(r => _isDeactivable ? (r as IDeactivable).IsActive : true)
                 .AsNoTracking()
                 .ToList();
        }
        public void DisableAuditable()
        {
            _isAuditable = false;
        }
        #region "Private Access"
        public string _GetComparisonOperator(string comparisonOperator)
        {
            var result = "";
            switch (comparisonOperator)
            {
                case "like":
                    result = ".Contains(";
                    break;
                case "not like":
                    result = ".Contains(";
                    break;
                case "!=":
                    result = "!=";
                    break;
                case "<":
                    result = " < ";
                    break;
                case ">":
                    result = " > ";
                    break;
                case "<=":
                    result = " <= ";
                    break;
                case ">=":
                    result = " >= ";
                    break;
                case "<>":
                    result = " <> ";
                    break;
                default:
                    result = "=";
                    break;

            }
            return result;
        }

        private string _GetClosedTagComparisonOperator(string comparisonOperator)
        {
            var result = "";
            switch (comparisonOperator)
            {
                case "like":
                    result = ")";
                    break;
                case "not like":
                    result = ") == false";
                    break;
            }
            return result;
        }
        private string _GetConverter(object value)
        {
            var result = "";
            switch (value)
            {
                case Guid:
                    result = ".ToString()";
                    break;
            }
            return result;
        }
        private object _GetValue(object value)
        {
            var result = value;
            switch (value)
            {
                case Enum:
                    result = (int)value;
                    break;
            }
            return result;
        }
        protected IQueryable<T> _Includes(IQueryable<T> query, string[] includes = null)
        {
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
        private bool _IsUseDoubleQuote(object value)
        {
            switch (value)
            {
                case int:
                case Enum:
                    return false;
            }
            return true;
        }
        protected IQueryable<T> _Filters(IQueryable<T> query, List<RequestFilterParameter> filters)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter.Type != "raw")
                    {
                        query = query.Where(
                         string.Format(
                             "{0}{5}{1}{3}{2}{3}{4}"
                             , filter.Field
                             , _GetComparisonOperator(filter.ComparisonOperator)
                             , _GetValue(filter.GetFilterValue())
                             , _IsUseDoubleQuote(filter.GetFilterValue()) ? "\"" : ""
                             , _GetClosedTagComparisonOperator(filter.ComparisonOperator)
                             , _GetConverter(filter.GetFilterValue())
                             )
                         );
                    }
                    else
                    {
                        var predicate = string.Format(
                           "{0}{5}{1}{3}{2}{3}{4}"
                           , $"{filter.Field.Split(".")[1]}.{ filter.Field.Split(".")[2]}"
                           , _GetComparisonOperator(filter.ComparisonOperator)
                           , _GetValue(filter.GetFilterValue())
                           , _IsUseDoubleQuote(filter.GetFilterValue()) ? "\"" : ""
                           , _GetClosedTagComparisonOperator(filter.ComparisonOperator)
                           , _GetConverter(filter.GetFilterValue())
                           );
                        query = query.Where($"{filter.Field.Split(".")[0]}.Any({predicate})");
                    }


                }
            }
            return query;
        }
        protected IQueryable<T> _Orders(IQueryable<T> query, List<string> orders, string sortType)
        {
            if (orders != null && orders.Count > 0)
            {
                query = query.OrderBy(
                        string.Format(
                            "{0} {1}"
                            , string.Join(",", orders)
                            , sortType
                            )
                        );
            }
            return query;
        }

        public async Task<List<T>> GetListByPredicate(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            query = _Includes(query, includes);
            query = query.Where(predicate);
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
