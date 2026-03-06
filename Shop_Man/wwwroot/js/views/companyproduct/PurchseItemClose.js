

    var ReportDate = "";
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];


    var date = new Date();
    var today = date.getDate() + '-' + months[date.getMonth()] + '-' + date.getFullYear();






    ReportDate = today;

    $(document).ready(function () {


        datePicker = flatpickr("#txtDateEntry", { dateFormat: "d-M-Y" });
    datePicker.setDate(date);







    LoadDefaultData();

    function LoadDefaultData() {
        $("#spInfo").html('');
    //  ClearData();
    $("#selLastYearList option").remove();

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "CompanyProduct/GetLastEntryDated",

    //data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);

    $(data).each(function (i, _singleObj) {

        $("#selLastYearList").append('<option value=' + _singleObj.lastEntryDate + '>' + _singleObj.lastEntryDate + ' </option>');

                                                });



                                            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                                            }
                                        });
                                    }



    $("#btnSearchdate").click(function () {
        $("#spInfo").html('');
    let _date = $("#selLastYearList").val();
    if (_date == '-') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
                                        }

    SelectDayClose(_date);
                                    });

    function SelectDayClose(_date) {
                                        // ClearData();
                                        var model = {
        'toDate': _date
                                        }



    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "CompanyProduct/Get_View_Item_Close_List",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
    console.log(data);
    var _obj = data.obj;
    BindData(_obj);


                                            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                                            }
                                        });
                                    }



    $("#btnSaveCust").click(function () {

        $("#spInfo").html('');

    var hi = confirm("Do you want to Save ?");

    if (!hi) {

                                            return;
                                        }


    let _date = $("#txtDateEntry").val();
    if (_date == '-') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
                                        }



    var model = {
        'toDate': _date
                                        }



    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "CompanyProduct/Save_Item_Close_List",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
    console.log(data);
    var _obj = data.obj;
    BindData(_obj);
    LoadDefaultData();
                                            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                                            }
                                        });


                                    });



    $("#btnViewCurrect").click(function () {

        $("#spInfo").html('');

    //  datePicker.setDate(today);
    let _date = $("#txtDateEntry").val();
    if (_date == '-') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
                                        }



    var model = {
        'toDate': today
                                        }



    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "CompanyProduct/Get_View_Item_Current_Close_List",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
    console.log(data);
    var _obj = data.obj;
    BindData(_obj);
    LoadDefaultData();
                                            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                                            }
                                        });


                                    });



    function BindData(_obj) {

        $("#tblOrder tr:gt(0)").remove();


    $.each(_obj, function (i, prodData) {

        $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.articleName + '</td><td>' + prodData.sizeName + '</td><td>' + prodData.fromDateFormated + '</td><td style="text-align:right;">' + prodData.toDateFormated + '</td><td style="text-align:right;">' + prodData.totalPurQty.toFixed(2) + '</td><td style="text-align:right;">' + prodData.totalPurAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.totalDiscount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.totalPurNetPurAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.avgAmount.toFixed(2) + '</td></tr>');


                                        });


                                    }



                                });

    function createPDF() {
                                    var sTable = document.getElementById('tab').innerHTML;

    var style = "<style>";
                                    //style = style + "table {width: 100%;font: 17px Calibri;}";
                                    //  style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
        style = style + "table, th, td {padding: 2px 3px;border-collapse: collapse;}";
        style = style + "</style>";

    // CREATE A WINDOW OBJECT.
    var win = window.open('', '', 'height=595,width=842');

    win.document.write('<html><head>');
        win.document.write('<title>Pur_Item_Close</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
                                }


