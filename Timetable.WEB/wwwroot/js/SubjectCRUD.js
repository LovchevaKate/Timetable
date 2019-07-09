// Получение всех предметов
function GetSubjects() {
  $.ajax({
    //url: 'https://timetableweb.azurewebsites.net/api/SubjectAPI',
    url: "https://localhost:44375/api/SubjectAPI",

    type: "GET",
    contentType: "application/json",
    success: function(subjects) {
      var rows = "";

      subjects.forEach(subject => {
        console.log(subject);
        // добавляем полученные элементы в таблицу
        rows += row(subject);
      });
      $("table tbody").append(rows);
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}
// Получение одного предмета
function GetSubject(id) {
  $.ajax({
    //url: 'https://timetableweb.azurewebsites.net/api/SubjectAPI/' + id,
    url: "https://localhost:44375/api/SubjectAPI/" + id,

    type: "GET",
    contentType: "application/json",
    success: function(subject) {
      var form = document.forms["subjectForm"];
      form.elements["id"].value = subject.id;
      form.elements["SubjectName"].value = subject.subjectName;
      form.elements["Professor"].value = subject.professor;
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}

// сброс формы
function reset() {
  var form = document.forms["subjectForm"];
  form.reset();
  form.elements["id"].value = 0;
}

// Удаление предмета
function DeleteSubject(id) {
  $.ajax({
    //url: "https://timetableweb.azurewebsites.net/api/SubjectAPI/" + id,
    url: "https://localhost:44375/api/SubjectAPI/" + id,

    contentType: "application/json",
    method: "DELETE",
    success: function(subject) {
      $("tr[data-rowid='" + id + "']").remove();
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}
// создание строки для таблицы
var row = function(subject) {
  return (
    "<tr data-rowid='" +
    subject.id +
    "'><td>" +
    subject.id +
    "</td>" +
    "<td>" +
    subject.subjectName +
    "</td> <td>" +
    subject.professor +
    "</td>" +
    "<td><a class='editLink' data-id='" +
    subject.id +
    "'>Изменить</a> | " +
    "<a class='removeLink' data-id='" +
    subject.id +
    "'>Удалить</a></td></tr>"
  );
};
// сброс значений формы
$("#reset").click(function(e) {
  e.preventDefault();
  reset();
});

// нажимаем на ссылку Изменить
$("body").on("click", ".editLink", function() {
  var id = $(this).data("id");
  GetSubject(id);
});
// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function() {
  var id = $(this).data("id");
  DeleteSubject(id);
});

// загрузка предметов
GetSubjects();
