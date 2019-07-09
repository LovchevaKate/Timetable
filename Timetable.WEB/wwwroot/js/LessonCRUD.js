// Получение всех пар
function GetLessons() {
  $.ajax({
    url: "https://localhost:44375/api/LessonAPI",
    //url: 'https://timetableweb.azurewebsites.net/api/LessonAPI',

    type: "GET",
    contentType: "application/json",
    success: function(lessons) {
      var rows = "";
      lessons.forEach(lesson => {
        // добавляем полученные элементы в таблицу
        rows += row(lesson);
      });
      $("table tbody").append(rows);
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}
// Получение одной пары
function GetLesson(id) {
  $.ajax({
    //url: 'https://timetableweb.azurewebsites.net/api/LessonAPI/' + id,
    url: "https://localhost:44375/api/LessonAPI/" + id,

    type: "GET",
    contentType: "application/json",
    success: function(lesson) {
      console.log(lesson);
      var form = document.forms["lessonForm"];
      form.elements["id"].value = lesson.id;
      form.elements["Auditorium"].value = lesson.auditorium;
      form.elements["Subject"].value = lesson.subject;
      form.elements["Course"].value = lesson.course;
      form.elements["Group"].value = lesson.group;
      form.elements["Subgroup"].value = lesson.subgroup;
      form.elements["Day"].value = lesson.day;
      form.elements["Time"].value = lesson.time;
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}

// Удаление пары
function DeleteLesson(id) {
  $.ajax({
    //url: "https://timetableweb.azurewebsites.net/api/LessonAPI/" + id,
    url: "https://localhost:44375/api/LessonAPI/" + id,

    contentType: "application/json",
    method: "DELETE",
    success: function(lesson) {
      $("tr[data-rowid='" + lesson.id + "']").remove();
    },
    error: function(xhr, ajaxOptions, thrownError) {
      alert(xhr.status);
      alert(thrownError);
    }
  });
}
// создание строки для таблицы
var row = function(lesson) {
  return (
    "<tr data-rowid='" +
    lesson.id +
    "'><td>" +
    lesson.id +
    "</td>" +
    "<td>" +
    lesson.auditorium +
    "</td> " +
    "<td>" +
    lesson.subject +
    "</td>" +
    "<td>" +
    lesson.day +
    "</td>" +
    "<td>" +
    lesson.time +
    "</td>" +
    "<td>" +
    lesson.course +
    "</td>" +
    "<td>" +
    lesson.group +
    "</td>" +
    "<td>" +
    lesson.subgroup +
    "</td>" +
    "<td><a class='editLink' data-id='" +
    lesson.id +
    "'>Изменить</a> | " +
    "<a class='removeLink' data-id='" +
    lesson.id +
    "'>Удалить</a></td></tr>"
  );
};

// нажимаем на ссылку Изменить
$("body").on("click", ".editLink", function() {
  var id = $(this).data("id");
  GetLesson(id);
});
// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function() {
  var id = $(this).data("id");
  DeleteLesson(id);
});

// сброс формы
function reset() {
  var form = document.forms["lessonForm"];
  form.reset();
}

// сброс значений формы
$("#reset").click(function(e) {
  e.preventDefault();
  reset();
});

// загрузка пар
GetLessons();
