using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMHDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
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
            appDBContext db = new appDBContext();
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
            appDBContext db = new appDBContext();
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
            appDBContext db = new appDBContext();

            var item = new blogDTO { BlogTitle = title, BlogAuthor = author, BlogContent = content };

            db.Blogs.Add(item);
            int result = db.SaveChanges();
            string msg = result > 0 ? "create success \n" : "failed \n";
            Console.WriteLine(msg);
        }

        private void update(int id, string title, string author, string content)
        {
            appDBContext db = new appDBContext();
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if (item == null)
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
            appDBContext db = new appDBContext();  
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);

            if(item == null)
            {
                Console.WriteLine("no data found \n");
            }

            db.Remove(item);
            int result = db.SaveChanges();
            string msg = result > 0 ? "deleted success \n" : "failed \n";
            Console.WriteLine(msg);
        }
    }
}
