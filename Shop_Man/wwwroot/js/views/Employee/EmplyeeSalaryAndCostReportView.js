
    var days = ['Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var EXPENSES_DATA = [];

    var SINGLE_EMPLOYEE=false;
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

                SINGLE_EMPLOYEE = true;
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
                url: WEB_URL + "Employee/GetEmplyeeSalaryAndCostReport",

                data: JSON.stringify(model),
                dataType: "JSON",
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    console.log(data.obj);
                    $("#spInfo").html('');
                    $("#spTitle").text(data.resultNo);
                    //if (queryStrings["IsDetails"] == "1") {
                       // BindDataDetails(data.obj);
                   // }
                    //else {
                    EXPENSES_DATA = data.obj;
                        BindData(data.obj);
                    //}
                    if (SINGLE_EMPLOYEE) {
                     
                        $("#spFilter").text('Employee Name : ' + data.obj[0].employeeName);

                        $("#btnSize").attr('disabled', 'disabled');
                        $("#btnArticle").attr('disabled', 'disabled');
                    }
                   else  {

                        $("#spFilter").text('All Employee' );
                    }
                },
                error: function (a, b, c) {
                    //  HideGateOverlay();
                    alert(a, c);
                }
            });

        }

        $("#btnSize").click(function () {

            BindData(EXPENSES_DATA);

        });

        function BindData(_obj) {
            $("#tblOrder tr").remove();
            if (SINGLE_EMPLOYEE) {

                $("#tblOrder").append(' <tr><td>SL</td><td>Inv</td><td>Date</td><td style="width:25%">Head Name</td><td  style="text-align:right;">Salary/Bonus Amount </td><td style="text-align:right;"> Paid Amount</td><td style="text-align:right;">Due Amount</td><td style="width:20%">Note</td></tr>');


                var _totAddAmount1 = 0;
                var _totDedAmount1 = 0;

                var _totPayAmount1 = 0;
                var _totRecAmount1 = 0;

                var _balanceAmount1 = 0;
                $.each(_obj, function (i, prodData) {

                    _totAddAmount1 += prodData.addAmount;
                    _totDedAmount1 += prodData.deductAmount;

                    _totPayAmount1 += prodData.payAmount;
                    _totRecAmount1 += prodData.receiveAmpunt;


                    _balanceAmount1 += prodData.balanceAmount;
                    var _currentBal = _balanceAmount1;
                    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.invNo + '</td><td>' + prodData.dDateFormated + '</td><td>' + prodData.headName + '</td><td style="text-align:right;">' + prodData.netAddAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.netPayAmount.toFixed(2) + '</td><td style="text-align:right;">' + _currentBal.toFixed(2) + '</td><td>' + prodData.remarks + '</td></tr>');


                });


                $("#tblOrder").append('<tr><td colspan="4">ToTal :</td><td style="text-align:right;">' + (_totAddAmount1 - _totDedAmount1).toFixed(2) + '</td><td style="text-align:right;">' + (_totPayAmount1 - _totRecAmount1).toFixed(2) + '</td><td style="text-align:right;">' + _balanceAmount1.toFixed(2)+'</td><td></td></tr>');



            }
            else {

                $("#tblOrder").append(' <tr><td rowspan="2">SL</td><td rowspan="2">Inv</td><td rowspan="2">Date</td><td rowspan="2">Emp Name</td><td rowspan="2">Head Name</td><td colspan="3">Salary/Bonus</td><td colspan="3">Employee Expenses</td><td rowspan="2">Note</td></tr>');
                $("#tblOrder").append('<tr><td style="border-left:1px solid black;">Add Amount</td><td>Ded Amount</td><td>Net Amount</td><td>Pay Amount</td><td>Rec Amount</td><td>Net Pay Amount</td></tr>');

                var _totAddAmount = 0;
                var _totDedAmount = 0;

                var _totPayAmount = 0;
                var _totRecAmount = 0;
                $.each(_obj, function (i, prodData) {

                    _totAddAmount += prodData.addAmount;
                    _totDedAmount += prodData.deductAmount;

                    _totPayAmount += prodData.payAmount;
                    _totRecAmount += prodData.receiveAmpunt;

                    $("#tblOrder").append('<tr><td>' + (i + 1) + '</td><td>' + prodData.invNo + '</td><td>' + prodData.dDateFormated + '</td><td>' + prodData.employeeName + '</td><td>' + prodData.headName + '</td><td style="text-align:right;">' + prodData.addAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.deductAmount.toFixed(2) + '</td><td style="text-align:right;">' + (prodData.addAmount - prodData.deductAmount).toFixed(2) + '</td><td style="text-align:right;">' + prodData.payAmount.toFixed(2) + '</td><td style="text-align:right;">' + prodData.receiveAmpunt.toFixed(2) + '</td><td style="text-align:right;">' + (prodData.payAmount - prodData.receiveAmpunt).toFixed(2) + '</td><td>' + prodData.remarks + '</td><</tr>');


                });


                $("#tblOrder").append('<tr><td colspan="5">ToTal :</td><td style="text-align:right;">' + _totAddAmount.toFixed(2) + '</td><td style="text-align:right;">' + _totDedAmount.toFixed(2) + '</td><td style="text-align:right;">' + (_totAddAmount - _totDedAmount).toFixed(2) + '</td><td style="text-align:right;">' + _totPayAmount.toFixed(2) + '</td><td style="text-align:right;">' + _totRecAmount.toFixed(2) + '</td><td style="text-align:right;">' + (_totPayAmount - _totRecAmount).toFixed(2) + '</td><td></td></tr>');

            }

        }



        $("#btnArticle").click(function () {
           
            BindDataEmployeewise();

        });

        function BindDataEmployeewise() {
            $("#tblOrder tr").remove();
            var arr = {};




            var _totAddAmountAll = 0;
            var _totDedAmountAll = 0;

            var _totPayAmountAll = 0;
            var _totRecAmountAll = 0;

            var _balanceAmountAll = 0;
            $.each(EXPENSES_DATA, function (i, prodData) {


                if (arr[prodData.employeeName]) {

                    var _ooo = arr[prodData.employeeName];
                    _ooo.push(prodData);

                    arr[prodData.employeeName] = _ooo;
                }
                else {
                    arr[prodData.employeeName] = [prodData];
                }

            });


            $("#tblOrder").append(' <tr><td rowspan="2">SL</td><td rowspan="2">Emp Name</td><td rowspan="2">Inv</td><td rowspan="2">Date</td><td rowspan="2">Head Name</td><td colspan="3">Salary/Bonus</td><td colspan="3">Employee Expenses</td><td rowspan="2">Due Amount</td><td rowspan="2">Note</td></tr>');
            $("#tblOrder").append('<tr><td style="border-left:1px solid black;">Add Amount</td><td>Ded Amount</td><td>Net Amount</td><td>Pay Amount</td><td>Rec Amount</td><td>Net Pay Amount</td></tr>');





            Object.entries(arr).forEach(([key, _value]) => {
                console.log(_value);

                var _str = '';

                if (Array.isArray(_value)) {
                    var _rowSpan = 0;
                    $.each(_value, function (i, val2) {
                        _rowSpan += 1;
                    })

                    var _totAddAmount = 0;
                    var _totDedAmount = 0;
                   

                    var _totPayAmount = 0;
                    var _totRecAmount = 0;


                    var _balanceAmount = 0;
                    $.each(_value, function (i, val2) {
                     

                        _totAddAmount += val2.addAmount;
                        _totDedAmount += val2.deductAmount;

                        _totPayAmount += val2.payAmount;
                        _totRecAmount += val2.receiveAmpunt;



                        _totAddAmountAll += val2.addAmount;
                        _totDedAmountAll += val2.deductAmount;

                        _totPayAmountAll += val2.payAmount;
                        _totRecAmountAll += val2.receiveAmpunt;

                        _balanceAmount += val2.balanceAmount;
                        var _currentBal = _balanceAmount;
                        _balanceAmountAll += val2.balanceAmount;
                        if (i == 0) {
                            _str += ' <tr style="background-color:#fff"><td>' + (i + 1) + '</td> <td rowspan=' + _rowSpan + '>' + val2.employeeName + '</td><td>' + val2.invNo + '</td><td>' + val2.dDateFormated + '</td><td>' + val2.headName + '</td><td style="text-align:right;">' + val2.addAmount.toFixed(2) + '</td><td style="text-align:right;">' + val2.deductAmount.toFixed(2) + '</td><td style="text-align:right;">' + val2.netAddAmount.toFixed(2) + '</td><td style="text-align:right;">' + val2.payAmount.toFixed(2) + '</td><td style="text-align:right;">' + val2.receiveAmpunt.toFixed(2) + '</td><td style="text-align:right;">' + val2.netPayAmount.toFixed(2) + '</td><td>' + _currentBal.toFixed(2) + '</td><td>' + val2.remarks + '</td></tr>';

                        }
                        else {

                            _str += ' <tr style="background-color:#fff"><td>' + (i + 1) + '</td> <td>' + val2.invNo + '</td><td>' + val2.dDateFormated + '</td><td>' + val2.headName + '</td><td style="text-align:right;">' + val2.addAmount.toFixed(2) + '</td><td style="text-align:right;">' + val2.deductAmount.toFixed(2) + '</td><td style="text-align:right;">' + val2.netAddAmount.toFixed(2) + '</td><td style="text-align:right;">' + val2.payAmount.toFixed(2) + '</td><td style="text-align:right;">' + val2.receiveAmpunt.toFixed(2) + '</td><td style="text-align:right;">' + val2.netPayAmount.toFixed(2) + '</td><td>' + _currentBal.toFixed(2) + '</td><td>' + val2.remarks + '</td></tr>';

                        }

      

                    });
                    $("#tblOrder").append(_str);

                    $("#tblOrder").append('<tr><td colspan="5">Sub ToTal :</td><td style="text-align:right;">' + _totAddAmount.toFixed(2) + '</td><td style="text-align:right;">' + _totDedAmount.toFixed(2) + '</td><td style="text-align:right;">' + (_totAddAmount - _totDedAmount).toFixed(2) + '</td><td style="text-align:right;">' + _totPayAmount.toFixed(2) + '</td><td style="text-align:right;">' + _totRecAmount.toFixed(2) + '</td><td style="text-align:right;">' + (_totPayAmount - _totRecAmount).toFixed(2) + '</td><td>' + _balanceAmount.toFixed(2)+'</td><td></td></tr>');



                }


            });


            $("#tblOrder").append('<tr><td colspan="5">Grand ToTal :</td><td style="text-align:right;">' + _totAddAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _totDedAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + (_totAddAmountAll - _totDedAmountAll).toFixed(2) + '</td><td style="text-align:right;">' + _totPayAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + _totRecAmountAll.toFixed(2) + '</td><td style="text-align:right;">' + (_totPayAmountAll - _totRecAmountAll).toFixed(2) + '</td><td>' + _balanceAmountAll.toFixed(2)+'</td><td></td></tr>');


            arr = {};
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
