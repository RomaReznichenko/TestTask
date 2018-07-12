function AppointmentBtnClick() {
    document.getElementById("Category").classList.remove("active");
    document.getElementById("Appointment").classList.add("active");
    document.getElementById("message").classList.add("hidden");

    Appointment();
}

function CategoryBtnClick() {
    document.getElementById("Appointment").classList.remove("active");
    document.getElementById("Category").classList.add("active");
    document.getElementById("message").classList.add("hidden");

    Category();
}

var category = [];

function getCategory() {
    var grid = this._grid;
    var xmlhttp = new XMLHttpRequest();
    var url = "http://localhost:3293/api/category";
    xmlhttp.open("GET", url, true);
    xmlhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var responseJson = JSON.parse(this.responseText);
            for (var i = 0; i < responseJson.length; i++) {
                var item = new Object();
                item.id = responseJson[i].id;
                item.name = responseJson[i].name;
                item.description = responseJson[i].description;
                category.push(item);
            }
        }
    };
    xmlhttp.send();
}

window.onload = function(){
    getCategory();
}

function Appointment () {
$.ajax({
        type: "GET",
        url: "http://localhost:3293/api/Appointment"
    }).done(function () {
        $("#jsGrid").jsGrid({
            width: "100%",
            height: "550px",
            inserting: true,
            editing: true,
            sorting: false,
            paging: false,
            autoload: true,
            pageSize: 10,
            controller: {
                loadData: function (filter) {
                    return $.ajax({
                        type: "GET",
                        url: "http://localhost:3293/api/appointment",
                        data: filter
                    });
                },
                insertItem: function (item) {
                    item.id = 0;
                    return $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "http://localhost:3293/api/appointment/add",
                        data: JSON.stringify(item)
                    });
                },
                updateItem: function (item) {
                    return $.ajax({
                        type: "PUT",
                        contentType: "application/json",
                        url: "http://localhost:3293/api/appointment/update",
                        data: JSON.stringify(item)
                    });
                },
                deleteItem: function (item) {
                    return $.ajax({
                        type: "DELETE",
                        contentType: "application/json",
                        url: "http://localhost:3293/api/appointment/delete",
                        data: JSON.stringify(item)
                    });
                }
            },
            fields: [
              { name: "dateCreate", type: "text", width: 80, validate: "required" },
              { name: "dateendofactuality", type: "text", width: 80 },
              { name: "description", type: "textarea", width: 250 },
              { name: "category_id", type: "select", items: category, valueField: "id", textField: "name" },
              { type: "control" }
          ]
        });

    });
}

function Category () {
    $.ajax({
        type: "GET",
        url: "http://localhost:3293/api/category"
    }).done(function () {
        $("#jsGrid").jsGrid({
            width: "100%",
            height: "550px",
            inserting: true,
            editing: true,
            sorting: false,
            paging: false,
            autoload: true,
            pageSize: 10,
            controller: {
                loadData: function (filter) {
                    return $.ajax({
                        type: "GET",
                        url: "http://localhost:3293/api/category",
                        data: filter
                    });
                },
                insertItem: function (item) {
                    item.id = 0;
                    return $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "http://localhost:3293/api/category/add",
                        data: JSON.stringify(item)
                    });
                },
                updateItem: function (item) {
                    return $.ajax({
                        type: "POST",
                        contentType: "application/json",
                        url: "http://localhost:3293/api/category/update",
                        data: JSON.stringify(item)
                    });
                },
                deleteItem: function (item) {
                    return $.ajax({
                        type: "DELETE",
                        contentType: "application/json",                        
                        url: "http://localhost:3293/api/category/delete",
                        data: JSON.stringify(item)
                    });
                }
            },
            fields: [
              { name: "name", type: "text", width: 100, validate: "required" },
              { name: "description", type: "textarea", width: 250 },
              { type: "control" }
          ]
        });
    });
}