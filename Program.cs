using EntityFrameworkUppgift;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;


string[] UserCSV = File.ReadAllLines("User.csv");
string[] PostCSV = File.ReadAllLines("Post.csv");
string[] BlogCSV = File.ReadAllLines("Blog.csv");

using var db = new BloggingContext();
db.Database.EnsureDeleted();
db.SaveChanges();

db.Database.EnsureCreated();
db.SaveChanges();

foreach (string line in UserCSV)
{
    string[] split = line.Split(",");
    var alreadyExistCheck = db.Users.Find(int.Parse(split[0]));
    if (alreadyExistCheck == null)
    {
        db.Add(new User { UserId = int.Parse(split[0]), Username = split[1], Password = split[2] });
    }
}
db.SaveChanges();

foreach (string line in BlogCSV)
{
    string[] split = line.Split(",");
    var alreadyExistCheck = db.Blogs.Find(int.Parse(split[0]));
    if (alreadyExistCheck == null)
    {
        db.Add(new Blog { BlogId = int.Parse(split[0]), Url = split[1], Name = split[2] });
    }
}
db.SaveChanges();

foreach (string line in PostCSV)
{
    string[] split = line.Split(",");

    db.Add(new Post { Title = split[1], Content = split[2], PublishedOn = DateOnly.Parse(split[3]), BlogId = int.Parse(split[4]), UserId = int.Parse(split[5]) });
}
db.SaveChanges();

foreach (User user in db.Users.OrderBy(u => u.Username))
{

    Console.WriteLine("-------------------------------");
    Console.WriteLine();
    Console.WriteLine("\t" + user.Username);
    foreach (Post post in user.Posts.OrderBy(t => t.Title))
    {
        Console.WriteLine();
        Console.WriteLine(post.Title + "\t " + post.Content + "\t " + post.PublishedOn + " \t");
        Console.WriteLine(post.Blog?.Name);
        Console.WriteLine(post.Blog?.Url);
    }
    Console.WriteLine();

}
db.SaveChanges();





