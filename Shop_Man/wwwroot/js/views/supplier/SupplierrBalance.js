
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];


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
    url: WEB_URL + "Supplier/GetSupplierBalanceReport",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data.obj);
    $("#spInfo").html('');
    $("#spTitle").text(data.resultNo);

    if (queryStrings["customerIDs"] == "0") {
        BindAllCustomerData(data.obj);
                    }
    else {
        BindData(data.obj);
                    }
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        }



    function BindData(_obj) {


        $("#tblOrder").append(' <tr><td>SL</td><td>Date</td><td>Type</td><td>Invoice No</td><td>Note</td><td>Purchase Amnt.</td><td>Cash Payment</td><td>Cheque Payment</td><td>Cheque Recv</td><td>Adjustment</td><td>Short Amount</td><td>Reject Goods Amnt</td><td>Balance</td></tr >');

    var _openingBalance = 0;

    var _recvAmountAll = 0;
    var _salesAmountAll = 0;
    var _otherAmountAll = 0;

    var _payAmountAll = 0;


    $.each(_obj, function (i, prodData) {


                //var _recvAmount = (prodData.cashReceiveAmount + prodData.receiveAmount + prodData.checkRecev)

                //_recvAmountAll += _recvAmount;


                //var _salesAmount = prodData.salesAmount;
                //_salesAmountAll += prodData.salesAmount;

                //var _otherAmount = prodData.totalSackNoFee + prodData.transportCost + prodData.addLessAmount;
                //_otherAmountAll += _otherAmount;

                //var _payAmount = prodData.checkPayment;
                //_payAmountAll += _payAmount;

                //   <td>`+ prodData.closingShortAmount +`</td>
               // <td>`+ prodData.rejectGoodsAmount +`</td>

                var _currentBalance = _openingBalance + prodData.openingBalance + prodData.purchaseAmount - prodData.cashPaymentAmount - prodData.checkPayment + prodData.checkRecev - prodData.adjustAmount - prodData.closingShortAmount- prodData.rejectGoodsAmount;



    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.dDateFormated + '</td><td>' + prodData.type + '</td><td>' + prodData.autoNo + '</td><td style="text-align:right;">' + prodData.note + '</td><td style="text-align:right;">' + prodData.purchaseAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.cashPaymentAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.checkPayment.toFixed(2) + '</td><td style="text-align:right;">' + prodData.checkRecev.toFixed(2) + '</td><td style="text-align:right;">' + prodData.adjustAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.closingShortAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.rejectGoodsAmount.toFixed(2) + '</td><td style="text-align:right;">' + _currentBalance.toFixed(2) + '</td></tr>');
    _openingBalance = _currentBalance;

            });


           // $("#tblOrder").append('<tr><td colspan="5">ToTal :</td><td style="text-align:right;">' + _salesAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _otherAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _recvAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _payAmountAll.toFixed(2) + '</td><td style="text-align:right;"></td></tr>');

        }



    function BindAllCustomerData(_obj) {


        $("#tblOrder").append('<thead> <tr><td>SL</td><td>ID</td><td>Supplier Name</td><td>Opening</td><td>Purchase Amnt.</td><td>Cash Payment Amnt.</td><td>Checque Payment</td><td>Check Receive</td><td>Adjustment</td><td>Short Amount</td><td>Reject Goods Amnt</td><td>Balance</td></tr> </thead> ');

    var _openingBalanceAll = 0;

    var _cashPayAmountAll = 0;
    var _purchaseAmountAll = 0;


    var _chekPayAmountAll = 0;
    var _checkRecvAll = 0;
    var _adjustAll = 0;

    var _balanceAll = 0;
    $.each(_obj, function (i, prodData) {

        _openingBalanceAll += prodData.openingBalance;
    _purchaseAmountAll += prodData.purchaseAmount;
    _cashPayAmountAll += prodData.cashPaymentAmount;
    _chekPayAmountAll += prodData.checkPayment;

    _checkRecvAll += prodData.checkRecev;
    _adjustAll += prodData.adjustAmount;

    var _currentBalance = prodData.openingBalance + prodData.purchaseAmount - prodData.cashPaymentAmount - prodData.checkPayment + prodData.checkRecev - prodData.adjustAmount- prodData.closingShortAmount - prodData.rejectGoodsAmount;;
    _balanceAll += _currentBalance;

    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.supplierId + '</td><td>' + prodData.name + '</td><td style="text-align:right;">' + prodData.openingBalance + '</td><td style="text-align:right;">' + prodData.purchaseAmount + '</td><td style="text-align:right;">' + prodData.cashPaymentAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.checkPayment.toFixed(2) + '</td><td style="text-align:right;">' + prodData.checkRecev.toFixed(2) + '</td><td style="text-align:right;">' + prodData.adjustAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.closingShortAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.rejectGoodsAmount.toFixed(2) + '</td><td style="text-align:right;">' + _currentBalance.toFixed(2) + '</td></tr>');

            });
    $("#tblOrder").append('<tr><td colspan="3">Total :</td><td style="text-align:right;">' + _openingBalanceAll.toFixed(2) + '</td><td style="text-align:right;">' + _purchaseAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _cashPayAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _chekPayAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _checkRecvAll.toFixed(2) + '</td><td style="text-align:right;">' + _adjustAll.toFixed(2) + '</td><d></td><d></td><td style="text-align:right;">' + _balanceAll.toFixed(2) + '</td></tr>');

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
        win.document.write('<title>Supplier_Balance_Report</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }

