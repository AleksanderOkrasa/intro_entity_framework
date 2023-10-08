using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }

    public string DbPath { get; }

    public SchoolContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "school.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }

    public List<Course> Courses { get; } = new();
}

public class Teacher
{
    public int TeacherId { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }

    public List<Course> Courses { get; } = new();
}

public class Course
{
    public int CourseId { get; set; }
    public string Name { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public List<Student> Students { get; } = new();
}
