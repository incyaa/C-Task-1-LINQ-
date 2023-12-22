namespace Task__1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student { StudentId = 1, Name = "Иванов", Age = 20 },
                new Student { StudentId = 2, Name = "Петров", Age = 22 },
                new Student { StudentId = 3, Name = "Сидоров", Age = 21 }
            };
            List<Enrollment> enrollments = new List<Enrollment>
            {
                new Enrollment { StudentId = 1, Course = "Математика" },
                new Enrollment { StudentId = 1, Course = "Физика" },
                new Enrollment { StudentId = 2, Course = "Химия" },
                new Enrollment { StudentId = 3, Course = "Математика" },
                new Enrollment { StudentId = 3, Course = "Физика" }
            };
            List<Grade> grades = new List<Grade>
            {
                new Grade { StudentId = 1, Course = "Математика", Score = 80.0 },
                new Grade { StudentId = 1, Course = "Физика", Score = 75.5 },
                new Grade { StudentId = 2, Course = "Химия", Score = 88.9 },
                new Grade { StudentId = 3, Course = "Математика", Score = 95.2 },
                new Grade { StudentId = 3, Course = "Физика", Score = 88.0 }
            };
            var ChangedStud = grades.GroupBy(g => g.StudentId, (g) => new
            {
                g.Course,
                Score = g.Score
            }).Join(students, eg => eg.Key, s => s.StudentId, (eg, s) => new
            {
                ID = s.StudentId,
                Name = s.Name,
                Age = s.Age,
                grades = eg.ToList()
            });
            var plus21 = ChangedStud.Where(s => s.Age > 21).ToList();
            foreach (var student in ChangedStud)
            {
                Console.WriteLine("Name: {0} \n\t\tCourse: {1} \n\t\tAvarage: {2}",
                                                                student.Name,
                                                                String.Join(",\n\t\t\t", student.grades.Select(g => g.Course).ToList()),
                                                                student.grades.Select(g => g.Score).ToList().Average()
                                                                );
            }
            var GenInfo = grades.GroupBy(gb => gb.Course).Select(gb => new
            {
                Name = gb.Key,
                AvarageCourse = gb.Select(g => g.Score).ToList().Sum() / 2,
                CountStud = gb.Select(g => g.StudentId).ToList().Count
            }
            );
            foreach (var item in GenInfo)
            {
                Console.WriteLine("Name Course: {0}\tAvarage Grade: {1}\tCount Students: {2}", item.Name, item.AvarageCourse, item.CountStud);
            }
        }

    }
}