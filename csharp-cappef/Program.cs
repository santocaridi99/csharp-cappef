using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace csharp_cappef
{
    internal class Program
    {
        //public static System.Collections.Generic.IEnumerable<int> Power(int number)
        //{
        //    int result = 1;
        //    while (true)
        //    {
        //        result = result * number; //Potrei fare: GetNextFromDB();
        //        yield return result;
        //        //La prossima volta che chiamate la funzione, ripartite esattamente da questo punto

        //    }
        //}
        //public static System.Collections.Generic.IEnumerable<long> Genera(long n)
        //{
        //    long result = 1;
        //    while (n > 0)
        //    {
                
        //        yield return result++;
               

        //    }
        //}

        //public static Func<int> accumul(int number)
        //{  //Closure
        //    int result = 1;

        //    return () =>
        //    {
        //        result = result * number;
        //        return result;
        //    };
        //}
        static void Main(string[] args)
        {
            //contare tutti i numeri che contengono la cifra 2
            //compresi tra 1 e 10000000000

            //soluzione 1
            //long conta = 0;
            //for(long i=0; i < 1000000000; ++i)
            //{
            //    if (i.ToString().Contains('2'))
            //        conta++;

            //}
            //Console.WriteLine(conta);

            ////soluzione 2
            //long conta1 = 0;
            //long letti1 = 0;
            //foreach(int n in Genera(10000000)){
            //    if (n.ToString().Contains('2'))
            //    {
            //        conta1++;
            //    }
            //}
            //Console.WriteLine(conta1);

            ////stampare somma dei pari usando genera
            //Console.WriteLine(Genera(1000000).Where(n => n%2 ==0).Sum());



            Console.WriteLine("Ciao");
            using (SchoolContext db = new SchoolContext())
            {
                // Create
                Student nuovoStudente =
                    new Student { Name = "Francescooo" , Surname = "Cozzaaa2" , Email = "cicciooo@gmail.com" };
                db.Add(nuovoStudente);
                db.SaveChanges();

                // Read
                Console.WriteLine("Ottenere lista di Studenti");
                List<Student> students = db.Students
                   .OrderBy(student => student.Name).ToList<Student>();
            }
        }
    }

    [Table("student")]
    [Index(nameof(Email), IsUnique = true)]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Column("student_email")]
        public string Email { get; set; }

        public List<Course> FrequentedCourses { get; set; }
        public List<Review> Reviews { get; set; }
    }

    [Table("course")]
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; }

        public CourseImage CourseImage { get; set; }
        public List<Student> StudentsEnrolled { get; set; }
    }

    [Table("course_image")]
    public class CourseImage
    {
        [Key]
        public int CourseImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }

    [Table("review")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }

    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ContoUniversity;Integrated Security=True;Pooling=False");
        }
    }

}
