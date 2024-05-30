using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMHDotNetCore.WinFormsApp.Queries
{
    internal class BlogQuery
    {
        public static string InsertQuery { get; } = @"INSERT INTO [dbo].[Tbl_Blog]
                                                    ([BlogTitle]
                                                    ,[BlogAuthor]
                                                    ,[BlogContent])
                                                     VALUES (@BlogTitle
                                                    ,@BlogAuthor       
                                                    ,@BlogContent)";

        public static string GetListQuery { get; } = @"SELECT [BlogId], 
                                            [BlogTitle], 
                                            [BlogContent],
                                            [BlogAuthor], FROM [dbo].[Tbl_Blog]";

        public static string UpdateQuery { get; } = @"UPDATE [dbo].[Tbl_Blog]
                                                SET [BlogTitle] = @BlogTitle
                                                ,[BlogAuthor] = @BlogAuthor
                                                ,[BlogContent] = @BlogContent
                                                WHERE BlogId = @BlogId";

        public static string DeleteQuery { get; } = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
    }
}
