
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var STOCK = [];
    var STOCK_REPORT = '';
    $(document).ready(function () {
        LoadData();
    function LoadData() {
        $("#spInfo").html('');



    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "CompanyProduct/GetProductCurrentStock",

    // data: JSON.stringify(model),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data.obj);
    $("#spInfo").html('');
    var  _stock = data.obj;
    // BindDataSizewise();
    STOCK_REPORT = "Product_Name_wise_Stock";
    BindDataNamewise(_stock);
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        }



    function BindDataNamewise(_stock) {
        $("#tblOrder tr").remove();

    var stkClass = "";

    let Op_Qty = 0;

    let Pur_Qty = 0;

    let Net_Pur_Val = 0;
    let Sales_Qty=0, Net_Sales_Val=0, Return_Qty=0, Net_Ret_Val=0, Pur_Ret_Qty=0, Pur_Ret_Val=0, Clos_Qty = 0;
    let Net_ProfAmount = 0;


    $("#tblOrder").append(' <thead><tr><td>SL</td><td>আর্টিকেল</td><td>সাইজ</td><td>Op Qty</td><td>Pur Qty</td><td>Net Pur Val</td><td>Sales Qty</td><td>Net Sales Val</td><td>Return Qty</td><td>Net Ret Val</td><td>Pur+Ret Qty</td><td>Pur+Ret Val</td><td>Per Itm  Rate</td><td>Per Itm Sal Rate</td> <td>Rate Diff</td><td>Profit Amount</td><td></td></tr></thead>');

    $.each(_stock, function (i, val2) {
        stkClass = "text-align: right";
    if (val2.purchaseQty == 0) {
        stkClass = "Background-color:red; color:white; text-align: right";
                }

                var _perItemRate = (val2.purchaseQty + val2.returnQty)>0? (val2.purchaseAmount + val2.returnPrice) / (val2.purchaseQty + val2.returnQty):0;
                var _perItemSalesRate = val2.salesQty > 0 ? val2.netSalesAmount / val2.salesQty : 0;

    var _profAmnt = (_perItemSalesRate - _perItemRate) * val2.salesQty;
    Net_ProfAmount += _profAmnt;
    Op_Qty += val2.opQty;
    Pur_Qty += val2.purchaseQty;
    Net_Pur_Val += val2.purchaseAmount;
    Sales_Qty += val2.salesQty;
    Net_Sales_Val += val2.netSalesAmount;
    Return_Qty += val2.returnQty;
    Net_Ret_Val += val2.returnPrice;
    Pur_Ret_Qty += (val2.purchaseQty + val2.returnQty);
    Pur_Ret_Val += (val2.purchaseAmount + val2.returnPrice);


    Clos_Qty += val2.stoctQty;

    $("#tblOrder").append(' <tr><td>' + (i + 1) + '</td><td>' + val2.article + '</td><td>' + val2.size + '</td><td style="text-align:right">' + val2.opQty + '</td><td style="text-align:right">' + val2.purchaseQty + '</td><td style="text-align:right">' + val2.purchaseAmount + '</td><td style="text-align:right">' + val2.salesQty + '</td><td style="text-align:right">' + val2.netSalesAmount + '</td><td style="text-align:right">' + val2.returnQty + '</td><td style="text-align:right">' + val2.returnPrice + '</td><td style="text-align:right">' + (val2.purchaseQty + val2.returnQty) + '</td><td style="text-align:right">' + (val2.purchaseAmount + val2.returnPrice) + '</td><td style="' + stkClass + '">' + _perItemRate.toFixed(2) + '</td><td style="text-align:right">' + _perItemSalesRate.toFixed(2) + '</td> <td style="' + stkClass + '">' + (_perItemSalesRate - _perItemRate).toFixed(2) + '</td><td style="' + stkClass + '">' + _profAmnt.toFixed(2)+ '</td><td><a href="#" target="_blank" class="btn btn-info stretched-link" articelName="' + val2.articleID + '" sizeName="' + val2.sizeID + '"  >Details </a></td></tr>');
            });
    console.log(Op_Qty);
    $("#tblOrder").append(' <tr><td colspan="3"> Total : </td><td style="text-align:right">' + Op_Qty.toFixed(2) + '</td><td style="text-align:right">' + Pur_Qty.toFixed(2) + '</td><td style="text-align:right">' + Net_Pur_Val.toFixed(2) + '</td><td style="text-align:right">' + Sales_Qty.toFixed(2) + '</td><td style="text-align:right">' + Net_Sales_Val.toFixed(2) + '</td><td style="text-align:right">' + Return_Qty.toFixed(2) + '</td><td style="text-align:right">' + Net_Ret_Val.toFixed(2) + '</td><td style="text-align:right">' + Pur_Ret_Qty + '</td><td style="text-align:right">' + Pur_Ret_Val.toFixed(2) + '</td><td style="text-align:right">' + (Pur_Ret_Val / Pur_Ret_Qty).toFixed(2) + '</td><td style="text-align:right">' + (Net_Sales_Val / Sales_Qty).toFixed(2) + '</td> <td style="text-align:right"></td><td>' + Net_ProfAmount.toFixed(2) +'</td><td></td></tr>');

        }










    $("#tblOrder").on('click','a', function () {

        let _artName = $(this).attr('articelname');
    let _sizeName = $(this).attr('sizename');


    $(this).attr('href', WEB_URL + 'CompanyProduct/ProductCurrentStockArticleSize?_articleID=' + _artName + "&&_sizeID=" + _sizeName );



        });

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
        win.document.write('<title>'+STOCK_REPORT+'</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }
