
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

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
        '০': '0',
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
    '১': '১',
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
    //$("#txtPair").bind("keypress", function (event) {
    //    // prevent if in array
    //    if ($.inArray(event.which, specialChars) == -1) {
    //        event.preventDefault();
    //    }
    //});
    //alert(ConvertBanglaToEnglish('৫৪৩৮৯০'));
    var BillNo_PDF = '';
    var CUST_LIST = [];
    var CUST_CAT = [];
    var SUB_CUST_CAT = [];
    var PROD_NAMES = [];
    var COMP_PRODUCTS = [];
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

    $("#txtCheckPasDate").datepicker({
        autoclose: true,
        }).on('changeDate', function (e) {
        ChangeDt("#txtCheckPasDate");
        });

    $("#txtDate").datepicker({
        autoclose: true,
        });

    $("#txtDate2").val(today);
    $("#txtDate").val(today);

    $("#txtCheckPasDate").val(today);

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
    url: WEB_URL + "Sales/GetSalesEntryViewModel",

    // data: JSON.stringify(receiveMain),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();
        $("#spInfo").html('');
    console.log(data);
    //RECEIVE = data;
    //BindProduct(data);
    //$("#spInfo").html('');
    CUST_CAT = data.customerCategorys;
    CUST_LIST = data.customers;
    SUB_CUST_CAT = data.customerSubCategorys;
    PROD_NAMES = data.prodNames;
    BindCustCategoryName(data.customerCategorys);
    BindProdName(data.prodNames);
    BindPaymentMedium(data.paymentMediums)
                    // BinSubCategoryTable(data.customerSubCategorys);


                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        }

    // ---id="btnEdit" btnViewModel
    //id="btnView"

    $("input").on('keypress', function (event) {

            // alert('ok');
            var _currentTab = $(this).attr("data-tab");
    var _id = $(this).attr("id");

    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
                if (_currentTab != undefined) {
                    if (_id !== 'btnAdd') {
                        var _nextTab = parseInt(_currentTab) + 1;
    $('input[data-tab="' + _nextTab + '"]').focus();
                    }
                }
            }


        });



    $("#btnView").click(function () {

            var _notViewBalance = $("#chkBalance").is(':checked');

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
    let _salesNo = ConvertBanglaToEnglish($("#txtBIllNo").val());
    if (_salesNo == '' || _salesNo == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Sales No</strong>');
    $("#txtBIllNo").focus();
    return;
            }
    var salesHead22 = {
        "GeneratedSalesNo": _salesNo
            }
    BillNo_PDF = "Sales_Bill_No_" + _salesNo;
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Sales/GetSalesBySalesNo",

    data: JSON.stringify(salesHead22),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
                    if (parseInt(data.resultID) > 0) {
        console.log(data);

    $("#spInfo").html('');

    $("#btnViewModel").click();

    BindDataReport(data.obj, _notViewBalance);

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



    function BindDataReport(_obj, notViewBal) {
        //
        $("#spHeaderBill").text(_obj.company.name);
    $("#spShortDes").text('(' + _obj.company.shortDescription + ')');
    $("#spPropritor").text('প্রোঃ' + _obj.company.proprietor);
    $("#spAddressBill").text(_obj.company.shopAddress);
    $("#spAddressMobile").text(_obj.company.mobileNo1 + ' , ' + _obj.company.mobileNo2);
    $("#spOrderNoBill").text(_obj.generatedSalesNo2);
    $("#spDateBill").text(_obj.salesDateFormated);
    $("#spcustwiseSalesNo").text(_obj.custwiseSalesNo);
    if (_obj.customer != null) {
        $("#spPartyBill").text(_obj.customer.name + '(' + _obj.customer.customerNo + ')');
    $("#spPartyshopNameBill").text(_obj.customer.shopName);
    $("#spPartyshopAddressBill").text(_obj.customer.address1);
            }
    else {
        $("#spPartyBill").text('Cash');
    $("#spPartyshopNameBill").text('');
    $("#spPartyshopAddressBill").text('');
            }



    var _totNeatAmount = 0;
    var _totAmount = 0;
    var _totCommAmount = 0;
    $(_obj.salesDetailsList).each(function (i, _singleObj) {

        $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" controlname="SlNo">' + (i + 1) + '</td> <td  class="TDRightBorder" controlname="ProdName">' + _singleObj.prodName.name + '</td> <td class="TDRightBorder" controlname="ArticleName">' + _singleObj.article.name + '</td> <td class="TDRightBorder" controlname="SizeName">' + _singleObj.size.name + '</td>  <td class="TDRightBorder" controlname="PairQty">' + _singleObj.salesQtyInPair + '</td><td class="TDRightBorder" style="text-align:right; controlname="SellPrice">' + _singleObj.salesRate + '</td><td class="TDRightBorder" style="text-align:right; controlname="Comm"> ' + _singleObj.commissionRate + '</td><td class="TDRightBorder" style="text-align:right;" controlname="TotPrice">' + (_singleObj.salesAmount - _singleObj.commissionAmount) + '</td> </tr>');


    _totNeatAmount += parseFloat(_singleObj.salesAmount) - parseFloat(_singleObj.commissionAmount);
    _totAmount += _singleObj.salesAmount;
    _totCommAmount += _singleObj.commissionAmount;

            });
    $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">মোট:</td><td class="TDRightBorder" style="text-align:right;">' + _totNeatAmount.toFixed(2) + '</td></tr>');

    $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">পরিবহন/পেকিং খরচ(+):</td><td class="TDRightBorder" style="text-align:right;">' + _obj.transportCost.toFixed(2) + '</td></tr>');
    $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="4" style="text-align:right;">মোট বস্তা:</td><td class="TDRightBorder" style="text-align:right;">' + _obj.totalSackNo.toFixed(2) + '</td><td colspan="2" class="TDAllBorder" style="text-align:right;">	মোট বস্তা খরচ(+):</td><td class="TDRightBorder" style="text-align:right;">' + _obj.totalSackNoFee.toFixed(2) + '</td></tr>');
    if (!notViewBal) {
        $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;"> পূর্বের  বাকি টাকা :</td><td class="TDRightBorder" style="text-align:right;">' + _obj.previousBalance.toFixed(2) + '</td></tr>');
            }
    $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">গ্রহন টাকা ( ' + _obj.paymentMedium.name + ' )(-):</td><td class="TDRightBorder" style="text-align:right;">' + _obj.receiveAmount.toFixed(2) + '</td></tr>');
    $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;"> বেশী/ কম:</td><td class="TDRightBorder" style="text-align:right;">' + _obj.addLessAmount.toFixed(2) + '</td></tr>');

    if (notViewBal) {


        $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">মোট বাকি টাকা :</td><td class="TDRightBorder" style="text-align:right;">' + (_totNeatAmount + _obj.transportCost + _obj.totalSackNoFee + _obj.addLessAmount - _obj.receiveAmount).toFixed(2) + '</td></tr>');

            }
    else {
        $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">মোট বাকি টাকা :</td><td class="TDRightBorder" style="text-align:right;">' + (_obj.previousBalance + _totNeatAmount + _obj.transportCost + _obj.totalSackNoFee + _obj.addLessAmount - _obj.receiveAmount).toFixed(2) + '</td></tr>');

            }

    $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black" colspan="8"> &nbsp;</td> </tr>');
    $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black" colspan="8"> &nbsp;</td> </tr>');

    $("#tblOrderBill").append('<tr> <td style="border-left:1px solid black; text-align:center" colspan="3"> Received By</td> <td colspan="2"> &nbsp;</td><td style="border-right:1px solid black;text-align:center" colspan="3">Prepared By</td></tr>');

    $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black" colspan="8"> &nbsp;</td> </tr>');

    $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black;border-bottom:1px solid black" colspan="8"> &nbsp;</td> </tr>');



        }

    $("#btnViewA5").click(function () {

            var _notViewBalance = $("#chkBalance").is(':checked');

    $("#spHeaderBillA5").text('');

    $("#spShortDesA5").text('');

    $("#spPropritorA5").text('');


    $("#spAddressBillA5").text('');
    $("#spOrderNoBillA5").text('');
    $("#spPartyBillA5").text('');
    $("#spAprtyAddressBillA5").text('');
    $("#spDateBillA5").text('');
    $("#spSalesRepresentativeA5").text('');
    $("#tblOrderBillA5 tr:gt(9)").remove();



    $("#spInfo").html('');
    let _salesNo = ConvertBanglaToEnglish($("#txtBIllNo").val());
    if (_salesNo == '' || _salesNo == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Sales No</strong>');
    $("#txtBIllNo").focus();
    return;
            }
    var salesHead22 = {
        "GeneratedSalesNo": _salesNo
            }
    BillNo_PDF = "Sales_Bill_No_" + _salesNo;
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Sales/GetSalesBySalesNo",

    data: JSON.stringify(salesHead22),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
                    if (parseInt(data.resultID) > 0) {
        console.log(data);

    $("#spInfo").html('');

    $("#btnViewModelA5").click();

    BindDataReportA5(data.obj, _notViewBalance);

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




    function BindDataReportA5(_obj, notViewBal) {
        //
        $("#spHeaderBillA5").text(_obj.company.name);
    $("#spAddressBillA5").text(_obj.company.shopAddress);
    $("#spShortDesA5").text('('+ _obj.company.shortDescription+')');
    $("#spPropritorA5").text('প্রোঃ' + _obj.company.proprietor);
    $("#spAddressMobileA5").text(_obj.company.mobileNo1 + ' , ' + _obj.company.mobileNo2);
    $("#spOrderNoBillA5").text(_obj.generatedSalesNo2);
    $("#spDateBillA5").text(_obj.salesDateFormated);
    $("#spcustwiseSalesNoA5").text(_obj.custwiseSalesNo);
    if (_obj.customer != null) {
        $("#spPartyBillA5").text(_obj.customer.name + '(' + _obj.customer.customerNo + ')');
    $("#spPartyshopNameBillA5").text(_obj.customer.shopName);
    $("#spPartyshopAddressBillA5").text(_obj.customer.address1);
            }
    else {
        $("#spPartyBillA5").text('Cash');
    $("#spPartyshopNameBillA5").text('');
    $("#spPartyshopAddressBillA5").text('');
            }



    let _totNeatAmountA5 = 0;
    let _totAmountA5 = 0;
    let _totCommAmountA5 = 0;
    let _totQtyA5 = 0;
    $(_obj.salesDetailsList).each(function (i, _singleObj) {

        $("#tblOrderBillA5").append('<tr>  <td class="TDAllBorder" controlname="SlNo">' + (i + 1) + '</td> <td  class="TDRightBorder" controlname="ProdName">' + _singleObj.prodName.name + '</td> <td class="TDRightBorder" controlname="ArticleName">' + _singleObj.article.name + '</td> <td class="TDRightBorder" controlname="SizeName">' + _singleObj.size.name + '</td>  <td class="TDRightBorder" controlname="PairQty">' + _singleObj.salesQtyInPair + '</td><td class="TDRightBorder" style="text-align:right; controlname="SellPrice">' + _singleObj.salesRate + '</td><td class="TDRightBorder" style="text-align:right; controlname="Comm"> ' + _singleObj.commissionRate + '</td><td class="TDRightBorder" style="text-align:right;" controlname="TotPrice">' + (_singleObj.salesAmount - _singleObj.commissionAmount) + '</td> </tr>');
    _totNeatAmountA5 += parseFloat(_singleObj.salesAmount) - parseFloat(_singleObj.commissionAmount);
    _totAmountA5 += _singleObj.salesAmount;
    _totCommAmountA5 += _singleObj.commissionAmount;
    _totQtyA5 += _singleObj.salesQtyInPair;

            });
    $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="4" style="text-align:right;">মোট:</td><td class="TDRightBorder" style="text-align:right;">' + _totQtyA5.toFixed(2) + '</td><td class="TDRightBorder"></td><td class="TDRightBorder"></td><td class="TDRightBorder" style="text-align:right;border-bottom:2px solid black">' + _totNeatAmountA5.toFixed(2) + '</td></tr>');

    $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">পরিবহন/পেকিং খরচ(+):</td><td class="TDRightBorder" style="text-align:right;">' + _obj.transportCost.toFixed(2) + '</td></tr>');
    $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="4" style="text-align:right;">মোট বস্তা:</td><td class="TDRightBorder" style="text-align:right;">' + _obj.totalSackNo.toFixed(2) + '</td><td colspan="2" class="TDAllBorder" style="text-align:right;">	মোট বস্তা খরচ(+):</td><td class="TDRightBorder" style="text-align:right;">' + _obj.totalSackNoFee.toFixed(2) + '</td></tr>');
    $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;"> মোট টাকা :(=)</td><td class="TDRightBorder" style="text-align:right;">' + (_totAmountA5 + _obj.transportCost + _obj.totalSackNoFee) + '</td></tr>');
    $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder">নোট :</td><td class="TDAllBorder" colspan="3" style="text-align:right;">বেশী/ কম:</td><td class="TDRightBorder" style="text-align:right;">' + _obj.addLessAmount.toFixed(2) + '</td><td colspan="2" class="TDAllBorder" style="text-align:right;">	কমিশন(-):</td><td class="TDRightBorder" style="text-align:right;">' + (-_obj.addLessAmount + _totCommAmountA5).toFixed(2) + '</td></tr>');


    $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="5" rowspan="2" style="text-align:left;">' + _obj.note1 + '</td> <td class="TDAllBorder" colspan="2" style="text-align:right;"> সর্বমোট টাকা :(=)</td><td class="TDRightBorder" style="text-align:right;">' + (_totAmountA5 + _obj.transportCost + _obj.totalSackNoFee + _obj.addLessAmount - _totCommAmountA5).toFixed(2) + '</td></tr>');
    $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="2" style="text-align:right;"> পণ্য ফেরত (-):</td><td class="TDRightBorder" style="text-align:right;">0</td></tr>');
    if (!notViewBal) {
        $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;"> পূর্বের  বাকি টাকা :</td><td class="TDRightBorder" style="text-align:right;">' + _obj.previousBalance.toFixed(2) + '</td></tr>');
            }
    $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">গ্রহন টাকা ( ' + _obj.paymentMedium.name + ' )(-):</td><td class="TDRightBorder" style="text-align:right;">' + _obj.receiveAmount.toFixed(2) + '</td></tr>');


    if (notViewBal) {


        $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">মোট বাকি টাকা :</td><td class="TDRightBorder" style="text-align:right;">' + (_totNeatAmountA5 + _obj.transportCost + _obj.totalSackNoFee + _obj.addLessAmount - _obj.receiveAmount).toFixed(2) + '</td></tr>');

            }
    else {
        $("#tblOrderBillA5").append('<tr> <td class="TDAllBorder" colspan="7" style="text-align:right;">মোট বাকি টাকা :</td><td class="TDRightBorder" style="text-align:right;">' + (_obj.previousBalance + _totNeatAmountA5 + _obj.transportCost + _obj.totalSackNoFee + _obj.addLessAmount - _obj.receiveAmount).toFixed(2) + '</td></tr>');

            }
    $("#tblOrderBillA5").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black" colspan="8"> &nbsp;</td> </tr>');
    $("#tblOrderBillA5").append('<tr> <td colspan="3" style="text-align:right;border-left:1px solid black"">বিক্রয় প্রতিনিধি :</td><td  colspan="2" style="text-align:center;">' + _obj.user.fullName + '</td><td colspan="3" style="text-align:right; ;border-right:1px solid black""></td></tr>');

$("#tblOrderBillA5").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black"   colspan="8"> &nbsp;</td> </tr>');
$("#tblOrderBillA5").append('<tr> <td  colspan="2" style="text-align:right; ;border-left:1px solid black;;border-bottom:1px solid black"></td><td  colspan="2" style="text-align:center; ;border-bottom:1px solid black"></td><td class="TDRightBorder" colspan="4" style="text-align:center;">বিক্রেতার স্বাক্ষর </td></tr>');



        }

$("#btnEdit").click(function () {

    $("#spInfo").html('');
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
            if (data.resultID == -1) {
                ClearControlAfterSave();
                $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
            }
            else {
                $("#btnSaveCust").removeAttr('disabled');
                $("#btnAdd").removeAttr('disabled');
                $("#txtBIllNo").val(data.obj.generatedSalesNo);
                $("#spInfo").html('');
                ClearControlAfterSave();

                $("#btnSaveCust").val('Update');
                BindData(data.obj, '');


            }





        },
        error: function (a, b, c) {
            //  HideGateOverlay();
            alert(a, c);
        }
    });



});


$("#btnNext").click(function () {

    $("#spInfo").html('');
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
        url: WEB_URL + "Sales/GetSalesBySalesNoNext",

        data: JSON.stringify(salesHead22),
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        success: function (data) {

            if (data.resultID == -1) {
                ClearControlAfterSave();
                $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
            }
            else {
                $("#btnSaveCust").removeAttr('disabled');
                $("#btnAdd").removeAttr('disabled');
                $("#txtBIllNo").val(data.obj.generatedSalesNo);
                $("#spInfo").html('');
                ClearControlAfterSave();

                $("#btnSaveCust").val('Update');
                BindData(data.obj, '');


            }
        },
        error: function (a, b, c) {
            //  HideGateOverlay();
            alert(a, c);
        }
    });



});



$("#btnPrev").click(function () {

    $("#spInfo").html('');
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
        url: WEB_URL + "Sales/GetSalesBySalesNoPrev",

        data: JSON.stringify(salesHead22),
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);

            if (data.resultID == -1) {
                ClearControlAfterSave();
                $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
            }
            else {
                $("#btnSaveCust").removeAttr('disabled');
                $("#btnAdd").removeAttr('disabled');
                $("#txtBIllNo").val(data.obj.generatedSalesNo);
                $("#spInfo").html('');
                ClearControlAfterSave();

                $("#btnSaveCust").val('Update');
                BindData(data.obj, '');


            }


        },
        error: function (a, b, c) {
            //  HideGateOverlay();
            alert(a, c);
        }
    });



});




$("#btnAdd").click(function () {

    $("#spInfo").html('');

    let _salesHeadID3 = $("#btnSaveCust").attr("data-salesheadid");
    //let _salesHeadID3 = $(this).attr("data-salesheadid");
    let _salesDetailsID3 = $(this).attr("data-salesdetailsid");

    let _rowSlNo = $(this).attr('data-slno');

    let _comProductID = $(this).attr('data-companyproductid');

    let _prodName = $("#txtProName").val();
    let _articleName = $("#txtArcticle").val();
    let _sizeName = $("#txtSize").val();

    let _uomName = $("#txtUOM").val();

    let _currentStock = $("#txtCurrentStock").val();

    let _pairQty = ConvertBanglaToEnglish($("#txtPair").val());
    let _dozenQty = ConvertBanglaToEnglish($("#txtDozen").val());
    let _comm = ConvertBanglaToEnglish($("#txtComm").val());

    let _sellPrice = ConvertBanglaToEnglish($("#txtSellPrice").val());


    let _totcomm = ConvertBanglaToEnglish($("#txtTotalComm").val());

    let _totsellPrice = ConvertBanglaToEnglish($("#txtTotalSellPrice").val());

    if (_comm == '') {
        _comm = '0';
    }

    if (_currentStock == '') {
        _currentStock = '0';
    }
    if (_comProductID == undefined || _comProductID == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Item.</strong>');
        return;
    }
    if (parseInt(_comProductID) < 1) {
        $("#spInfo").html('<strong style="color:red">Invalid Item..</strong>');
        return;
    }

    if (_prodName == '' || _prodName == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Item Name</strong>');
        return;
    }

    if (_articleName == '' || _articleName == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Article</strong>');
        return;
    }


    if (_sizeName == '' || _sizeName == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Size</strong>');
        return;
    }

    if (_uomName == '' || _uomName == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Unit name</strong>');
        return;
    }
    if (_pairQty == '' || _pairQty == null || isNaN(_pairQty)) {
        $("#spInfo").html('<strong style="color:red">Invalid Qty</strong>');
        $("#txtPair").focus();
        return;
    }

    if (parseFloat(_pairQty) <= 0) {
        $("#spInfo").html('<strong style="color:red">Invalid Qty</strong>');
        $("#txtPair").focus();
        return;
    }

    if (_sellPrice == '' || _sellPrice == null || isNaN(_sellPrice)) {
        $("#spInfo").html('<strong style="color:red">Invalid Price</strong>');
        $("#txtPair").focus();
        return;
    }

    if (parseFloat(_sellPrice) <= 0) {
        $("#spInfo").html('<strong style="color:red">Invalid Price</strong>');
        $("#txtSellPrice").focus();
        return;
    }

    console.log('Com Product ID ,' + _comProductID);
    let _singleObj = COMP_PRODUCTS.find(_prod => _prod.companyProductID == _comProductID);
    if (_singleObj == undefined || _singleObj == null) {
        $("#spInfo").html('<strong style="color:red">Invalid Item...</strong>');

        return;

    }


    if (parseInt(_salesHeadID3) > 0) {

        var salesDetails = {
            "SalesDetailsID": _salesDetailsID3,
            "SalesHeadID": _salesHeadID3,
            "CompanyProductID": _comProductID,
            "SalesQtyInPair": _pairQty,
            "CommissionRate": _comm,
            "SalesRate": _sellPrice
        }


        $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
        $.ajax({
            type: "POST",
            url: WEB_URL + "Sales/InsertSalesDetails",

            data: JSON.stringify(salesDetails),
            dataType: "JSON",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                // HideGateOverlay();
                if (parseInt(data.resultID) > 0) {
                    $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');
                    console.log(data);
                    clearItemAfterAdd();


                    $("#btnSaveCust").removeAttr('disabled');
                    $("#btnAdd").removeAttr('disabled');
                    $("#txtBIllNo").val(data.obj.generatedSalesNo);
                    $("#spInfo").html('');
                    ClearControlAfterSave();

                    $("#btnSaveCust").val('Update');
                    BindData(data.obj, '');

                    $("#txtProName").focus();
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



        if (parseInt(_rowSlNo) > 0) {

            $("#tblProdItem tr:gt(0)").each(function (i, row) {

                let _currentSL = parseFloat($(this).find('td[controlname="SlNo"]').text());

                if (_currentSL == _rowSlNo) {

                    $(this).html('');

                    $(this).html('<td style="display:none;" controlname="CommProductID">' + _comProductID + '</td> <td controlname="SlNo">' + _rowSlNo + '</td> <td  controlname="ProdName">' + _singleObj.prodName.name + '</td> <td controlname="ArticleName">' + _singleObj.article.name + '</td> <td></td><td controlname="SizeName">' + _singleObj.size.name + '</td> <td controlname="CurrentStock">' + _currentStock + '</td><td  controlname="UOMName">' + _uomName + '</td> <td controlname="DozenQty">' + _dozenQty + '</td><td controlname="PairQty">' + _pairQty + '</td><td controlname="Comm"> ' + _comm + '</td><td controlname="SellPrice">' + _sellPrice + '</td><td controlname="TotPrice">' + _totsellPrice + '</td> <td controlname="TotComm">' + _totcomm + '</td><td><button data-salesheadid="0" data-salesdetailsid="0" data-companyproductid="' + _comProductID + '" data-slno="' + _rowSlNo + '" data-for="edit" class="mb-2 mr-2 btn-dashed btn btn-outline-info"> Edit</button></td> <td><button   data-salesheadid="0" data-salesdetailsid="0" data-companyproductid="' + _comProductID + '" data-slno="' + _rowSlNo + '"  data-for="delete" class="mb-2 mr-2  btn-dashed btn btn-outline-danger"> Delete</button></td>');
                }

            });
        }
        else {
            let _slNo = $('#tblProdItem').find('tr').length;

            $("#tblProdItem").append('<tr> <td style="display:none;" controlname="CommProductID">' + _comProductID + '</td> <td controlname="SlNo">' + _slNo + '</td> <td  controlname="ProdName">' + _singleObj.prodName.name + '</td> <td controlname="ArticleName">' + _singleObj.article.name + '</td> <td></td><td controlname="SizeName">' + _singleObj.size.name + '</td> <td controlname="CurrentStock">' + _currentStock + '</td><td  controlname="UOMName">' + _uomName + '</td> <td controlname="DozenQty">' + _dozenQty + '</td><td controlname="PairQty">' + _pairQty + '</td><td controlname="Comm"> ' + _comm + '</td><td controlname="SellPrice">' + _sellPrice + '</td><td controlname="TotPrice">' + _totsellPrice + '</td> <td controlname="TotComm">' + _totcomm + '</td><td><button data-salesheadid="0" data-salesdetailsid="0" data-companyproductid="' + _comProductID + '" data-slno="' + _slNo + '" data-for="edit" class="mb-2 mr-2 btn-dashed btn btn-outline-info"> Edit</button></td> <td><button   data-salesheadid="0" data-salesdetailsid="0" data-companyproductid="' + _comProductID + '" data-slno="' + _slNo + '"  data-for="delete" class="mb-2 mr-2  btn-dashed btn btn-outline-danger"> Delete</button></td></tr>');
            $("#txtProName").focus();

        }
        CalCulateTotalAmount();
        CalculateGrandTotal();
        CalculateDueAmount();
        clearItemAfterAdd();
    }
});

$("#btnAddNewCust").click(function () {

    $("#btnSaveCust").removeAttr('disabled');

    clearItemAfterAdd();
    ClearControlAfterSave();


    CalculateSingleItemAmount();
    CalCulateTotalAmount();
    CalCulateTotalAmount();

});



$("#btnSaveCust").click(function () {

    $("#btnSaveCust").attr('disabled', 'disabled');


    $("#spInfo").html('');
    if ($("#txtPackCost").val() == '') {
        $("#txtPackCost").val('0');
    }
    if ($("#txtBostaQty").val() == '') {
        $("#txtBostaQty").val('0');
    }
    if ($("#txtBostaAmount").val() == '') {
        $("#txtBostaAmount").val('0');
    }

    if ($("#txtAddLess").val() == '') {
        $("#txtAddLess").val('0');
    }

    let _ssalesHeadID = $(this).attr('data-salesheadid');
    let _isCashSales = $("#chkCashSales").is(':checked');

    let _salesDate = $("#txtDate2").val();
    let _transportCost = ConvertBanglaToEnglish($("#txtPackCost").val());


    let _payName2 = $("#txtPaymentMedium").val();
    let _paymentID = GetDataID(_payName2, "#dlPaymentMedium");


    let _accNo = $("#txtAccNo").val();

    let _chkTTNo = $("#txtCheckTTNo").val();

    let _chkPassDate = $("#txtCheckPasDate").val();

    let _todayJoma = ConvertBanglaToEnglish($("#txtTodayJoma").val());
    let _bostaQty = ConvertBanglaToEnglish($("#txtBostaQty").val());
    let _bostaAmount = ConvertBanglaToEnglish($("#txtBostaAmount").val());
    let _addLess = ConvertBanglaToEnglish($("#txtAddLess").val());

    if (_todayJoma == '') {
        _todayJoma = "0";
    }

    let _note1 = $("#txtNote1").val();
    console.log(_isCashSales);

    if (_paymentID == undefined || _paymentID == null || _paymentID == '') {

        $("#spInfo").html('<strong style="color:red">Invalid Payment Medium!</strong>');

        $("#txtPaymentMedium").focus();
        $("#btnSaveCust").removeAttr('disabled');
        return;
    }

    let _custName2 = $("#txtCustName").val();
    let _custNameID2 = GetDataID(_custName2, "#dltxtCustName");
    if (!_isCashSales) {

        if (_custNameID2 == undefined || _custNameID2 == null || _custNameID2 == '') {

            $("#spInfo").html('<strong style="color:red">Invalid Customer !</strong>');

            $("#txtCustName").focus();
            $("#btnSaveCust").removeAttr('disabled');
            return;
        }

    }

    if ($("#txtGrandTotal").val() == '') {
        $("#spInfo").html('<strong style="color:red">Amount Missmatch !</strong>');

        $("#txtAddLess").focus();
        $("#btnSaveCust").removeAttr('disabled');
        return;
    }


    var _allLines = [];
    var _single_line = {};

    $("#tblProdItem tr:gt(0)").each(function (i, row) {
        let _pair2 = parseFloat($(this).find('td[controlname="PairQty"]').text());
        let _dzen = $(this).find('td[controlname="DozenQty"]').text();
        let _price2 = parseFloat($(this).find('td[controlname="SellPrice"]').text());
        let _comm2 = parseFloat($(this).find('td[controlname="Comm"]').text());

        let _comProductID = parseInt($(this).find('td[controlname="CommProductID"]').text());

        _single_line = {
            "CompanyProductID": _comProductID,
            "SalesQtyInPair": _pair2,
            "CommissionRate": _comm2,
            "SalesRate": _price2
        }

        _allLines.push(_single_line);
    });

    var salesHead = {
        "SalesHeadID": _ssalesHeadID,
        "SalesDetailsList": _allLines,
        "IsCashSales": _isCashSales,
        "Customer": { "CustomerID": _custNameID2 },
        "SalesDate": _salesDate,
        "TransportCost": _transportCost,
        "PaymentMediumID": _paymentID,
        "AccNo": _accNo,
        "CheckNo": _chkTTNo,
        "CheckPassDate": _chkPassDate,
        "Note1": _note1,
        "ReceiveAmount": parseFloat(_todayJoma),
        "TotalSackNo": parseFloat(_bostaQty),
        "TotalSackNoFee": parseFloat(_bostaAmount),
        "AddLessAmount": parseFloat(_addLess)
    }

    console.log(salesHead);


    if (_allLines.length == 0) {
        $("#spInfo").html('<strong style="color:red">No Data Found !</strong>');
        $("#btnSaveCust").removeAttr('disabled');
        return;
    }

    $("#spInfo").html('<strong style="color:blue">Savng Data Please wait.. !</strong>');

    let _methodName = "";
    if (parseInt(_ssalesHeadID) > 0) {
        _methodName = "UpdateSales";

    }
    else {
        _methodName = "SaveSales";
    }

    $.ajax({
        type: "POST",
        url: WEB_URL + "Sales/" + _methodName,

        data: JSON.stringify(salesHead),
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            console.log(data);
            if (parseInt(data.resultID) > 0) {
                $("#txtBIllNo").val(data.resultNo);
                $("#spInfo").html('<strong style="color:blue">' + data.resultMessage + '</strong>');
                ClearControlAfterSave();

                $("#btnSaveCust").attr('disabled', 'disabled');
                $("#btnAdd").attr('disabled', 'disabled');
                BindData(data.obj, 'disabled');
            }
            else {
                // HideGateOverlay();
                $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                $("#btnSaveCust").removeAttr('disabled');
            }


        },
        error: function (a, b, c) {
            //  HideGateOverlay();
            alert(a, c);
        }
    });




});



$("#txtCustName").bind('input', function () {
    $("#txtCustNameAddress").val('');
    let _custName2 = $("#txtCustName").val();
    let _custNameAndAddress = GetShopName(_custName2, "#dltxtCustName");

    console.log(_custNameAndAddress);

    if (!(_custNameAndAddress == undefined || _custNameAndAddress == null)) {
        $("#txtCustNameAddress").val(_custNameAndAddress);
    }

});



$("#txtCustName").bind('input', function () {
    $("#txtPrevDue").val('0')

    let _custName2 = $("#txtCustName").val();
    let _cstID = GetDataID(_custName2, "#dltxtCustName");





    if ((_cstID == null || _cstID == undefined || _cstID == '0')) {
        return;
    }

    let _cust = {};


    let _url = "";
    let _obj = {}


    _cust = {
        "CustomerID": parseInt(_cstID)
    };
    _url = WEB_URL + "Customer/GetCustomerCurrentBalalce";
    _obj = _cust;

    $("#spInfo").html('<strong style="color:blue">Loading Customer Current Balance Please wait.. !</strong>');



    $.ajax({
        type: "POST",
        data: JSON.stringify(_obj),
        url: _url,
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            // HideGateOverlay();

            console.log(data);
            $("#txtPrevDue").val(data.currentBalance);

            $("#spInfo").html('');
            CalculateGrandTotal();
            CalculateDueAmount();


        },
        error: function (a, b, c) {
            //  HideGateOverlay();
            alert(a, c);
        }
    });






});


$("#txtPackCost").bind('input', function () {

    CalculateGrandTotal();
    CalculateDueAmount();
});
$("#txtBostaAmount").bind('input', function () {

    CalculateGrandTotal();
    CalculateDueAmount();
});
$("#txtAddLess").bind('input', function () {

    CalculateGrandTotal();
    CalculateDueAmount();
});



$("#txtPair").bind('input', function () {

    CalculateSingleItemAmount();
});

$("#txtComm").bind('input', function () {

    CalculateSingleItemAmount();
});

$("#txtSellPrice").bind('input', function () {

    CalculateSingleItemAmount();
});

function BindCustCategoryName(_catList) {
    $("#dltxtCategory option").remove();
    $.each(_catList, function (i, prodData) {

        $("#dltxtCategory").append(' <option data-id="' + prodData.customerCategoryID + '" data-custID="' + prodData.customerCategoryID + '" value="' + prodData.customerCategoryName + '"  />');


    });

}


$("#chkCashSales").change(function () {
    if ($(this).is(':checked')) {
        $("#txtCategory").attr('disabled', 'disabled');
        $("#txtSubCategory").attr('disabled', 'disabled');
        $("#txtCustName").attr('disabled', 'disabled');


        $("#txtCategory").val('');
        $("#txtSubCategory").val('');
        $("#txtCustName").val('');
    }

    else {
        $("#txtCategory").removeAttr('disabled');
        $("#txtSubCategory").removeAttr('disabled');
        $("#txtCustName").removeAttr('disabled');
    }
});



$("#tblProdItem").on('click', 'tr td button', function (e) {

    $("#spInfo").html('');
    //var thisTxtBox = $(this);
    e.preventDefault();
    //ClearControl();

    //alert('working in Progress');
    //var thisTxtBox = $(this);
    e.preventDefault();

    let _for = $(this).attr("data-for");
    let _slNo = $(this).attr("data-slno");

    let _comProductID = $(this).attr("data-companyproductid");
    let _salesHeadID = $(this).attr("data-salesheadid");
    let _salesDetailsID = $(this).attr("data-salesdetailsid");


    let _pairQty = parseFloat($(this).parent("td").parent("tr").find('td[controlname="PairQty"]').text());
    let _dzQty = parseFloat($(this).parent("td").parent("tr").find('td[controlname="DozenQty"]').text());
    let _comm = parseFloat($(this).parent("td").parent("tr").find('td[controlname="Comm"]').text());
    let _price = parseFloat($(this).parent("td").parent("tr").find('td[controlname="SellPrice"]').text());


    let _corrStock = parseFloat($(this).parent("td").parent("tr").find('td[controlname="CurrentStock"]').text());

    let _prodName = $(this).parent("td").parent("tr").find('td[controlname="ProdName"]').text();
    let _articleName = $(this).parent("td").parent("tr").find('td[controlname="ArticleName"]').text();
    let _sizeName = $(this).parent("td").parent("tr").find('td[controlname="SizeName"]').text();
    let _uomName = $(this).parent("td").parent("tr").find('td[controlname="UOMName"]').text();



    if (_for == 'edit') {

        $("#btnAdd").val('Update');


        $("#txtProName").val(_prodName);
        var _val = $("#txtProName").val();
        GetItemsInProName(_val);


        $("#btnAdd").attr('data-salesheadid', _salesHeadID);
        $("#btnAdd").attr('data-salesdetailsid', _salesDetailsID);
        $("#btnAdd").attr('data-companyproductid', _comProductID);
        $("#btnAdd").attr('data-slno', _slNo);

        $("#txtArcticle").val(_articleName);

        $("#txtSize").val(_sizeName);


        $("#txtUOM").val(_uomName);
        $("#txtCurrentStock").val(_corrStock);
        $("#txtPair").val(_pairQty);

        $("#txtDozen").val(_dzQty);
        $("#txtComm").val(_comm);
        $("#txtSellPrice").val(_price);

        CalculateSingleItemAmount();
    }

    if (_for == 'delete') {


        if (confirm('Are you sure to delete This Item?')) {
            // Save it!
            if (parseInt(_salesDetailsID) > 0) {
                let salesDetails = {
                    "SalesDetailsID": _salesDetailsID,
                    "SalesHeadID": _salesHeadID,
                    "CompanyProductID": _comProductID,

                };


                $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
                $.ajax({
                    type: "POST",
                    url: WEB_URL + "Sales/DeleteSalesDetails",

                    data: JSON.stringify(salesDetails),
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        if (parseInt(data.resultID) > 0) {
                            $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');
                            console.log(data);
                            clearItemAfterAdd();


                            $("#btnSaveCust").removeAttr('disabled');
                            $("#btnAdd").removeAttr('disabled');
                            $("#txtBIllNo").val(data.obj.generatedSalesNo);
                            $("#spInfo").html('');
                            ClearControlAfterSave();

                            $("#btnSaveCust").val('Update');
                            BindData(data.obj, '');

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

                CalCulateTotalAmount();

            }

        }
        else {
            return;
        }


    }


});




$("#txtCategory").bind('input', function () {
    // ClearAfterBuyerChange();
    $("#dltxtSubCategory option").remove();
    $("#txtSubCategory").val('');
    // var _thisVal=
    var _val = $(this).val();

    var _catID = GetDataID(_val, "#dltxtCategory");
    //var _subCatList = [];
    //if (_catID != undefined) {
    //    _subCatList = SUB_CUST_CAT.filter(function (_obj) {
    //        return _obj.customerCategoryID == _catID;
    //    });

    //    BindSubCustName(_subCatList);

    //}
    var _CustList = [];
    if (_catID != undefined) {
        _CustList = CUST_LIST.filter(function (_obj) {
            return _obj.customerSubCategory.customerCategoryID == _catID;
        });

        BindCustName(_CustList);
    }

});


$("#txtSubCategory").bind('input', function () {

    $("#dltxtCustName option").remove();
    $("#txtCustNameAddress").val('');
    $("#txtCustName").val('');

    var _val = $(this).val();

    var _subCatID = GetDataID(_val, "#dltxtSubCategory");

    if (_subCatID != undefined) {
        var _CustList = CUST_LIST.filter(function (_obj) {
            return _obj.customerSubCategoryID == _subCatID;
        });

        BindCustName(_CustList);
    }

});

$("#txtTodayJoma").bind('input', function () {

    CalculateDueAmount();
});

function CalculateDueAmount() {

    var _neatAmont = $("#txtNeatAmount").val();

    var _todayJoma = ConvertBanglaToEnglish($("#txtTodayJoma").val());
    var _prevDue = $("#txtPrevDue").val();

    if (isNaN(_neatAmont) || _neatAmont == undefined || _neatAmont == '') {
        _neatAmont = '0';
    }
    console.log(_neatAmont);
    if (isNaN(_todayJoma) || _todayJoma == undefined || _todayJoma == '') {
        _todayJoma = '0';
    }
    console.log(_todayJoma);
    if (isNaN(_prevDue) || _prevDue == undefined || _prevDue == '') {
        _prevDue = '0';
    }
    var _todayDue = parseFloat(_neatAmont) - parseFloat(_todayJoma);
    $("#txtTodayDue").val(_todayDue.toFixed(2));

    $("#txtTotalDue").val((parseFloat(_prevDue) + _todayDue).toFixed(2));



}



$("#txtProName").bind('input', function () {

    var _val = $("#txtProName").val();
    GetItemsInProName(_val);
});


function GetItemsInProName(_val2) {
    $("#dltxtArcticle option").remove();
    $("#txtArcticle").val('');
    $("#btnAdd").attr('data-companyproductid', '0');
    COMP_PRODUCTS = [];


    var _prodNameID = GetDataID(_val2, "#dltxtProName");

    if (_prodNameID != undefined) {

        var _prodName = {
            "ProdNameID": _prodNameID
        }

        LoadItemsInProName(_prodName);
    }
}


$("#txtArcticle").bind('input', function () {
    $("#txtExtraArticle").val('');

    $("#txtSize").val('');


    $("#txtUOM").val('');
    $("#txtCurrentStock").val('');
    $("#txtComm").val('');
    $("#txtSellPrice").val('');

    $("#btnAdd").attr('data-companyproductid', '0');

    var _thisVal = $(this).val();

    var _comProductID = GetDataID(_thisVal, "#dltxtArcticle");

    if (_comProductID != undefined) {
        BindSizeAndUOM(_comProductID);
    }


    if (_thisVal.includes('|')) {
        //alert(_thisVal);
        var _artName = _thisVal.split('|')[0];
        $("#txtArcticle").val($.trim(_artName));
    }
});

function BindSizeAndUOM(_comProductID) {
    console.log(_comProductID);
    var _singleObj = COMP_PRODUCTS.find(_prod => _prod.companyProductID == _comProductID);
    if (!(_singleObj == undefined || _singleObj == null)) {
        $("#txtSize").val(_singleObj.size.name);
        $("#txtUOM").val(_singleObj.uom.name);
        $("#txtCurrentStock").val(_singleObj.currentStock);

        $("#txtComm").val(_singleObj.sellComm);
        $("#txtSellPrice").val(_singleObj.sellPrice);

        $("#btnAdd").attr('data-companyproductid', _comProductID);

    }
}

function LoadItemsInProName(_obj) {
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
        url: WEB_URL + "CompanyProduct/GetCompanyProductByProdName",

        data: JSON.stringify(_obj),
        dataType: "JSON",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            // HideGateOverlay();
            $("#spInfo").html('');
            console.log(data);
            COMP_PRODUCTS = data;
            BIndArticleAndSize(data);

        },
        error: function (a, b, c) {
            //  HideGateOverlay();
            alert(a, c);
        }
    });



}
function BindData(_obj, _disabledStr) {

    console.log(_obj);
    $("#txtBIllNo").val(_obj.generatedSalesNo);
    $("#txtDate2").val(_obj.salesDateFormated);
    $("#txtBIllNoParty").val(_obj.custwiseSalesNo);
    $("#txtPaymentMedium").val(_obj.paymentMedium.name);

    $("#txtAccNo").val(_obj.accNo);

    $("#txtCheckTTNo").val(_obj.checkNo);
    $("#txtCheckPasDate").val(_obj.checkPassDateFormat);
    $("#txtNote1").val(_obj.note1);


    $("#btnSaveCust").val('Update');
    $("#btnSaveCust").attr('data-salesheadid', _obj.salesHeadID);

    if (_obj.isCashSales) {
        $("#chkCashSales").prop("checked", true);
        $("#txtCustName").attr('disabled', 'disabled');
    }
    else {
        $("#chkCashSales").prop("checked", false);
        $("#txtCustName").removeAttr('disabled');

        if (_obj.customer != null) {

            $("#txtCustName").val(_obj.customer.shopName + '-' + _obj.customer.customerNo);
            $("#txtCustNameAddress").val(_obj.customer.name + ',' + _obj.customer.address1);

            $("#dltxtCustName").append(' <option data-id="' + _obj.customer.customerID + '" data-custID="' + _obj.customer.customerID + '"  value="' + _obj.customer.shopName + '-' + _obj.customer.customerNo + '"  custname="' + _obj.customer.name + ', ' + _obj.customer.address1 + '"/>');



        }
    }

    $("#txtPackCost").val(_obj.transportCost.toFixed(2));

    $("#txtTodayJoma").val(_obj.receiveAmount.toFixed(2));

    $("#txtPrevDue").val(_obj.previousBalance.toFixed(2));

    $("#txtBostaQty").val(_obj.totalSackNo);
    $("#txtBostaAmount").val(_obj.totalSackNoFee.toFixed(2));
    $("#txtAddLess").val(_obj.addLessAmount.toFixed(2));



    BindTableRows(_obj.salesDetailsList, _disabledStr)

    CalculateGrandTotal();
    CalculateDueAmount();
}

function BindTableRows(_objlist, disabledStr) {

    console.log(_objlist);
    $("#tblProdItem tr:gt(0)").remove();

    $(_objlist).each(function (i, _singleObj) {

        $("#tblProdItem").append('<tr> <td style="display:none;" controlname="CommProductID">' + _singleObj.companyProduct.companyProductID + '</td> <td controlname="SlNo">' + (i + 1) + '</td> <td  controlname="ProdName">' + _singleObj.prodName.name + '</td> <td controlname="ArticleName">' + _singleObj.article.name + '</td> <td></td><td controlname="SizeName">' + _singleObj.size.name + '</td> <td controlname="CurrentStock">' + _singleObj.currentStockQty + '</td><td  controlname="UOMName">' + _singleObj.uom.name + '</td> <td controlname="DozenQty">' + _singleObj.salesQtyInDozen + '</td><td controlname="PairQty">' + _singleObj.salesQtyInPair + '</td><td controlname="Comm"> ' + _singleObj.commissionRate + '</td><td controlname="SellPrice">' + _singleObj.salesRate + '</td><td controlname="TotPrice">' + _singleObj.salesAmount + '</td> <td controlname="TotComm">' + _singleObj.commissionAmount + '</td><td><button data-salesheadid="' + _singleObj.salesHeadID + '" data-salesdetailsid="' + _singleObj.salesDetailsID + '" data-companyproductid="' + _singleObj.companyProduct.companyProductID + '" data-slno="' + (i + 1) + '" data-for="edit" class="mb-2 mr-2 btn-dashed btn btn-outline-info"  ' + disabledStr + '> Edit</button></td> <td><button   data-salesheadid="' + _singleObj.salesHeadID + '" data-salesdetailsid="' + _singleObj.salesDetailsID + '" data-companyproductid="' + _singleObj.companyProduct.companyProductID + '" data-slno="' + (i + 1) + '"  data-for="delete" class="mb-2 mr-2  btn-dashed btn btn-outline-danger" ' + disabledStr + '> Delete</button></td></tr>');
    });

    CalCulateTotalAmount();

}

function BIndArticleAndSize(_objList) {

    $.each(_objList, function (i, prodData) {

        var _value = prodData.article.name + ' | ' + prodData.size.name + ' | ' + prodData.currentStock + ' | ' + prodData.uom.name

        $("#dltxtArcticle").append(' <option  data-article="' + prodData.article.name + '" data-id="' + prodData.companyProductID + '" data-companyProductID="' + prodData.companyProductID + '" value="' + _value + '" />');


    });
}



function BindCustName(_custList) {
    $("#dltxtCustName option").remove();

    $.each(_custList, function (i, prodData) {

        $("#dltxtCustName").append(' <option data-id="' + prodData.customerID + '" data-custID="' + prodData.customerID + '"   value="' + prodData.shopName + '-' + prodData.customerNo + '"  custname="' + prodData.name + ', ' + prodData.address1 + '"/>');


    });

}




function BindProdName(_subcatList) {

    $("#dltxtProName option").remove();

    $.each(_subcatList, function (i, prodData) {

        $("#dltxtProName").append(' <option data-id="' + prodData.prodNameID + '" data-prodNameID="' + prodData.prodNameID + '" value="' + prodData.name + '" />');


    });

}
function BindPaymentMedium(_objList) {

    $("#dlPaymentMedium option").remove();

    $.each(_objList, function (i, prodData) {

        $("#dlPaymentMedium").append(' <option data-id="' + prodData.paymentMediumID + '" data-paymentMediumID="' + prodData.paymentMediumID + '" value="' + prodData.name + '" />');


    });

}

function CalculateSingleItemAmount() {

    var _parQty = ConvertBanglaToEnglish($("#txtPair").val());
    //var _dznQty = $("#txtDozen").val();

    var _commAmount = ConvertBanglaToEnglish($("#txtComm").val());

    var _selPrice = ConvertBanglaToEnglish($("#txtSellPrice").val());

    if (_parQty == '') { _parQty = '0'; }
    if (_commAmount == '') { _commAmount = '0'; }
    if (_selPrice == '') { _selPrice = '0'; }

    var _dzQty = parseFloat(_parQty) / 12;

    var _totComm = parseFloat(_commAmount) * parseFloat(_parQty);

    var _totSelPrice = parseFloat(_selPrice) * parseFloat(_parQty);

    $("#txtDozen").val(ConvertEnglishToBengali(_dzQty.toFixed(2)));
    $("#txtTotalComm").val(ConvertEnglishToBengali(_totComm.toFixed(2)));
    $("#txtTotalSellPrice").val(ConvertEnglishToBengali(_totSelPrice.toFixed(2)));

}

function CalCulateTotalAmount() {

    let _totDozen2 = 0
    let _totQty2 = 0
    let _totAmount2 = 0;
    let _totComm2 = 0;



    $("#tblProdItem tr:gt(0)").each(function (i, row) {
        _totQty2 += parseFloat($(this).find('td[controlname="PairQty"]').text());
        _totDozen2 += parseFloat($(this).find('td[controlname="DozenQty"]').text());
        _totAmount2 += parseFloat($(this).find('td[controlname="TotPrice"]').text());
        _totComm2 += parseFloat($(this).find('td[controlname="TotComm"]').text());

    });
    $("#txtTotalDozen").val(_totDozen2.toFixed(2));
    $("#txtTotalpair").val(_totQty2.toFixed(2));
    //$("#txtNeatAmount").val(_totAmount2.toFixed(2));
    $("#txtTotalComm2").val(_totComm2.toFixed(2));

    $("#txtTotalAmount").val(_totAmount2.toFixed(2));

}

function CalculateGrandTotal() {

    let _totAmount = 0;

    let _totComm = 0;

    let _transportFee = 0;
    let _bostaQty = 0;
    let _bostaAmount = 0;
    let _addLess = 0;

    let _grandTotal = 0;
    if ($("#txtTotalComm2").val() == '') {
        $("#txtTotalComm2").val('0')
    }
    if ($("#txtPackCost").val() == '') {
        $("#txtPackCost").val('0')
    }

    if ($("#txtBostaAmount").val() == '') {
        $("#txtBostaAmount").val('0')
    }

    if ($("#txtAddLess").val() == '') {
        _addLess = parseFloat('0');
    }

    else {
        _addLess = parseFloat(ConvertBanglaToEnglish($("#txtAddLess").val()));
    }

    _totAmount = parseFloat($("#txtTotalAmount").val());
    _totComm = parseFloat($("#txtTotalComm2").val());
    _transportFee = parseFloat(ConvertBanglaToEnglish($("#txtPackCost").val()));
    _bostaAmount = parseFloat(ConvertBanglaToEnglish($("#txtBostaAmount").val()));


    _grandTotal = _totAmount - _totComm + _transportFee + _bostaAmount + _addLess;


    $("#txtGrandTotal").val(_grandTotal.toFixed(2))
    $("#txtNeatAmount").val(_grandTotal.toFixed(2))

}



function clearItemAfterAdd() {
    $("#txtArcticle").val('');
    $("#txtExtraArticle").val('');
    $("#txtCurrentStock").val('');
    $("#txtPair").val('');
    $("#txtSize").val('');

    $("#txtComm").val('0');

    $("#txtSellPrice").val('0');

    $("#btnAdd").val('Add');

    $("#btnAdd").attr('data-salesheadid', "0");
    $("#btnAdd").attr('data-salesdetailsid', "0");
    $("#btnAdd").attr('data-companyproductid', "0");
    $("#btnAdd").attr('data-slno', "0");

    CalculateSingleItemAmount();
}

function ClearControlAfterSave() {
    clearItemAfterAdd();

    $("#btnSaveCust").val('Save');
    $("#btnSaveCust").attr('data-salesheadid', "0");
    $("#btnSaveCust").attr('data-salesdetailsid', "0");
    $("#txtBIllNoParty").val('');
    $("#tblProdItem tr:gt(0)").remove();
    $("#txtCustName").val('');
    $("#txtCustNameAddress").val('');
    $("#txtTotalDozen").val('0');
    $("#txtTotalpair").val('0');

    $("#txtNeatAmount").val('0');
    $("#txtTodayJoma").val('0');

    $("#txtBostaQty").val('0');
    $("#txtBostaAmount").val('0');
    $("#txtAddLess").val('0');

    $("#txtTodayDue").val('0');
    $("#txtTotalDue").val('0');

    $("#txtTotalAmount").val('0');
    $("#txtTotalComm2").val('0');

    $("#txtPrevDue").val('0');
    $("#txtTotalDue").val('0');

    $("#txtPackCost").val('0');
    $("#txtGrandTotal").val('0');
    $("#txtNeatAmount").val('0');
    $("#txtPaymentMedium").val('');
    $("#txtCustName").val('');
    $("#txtCustNameAddress").val('');
    $("#chkCashSales").prop("checked", false);
    $("#txtCustName").removeAttr("disabled");
    $("#btnAdd").removeAttr("disabled");

    CalculateGrandTotal();
    CalculateDueAmount();

}



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

function GetShopName(value, dataList) {
    var z = $(dataList);
    var val = $(z).find('option[value="' + value + '"]');
    var endval = val.attr('custname');
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
    win.document.write('<title>' + BillNo_PDF + '</title>');   // <title> FOR PDF HEADER.
    win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
    win.document.write('</head>');
    win.document.write('<body>');
    win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
    win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
}


function createPDFA5() {
    var sTable = document.getElementById('tabA5').innerHTML;

    var style = "<style>";
    //style = style + "table {width: 100%;font: 17px Calibri;}";
    //  style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
    style = style + "table, th, td {padding: 2px 3px;border-collapse: collapse;}";
    style = style + "</style>";

    // CREATE A WINDOW OBJECT.
    var win = window.open('', '', 'height=595,width=842');

    win.document.write('<html><head>');
    win.document.write('<title>' + BillNo_PDF + '</title>');   // <title> FOR PDF HEADER.
    win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
    win.document.write('</head>');
    win.document.write('<body>');
    win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
    win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
}
