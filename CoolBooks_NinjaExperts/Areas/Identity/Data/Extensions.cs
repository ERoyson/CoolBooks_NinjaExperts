using CoolBooks_NinjaExperts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoolBooks_NinjaExperts.Areas.Identity.Data
{
    public static class Extensions
    {
        public static void SeedAuthors(this ModelBuilder builder) // Images here?
        {
            builder.Entity<Authors>().HasData(
                new Authors { Id = 1, FullName = "JK Rowling", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 16 }, // HarryP 1 & 7
                new Authors { Id = 2, FullName = "Andrzej Sapkowski", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 17 }, // Ladyofthelake, lastwish, swordofdestiny
                new Authors { Id = 3, FullName = "E.L. James", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 19 },  // 50 shades
                new Authors { Id = 4, FullName = "Stephen King", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 25 }, // The Outsider
                new Authors { Id = 5, FullName = "Bret Easton Ellis", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 18 }, // American Psycho
                new Authors { Id = 6, FullName = "Frank Herbert", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 20 }, // Dune
                new Authors { Id = 7, FullName = "F Scott Fitzgerald", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 21 }, // The Great Gatsby
                new Authors { Id = 8, FullName = "Peter Benchley", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 24 }, // Jaws
                new Authors { Id = 9, FullName = "Mario Puzo", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 23 }, // The Godfather
                new Authors { Id = 10, FullName = "J.R.R. Tolkien", Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", ImageId = 22 } // The Lord of the Rings
                );
        }

        public static void SeedGenres(this ModelBuilder builder)
        {
            builder.Entity<Genres>().HasData(
                    new Genres { Id = 1, Name = "Detective & Mystery", Description = "One of the toughest genres to write, this one centers on a mystery and involves either a professional or amateur sleuth. Examples: Murder on the the Orient Express by Agatha Christie." },
                    new Genres { Id = 2, Name = "Thriller", Description = "This genre also has scary elements, but its main objective is to keep your reader in a state of suspense until the story’s resolution. Example: The Girl with the Dragon Tattoo by Stieg Larsson" },
                    new Genres { Id = 3, Name = "Romance", Description = "Any novel where the main storyline centers on a romantic relationship falls into this category, which has several subgenres. Examples include The Overdue Life of Amy Byler by Kelly Harms" },
                    new Genres { Id = 4, Name = "Science fiction", Description = " Similar to fantasy, this genre explores futuristic or technological themes and ideas to address scientific “what if” questions. Examples: The Hitchhiker’s Guide to the Galaxy by Douglas Adams and The Atlantis Gene by A.G. Riddle" },
                    new Genres { Id = 5, Name = "Fantasy", Description = "The fantasy genre involves world-building and characters who are supernatural, mythological, magical, or a combination of these. Examples: Game of Thrones by George R.R. Martin and Circe by Madeline Miller" },
                    new Genres { Id = 6, Name = "Young adult", Description = "Young adult fiction is written for readers ages 12 to 18. They incorporate the typical reading level and worldview of tweens and teens in this age group. The Hunger Games series by Suzanne Collins is an example of popular young adult fiction." },
                    new Genres { Id = 7, Name = "Historical fiction", Description = "This genre covers fiction set in a specific time period and providing historically accurate detail relevant to the period and its characters. Examples: The Help by Kathryn Stockett" },
                    new Genres { Id = 8, Name = "Adventure", Description = "Any novel that focuses on an adventure undertaken by the main character (with or without help) falls under the adventure genre. This genre can easily be combined with others. Example: White Fang by Jack London" },
                    new Genres { Id = 9, Name = "Horror", Description = "The goal of this genre is to scare your readers and keep them that way until the hero vanquishes the threat. Example: The Shining by Stephen King" },
                    new Genres { Id = 10, Name = "Children", Description = "Children’s fiction is often called children’s literature or juvenile fiction. Books in this genre are written with readers under the age of 12 in mind. Types of children’s books include picture books and chapter books. Classic Dr. Seuss books like Green Eggs and Ham are examples of children’s fiction." },
                    new Genres { Id = 11, Name = "Dystopian", Description = "Sometimes considered a subgenre of fantasy or of science fiction, this genre is usually set in a bleak future (near or distant) to explore cultural or social issues. Examples include Wool by Hugh Howey and The Handmaid’s Tale by Margaret Atwood" },
                    new Genres { Id = 12, Name = "Novel", Description = "The novel is a genre of fiction, and fiction may be defined as the art or craft of contriving, through the written word, representations of human life that instruct or divert or both." }
                );

        }

        public static void SeedImages(this ModelBuilder builder)
        {
            builder.Entity<Images>().HasData(
                // BookCovers
                new Images { Id = 1, Image = ReadFile("00_55.jpg"), Thumbnail = SetThumbnail(ReadFile("00_55.jpg")) },
                new Images { Id = 2, Image = ReadFile("32841081._SY475_.jpg"), Thumbnail = SetThumbnail(ReadFile("32841081._SY475_.jpg")) },
                new Images { Id = 3, Image = ReadFile("3e206b5824c721fec49a9453b4336f09--christian-grey-james-darcy.jpg"), Thumbnail = SetThumbnail(ReadFile("3e206b5824c721fec49a9453b4336f09--christian-grey-james-darcy.jpg")) },
                new Images { Id = 4, Image = ReadFile("51J1B27J8CL.jpg"), Thumbnail = SetThumbnail(ReadFile("51J1B27J8CL.jpg")) },
                new Images { Id = 5, Image = ReadFile("9781501180989.jpg"), Thumbnail = SetThumbnail(ReadFile("9781501180989.jpg")) },
                new Images { Id = 6, Image = ReadFile("american-psycho-670x1024.jpg"), Thumbnail = SetThumbnail(ReadFile("american-psycho-670x1024.jpg")) },
                new Images { Id = 7, Image = ReadFile("bbcdune.jpg"), Thumbnail = SetThumbnail(ReadFile("bbcdune.jpg")) },
                new Images { Id = 8, Image = ReadFile("gatsby-original2.jpg"), Thumbnail = SetThumbnail(ReadFile("gatsby-original2.jpg")) },
                new Images { Id = 9, Image = ReadFile("HarryPotter1.jpg"), Thumbnail = SetThumbnail(ReadFile("HarryPotter1.jpg")) },
                new Images { Id = 10, Image = ReadFile("HarryPotter7.png"), Thumbnail = SetThumbnail(ReadFile("HarryPotter7.png")) },
                new Images { Id = 11, Image = ReadFile("SwordofDestiny.jpg"), Thumbnail = SetThumbnail(ReadFile("SwordofDestiny.jpg")) },
                new Images { Id = 12, Image = ReadFile("Jaws.jpg"), Thumbnail = SetThumbnail(ReadFile("Jaws.jpg")) },
                new Images { Id = 13, Image = ReadFile("The-Godfather-Book-Cover-399x600.jpg"), Thumbnail = SetThumbnail(ReadFile("The-Godfather-Book-Cover-399x600.jpg")) },
                new Images { Id = 14, Image = ReadFile("The-Last-Wish-Geralt.jpg"), Thumbnail = SetThumbnail(ReadFile("The-Last-Wish-Geralt.jpg")) },
                new Images { Id = 15, Image = ReadFile("y648.jpg"), Thumbnail = SetThumbnail(ReadFile("y648.jpg")) },
                // --------------------
                // Author images
                new Images { Id = 16, Image = ReadFile("JK-Rowling.jpg"), Thumbnail = SetThumbnail(ReadFile("JK-Rowling.jpg")) }, // JK 
                new Images { Id = 17, Image = ReadFile("Andrzej Sapkowski.jpg"), Thumbnail = SetThumbnail(ReadFile("Andrzej Sapkowski.jpg")) }, // Andrzej Sapkowski
                new Images { Id = 18, Image = ReadFile("BretEastonEllis.jpg"), Thumbnail = SetThumbnail(ReadFile("BretEastonEllis.jpg")) },
                new Images { Id = 19, Image = ReadFile("E.L.James.jpg"), Thumbnail = SetThumbnail(ReadFile("E.L.James.jpg")) },
                new Images { Id = 20, Image = ReadFile("Frank-Herbert-September-27-1982.jpg"), Thumbnail = SetThumbnail(ReadFile("Frank-Herbert-September-27-1982.jpg")) },
                new Images { Id = 21, Image = ReadFile("F-Scott-Fiztgerald.jpg"), Thumbnail = SetThumbnail(ReadFile("F-Scott-Fiztgerald.jpg")) },
                new Images { Id = 22, Image = ReadFile("jrrTolkien.jpg"), Thumbnail = SetThumbnail(ReadFile("jrrTolkien.jpg")) },
                new Images { Id = 23, Image = ReadFile("MarioPuzo.jpg"), Thumbnail = SetThumbnail(ReadFile("MarioPuzo.jpg")) },
                new Images { Id = 24, Image = ReadFile("PeterBenchley.jpg"), Thumbnail = SetThumbnail(ReadFile("PeterBenchley.jpg")) },
                new Images { Id = 25, Image = ReadFile("stephenking.jpg"), Thumbnail = SetThumbnail(ReadFile("stephenking.jpg")) }


                );

        }

        public static void SeedBooks(this ModelBuilder builder)
        {


            builder.Entity<Books>().HasData(
                new Books { Id = 1, UserId = "0cgg7156-dee2-fe6g-4df6-147f5836e357", Title = "Harry Potter and The Sorcerer's Stone", BookSeries = "Harry Potter", Published = DateTime.Parse("1997-06-26"), Description = "The first novel in the Harry Potter series and Rowling's debut novel, it follows Harry Potter, a young wizard who discovers his magical heritage on his eleventh birthday, when he receives a letter of acceptance to Hogwarts School of Witchcraft and Wizardry.", ImageId = 9, ISBN = 9780590353403, Rating = 5 },
                new Books { Id = 2, UserId = "0cgg7156-dee2-fe6g-4df6-147f5836e357", Title = "Harry Potter and The Deathly Hallows", BookSeries = "Harry Potter", Published = DateTime.Parse("2007-07-21"), Description = "Harry Potter is a wizard...", ImageId = 10, ISBN = 9780545029377, Rating = 5 },
                new Books { Id = 3, UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", Title = "Witcher: The Last Wish", BookSeries = "Witcher", Published = DateTime.Parse("1993-01-01"), Description = "Short stories - Geralt of Rivia is a mutated monsterhunter...", ImageId = 14, ISBN = 9781473231061, Rating = 5 },
                new Books { Id = 4, UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", Title = "Witcher: Sword of Destiny", BookSeries = "Witcher", Published = DateTime.Parse("1992-01-01"), Description = "Short stories - Geralt of Rivia is a mutated monsterhunter...", ImageId = 1, ISBN = 978147323108, Rating = 5 },
                new Books { Id = 5, UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", Title = "Witcher: Lady of the Lake", BookSeries = "Witcher", Published = DateTime.Parse("1999-01-01"), Description = "Geralt of Rivia is a mutated monsterhunter...", ImageId = 2, ISBN = 9781473231122, Rating = 5 },
                new Books { Id = 6, UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", Title = "Fifthy Shades of Grey", BookSeries = "Fifthy Shades of Grey", Published = DateTime.Parse("2012-04-17"), Description = "Mr Grey is a rich dude with kinky stuff going on...", ImageId = 3, ISBN = 9780345803481, Rating = 5 },
                new Books { Id = 7, UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", Title = "The Outsider", BookSeries = "The Outsider", Published = DateTime.Parse("2018-05-22"), Description = "An eleven-year-old boy’s violated corpse is discovered in a town park...", ImageId = 5, ISBN = 9781501180989, Rating = 5 },
                new Books { Id = 8, UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", Title = "American Psycho", BookSeries = "American Psycho", Published = DateTime.Parse("1991-03-06"), Description = "Patrick Bateman is Harvard-educated and intelligent. He works by day on Wall Street, earning a fortune to complement the one he was born with. His nights he spends in ways we cannot begin to fathom - doing impermissible things to women. He is living his own American Dream....", ImageId = 6, ISBN = 9780330484770, Rating = 5 },
                new Books { Id = 9, UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", Title = "Dune", BookSeries = "Dune", Published = DateTime.Parse("1965-08-01"), Description = "Dune is the story of the boy Paul Atreides, who would become the mysterious man...", ImageId = 7, ISBN = 9789178934751, Rating = 5 },
                new Books { Id = 10, UserId = "85e508e4-80ce-9d68-84c3-g1gd726e97g1", Title = "The Great Gatsby", BookSeries = "The Great Gatsby", Published = DateTime.Parse("1925-04-10"), Description = "Party hard...", ImageId = 8, ISBN = 9780020198819, Rating = 5 },
                new Books { Id = 11, UserId = "85e508e4-80ce-9d68-84c3-g1gd726e97g1", Title = "Jaws", BookSeries = "Jaws", Published = DateTime.Parse("1974-02-01"), Description = "Hungry Shark...", ImageId = 12, ISBN = 9780385047715, Rating = 5 },
                new Books { Id = 12, UserId = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5", Title = "The Godfather", BookSeries = "The Godfather", Published = DateTime.Parse("1969-03-10"), Description = "Gangsters...", ImageId = 13, ISBN = 9780434604913, Rating = 5 },
                new Books { Id = 13, UserId = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5", Title = "Lord of the Rings", BookSeries = "Lord of the Rings", Published = DateTime.Parse("1955-10-20"), Description = "The hobbit Frodo Baggins takes it upon himself to destroy the ring of power...", ImageId = 15, ISBN = 9780007136575, Rating = 5 }
                );
        }


        public static void SeedAuthorBooks(this ModelBuilder builder)
        {
            builder.Entity<AuthorsBooks>().HasData(
                new AuthorsBooks { BooksId = 1, AuthorsId = 1 },
                new AuthorsBooks { BooksId = 2, AuthorsId = 1 },
                new AuthorsBooks { BooksId = 3, AuthorsId = 2 },
                new AuthorsBooks { BooksId = 4, AuthorsId = 2 },
                new AuthorsBooks { BooksId = 5, AuthorsId = 2 },
                new AuthorsBooks { BooksId = 6, AuthorsId = 3 },
                new AuthorsBooks { BooksId = 7, AuthorsId = 4 },
                new AuthorsBooks { BooksId = 8, AuthorsId = 5 },
                new AuthorsBooks { BooksId = 9, AuthorsId = 6 },
                new AuthorsBooks { BooksId = 10, AuthorsId = 7 },
                new AuthorsBooks { BooksId = 11, AuthorsId = 8 },
                new AuthorsBooks { BooksId = 12, AuthorsId = 9 },
                new AuthorsBooks { BooksId = 13, AuthorsId = 10 }

                );
        }

        public static void SeedBooksGenres(this ModelBuilder builder)
        {
            builder.Entity<BooksGenres>().HasData(
                new BooksGenres { BooksId = 1, GenresId = 5 },
                new BooksGenres { BooksId = 1, GenresId = 8 },
                new BooksGenres { BooksId = 2, GenresId = 5 },
                new BooksGenres { BooksId = 2, GenresId = 8 },
                new BooksGenres { BooksId = 3, GenresId = 5 },
                new BooksGenres { BooksId = 4, GenresId = 5 },
                new BooksGenres { BooksId = 5, GenresId = 5 },
                new BooksGenres { BooksId = 6, GenresId = 3 },
                new BooksGenres { BooksId = 7, GenresId = 2 },
                new BooksGenres { BooksId = 7, GenresId = 9 },
                new BooksGenres { BooksId = 7, GenresId = 1 },
                new BooksGenres { BooksId = 8, GenresId = 2 },
                new BooksGenres { BooksId = 8, GenresId = 9 },
                new BooksGenres { BooksId = 9, GenresId = 4 },
                new BooksGenres { BooksId = 9, GenresId = 8 },
                new BooksGenres { BooksId = 9, GenresId = 5 },
                new BooksGenres { BooksId = 10, GenresId = 11 },
                new BooksGenres { BooksId = 11, GenresId = 11 },
                new BooksGenres { BooksId = 11, GenresId = 2 },
                new BooksGenres { BooksId = 11, GenresId = 9 },
                new BooksGenres { BooksId = 12, GenresId = 1 },
                new BooksGenres { BooksId = 12, GenresId = 2 },
                new BooksGenres { BooksId = 13, GenresId = 5 },
                new BooksGenres { BooksId = 13, GenresId = 8 }
                );
        }

        public static void SeedBookReviews(this ModelBuilder builder)
        {
            builder.Entity<Reviews>().HasData(
                new Reviews
                {
                    Id = 1,
                    BookId = 1, //Harry Potter & The Sorcerer's Stone
                    UserId = "0cgg7156-dee2-fe6g-4df6-147f5836e357", //Karl
                    Title = "The book was magical!",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 4
                },
                new Reviews
                {
                    Id = 2,
                    BookId = 2, //Harry Potter & The Deathly Hallows
                    UserId = "0cgg7156-dee2-fe6g-4df6-147f5836e357", //Karl
                    Title = "I never stopped reading",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 5
                },
                new Reviews
                {
                    Id = 3,
                    BookId = 2, //Harry Potter & The Deathly Hallows
                    UserId = "85e508e4-80ce-9d68-84c3-g1gd726e97g1", //Anna
                    Title = "Boring book",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 1
                },
                new Reviews
                {
                    Id = 4,
                    BookId = 3, //Witcher: The Last Wish
                    UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", //Per
                    Title = "Read this book if you like to be mindblown",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 4
                },
                new Reviews
                {
                    Id = 5,
                    BookId = 4, //Witcher: Sword of Destiny
                    UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", //Per
                    Title = "The shortstories are something else",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 4
                },
                new Reviews
                {
                    Id = 6,
                    BookId = 5, //Witcher: Lady of the Lake
                    UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", //Per
                    Title = "Meh.. Was kind of disapoitend",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 2
                },
                new Reviews
                {
                    Id = 7,
                    BookId = 6, //Fifty Shades of Grey
                    UserId = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5", //Berit
                    Title = "Mr Grey.. What a man",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 4
                },
                new Reviews
                {
                    Id = 8,
                    BookId = 7, //The Outsider
                    UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", //Per
                    Title = "Loved it!",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 4
                },
                new Reviews
                {
                    Id = 9,
                    BookId = 8, //American Psyco
                    UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", //Mats
                    Title = "The movie was better",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 3
                },
                new Reviews
                {
                    Id = 10,
                    BookId = 9, //Dune
                    UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", //Mats
                    Title = "Amazing, just amazing...",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 5
                },
                new Reviews
                {
                    Id = 11,
                    BookId = 10, //The Great Gatsby
                    UserId = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5", //Berit
                    Title = "Don't bother reading, it sucks (my opinion)",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 1
                },
                new Reviews
                {
                    Id = 12,
                    BookId = 11, //Jaws
                    UserId = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5", //Berit
                    Title = "Well, this was a waste of time",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 2
                },
                new Reviews
                {
                    Id = 13,
                    BookId = 12, //The Godfather
                    UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", //Mats
                    Title = "Wow.. just wow",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 4
                },
                new Reviews
                {
                    Id = 14,
                    BookId = 13, //Lord of the Rings
                    UserId = "0cgg7156-dee2-fe6g-4df6-147f5836e357", //Karl
                    Title = "If you love the movies you will love this book",
                    Text = "Lorem ipsum dolor sit amet. Et cupiditate praesentium qui odit recusandae ut beatae quis et adipisci ipsa et maxime quia? Non voluptas libero est beatae velit et quam inventore quo eveniet quos. Et ratione consequatur quo culpa veritatis At corrupti omnis?Est quibusdam quos et illo doloribus ut quis veritatis At placeat esse aut eligendi omnis id consequatur recusandae ? Sit repellendus quae cum atque voluptatem eum odit error est assumenda necessitatibus est molestiae deserunt in illum repellat et facere beatae. Sed beatae omnis qui iure facilis id dicta dolorem ut dolore esse a perspiciatis totam.Ea expedita nesciunt aut vero aliquid sed voluptatem quisquam aut iusto molestias et enim Quis.",
                    Rating = 5
                }
            );
        }

        public static void SeedReviewComments(this ModelBuilder builder)
        {
            builder.Entity<Comments>().HasData(
                new Comments
                {
                    Id = 1,
                    ReviewsId = 1, //Harry Potter & The Sorcerer's Stone: "The book was magical!" -Karl
                    UserId = "85e508e4-80ce-9d68-84c3-g1gd726e97g1", //Anna
                    Comment = "I couldn't agree more!"
                },
                new Comments
                {
                    Id = 2,
                    ReviewsId = 1, //Harry Potter & The Sorcerer's Stone: "The book was magical!" -Karl
                    UserId = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5", //Berit
                    Comment = "I thought the book was boring..."
                },
                new Comments
                {
                    Id = 3,
                    ReviewsId = 7, //Fifty Shades of Grey: "Mr Grey.. What a man" -Berit
                    UserId = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5", //Berit
                    Comment = "Yeah"
                });
        }

        public static void SeedCommentReplies(this ModelBuilder builder)
        {
            builder.Entity<Replies>().HasData(
                new Replies
                {
                    Id = 1,
                    CommentsId = 1, //Harry Potter & The Sorcerer's Stone: "The book was magical!" -Karl
                    UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", //Mats
                    Reply = "Agree!"
                },
                new Replies
                {
                    Id = 2,
                    CommentsId = 2, //Harry Potter & The Sorcerer's Stone: "The book was magical!" -Karl
                    UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d", //Per
                    Reply = "Yeah, didn´t like it "
                },
                new Replies
                {
                    Id = 3,
                    CommentsId = 3, //Fifty Shades of Grey: "Mr Grey.. What a man" -Berit, "
                    UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", //Mats
                    Reply = "Really...?"
                });
        }

        public static void SeedFlaggedTable(this ModelBuilder builder)
        {
            builder.Entity<Flagged>().HasData(
                new Flagged { Id = 1, IsFlagged = false },
                new Flagged { Id = 2, IsFlagged = true }
                );
        }
        public static void SeedFlaggedReviews(this ModelBuilder builder)
        {
            builder.Entity<FlaggedReviews>().HasData(
                new FlaggedReviews { UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f", FlaggedId = 2, ReviewId = 1 },
                new FlaggedReviews { UserId = "0cgg7156-dee2-fe6g-4df6-147f5836e357", FlaggedId = 2, ReviewId = 2 },
                new FlaggedReviews { UserId = "0cgg7156-dee2-fe6g-4df6-147f5836e357", FlaggedId = 2, ReviewId = 3 }
              
                );
        }

        public static byte[] ReadFile(string imageFileName) // Get images from filepath
        {
            string sPath = "../CoolBooks_NinjaExperts/wwwroot/img/CoolBooksImages/";
            sPath = sPath + imageFileName;
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }
        public static byte[] SetThumbnail(byte[] imgFile)
        {

            MemoryStream ms = new MemoryStream(imgFile);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            // convert img to thumbnail
            var thumbimg = image.GetThumbnailImage(128, 200, new System.Drawing.Image.GetThumbnailImageAbort(() => false), IntPtr.Zero);


            // convert to byte[]
            using (var ms2 = new MemoryStream())
            {
                thumbimg.Save(ms2, image.RawFormat);
                return ms2.ToArray();
            }
        }


        public static void SeedUsers(this ModelBuilder builder)
        {
            //Admin
            UserInfo admin = new UserInfo()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                FirstName = "Default",
                LastName = "Adminsson",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                //LockoutEnabled = false,
                PhoneNumber = "1234567890",
                Created = DateTime.Now
            };

            //Moderator
            UserInfo mod = new UserInfo()
            {
                Id = "803efbbg-d6c9-83f9-ee5f-f8a0fbd5d06b",
                FirstName = "Default",
                LastName = "Modsson",
                UserName = "Moderator",
                NormalizedUserName = "MODERATOR",
                Email = "moderator@gmail.com",
                NormalizedEmail = "MODERATOR@GMAIL.COM",
                //LockoutEnabled = false,
                PhoneNumber = "1234567890",
                Created = DateTime.Now
            };

            //User
            UserInfo user = new UserInfo()
            {
                Id = "ag0265e7-f68f-6ge5-6445-2a3ac7037929",
                FirstName = "Default",
                LastName = "Usersson",
                UserName = "User",
                NormalizedUserName = "USER",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                //LockoutEnabled = false,
                PhoneNumber = "1234567890",
                Created = DateTime.Now
            };

            UserInfo user1 = new UserInfo()
            {
                Id = "0cgg7156-dee2-fe6g-4df6-147f5836e357",
                FirstName = "Karl",
                LastName = "Karlsson",
                UserName = "Karl",
                NormalizedUserName = "KARL",
                Email = "karl@gmail.com",
                NormalizedEmail = "KARL@GMAIL.COM",
                //LockoutEnabled = false,
                PhoneNumber = "1234567899",
                Created = DateTime.Now
            };
            UserInfo user2 = new UserInfo()
            {
                Id = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d",
                FirstName = "Per",
                LastName = "Persson",
                UserName = "Per",
                NormalizedUserName = "PER",
                Email = "per@gmail.com",
                NormalizedEmail = "PER@GMAIL.COM",
                //LockoutEnabled = false,
                PhoneNumber = "1234567889",
                Created = DateTime.Now
            };
            UserInfo user3 = new UserInfo()
            {
                Id = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f",
                FirstName = "Mats",
                LastName = "Matsson",
                UserName = "Mats",
                NormalizedUserName = "MATS",
                Email = "mats@gmail.com",
                NormalizedEmail = "MATS@GMAIL.COM",
                //LockoutEnabled = false,
                PhoneNumber = "1234567789",
                Created = DateTime.Now
            };
            UserInfo user4 = new UserInfo()
            {
                Id = "85e508e4-80ce-9d68-84c3-g1gd726e97g1",
                FirstName = "Anna",
                LastName = "Annasson",
                UserName = "Anna",
                NormalizedUserName = "ANNA",
                Email = "anna@gmail.com",
                NormalizedEmail = "ANNA@GMAIL.COM",
                //LockoutEnabled = false,
                PhoneNumber = "1234566789",
                Created = DateTime.Now
            };
            UserInfo user5 = new UserInfo()
            {
                Id = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5",
                FirstName = "Berit",
                LastName = "Beritsson",
                UserName = "Berit",
                NormalizedUserName = "BERIT",
                Email = "berit@gmail.com",
                NormalizedEmail = "BERIT@GMAIL.COM",
                //LockoutEnabled = false,
                PhoneNumber = "1234556789",
                Created = DateTime.Now
            };

            PasswordHasher<UserInfo> passwordHasher = new PasswordHasher<UserInfo>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin*123");
            mod.PasswordHash = passwordHasher.HashPassword(mod, "Mod*123");
            user.PasswordHash = passwordHasher.HashPassword(user, "User*123");
            user1.PasswordHash = passwordHasher.HashPassword(user1, "Karl*123");
            user2.PasswordHash = passwordHasher.HashPassword(user2, "Per*123");
            user3.PasswordHash = passwordHasher.HashPassword(user3, "Mats*123");
            user4.PasswordHash = passwordHasher.HashPassword(user4, "Anna*123");
            user5.PasswordHash = passwordHasher.HashPassword(user5, "Berit*123");

            builder.Entity<UserInfo>().HasData(admin);
            builder.Entity<UserInfo>().HasData(mod);
            builder.Entity<UserInfo>().HasData(user);
            builder.Entity<UserInfo>().HasData(user1);
            builder.Entity<UserInfo>().HasData(user2);
            builder.Entity<UserInfo>().HasData(user3);
            builder.Entity<UserInfo>().HasData(user4);
            builder.Entity<UserInfo>().HasData(user5);
        }

        public static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "Moderator", NormalizedName="MODERATOR" },
                new IdentityRole() { Id = "g4a251c5-6285-9362-def3-d722e74c5378", Name = "User", NormalizedName="USER" }
                );
        }

        public static void SeedUserRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }, //Admin
                new IdentityUserRole<string>() { RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330", UserId = "803efbbg-d6c9-83f9-ee5f-f8a0fbd5d06b" }, //Moderator
                new IdentityUserRole<string>() { RoleId = "g4a251c5-6285-9362-def3-d722e74c5378", UserId = "ag0265e7-f68f-6ge5-6445-2a3ac7037929" },  //User
                new IdentityUserRole<string>() { RoleId = "g4a251c5-6285-9362-def3-d722e74c5378", UserId = "0cgg7156-dee2-fe6g-4df6-147f5836e357" },
                new IdentityUserRole<string>() { RoleId = "g4a251c5-6285-9362-def3-d722e74c5378", UserId = "be661gc4-d6b6-3b1d-23d4-2485c1bf5c0d" },
                new IdentityUserRole<string>() { RoleId = "g4a251c5-6285-9362-def3-d722e74c5378", UserId = "b1ae3gdb-2bba-8ea7-0eb6-12e3g1gba51f" },
                new IdentityUserRole<string>() { RoleId = "g4a251c5-6285-9362-def3-d722e74c5378", UserId = "85e508e4-80ce-9d68-84c3-g1gd726e97g1" },
                new IdentityUserRole<string>() { RoleId = "g4a251c5-6285-9362-def3-d722e74c5378", UserId = "bd332a8c-b4e4-5cg1-b68d-g25g3cg8dgc5" }                
                );
        }
    }
}
