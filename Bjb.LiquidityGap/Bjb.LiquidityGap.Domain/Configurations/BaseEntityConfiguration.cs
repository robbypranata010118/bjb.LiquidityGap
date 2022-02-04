using Bjb.LiquidityGap.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Domain.Configurations
{
    public static class BaseEntityConfiguration
    {
        static void Configure<TEntity, T>(ModelBuilder modelBuilder)
       where TEntity : BaseEntity<T>
        {
            modelBuilder.Entity<TEntity>(builder =>
            {
                builder.Property(x => x.IsActive).IsRequired();
                builder.HasIndex(x => x.IsActive);
                builder.HasQueryFilter(x => x.IsActive);
            });
        }

        public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder)
        {
            var method = typeof(BaseEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(Configure));
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.IsBaseEntity(out var T))
                    method.MakeGenericMethod(entityType.ClrType, T).Invoke(null, new[] { modelBuilder });
            }
            return modelBuilder;
        }

        static bool IsBaseEntity(this Type type, out Type T)
        {
            for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(BaseEntity<>))
                {
                    T = baseType.GetGenericArguments()[0];
                    return true;
                }
            }
            T = null;
            return false;
        }
    }
}
