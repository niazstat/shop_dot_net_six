
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

                $("#spFilter").text('FIlter by Supplier ');
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
    url: WEB_URL + "Purchase/GetPurchaseReport",

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


        $("#tblOrder").append(' < tr ><td>SL</td><td>Invoice No</td><td>Supp Bill No</td><td>Date</td><td>Supplier</td><td>Total Qty</td><td>Total Pur Amount</td><td>Commission</td></tr >');


    var _totQtyAll = 0;
    var _totSalesAll = 0;

    var _totCommissionAll = 0;
    var _totTransChage = 0;

    var _totRecvAmount = 0;


    $.each(_obj, function (i, prodData) {

                var _totQty = 0;
    var _totSales = 0;

    var _totCommission = 0;
    $.each(prodData.purchaseDetailsList, function (i, _details) {
        _totQty += _details.purchaseQtyInPair;
    _totSales += _details.purchaseQtyInPairAmount;
    _totCommission += _details.commissionAmount;

    _totQtyAll += _details.purchaseQtyInPair;
    _totSalesAll += _details.purchaseQtyInPairAmount;
    _totCommissionAll += _details.commissionAmount;
                });


    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.generatedPurchaseHeadNo2 + '</td><td>' + prodData.suppChallanNo + '</td><td>' + prodData.purchaseDateFormated + '</td><td>' + (prodData.supplier == null ? '' : prodData.supplier.name) + '</td><td style="text-align:right;">' + _totQty.toFixed(2) + '</td><td style="text-align:right;">' + _totSales.toFixed(2) + '</td><td style="text-align:right;">' + _totCommission.toFixed(2) + '</td></tr>');


            });


    $("#tblOrder").append('<tr><td colspan="5">ToTal :</td><td style="text-align:right;">' + _totQtyAll.toFixed(2) + '</td><td style="text-align:right;">' + _totSalesAll.toFixed(2) + '</td><td style="text-align:right;">' + _totCommissionAll.toFixed(2) + '</td></tr>');

        }

    function BindDataDetails(_obj) {
            var _totQtyAll = 0;
    var _totSalesAll = 0;
    var _totCommissionAll=00


    $.each(_obj, function (i, prodData) {

        $("#tblOrder").append('  <tr><td><strong> ' + (i + 1) + '</strong></td><td><strong>Invoice No:</strong></td><td><strong>' + prodData.generatedPurchaseHeadNo2 + '</strong></td><td><strong>Supp Bill No:</strong></td><td><strong>' + prodData.suppChallanNo + '</strong></td><td>Date :</td><td><strong>' + prodData.purchaseDateFormated + '</strong></td><td><strong> Supplier Name :</strong></td><td colspan="5" style="text-lign:left"><strong>' + (prodData.supplier == null ? '' : prodData.supplier.name) + '</strong></td></tr>');


    var _totQty = 0;
    var _totSales = 0;
    var _totCommission = 0;

    $("#tblOrder").append('<tr><td></td><td>SL</td><td>Name</td><td>Article</td><td>Size</td><td>Dozen/Pair</td><td>Pair</td><td>Pur Rate</td><td>Comm Rate</td><td>Purchase Amount</td><td>Comm Amount</td><td>Net Amount</td></tr>');

    $.each(prodData.purchaseDetailsList, function (j, _details) {
        _totQty += _details.purchaseQtyInPair;
    _totSales += _details.purchaseQtyInPairAmount;
    _totCommission += _details.commissionAmount;

    _totQtyAll += _details.purchaseQtyInPair;
    _totSalesAll += _details.purchaseQtyInPairAmount;
    _totCommissionAll += _details.commissionAmount;

    $("#tblOrder").append('<tr><td></td><td>' + (j + 1) + '</td><td>' + _details.prodName.name + '</td><td>' + _details.article.name + '</td><td>' + _details.size.name + '</td><td>' + _details.uom.name + '</td><td>' + _details.purchaseQtyInPair + '</td><td>' + _details.purchaseRate + '</td><td>' + _details.commissionRate + '</td><td>' + _details.purchaseQtyInPairAmount + '</td><td>' + _details.commissionAmount + '</td><td>' + (_details.purchaseQtyInPairAmount-_details.commissionAmount )+'</td></tr>');


                });



    $("#tblOrder").append('<tr style="background-color: #aad4ff;"><td colspan="6" style="text-align:right;"> <strong> Sub Total :</strong></td><td><strong>' + _totQty.toFixed(2) + '</strong></td><td></td><td></td><td><strong>' + _totSales.toFixed(2) + '</strong></td><td><strong>' + _totCommission.toFixed() + '</strong></td><td><strong>' + (_totSales - _totCommission) + '</strong></td></tr>');



            });


    $("#tblOrder").append('<tr style="background-color: #aad4ff;"><td colspan="6" style="text-align:right;"> <strong> Grand Total :</strong></td><td><strong>' + _totQtyAll.toFixed(2) + '</strong></td><td></td><td></td><td><strong>' + _totSalesAll.toFixed(2) + '</strong></td><td><strong>' + _totCommissionAll.toFixed() + '</strong></td><td><strong>' + (_totSalesAll - _totCommissionAll) + '</strong></td></tr>');

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
        win.document.write('<title>Sales-Report</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }


