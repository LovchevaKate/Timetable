// Получение всех пользователей
function GetUsers() {
  $.ajax({
    //url: 'https://timetableweb.azurewebsites.net/api/UserAPI',
    url: "https://localhost:44375/api/UserAPI",

    type: "GET",
    contentType: "application/json",
    success: function(users) {
      var rows = "";
      users.forEach(user => {
        // добавляем полученные элементы в таблицу
        console.log(user);
        rows += row(user);
      });
      $("table tbody").append(rows);
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}
// Получение одного пользователя
function GetUser(id) {
  console.log(id);
  $.ajax({
    //url: 'https://timetableweb.azurewebsites.net/api/UserAPI/' + id,
    url: "https://localhost:44375/api/UserAPI/" + id,

    type: "GET",
    contentType: "application/json",
    success: function(user) {
      console.log(user);
      var form = document.forms["userForm"];
      form.elements["id"].value = user.id;
      form.elements["Login"].value = user.login;
      form.elements["Password"].value = user.password;
      form.elements["Role"].value = user.roleType;
      form.elements["Group"].value = user.group;
      form.elements["Subgroup"].value = user.subgroup;
      form.elements["Course"].value = user.course;
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}

// создание строки для таблицы
var row = function(user) {
  if (user.course == 0 && user.group == 0 && user.subgroup == null) {
    return (
      "<tr data-rowid='" +
      user.id +
      "'><td>" +
      user.id +
      "</td>" +
      "<td>" +
      user.login +
      "</td> <td>" +
      user.password +
      "</td>" +
      "<td>" +
      user.role +
      "</td>" +
      "<td><a class='editLink' class='btn btn-sm btn-primary' data-id='" +
      user.id +
      "'>Изменить</a> | " +
      "<a class='removeLink' class='btn btn-sm btn-primary' data-id='" +
      user.id +
      "'>Удалить</a></td></tr>"
    );
  } else {
    return (
      "<tr data-rowid='" +
      user.id +
      "'><td>" +
      user.id +
      "</td>" +
      "<td>" +
      user.login +
      "</td> <td>" +
      user.password +
      "</td>" +
      "<td>" +
      user.role +
      "</td> <td>" +
      user.group +
      "</td>" +
      "<td>" +
      user.subgroup +
      "</td> <td>" +
      user.course +
      "</td>" +
      "<td><a class='editLink' class='btn btn-sm btn-primary' data-id='" +
      user.id +
      "'>Изменить</a> | " +
      "<a class='removeLink' class='btn btn-sm btn-primary' data-id='" +
      user.id +
      "'>Удалить</a></td></tr>"
    );
  }
};

// нажимаем на ссылку Изменить
$("body").on("click", ".editLink", function() {
  var id = $(this).data("id");
  GetUser(id);
});
// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function() {
  var id = $(this).data("id");
  DeleteUser(id);
});

// сброс формы
function reset() {
  var form = document.forms["userForm"];
  form.reset();
}

// сброс значений формы
$("#reset").click(function(e) {
  e.preventDefault();
  reset();
});

// нажимаем на ссылку Удалить
function DeleteUser(id) {
  console.log(id);
  $.ajax({
    //url: "https://timetableweb.azurewebsites.net/api/UserAPI/" + id,
    url: "https://localhost:44375/api/UserAPI/" + id,

    contentType: "application/json",
    method: "DELETE",
    success: function(user) {
      $("tr[data-rowid='" + user.id + "']").remove();
    }
  });
}
// загрузка пользователей
GetUsers();
