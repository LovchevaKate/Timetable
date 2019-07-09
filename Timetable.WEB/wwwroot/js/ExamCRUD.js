// Получение всех пользователей
function GetExams() {
  $.ajax({
    //url: 'https://timetableweb.azurewebsites.net/api/ExamAPI',
    url: "https://localhost:44375/api/ExamAPI",
    type: "GET",
    contentType: "application/json",
    success: function(exams) {
      var rows = "";
      exams.forEach(exam => {
        // добавляем полученные элементы в таблицу
        console.log(exam);
        rows += row(exam);
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
function GetExam(id) {
  $.ajax({
    //url: 'https://timetableweb.azurewebsites.net/api/ExamAPI/' + id,
    url: "https://localhost:44375/api/ExamAPI/" + id,

    type: "GET",
    contentType: "application/json",
    success: function(exam) {
      console.log(exam);
      var form = document.forms["examForm"];
      console.log(exam);
      form.elements["id"].value = exam.id;
      form.elements["Subject"].value = exam.subject;
      form.elements["Group"].value = exam.group;
      form.elements["Date"].value = exam.date;
      form.elements["Course"].value = exam.course;
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}

// сброс формы
function reset() {
  var form = document.forms["examForm"];
  form.reset();
}
// создание строки для таблицы
var row = function(exam) {
  return (
    "<tr data-rowid='" +
    exam.id +
    "'><td>" +
    exam.id +
    "</td>" +
    "<td>" +
    exam.subject +
    "</td>" +
    "<td>" +
    exam.group +
    "</td>" +
    "<td>" +
    exam.date +
    "</td>" +
    "<td>" +
    exam.course +
    "</td>" +
    "<td><a class='editLink' data-id='" +
    exam.id +
    "'>Изменить</a> | " +
    "<a class='removeLink' data-id='" +
    exam.id +
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
  GetExam(id);
});
// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function() {
  var id = $(this).data("id");
  $.ajax({
    //url: "https://timetableweb.azurewebsites.net/api/ExamAPI/" + id,
    url: "https://localhost:44375/api/ExamAPI/" + id,
    contentType: "application/json",
    method: "DELETE",
    success: function(exam) {
      $("tr[data-rowid='" + exam.id + "']").remove();
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
});

// загрузка пользователей
GetExams();
