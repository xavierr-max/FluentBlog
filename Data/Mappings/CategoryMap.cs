using Microsoft.EntityFrameworkCore;
using FluentBlog.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentBlog.Data.Mappings
{
    class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category"); //mapeia da entidade Category para a table "Category" no banco de dados

            builder.HasKey(x => x.Id); //chave primaria

            builder.Property(x => x.Id) //Seleciona a propriedade Id da entidade para configurar.
                .ValueGeneratedOnAdd() //valor do Id será gerado automaticamente quando um novo registro for adicionado
                .UseIdentityColumn(); //coluna do tipo identidade, começa em 1 e incrementa de 1 em 1 automaticamente.

            builder.Property(x => x.Name)
                .IsRequired() //IS NOT NULL
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.HasIndex(x => x.Slug, "IX_Category_Slug") //indice (index)
                .IsUnique(); //indice deve ser único
        }
    }
}


