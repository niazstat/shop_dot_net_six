
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];


    $(document).ready(function () {


        var date = new Date();
    var today = date.getDate() + '-' + months[date.getMonth()] + '-' + date.getFullYear();
    //        new Date(date.getFullYear(), date.getMonth(), date.getd());

    //$("#txtDate2").datepicker('setDate', 'now');
    //alert('OK');
    LoadDefaultData();

    $("#txtDate2").datepicker({
        autoclose: true,
        }).on('changeDate', function (e) {
        ChangeDt("#txtDate2");
        });

    $("#txtDate").datepicker({
        autoclose: true,
        }).on('changeDate', function (e) {
        ChangeDt("#txtDate");
        });

    $("#txtDate").datepicker({
        autoclose: true,
        });

    $("#txtDate2").val(today);
    $("#txtDate").val(today);



    function ChangeDt(caller) {

        console.log(caller);
    let _thisVal2 = $(caller).val();

            if (_thisVal2.indexOf('-') > 0) {

        let _existValue = _thisVal2.split('-');
    $(caller).val(_existValue[0] + '-' + months[_existValue[1] - 1] + '-' + _existValue[2]);

            }



        };





    function LoadDefaultData() {
        //ClearControl();
        $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Customer/GetCustomers",

    // data: JSON.stringify(receiveMain),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);


    BindCustName(data)
                    // BinSubCategoryTable(data.customerSubCategorys);


                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        }





    function BindCustName(_custList) {
        $("#selCust option").remove();

    $.each(_custList, function (i, prodData) {

        $("#selCust").append(' <option data-id="' + prodData.customerID + '" data-custID="' + prodData.customerID + '" value="' + prodData.customerID + '" > ' + prodData.shopName + '</option>');


            });

        }


    $("#chkCashSales").change(function () {
            if ($(this).is(':checked')) {
        $("#selCust").attr('disabled', 'disabled');
    $("#selCust").val('');
            }

    else {
        $("#selCust").removeAttr('disabled', 'disabled');

            }
        });








    $("a.reports").on('click', function (e) {
        $("#spInfo").html('');


    var _fromDate = $("#txtDate").val();
    var _toDate = $("#txtDate2").val();
    var _custIDs = '';
    var _yearName = $("#selYear").val();
    var _id = $(this).attr('id');

    //  console.log(_custIDs);

    if ($("#chkCashSales").is(':checked')) {
        // alert(_custIDs);
        _custIDs = '0';
                // alert(_custIDs);
            }
    else {
        _custIDs = $("#selCust").val();
    console.log(_custIDs);
            }

    if (_custIDs == '') {
        $("#spInfo").html('<strong style="color:red">Please selet a Customer</strong>');
    e.preventDefault();
    return;
            }


    var type = $(this).attr('data-type');
    if (_id == 'btnYearwiseClose') {

                if (_yearName.toUpperCase() == "CURRENT") {

        let  _year2= date.getFullYear()
    $(this).attr('href', WEB_URL + 'Customer/CustomerYearClosingViewCurrent');


                   // $(this).attr('href', WEB_URL + 'Customer/CustomerClosingViewCurrentDate?customerID=' + _custIDs + '&&yearName=' + _year2 + '&&_type=VIEW&&dDate=' + today);

                }
    else {
        $(this).attr('href', WEB_URL + 'Customer/CustomerClosingInAYear?yearName=' + _yearName);
                }
            

            }

    else if (_id == 'btnAllCustYearwiseClose') {
                if (_yearName.toUpperCase() == "CURRENT") {


        $(this).attr('href', WEB_URL + 'Customer/View_ALL_Customer_Year_Close?dDate=' + today);
                }
    else {

                    var _yearName = $("#selYear").val();
    $(this).attr('href', WEB_URL + 'Customer/CustomerClosingInAYearAllCustomer?yearName=' + _yearName);



                }


            }


    else {
        $(this).attr('href', WEB_URL + 'Customer/CustomerBalance?fromDate=' + _fromDate + "&&toDate=" + _toDate + "&&type=" + type + "&&customerIDs=" + _custIDs);

            }
            
        });


    function GetDataValue(value, dataList) {
            var z = $(dataList);
    var val = $(z).find('option[data-id="' + value + '"]');
    var endval = val.attr('value');
    return endval;
        }
    function GetDataID(value, dataList) {
            var z = $(dataList);
    var val = $(z).find('option[value="' + value + '"]');
    var endval = val.attr('data-id');
    return endval;
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
        win.document.write('<title>Bill</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }
