using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.Books;

public class BooksDBContext : DbContext
{
    internal DbSet<Book> Books { get; set; }

    public BooksDBContext(DbContextOptions<BooksDBContext> options) : base(options)
    {
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Books");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
    }

}


internal class BookConfiguration : IEntityTypeConfiguration<Book>
{

    internal static readonly Guid Book1Id = new Guid("00000000-0000-0000-0000-000000000001");
    internal static readonly Guid Book2Id = new Guid("00000000-0000-0000-0000-000000000002");
    internal static readonly Guid Book3Id = new Guid("00000000-0000-0000-0000-000000000003");
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(p => p.Title).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH).IsRequired();
        builder.Property(p => p.Author).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH).IsRequired();
        builder.HasData(GetSampleBookData());
    }

    private IEnumerable<Book> GetSampleBookData()
    {
        var author = "JB Peterson";
        return new List<Book>
        {
            new(id: Book1Id, title: "12 Rules for Life", author: author, price: 20.00m,year: 2018),
            new Book(id: Book2Id, title: "Beyond Order", author: author, price: 25.00m,year: 2021),
            new Book(id: Book3Id, title: "Maps of Meaning", author: author, price: 30.00m,year: 1999)
        };
    }
}

internal class DataSchemaConstants
{
    public static int DEFAULT_NAME_LENGTH { get; internal set; } = 200;
}