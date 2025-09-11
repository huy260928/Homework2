using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        List<Student> students = new List<Student>
        {
            new Student(1, "Nguyen Van A", 17),
            new Student(2, "Tran Thi B", 16),
            new Student(3, "Pham Dinh C", 18),
            new Student(4, "Le Van D", 15),
            new Student(5, "Do Thi E", 19),
            new Student(6, "Anh Tuan", 17),
            new Student(7, "Nguyen Minh An", 20)
        };
        Console.WriteLine("a. Toàn bộ danh sách học sinh:");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
        Console.WriteLine();
        var studentsByAge = students.Where(s => s.Age >= 15 && s.Age <= 18);
        Console.WriteLine("b. Danh sách học sinh có tuổi từ 15 đến 18:");
        foreach (var student in studentsByAge)
        {
            Console.WriteLine(student);
        }
        Console.WriteLine();
        var studentsWithNameA = students.Where(s => s.Name.StartsWith("A"));
        Console.WriteLine("c. Danh sách học sinh có tên bắt đầu bằng chữ 'A':");
        foreach (var student in studentsWithNameA)
        {
            Console.WriteLine(student);
        }
        Console.WriteLine();
        int totalAge = students.Sum(s => s.Age);
        Console.WriteLine($"d. Tổng tuổi của tất cả học sinh là: {totalAge}");
        Console.WriteLine();
        var oldestStudent = students.OrderByDescending(s => s.Age).FirstOrDefault();
        Console.WriteLine("e. Học sinh có tuổi lớn nhất:");
        Console.WriteLine(oldestStudent);
        Console.WriteLine();
        var sortedStudents = students.OrderBy(s => s.Age);
        Console.WriteLine("f. Danh sách học sinh được sắp xếp theo tuổi tăng dần:");
        foreach (var student in sortedStudents)
        {
            Console.WriteLine(student);
        }
    }
}