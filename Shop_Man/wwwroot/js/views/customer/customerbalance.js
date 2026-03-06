
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

    var CATEGORYWISE_CUSTOMER = [];
    var ALL_CUST = [];
    $(document).ready(function () {

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            console.log(vars);


            return vars;
        }

        var queryStrings = getUrlVars();

    LoadData();
    LoadSubcategory();
    function LoadData() {
        $("#spInfo").html('');

    var model = {
        "FromDate": queryStrings["fromDate"],
    "ToDate": queryStrings["toDate"],
    "CustomerIds": queryStrings["customerIDs"],
    "Type": queryStrings["type"],
            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Customer/GetPartyBalanceReport",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data.obj);
    $("#spInfo").html('');
    $("#spTitle").text(data.resultNo);
                    //if (queryStrings["IsDetails"] == "1") {
                    //    BindDataDetails(data.obj);
                    //}
                    //else {
                    //    BindData(data.obj);
                    //}
                    if (queryStrings["customerIDs"] == "0") {

        ALL_CUST = data.obj;
    BindAllCustomerData(ALL_CUST);
                    }
    else {
        $('input[type="button"].FilterCust').css('display', 'none');
    $('input[type="text"].FilterCust').css('display', 'none');
    BindData(data.obj);
                    }
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        }

    function LoadSubcategory() {
        // $("#spInfo").html('');



        // $("#spInfo").html('<strong style="color:blue">Loading Sub category.. !</strong>');
        $.ajax({
            type: "POST",
            url: WEB_URL + "Customer/CustomerSubcategory",

            // data: JSON.stringify(model),
            dataType: "JSON",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                console.log(data.obj);
                // $("#spInfo").html('');


                LoadSubCustName(data.customerSubCategorys);


            },
            error: function (a, b, c) {
                //  HideGateOverlay();
                alert(a, c);
            }
        });

        }


    $("#txtSubCategory").bind('input', function () {
           
            // var _thisVal=
            var _val = $(this).val();

    var _catID = GetDataID(_val, "#dltxtSubCategory");

    if (_catID != undefined) {


                var _allData2 = ALL_CUST.filter(_a => _a.customerSubCategoryName == _val)


    BindAllCustomerData(_allData2);

               
            }

    if (_val == '') {
        BindAllCustomerData(ALL_CUST);

            }
         

        });
        //<tr>
        //    <td>SL</td>
        //    <td>Invoice No</td>
        //    <td>Date</td>
        //    <td>Customer</td>
        //    <td>Total Sales</td>
        //    <td>Commission</td>
        //    <td>Pay Type</td>
        //    <td>Receive Amount</td>
        //</tr>
    function BindData(_obj) {


        $("#tblOrder").append(' <tr><td>SL</td><td>Date</td><td>Type</td><td>Invoice No</td><td>Note</td><td>Sales Amnt.</td><td>Sack Exp (বস্তা করচ)</td><td>Others(Trans,Sack, <br> Add/Leass)Amnt.</td><td>Receive Amnt.</td><td>Cheq Pay Amnt.</td><td>Cash Pay Amnt.</td><td>Adjust Amount</td><td>Return Amount</td><td>Year Short Amnt</td><td>Rej Itm Amnt</td><td>Sales Adjust</td><td>Balance</td></tr >');

    var _openingBalance = 0;

    var _recvAmountAll = 0;
    var _salesAmountAll = 0;
    var _otherAmountAll = 0;

    var _payAmountAll = 0;
    var _sackAmountAll = 0;
    var _payCashAmountAll = 0;

    var _adjAountAll = 0;

    var _retAmountAll = 0;

    var _yearShortAmntAll = 0;
    var _rejItmAmntAll = 0;
    var _salesAdjAmntAll = 0;

    $.each(_obj, function (i, prodData) {

            
                var _recvAmount = (prodData.cashReceiveAmount + prodData.receiveAmount + prodData.checkRecev)

    _recvAmountAll += _recvAmount;


    var _salesAmount = prodData.salesAmount;
    _salesAmountAll += prodData.salesAmount;

    var _otherAmount =   prodData.transportCost + prodData.addLessAmount;
    _otherAmountAll += _otherAmount;

    var _payAmount = prodData.checkPayment;
    _payAmountAll += _payAmount;

    _sackAmountAll += prodData.totalSackNoFee;

    var _payCashAmount = prodData.cashPayment;

    _payCashAmountAll += prodData.cashPayment;

    _adjAountAll += prodData.adjustAmount;
    var _adjAount = prodData.adjustAmount;


    _retAmountAll += prodData.returnAmount;
    var _retAmount = prodData.returnAmount;



    _yearShortAmntAll += prodData.closingShortAmount;
    var _yearShortAmnt = prodData.closingShortAmount;

    _rejItmAmntAll += prodData.rejectGoodsAmount;
    var _rejItmAmnt = prodData.rejectGoodsAmount;

    _salesAdjAmntAll += prodData.salesAdjustAmount;

    var _salesAdjAmnt= prodData.salesAdjustAmount

    var _currentBalance = _openingBalance + prodData.openingBalance + _salesAmount + prodData.totalSackNoFee + _otherAmount + _payAmount - _recvAmount + _payCashAmount - _adjAount - _retAmount - _yearShortAmnt - _rejItmAmnt - _salesAdjAmnt;



    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.dDateFormated + '</td><td>' + prodData.type + '</td><td>' + prodData.autoNo + '</td><td style="text-align:right;">' + prodData.note + '</td><td style="text-align:right;">' + _salesAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.totalSackNoFee.toFixed(2) + '</td><td style="text-align:right;">' + _otherAmount.toFixed(2) + '</td><td style="text-align:right;">' + _recvAmount.toFixed(2) + '</td><td style="text-align:right;">' + _payAmount.toFixed(2) + '</td><td style="text-align:right;">' + _payCashAmount.toFixed(2) + '</td><td style="text-align:right;">' + _adjAount.toFixed(2) + '</td><td style="text-align:right;">' + _retAmount.toFixed(2) + '</td><td style="text-align:right;">' + _yearShortAmnt.toFixed(2) + '</td><td style="text-align:right;">' + _rejItmAmnt.toFixed(2) + '</td><td style="text-align:right;">' + _salesAdjAmnt.toFixed(2) + '</td><td style="text-align:right;">' + _currentBalance.toFixed(2) + '</td></tr>');



    _openingBalance = _currentBalance;

            });


    $("#tblOrder").append('<tr><td colspan="5">ToTal :</td><td style="text-align:right;">' + _salesAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _sackAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _otherAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _recvAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _payAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _payCashAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _adjAountAll.toFixed(2) + '</td><td style="text-align:right;">' + _retAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _yearShortAmntAll.toFixed(2) + '</td><td style="text-align:right;">' + _rejItmAmntAll.toFixed(2) + '</td><td style="text-align:right;">' + _salesAdjAmntAll.toFixed(2) + '</td><td style="text-align:right;"></td></tr>');

        }




    $("#btnActive").click(function () {

            var _allData = ALL_CUST.filter(_a => _a.isBlocked == 0)


    BindAllCustomerData(_allData);

        });



    $("#btnDeActive").click(function () {

            var _allData = ALL_CUST.filter(_a => _a.isBlocked == 1)


    BindAllCustomerData(_allData);

        });


    $("#btnAll").click(function () {



        BindAllCustomerData(ALL_CUST);

        });




    function BindAllCustomerData(_obj) {

        $('#tblOrder').empty();
    CATEGORYWISE_CUSTOMER = [];
    $.each(_obj, function (i, prodData) {


                if (CATEGORYWISE_CUSTOMER[prodData.customerSubCategoryName]) {

                    var _ooo = CATEGORYWISE_CUSTOMER[prodData.customerSubCategoryName];
    _ooo.push(prodData);

    CATEGORYWISE_CUSTOMER[prodData.customerSubCategoryName] = _ooo;
                }
    else {
        CATEGORYWISE_CUSTOMER[prodData.customerSubCategoryName] = [prodData];
                }

            });

    console.log(CATEGORYWISE_CUSTOMER);


    $("#tblOrder").append('<thead> <tr><td>SL</td><td>ID</td><td>Shop Name</td><td>Opening</td><td>Sales Amnt.</td><td>Sack Exp (বস্তা করচ)</td><td>Others(Trans, <br> Add/Leass)Amnt.</td><td>Receive Amnt.</td><td>Cash Receive</td><td>Check Receive</td><td>Chk Pay Amnt.</td> <td>Cash Pay Amnt.</td><td>Adjust Amount</td><td>Return Amount</td><td>Year Short Amnt</td><td>Rej Itm Amnt</td><td>Sales Adjust</td><td>Balance</td> </tr> </thead> ');








    var _openingBalanceAll = 0;
    var _sackFeeAll = 0;
    var _recvAmountAll = 0;
    var _salesAmountAll = 0;
    var _otherAmountAll = 0;

    var _payAmountAll = 0;
    var _balanceAll = 0;
    var _recvCashAll = 0;
    var _recvCheckAll = 0;
    var _payCashAmountAll = 0;
    var _balanceAll= 0;
    var _adjAmountAll = 0;

    var _retAmountAll=0;

    var _shortAmountAll = 0;
    var _rejItmAmountAll = 0;
    var _salesAdjAmntAll = 0;





    var _openingBalanceSub = 0;
    var _sackFeeSub = 0;
    var _recvAmountSub = 0;

    var _salesAmountSub = 0;
    var _otherAmountSub = 0;

    var _recvCashSub = 0;
    var _payAmountSub = 0;
    var _payCashAmountsub = 0;
    var _balanceSub = 0;

    var _recvCheckSub = 0;

    var _adjAmountSub = 0;
    var _retAmountSub = 0;


    var _shortAmountSub = 0;
    var _rejItmAmountSub = 0;
    var _salesAdjAmntSub = 0;

            Object.entries(CATEGORYWISE_CUSTOMER).forEach(([key, _value]) => {
        _openingBalanceSub = 0;
    _sackFeeSub = 0;
    _recvAmountSub = 0;
    _salesAmountSub = 0;
    _otherAmountSub = 0;

    _payAmountSub = 0;

    _balanceSub = 0;
    _recvCashSub = 0;
    _recvCheckSub = 0;
    _payCashAmountsub = 0;
    _adjAmountSub = 0;
    _retAmountSub = 0;

    _shortAmountSub = 0;
    _rejItmAmountSub = 0;
    _salesAdjAmntSub = 0;
    $("#tblOrder").append('<tr><td colspan="18" style="Text-align:Left; background-color:yellow; color:red;"><strong> Sub Category :' + key + '</strong></td></tr>');


    $.each(_value, function (i, prodData) {

        let _otherAmounts = prodData.transportCost + prodData.addLessAmount;

    var _currentBalance = prodData.openingBalance + prodData.salesAmount + prodData.totalSackNoFee + _otherAmounts - prodData.receiveAmount - prodData.cashReceiveAmount - prodData.checkRecev + prodData.checkPayment + prodData.cashPayment - prodData.adjustAmount - prodData.returnAmount - prodData.closingShortAmount - prodData.rejectGoodsAmount - prodData.salesAdjustAmount;

    _openingBalanceSub += prodData.openingBalance;
    _openingBalanceAll += prodData.openingBalance;

    _sackFeeSub += prodData.totalSackNoFee;
    _sackFeeAll += prodData.totalSackNoFee;

    _salesAmountSub += prodData.salesAmount;
    _salesAmountAll += prodData.salesAmount;

    _otherAmountSub += _otherAmounts;
    _otherAmountAll += _otherAmounts;

    _recvAmountSub += prodData.receiveAmount;
    _recvAmountAll += prodData.receiveAmount;


    _recvCashSub += prodData.cashReceiveAmount;
    _recvCashAll += prodData.cashReceiveAmount;


    _recvCheckSub += prodData.checkRecev;
    _recvCheckAll += prodData.checkRecev;

    _payAmountSub += prodData.checkPayment;
    _payAmountAll += prodData.checkPayment;

    _payCashAmountAll += prodData.cashPayment;
    _payCashAmountsub += prodData.cashPayment;


    _adjAmountAll += prodData.adjustAmount;
    _adjAmountSub += prodData.adjustAmount;

    _retAmountAll += prodData.returnAmount;
    _retAmountSub += prodData.returnAmount;

    _shortAmountSub += prodData.closingShortAmount ;
    _shortAmountAll += prodData.closingShortAmount ;
    _rejItmAmountSub += prodData.rejectGoodsAmount;
    _rejItmAmountAll += prodData.rejectGoodsAmount;



    _salesAdjAmntAll += prodData.salesAdjustAmount;
    _salesAdjAmntSub += prodData.salesAdjustAmount;

    _balanceSub += _currentBalance;
    _balanceAll += _currentBalance;



    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.customerNo + '</td><td>' + prodData.shopName + '</td><td style="text-align:right;">' + prodData.openingBalance + '</td><td style="text-align:right;">' + prodData.salesAmount + '</td><td style="text-align:right;">' + prodData.totalSackNoFee + '</td><td style="text-align:right;">' + _otherAmounts + '</td><td style="text-align:right;">' + prodData.receiveAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.cashReceiveAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.checkRecev.toFixed(2) + '</td><td style="text-align:right;">' + prodData.checkPayment.toFixed(2) + '</td><td style="text-align:right;">' + prodData.cashPayment.toFixed(2) + '</td><td style="text-align:right;">' + prodData.adjustAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.returnAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.closingShortAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.rejectGoodsAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.salesAdjustAmount.toFixed(2) + '</td><td style="text-align:right;">' + _currentBalance.toFixed(2) + '</td></tr>');

                });
    $("#tblOrder").append('<tr style="Text-align:Left; background-color:yellow; color:red;"><td colspan="3" >Sub Total : </td><td style="text-align:right;">' + _openingBalanceSub.toFixed(2) + '</td><td style="text-align:right;">' + _salesAmountSub.toFixed(2) + '</td><td style="text-align:right;">' + _sackFeeSub.toFixed(2) + '</td><td style="text-align:right;">' + _otherAmountSub.toFixed(2) + '</td><td style="text-align:right;">' + _recvAmountSub.toFixed(2) + '</td><td style="text-align:right;">' + _recvCashSub.toFixed(2) + '</td><td style="text-align:right;">' + _recvCheckSub.toFixed(2) + '</td><td style="text-align:right;">' + _payAmountSub.toFixed(2) + '</td><td style="text-align:right;">' + _payCashAmountsub.toFixed(2) + '</td><td style="text-align:right;">' + _adjAmountSub.toFixed(2) + '</td><td style="text-align:right;">' + _retAmountSub.toFixed(2) + '</td><td style="text-align:right;">' + _shortAmountSub.toFixed(2) + '</td><td style="text-align:right;">' + _rejItmAmountSub.toFixed(2) + '</td><td style="text-align:right;">' + _salesAdjAmntSub.toFixed(2) + '</td><td style="text-align:right;">' + _balanceSub.toFixed(2) + '</td></tr>');

            });

    $("#tblOrder").append('<tr style="Text-align:Left; background-color:yellow; color:red;"><td colspan="3" >Grand Total : </td><td style="text-align:right;">' + _openingBalanceAll.toFixed(2) + '</td><td style="text-align:right;">' + _salesAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _sackFeeAll.toFixed(2) + '</td><td style="text-align:right;">' + _otherAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _recvAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _recvCashAll.toFixed(2) + '</td><td style="text-align:right;">' + _recvCheckAll.toFixed(2) + '</td><td style="text-align:right;">' + _payAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _payCashAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _adjAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _retAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _shortAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _rejItmAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _salesAdjAmntAll.toFixed(2) + '</td><td style="text-align:right;">' + _balanceAll.toFixed(2) + '</td></tr>');

        }


    function LoadSubCustName(_subcatList) {
        $("#dltxtSubCategory option").remove();

    $.each(_subcatList, function (i, prodData) {

        $("#dltxtSubCategory").append(' <option data-id="' + prodData.customerSubCategoryID + '" data-custID="' + prodData.customerSubCategoryID + '" value="' + prodData.customerSubCategoryName + '" />');


            });

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
        win.document.write('<title>Party_Balance_Report</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }

