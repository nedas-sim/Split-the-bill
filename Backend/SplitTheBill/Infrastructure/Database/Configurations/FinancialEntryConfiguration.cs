using Domain.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public static class FinancialEntryConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        ConfigureFinancialEntry(modelBuilder.Entity<Entry>());
        ConfigureFinancialEntryLines(modelBuilder.Entity<EntryExpense>());
    }

    private static void ConfigureFinancialEntry(EntityTypeBuilder<Entry> builder)
    {
        builder.HasOne(e => e.Group)
               .WithMany()
               .HasForeignKey(e => e.GroupId)
               .OnDelete(DeleteBehavior.NoAction);
    }

    private static void ConfigureFinancialEntryLines(EntityTypeBuilder<EntryExpense> builder)
    {
        builder.HasOne(ee => ee.Entry)
               .WithMany(e => e.Expenses)
               .HasForeignKey(ee => ee.EntryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ee => ee.Payer)
               .WithMany()
               .HasForeignKey(ee => ee.PayerId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ee => ee.Debtor)
               .WithMany()
               .HasForeignKey(ee => ee.DebtorId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
