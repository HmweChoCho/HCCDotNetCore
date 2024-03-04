const tblBlog = "Tbl_Blog";
runBlog();

function runBlog() {
  createBlog("test title", "test author", "test content");
  readBlog();
  editBlog("6e0077aa-a26c-463a-a9a0-95f121febb25");
  editBlog("1");
  updateBlog(
    "6e0077aa-a26c-463a-a9a0-95f121febb25",
    "test 1",
    "test 2",
    "test 3"
  );
  // let id = prompt("Enter Id");
  // deleteBlog(id);

  const id = prompt("Enter ID");
  const title = prompt("Enter Title");
  const author = prompt("Enter Author");
  const content = prompt("Enter Content");
  updateBlog(id, title, author, content);
}

function readBlog() {
  let lstBlogs = getBlogs();
  for (let i = 0; i < lstBlogs.length; i++) {
    const item = lstBlogs[i];
    console.log(item.Id);
    console.log(item.Title);
    console.log(item.Author);
    console.log(item.Content);
  }
}

function editBlog(id) {
  var lstBlog = getBlogs();
  var lst = lstBlog.filter((x) => x.Id == id);
  if (lst.length == 0) {
    console.log("No Data Found....");
    return;
  }
  let item = lst[0];
  console.log(item.Id);
  console.log(item.Title);
  console.log(item.Author);
  console.log(item.Content);
}

function createBlog(title, author, content) {
  let lstBlogs = getBlogs();
  const blog = {
    Id: uuidv4(),
    Title: title,
    Author: author,
    Content: content,
  };
  lstBlogs.push(blog);
  // let jsonStr = JSON.stringify(lstBlogs);
  // localStorage.setItem(tblBlog, jsonStr);
  setLocalStorage(lstBlogs);
}

function updateBlog(id, title, author, content) {
  var lstBlog = getBlogs();
  var lst = lstBlog.filter((x) => x.Id == id);
  console.log(lst);
  if (lst.length == 0) {
    console.log("No Data Found....");
    return;
  }
  let index = lstBlog.findIndex((x) => x.Id == id);
  lstBlog[index] = {
    Id: id,
    Title: title,
    Author: author,
    Content: content,
  };
  setLocalStorage(lstBlog);
}

function deleteBlog(id) {
  var lstBlog = getBlogs();
  var lst = lstBlog.filter((x) => x.Id == id);
  if (lst.length == 0) {
    console.log("No Data Found....");
    return;
  }
  let item = lst[0];
  lstBlog = lstBlog.filter((x) => x.Id != id);
  setLocalStorage(lstBlog);
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
    ).toString(16)
  );
}

function getBlogs() {
  let lstBlog = [];
  let blogStr = localStorage.getItem(tblBlog);
  if (blogStr != null) {
    lstBlog = JSON.parse(blogStr);
  }
  return lstBlog;
}

function setLocalStorage(blogs) {
  let jsonStr = JSON.stringify(blogs);
  localStorage.setItem(tblBlog, jsonStr);
}
