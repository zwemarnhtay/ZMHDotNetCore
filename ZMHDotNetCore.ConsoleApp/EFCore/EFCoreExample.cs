using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMHDotNetCore.ConsoleApp.DTO;

namespace ZMHDotNetCore.ConsoleApp.EFCore
{
    public class EFCoreExample
    {
        private readonly AppDBContext db;

        public EFCoreExample(AppDBContext db)
        {
            this.db = db;
        }

        public void run()
        {
            // find(11);
            // delete(9);
            // update(11, "yoyo", "Mr.Y", "Blahblahblah");
            //create("new Title", "Yotj", "new content");
            read();
        }

        private void read()
        {
            //AppDBContext db = new AppDBContext();
            var list = db.Blogs.ToList();

            foreach (var item in list)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-------->>>");
            }
        }

        private void find(int id)
        {
            //AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item == null)
            {
                Console.WriteLine("not found!");
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-------->>>");
        }

        private void create(string title, string author, string content)
        {
           // AppDBContext db = new AppDBContext();

            var item = new BlogDTO { BlogTitle = title, BlogAuthor = author, BlogContent = content };

            db.Blogs.Add(item);
            int result = db.SaveChanges();
            string msg = result > 0 ? "create success \n" : "failed \n";
            Console.WriteLine(msg);
        }

        private void update(int id, string title, string author, string content)
        {
            //AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("not found! \n");
            }

            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = db.SaveChanges();
            string msg = result > 0 ? "updated success \n" : "failed \n";
            Console.WriteLine(msg);
        }

        private void delete(int id)
        {
            //AppDBContext db = new AppDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item is null)
            {
                Console.WriteLine("no data found \n");
            }

            db.Blogs.Remove(item);
            int result = db.SaveChanges();
            string msg = result > 0 ? "deleted success \n" : "failed \n";
            Console.WriteLine(msg);
        }
    }
}
