
    var days = ['Saturday','Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday' ];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];


    var SALES_DATA = { }
    //var REGEXP_NUM = new RegExp("^[a-zA-Z0-9]+$");
    var specialChars = [
    '০',
    '১',
    '২',
    '৩',
    '৪',
    '৫',
    '৬',
    '৭',
    '৮',
    '৯',
    '0',
    '1',
    '2',
    '3',
    '4',
    '5',
    '6',
    '7',
    '8',
    '9',
    '.',
    '.',
    '-',
    '-'
    ];
    var BeagaliToEnglish = {
        '০':'0',
    '১': '1',
    '২': '2',
    '৩': '3',
    '৪': '4',
    '৫': '5',
    '৬': '6',
    '৭': '7',
    '৮': '8',
    '৯': '9',
    '0': '0',
    '1': '1',
    '2': '2',
    '3': '3',
    '4': '4',
    '5': '5',
    '6': '6',
    '7': '7',
    '8': '8',
    '9': '9',
    '.': '.',
    '.': '.',

    '-': '-',
    '-': '-'

    }

    var EnglisgToBengali = {
        '০': '০',
    '১':'১',
    '২': '২',
    '৩': '৩',
    '৪': '৪',
    '৫': '৫',
    '৬': '৬',
    '৭': '৭',
    '৮': '৮',
    '৯': '9',
    '0': '০',
    '1': '১',
    '2': '২',
    '3': '৩',
    '4': '৪',
    '5': '৫',
    '6': '৬',
    '7': '৭',
    '8': '৮',
    '9': '৯',
    '.': '.',
    '.': '.',
    '-': '-',
    '-': '-'

    }


    function ConvertBanglaToEnglish(_str) {
        var res = '';

    if (_str == '') return '';

    var _arr = _str.split('');
    for (var i = 0; i < _arr.length; i++) {
        res += BeagaliToEnglish[_arr[i]];
        }
    return res;
    }

    function ConvertEnglishToBengali(_str) {
        var _res = '';

    if (_str == '') return '';

    var _arr = _str.split('');
    for (var i = 0; i < _arr.length; i++) {
        _res += EnglisgToBengali[_arr[i]];
        }
    return _res;
    }

    $(document).ready(function () {


        var date = new Date();
    var today = date.getDate() + '-' + months[date.getMonth()] + '-' + date.getFullYear();


    $("#txtReturnDate").datepicker({
        autoclose: true,
        }).on('changeDate', function (e) {
        ChangeDt("#txtReturnDate");
        });



    $("#txtReturnDate").datepicker({
        autoclose: true,
        });


    $("#txtReturnDate").val(today);



    function ChangeDt(caller) {

        console.log(caller);
    let _thisVal2 = $(caller).val();

            if (_thisVal2.indexOf('-') > 0) {

        let _existValue = _thisVal2.split('-');
    $(caller).val(_existValue[0] + '-' + months[_existValue[1] - 1] + '-' + _existValue[2]);

            }



        };


    $("#btnAddNewCust").click(function () {
        ClearData();
        });


    $("#btnLoad").click(function () {

        LoadData();

        });

    $("#btnEdit").click(function () {

        LoadDataForEdit();

        });




    function LoadData() {
        ClearData();
    let _salesNo = ConvertBanglaToEnglish($("#txtBIllNo").val());
    if (_salesNo == '' || _salesNo == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Sales No</strong>');
    $("#txtBIllNo").focus();
    return;
            }
    var salesHead22 = {
        "GeneratedSalesNo": _salesNo
            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",

    url: WEB_URL + "Sales/GetSalesBySalesNo",
    data: JSON.stringify(salesHead22),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {

        console.log(data);
                    if (parseInt(data.resultID) > 0) {

        $("#spInfo").html('');

    SALES_DATA = data.obj;

    BindData();

                    }
    else {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }



                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        }




    function LoadDataForEdit() {
        ClearData();
    let _salesNo = ConvertBanglaToEnglish($("#txtBIllNoRet").val());
    if (_salesNo == '' || _salesNo == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Sales No</strong>');
    $("#txtBIllNo").focus();
    return;
            }
    var salesHead22 = {
        "GeneratedReturnNo": _salesNo
            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "SalesReturn/GetSalesReturnBySalesNoEdit",
    //url: WEB_URL + "Sales/GetSalesBySalesNo",
    data: JSON.stringify(salesHead22),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {

        console.log(data);
                    if (parseInt(data.resultID) > 0) {

        $("#spInfo").html('');

    SALES_DATA = data.obj;

    BindDataEdit();

                    }
    else {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }



                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        }







    function ClearData() {
        SALES_DATA = {}

            $("#txtBIllNoParty").val('');
    $("#txtCustName").val('');
    $("#txtShopName").val('');
    $("#txtArticleSize").val('');

    $("#txtTotalRetQty").val('0');
    $("#dlArticleSize option").remove();
    $("#spInfo").html('');
    $("#tblProdItem tr:gt(0)").remove();
    $("#tblReturnItem tr:gt(0)").remove();
    $("#txtShopName").val('');
    $("#btnSaveReturn").removeAttr('disabled');
        }



    function BindData() {
            var _order = SALES_DATA;

    $("#txtBIllNoParty").val(_order.custwiseSalesNo);
    $("#txtDate2").val(_order.salesDateFormated);
    if (_order.customer != null) {
        $("#txtCustName").val(_order.customer.name + '_' + _order.customer.customerID);
    $("#txtShopName").val(_order.customer.shopName + ', ' + _order.customer.address1);
            }
    $.each(_order.salesDetailsList, function (i, prodData) {
        $("#dlArticleSize").append('<option data-salesheadid=' + prodData.salesHeadID + '  data-salesdetailsid=' + prodData.salesDetailsID + '  value="' + prodData.article.name + ' X ' + prodData.size.name + '(' + (i + 1) + ')"/>');


            });



        }


    function BindDataEdit() {
            var _order = SALES_DATA;

    $("#txtBIllNoParty").val(_order.custwiseSalesNo);
    $("#txtDate2").val(_order.salesDateFormated);
    if (_order.customer != null) {
        $("#txtCustName").val(_order.customer.name + '_' + _order.customer.customerID);
    $("#txtShopName").val(_order.customer.shopName + ', ' + _order.customer.address1);
            }
    $.each(_order.salesDetailsList, function (i, prodData) {
        $("#dlArticleSize").append('<option data-salesheadid=' + prodData.salesHeadID + '  data-salesdetailsid=' + prodData.salesDetailsID + '  value="' + prodData.article.name + ' X ' + prodData.size.name + '(' + (i + 1) + ')"/>');

    if (prodData.salesReturnDetails != null) {

        $.each(prodData.salesReturnDetails, function (i, _ringleRetData) {

            BindSavedRetunDetails(_order.customer.customerID, _ringleRetData);

        });
    CalculateTotRetQty();

                }

            });



        }



    $("#txtArticleSize").on('input', function () {
        let _str = $("#txtArticleSize").val();
    let _salesId = GetDataID(_str, '#dlArticleSize', 'data-salesheadid')
    let _salesDetailsId = GetDataID(_str, '#dlArticleSize', 'data-salesdetailsid')
    //alert(_salesId+'___'+_salesDetailsId)
    if (_salesId == null || _salesId == undefined) {
               // alert(_salesId);
                return;
            }
    if (_salesDetailsId == null || _salesDetailsId == undefined) {
               // alert('Ret 2');
                return;
            }


    BindSalesDetails(_salesId, _salesDetailsId,'0','0');
           
        });



    $("#tblProdItem").on('input', 'tr td input[type="number"]', function () {

        let retQty = parseFloat($(this).closest('td').closest('tr').find("td").find('input[type="number"][data-inputfor="retQty"]').val());
    let retRate = parseFloat($(this).closest('td').closest('tr').find("td").find('input[type="number"][data-inputfor="retRate"]').val());
    let retCommRate = parseFloat($(this).closest('td').closest('tr').find("td").find('input[type="number"][data-inputfor="retComRate"]').val());

    let retAmount =retQty * retRate;

    let retComAmount = retQty *retCommRate;

    $(this).closest('td').closest('tr').children("td").eq(6).text(retAmount);
    $(this).closest('td').closest('tr').children("td").eq(7).text(retComAmount);

        });




    $("#tblProdItem").on('click', 'tr td input[type="button"]', function (e) {
        $("#spInfo").html('');

    var _retQty = parseFloat($(this).closest('td').closest('tr').find('td input[type="number"][data-inputfor="retQty"]').val());
    var _retRate = parseFloat($(this).closest('td').closest('tr').find('td input[type="number"][data-inputfor="retRate"]').val());
    var _retcomRate = parseFloat($(this).closest('td').closest('tr').find('td input[type="number"][data-inputfor="retComRate"]').val());


    var _salesDetID = $.trim($(this).attr('data-salesdetailsid'));
    var _salesHeadID = $.trim($(this).attr('data-salesheadid'));
    var _custid = $.trim($(this).attr('data-customerID'));
    var _retDetailsID = $.trim($(this).attr('data-salesreturndetailsid'));
    var _dupliocateFound = false;


    var _salesReturnID_EDIT = SALES_DATA["SalesReturnID_EDIT"];
    var _slesHeadID_EDIT = SALES_DATA["SalesHeadID_EDIT"];
    let _for = $(this).attr("data-for");

    if (_for == "Cancel") {
        $("#tblProdItem tr:gt(0)").remove();
            }
    else if (_for == "Add") {

                if (parseInt(_retDetailsID) > 0) {

        let salesReturnDet = {
        "SalesReturnDetailsID": _retDetailsID,
    "SalesDetailsID": _salesDetID,
    "ReyurnQtyInPair": _retQty,
    "RetRate": _retRate,
    "ReturnCommissionRate": _retcomRate


                    };



    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "SalesReturn/UpdateSalesReturnDetails",

    data: JSON.stringify(salesReturnDet),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {

        console.log(data);
                            if (parseInt(data.resultID) > 0) {

        $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');

    LoadDataForEdit();

                            }
    else {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                            }



                        },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                        }
                    });



                }

    else {





        $("#tblReturnItem tr:gt(0)").each(function (i, row) {
            if (_salesDetID == $.trim($(this).find('td[col="salesdetailsid"]').text())) {


                _dupliocateFound = true

            }
        });

    if (_dupliocateFound) {
        $("#spInfo").html('<strong style="color:red">This Item Already added !</strong>');

    return;
                    }

    if (isNaN(_retQty)) {
        $("#spInfo").html('<strong style="color:red">Invalid Qty!</strong>');
    return;
                    }
    if (parseFloat(_retQty) == 0) {
        $("#spInfo").html('<strong style="color:red">Invalid Qty!</strong>');
    return;
                    }



    if (_salesReturnID_EDIT == undefined || _slesHeadID_EDIT == undefined || _salesReturnID_EDIT == null || _slesHeadID_EDIT == null) {
        BindRetunDetails(_salesHeadID, _salesDetID, _custid, _retQty, 0, _retRate, _retcomRate);
                    }
    else {
        let salesReturnDet = {
        "SalesReturnDetailsID": _retDetailsID,
    "SalesDetailsID": _salesDetID,
    "ReyurnQtyInPair": _retQty,
    "SalesHeadID": _slesHeadID_EDIT,
    "SalesReturnID": _salesReturnID_EDIT,
    "RetRate": _retRate,
    "ReturnCommissionRate": _retcomRate

                        };



    $("#spInfo").html('<strong style="color:blue">Updaing Data,Pleas wait......!</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "SalesReturn/InsertSalesReturnDetails",

    data: JSON.stringify(salesReturnDet),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {

        console.log(data);
                                if (parseInt(data.resultID) > 0) {

        $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');

    LoadDataForEdit();

                                }
    else {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                                }



                            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                            }
                        });

                    }
                    
                }
            }

    else {
        alert('invalid click');
            }
        });

    function BindRetunDetails(_salesHeadID, _salesDetID, _custid, _retQty, _retDetID, _retRate,  _retcomRate) {
         

            var _obj = SALES_DATA.salesDetailsList;
       
            var _singleItem = _obj.find(item => item.salesDetailsID == _salesDetID);

    console.log(_singleItem);

    $("#tblReturnItem").append('<tr> <td col="custid" style="display:none">' + _custid + '</td><td col="salesdetailsid" style="display:none">' + _singleItem.salesDetailsID + '</td><td col="salesheadid" style="display:none">' + _salesHeadID + '</td> <td>' + _singleItem.prodName.name + '</td><td>' + _singleItem.article.name + '</td> <td>' + _singleItem.size.name + '</td><td col="retQty" style="background-color:darkolivegreen; color:#fff"> ' + _retQty + ' </td><td col="retRate">' + _retRate + '</td><td>' + (parseFloat(_retQty) * parseFloat(_retRate)) + '</td><td col="retComm">' + _retcomRate + '</td><td>' + (parseFloat(_retQty) * parseFloat(_retcomRate)) + '</td><td>' + (parseFloat(_retQty) * (parseFloat(_retRate) - parseFloat(_retcomRate))) + '</td><td><button data-salesreturndetailsid="' + _retDetID + '" data-salesheadid="' + _salesHeadID + '" data-salesdetailsid="' + _salesDetID + '" data-for="edit" class="mb-2 mr-2  btn-dashed btn btn-outline-dark"> Edit</button> </td><td> <button data-salesreturndetailsid="' + _retDetID + '" data-salesheadid="' + _salesHeadID + '" data-salesdetailsid="' + _salesDetID + '" data-for="delete" class="mb-2 mr-2  btn-dashed btn btn-outline-danger"> Delete</button> </td></tr>');
    CalculateTotRetQty();

        }

    function BindSavedRetunDetails(_custID,_singleRetItem) {

        $("#btnSaveReturn").attr('disabled', 'disabled');
    SALES_DATA["SalesReturnID_EDIT"] = _singleRetItem.salesReturnID;
    SALES_DATA["SalesHeadID_EDIT"] = _singleRetItem.salesHeadID;
    $("#tblReturnItem").append('<tr> <td col="custid" style="display:none">' + _custID + '</td><td col="salesdetailsid" style="display:none">' + _singleRetItem.salesDetailsID + '</td><td col="salesheadid" style="display:none">' + _singleRetItem.salesHeadID + '</td> <td>' + _singleRetItem.prodName.name + '</td><td>' + _singleRetItem.article.name + '</td> <td>' + _singleRetItem.size.name + '</td><td col="retQty" style="background-color:darkolivegreen; color:#fff"> ' + _singleRetItem.reyurnQtyInPair + ' </td><td col="retRate">' + _singleRetItem.retRate + '</td><td>' + (parseFloat(_singleRetItem.reyurnQtyInPair) * parseFloat(_singleRetItem.retRate)) + '</td><td col="retComm">' + _singleRetItem.returnCommissionRate + '</td><td>' + (parseFloat(_singleRetItem.reyurnQtyInPair) * parseFloat(_singleRetItem.returnCommissionRate)) + '</td><td>' + (parseFloat(_singleRetItem.reyurnQtyInPair) * parseFloat(_singleRetItem.retRate - _singleRetItem.returnCommissionRate)) + '</td><td> <button data-salesreturndetailsid="' + _singleRetItem.salesReturnDetailsID + '" data-salesdetailsid="' + _singleRetItem.salesDetailsID + '" data-salesheadid="' + _singleRetItem.salesHeadID + '" data-for="edit" class="mb-2 mr-2  btn-dashed btn btn-outline-dark"> Edit</button> </td><td>  <button data-salesreturndetailsid="' + _singleRetItem.salesReturnDetailsID + '" data-salesdetailsid="' + _singleRetItem.salesDetailsID + '" data-salesheadid="' + _singleRetItem.salesHeadID + '" data-for="delete" class="mb-2 mr-2  btn-dashed btn btn-outline-danger"> Delete</button></td></tr>');

    console.log(SALES_DATA);

        }




    $("#tblReturnItem").on('click', 'tr td button', function (e) {

        $("#spInfo").html('');
    //var thisTxtBox = $(this);
    e.preventDefault();
    //ClearControl();

    //alert('working in Progress');
    //var thisTxtBox = $(this);
    let _for = $(this).attr("data-for");
    let _salesreturnDetailsID = $(this).attr("data-salesreturndetailsid");
    let _salesDetailsID = $(this).attr("data-salesdetailsid");
    let _salesHeadID = $(this).attr("data-salesheadid");
    let _retQty = parseInt($(this).closest('td').closest('tr').find('td[col="retQty"]').text());

    let _retrtate = $(this).closest('td').closest('tr').find('td[col="retRate"]').text()
    let _retComm = $(this).closest('td').closest('tr').find('td[col="retComm"]').text()
    //alert(_retQty);
    if (_for == 'edit') {

        BindSalesDetails(_salesHeadID, _salesDetailsID, _salesreturnDetailsID, _retQty, _retrtate, _retComm)

    }
    if (_for == 'delete') {


                if (confirm('Are you sure to delete This Item?')) {
                    // Save it!
                    if (parseInt(_salesreturnDetailsID) > 0) {
        let salesReturnDet = {
        "SalesReturnDetailsID": _salesreturnDetailsID,
    "SalesDetailsID": _salesDetailsID
                      
                        };



    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "SalesReturn/DeleteSalesReturnLine",

    data: JSON.stringify(salesReturnDet),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {

        console.log(data);
                                if (parseInt(data.resultID) > 0) {

        $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');

    LoadDataForEdit();

                                }
    else {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                                }



                            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                            }
                        });






                    }
    else {

        $(this).parent("td").parent("tr").remove();
    CalculateTotRetQty();

                    }
                }
            }

        });

    function CalculateTotRetQty() {
            var _retQty = 0;
    var _retAmount = 0;

    $("#tblReturnItem tr:gt(0)").each(function () {
        _retQty += parseFloat($(this).children('td[col="retQty"]').text());


            });
    $("#txtTotalRetQty").val(_retQty.toFixed(2));
        }


    function BindSalesDetails(_salesId, _salesDetailsId, _retDetailsID, retQty, _retrtate, _retCommRate) {

        // alert(_salesDetailsId)
        $("#tblProdItem tr:gt(0)").remove();

    var _obj = SALES_DATA.salesDetailsList;
    console.log(_obj);
            var _singleItem = _obj.find(item => item.salesDetailsID == _salesDetailsId);

    console.log(_singleItem);
            let _text = parseInt(_retDetailsID) > 0 ? 'Update' : 'Add'
            let _retRate2 = parseInt(_retDetailsID) > 0 ? _retrtate : _singleItem.salesRate;
            let _retComRate2 = parseInt(_retDetailsID) > 0 ? _retCommRate : _singleItem.commissionRate;


    $("#tblProdItem").append('  <tr><td>' + _singleItem.prodName.name + '</td><td>' + _singleItem.article.name + '</td><td>' + _singleItem.size.name + '</td><td>' + _singleItem.salesQtyInPair + '</td><td controlname="comRate"> <input class="form-control-sm form-control" type="number" data-inputfor="retComRate" value="' + _retComRate2 + '" /></td><td controlname="retRate" > <input type="number" data-inputfor="retRate" class="form-control-sm form-control" value="' + _retRate2 + '" /></td><td controlname="retAmount">' + (retQty * _retRate2) + '</td><td controlname="retComAmount">' + (retQty * _retComRate2) + '</td><td controlname="reretQtyRate"><input class="form-control-sm form-control" type="number" data-inputfor="retQty" value="' + retQty + '" /></td><td><input data-for="Add" data-customerID=' + SALES_DATA.customer.customerID + ' data-salesreturndetailsid=' + _retDetailsID + ' data-salesheadid=' + _salesId + ' data-salesdetailsid=' + _singleItem.salesDetailsID + ' class="btn btn-gradient-success btn-block" type="button" value=" ' + _text + '" /></td> <td><input data-for="Cancel" class="btn btn-gradient-danger btn-block" type="button" value="Cancel" /></td></tr>');

        }
    $("#btnSaveReturn").on('click', function () {
        //SalesReturnID
        $("#spInfo").html('');


    if (SALES_DATA == null) {
        $("#spInfo").html('<strong style="color:red">invalid Data !</strong>');

    return;
            }
    if (SALES_DATA.customer == null || SALES_DATA.customer == undefined) {
        $("#spInfo").html('<strong style="color:red">invalid Customer !</strong>');

    return;

            }
    var retDetails = [];

    var _customerMissmatch = false;

    var singleRet = { }
    $("#tblReturnItem tr:gt(0)").each(function () {
                var _salesDet = parseInt($(this).children('td[col="salesdetailsid"]').text());
    var _salesHeadID = parseInt($(this).children('td[col="salesheadid"]').text());
    var _retQty = parseFloat($(this).children('td[col="retQty"]').text());
    var _custid = parseFloat($(this).children('td[col="custid"]').text());

    var _retQty = parseFloat($(this).children('td[col="retQty"]').text());

    var _retRate = parseFloat($(this).children('td[col="retRate"]').text());
    var _retComm = parseFloat($(this).children('td[col="retComm"]').text());

    if (parseInt(_custid) != parseInt(SALES_DATA.customer.customerID)) {

        _customerMissmatch = true;
                }
    var _salesDetailsID =
    singleRet = {
        'SalesReturnDetailsID': 0,
    'SalesDetailsID': _salesDet,
    'SalesHeadID': _salesHeadID,
                        //'SalesDetails': {
        //    'SalesDetailsID': _salesDet,

        //},
        'ReyurnQtyInPair': _retQty,
    "RetRate": _retRate,
    "ReturnCommissionRate":_retComm,

                    }

    retDetails.push(singleRet);
            });

    var salesReturn = {
        'ReturnDate': $("#txtReturnDate").val(),
    "ReturnNo": SALES_DATA.generatedSalesNo2,
    'Customer': {
        'CustomerID': SALES_DATA.customer.customerID,
                  
                },
    'SalesReturnDetailsList': retDetails
            }

    if (_customerMissmatch) {
        $("#spInfo").html('<strong style="color:red">Customer Missmatch !</strong>');

    return;
            }

    if (retDetails == null || retDetails == undefined) {
        $("#spInfo").html('<strong style="color:red">No data found !</strong>');

    return;

            }


    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "SalesReturn/SaveSalesReturn",

    data: JSON.stringify(salesReturn),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data);
                    if (parseInt(data.resultID) > 0) {

        $("#spInfo").html('');

    $("#txtBIllNoRet").val(data.resultNo);

    $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');

                    }
    else {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }



                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });


        });


    function GetDataValue(value, dataList) {
            var z = $(dataList);
    var val = $(z).find('option[data-id="' + value + '"]');
    var endval = val.attr('value');
    return endval;
        }
    function GetDataID(value, dataList,dataField) {
            var z = $(dataList);
    var val = $(z).find('option[value="' + value + '"]');
    var endval = val.attr(dataField);
    return endval;
        }
    $("#btnView").click(function () {



        $("#spHeaderBill").text('');
    $("#spShortDes").text('');
    $("#spPropritor").text('');
    $("#spAddressBill").text('');
    $("#spOrderNoBill").text('');
    $("#spPartyBill").text('');
    $("#spAprtyAddressBill").text('');
    $("#spDateBill").text('');
    $("#spSalesRepresentative").text('');
    $("#tblOrderBill tr:gt(9)").remove();



    $("#spInfo").html('');
    let _salesNo = ConvertBanglaToEnglish($("#txtBIllNoRet").val());

    console.log(_salesNo);
    if (_salesNo == '' || _salesNo == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Return No No</strong>');
    $("#txtBIllNoRet").focus();
    return;
            }
    var salesHead22 = {
        "GeneratedReturnNo": _salesNo
            }
    BillNo_PDF = "Return_Bill_No_" + _salesNo;
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "SalesReturn/GetSalesRetirnBySalesNo",

    data: JSON.stringify(salesHead22),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
                    if (parseInt(data.resultID) > 0) {
        console.log(data);

    $("#spInfo").html('');

    $("#btnViewModel").click();

    BindDataReport(data.obj);

                    }
    else {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }



                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        });

    function BindDataReport(_obj) {
        //
        $("#spHeaderBill").text(_obj.company.name);
    $("#spShortDes").text('(' + _obj.company.shortDescription + ')');
    $("#spPropritor").text('প্রোঃ' + _obj.company.proprietor);
    $("#spAddressBill").text(_obj.company.shopAddress);
    $("#spAddressMobile").text(_obj.company.mobileNo1 + ' , ' + _obj.company.mobileNo2);
    $("#spOrderNoBill").text(_obj.generatedReturnNo2);
    $("#spDateBill").text(_obj.returnDateFormated);
    $("#spcustwiseSalesNo").text(_obj.returnNo);

    $("#spPartyBill").text(_obj.customer.name + '(' + _obj.customer.customerNo + ')');
    $("#spPartyshopNameBill").text(_obj.customer.shopName);
    $("#spPartyshopAddressBill").text(_obj.customer.address1);




    var _totNeatAmount = 0;
    var _totAmount = 0;
    var _totCommAmount = 0;
    var _totretQty = 0;
    $(_obj.salesReturnDetailsList).each(function (i, _singleObj) {

        $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" controlname="SlNo">' + (i + 1) + '</td> <td  class="TDRightBorder" controlname="ProdName">' + _singleObj.prodName.name + '</td> <td class="TDRightBorder" controlname="ArticleName">' + _singleObj.article.name + '</td> <td class="TDRightBorder" controlname="SizeName">' + _singleObj.size.name + '</td>  <td class="TDRightBorder" controlname="PairQty">' + _singleObj.reyurnQtyInPair + '</td><td class="TDRightBorder" style="text-align:right; controlname="SellPrice">' + _singleObj.retRate + '</td><td class="TDRightBorder" style="text-align:right; controlname="Comm"> ' + _singleObj.returnCommissionRate + '</td><td class="TDRightBorder" style="text-align:right;" controlname="TotPrice">' + (_singleObj.retAmount - _singleObj.returnCommissionAmount) + '</td> </tr>');
    _totNeatAmount += parseFloat(_singleObj.retAmount) - parseFloat(_singleObj.returnCommissionAmount);

    _totAmount += _singleObj.retAmount;
    _totCommAmount += _singleObj.returnCommissionAmount;
    _totretQty += _singleObj.reyurnQtyInPair;
            });
    $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="4" style="text-align:right;">মোট:</td><td class="TDRightBorder" style="text-align:right;">' + _totretQty.toFixed(2) + '</td><td colspan="2" class="TDRightBorder" style="text-align:right;"></td><td class="TDRightBorder" style="text-align:right;">' + _totNeatAmount.toFixed(2) + '</td></tr>');

         

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
        win.document.write('<title>' + BillNo_PDF+'</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }
