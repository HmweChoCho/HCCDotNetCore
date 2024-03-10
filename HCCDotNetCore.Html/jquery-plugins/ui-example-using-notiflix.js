const tblBlog = "Tbl_Blog";
let _blogId = "";
runBlog();

function runBlog() {
  creatTalbe();
  // readBlog();
  //   createBlog("test title", "test author", "test content");
  //   readBlog();
  //   editBlog("6e0077aa-a26c-463a-a9a0-95f121febb25");
  //   editBlog("1");
  //   updateBlog(
  //     "6e0077aa-a26c-463a-a9a0-95f121febb25",
  //     "test 1",
  //     "test 2",
  //     "test 3"
  //   );
  //   // let id = prompt("Enter Id");
  //   // deleteBlog(id);
  //   const id = prompt("Enter ID");
  //   const title = prompt("Enter Title");
  //   const author = prompt("Enter Author");
  //   const content = prompt("Enter Content");
  //   updateBlog(id, title, author, content);
}

function creatTalbe() {
  for (let index = 0; index < 100; index++) {
    createBlog("title", "author", "content");
  }
}

function readBlog() {
  $("#tbDataTable").html("");
  let htmlRow = "";
  let lstBlogs = getBlogs();
  for (let i = 0; i < lstBlogs.length; i++) {
    const item = lstBlogs[i];
    // console.log(item.Id);
    // console.log(item.Title);
    // console.log(item.Author);
    // console.log(item.Content);
    htmlRow += ` 
                <tr>
                <td>
                <button type="button" class="btn btn-warning" onClick="editBlog('${
                  item.Id
                }')">Edit</button>
                <button type="button" class="btn btn-danger" onClick="deleteBlog('${
                  item.Id
                }')" >Delete</button>
                </td>
                <th scope="row">${i + 1}</th>
                    <td>${item.Title}</td>
                    <td>${item.Author}</td>
                    <td>${item.Content}</td>
                </tr>`;
    $("#tbDataTable").html(htmlRow);
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
  //   console.log(item.Id);
  //   console.log(item.Title);
  //   console.log(item.Author);
  //   console.log(item.Content);
  $("#Title").val(item.Title);
  $("#Author").val(item.Author);
  $("#Content").val(item.Content);
  _blogId = item.Id;
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
  Notiflix.Confirm.show(
    "Confirm",
    "Are you sure want to delete?",
    "Yes",
    "No",
    function okCb() {
      Notiflix.Block.dots("#frm1");
      setTimeout(() => {
        var lstBlog = getBlogs();
        var lst = lstBlog.filter((x) => x.Id == id);
        if (lst.length == 0) {
          console.log("No Data Found....");
          return;
        }
        let item = lst[0];
        lstBlog = lstBlog.filter((x) => x.Id != id);
        setLocalStorage(lstBlog);
        Notiflix.Block.remove("#frm1");
        successMessage("Delet successful......");
        readBlog();
      }, 3000);
    },
    function cancelCb() {},
    {}
  );
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

$("#btnSave").click(function () {
  let title = $("#Title").val();
  let author = $("#Author").val();
  let content = $("#Content").val();

  if (_blogId == null || _blogId == "") {
    Notiflix.Loading.circle();
    setTimeout(() => {
      createBlog(title, author, content);
      Notiflix.Loading.remove();
      successMessage("Saving successful....");
    }, 3000);
  } else {
    Notiflix.Loading.circle();
    setTimeout(() => {
      updateBlog(_blogId, title, author, content);
      Notiflix.Loading.remove();
      successMessage("Update successful....");
    }, 3000);

    _blogId = "";
  }

  $("#Title").val("");
  $("#Author").val("");
  $("#Content").val("");

  $("#Title").focus();

  readBlog();
});

function successMessage(message) {
  // Notiflix.Notify.success(message);
  Notiflix.Report.success("Success", message, "Okay");
}
