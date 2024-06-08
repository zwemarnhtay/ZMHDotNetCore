console.log("hello");

const storageKey = "blogs";
let blogId = null;

loadBlogTable();

$('#btnSave').on("click", function () {
    const title = $('#txtTitle').val();
    const content = $('#txtContent').val();
    const author = $('#txtAuthor').val();

    if (blogId == null) {
        createBlog(title, content, author);
    } else {
        updateBlog(blogId, title, content, author);
        blogId = null;
    }

});

function createBlog(title, content, author) {

    if (title == "" || content == "" || author == "") return errorMsg("fill the blank");
    let list = getBlogs();

    const reqModel = {
        id: uuidv4(),
        title: title,
        content: content,
        author: author
    };

    list.push(reqModel);

    const jsonBlog = JSON.stringify(list);
    localStorage.setItem(storageKey, jsonBlog);

    successMsg("created success");
    reloadForm();
    loadBlogTable();
}

function getBlogs() {
    let blogs = localStorage.getItem(storageKey);

    console.log(blogs);

    let list = [];
    if (blogs !== null) {
        list = JSON.parse(blogs);
    }
    console.log(list);
    return list;
}

function loadBlogTable() {
    let list = getBlogs();
    let table = '';
    let count = 0;
    list.forEach(blog => {
        table += `<tr>
                    <td>
                        <button type="button" class="btn btn-warning" onclick="editBlog('${blog.id}')">Edit</button>
                        <button type="button" class="btn btn-danger" onclick="deleteBlog('${blog.id}')">Delete</button>
                    </td>
                    <td>${++count}</td>
                    <td>${blog.title}</td>
                    <td>${blog.author}</td>
                    <td>${blog.content}</td>
                </tr>`;
    });

    $('#tbody').html(table);
}

function editBlog(id) {

    let blog = filterBlog(id);
    if (blog == null) return;

    blogId = blog.id;
    $('#txtTitle').val(blog.title);
    $('#txtAuthor').val(blog.author);
    $('#txtContent').val(blog.content);
    $('#txtTitle').focus();
}

function updateBlog(id, title, content, author) {
    if (title == "" || content == "" || author == "") return errorMsg("fill the blank");

    let blog = filterBlog(id);
    if (blog == null) return;

    blog.title = title;
    blog.content = content;
    blog.author = author;

    let list = getBlogs();
    let index = list.findIndex(x => x.id === id);
    list[index] = blog;

    const jsonBlog = JSON.stringify(list);
    localStorage.setItem(storageKey, jsonBlog);

    successMsg("updated success");
}

function deleteBlog(id) {
    // Swal.fire({
    //     title: "Are you sure?",
    //     text: "You won't be able to revert this!",
    //     icon: "warning",
    //     showCancelButton: true,
    //     confirmButtonColor: "#3085d6",
    //     cancelButtonColor: "#d33",
    //     confirmButtonText: "Yes, delete it!"
    // }).then((result) => {
    //     if (!result.isConfirmed) return;

    //     let blog = filterBlog(id);
    //     if (blog == null) return;

    //     let list = getBlogs();
    //     list = list.filter(x => x.id !== blog.id);

    //     const jsonBlog = JSON.stringify(list);
    //     localStorage.setItem(storageKey, jsonBlog);

    //     successMsg("deleted success");

    //     loadBlogTable();
    // });

    confirmMsg("Are you sure to delete this").then(
        function (value) {
            let blog = filterBlog(id);
            if (blog == null) return;

            let list = getBlogs();
            list = list.filter(x => x.id !== blog.id);

            const jsonBlog = JSON.stringify(list);
            localStorage.setItem(storageKey, jsonBlog);

            successMsg("deleted success");

            loadBlogTable();
        }
    )
}

function filterBlog(id) {
    let list = getBlogs();
    let item = list.filter(x => x.id == id);
    console.log(item);

    if (item.length == 0) {
        errorMsg("no data found");
        return null;
    }

    let blog = item[0]; //array to obj
    console.log(blog);
    return blog;
}

function reloadForm() {
    $('#txtTitle').val('');
    $('#txtContent').val('');
    $('#txtAuthor').val('');
    $('#txtTitle').focus();
}