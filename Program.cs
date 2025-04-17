using System;
using FluentBlog.Data;
using FluentBlog.Models;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new BlogDataContext();

            context.Users.Add(new User
            {
                Name = "Maxwell Araújo",
                Bio = "Desenvolvedor backend apaixonado por .NET.",
                Email = "maxwelldearaujo1017@gmail.com",
                Image = "https://balta.io", 
                PasswordHash = "minhasenha123",
                Slug = "maxwell-araujo"
            });
            context.SaveChanges();

            Console.WriteLine("Usuário salvo com sucesso!");
        
        }
    }
}
