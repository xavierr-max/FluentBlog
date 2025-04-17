using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using FluentBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentBlog.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Tabela
            builder.ToTable("Post");

            // Chave Primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Propriedades
            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60)
                .HasDefaultValueSql("GETDATE()");
            // .HasDefaultValue(DateTime.Now.ToUniversalTime());

            // Índices
            builder
                .HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();

            //"um para muitos"
            builder
                .HasOne(x => x.Author) // um post "tem-um" autor
                .WithMany(x => x.Posts) // um autor "tem-muitos" post
                .HasConstraintName("FK_Post_Author")
                .OnDelete(DeleteBehavior.Cascade); //Se um Author for deletado, todos os Posts relacionados também serão automaticamente deletados no banco.
            
            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Posts)
                .HasConstraintName("FK_Post_Category")
                .OnDelete(DeleteBehavior.Cascade);

            //"muitos para muitos"
            builder
                //define a relação
                .HasMany(x => x.Tags)
                .WithMany(x => x.Posts) 
                //tabela virtual PostTag, define a tabela de junção manualmente
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag", //nome da tabela
                    post => post //a tabela PostTag terá um campo PostId apontando para Post
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_PostTag_PostId")
                        .OnDelete(DeleteBehavior.Cascade),
                    tag => tag //a tabela PostTag terá também um campo TagId apontando para Tag
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PostTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade));
                
                
                
        }
    }
}