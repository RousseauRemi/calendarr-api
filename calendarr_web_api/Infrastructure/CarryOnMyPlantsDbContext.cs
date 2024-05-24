using calendarr_web_api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace calendarr_web_api.Infrastructure;

public class CalendarrDbContext : DbContext
{
#pragma warning restore CS8618

    public CalendarrDbContext(
        DbContextOptions<CalendarrDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder
            .ApplyConfiguration(new ApiEntityDbConfiguration())
            .ApplyConfiguration(new JellyseerEntityDbConfiguration())
            .ApplyConfiguration(new ApiConfigEntityDbConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    
    #region dbSets

    public DbSet<ApiEntity?> ApiEntities { get; set; }
    public DbSet<JellyseerEntity> JellyseerEntities { get; set; }
    public DbSet<ApiConfigEntity> ApiConfigEntities { get; set; }

    #endregion dbSets
}

public class ApiEntityDbConfiguration: IEntityTypeConfiguration<ApiEntity>
{
    public void Configure(EntityTypeBuilder<ApiEntity> builder)
    {
        builder.HasKey(s => s.Name);
    }
}
public class JellyseerEntityDbConfiguration: IEntityTypeConfiguration<JellyseerEntity>
{
    public void Configure(EntityTypeBuilder<JellyseerEntity> builder)
    {
        builder.HasKey(s => s.Name);
    }
}
public class ApiConfigEntityDbConfiguration: IEntityTypeConfiguration<ApiConfigEntity>
{
    public void Configure(EntityTypeBuilder<ApiConfigEntity> builder)
    {
        builder.HasKey(s => s.Url);
    }
}