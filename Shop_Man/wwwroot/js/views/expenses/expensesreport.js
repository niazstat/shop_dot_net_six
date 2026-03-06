
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
    "ExpensesHeadIDs": queryStrings["customerIDs"],
    "ArticleNames": queryStrings["fromDate"],
    "IsDetails": queryStrings["IsDetails"],
    "Type": queryStrings["type"],
    "RecvPayType": queryStrings["recvPayType"],
            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Expenses/GetExpensesReport",

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



    function BindData(_obj) {


        $("#tblOrder").append(' <tr><td>SL</td><td>Invoice No</td><td>Date</td><td>Receive/Pay</td><td>Expense Head</td><td>Note</td><td>Recv Amount</td><td>Pay Amount</td></tr >');


    var _totPayAmountAll = 0;
    var _totRecvAmountAll = 0;
    $.each(_obj, function (i, prodData) {
                var _totPayAmount = 0;
    var _totRecvAmount = 0;

    var _payStyle = 'style="Color:green"';


    if (prodData.type == "Payment") {
        _totPayAmount = prodData.amount;
    _totPayAmountAll += prodData.amount;
                   
                }

    else {
        _totRecvAmount = prodData.amount;
    _totRecvAmountAll += prodData.amount;
    _payStyle = 'style="Color:red"';
                }


    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.generatedAutoSLNo + '</td><td>' + prodData.formatedDdate + '</td><td ' + _payStyle+'>' + prodData.type + '</td><td>' + prodData.name + '</td><td>' + prodData.note + '</td><td style="text-align:right;">' + _totRecvAmount.toFixed(2) + '</td><td style="text-align:right;">' + _totPayAmount.toFixed(2) + '</td></tr>');


            });


$("#tblOrder").append('<tr><td colspan="6">ToTal :</td><td style="text-align:right;">' + _totRecvAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _totPayAmountAll.toFixed(2) + '</td></tr>');

        }

function BindDataDetails(_obj) {

    $("#tblOrder").append(' <tr><td>SL</td><td>Invoice No</td><td>Date</td><td>Expense Head</td><td>Receive/Pay</td><td>Recv Amount</td><td>Pay Amount</td><td>Note</td></tr >');

    var _totPayAmountAll = 0;
    var _totRecvAmountAll = 0;
    $.each(_obj, function (i, prodData) {
        var _totPayAmount = 0;
        var _totRecvAmount = 0;

        if (prodData.type == "Payment") {
            _totPayAmount = prodData.amount;
            _totPayAmountAll += prodData.amount;
        }

        else {
            _totRecvAmount = prodData.amount;
            _totRecvAmountAll += prodData.amount;
        }


        $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.generatedAutoSLNo + '</td><td>' + prodData.formatedDdate + '</td><td>' + prodData.name + '</td><td>' + prodData.type + '</td><td style="text-align:right;">' + _totRecvAmount.toFixed(2) + '</td><td style="text-align:right;">' + _totPayAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.note + '</td></tr>');



    });


    $("#tblOrder").append('<tr style="background-color: #aad4ff;"><td colspan="5" style="text-align:right;"> <strong> Grand Total :</strong></td><td><strong>' + _totRecvAmountAll.toFixed(2) + '</strong></td><td><strong>' + _totPayAmountAll.toFixed(2) + '</strong></td><td></td></tr>');

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


