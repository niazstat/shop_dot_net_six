
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

            if (vars["customerIDs"] != '0') {

                $("#spFilter").text('FIlter by Customer ');
            }
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
    "ArticleNames": queryStrings["fromDate"],
    "IsDetails": queryStrings["IsDetails"],
    "Type": queryStrings["type"],
            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "SalesReturn/GetSalesReturnReport",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data.obj);
    $("#spInfo").html('');
    $("#spTitle").text(data.resultNo);
    if (queryStrings["IsDetails"] == "1") {
        BindDataDetails(data.obj);
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


        $("#tblOrder").append('<thead> <tr><td>SL</td><td>Invoice No</td><td>Ret Date</td><td>Customer</td><td>Total Return </br>Qty</td><td>Ret Amount</td><td>Ret Comm </br/> Amount</td><td>Actual Ret Amount</td></tr></thead>');


    var _totRetQtyAll = 0;
    var _totRetAmountAll = 0;
    var _totCommAmountAll = 0;
    var _totActualAmountAll = 0;


    var _totRetQty = 0;

    var _totRetAmount = 0;
    var _totCommAmount = 0;
    var _totActualAmount = 0;
    $.each(_obj, function (i, prodData) {

        _totRetQty = 0;

    _totRetAmount = 0;
    _totCommAmount = 0;
    _totActualAmount = 0;


    $.each(prodData.salesReturnDetailsList, function (i, _details) {
        _totRetQty += _details.reyurnQtyInPair;
    _totRetQtyAll += _details.reyurnQtyInPair;


    _totRetAmount += _details.retAmount;
    _totCommAmount += _details.returnCommissionAmount;
    _totActualAmount += _details.retAmount - _details.returnCommissionAmount;


    _totRetAmountAll += _details.retAmount;;
    _totCommAmountAll += _details.returnCommissionAmount;;;
    _totActualAmountAll += _details.retAmount - _details.returnCommissionAmount;;
                });



    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.generatedReturnNo2 + '</td><td>' + prodData.returnDateFormated + '</td><td>' + (prodData.customer == null ? 'Cash' : prodData.customer.shopName) + '</td><td style="text-align:right;">' + _totRetQty + '</td><td style="text-align:right;">' + _totRetAmount.toFixed(2) + '</td><td style="text-align:right;">' + _totCommAmount.toFixed(2) + '</td><td style="text-align:right;">' + _totActualAmount.toFixed(2) + '</td></tr>');


            });


    $("#tblOrder").append('<tr><td colspan="4">Total :</td><td style="text-align:right;">' + _totRetQtyAll + '</td><td style="text-align:right;">' + _totRetAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _totCommAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _totActualAmountAll.toFixed(2) + '</td></tr>');


        }

    function BindDataDetails(_obj) {




            var _totRetQtyAll = 0;
    var _totRetAmountAll = 0;
    var _totCommAmountAll = 0;
    var _totActualAmountAll = 0;


    var _totRetQty = 0;

    var _totRetAmount = 0;
    var _totCommAmount = 0;
    var _totActualAmount = 0;


    $.each(_obj, function (i, prodData) {

        $("#tblOrder").append('  <tr <thead>><td><strong> ' + (i + 1) + '</strong></td><td><strong>Ret Invoice No:</strong></td><td><strong>' + prodData.generatedReturnNo2 + '</strong></td><td>Date :</td><td><strong>' + prodData.returnDateFormated + '</strong></td><td><strong> Pairty Name :</strong></td><td colspan="6" style="text-lign:left"><strong>' + (prodData.customer == null ? '' : prodData.customer.name) + '</strong></td></tr></thead>');

    _totRetQty = 0;

    _totRetAmount = 0;
    _totCommAmount = 0;
    _totActualAmount = 0;

    $("#tblOrder").append('<tr><td></td><td>SL</td><td>Name</td><td>Article</td><td>Size</td><td style="text-align:right;">Ret Qty </br> (Pair)</td><td style="text-align:right;">Ret. Rate</td><td style="text-align:right;">Ret Comm Rate</td><td style="text-align:right;">Return Amount</td><td style="text-align:right;">Comm Amount</td><td style="text-align:right;">Actual Ret Amount</td></tr>');

$.each(prodData.salesReturnDetailsList, function (j, _details) {
    _totRetQty += _details.reyurnQtyInPair;
    _totRetQtyAll += _details.reyurnQtyInPair;


    _totRetAmount += _details.retAmount;
    _totCommAmount += _details.returnCommissionAmount;
    _totActualAmount += _details.retAmount - _details.returnCommissionAmount;


    _totRetAmountAll += _details.retAmount;;
    _totCommAmountAll += _details.returnCommissionAmount;;;
    _totActualAmountAll += _details.retAmount - _details.returnCommissionAmount;;

    $("#tblOrder").append('<tr><td></td><td>' + (j + 1) + '</td><td>' + _details.prodName.name + '</td><td>' + _details.article.name + '</td><td>' + _details.size.name + '</td><td>' + _details.reyurnQtyInPair + '</td><td style="text-align:right;">' + _details.retRate + '</td><td style="text-align:right;">' + _details.returnCommissionRate + '</td><td style="text-align:right;">' + _details.retAmount + '</td><td style="text-align:right;">' + _details.returnCommissionAmount + '</td><td style="text-align:right;">' + (_details.retAmount - _details.returnCommissionAmount) + '</td></tr>');


});


$("#tblOrder").append('<tr style="background-color: #aad4ff;"><td colspan="5" style="text-align:right;"> <strong> Sub Total :</strong></td><td><strong>' + _totRetQty.toFixed(2) + '</strong></td><td colspan="2"></td><td style="text-align:right;"><strong>' + _totRetAmount.toFixed(2) + '</strong></td><td style="text-align:right;"><strong>' + _totCommAmount.toFixed(2) + '</strong></td><td style="text-align:right;"><strong>' + _totActualAmount.toFixed(2) + '</strong></td></tr>');
        
            });


$("#tblOrder").append('<tr style="background-color: #aad4ff;"><td colspan="5" style="text-align:right;"> <strong> Grand Total :</strong></td><td><strong>' + _totRetQtyAll.toFixed(2) + '</strong></td><td colspan="2"></td><td style="text-align:right;"><strong>' + _totRetAmountAll.toFixed(2) + '</strong></td><td style="text-align:right;"><strong>' + _totCommAmountAll.toFixed(2) + '</strong></td><td style="text-align:right;"><strong>' + _totActualAmountAll.toFixed(2) + '</strong></td></tr>');

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
    win.document.write('<title>Sales-Return-Report</title>');   // <title> FOR PDF HEADER.
    win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
    win.document.write('</head>');
    win.document.write('<body>');
    win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
    win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
}
