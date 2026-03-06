
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

    var CATEGORYWISE_CUSTOMER = [];
    var ALL_DATA = [];
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

        "dDate": queryStrings["dDate"] == null ? '01-Jan-1900' : queryStrings["dDate"],
    "YearName": queryStrings["yearName"] == null ? '0' : queryStrings["yearName"],
                }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Customer/Get_SP_View_ALL_Customer_Year_Close_Previous",

    data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data.obj);
    $("#spInfo").html('');
    ALL_DATA = data.obj;
    BindData(ALL_DATA);

                    },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                    }
                });

            }



    $("#btnActive").click(function () {

                var _allData = ALL_DATA.filter(_a => _a.isBlocked == 0)


    BindData(_allData);

            });



    $("#btnDeActive").click(function () {

                var _allData = ALL_DATA.filter(_a => _a.isBlocked == 1)


    BindData(_allData);

            });


    $("#btnAll").click(function () {



        BindData(ALL_DATA);

            });



    function BindData(_obj) {
        $('#tblOrder').empty()

                $("#tblOrder").append(`<thead>
        < tr >
            <td>SL</td>
            <td>ID</td>
            <td>Name</td>
            <td>Shop Name</td>
            <td>Close Date</td>
            <td>সেলস</td>
            <td>ক্রয়</td>
            <td>লাভ</td>

            <td>কম/বেশি</td>

            <td>এডজাস্ট</td>
            <td>রিটার্ন </td>
            <td> ক্রয় <br>(রিটার্ন)</td>
            <td>শর্ট</td>
            <td>রিজেক্ট</td>
            <td>সেলস <br> এডজাস্ট</td>
            <td>Net Profit</td>
        </tr >
    </thead >`);

    var _totSalesAmount = 0;
    var _totBuyAmount = 0;

    var _totaddLessAmount = 0;


    var _totadjustAmount = 0;

    var _totreturnAmount = 0;

    var _totreturnPurAmount = 0;

    var _totclosingShortAmount = 0;

    var _totrejectGoodsAmount = 0;
    var _profLossAmnt = 0;

    var _netProfitAmount = 0;

    var _totSalesAdjustAmount = 0;

    $.each(_obj, function (i, prodData) {


        _totSalesAmount += prodData.salesAmount;

    _totBuyAmount += prodData.purchaseAmount;

    _profLossAmnt += (prodData.salesAmount - prodData.purchaseAmount);

    _totaddLessAmount += prodData.addLessAmount;

    _totreturnAmount += prodData.returnAmount;

    _totadjustAmount += prodData.adjustAmount;


    _totreturnPurAmount += prodData.retPurchaseAmount;

    _totclosingShortAmount += prodData.closingShortAmount;

    _totrejectGoodsAmount += prodData.rejectGoodsAmount;


    _totSalesAdjustAmount += prodData.salesAdjustAmount;



    _netProfitAmount += prodData.netProfit;




    $("#tblOrder").append(` <tr>
        <td>`+ (i + 1) + `</td>
        <td>`+ prodData.customerID + `</td>
        <td>`+ prodData.customerName + `</td>
        <td>`+ prodData.shopName + `</td>
        <td>`+ prodData.yearCloseDateFormated + `</td>
        <td style="text-align:right">`+ prodData.salesAmount.toFixed(2) + `</td>
        <td style="text-align:right">`+ prodData.purchaseAmount.toFixed(2) + `</td>
        <td style="text-align:right">`+ (parseFloat(prodData.salesAmount) - parseFloat(prodData.purchaseAmount)).toFixed(2) + `</td>

        <td style="text-align:right">`+ prodData.addLessAmount.toFixed(2) + `</td>

        <td style="text-align:right">`+ prodData.adjustAmount.toFixed(2) + `</td>
        <td style="text-align:right">`+ prodData.returnAmount.toFixed(2) + ` </td>
        <td style="text-align:right">`+ prodData.retPurchaseAmount.toFixed(2) + ` </td>
        <td style="text-align:right">`+ prodData.closingShortAmount.toFixed(2) + `</td>
        <td style="text-align:right">`+ prodData.rejectGoodsAmount.toFixed(2) + `</td>

        <td style="text-align:right">`+ prodData.salesAdjustAmount.toFixed(2) + `</td>
        <td style="text-align:right">`+ prodData.netProfit.toFixed(2) + `</td>
    </tr >`);


                });

    $("#tblOrder").append(` <tr>
        <td colspan="5">Total :</td>

        <td style="text-align:right;   font-weight: bolder;">`+ _totSalesAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totBuyAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _profLossAmnt.toFixed(2) + `</td>

        <td style="text-align:right;   font-weight: bolder;">`+ _totaddLessAmount.toFixed(2) + `</td>

        <td style="text-align:right;   font-weight: bolder;">`+ _totadjustAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totreturnAmount.toFixed(2) + ` </td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totreturnPurAmount.toFixed(2) + ` </td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totclosingShortAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totrejectGoodsAmount.toFixed(2) + `</td>
        <td style="text-align:right;   font-weight: bolder;">`+ _totSalesAdjustAmount.toFixed(2) + `</td>


        <td style="text-align:right;   font-weight: bolder;">`+ _netProfitAmount.toFixed(2) + `</td>
    </tr >`);




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

