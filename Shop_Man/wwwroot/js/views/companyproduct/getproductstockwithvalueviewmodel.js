
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
    url: WEB_URL + "CompanyProduct/GetProductStockViewModel",

    // data: JSON.stringify(receiveMain),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);

    BindArticles(data.articles);
    BindProdNames(data.prodNames)
    BindSizes(data.sizes)
                                // BinSubCategoryTable(data.customerSubCategorys);


                            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                            }
                        });
                    }





    function BindProdNames(_custList) {
        $("#selProdNames option").remove();

    $.each(_custList, function (i, prodData) {

        $("#selProdNames").append(' <option data-id="' + prodData.prodNameID + '" data-custID="' + prodData.prodNameID + '" value="' + prodData.prodNameID + '" > ' + prodData.name + '</option>');


                        });

                    }



    function BindArticles(_custList) {
        $("#selArticle option").remove();

    $.each(_custList, function (i, prodData) {

        $("#selArticle").append(' <option data-id="' + prodData.customerID + '" data-custID="' + prodData.customerID + '" value="' + prodData.customerID + '" > ' + prodData.name + '</option>');


                        });

                    }
    function BindSizes(_custList) {
        $("#selSize option").remove();

    $.each(_custList, function (i, prodData) {

        $("#selSize").append(' <option data-id="' + prodData.sizeID + '" data-custID="' + prodData.sizeID + '" value="' + prodData.sizeID + '" > ' + prodData.name + '</option>');


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



    $("a.reports").on('click', function () {
        $("#spInfo").html('');


    var _fromDate = $("#txtDate").val();
    var _toDate = $("#txtDate2").val();
    var _custIDs = $("#selCust").val();
    if (_custIDs == '') {
        _custIDs = "0";
                        }


                        //if ($("chkCashSales").is(':checked')) {

                        //    _custIDs = "0";
                        //}

                        //var IsDetails = "0";

                        //if ($('#chkDetails').is(':checked')) {
                        //    IsDetails = "1";
                        //}

                        var type = $(this).attr('data-type');
    var _id = $(this).attr('id');
    if (_id == 'btCurrentStock') {
        $(this).attr('href', WEB_URL + 'CompanyProduct/ProductCurrentStock');
                        }
    if (_id == 'btCurrentStockValue') {
        $(this).attr('href', WEB_URL + 'CompanyProduct/ProductCurrentStockValue');
                        }

    if (_id == 'btDateWiseStockValue') {
        $(this).attr('href', WEB_URL + 'CompanyProduct/ProductDatewiseStock?fromDate=' + _fromDate + '&&toDate=' + _toDate);
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

