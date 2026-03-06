
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
    STOCK = data.obj;
    // BindDataSizewise();
    STOCK_REPORT = "Product_Name_wise_Stock";
    BindDataNamewise();
                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        }


    $("#btnArticle").click(function () {
        STOCK_REPORT = "Article_wise_Stock";
    BindDataArticlewise();

        });
    $("#btnSize").click(function () {
        STOCK_REPORT = "Size_wise_Stock";
    BindDataSizewise();

        });
    $("#btnName").click(function () {
        STOCK_REPORT = "Product_Name_wise_Stock";
    BindDataNamewise();

        });
    function BindDataNamewise() {
        $("#tblOrder tr").remove();
    let IsnegativeStock = $('#chkNeg').is(':checked');
    var arr = { };
    var _totOPQtyAll = 0;
    var _totAllAty = 0;
    var _totAllPurQty = 0;
    var _totAllPSalesQty = 0;
    var _totAllReturnQty = 0;

    var _totAllAdjustQty = 0;



    var _totAllAvgVal = 0;
    var _totAllCurrVal = 0;

    $.each(STOCK, function (i, prodData) {

                if (IsnegativeStock == true) {

                    if (prodData.stoctQty<0) {
                        if (arr[prodData.prodName]) {

                            var _ooo = arr[prodData.prodName];
    _ooo.push(prodData);

    arr[prodData.prodName] = _ooo;
                        }
    else {
        arr[prodData.prodName] = [prodData];
                        }
                    }
                }
    else {

                    if (arr[prodData.prodName]) {

                        var _ooo = arr[prodData.prodName];
    _ooo.push(prodData);

    arr[prodData.prodName] = _ooo;
                    }
    else {
        arr[prodData.prodName] = [prodData];
                    }
                }
            
            });


    $("#tblOrder").append(' <thead><tr><td>SL</td><td>পণ্যের নাম</td><td>পণ্যের ধরন</td><td>আর্টিকেল</td><td>সাইজ</td><td>Op Qty</td><td>Purchase Qty</td><td>Sales Qty</td><td>Return Qty</td><td>Adjust Qty</td><td>Stock Qty</td><td>Avg Rate</td><td>Curr Rate</td><td>Avg Value</td> <td>Curr Value</td><td></td></tr></thead>');


            Object.entries(arr).forEach(([key, _value]) => {
        console.log(_value);

    if (Array.isArray(_value)) {

                    var _rowSpan = 0;
    $.each(_value, function (i, val2) {
        _rowSpan += 1;
                    })
    var _totOpQty = 0;
    var _totQty = 0;
    var _totPurQty = 0;
    var _totPSalesQty = 0;
    var _totReturnQty = 0;
    var _totAdjustQty = 0;
    var _totAvgVal = 0;
    var _totCurrVal = 0;

    var stkClass = "";
    $.each(_value, function (i, val2) {

                        var _avgRate = 0;

    console.log(IsnegativeStock);



    if (val2.purchaseQty == 0) {
        _avgRate = 0
    }
    else {
        _avgRate = val2.purchaseAmount / val2.purchaseQty;
                            }
    stkClass = "";
    if (val2.stoctQty < 0) {
        stkClass = "Background-color:red; color:white;"
    }


    if (i == 0) {
        $("#tblOrder").append(' <tr><td>' + (i + 1) + '</td><td rowspan=' + _rowSpan + '>' + val2.prodName + '</td><td>' + val2.prodType + '</td><td>' + val2.article + '</td><td>' + val2.size + '</td><td>' + val2.opQty + '</td><td>' + val2.purchaseQty + '</td><td>' + val2.salesQty + '</td><td>' + val2.returnQty + '</td><td>' + val2.adjustQty + '</td><td style="' + stkClass + '">' + val2.stoctQty + '</td><td style="text-align:right">' + _avgRate.toFixed(2) + '</td><td style="text-align:right">' + val2.buyPrice + '</td><td style="text-align:right">' + (_avgRate * val2.stoctQty).toFixed(2) + '</td><td style="text-align:right">' + (val2.buyPrice * val2.stoctQty).toFixed(2) + '</td> <td><a class="btn btn-info stretched-link" target="_blank" href="#" articelName="' + val2.articleID + '" sizeName="' + val2.sizeID + '"  >Details </a></td>  </tr >');


                            }
    else {
        $("#tblOrder").append(' <tr><td>' + (i + 1) + '</td><td>' + val2.prodType + '</td><td>' + val2.article + '</td><td>' + val2.size + '</td><td>' + val2.opQty + '</td><td>' + val2.purchaseQty + '</td><td>' + val2.salesQty + '</td><td>' + val2.returnQty + '</td><td>' + val2.adjustQty + '</td><td style="' + stkClass + '">' + val2.stoctQty + '</td><td style="text-align:right">' + _avgRate.toFixed(2) + '</td><td style="text-align:right">' + val2.buyPrice + '</td><td style="text-align:right">' + (_avgRate * val2.stoctQty).toFixed(2) + '</td><td style="text-align:right">' + (val2.buyPrice * val2.stoctQty).toFixed(2) + '</td> <td><a href="#" target="_blank" class="btn btn-info stretched-link" articelName="' + val2.articleID + '" sizeName="' + val2.sizeID + '"  >Details </a></td></tr >');

                            }

    _totOpQty += val2.opQty;
    _totOPQtyAll += val2.opQty;



    _totQty += val2.stoctQty;
    _totAllAty += val2.stoctQty;

    _totAllPurQty += val2.purchaseQty;
    _totAllPSalesQty += val2.salesQty;;

    _totPurQty += val2.purchaseQty;
    _totPSalesQty += val2.salesQty;

    _totAllReturnQty += val2.returnQty;
    _totReturnQty += val2.returnQty;

    _totAdjustQty += val2.adjustQty;
    _totAllAdjustQty += val2.adjustQty;


    _totAvgVal += _avgRate * val2.stoctQty;
    _totCurrVal += val2.buyPrice * val2.stoctQty;


    _totAllAvgVal += _avgRate * val2.stoctQty;
    _totAllCurrVal += val2.buyPrice * val2.stoctQty;

                        

                        });

    $("#tblOrder").append(' <tr><td colspan="5"> Sub Total :</td><td>' + _totOpQty + '</td> <td>' + _totPurQty + '</td><td>' + _totPSalesQty + '</td> <td>' + _totAdjustQty + '</td><td>' + _totAdjustQty + '</td><td>' + _totQty + '</td>  <td></td> <td></td> <td style="text-align:right">' + _totAvgVal.toFixed(2) + '</td> <td style="text-align:right">' + _totCurrVal.toFixed(2) + '</td>   </tr >');


                    }
             

            });

    $("#tblOrder").append(' <tr><td colspan="5"> Total :</td><td>' + _totOPQtyAll + '</td><td>' + _totAllPurQty + '</td><td>' + _totAllPSalesQty + '</td><td>' + _totAllReturnQty + '</td><td>' + _totAllAdjustQty + '</td><td>' + _totAllAty + '</td>  <td></td> <td></td> <td style="text-align:right">' + _totAllAvgVal.toFixed(2) + '</td> <td style="text-align:right">' + _totAllCurrVal.toFixed(2) + '</td></tr >');
    arr = { };
        }



    function BindDataArticlewise() {
        let IsnegativeStock = $('#chkNeg').is(':checked');
    $("#tblOrder tr").remove();
    var arr = { };




    var _totOPQtyAll = 0;

    var _totAllAty = 0;
    var _totAllPurQty = 0;
    var _totAllPSalesQty = 0;
    var _totAllReturnQty = 0;

    var _totAllAvgVal = 0;
    var _totAllCurrVal = 0;


    var _totAllAdjustQty = 0;

    $.each(STOCK, function (i, prodData) {




                if (IsnegativeStock == true) {

                    if (prodData.stoctQty < 0) {
                        if (arr[prodData.article]) {

                            var _ooo = arr[prodData.article];
    _ooo.push(prodData);

    arr[prodData.article] = _ooo;
                        }
    else {
        arr[prodData.article] = [prodData];
                        }
                    }
                }
    else {

                    if (arr[prodData.article]) {

                        var _ooo = arr[prodData.article];
    _ooo.push(prodData);

    arr[prodData.article] = _ooo;
                    }
    else {
        arr[prodData.article] = [prodData];
                    }
                }



            });


    $("#tblOrder").append('<thead> <tr><td>SL</td><td>আর্টিকেল</td><td>পণ্যের নাম</td><td>পণ্যের ধরন</td><td>সাইজ</td><td>Op Qty</td><td>Purchase Qty</td><td>Sales Qty</td><td>Return Qty</td><td>Adjust Qty</td><td>Stock Qty</td><td>Avg Rate</td><td>Curr Rate</td><td>Avg Value</td> <td>Curr Value</td></tr ></thead>');


            Object.entries(arr).forEach(([key, _value]) => {
        console.log(_value);

    var _str = '';

    if (Array.isArray(_value)) {
                    var _rowSpan = 0;
    $.each(_value, function (i, val2) {
        _rowSpan += 1;
                    })

    var stkClass = "";

    var _totOpQty = 0;



    var _totQty = 0;
    var _totPurQty = 0;
    var _totPSalesQty = 0;
    var _totReturnQty = 0;

    var _totAvgVal = 0;
    var _totCurrVal = 0;


    var _totAdjustQty = 0;

    $.each(_value, function (i, val2) {
                        var stkClass = "";
    var _avgRate = 0;

    if (val2.purchaseQty == 0) {
        _avgRate = 0
    }
    else {
        _avgRate = val2.purchaseAmount / val2.purchaseQty;
                        }
    stkClass = "";
    if (val2.stoctQty < 0) {
        stkClass = "Background-color:red; color:white;"
    }



    if (i == 0) {
        _str += ' <tr style="background-color:#fff"><td>' + (i + 1) + '</td> <td rowspan=' + _rowSpan + '>' + val2.article + '</td><td >' + val2.prodName + '</td><td>' + val2.prodType + '</td><td>' + val2.size + '</td><td style="text-align:right">' + val2.opQty + '</td><td style="text-align:right">' + val2.purchaseQty + '</td><td style="text-align:right">' + val2.salesQty + '</td><td style="text-align:right">' + val2.returnQty + '</td><td>' + val2.adjustQty + '</td><td style="' + stkClass + '">' + val2.stoctQty + '</td><td style="text-align:right">' + _avgRate.toFixed(2) + '</td><td style="text-align:right">' + val2.buyPrice + '</td><td style="text-align:right">' + (_avgRate * val2.stoctQty).toFixed(2) + '</td><td style="text-align:right">' + (val2.buyPrice * val2.stoctQty).toFixed(2) + '</td></tr >';

                        }
    else {
        _str += ' <tr style="background-color:#fff"><td>' + (i + 1) + '</td><td>' + val2.prodName + '</td><td>' + val2.prodType + '</td><td>' + val2.size + '</td><td style="text-align:right">' + val2.opQty + '</td><td style="text-align:right">' + val2.purchaseQty + '</td><td style="text-align:right">' + val2.salesQty + '</td><td style="text-align:right">' + val2.returnQty + '</td><td>' + val2.adjustQty + '</td><td style="' + stkClass + '">' + val2.stoctQty + '</td><td style="text-align:right">' + _avgRate.toFixed(2) + '</td><td style="text-align:right">' + val2.buyPrice + '</td><td style="text-align:right">' + (_avgRate * val2.stoctQty).toFixed(2) + '</td><td style="text-align:right">' + (val2.buyPrice * val2.stoctQty).toFixed(2) + '</td></tr >';

                        }

    _totOpQty += val2.opQty;
    _totOPQtyAll += val2.opQty;



    _totQty += val2.stoctQty;
    _totAllAty += val2.stoctQty;

    _totAllPurQty += val2.purchaseQty;
    _totAllPSalesQty += val2.salesQty;;

    _totPurQty += val2.purchaseQty;
    _totPSalesQty += val2.salesQty;


    _totAvgVal += _avgRate * val2.stoctQty;
    _totCurrVal += val2.buyPrice * val2.stoctQty;


    _totAllAvgVal += _avgRate * val2.stoctQty;
    _totAllCurrVal += val2.buyPrice * val2.stoctQty;


    _totAllReturnQty += val2.returnQty;
    _totReturnQty += val2.returnQty;

    _totAdjustQty += val2.adjustQty;
    _totAllAdjustQty += val2.adjustQty;


                    });
    $("#tblOrder").append(_str);
    $("#tblOrder").append(' <tr style="background-color:#ccc"><td colspan="5"> Sub Total :</td><td style="text-align:right">' + _totOpQty.toFixed(2) + '</td><td style="text-align:right">' + _totPurQty.toFixed(2) + '</td><td style="text-align:right">' + _totPSalesQty.toFixed(2) + '</td><td style="text-align:right">' + _totReturnQty.toFixed(2) + '</td>  <td>' + _totAdjustQty.toFixed(2) + '</td> <td style="text-align:right">' + _totQty.toFixed(2) + '</td>  <td></td> <td></td> <td style="text-align:right">' + _totAvgVal.toFixed(2) + '</td> <td style="text-align:right">' + _totCurrVal.toFixed(2) + '</td> </tr >');

                }


            });

    $("#tblOrder").append(' <tr><td colspan="5"> Total :</td><td style="text-align:right">' + _totOPQtyAll.toFixed(2) + '</td><td>' + _totAllPurQty.toFixed(2) + '</td><td style="text-align:right">' + _totAllPSalesQty.toFixed(2) + '</td><td style="text-align:right">' + _totAllReturnQty.toFixed(2) + '</td> <td>' + _totAllAdjustQty.toFixed(2) + '</td> <td style="text-align:right">' + _totAllAty.toFixed(2) + '</td>  <td></td> <td></td> <td style="text-align:right">' + _totAllAvgVal.toFixed(2) + '</td> <td style="text-align:right">' + _totAllCurrVal.toFixed(2) + '</td></tr >');

    arr = { };
        }



    function BindDataSizewise() {
        let IsnegativeStock = $('#chkNeg').is(':checked');
    $("#tblOrder tr").remove();
    var arr = { };
    var _totOPQtyAll = 0;
    var _totAllAty = 0;
    var _totAllPurQty = 0;
    var _totAllPSalesQty = 0;

    var _totAllAvgVal = 0;
    var _totAllCurrVal = 0;

    var _totAllReturnQty = 0;

    var _totAllAdjustQty = 0;

    $.each(STOCK, function (i, prodData) {



                if (IsnegativeStock == true) {

                    if (prodData.stoctQty < 0) {
                        if (arr[prodData.size]) {

                            var _ooo = arr[prodData.size];
    _ooo.push(prodData);

    arr[prodData.size] = _ooo;
                        }
    else {
        arr[prodData.size] = [prodData];
                        }
                    }
                }
    else {

                    if (arr[prodData.size]) {

                        var _ooo = arr[prodData.size];
    _ooo.push(prodData);

    arr[prodData.size] = _ooo;
                    }
    else {
        arr[prodData.size] = [prodData];
                    }
                }

            });


    $("#tblOrder").append('<thead> <tr><td>SL</td><td>সাইজ</td><td>আর্টিকেল</td><td>পণ্যের নাম</td><td>পণ্যের ধরন</td><td>Op Qty</td><td>Purchase Qty</td><td>Sales Qty</td><td>Return Qty</td><td>Adjust Qty</td><td>Stock Qty</td><td>Avg Rate</td><td>Curr Rate</td><td>Avg Value</td> <td>Curr Value</td></tr ></thead>');


            Object.entries(arr).forEach(([key, _value]) => {
        console.log(_value);

    var _str = '';

    if (Array.isArray(_value)) {
                    var _rowSpan = 0;
    $.each(_value, function (i, val2) {
        _rowSpan += 1;
                    })

    var _totOpQty = 0;
    var _totQty = 0;
    var _totPurQty = 0;
    var _totPSalesQty = 0;


    var _totAvgVal = 0;
    var _totCurrVal = 0;
    var _totReturnQty = 0;

    var _totAdjustQty = 0;
    var stkClass = "";

    $.each(_value, function (i, val2) {

                        var stkClass = "";
    var _avgRate = 0;

    if (val2.purchaseQty == 0) {
        _avgRate = 0
    }
    else {
        _avgRate = val2.purchaseAmount / val2.purchaseQty;
                        }
    stkClass = "";
    if (val2.stoctQty < 0) {
        stkClass = "Background-color:red; color:white;"
    }

    if (i == 0) {
        _str += ' <tr><td>' + (i + 1) + '</td><td rowspan=' + _rowSpan + '>' + val2.size + '</td> <td>' + val2.article + '</td><td >' + val2.prodName + '</td><td>' + val2.prodType + '</td><td style="text-align:right">' + val2.opQty + '</td><td style="text-align:right">' + val2.purchaseQty + '</td><td style="text-align:right">' + val2.salesQty + '</td><td style="text-align:right">' + val2.returnQty + '</td> <td>' + val2.adjustQty + '</td> <td style="' + stkClass + '">' + val2.stoctQty + '</td><td style="text-align:right">' + _avgRate.toFixed(2) + '</td><td style="text-align:right">' + val2.buyPrice + '</td><td style="text-align:right">' + (_avgRate * val2.stoctQty).toFixed(2) + '</td><td style="text-align:right">' + (val2.buyPrice * val2.stoctQty).toFixed(2) + '</td></tr >';

                        }
    else {
        _str += ' <tr><td>' + (i + 1) + '</td> <td>' + val2.article + '</td><td>' + val2.prodName + '</td><td>' + val2.prodType + '</td><td style="text-align:right">' + val2.opQty + '</td><td style="text-align:right">' + val2.purchaseQty + '</td><td style="text-align:right">' + val2.salesQty + '</td><td style="text-align:right">' + val2.returnQty + '</td> <td>' + val2.adjustQty + '</td><td style="' + stkClass + '">' + val2.stoctQty + '</td><td style="text-align:right">' + _avgRate.toFixed(2) + '</td><td style="text-align:right">' + val2.buyPrice + '</td><td style="text-align:right">' + (_avgRate * val2.stoctQty).toFixed(2) + '</td><td style="text-align:right">' + (val2.buyPrice * val2.stoctQty).toFixed(2) + '</td></tr >';

                        }

    _totOpQty += val2.opQty;
    _totOPQtyAll += val2.opQty;


    _totQty += val2.stoctQty;
    _totAllAty += val2.stoctQty;

    _totAllPurQty += val2.purchaseQty;
    _totAllPSalesQty += val2.salesQty;;

    _totPurQty += val2.purchaseQty;
    _totPSalesQty += val2.salesQty;


    _totAvgVal += _avgRate * val2.stoctQty;
    _totCurrVal += val2.buyPrice * val2.stoctQty;


    _totAllAvgVal += _avgRate * val2.stoctQty;
    _totAllCurrVal += val2.buyPrice * val2.stoctQty;

    _totAllReturnQty += val2.returnQty;
    _totReturnQty += val2.returnQty;


    _totAdjustQty += val2.adjustQty;
    _totAllAdjustQty += val2.adjustQty;
                    });
    $("#tblOrder").append(_str);
    $("#tblOrder").append(' <tr style="background-color:#ccc"><td colspan="5"> Sub Total :</td><td style="text-align:right">' + _totOpQty.toFixed(2) + '</td><td style="text-align:right">' + _totPurQty.toFixed(2) + '</td><td style="text-align:right">' + _totPSalesQty.toFixed(2) + '</td><td style="text-align:right">' + _totAdjustQty.toFixed(2) + '</td> <td style="text-align:right">' + _totAdjustQty.toFixed(2) + '</td><td style="text-align:right">' + _totQty.toFixed(2) + '</td>  <td></td> <td></td> <td style="text-align:right">' + _totAvgVal.toFixed(2) + '</td> <td style="text-align:right">' + _totCurrVal.toFixed(2) + '</td> </tr >');

                }


            });

    $("#tblOrder").append(' <tr><td colspan="5"> Total :</td><td style="text-align:right">' + _totOPQtyAll.toFixed(2) + '</td><td>' + _totAllPurQty.toFixed(2) + '</td><td style="text-align:right">' + _totAllPSalesQty.toFixed(2) + '</td><td style="text-align:right">' + _totAllReturnQty.toFixed(2) + '</td> <td style="text-align:right">' + _totAllAdjustQty.toFixed(2) + '</td> <td style="text-align:right">' + _totAllAty.toFixed(2) + '</td>  <td></td> <td></td> <td style="text-align:right">' + _totAllAvgVal.toFixed(2) + '</td> <td style="text-align:right">' + _totAllCurrVal.toFixed(2) + '</td></tr >');

    arr = { };
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
