
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
    url: WEB_URL + "SalesAdjust/GetSalesAdjustReport",

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


        $("#tblOrder").append('<thead> <tr><td>SL</td><td>Adjust No</td><td>Bill No</td><td>Date</td><td>Customer</td><td>Adjust Amount</td></tr></thead>');

    var _totAdjQtyAll = 0;
    var _totAdjAmountAll = 0;

    $.each(_obj, function (i, prodData) {

                var _totAdjQty = 0;
    var _totAdjAmount = 0;

    var _totCommission = 0;
    $.each(prodData.salesAdjustDetailsList, function (i, _details) {
        _totAdjQty += _details.salesAdjustInPair;
    _totAdjAmount += _details.salesAdjustAmount;


    _totAdjQtyAll += _details.salesAdjustInPair;
    _totAdjAmountAll += _details.salesAdjustAmount;
                });



    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.generatedSalesAdjustNo + '</td><td>' + prodData.salesAdjustNo + '</td><td>' + prodData.salesAdjustFormated + '</td><td>' + (prodData.customer == null ? 'Cash' : prodData.customer.shopName) + '</td><td style="text-align:right;">' + _totAdjAmount.toFixed(2) + '</td></tr>');


            });


    $("#tblOrder").append('<tr><td colspan="5">ToTal :</td><td style="text-align:right;">' + _totAdjAmountAll.toFixed(2) + '</td></tr>');

        }

    function BindDataDetails(_obj) {




            var _totAdjQtyAll = 0;
    var _totAdjAmountAll = 0;


    $.each(_obj, function (i, prodData) {

        $("#tblOrder").append('  <tr <thead>><td><strong> ' + (i + 1) + '</strong></td><td><strong>Adjust No:</strong></td><td><strong>' + prodData.generatedSalesAdjustNo + ' / ' + prodData.salesAdjustNo + '</strong></td><td>Date :</td><td><strong>' + prodData.salesAdjustFormated + '</strong></td><td><strong> Party Name :</strong></td><td colspan="2" style="text-lign:left"><strong>' + (prodData.customer == null ? '' : prodData.customer.name) + '</strong></td></tr></thead>');


    var _totQty = 0;
    var _totSales = 0;

    var _totCommission = 0;

    $("#tblOrder").append('<tr><td></td><td>SL</td><td>Name</td><td>Article</td><td>Size</td><td>Adj Qty</td><td>Adj Rate</td><td>Adust Amount</td></tr>');

    $.each(prodData.salesAdjustDetailsList, function (j, _details) {
                    var _totAdjQty = 0;
    var _totAdjAmount = 0;


    _totAdjQty += _details.salesAdjustInPair;
    _totAdjAmount += _details.salesAdjustAmount;


    _totAdjQtyAll += _details.salesAdjustInPair;
    _totAdjAmountAll += _details.salesAdjustAmount;
    $("#tblOrder").append('<tr><td></td><td>' + (j + 1) + '</td><td>' + _details.prodName.name + '</td><td>' + _details.article.name + '</td><td>' + _details.size.name + '</td><td>' + _details.salesAdjustInPair + '</td><td style="text-align:right;">' + _details.salesAdjustRate + '</td><td style="text-align:right;">' + _details.salesAdjustAmount + '</td></tr>');


                });

          

            });


            // $("#tblOrder").append('<tr><td colspan="6" style="text-align:right;"> <strong> Grand Total :</strong></td><td><strong>' + _totQtyAll.toFixed(2) + '</strong></td><td></td><td></td><td></td><td></td><td><strong>' + _totSalesAll.toFixed(2) + '</strong></td><td><strong>' + _totCommissionAll.toFixed() + '</strong></td><td style="text-align:right;"><strong>' + (_totSalesAll - _totCommissionAll) + '</strong></td></tr>');

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
