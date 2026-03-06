

    var SUPP_LIST = [];
    var SUPP_CAT = [];
    var SUB_SUPP_CAT = [];
    var tableCust;
    $(document).ready(function () {

        // alert('ghgh');

        LoadDefaultData();


    tableCust = $('#dataTable').DataTable({
        data: [],
    "columnDefs": [

    {
        "searchable": true,
    "orderable": false,
    "targets": 0
                }
    ],
    "language":
    {
        "processing": '<div class="font-icon-wrapper float-left mr-3 mb-3" style="margin-left: 9%;">' +
            '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
                '<div class="loader">' +
                    '<div class="line-scale-pulse-out-rapid">' +
                        '<div></div>' +
                    '<div></div>' +
                    '<div></div>' +
                    '<div></div>' +
                    '<div></div>' +
                    '</div>' +
                '</div>' +
            '</div>' +
        '<p>Loading</p></div>'
            },
    //"responsive": true,
    //"processing": true,
    //"serverSide": true,
    "lengthMenu": [
    [10, 15, 25, 50, 100, -1],
    [10, 15, 25, 50, 100, "All"]
    ],

    "order": [1, "asc"],
    "start": 0,
    "length": 10,
    "bFilter": true,

    "columns": [
    {"data": "supplierId" },

    {"data": "supplierSubCategory.supplierSubCategoryName" },
    {"data": "name" },
    {"data": "contactPerson" },
    {"data": "address1" },
    {"data": "openingBalance" },

    {"data": "mobileNo1" },


    {
        "data": null,
    "mRender": function (data, type, full) {
                        return '<button class="mb-2 mr-2 btn-icon btn-shadow btn-dashed btn btn-outline-info" onclick="editCustomer(\'' + data.supplierId + '\')">Edit</button>';
                    }
                },
    {
        "data": null,
    "mRender": function (data, type, full) {
                        return '<button class="mb-2 mr-2 btn-icon btn-shadow btn-dashed btn btn-outline-danger" onclick="deleteCustomer(\'' + data.supplierId + '\')">Delete</button>';
                    }
                }
    ]
        });



    function LoadDefaultData() {
        ClearControl();
    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/SupplierEntryViewModel",

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
    SUPP_CAT = data.supplierCategorys;
    SUPP_LIST = data.suppliers;
    SUB_SUPP_CAT = data.supplierSubCategorys;
    LoadCustCategoryName(data.supplierCategorys);
    BinCategoryTable(data.supplierCategorys);

    BinSubCategoryTable(data.supplierSubCategorys);
    tableCust.rows.add(data.suppliers).draw();

                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        }


    $("input").on('keypress', function (event) {

            // alert('ok');
            var _currentTab = $(this).attr("data-tab");
    var _id = $(this).attr("id");

    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
                if (_currentTab != undefined) {
                    if (_id !== 'btnSaveCust') {
                        var _nextTab = parseInt(_currentTab) + 1;
    $('input[data-tab="' + _nextTab + '"]').focus();
                    }
                }
            }


        });


    $("#btnEditCat").click(function () {
        $("#spInfo2").html('');
    ClearControl();
        });
    $("#txtCategory").bind('input', function () {
        // ClearAfterBuyerChange();
        $("#dltxtSubCategory option").remove();
    $("#txtSubCategory").val('');
    // var _thisVal=
    var _val = $(this).val();

    var _catID = GetDataID(_val, "#dltxtCategory");

    if (_catID != undefined) {
                var _SubCatList = SUB_SUPP_CAT.filter(function (_obj) {
                    return _obj.supplierCategoryID == _catID;
                });

    LoadSubCustName(_SubCatList);
            }

        });


    $("#btnSaveCust").on('click', function () {

        $("input").each(function () {
            $(this).removeClass("input-validation-error");
        });
    $("#spInfo").html('');


    var _custId = $("#btnSaveCust").attr('data-suppid');


    var _catName = $("#txtCategory").val();

    var _subcatName = $("#txtSubCategory").val();



    var _custName = $("#txtCustName").val();

    var _contactPerson = $("#txtContactPerson").val();

    var _address = $("#txtAddress").val();

    var _mobile1 = $("#txtMobile1").val();

    var _mobile2 = $("#txtMobile2").val();

    var _OpBal = $("#txtOpBalance").val();




    var _catID = GetDataID(_catName, "#dltxtCategory");

    var _subCatID = GetDataID(_subcatName, "#dltxtSubCategory");




    if (_catName == '') {
        $("#txtCategory").addClass("input-validation-error");
    $("#spInfo").html('<strong style="color:red">Invalid Category !</strong>');
    return;
            }
    if (_contactPerson == '') {
        $("#txtContactPerson").addClass("input-validation-error");
    $("#spInfo").html('<strong style="color:red">Invalid Contact Person !</strong>');
    return;
            }
    if (_catID == '' || _catID == null || _catID == undefined) {
        $("#txtCategory").addClass("input-validation-error");
    $("#spInfo").html('<strong style="color:red">Invalid Category !</strong>');
    return;
            }
    if (_subcatName == '') {
        $("#txtSubCategory").addClass("input-validation-error");
    $("#spInfo").html('<strong style="color:red">Invalid Sub Category !</strong>');
    return;
            }


    if (_subCatID == '' || _subCatID == null || _subCatID == undefined) {
        $("#txtSubCategory").addClass("input-validation-error");
    $("#spInfo").html('<strong style="color:red">Invalid Sub Category !</strong>');
    return;
            }



    if (_custName == '') {
        $("#txtCustName").addClass("input-validation-error");
    $("#spInfo").html('<strong style="color:red">Invalid Customer Name !</strong>');
    return;
            }
    if (_address == '') {
        $("#txtAddress").addClass("input-validation-error");
    $("#spInfo").html('<strong style="color:red">Invalid Address !</strong>');
    return;
            }

    if (_mobile1 == '') {
        $("#txtMobile1").addClass("input-validation-error");
    $("#spInfo").html('<strong style="color:red">Invalid Mobilr !</strong>');
    return;
            }


    if (_OpBal == '') {
        _OpBal = '0';
            }


    $("#spInfo").html('<strong>Please wait ....</strong>');
    var supplier = {
        "SupplierId": _custId,

    "Name": _custName,

    "MobileNo1": _mobile1,
    "MobileNo2": _mobile2,
    "Address1": _address,
    "ContactPerson": _contactPerson,


    "OpeningBalance": _OpBal,
    //"OpeningCommission": _opComm,
    //"OpeningQty": _opPcs,
    "CurrentBalance": _OpBal,

    "SupplierSubCategoryID": _subCatID,

            }
    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/AddSuppllier",

    data: JSON.stringify(supplier),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();

        console.log(data);

    if (parseInt(data.resultID) == -1) {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');

                    }
    else {
        ClearControl();
    $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');
    CUST_LIST = data.obj;
    // tableCust.fnClearTable();
    tableCust.clear().draw();
    tableCust.rows.add(data.obj).draw();

                    }




                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });



        });



    $("#btnSaveCat").on('click', function () {

        $("input").each(function () {
            $(this).removeClass("input-validation-error");
        });

    var _id = $("#btnSaveCat").attr('data-catId');

    $("#spInfo2").html('');

    var _catName = $("#txtCatNameEdit").val();

    if (_catName == '') {
        $("#txtCatNameEdit").addClass("input-validation-error");
    $("#spInfo2").html('<strong style="color:red">Invalid Product !</strong>');
    return;
            }
    $("#spInfo2").html('<strong>Please wait ....</strong>');
    var supplierCategory = {
        "SupplierCategoryID": _id,
    "SupplierCategoryName": _catName
            }
    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/InsertNewSupplierCategory",

    data: JSON.stringify(supplierCategory),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();

        console.log(data);


    if (parseInt(data.resultID) == -1) {
        $("#spInfo2").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }
    else {
        ClearControl();
    $("#spInfo2").html('<strong style="color:green">' + data.resultMessage + '</strong>');
    SUPP_CAT = data.obj;
    LoadCustCategoryName(data.obj)
    BinCategoryTable(data.obj);
                    }



                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        });

    $("#btnAddNewCust").on('click', function () {
        ClearControl();
        });

    $("#btnSaveSubCat").on('click', function () {

        $("input").each(function () {
            $(this).removeClass("input-validation-error");
        });
    $("#spInfo3").html('');

    var _catName = $("#txtCatNameEditSub").val();

    var _subCatName = $("#txtSubCatNameEditSub").val();

    var _id = $(this).attr("data-subcatId");

    if (_catName == '') {
        $("#txtCatNameEditSub").addClass("input-validation-error");
    $("#spInfo3").html('<strong style="color:red">Invalid Category !</strong>');
    return;
            }


    if (_subCatName == '') {
        $("#txtSubCatNameEditSub").addClass("input-validation-error");
    $("#spInfo3").html('<strong style="color:red">Invalid Sub Category !</strong>');
    return;
            }


    var _catID = GetDataID(_catName, "#dltxtCategory");

    if (_catID == '' || _catID == null || _catID == undefined) {
        $("#txtCatNameEditSub").addClass("input-validation-error");
    $("#spInfo3").html('<strong style="color:red">Invalid Category !</strong>');
    return;
            }



    $("#spInfo3").html('<strong>Please wait ....</strong>');
    var supplierSubCategory = {
        "SupplierCategoryID": _catID,
    "SupplierSubCategoryID": _id,
    "SupplierSubCategoryName": _subCatName
            }
    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/InsertNewSubSupplierCategory",

    data: JSON.stringify(supplierSubCategory),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();

        console.log(data);


    if (parseInt(data.resultID) == -1) {
        $("#spInfo3").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }
    else {
        $("#txtSubCatNameEditSub").val('');
    $("#spInfo3").html('<strong style="color:green">' + data.resultMessage + '</strong>');
    ClearControl();

    BinSubCategoryTable(data.obj)
    SUB_SUPP_CAT = data.obj;
                    }




                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        });



    /////////////----------------




    function LoadCustCategoryName(_catList) {
        $("#dltxtCategory option").remove();
    $.each(_catList, function (i, prodData) {

        $("#dltxtCategory").append(' <option data-id="' + prodData.supplierCategoryID + '" data-custID="' + prodData.supplierCategoryID + '" value="' + prodData.supplierCategoryName + '" />');


            });

        }


    function BinCategoryTable(_catList) {
        $("#tblCat tr:gt(0)").remove();

    $.each(_catList, function (i, prodData) {

        $("#tblCat").append('<tr> <td>' + prodData.supplierCategoryID + '</td> <td>' + prodData.supplierCategoryName + '</td> <td>' + '  <button data-for="edit" data-catId=' + prodData.supplierCategoryID + ' class= "mb-2 mr-2 btn-icon btn-shadow btn-dashed btn btn-outline-info"  > <i class="pe-7s-pen btn-icon-wrapper"> </i>Edit</button > <button data-for="delete" data-catId=' + prodData.supplierCategoryID + ' class="mb-2 mr-2 btn-icon btn-shadow btn-dashed btn btn-outline-danger" ><i class="pe-7s-trash btn-icon-wrapper"> </i>Delete</button>' + '</td> </tr> ');


            });

        }

    function BinSubCategoryTable(_subcatList) {
        $("#tblSubCat tr:gt(0)").remove();
    $.each(_subcatList, function (i, prodData) {

        $("#tblSubCat").append('<tr> <td>' + prodData.supplierSubCategoryID + '</td> <td>' + prodData.supplierCategory.supplierCategoryName + '</td><td>' + prodData.supplierSubCategoryName + '</td> <td>' + '  <button data-for="edit" data-catId=' + prodData.supplierCategoryID + '  data-subcatId=' + prodData.supplierSubCategoryID + ' class= "mb-2 mr-2 btn-icon btn-shadow btn-dashed btn btn-outline-info"  > <i class="pe-7s-pen btn-icon-wrapper"> </i>Edit</button > <button data-for="delete" data-catId=' + prodData.supplierCategoryID + ' data-subcatId=' + prodData.supplierSubCategoryID + ' class="mb-2 mr-2 btn-icon btn-shadow btn-dashed btn btn-outline-danger" ><i class="pe-7s-trash btn-icon-wrapper"> </i>Delete</button>' + '</td> </tr> ');


            });

        }




    $("#tblSubCat").on('click', 'tr td button', function (e) {


        //var thisTxtBox = $(this);
        e.preventDefault();



    ClearControl();

    //alert('working in Progress');
    //var thisTxtBox = $(this);
    e.preventDefault();

    var _for = $(this).attr("data-for");
    var _id = $(this).attr("data-subcatId");
    var supplierSubCategory = {
        "supplierSubCategoryID": _id
            }



    if (_for == 'edit') {
        LoadCustSUBCategoryForEdit(supplierSubCategory);
            }

    if (_for == 'delete') {


                if (confirm('Are you sure to delete Sub Category?')) {
        // Save it!
        DeleteCustSUBCategory(supplierSubCategory)
    }
    else {
                    return;
                }


            }

        });





    $("#tblCat").on('click', 'tr td button', function (e) {

        ClearControl();

    //alert('working in Progress');
    //var thisTxtBox = $(this);
    e.preventDefault();

    var _for = $(this).attr("data-for");
    var _id = $(this).attr("data-catId");
    var supplierCategory = {
        "supplierCategoryID": _id
            }



    if (_for == 'edit') {
        LoadCustCategoryForEdit(supplierCategory);
            }

    if (_for == 'delete') {


                if (confirm('Are you sure to delete Category?')) {
        // Save it!
        DeleteCustCategory(supplierCategory)
    }
    else {
                    return;
                }


            }
        });




    function LoadCustCategoryForEdit(_obj) {
        $("#spInfo2").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/GetSupplierCategory",

    data: JSON.stringify(_obj),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data);
    $("#btnSaveCat").val('Update');
    $("#btnSaveCat").attr('data-catId', data.supplierCategoryID);


    $("#txtCatNameEdit").val(data.supplierCategoryName);

    $("#spInfo2").html('');


                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        }

    function DeleteCustCategory(_customerCategory) {

        $("#spInfo2").html('<strong>Please wait ....</strong>');

    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/DeleteSupplierCategory",

    data: JSON.stringify(_customerCategory),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();

        console.log(data);


    if (parseInt(data.resultID) == -1) {
        $("#spInfo2").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }
    else {
        $("#txtCatNameEdit").val('');
    $("#spInfo2").html('<strong style="color:green">' + data.resultMessage + '</strong>');
    SUPP_CAT = data.obj;
    LoadCustCategoryName(data.obj)
    BinCategoryTable(data.obj);
                    }


                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });

        }


    function LoadCustSUBCategoryForEdit(_obj) {
        $("#spInfo3").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/GetSubSupplierCategory",

    data: JSON.stringify(_obj),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data);
    $("#btnSaveSubCat").val('Update');
    $("#btnSaveSubCat").attr('data-subcatId', data.supplierSubCategoryID);


    $("#txtSubCatNameEditSub").val(data.supplierSubCategoryName);

    $("#spInfo3").html('');


                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });
        }



    function DeleteCustSUBCategory(_supplierSubCategory) {

        $("#spInfo3").html('<strong>Please wait ....</strong>');

    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/DeleteSubSupplierrCategory",

    data: JSON.stringify(_supplierSubCategory),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        // HideGateOverlay();

        console.log(data);


    if (parseInt(data.resultID) == -1) {
        $("#spInfo3").html('<strong style="color:red">' + data.resultMessage + '</strong>');
                    }
    else {
        $("#txtSubCatNameEditSub").val('');
    $("#spInfo3").html('<strong style="color:green">' + data.resultMessage + '</strong>');
    //CUST_CAT = data.obj;
    BinSubCategoryTable(data.obj)
    SUB_SUPP_CAT = data.obj;
                    }




                },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
                }
            });





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

    function editCustomer(id) {
        ClearControl();
    var supplier = {
        "supplierId": id
        }


    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/GetSupplier",

    data: JSON.stringify(supplier),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data);
    $("#btnSaveCust").val('Update');
    $("#btnSaveCust").attr('data-suppid', data.supplierId);
    $("#txtCustCode1").val(data.customerNo);
    $("#txtCustName").val(data.name);
    $("#txtShopName").val(data.shopName);
    $("#txtMobile1").val(data.mobileNo1);
    $("#txtMobile2").val(data.mobileNo2);
    $("#txtAddress").val(data.address1);
    $("#txtOpBalance").val(data.openingBalance);
    $("#txtContactPerson").val(data.contactPerson);




                // console.log(data.customerSubCategoryID);
                var subCat = SUB_SUPP_CAT.find(a => a.supplierSubCategoryID == data.supplierSubCategoryID);
    console.log(subCat);
    // $("#txtCategory").val();

    var _catName = subCat.supplierCategory.supplierCategoryName;
    var _catID = subCat.supplierCategoryID
    $("#txtSubCategory").val(subCat.supplierSubCategoryName);
    $("#txtCategory").val(_catName);
    $("#spInfo").html('');





    if (_catID != undefined) {
                    var _SubCatList = SUB_SUPP_CAT.filter(function (_obj) {
                        return _obj.supplierCategoryID == _catID;
                    });

    LoadSubCustName(_SubCatList);
                }


            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
            }
        });



    }

    function LoadSubCustName(_subcatList) {
        $("#dltxtSubCategory option").remove();

    $.each(_subcatList, function (i, prodData) {

        $("#dltxtSubCategory").append(' <option data-id="' + prodData.supplierSubCategoryID + '" data-custID="' + prodData.supplierSubCategoryID + '" value="' + prodData.supplierSubCategoryName + '" />');


        });

    }
    function deleteCustomer(id) {

        ClearControl();

    if (confirm('Are you sure to delete Customer?')) {
        // Save it!
        console.log('Thing was saved to the database.');
        }
    else {
            return;
        }




    var supplier = {
        "SupplierId": id
        }


    $("#spInfo").html('<strong style="color:blue"> Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Supplier/DeleteSupplier",

    data: JSON.stringify(supplier),
    dataType: "JSON",
    contentType: "application/json;charset=utf-8",
    success: function (data) {
        console.log(data);



    if (parseInt(data.resultID) == -1) {
        $("#spInfo").html('<strong style="color:red">' + data.resultMessage + '</strong>');

                }
    else {
        ClearControl();
    $("#spInfo").html('<strong style="color:green">' + data.resultMessage + '</strong>');
    CUST_LIST = data.obj;
    // tableCust.fnClearTable();
    tableCust.clear().draw();
    tableCust.rows.add(data.obj).draw();

                }





    $("#spInfo").html('');

            },
    error: function (a, b, c) {
        //  HideGateOverlay();
        alert(a, c);
            }
        });

    }

    function ClearControl() {
        $("#btnSaveCust").val('Save');
    $("#txtCategory").val('');
    $("#txtSubCategory").val('');

    $("#txtContactPerson").val('');

    $("#txtCustName").val('');

    //$("#txtDistrict").val('');
    //$("#txtDistrictNameEdit").val('');

    $("#txtAddress").val('');

    $("#txtMobile1").val('');

    $("#txtMobile2").val('');

    $("#txtCatNameEdit").val('');
    $("#txtCatNameEditSub").val('');

    $("#txtSubCatNameEditSub").val('');

    $("#txtOpBalance").val('0');

    //$("#txtOpComm").val('0');
    //$("#txtOpPcs").val('0');
    $("#btnSaveCust").attr('data-suppid', '0');

    //$("#btnSaveDistrict").attr('data-districtid', '0');
    //$("#btnSaveDistrict").val('Save');


    $("#btnSaveCat").attr('data-catId', '0');
    $("#btnSaveCat").val('Save');




    $("#btnSaveSubCat").attr('data-subcatId', '0');
    $("#btnSaveSubCat").val('Save');
    }



