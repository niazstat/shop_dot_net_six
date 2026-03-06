 
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

    var CATEGORYWISE_CUSTOMER = [];
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
        "CustomerID": queryStrings["customerID"],
    "YearName": queryStrings["yearName"],
    "Type": queryStrings["_type"] == null ? 'N' : queryStrings["_type"],
    "dDate": queryStrings["dDate"] == null ? '01-Jan-1900' : queryStrings["dDate"],
            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Customer/GetExistingCustomerYearCloseDetails",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data.obj);
    $("#spInfo").html('');

    BindData(data.obj);

                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        }







    function BindData(_obj) {


        $("#tblOrder").append(`<thead>
                < tr >
                                              <td>SL</td>
                                                <td>Type</td>
                                                <td>Date</td>
                                                <td>No</td>
                                                <td>সেলস</td>
                                                <td>ক্রয়</td>
                                                <td>লাভ</td>
                                                <td>বস্তা</td>
                                                <td>যাতায়ত</td>
                                                <td>কম/বেশি</td>
                                                <td>রিসিভ</td>
                                                <td>ক্যাশ রিসিভ</td>
                                                <td>চেক রিসিভ</td>
                                                <td>চেক পেমেন্ট</td>
                                                <td>ক্যাশ পেমেন্ট</td>
                                                <td>এডজাস্ট</td>
                                                <td>রিটার্ন </td>
                                                <td>শর্ট</td>
                                                <td>রিজেক্ট</td>
                                                <td>সেলস <br> এডজাস্ট</td>
                                            </tr >
                                        </thead >`);

    var _totSalesAmount = 0;
    var _totBuyAmount = 0;
    var _totalSackNoFee = 0;
    var _tottransportCost = 0;
    var _totaddLessAmount = 0;
    var _totreceiveAmount = 0;
    var _totcashReceiveAmount = 0;
    var _totcheckRecev = 0;
    var _totcheckPayment = 0;
    var _totcashPayment = 0;
    var _totadjustAmount = 0;

    var _totreturnAmount = 0;
    var _totclosingShortAmount = 0;

    var _totrejectGoodsAmount = 0;
    var _profLossAmnt = 0;

    var _totreturnPurAmount = 0;

    var _totSalesAdjustAmount = 0;
    $.each(_obj, function (i, prodData) {


        _totSalesAmount += prodData.salesAmount;
    _totBuyAmount += prodData.purchaseAmount;
    _totalSackNoFee += prodData.totalSackNoFee;
    _tottransportCost += prodData.transportCost;
    _totaddLessAmount += prodData.addLessAmount;
    _totreceiveAmount += prodData.receiveAmount;
    _totcashReceiveAmount += prodData.cashReceiveAmount;
    _totcheckRecev += prodData.checkRecev;
    _totcheckPayment += prodData.checkPayment;
    _totcashPayment += prodData.cashPayment;
    _totadjustAmount += prodData.adjustAmount;

    _totreturnAmount += prodData.returnAmount;
    _totclosingShortAmount += prodData.closingShortAmount;

    _totrejectGoodsAmount += prodData.rejectGoodsAmount;

    _totreturnPurAmount += prodData.retPurchaseAmount;

    _totSalesAdjustAmount += prodData.salesAdjustAmount;

    _profLossAmnt += parseFloat(prodData.salesAmount) - parseFloat(prodData.purchaseAmount);

    $("#tblOrder").append(` <tr>
        <td>`+ (i + 1) +`</td>
        <td>`+ prodData.type +`</td>
        <td>`+ prodData.dDateFormated+`</td>
        <td>`+ prodData.autoNo +`</td>
        <td style="text-align:right">`+ prodData.salesAmount.toFixed(2) +`</td>
        <td style="text-align:right">`+ prodData.purchaseAmount.toFixed(2) +`</td>
        <td style="text-align:right">`+ (parseFloat(prodData.salesAmount)-parseFloat( prodData.purchaseAmount)).toFixed(2) +`</td>
        <td style="text-align:right">`+ prodData.totalSackNoFee.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.transportCost.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.addLessAmount.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.receiveAmount.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.cashReceiveAmount.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.checkRecev.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.checkPayment.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.cashPayment.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.adjustAmount.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.returnAmount.toFixed(2)  +` </td>
        <td style="text-align:right">`+ prodData.closingShortAmount.toFixed(2)  +`</td>
        <td style="text-align:right">`+ prodData.rejectGoodsAmount.toFixed(2)  +`</td>

        <td style="text-align:right">`+ prodData.salesAdjustAmount.toFixed(2) +`</td>
    </tr >`);


            });

    $("#tblOrder").append(` <tr>
        <td colspan="4">Total :</td>

        <td style="text-align:right;   font-weight: bolder;">`+ _totSalesAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totBuyAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _profLossAmnt.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totalSackNoFee.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _tottransportCost.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totaddLessAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totreceiveAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totcashReceiveAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totcheckRecev.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totcheckPayment.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+_totcashPayment.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totadjustAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totreturnAmount.toFixed(2) + ` </td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totclosingShortAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totrejectGoodsAmount.toFixed(2) + `</td>

        <td style="text-align:right;   font-weight: bolder;">`+ _totSalesAdjustAmount.toFixed(2) + `</td>
    </tr >`);



    $("#tblOrder").append(` <tr><td></td><td>বিক্রয় মূল্য</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totSalesAmount.toFixed(2) + `</td><td style="text-align:right">বিক্রয় মূল্য</td><td style="text-align:right"></td><td style="text-align:right">` + _totSalesAmount.toFixed(2) +`</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)
    $("#tblOrder").append(` <tr><td></td><td>ক্রয় মূল্য</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totBuyAmount.toFixed(2) + `</td><td style="text-align:right">রিসিভ</td><td style="text-align:right"></td><td style="text-align:right">` + _totreceiveAmount.toFixed(2) +`</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)
    $("#tblOrder").append(` <tr><td></td><td>মোট লাভ</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _profLossAmnt.toFixed(2) + `</td><td style="text-align:right">ক্যাশ রিসিভ</td><td style="text-align:right"></td><td style="text-align:right">` + _totcashReceiveAmount.toFixed(2) +`</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)
    $("#tblOrder").append(` <tr><td></td><td>রিটার্ন</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totreturnAmount.toFixed(2) + `</td><td style="text-align:right">চেক রিসিভ</td><td style="text-align:right"></td><td style="text-align:right">` + _totcheckRecev.toFixed(2) +`</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)

    $("#tblOrder").append(` <tr><td></td><td>কম/বেশি</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totaddLessAmount.toFixed(2) + `</td><td style="text-align:right">রিটার্ন</td><td style="text-align:right"></td><td style="text-align:right">` + _totreturnAmount.toFixed(2) +`</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)
    $("#tblOrder").append(` <tr><td></td><td>এডজাস্ট</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totadjustAmount.toFixed(2) + `</td><td style="text-align:right">কম/বেশি</td><td style="text-align:right"></td><td style="text-align:right">` + _totaddLessAmount.toFixed(2) +`</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)
    $("#tblOrder").append(` <tr><td></td><td>শর্ট</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totclosingShortAmount.toFixed(2) + `</td><td style="text-align:right">এডজাস্ট</td><td style="text-align:right"></td><td style="text-align:right">` + _totadjustAmount.toFixed(2) +`</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)
    $("#tblOrder").append(` <tr><td></td><td>রিজেক্ট</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totrejectGoodsAmount.toFixed(2) + `</td><td style="text-align:right">শর্ট</td><td style="text-align:right"></td><td style="text-align:right">` + _totclosingShortAmount.toFixed(2) +`</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)
    $("#tblOrder").append(` <tr><td></td><td>ক্রয়(রিটার্ন)</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totreturnPurAmount.toFixed(2) + `</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td></tr>`)
    $("#tblOrder").append(` <tr><td></td><td>সেলস এডজাস্ট</td><td></td><td style="text-align:right;   font-weight: bolder;">` + _totSalesAdjustAmount.toFixed(2) + `</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td></tr>`)


    $("#tblOrder").append(` <tr><td></td><td>Neat লাভ</td><td></td><td style="text-align:right;   font-weight: bolder;">` + (_profLossAmnt - _totreturnAmount + _totaddLessAmount + _totreturnPurAmount - _totadjustAmount - _totclosingShortAmount - _totrejectGoodsAmount - _totSalesAdjustAmount).toFixed(2) + `</td><td style="text-align:right">রিজেক্ট</td><td style="text-align:right"></td><td style="text-align:right">` + _totrejectGoodsAmount.toFixed(2) + `</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)
    $("#tblOrder").append(` <tr><td></td><td></td><td></td><td></td><td style="text-align:right">Neat Due</td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"></td><td style="text-align:right"> </td><td style="text-align:right"></td><td style="text-align:right"></td></tr >`)




    LoadSizeData();

        }



    function LoadSizeData() {
        $("#spInfo").html('');

    var model = {
        "CustomerID": queryStrings["customerID"],
    "YearName": queryStrings["yearName"],
    "Type": queryStrings["_type"],
    "dDate": queryStrings["dDate"],
            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Customer/Get_SP_Customer_Close_SizeDetails",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data.obj);
    $("#spInfo").html('');

    BindSizeData(data.obj);

                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        }






    function BindSizeData(_obj) {
            var _totQtySise = 0;
    $("#tblOrder").append(` <tr>
        <td></td>
        <td>সাইজ</td><td>জোড়া</td><td style="text-align:right;   border-right:1px solid black;"></td>`)
        $.each(_obj, function (i, prodData) {

            _totQtySise += parseInt(prodData.salesQtyInPair);

        $("#tblOrder").append(` <tr>
            <td>`+ (i + 1) + `</td>
            <td>`+ prodData.sizeName + `</td><td style="text-align:right;   font-weight: bolder;">` + prodData.salesQtyInPair + `</td><td style="text-align:right;   border-right:1px solid black;"></td>`)
            });



            $("#tblOrder").append(` <tr>
                <td></td>
                <td>মোট </td><td style="text-align:right;   font-weight: bolder; ">`+ _totQtySise +`</td> <td style="text-align:right;   border-right:1px solid black;"></td>`)


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
                    win.document.write('<title>Party_Year_Closing_List_Report</title>');   // <title> FOR PDF HEADER.
                        win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
                        win.document.write('</head>');
                    win.document.write('<body>');
                        win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
                        win.document.write('</body></html>');

                win.document.close(); 	// CLOSE THE CURRENT WINDOW.

                win.print();    // PRINT THE CONTENTS.
    }
         



