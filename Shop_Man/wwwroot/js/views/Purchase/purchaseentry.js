
    var days = ['Saturday','Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday' ];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var BillNo_PDF = "";

    var CUST_LIST = [];
    var CUST_CAT = [];
    var SUB_CUST_CAT = [];
    var PROD_NAMES = [];
    var COMP_PRODUCTS = [];

    var specialChars = [
        '০',
        '১',
        '২',
        '৩',
        '৪',
        '৫',
        '৬',
        '৭',
        '৮',
        '৯',
        '0',
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9',
        '.',
        '.',
        '-',
        '-'
    ];
    var BeagaliToEnglish = {
        '০': '0',
        '১': '1',
        '২': '2',
        '৩': '3',
        '৪': '4',
        '৫': '5',
        '৬': '6',
        '৭': '7',
        '৮': '8',
        '৯': '9',
        '0': '0',
        '1': '1',
        '2': '2',
        '3': '3',
        '4': '4',
        '5': '5',
        '6': '6',
        '7': '7',
        '8': '8',
        '9': '9',
        '.': '.',
        '.': '.',

        '-': '-',
        '-': '-'

    }

    var EnglisgToBengali = {
        '০': '০',
        '১': '১',
        '২': '২',
        '৩': '৩',
        '৪': '৪',
        '৫': '৫',
        '৬': '৬',
        '৭': '৭',
        '৮': '৮',
        '৯': '9',
        '0': '০',
        '1': '১',
        '2': '২',
        '3': '৩',
        '4': '৪',
        '5': '৫',
        '6': '৬',
        '7': '৭',
        '8': '৮',
        '9': '৯',
        '.': '.',
        '.': '.',
        '-': '-',
        '-': '-'

    }


    function ConvertBanglaToEnglish(_str) {
        var res = '';

        if (_str == '') return '';

        var _arr = _str.split('');
        for (var i = 0; i < _arr.length; i++) {
            res += BeagaliToEnglish[_arr[i]];
        }
        return res;
    }

    function ConvertEnglishToBengali(_str) {
        var _res = '';

        if (_str == '') return '';

        var _arr = _str.split('');
        for (var i = 0; i < _arr.length; i++) {
            _res += EnglisgToBengali[_arr[i]];
        }
        return _res;
    }



    $(document).ready(function () {


        var date = new Date();
        var today = date.getDate() + '-' + months[date.getMonth()] + '-' + date.getFullYear();
    //        new Date(date.getFullYear(), date.getMonth(), date.getd());

        //$("#txtDate2").datepicker('setDate', 'now');
        //alert('OK');


        datePicker1 = flatpickr("#txtDate", { dateFormat: "d-M-Y" });
        datePicker1.setDate(date);




        LoadDefaultData();

        //$("#txtDateEntry").datepicker({
        //    autoclose: true,
        //}).on('changeDate', function (e) {
        //    ChangeDt("#txtDateEntry");
        //});

        //$("#txtDate").datepicker({
        //    autoclose: true,
        //}).on('changeDate', function (e) {
        //    ChangeDt("#txtDate");
        //});


        //$("#txtDate").datepicker({
        //    autoclose: true,
        //});

        $("#txtDateEntry").val(today);
     /*   $("#txtDate").val(today);*/


        $("input").on('keypress', function (event) {

            // alert('ok');
            var _currentTab = $(this).attr("data-tab");
            var _id = $(this).attr("id");

            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                if (_currentTab != undefined) {
                    if (_id !== 'btnAdd') {
                        var _nextTab = parseInt(_currentTab) + 1;
                        $('input[data-tab="' + _nextTab + '"]').focus();
                    }
                }
            }


        });

        function ChangeDt(caller) {

            //console.log(caller);
            let _thisVal2 = $(caller).val();

            if (_thisVal2.indexOf('-') > 0) {

                let _existValue = _thisVal2.split('-');
                $(caller).val(_existValue[0] + '-' + months[_existValue[1] - 1] + '-' + _existValue[2]);

            }



        };



        function LoadDefaultData() {
            //ClearControl();
            $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
            $.ajax({
                type: "POST",
                url: WEB_URL + "Purchase/GetPurchaseEntryViewModel",

                // data: JSON.stringify(receiveMain),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    // HideGateOverlay();
                    $("#spInfo").html('');
                    console.log(data);
                    //RECEIVE = data;
                    //BindProduct(data);
                    //$("#spInfo").html('');
                    CUST_CAT = data.supplierCategorys;
                    CUST_LIST = data.suppliers;
                    SUB_CUST_CAT = data.supplierSubCategorys;
                    PROD_NAMES = data.prodNames;
                    BindCustCategoryName(data.supplierCategorys);
                    BindProdName(data.prodNames);
                   
                   // BinSubCategoryTable(data.customerSubCategorys);


                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });
        }

        $("#btnNext").click(function () {
            $("#spInfo").html('');
            let _purchaseNo = ConvertBanglaToEnglish($("#txtBIllNo").val());
            if (_purchaseNo == '' || _purchaseNo == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Purcase No</strong>');
                $("#txtBIllNo").focus();
                return;
            }
            var purchaseHead = {
                "GeneratedPurchaseHeadNo": _purchaseNo
            }

            $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
            $.ajax({
                type: "POST",
                url: WEB_URL + "Purchase/GetPurchaseByNoNext",

                data: JSON.stringify(purchaseHead),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    console.log(data);
                    if (data.obj != null) {

                        $("#btnSaveCust").removeAttr('disabled');
                        $("#btnAdd").removeAttr('disabled');
                        $("#txtBIllNo").val(data.obj.generatedSalesNo);
                        $("#spInfo").html('');
                        ClearControlAfterSave();

                        $("#btnSaveCust").val('Update');
                        BindData(data.obj, '');
                        $("#spInfo").html('');

                    }
                    else {
                        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }


                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });

        });



        $("#btnPrev").click(function () {

            $("#spInfo").html('');
            let _purchaseNo = ConvertBanglaToEnglish($("#txtBIllNo").val());
            if (_purchaseNo == '' || _purchaseNo == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Purcase No</strong>');
                $("#txtBIllNo").focus();
                return;
            }
            var purchaseHead = {
                "GeneratedPurchaseHeadNo": _purchaseNo
            }

            $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
            $.ajax({
                type: "POST",
                url: WEB_URL + "Purchase/GetPurchaseByNoPrev",

                data: JSON.stringify(purchaseHead),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    console.log(data);
                    if (data.obj != null) {

                        $("#btnSaveCust").removeAttr('disabled');
                        $("#btnAdd").removeAttr('disabled');
                        $("#txtBIllNo").val(data.obj.generatedSalesNo);
                        $("#spInfo").html('');
                        ClearControlAfterSave();

                        $("#btnSaveCust").val('Update');
                        BindData(data.obj, '');
                        $("#spInfo").html('');

                    }
                    else {
                        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }


                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });

        });

        $("#btnAdd").click(function () {

            $("#spInfo").html('');

            let _purchaseHeadID = $("#btnSaveCust").attr("data-purchaseheadid");
      
            let _purchaseDetailsID = $(this).attr("data-purchasedetailsid");

            let _rowSlNo = $(this).attr('data-slno');

            let _comProductID = $(this).attr('data-companyproductid');

            let _prodName = $("#txtProName").val();
            let _articleName = $("#txtArcticle").val();
            let _sizeName = $("#txtSize").val();

            let _uomName = $("#txtUOM").val();

            let _currentStock = $("#txtCurrentStock").val();

            let _pairQty = ConvertBanglaToEnglish( $("#txtPair").val());
        let _dozenQty = ConvertBanglaToEnglish( $("#txtDozen").val());
            let _comm = ConvertBanglaToEnglish( $("#txtComm").val());

            let _buyPrice = ConvertBanglaToEnglish( $("#txtBuyPrice").val());


            let _totcomm = ConvertBanglaToEnglish( $("#txtTotalComm").val());

            let _totbuyPrice = ConvertBanglaToEnglish( $("#txtTotalBuyPrice").val());

            if (_comm == '') {
                _comm = '0';
            }

            if (_currentStock == '') {
                _currentStock = '0';
            }
            if (_comProductID == undefined || _comProductID == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Item</strong>');
                return;
            }
            if (parseInt(_comProductID) < 1) {
                $("#spInfo").html('<strong style="color:red">Invalid Item</strong>');
                return;
            }

            if (_prodName == '' || _prodName == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Item</strong>');
                return;
            }

            if (_articleName == '' || _articleName == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Article</strong>');
                return;
            }


            if (_sizeName == '' || _sizeName == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Size</strong>');
                return;
            }

            if (_uomName == '' || _uomName == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Unit name</strong>');
                return;
            }
            if (_pairQty == '' || _pairQty == null || isNaN(_pairQty)) {
                $("#spInfo").html('<strong style="color:red">Invalid Qty</strong>');
                $("#txtPair").focus();
                return;
            }

            if (parseFloat(_pairQty) <= 0) {
                $("#spInfo").html('<strong style="color:red">Invalid Qty</strong>');
                $("#txtPair").focus();
                return;
            }

            if (_buyPrice == '' || _buyPrice == null || isNaN(_buyPrice)) {
                $("#spInfo").html('<strong style="color:red">Invalid Price</strong>');
                $("#txtPair").focus();
                return;
            }

            if (parseFloat(_buyPrice) <= 0) {
                $("#spInfo").html('<strong style="color:red">Invalid Price</strong>');
                $("#txtBuyPrice").focus();
                return;
            }

            let _singleObj = COMP_PRODUCTS.find(_prod => _prod.companyProductID == _comProductID);
            if (_singleObj == undefined || _singleObj == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Item</strong>');

                return;

            }


            if (parseInt(_purchaseHeadID) > 0) {

                var purchaseDetails = {
                    "PurchaseDetailsID": _purchaseDetailsID,
                    "PurchaseHeadID": _purchaseHeadID,
                    "CompanyProductID": _comProductID,
                    "PurchaseQtyInPair": _pairQty,
                    "CommissionRate": _comm,
                    "PurchaseRate": _buyPrice
                }

                $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
                $.ajax({
                    type: "POST",
                    url: WEB_URL + "Purchase/InsertPurchaseDetails",

                    data: JSON.stringify(purchaseDetails),
                    dataType: "JSON",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        // HideGateOverlay();
                        if (parseInt(data.resultID) > 0) {
                            $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');
                            console.log(data);
                            clearItemAfterAdd();


                            $("#btnSaveCust").removeAttr('disabled');
                            $("#btnAdd").removeAttr('disabled');
                            $("#txtBIllNo").val(data.obj.generatedSalesNo);
                            $("#spInfo").html('');
                            ClearControlAfterSave();


                            $("#btnSaveCust").val('Update');
                            $("#txtArcticle").focus();
                            BindData(data.obj, '');
                        }
                        else {
                            $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                        }
                    },
                    error: function (a, b, c) {
                        //  HideGateOverlay();
                        alert(a, c);
                    }
                });




            }
            else {


                //Edit Which Not Saved
                if (parseInt(_rowSlNo) > 0) {

                    $("#tblProdItem tr:gt(0)").each(function (i, row) {

                        let _currentSL = parseFloat($(this).find('td[controlname="SlNo"]').text());

                        if (_currentSL == _rowSlNo) {

                            $(this).html('');

                            $(this).html('<td style="display:none;" controlname="CommProductID">' + _comProductID + '</td> <td controlname="SlNo">' + _rowSlNo + '</td> <td  controlname="ProdName">' + _singleObj.prodName.name + '</td> <td controlname="ArticleName">' + _singleObj.article.name + '</td> <td controlname="SizeName">' + _singleObj.size.name + '</td> <td controlname="CurrentStock">' + _currentStock + '</td><td  controlname="UOMName">' + _uomName + '</td> <td controlname="DozenQty">' + _dozenQty + '</td><td controlname="PairQty">' + _pairQty + '</td><td controlname="BuyPrice">' + _buyPrice + '</td><td controlname="Comm"> ' + _comm + '</td><td controlname="TotPrice">' + _totbuyPrice + '</td> <td controlname="TotComm">' + _totcomm + '</td><td><button data-purchaseheadid="0" data-purchasedetailsid="0" data-companyproductid="' + _comProductID + '" data-slno="' + _rowSlNo + '" data-for="edit" class="mb-2 mr-2 btn-dashed btn btn-outline-info"> Edit</button></td> <td><button   data-purchaseheadid="0" data-purchasedetailsid="0" data-companyproductid="' + _comProductID + '" data-slno="' + _rowSlNo + '"  data-for="delete" class="mb-2 mr-2  btn-dashed btn btn-outline-danger"> Delete</button></td>');
                        }

                    });
                }
                else {
                    let _slNo = $('#tblProdItem').find('tr').length;

                    $("#tblProdItem").append('<tr> <td style="display:none;" controlname="CommProductID">' + _comProductID + '</td> <td controlname="SlNo">' + _slNo + '</td> <td  controlname="ProdName">' + _singleObj.prodName.name + '</td> <td controlname="ArticleName">' + _singleObj.article.name + '</td> <td controlname="SizeName">' + _singleObj.size.name + '</td> <td controlname="CurrentStock">' + _currentStock + '</td><td  controlname="UOMName">' + _uomName + '</td> <td controlname="DozenQty">' + _dozenQty + '</td><td controlname="PairQty">' + _pairQty + '</td><td controlname="BuyPrice">' + _buyPrice + '</td><td controlname="Comm"> ' + _comm + '</td><td controlname="TotPrice">' + _totbuyPrice + '</td> <td controlname="TotComm">' + _totcomm + '</td><td><button data-purchaseheadid="0" data-purchasedetailsid="0" data-companyproductid="' + _comProductID + '" data-slno="' + _slNo + '" data-for="edit" class="mb-2 mr-2 btn-dashed btn btn-outline-info"> Edit</button></td> <td><button   data-purchaseheadid="0" data-purchasedetailsid="0" data-companyproductid="' + _comProductID + '" data-slno="' + _slNo + '"  data-for="delete" class="mb-2 mr-2  btn-dashed btn btn-outline-danger"> Delete</button></td></tr>');
                    $("#txtArcticle").focus();
                }
                CalCulateTotalAmount();


                clearItemAfterAdd();
            }
        });






        function BindCustCategoryName(_catList) {
            $("#dltxtCategory option").remove();
            $.each(_catList, function (i, prodData) {

                $("#dltxtCategory").append(' <option data-id="' + prodData.supplierCategoryID + '" data-custID="' + prodData.supplierCategoryID + '" value="' + prodData.supplierCategoryName + '" />');


            });

        }


  



        $("#tblProdItem").on('click', 'tr td button', function (e) {

            $("#spInfo").html('');
            //var thisTxtBox = $(this);
            e.preventDefault();
            //ClearControl();

            //alert('working in Progress');
            //var thisTxtBox = $(this);
            e.preventDefault();

            let _for = $(this).attr("data-for");
            let _slNo = $(this).attr("data-slno");

            let _comProductID = $(this).attr("data-companyproductid");
            let _purchaseHeadID = $(this).attr("data-purchaseheadid");
            let _purchaseDetailsID = $(this).attr("data-purchasedetailsid");


            let _pairQty = parseFloat($(this).parent("td").parent("tr").find('td[controlname="PairQty"]').text());
            let _dzQty = parseFloat($(this).parent("td").parent("tr").find('td[controlname="DozenQty"]').text());
            let _comm = parseFloat($(this).parent("td").parent("tr").find('td[controlname="Comm"]').text());
            let _price = parseFloat($(this).parent("td").parent("tr").find('td[controlname="BuyPrice"]').text());


            let _corrStock =parseFloat( $(this).parent("td").parent("tr").find('td[controlname="CurrentStock"]').text());

            let _prodName = $(this).parent("td").parent("tr").find('td[controlname="ProdName"]').text();
            let _articleName = $(this).parent("td").parent("tr").find('td[controlname="ArticleName"]').text();
            let _sizeName = $(this).parent("td").parent("tr").find('td[controlname="SizeName"]').text();
            let _uomName = $(this).parent("td").parent("tr").find('td[controlname="UOMName"]').text();



            if (_for == 'edit') {

                $("#btnAdd").val('Update');



                $("#btnAdd").attr('data-purchaseheadid', _purchaseHeadID);
                $("#btnAdd").attr('data-purchasedetailsid', _purchaseDetailsID);
                $("#btnAdd").attr('data-companyproductid', _comProductID);
                $("#btnAdd").attr('data-slno', _slNo);

                $("#txtArcticle").val(_articleName);

                $("#txtSize").val(_sizeName);


                $("#txtUOM").val(_uomName);
                $("#txtCurrentStock").val(_corrStock);
                $("#txtPair").val(_pairQty);

                $("#txtDozen").val(_dzQty);
                $("#txtComm").val(_comm);
                $("#txtBuyPrice").val(_price);
                $("#txtProName").val(_prodName);

                ProcessItemInProduct();



                CalculateSingleItemAmount();
            }

            if (_for == 'delete') {


                if (confirm('Are you sure to delete This Item?')) {
                    // Save it!
                    if (parseInt(_purchaseDetailsID) > 0) {
                        let purchaseDetails = {
                            "PurchaseDetailsID": _purchaseDetailsID,
                            "PurchaseHeadID": _purchaseHeadID,
                            "CompanyProductID": _comProductID,

                        };


                        $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
                        $.ajax({
                            type: "POST",
                            url: WEB_URL + "Purchase/DeletePurchaseDetails",

                            data: JSON.stringify(purchaseDetails),
                            dataType: "JSON",
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (parseInt(data.resultID) > 0) {
                                    $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');
                                    console.log(data);
                                    clearItemAfterAdd();


                                    $("#btnSaveCust").removeAttr('disabled');
                                    $("#btnAdd").removeAttr('disabled');
                                    $("#txtBIllNo").val(data.obj.generatedSalesNo);
                                    $("#spInfo").html('');
                                    ClearControlAfterSave();

                                    $("#btnSaveCust").val('Update');
                                    BindData(data.obj, '');

                                }
                                else {
                                    $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                                }


                            },
                            error: function (a, b, c) {
                                //  HideGateOverlay();
                                alert(a, c);
                            }
                        });

                    }
                    else {

                        $(this).parent("td").parent("tr").remove();

                        CalCulateTotalAmount();

                    }

                }
                else {
                    return;
                }


            }


        });


        function ProcessItemInProduct() {
            COMP_PRODUCTS = [];
            var _val = $("#txtProName").val();

            var _prodNameID = GetDataID(_val, "#dltxtProName");

            if (_prodNameID != undefined) {

                var _prodName = {
                    "ProdNameID": _prodNameID
                }

                LoadItemsInProName(_prodName);
            }
        }



        $("#btnSaveCust").click(function () {
            $("#spInfo").html('');
         

            let _purchaseheadid = $(this).attr('data-purchaseheadid');
           

            let _purchaseDate = $("#txtDate").val();
           
        
            //---===========
            let _note1 = $("#txtNote1").val();
           

       
            let _suppName = $("#txtSupplierName").val();
            let _suppID2 = GetDataID(_suppName, "#dltxtSupplierName");
            let _suppChallanNo = $("#txtSuppBilNo").val();

            if (_suppID2 == undefined || _suppID2 == null || _suppID2 == '') {

                    $("#spInfo").html('<strong style="color:red">Invalid Supplier !</strong>');

                    $("#txtSupplierName").focus();
                    return;
                }

            

            var _allLines = [];
            var _single_line = {};

            $("#tblProdItem tr:gt(0)").each(function (i, row) {
                let _pair2 = parseFloat($(this).find('td[controlname="PairQty"]').text());
        
                let _price2 = parseFloat($(this).find('td[controlname="BuyPrice"]').text());
                let _comm2 = parseFloat($(this).find('td[controlname="Comm"]').text());

                let _comProductID = parseInt($(this).find('td[controlname="CommProductID"]').text());

                _single_line = {
                    "CompanyProductID": _comProductID,
                    "PurchaseQtyInPair": _pair2,
                    "CommissionRate": _comm2,
                    "PurchaseRate": _price2
                }

                _allLines.push(_single_line);
            });

            var salesHead = {
                "PurchaseHeadID": _purchaseheadid,
                "PurchaseDetailsList": _allLines,
               
                "Supplier": { "SupplierId": _suppID2 },
                "PurchaseDate": _purchaseDate,
                "suppChallanNo": _suppChallanNo,
              
                "Note1": _note1,
               
            }

            console.log(salesHead);


            if (_allLines.length == 0) {
                $("#spInfo").html('<strong style="color:red">No Data Found !</strong>');
                return;
            }

            $("#spInfo").html('<strong style="color:blue">Savng Data Please wait.. !</strong>');

            let _methodName = "";
            if (parseInt(_purchaseheadid) > 0) {
                _methodName = "UpdatePurchase";

            }
            else {
                _methodName = "SavePurchase";
            }

            $.ajax({
                type: "POST",
                url: WEB_URL + "Purchase/" + _methodName,

                data: JSON.stringify(salesHead),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    console.log(data);
                    if (parseInt(data.resultID) > 0) {
                        $("#txtBIllNo").val(data.resultNo);
                        $("#spInfo").html('<strong style="color:blue">' + data.resultMessage + '</strong>');
                        ClearControlAfterSave();

                        $("#btnSaveCust").attr('disabled', 'disabled');
                        $("#btnAdd").attr('disabled', 'disabled');
                        BindData(data.obj, 'disabled');
                    }
                    else {
                        // HideGateOverlay();
                        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }


                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });




        });



        $("#btnEdit").click(function () {

            $("#spInfo").html('');
            let _purchaseNo =ConvertBanglaToEnglish( $("#txtBIllNo").val());
            if (_purchaseNo == '' || _purchaseNo == null) {
                $("#spInfo").html('<strong style="color:red">Invalid Purcase No</strong>');
                $("#txtBIllNo").focus();
                return;
            }
            var purchaseHead = {
                "GeneratedPurchaseHeadNo": _purchaseNo
            }

            $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
            $.ajax({
                type: "POST",
                url: WEB_URL + "Purchase/GetPurchaseBySalesNo",

                data: JSON.stringify(purchaseHead),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    console.log(data);
                    if (data.obj != null) {

                        $("#btnSaveCust").removeAttr('disabled');
                        $("#btnAdd").removeAttr('disabled');
                        $("#txtBIllNo").val(data.obj.generatedSalesNo);
                        $("#spInfo").html('');
                        ClearControlAfterSave();

                        $("#btnSaveCust").val('Update');
                        BindData(data.obj, '');
                        $("#spInfo").html('');

                    }
                    else {
                        $("#spInfo").html('<strong style="color:red">' + data.resultMessage+'</strong>');
                    }


                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });



        });


        $("#btnView").click(function () {

            $("#spHeaderBill").text('');
            $("#spAddressBill").text('');
            $("#spOrderNoBill").text('');
            $("#spPartyBill").text('');
            $("#spAprtyAddressBill").text('');
            $("#spDateBill").text('');
            $("#spContactPerson").text('');
            $("#spSuppBillNo").text('');

            $("#tblOrderBill tr:gt(9)").remove();



            $("#spInfo").html('');
            let _purchaseNo = ConvertBanglaToEnglish($("#txtBIllNo").val());
            if (_purchaseNo == '' || _purchaseNo == null) {
                $("#spInfo").html('<strong style="color:red">Invalid  No</strong>');
                $("#txtBIllNo").focus();
                return;
            }
            BillNo_PDF = "Purchase_Bill_No_" + _purchaseNo;
            var purchaseHead = {
                "GeneratedPurchaseHeadNo": _purchaseNo
            }


            $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
            $.ajax({
                type: "POST",
                url: WEB_URL + "Purchase/GetPurchaseBySalesNo",

                data: JSON.stringify(purchaseHead),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    console.log(data);
                    if (parseInt(data.resultID) > 0) {
                        console.log(data);

                        $("#spInfo").html('');

                        //$("#btnViewModel").click();

                        BindDataReport(data.obj);

                    }
                    else {
                        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }



                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });

        });


     





       // $("#txtNote1").val(_obj.note1);


      


        function BindDataReport(_obj) {
            //
            $("#spHeaderBill").text(_obj.company.name);
            $("#spAddressBill").text(_obj.company.shopAddress);
            $("#spAddressMobile").text(_obj.company.mobileNo1 + ' , ' + _obj.company.mobileNo2);
            $("#spOrderNoBill").text(_obj.generatedPurchaseHeadNo2);
            $("#spDateBill").text(_obj.purchaseDateFormated);
          
            $("#spPartyBill").text(_obj.supplier.name);

            $("#spContactPerson").text(_obj.supplier.contactPerson);
            $("#spSuppBillNo").text(_obj.suppChallanNo);
            
            $("#spPartyshopAddressBill").text(_obj.supplier.address1);

            var _totQtyPair = 0;
            var _totNeatAmount = 0;
            var _totAmount = 0;
            var _totCommAmount = 0;
            $(_obj.purchaseDetailsList).each(function (i, _singleObj) {

                $("#tblOrderBill").append('<tr>  <td class="TDAllBorder" controlname="SlNo">' + (i + 1) + '</td> <td  class="TDRightBorder" controlname="ProdName">' + _singleObj.prodName.name + '</td> <td class="TDRightBorder" controlname="ArticleName">' + _singleObj.article.name + '</td> <td class="TDRightBorder" controlname="SizeName">' + _singleObj.size.name + '</td> <td class="TDRightBorder" controlname="UOMName">' + _singleObj.uom.name + '</td><td class="TDRightBorder" controlname="DozenQty">' + _singleObj.purchaseQtyInDozen.toFixed(2) + '</td> <td class="TDRightBorder" controlname="PairQty">' + _singleObj.purchaseQtyInPair + '</td><td class="TDRightBorder" controlname="BuyPrice">' + _singleObj.purchaseRate + '</td><td class="TDRightBorder" controlname="Comm"> ' + _singleObj.commissionRate + '</td><td class="TDRightBorder" controlname="TotPrice">' + _singleObj.purchaseQtyInPairAmount + '</td><td class="TDRightBorder" controlname="CommAmount">' + _singleObj.commissionAmount + '</td><td class="TDRightBorder" controlname="NeatAmount">' +( _singleObj.purchaseQtyInPairAmount- _singleObj.commissionAmount) + '</td></tr>');
                _totQtyPair += _singleObj.purchaseQtyInPair;
               
                _totNeatAmount += parseFloat(_singleObj.purchaseQtyInPairAmount) - parseFloat(_singleObj.commissionAmount);
                _totAmount +=_singleObj.purchaseQtyInPairAmount;
                _totCommAmount += _singleObj.commissionAmount;

            });
            $("#tblOrderBill").append('<tr> <td class="TDAllBorder" colspan="6" style="text-align:right;">Total:</td><td class="TDRightBorder" style="text-align:right;">' + _totQtyPair.toFixed(2) + '</td><td colspan="2" class="TDRightBorder" style="text-align:right;"></td>  <td   class="TDRightBorder" style="text-align:right;">' + _totAmount.toFixed(2) + '</td><td   class="TDRightBorder" style="text-align:right;">' + _totCommAmount.toFixed(2) + '</td><td   class="TDRightBorder" style="text-align:right;">' + _totNeatAmount.toFixed(2) + '</td></tr>');

           
            $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black"   colspan="12"> &nbsp;</td> </tr>');
            $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black"   colspan="12"> &nbsp;</td> </tr>');
            $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black"   colspan="12"> &nbsp;</td> </tr>');
 

           $("#tblOrderBill").append('<tr> <td style="border-left:1px solid black; text-align:center"   colspan="5"> Received By</td> <td   colspan="2"> &nbsp;</td><td style="border-right:1px solid black;text-align:center"   colspan="5">Prepared By</td></tr>');

            $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black"   colspan="12"> &nbsp;</td> </tr>');

            $("#tblOrderBill").append('<tr> <td style="border-right:1px solid black;border-left:1px solid black;border-bottom:1px solid black"   colspan="12"> &nbsp;</td> </tr>');



        }

        $("#btnAddNewCust").click(function () {

            $("#btnSaveCust").removeAttr('disabled');

            clearItemAfterAdd();
            ClearControlAfterSave();


            CalculateSingleItemAmount();
            CalCulateTotalAmount();
            CalCulateTotalAmount();

        });



        $("#txtCategory").bind('input', function () {
            // ClearAfterBuyerChange();
            $("#dltxtSubCategory option").remove();
            $("#txtSubCategory").val('');
            // var _thisVal=
            var _val = $(this).val();

            var _catID = GetDataID(_val, "#dltxtCategory");
            //var _subCatList = [];
            //if (_catID != undefined) {
            //    _subCatList = SUB_CUST_CAT.filter(function (_obj) {
            //        return _obj.supplierCategoryID == _catID;
            //    });

            //    BindSubCustName(_subCatList);

            //}

            var _CustList = [];
            if (_catID != undefined) {
                _CustList = CUST_LIST.filter(function (_obj) {
                    return _obj.supplierSubCategory.supplierCategoryID == _catID;
                });

                BindCustName(_CustList);
            }


        });


        $("#txtSubCategory").bind('input', function () {

            $("#dltxtSupplierName option").remove();
            $("#txtSupplierName").val('');

            var _val = $(this).val();

            var _subCatID = GetDataID(_val, "#dltxtSubCategory");

            if (_subCatID != undefined) {
                var _CustList = CUST_LIST.filter(function (_obj) {
                    return _obj.supplierSubCategoryID == _subCatID;
                });

                BindCustName(_CustList);
            }

        });

        $("#txtProName").bind('input', function () {

            $("#dltxtArcticle option").remove();
            $("#txtArcticle").val('');
            $("#btnAdd").attr('data-companyproductid','0');
            COMP_PRODUCTS = [];
            var _val = $(this).val();

            var _prodNameID = GetDataID(_val, "#dltxtProName");

            if (_prodNameID != undefined) {

                var _prodName = {
                    "ProdNameID": _prodNameID
                }


                LoadItemsInProName(_prodName);
            }

        });



        $("#txtArcticle").bind('input', function () {
            $("#txtExtraArticle").val('');

            $("#txtSize").val('');


            $("#txtUOM").val('');
            $("#txtCurrentStock").val('');
            $("#txtSellPrice").val('');
           // $("#txtComm").val('');
           // $("#txtBuyPrice").val('');

            $("#btnAdd").attr('data-companyproductid', '0');

            var _thisVal = $(this).val();

            var _comProductID = GetDataID(_thisVal, "#dltxtArcticle");

            if (_comProductID != undefined) {
                BindSizeAndUOM(_comProductID);
            }


            if (_thisVal.includes('|')) {
                //alert(_thisVal);
                var _artName = _thisVal.split('|')[0];
                $("#txtArcticle").val($.trim( _artName));
            }
        });

        function BindSizeAndUOM(_comProductID) {
            console.log(_comProductID);
            var _singleObj = COMP_PRODUCTS.find(_prod => _prod.companyProductID == _comProductID);
            if (!(_singleObj == undefined || _singleObj == null)) {
                $("#txtSize").val(_singleObj.size.name);
                $("#txtUOM").val(_singleObj.uom.name);
                $("#txtCurrentStock").val(_singleObj.currentStock);
                $("#txtSellPrice").val(_singleObj.sellPrice);

                $("#btnAdd").attr('data-companyproductid', _comProductID);

            }
        }

        function LoadItemsInProName(_obj) {
            $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
            $.ajax({
                type: "POST",
                url: WEB_URL + "CompanyProduct/GetCompanyProductByProdName",

                data: JSON.stringify(_obj),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    // HideGateOverlay();
                    $("#spInfo").html('');
                    console.log(data);
                    COMP_PRODUCTS = data;
                    BIndArticleAndSize(data);

                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });



        }
        function BindData(_obj, _disabledStr) {

            console.log(_obj);
            $("#txtBIllNo").val(_obj.generatedPurchaseHeadNo);

            datePicker1.setDate(_obj.purchaseDateFormated);
           // $("#txtDate").val(_obj.purchaseDateFormated);


     
        
         
            $("#txtNote1").val(_obj.note1);


            $("#btnSaveCust").val('Update');
            $("#btnSaveCust").attr('data-purchaseheadid', _obj.purchaseHeadID);

       
            $("#txtSuppBilNo").val(_obj.suppChallanNo);

          
            $("#txtSupplierName").val(_obj.supplier.name);

            $("#dltxtSupplierName").append(' <option data-id="' + _obj.supplier.supplierId + '" data-custID="' + _obj.supplier.supplierId + '" value="' + _obj.supplier.name + '" />');

        
            BindTableRows(_obj.purchaseDetailsList, _disabledStr)

            CalculateGrandTotal();
        }

        function BindTableRows(_objlist, disabledStr) {

            $(_objlist).each(function (i, _singleObj) {

                $("#tblProdItem").append('<tr> <td style="display:none;" controlname="CommProductID">' + _singleObj.companyProduct.companyProductID + '</td> <td controlname="SlNo">' + (i + 1) + '</td> <td  controlname="ProdName">' + _singleObj.prodName.name + '</td> <td controlname="ArticleName">' + _singleObj.article.name + '</td><td controlname="SizeName">' + _singleObj.size.name + '</td> <td controlname="CurrentStock">' + _singleObj.currentStockQty + '</td><td  controlname="UOMName">' + _singleObj.uom.name + '</td> <td controlname="DozenQty">' + _singleObj.purchaseQtyInDozen.toFixed(2) + '</td><td controlname="PairQty">' + _singleObj.purchaseQtyInPair + '</td><td controlname="BuyPrice">' + _singleObj.purchaseRate + '</td><td controlname="Comm"> ' + _singleObj.commissionRate + '</td><td controlname="TotPrice">' + _singleObj.purchaseQtyInPairAmount + '</td> <td controlname="TotComm">' + _singleObj.commissionAmount + '</td><td><button data-purchaseheadid="' + _singleObj.purchaseHeadID + '" data-purchasedetailsid="' + _singleObj.purchaseDetailsID + '" data-companyproductid="' + _singleObj.companyProduct.companyProductID + '" data-slno="' + (i + 1) + '" data-for="edit" class="mb-2 mr-2 btn-dashed btn btn-outline-info"  ' + disabledStr + '> Edit</button></td> <td><button   data-purchaseheadid="' + _singleObj.purchaseHeadID + '" data-purchasedetailsid="' + _singleObj.purchaseDetailsID + '" data-companyproductid="' + _singleObj.companyProduct.companyProductID + '" data-slno="' + (i + 1) + '"  data-for="delete" class="mb-2 mr-2  btn-dashed btn btn-outline-danger" ' + disabledStr + '> Delete</button></td></tr>');
            });

            CalCulateTotalAmount();

        }

        function BIndArticleAndSize(_objList) {
            $("#dltxtArcticle option").remove();
            $.each(_objList, function (i, prodData) {
             
                var _value = prodData.article.name + ' | ' + prodData.size.name + ' | ' + prodData.currentStock + ' | ' + prodData.uom.name + ' | ' + prodData.sellPrice

                $("#dltxtArcticle").append(' <option  data-article="' + prodData.article.name + '" data-id="' + prodData.companyProductID + '" data-companyProductID="' + prodData.companyProductID + '" value="' + _value + '" />');


            });
        }



        function BindCustName(_custList) {
            $("#dltxtSupplierName option").remove();

            $.each(_custList, function (i, prodData) {

                $("#dltxtSupplierName").append(' <option data-id="' + prodData.supplierId + '" data-supplierId="' + prodData.supplierId + '" value="' + prodData.name + '" />');


            });

        }


        function BindSubCustName(_subcatList) {
            console.log(_subcatList);
            $("#dltxtSubCategory option").remove();

            $.each(_subcatList, function (i, prodData) {

                $("#dltxtSubCategory").append(' <option data-id="' + prodData.supplierSubCategoryID + '" data-custID="' + prodData.supplierSubCategoryID + '" value="' + prodData.supplierSubCategoryName + '" />');


            });

        }

        function BindProdName(_subcatList) {

            $("#dltxtProName option").remove();

            $.each(_subcatList, function (i, prodData) {

                $("#dltxtProName").append(' <option data-id="' + prodData.prodNameID + '" data-prodNameID="' + prodData.prodNameID + '" value="' + prodData.name + '" />');


            });

        }
  
        $("#txtPair").bind('input', function () {

            CalculateSingleItemAmount();
        });


        $("#txtComm").bind('input', function () {

            CalculateSingleItemAmount();
        });


        $("#txtSupplierName").bind('input', function () {
            $("#txtPrevDue").val('');
            let _suppName = $("#txtSupplierName").val();
            let _suppID2 = GetDataID(_suppName, "#dltxtSupplierName");


            if (!(_suppID2 == undefined || _suppID2 == null || _suppID2 == '')) {

                var model = { "SupplierId": _suppID2 };


                LoadSupplierBalance(model);
            }




        });


        function LoadSupplierBalance(_obj) {
            //txtPrevDue


            $("#spInfo").html('<strong style="color:blue">Loading Supplier Balance....</strong>');
            $.ajax({
                type: "POST",
                url: WEB_URL + "Supplier/GetSupplier",

                data: JSON.stringify(_obj),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    // HideGateOverlay();
                    $("#spInfo").html('');
                    console.log(data);
                    $("#txtPrevDue").val(data.currentBalance);

                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });



        }



        $("#txtBuyPrice").bind('input', function () {

            CalculateSingleItemAmount();
        });

        function CalculateSingleItemAmount() {

            var _parQty = ConvertBanglaToEnglish($("#txtPair").val());
            //var _dznQty = $("#txtDozen").val();

            var _commAmount = ConvertBanglaToEnglish($("#txtComm").val());

            
            var _buyPrice = ConvertBanglaToEnglish($("#txtBuyPrice").val());

            if (_parQty == '') { _parQty = '0'; }
            if (_commAmount == '') { _commAmount = '0'; }
            if (_buyPrice == '') { _buyPrice = '0'; }

            var _dzQty = parseFloat(_parQty) / 12;

            var _totComm = parseFloat(_commAmount) * parseFloat(_parQty);

            var _toBuyPrice = parseFloat(_buyPrice) * parseFloat(_parQty);

            $("#txtDozen").val(ConvertEnglishToBengali(_dzQty.toFixed(2)));
            $("#txtTotalComm").val(ConvertEnglishToBengali(_totComm.toFixed(2)));
            $("#txtTotalBuyPrice").val(ConvertEnglishToBengali(_toBuyPrice.toFixed(2)));

        }

        function CalCulateTotalAmount() {

            let _totDozen2 = 0
            let _totQty2 = 0
            let _totAmount2 = 0;
            let _totComm2 = 0;



            $("#tblProdItem tr:gt(0)").each(function (i, row) {
                _totQty2 += parseFloat( $(this).find('td[controlname="PairQty"]').text());
                _totDozen2 += parseFloat($(this).find('td[controlname="DozenQty"]').text());
                _totAmount2 += parseFloat($(this).find('td[controlname="TotPrice"]').text());
                _totComm2 += parseFloat($(this).find('td[controlname="TotComm"]').text());

            });
            $("#txtTotalDozen").val(_totDozen2.toFixed(2));
            $("#txtTotalpair").val(_totQty2.toFixed(2));
            $("#txtNeatAmount").val((_totAmount2 - _totComm2).toFixed(2));
            $("#txtTotalComm2").val(_totComm2.toFixed(2));

            $("#txtTotalAmount").val(_totAmount2.toFixed(2));

        }

        function CalculateGrandTotal() {

            let _totAmount = 0;

            let _totComm = 0;

            let _transportFee = 0;
            let _grandTotal = 0;
            if ($("#txtTotalComm2").val() == '') {
                $("#txtTotalComm2").val('0')
            }
         

            _totAmount = parseFloat($("#txtTotalAmount").val());
            _totComm = parseFloat($("#txtTotalComm2").val());
            _transportFee = parseFloat($("#txtPackCost").val());


             _grandTotal = _totAmount - _totComm + _transportFee;
            $("#txtGrandTotal").val(_grandTotal.toFixed(2))

        }



        function clearItemAfterAdd() {
            $("#txtArcticle").val('');
            $("#txtExtraArticle").val('');
            $("#txtCurrentStock").val('');
            $("#txtSellPrice").val('');
            $("#txtPair").val('');
            $("#txtSize").val('');

            $("#txtComm").val('0');

            $("#txtBuyPrice").val('0');

            $("#btnAdd").val('Add');

            $("#btnAdd").attr('data-purchaseheadid',"0");
            $("#btnAdd").attr('data-purchasedetailsid', "0");
            $("#btnAdd").attr('data-companyproductid', "0");
            $("#btnAdd").attr('data-slno', "0");

            CalculateSingleItemAmount();
        }

        function ClearControlAfterSave() {
            clearItemAfterAdd();

            $("#btnSaveCust").val('Save');
            $("#btnSaveCust").attr('data-purchaseheadid', "0");
            $("#btnSaveCust").attr('data-purchasedetailsid', "0");
            $("#tblProdItem tr:gt(0)").remove();
            $("#txtSupplierName").val('');
            $("#txtSuppBilNo").val('');
            $("#txtTotalDozen").val('0');
            $("#txtTotalpair").val('0');

            $("#txtNeatAmount").val('0');
            $("#txtTodayJoma").val('0');
            $("#txtTodayDue").val('0');
            $("#txtTotalDue").val('0');

            $("#txtTotalAmount").val('0');
            $("#txtTotalComm2").val('0');


            $("#txtPackCost").val('0');
            $("#txtGrandTotal").val('0');
            $("#txtPaymentMedium").val('');
            $("#txtSupplierName").val('');
           
            $("#btnAdd").removeAttr("disabled");

            CalculateGrandTotal();

        }



        function GetDataValue(value, dataList) {
            var z = $(dataList);
            var val = $(z).find('option[data-id="' + value + '"]');
            var endval = val.attr('value');
            return endval;
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
        win.document.write('<title>' + BillNo_PDF+'</title>');   // <title> FOR PDF HEADER.
        win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
        win.document.write('</head>');
        win.document.write('<body>');
        win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
        win.document.write('</body></html>');

        win.document.close(); 	// CLOSE THE CURRENT WINDOW.

        win.print();    // PRINT THE CONTENTS.
    }
