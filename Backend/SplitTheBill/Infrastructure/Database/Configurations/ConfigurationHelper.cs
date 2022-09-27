using Domain.Common.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public static class ConfigurationHelper
{
    public static void ConfigureIdForEntity<TEntity, TId>(EntityTypeBuilder<TEntity> typeBuilder)
        where TEntity : BaseEntity<TId>
        where TId : DatabaseEntityId, new()
    {
        typeBuilder.Property(x => x.Id)
                   .HasConversion(x => x.Id, id => new TId { Id = id })
                   .HasColumnName(nameof(DatabaseEntityId.Id))
                   .IsRequired()
                   ;
    }
}