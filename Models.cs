namespace EntityFrameworkUppgift;
using Microsoft.EntityFrameworkCore;
using System;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public BloggingContext()
    {
        var folder = Environment.CurrentDirectory;
        DbPath = System.IO.Path.Join(folder, "cs.forts-BennyAhlin.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Blog
{
    public int BlogId { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }

    public List<Post> Posts { get; set; } = new List<Post>();

}

public class User
{
    public int UserId { get; set; }         
    public string? Username { get; set; }
    public string? Password { private get; set; }

    public List<Post> Posts { get; set; } = new List<Post>();

}

public class Post
{
    public int PostId { get; set; }                 
    public string? Title { get; set; }              
    public string? Content { get; set; }            
    public DateOnly PublishedOn { get; set; }       

    public int BlogId { get; set; }
    public Blog? Blog { get; set; }                 

    public int UserId { get; set; }                    
    public User? User { get; set; }  
    
}