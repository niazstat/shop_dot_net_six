
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

            }

    $("#spInfo").html('<strong style="color:blue">Loading Data Please wait.. !</strong>');
    $.ajax({
        type: "POST",
    url: WEB_URL + "Customer/GetExistingCustomerYearCloseList",

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
                                              <td>বছর</td>
                                                <td>ক্লোজ তাঃ</td>
                                                <td>ওপেনিং</td>
                                                <td>সেলস</td>
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
                                                <td>ক্লোসিং</td>

                                                <td></td>
                                            </tr >
                                        </thead >`);

    var _openingBalance = 0;

    var _recvAmountAll = 0;
    var _salesAmountAll = 0;
    var _otherAmountAll = 0;

    var _payAmountAll = 0;
    var _sackAmountAll = 0;
    var _payCashAmountAll = 0;

    var _adjAountAll = 0;

    var _retAmountAll = 0;

    var _yearShortAmntAll = 0;
    var _rejItmAmntAll = 0;

    $.each(_obj, function (i, prodData) {




        $("#tblOrder").append(` <tr>
                    <td>`+ prodData.yearName + `</td>
                                                <td>`+ prodData.yearCloseDateFormated + `</td>
                                                <td>`+ prodData.openingBalance + `</td>
                                                <td>`+ prodData.salesAmount + `</td>
                                                <td>`+ prodData.totalSackNoFee + `</td>
                                                <td>`+ prodData.transportCost + `</td>
                                                <td>`+ prodData.addLessAmount + `</td>
                                                <td>`+ prodData.receiveAmount + `</td>
                                                <td>`+ prodData.cashReceiveAmount + `</td>
                                                <td>`+ prodData.checkRecev + `</td>
                                                <td>`+ prodData.checkPayment + `</td>
                                                <td>`+ prodData.cashPayment + `</td>
                                                <td>`+ prodData.adjustAmount + `</td>
                                                <td>`+ prodData.returnAmount + ` </td>
                                                <td>`+ prodData.closingShortAmount + `</td>
                                                <td>`+ prodData.rejectGoodsAmount + `</td>
                                                <td>`+ prodData.closingAmount + `</td>

                                                <td><a class="mb-2 mr-2 btn-icon btn-shadow  btn btn-outline-success" href="#" target="_blank" data-yearname="` + prodData.yearName + `" data-custid="` + prodData.customerID + `">Details</a></td>
                                            </tr >`);


            });



        }





    $("#tblOrder").on('click', 'tr td a', function () {


        let _custid = $(this).attr("data-custid");
    let _yearname = $(this).attr("data-yearname");
    $(this).attr('href', WEB_URL + 'Customer/CustomerClosingDetails?customerID=' + _custid + '&&yearName=' + _yearname +'&&_type=n&&dDate=01-Jan-1900');

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
        win.document.write('<title>Party_Year_Closing_List_Report</title>');   // <title> FOR PDF HEADER.
            win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
            win.document.write('</head>');
        win.document.write('<body>');
            win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
            win.document.write('</body></html>');

    win.document.close(); 	// CLOSE THE CURRENT WINDOW.

    win.print();    // PRINT THE CONTENTS.
    }


