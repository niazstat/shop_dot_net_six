
    var ReportDate = "";
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];


    var date = new Date();
    var today = date.getDate() + '-' + months[date.getMonth()] + '-' + date.getFullYear();
    ReportDate = today;
    var tableCust;
    $(document).ready(function () {


        $("#txtYear").val(date.getFullYear());

    $("#txtDateEntry").datepicker({
        autoclose: true,
        }).on('changeDate', function (e) {
        ChangeDt("#txtDateEntry");
        });

    $("#txtDateEntry").val(today);
    $("#txtDateEntrySearch").val(today);
    $("#txtDateEntrySearch").datepicker({
        autoclose: true,
        }).on('changeDate', function (e) {
        ChangeDt("#txtDateEntrySearch");
        });



    function ChangeDt(caller) {
        ClearData();
    console.log(caller);
    let _thisVal2 = $(caller).val();

            if (_thisVal2.indexOf('-') > 0) {

        let _existValue = _thisVal2.split('-');
    console.log(_existValue);
    $(caller).val(_existValue[0] + '-' + months[_existValue[1] - 1] + '-' + _existValue[2]);

            }
        };


    function currentTime() {
        let date = new Date();
    let hh = date.getHours();
    let mm = date.getMinutes();
    let ss = date.getSeconds();
    let session = "AM";

    if (hh === 0) {
        hh = 12;
            }
            if (hh > 12) {
        hh = hh - 12;
    session = "PM";
            }

    hh = (hh < 10) ? "0" + hh : hh;
    mm = (mm < 10) ? "0" + mm : mm;
    ss = (ss < 10) ? "0" + ss : ss;

    let time = hh + ":" + mm + ":" + ss + " " + session;

    //document.getElementById("clock").innerText = time;

    $("#txtDateEntryTime").val(time)
    let t = setTimeout(function () {currentTime()}, 1000);
        }



    tableCust = $('#dataTable').DataTable({
        data: [],
    "columnDefs": [

    {
        "searchable": true,
    "orderable": false,
    "targets": 0
                }
    ],
    "language":
    {
        "processing": '<div class="font-icon-wrapper float-left mr-3 mb-3" style="margin-left: 9%;">' +
            '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
                '<div class="loader">' +
                    '<div class="line-scale-pulse-out-rapid">' +
                        '<div></div>' +
                    '<div></div>' +
                    '<div></div>' +
                    '<div></div>' +
                    '<div></div>' +
                    '</div>' +
                '</div>' +
            '</div>' +
        '<p>Loading</p></div>'
            },
    //"responsive": true,
    //"processing": true,
    //"serverSide": true,
    "lengthMenu": [
    [10, 15, 25, 50, 100, -1],
    [10, 15, 25, 50, 100, "All"]
    ],

    "order": [0, "desc"],
    "start": 0,
    "length": 10,
    "bFilter": true,

    "columns": [
    {"data": "slNo" },
    {
        "data": "yearName"
                },
    {
        "data": "prevBalance"
                },
    {
        "data": "totalReceive"
                },
    {
        "data": "totalPayment"
                },
    {
        "data": "addLess"
                },
    {"data": "balance" },
    {
        "data": "totalSales"
                },
    {"data": "totalSalesQty" },
    {
        "data": "totalSalesReturnAmount"
                },
    {"data": "totalSalesReturnQty" },
    {
        "data": "totalPurchase"
                },
    {"data": "totalPurchaseQty" },
    {
        "data": null,
    "mRender": function (data, type, full) {
                        return '<button class="mb-2 mr-2 btn-icon btn-shadow btn-dashed btn btn-outline-dark" onclick="SelectDayClose(\'' + data.yearName + '\')">Select</button>';
                    }
                },
    {
        "data": null,
    "mRender": function (data, type, full) {
                        return '<button class="mb-2 mr-2 btn-icon btn-shadow btn-dashed btn btn-outline-dark" onclick="SelectDayClose(\'' + data.yearName + '\')">Select</button>';
                    }
                }
    ]
        });


    currentTime();


    LoadDefaultData();

    $("#btnSaveCust").attr('disabled', 'disabled');
    function LoadDefaultData() {
        $("#spInfo").html('');
    ClearData();


    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/GetExistingYearCloseList",

    //data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();

        console.log(data);
    var _obj = data.obj;
    $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
    tableCust.rows.add(_obj).draw();
                    // BindData(_obj);
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        }

    $("#btnSearchdate").click(function () {

        let _date = $("#txtYear").val();
    if (_date == '') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
            }

    SelectDayClose(_date);
        });



    $("#btnEditorNewEntry").click(function () {
        ClearData();
    let _date = $("#txtYear").val();
    if (_date == '') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
            }

    var model = {
        'yearName': parseInt(_date)
            }
    $("#btnSaveCust").removeAttr('disabled'
    );
    $("#btnSaveCust").attr('data-editstatus', '1')
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/GetYearCloseNew",

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
        });




    $("#txtAddLess").bind('input', function () {

        CalculateBalance();
        });

    function CalculateBalance() {


        let _prevBalance = $("#txtPrevBalance").val();

    let _totalReceive = $("#txtTotalReceive").val();
    let _totalPayment = $("#txtTotalPayment").val();
    let _addLess = $("#txtAddLess").val();

    if (_prevBalance == '') {_prevBalance = '0'}
    if (_totalReceive == '') {_totalReceive = '0'}
    if (_totalPayment == '') {_totalPayment = '0'}
    if (_addLess == '') {_addLess = '0'}

    let _balance = parseFloat(_prevBalance) + parseFloat(_totalReceive) + parseFloat(_addLess) - parseFloat(_totalPayment);

    $("#txtBalance").val(_balance);
        }

    $("#btnPrev").click(function () {
        ClearData();

    let _ddate = $("#txtYear").val();
    if (_ddate == '') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
            }
    //let _existValue = _ddate.split('-');

    let _dDat2 = parseInt(_ddate) - 1;


    $("#txtYear").val(_dDat2);





    var model = {
        'yearName': _dDat2
            }

    $("#btnSaveCust").attr('data-editstatus', '0')
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/GetExistingYearClose",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);
    $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
    var _obj = data.obj;
    BindData(_obj);
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        });


    $("#btnNext").click(function () {
        ClearData();

    let _ddate = $("#txtYear").val();
    if (_ddate == '') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
            }
    //let _existValue = _ddate.split('-');

    let _dDat2 = parseInt(_ddate) + 1;

    $("#txtYear").val(_dDat2);
    var model = {
        'yearName': _dDat2
            }
    $("#btnSaveCust").attr('data-editstatus', '0')
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');

    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/GetExistingYearClose",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);
    var _obj = data.obj;
    $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
    BindData(_obj);
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        });


    $("#btnRecvDetails").click(function () {

        ViewData('RECEIVE');

        });

    $("#btnPayDetails").click(function () {

        ViewData('PAYMENT');

        });

    $("#btnBalDetails").click(function () {

        ViewData('ALL');

        });

    function ViewData(_transType) {
        $("#tblOrderBill tr:gt(4)").remove();
    $("#spDate").text('');
    let viewType = $("#btnSaveCust").attr('data-editstatus')
    let transType = _transType;
    let _dDate = $("#txtYear").val();
    ReportDate = "Year_Close_Report_" + _dDate;
    model = {
        "TransType": transType,
    "YearName": parseInt(_dDate),
    "ViewType": parseInt(viewType)

            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/YearCloseDetailsList",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#btnViewModel").click();
    $("#spInfo").html('');
    console.log(data);

    var _objList = data.obj;
    var _obj2 = data.obj2;
    var totalReceive = 0;
    var totalPayment = 0;

    //dDateFormated
    //  $("#spDayCloseNo").text(_obj2.slNo);

    var isDateAdded = false
    $(_objList).each(function (i, _singleObj) {

                        if (!isDateAdded) {
        $("#spDate").text(_dDate);
    isDateAdded = true;
                        }
    totalReceive += _singleObj.receiveAmount;
    totalPayment += _singleObj.paymentAmount;

    $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" controlname="SlNo">' + (i + 1) + '</td> <td class="TDRightBorder"  >' + _singleObj.dDateFormated + '</td> <td class="TDRightBorder"  >' + _singleObj.transType + '</td> <td class="TDRightBorder" controlname="SizeName" style="text-align:left;">' + _singleObj.describtion + '</td>  <td class="TDRightBorder" style="text-align:right;">' + _singleObj.receiveAmount.toFixed(2) + '</td><td class="TDRightBorder" style="text-align:right;"  >' + _singleObj.paymentAmount.toFixed(2) + '</td> </tr>');

                    });
    $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" Colspan="4"><strong> Total : </strong></td>  <td class="TDRightBorder" style="text-align:right;"><strong> ' + totalReceive.toFixed(2) + '</strong> </td><td class="TDRightBorder" style="text-align:right;"  ><strong> ' + totalPayment.toFixed(2) + '</strong> </td> </tr>');

                    /*

                    if (_transType == 'ALL') {
        $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" Colspan="2">মোট বিক্রয় : </td><td class="TDRightBorder" style="text-align:right;">' + _obj2.totalSales.toFixed(2) + '</td>  <td class="TDRightBorder"   style="text-align:right;"><strong> পূর্বের ব্যাল্যান্স :</strong> </td><td class="TDRightBorder" style="text-align:right;" >' + _obj2.prevBalance.toFixed(2) + '</td> <td class="TDRightBorder"   style="text-align:right;"></td></tr>');
    $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" Colspan="2"> মোট ক্রয় : </td><td class="TDRightBorder" style="text-align:right;">' + _obj2.totalPurchase.toFixed(2) + '</td>  <td class="TDRightBorder" style="text-align:right;"><strong> মোট গ্রহণ:</strong></td><td class="TDRightBorder" style="text-align:right;"  >  ' + totalReceive.toFixed(2) + '</td> <td class="TDRightBorder" style="text-align:right;"></td></tr>');
    $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" Colspan="2"> </td><td class="TDRightBorder" style="text-align:right;"  ></td>  <td class="TDRightBorder" style="text-align:right;"><strong> মোট :</strong></td><td class="TDRightBorder" style="text-align:right;"  >  ' + (parseFloat(_obj2.prevBalance) + parseFloat(totalReceive)).toFixed(2) + '</td> <td class="TDRightBorder" style="text-align:right;"></td></tr>');
    $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" Colspan="2"> </td><td class="TDRightBorder" style="text-align:right;"  ></td>  <td class="TDRightBorder" style="text-align:right;"><strong> মোট প্রদান :</strong></td><td class="TDRightBorder" style="text-align:right;"  >' + totalPayment.toFixed(2) + '</td> <td class="TDRightBorder" style="text-align:right;"></td> </tr>');
    $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" Colspan="2"> </td><td class="TDRightBorder" style="text-align:right;"  ></td>  <td class="TDRightBorder" style="text-align:right;"><strong> Add/Less :</strong></td><td class="TDRightBorder" style="text-align:right;"  >' + _obj2.addLess.toFixed(2) + '</td> <td class="TDRightBorder" style="text-align:right;"></td> </tr>');
    $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" Colspan="2"> </td><td class="TDRightBorder" style="text-align:right;"  ></td>  <td class="TDRightBorder" style="text-align:right;"><strong> ব্যাল্যান্স :</strong></td><td class="TDRightBorder" style="text-align:right;"  >' + _obj2.balance.toFixed(2) + '</td> <td class="TDRightBorder" style="text-align:right;"></td></tr>');
    $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" Colspan="2">  <td class="TDRightBorder" style="text-align:right;"><strong> নোট :</strong></td><td class="TDRightBorder" style="text-align:left;" colspan="3" >' + _obj2.note + '</td>  </tr>');

                    }

    */
                    //var _obj = data.obj;
                    //BindData(_obj);
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });


        }


    $("#btnBalDetailsSales").click(function () {

        ViewSalesPurReturnData('ALL');

        });

    function ViewSalesPurReturnData(_transType) {
        $("#tblOrderBill2 tr:gt(4)").remove();
    $("#spDate2").text('');
    $("#spDayCloseNo2").text('');
    let viewType = $("#btnSaveCust").attr('data-editstatus')
    let transType = _transType;
    let _dDate = $("#txtDateEntrySearch").val();
    ReportDate = "DayClose_Report_(Sales_Purchase_Return)" + _dDate;
    model = {
        "TransType": transType,
    "dDate": _dDate,
    "ViewType": parseInt(viewType)

            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/DayCloseDetailsSalesPurchaseList",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#btnViewModel2").click();
    $("#spInfo").html('');
    console.log(data);

    var _objList = data.obj;
    var _obj2 = data.obj2;
    var totalSalesAmount = 0;
    var totalPurAmount = 0;
    var totalRetAmount = 0;

    var totalRecvAmount = 0;
    //dDateFormated
    $("#spDayCloseNo2").text(_obj2.slNo);

    var isDateAdded = false
    $(_objList).each(function (i, _singleObj) {

                        if (!isDateAdded) {
        $("#spDate2").text(_singleObj.dDateFormated);
    isDateAdded = true;
                        }


    totalSalesAmount += _singleObj.salesAmount;
    totalPurAmount += _singleObj.purchaseAmount;
    totalRetAmount += _singleObj.salesReturnAmount;
    totalRecvAmount += _singleObj.receiveAmount;

    $("#tblOrderBill2").append('<tr>  <td class="TDAllBorder" controlname="SlNo">' + (i + 1) + '</td> <td class="TDRightBorder"  >' + _singleObj.dDateFormated + '</td> <td class="TDRightBorder"  >' + _singleObj.salesAndReturnType + '</td> <td class="TDRightBorder" controlname="SizeName" style="text-align:left;">' + _singleObj.describtion + '</td>  <td class="TDRightBorder" style="text-align:right;">' + _singleObj.salesAmount.toFixed(2) + '</td><td class="TDRightBorder" style="text-align:right;">' + _singleObj.receiveAmount.toFixed(2) + '</td><td class="TDRightBorder" style="text-align:right;"  >' + _singleObj.purchaseAmount.toFixed(2) + '</td> <td class="TDRightBorder" style="text-align:right;"  >' + _singleObj.salesReturnAmount.toFixed(2) + '</td></tr>');

                    });
    $("#tblOrderBill2").append('<tr>  <td class="TDAllBorder" Colspan="4"><strong> Total : </strong></td>  <td class="TDRightBorder" style="text-align:right;"><strong> ' + totalSalesAmount.toFixed(2) + '</strong> </td><td class="TDRightBorder" style="text-align:right;"><strong> ' + totalRecvAmount.toFixed(2) + '</strong> </td><td class="TDRightBorder" style="text-align:right;"  ><strong> ' + totalPurAmount.toFixed(2) + '</strong> </td><td class="TDRightBorder" style="text-align:right;"  ><strong> ' + totalRetAmount.toFixed(2) + '</strong> </td> </tr>');



                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });


        }


    $("#btnLastEntry").click(function () {
        ClearData();
    $("#btnSaveCust").attr('data-editstatus', '0')
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/GetLastYearClose",

    // data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);
    var _obj = data.obj;
    BindData(_obj);
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        });






    $("#btnAddNewCust").click(function () {
        ClearData();
    let _date = $("#txtDateEntrySearch").val();
    if (_date == '') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
            }

    var model = {
        'dDate': _date
            }
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $("#btnSaveCust").attr('data-editstatus', '1')
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/GetDayCloseNew",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);
    $("#btnSaveCust").removeAttr('disabled');
    var _obj = data.obj;
    BindData(_obj);
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        });
    ////--------


    $("#btnReport").click(function () {
        ClearData();
    let _date = $("#txtDateEntrySearch").val();
    if (_date == '') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
            }
    ReportDate = _date;
    var model = {
        'dDate': _date
            }
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/DayCloseReportSingleView",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);
    var _obj = data.obj;
    $("#btnViewModel").click();
    BindReportData(_obj);
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        });



    $("#btnSaveCust").click(function () {
        //
        let _date = $("#txtYear").val();
    if (_date == '') {
        $("#spInfo").html('<strong style="color:red">Invalid Date !!!</strong>');

    return;
            }

    let _prevBalance = $("#txtPrevBalance").val();
    let _balance = $("#txtBalance").val();
    let _totalSales = $("#txtTotalSell").val();
    let _totalPurchase = $("#txtTotalBuy").val();
    let _addLess = $("#txtAddLess").val();
    let _note = $("#txtNote").val();

    if (_prevBalance == '') {
        _prevBalance = '0';
            }

    if (_balance == '') {
        _balance = '0';
            }
    if (_totalSales == '') {
        _totalSales = '0';
            }
    if (_totalPurchase == '') {
        _totalPurchase = '0';
            }

    if (_addLess == '') {
        _addLess = '0';
            }
    var model = {
        'yearName': parseInt(_date),
    'prevBalance': parseFloat(_prevBalance),
    'addLess': parseFloat(_addLess),
    'totalSales': parseFloat(_totalSales),
    'totalPurchase': parseFloat(_totalPurchase),
    'balance': parseFloat(_balance),
    'note': _note

            }
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/SaveYearClose",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
                    // HideGateOverlay();

                    if (parseInt(data.resultID) > 0) {
        ClearData();


    $("#btnSaveCust").attr('data-editstatus', '0')
    console.log(data);
    tableCust.clear().draw();
    tableCust.rows.add(data.obj).draw();

    $("#spInfo").html('<strong style="color:blue">' + data.resultMessage + '</strong>');
                    }
    else {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }
                    //var _obj = data.obj;

                    //if (_obj != null) {
        //    BindData(_obj);
        //}
    },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        });









    });



    function SelectDayClose(_date) {
        ClearData();
    var model = {
        'yearName': _date
        }

    $("#btnSaveCust").attr('data-editstatus', '0')

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "DayClose/GetExistingYearClose",

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
    function ClearData() {
        $("#txtInvoiceNo").val('');
    $("#txtPrevBalance").val('');
    $("#txtTotalReceive").val('');
    $("#txtTotalPayment").val('');
    $("#txtAddLess").val('');
    $("#txtBalance").val('');



    $("#txtTotalSell").val('');
    $("#txtTotalBuy").val('');

    $("#txtTotalSellQty").val('');
    $("#txtTotalBuyQty").val('');

    $("#txtTotalSellQtyReturn").val('');
    $("#txtTotalSellReturn").val('');

    $("#txtNote").val('');
    $("#btnSaveCust").attr('disabled', 'disabled');
    }
    function BindData(_obj) {
        if (_obj != null) {


        $("#txtYear").val(_obj.yearName);
    $("#txtInvoiceNo").val(_obj.slNo);

    $("#txtDateEntry").val(_obj.dDateFormated);
    $("#txtPrevBalance").val(_obj.prevBalance);
    $("#txtTotalReceive").val(_obj.totalReceive);
    $("#txtTotalPayment").val(_obj.totalPayment);
    $("#txtAddLess").val(_obj.addLess);
    $("#txtBalance").val(_obj.balance);



    $("#txtTotalSell").val(_obj.totalSales);
    $("#txtTotalBuy").val(_obj.totalPurchase);

    $("#txtTotalSellQty").val(_obj.totalSalesQty);
    $("#txtTotalBuyQty").val(_obj.totalPurchaseQty);



    $("#txtTotalSellQtyReturn").val(_obj.totalSalesReturnAmount);
    $("#txtTotalSellReturn").val(_obj.totalSalesReturnQty);


    $("#txtNote").val(_obj.note);
        }
    }
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
        win.document.write('<title>Day_Close_' + ReportDate + '</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }


    function exportReport(tableName) {

        var downloadLink;
    var dataType = 'application/vnd.ms-excel';
    var tableSelect = document.getElementById(tableName);
    var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

    // Specify file name
    var filename = ReportDate + '.xls';

    // Create download link element
    downloadLink = document.createElement("a");

    document.body.appendChild(downloadLink);

    if (navigator.msSaveOrOpenBlob) {
            var blob = new Blob(['\ufeff', tableHTML], {
        type: dataType
            });
    navigator.msSaveOrOpenBlob(blob, filename);
        } else {
        // Create a link to the file
        downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

    // Setting the file name
    downloadLink.download = filename;

    //triggering the function
    downloadLink.click();

        }

    }
    function createPDFSales() {
        var sTable = document.getElementById('tabSales').innerHTML;

    var style = "<style>";
        //style = style + "table {width: 100%;font: 17px Calibri;}";
        //  style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
        style = style + "table, th, td {padding: 2px 3px;border-collapse: collapse;}";
        style = style + "</style>";

    // CREATE A WINDOW OBJECT.
    var win = window.open('', '', 'height=595,width=842');

    win.document.write('<html><head>');
        win.document.write('<title>Day_Close_' + ReportDate + '</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }
