using ForumAppCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            //modelBuilder.Entity<Author>().HasData(
            //    new Author
            //    {
            //        AuthorId = 1,
            //        FirstName = "William",
            //        LastName = "Shakespeare"
            //    }
            //);
            //modelBuilder.Entity<Book>().HasData(
            //    new Book { BookId = 1, AuthorId = 1, Title = "Hamlet" },
            //    new Book { BookId = 2, AuthorId = 1, Title = "King Lear" },
            //    new Book { BookId = 3, AuthorId = 1, Title = "Othello" }
            //);

            builder.Entity<TagsLanguage>().HasData(
                new TagsLanguage {Id = 1, Name = "C#" },
                new TagsLanguage { Id = 2, Name = "C++" },
                new TagsLanguage { Id = 3, Name = "C" },
                new TagsLanguage { Id = 4, Name = "Dart" },
                new TagsLanguage { Id = 5, Name = "Flutter" },
                new TagsLanguage { Id = 6, Name = "Angular" },
                new TagsLanguage { Id = 7, Name = "TypeScript" },
                new TagsLanguage { Id = 8, Name = "ReactJs" },
                new TagsLanguage { Id = 9, Name = "VueJs" },
                new TagsLanguage { Id = 10, Name = "html" },
                new TagsLanguage { Id = 11, Name = "css" },
                new TagsLanguage { Id = 12, Name = "scss" },
                new TagsLanguage { Id = 13, Name = "css" },
                new TagsLanguage { Id = 14, Name = "javascript" },
                new TagsLanguage { Id = 15, Name = "java" },
                new TagsLanguage { Id = 16, Name = "Sql" },
                new TagsLanguage { Id = 17, Name = "Swift" },
                new TagsLanguage { Id = 18, Name = "kotlin" },
                new TagsLanguage { Id = 19, Name = "Android" },
                new TagsLanguage { Id = 20, Name = "IOS" },
                new TagsLanguage { Id = 21, Name = "ReactNative" }
           );
        }
    }
}
