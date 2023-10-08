﻿using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace SchoolApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SchoolContext())
            {
                /*
                ShowStudents();
                ShowTeachers();

                AddStudent();
                AddTeacher();

                ShowTeachers();
                ShowStudents();

                UpdateStudent();
                ShowStudents();

                DeleteStudent();
                ShowStudents();
                */
                AddCourse();
                ShowCourses();
                ShowCourseDetail();

                void AddCourse()
                {
                    Console.WriteLine("Podaj nazwę kursu:");
                    string courseName = Console.ReadLine();
                    ShowTeachers();
                    Console.WriteLine("Podaj id nauczyciela prowadzącego kurs " + courseName);
                    int teacherId = Convert.ToInt32(Console.ReadLine());
                    var teacherToAdd = db.Teachers.Find(teacherId);

                    var newCourse = new Course { Name = courseName, Teacher = teacherToAdd };

                    db.Courses.Add(newCourse);
                    db.SaveChanges();
                }
                
                void ShowCourses()
                {
                    Console.WriteLine("Kursy:");
                    var courses = db.Courses.ToList();
                    foreach (var course in courses)
                    {
                        Console.WriteLine(course.CourseId + ". " + course.Name);
                    }
                }

                void ShowCourseDetail()
                {
                    Console.WriteLine("Podaj ID kursu do wyświetlenia szczegółów:");
                    int courseIdToFind = Convert.ToInt32(Console.ReadLine());
                    var courseToShowDetail= db.Courses.Find(courseIdToFind);
                    if (courseToShowDetail != null)
                    {
                        Console.WriteLine("Nazwa: " + courseToShowDetail.Name);
                        Console.WriteLine("Nauczyciel: " + courseToShowDetail.Teacher.Name);
                        Console.WriteLine("Studenci:");
                        foreach (var student in courseToShowDetail.Students)
                        {
                            Console.WriteLine(student.StudentId + ". " + student.Name);
                        }
                            db.SaveChanges();
                    }
                }


                void AddTeacher()
                {
                    Console.WriteLine("Podaj imię i nazwisko nauczyciela:");
                    string teacherName = Console.ReadLine();
                    Console.WriteLine("Podaj nazwę przedmiotu (prowadzonego przez powyższego nauczyciela):");
                    string teacherSubject = Console.ReadLine();
                    var newTeacher = new Teacher { Name = teacherName, Subject = teacherSubject };
                    db.Teachers.Add(newTeacher);
                    db.SaveChanges();
                }

                void AddStudent()
                {
                    Console.WriteLine("Podaj imię i nazwisko nowego studenta:");
                    string studentName = Console.ReadLine();
                    var newStudent = new Student { Name = studentName };
                    db.Students.Add(newStudent);
                    db.SaveChanges();
                }

                void ShowStudents()
                {
                    // Odczytywanie i wyświetlanie wszystkich studentów
                    Console.WriteLine("Studenci:");
                    var students = db.Students.ToList();
                    foreach (var student in students)
                    {
                        Console.WriteLine(student.StudentId + ". " + student.Name);
                    }

                }
                void ShowTeachers()
                {
                    Console.WriteLine("Nauczyciele:");
                    var teachers = db.Teachers.ToList();
                    foreach (var teacher in teachers)
                    {
                        Console.WriteLine(teacher.TeacherId + ". " + teacher.Name + " -> " + teacher.Subject);;
                    }
                }
                void DeleteStudent()
                {
                    Console.WriteLine("Podaj ID studenta do usunięcia:");
                    int studentIdToDelete = Convert.ToInt32(Console.ReadLine());
                    var studentToDelete = db.Students.Find(studentIdToDelete);
                    if (studentToDelete != null)
                    {
                        db.Students.Remove(studentToDelete);
                        db.SaveChanges();
                    }
                }
                void UpdateStudent()
                {
                    Console.WriteLine("Podaj ID studenta do aktualizacji:");
                    int studentIdToUpdate = Convert.ToInt32(Console.ReadLine());
                    var studentToUpdate = db.Students.Find(studentIdToUpdate);
                    if (studentToUpdate != null)
                    {
                        Console.WriteLine("Podaj nowe imię i nazwisko:");
                        studentToUpdate.Name = Console.ReadLine();
                        db.SaveChanges();
                    }
                }

            }
        }
    }
}
