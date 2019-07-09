// Получение всех оценок
function GetMarks() {
  let form = document.forms["markForm"];
  let subjectform = String(form.elements["Subject"].value);
  let groupform = String(form.elements["Group"].value);
  let courseform = String(form.elements["Course"].value);
  let subgroupform = String(form.elements["Subgroup"].value);

  if (
    subjectform != "" ||
    groupform != "" ||
    courseform != "" ||
    subgroupform != ""
  ) {
    $.ajax({
      //url: 'https://timetableweb.azurewebsites.net/api/MarkAPI',
      url: "https://localhost:44375/api/MarkAPI",
      type: "GET",
      contentType: "application/json",
      headers: {
        subject: subjectform,
        group: groupform,
        course: courseform,
        subgroup: subgroupform
      },
      success: function(marks) {
        let rows = "";
        marks.forEach(mark => {
          rows += row(mark);
        });
        $("table tbody").append(rows);
      }
    });
  } else {
    alert("Error. Write more information");
  }
}

let row = function(mark) {
  return (
    "<tr data-rowid='" +
    mark.id +
    "'><td>" +
    mark.id +
    "    </td> " +
    "<td><p class='tableSubject'>" +
    mark.subject +
    "</p></td>" +
    "<td><p class='tableUser'>" +
    mark.user +
    "</p></td>" +
    "<td><input class='mark' type='text' value = '" +
    mark.value +
    "'/>" +
    "<td><a class='editLink' class='btn btn-sm btn-primary' data-id='" +
    mark.id +
    "'>Изменить</a>" +
    "</td > "
  );
};

$("#marksTable").on("click", ".editLink", function() {
  // get the current row
  let currentRow = $(this).closest("tr");

  let col1 = currentRow.find("td:eq(0)").html();
  let col2 = currentRow.find(".tableSubject").html();
  let col3 = currentRow.find(".tableUser").html();
  let col4 = Number(currentRow.find(".mark").val());

  //if (col4 <= 0 && col4 >= 11)
  //    alert("Error. Wrong mark!");

  if (col4 >= 1 && col4 <= 10) {
    $.ajax({
      //url: 'https://timetableweb.azurewebsites.net/api/MarkAPI'
      url: "https://localhost:44375/api/MarkAPI",
      contentType: "application/json",
      method: "PUT",
      headers: {
        id: col1,
        subject: col2,
        user: col3,
        mark: col4
      },
      success: function(mark) {
        alert("Save");
        $("tr[data-rowid='" + mark.id + "']").replaceWith(row(mark));
      }
    });
  } else {
    alert("Error. Wrong mark!");
  }
});
