using System;
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
                int choice;
                do
                {
                    Console.WriteLine("\nWybierz opcję:");
                    Console.WriteLine("1 - Pokaż studentów");
                    Console.WriteLine("2 - Pokaż nauczycieli");
                    Console.WriteLine("3 - Dodaj studenta");
                    Console.WriteLine("4 - Dodaj nauczyciela");
                    Console.WriteLine("5 - Usuń studenta");
                    Console.WriteLine("6 - Dodaj kurs");
                    Console.WriteLine("7 - Aktualizuj kurs");
                    Console.WriteLine("8 - Pokaż kursy");
                    Console.WriteLine("9 - Pokaż szczegóły kursu");
                    Console.WriteLine("0 - Wyjście");
                    try
                    {
                        choice = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (FormatException) 
                    {
                        Console.WriteLine("Wybór opcji jest sterowany cyframi!");
                        choice = 99;
                        continue;
                    }
                    switch (choice)
                    {
                        case 1:
                            ShowStudents();
                            break;
                        case 2:
                            ShowTeachers();
                            break;
                        case 3:
                            AddStudent();
                            break;
                        case 4:
                            AddTeacher();
                            break;
                        case 5:
                            DeleteStudent();
                            break;
                        case 6:
                            AddCourse();
                            break;
                        case 7:
                            UpdateCourse();
                            break;
                        case 8:
                            ShowCourses();
                            break;
                        case 9:
                            ShowCourseDetail();
                            break;
                        case 0:
                            Console.WriteLine("Zamykanie programu...");
                            break;
                        default:
                            Console.WriteLine("Nieznana opcja");
                            break;
                    }
                }
                while (choice != 0);




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
                    Console.WriteLine("Studenci:");
                    var students = db.Students.ToList();
                    foreach (var student in students)
                    {
                        Console.WriteLine(student.StudentId + ". " + student.Name);
                    }
                }

                void DeleteStudent()
                {
                    ShowStudents();
                    Console.WriteLine("Podaj ID studenta do usunięcia:");
                    int studentIdToDelete = Convert.ToInt32(Console.ReadLine());
                    var studentToDelete = db.Students.Find(studentIdToDelete);
                    if (studentToDelete != null)
                    {
                        db.Students.Remove(studentToDelete);
                        db.SaveChanges();
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
                    ShowCourses();
                    Console.WriteLine("Podaj ID kursu do wyświetlenia szczegółów:");
                    int courseIdToFind = Convert.ToInt32(Console.ReadLine());
                    var courseToShowDetail = db.Courses.Find(courseIdToFind);
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

                void UpdateCourse()
                {
                    ShowCourses();
                    Console.WriteLine("Podaj ID kursu do aktualizacji:");
                    int courseIdToUpdate = Convert.ToInt32(Console.ReadLine());
                    var courseToUpdate = db.Courses.Find(courseIdToUpdate);
                    if (courseToUpdate != null)
                    {
                        Console.WriteLine("Podaj nową nazwę kursu (lub zostaw puste):");
                        string newName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newName))
                        {
                            courseToUpdate.Name = newName;
                        }

                        ShowTeachers();
                        Console.WriteLine("Podaj ID nauczyciela prowadzącego kursu (lub zostaw puste):");
                        try
                        {
                            int teacherId = Convert.ToInt32(Console.ReadLine());
                            var teacherToUpdate = db.Teachers.Find(teacherId);

                            if (teacherToUpdate != null)
                            {
                                courseToUpdate.Teacher = teacherToUpdate;
                            }
                        }
                        catch (Exception ex) { }

                        ShowStudents();
                        Console.WriteLine("Podaj ID ucznia do dodania do kursu (możesz dodać kilku, oddzielając je przecinkami):");
                        string[] studentIds = Console.ReadLine().Split(',');
                        foreach (var id in studentIds)
                        {
                            int studentId = Convert.ToInt32(id);
                            var studentToAdd = db.Students.Find(studentId);
                            if (studentToAdd != null)
                            {
                                courseToUpdate.Students.Add(studentToAdd);
                            }
                        }

                        db.SaveChanges();
                    }
                }


            }
        }
    }
}
