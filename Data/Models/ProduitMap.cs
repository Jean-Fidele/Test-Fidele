using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Models
{
    internal class ProduitMap : IEntityTypeConfiguration<Produit>
    {
        public void Configure(EntityTypeBuilder<Produit> builder)
        {
            builder.ToTable("Produit");
            builder.HasKey(x => x.Code);

            builder.HasOne(x => x.Categorie)
                   .WithMany(x=>x.Produits)
                   .HasForeignKey(x => x.CategorieId)
                   .IsRequired();
        }
    }
}
