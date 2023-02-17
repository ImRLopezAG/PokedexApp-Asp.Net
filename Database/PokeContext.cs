using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class PokeContext : DbContext {
  public PokeContext(DbContextOptions<PokeContext> options) : base(options) { }

  public DbSet<PokemonModel> Pokemons { get; set; }
  public DbSet<TypeModel> Types { get; set; }
  public DbSet<RegionModel> Regions { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    #region tables
    modelBuilder.Entity<PokemonModel>().ToTable("Pokemons");
    modelBuilder.Entity<RegionModel>().ToTable("Regions");
    modelBuilder.Entity<TypeModel>().ToTable("Types");
    #endregion

    #region "primary keys"
    modelBuilder.Entity<PokemonModel>().HasKey(pokemon => pokemon.Id);
    modelBuilder.Entity<RegionModel>().HasKey(region => region.Id);
    modelBuilder.Entity<TypeModel>().HasKey(pokemonTypes => pokemonTypes.Id);
    #endregion

    #region relationships
    modelBuilder.Entity<RegionModel>()
        .HasMany(region => region.Pokemons)
        .WithOne(pokemon => pokemon.Regions)
        .HasForeignKey(pokemon => pokemon.RegionId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<TypeModel>()
        .HasMany(type => type.PrimaryType)
        .WithOne(pokemon => pokemon.PrimaryType)
        .HasForeignKey(pokemon => pokemon.PrimaryTypeId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<TypeModel>()
        .HasMany(type => type.SecondaryType)
        .WithOne(pokemon => pokemon.SecondaryType)
        .HasForeignKey(pokemon => pokemon.SecondaryTypeId)
        .OnDelete(DeleteBehavior.NoAction);
    #endregion

    #region "property configurations"

    #region pokemons
    modelBuilder.Entity<PokemonModel>()
        .Property(pokemon => pokemon.Name)
        .IsRequired()
        .HasMaxLength(100);

    modelBuilder.Entity<PokemonModel>()
        .Property(pokemon => pokemon.Image)
        .IsRequired();

    modelBuilder.Entity<PokemonModel>()
        .Property(pokemon => pokemon.RegionId)
        .IsRequired();

    modelBuilder.Entity<PokemonModel>()
        .Property(pokemon => pokemon.PrimaryTypeId)
        .IsRequired();
    #endregion

    #region regions
    modelBuilder.Entity<RegionModel>()
        .Property(region => region.Name)
        .IsRequired()
        .HasMaxLength(35);
    #endregion

    #region pokemonTypes
    modelBuilder.Entity<TypeModel>()
       .Property(pokemonTypes => pokemonTypes.Name)
       .IsRequired()
       .HasMaxLength(35);
    #endregion

    #endregion
  }
}




