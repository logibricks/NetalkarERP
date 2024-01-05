//var unsaved = false;
//$(window).bind('beforeunload', function () {
//    if (unsaved) {
//        return "";
//    }
//});
//$(document).on('change', ':input', function () {
//    unsaved = true;
//});
//$(window).bind('beforeunload', function () {
//    return '>>>>>Before You Go<<<<<<<< \n "yes"';
//});


$("#create").click(function (e) {
    e.preventDefault();
    //FOR INPUT
    $('.field-validation-valid').remove();
    var requirectrl = $('.validinput');
    for (i = 0; i <= requirectrl.length - 1; i++) {
        var inputctrl = requirectrl[i];
        var lbl = $(inputctrl).parent('div').prev('label').text();
        var name = lbl.replace('*', "");
        var nameId = $(inputctrl).attr('id');
        $('#valid' + nameId).remove();
        if ($(inputctrl).val() == '' || $(inputctrl).val() == '--Select--') {
            $(inputctrl).after('<span class="dirty" style="color:#a94442" id=valid' + nameId + '>' + name + ' is required </span>');
        }
    }

    //FOR AUTOCOMPLETE
    var requirectrlauto = $('.autocompletes');
    for (i = 0; i <= requirectrlauto.length - 1; i++) {
        var autoinputctrl = requirectrlauto[i];
        var autoctrl = $(autoinputctrl).parent('div').find('input');
        var lbl = $(autoinputctrl).text();
        var name = lbl.replace('*', "");
        var nameId = $(autoctrl).attr('id');
        $('#valid' + nameId).remove();
        if ($(autoctrl).val() == '') {
            $(autoinputctrl).parent('div').find('div').find('label').after('<span class="dirty" style="color:#a94442" id=valid' + nameId + '>' + name + ' is required </span>');
        }
    }

    //FOR MULTIPLE SELECT
    var requirectrlmultiselect = $('.form');
    for (i = 0; i <= requirectrlmultiselect.length - 1; i++) {
        var multiselectctrl = requirectrlmultiselect[i];
        var nameId = $(multiselectctrl).attr('id');
        var lbl2 = $('label[for=' + nameId + ']').text();
        var name = lbl2.replace('*', "");
        var len = $('#' + nameId + ' :selected').length;
        $('#valid' + nameId).remove();
        if (len < 1) {
            $(multiselectctrl).next('div').after('<span class="dirty" style="color:#a94442" id=valid' + nameId + '>' + name + ' is required </span>');
        }
    }



    //FOR EMAIL FIELD
    var requirectrlemail = $('.email');
    for (i = 0; i <= requirectrlemail.length - 1; i++) {
        var emailctrl = requirectrlemail[i];
        var nameId = $(emailctrl).attr('id');
        var lbl4 = $('label[for=' + nameId + ']').text();
        var name = lbl4.replace('*', "");
        if ($(emailctrl).val() != '') {
            if (validemail($(emailctrl).val()) == false) {
                $('#valid' + nameId).remove();
                $(emailctrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Email is not valid</span>');
            }
        }
    }

    //FOR URL FIELD
    var requirectrlurl = $('.url');
    for (i = 0; i <= requirectrlurl.length - 1; i++) {
        var urlctrl = requirectrlurl[i];
        var nameId = $(urlctrl).attr('id');
        var lbl4 = $('label[for=' + nameId + ']').text();
        var name = lbl4.replace('*', "");
        if ($(urlctrl).val() != '') {
            if (validurl($(urlctrl).val()) == false) {
                $('#valid' + nameId).remove();
                $(urlctrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>URL is not valid</span>');
            }
        }
    }



    //FOR START AND END DATE FIELD
    if ($('.validation_start_date').val() != undefined && $('.validation_end_date').val() != undefined) {
        $('#validactual_finish_time').remove();
        $('#validactual_finish_date').remove();
        if (new Date($('.validation_start_date').val()) > (new Date($('.validation_end_date').val()))) {
            let _finish_name = $('.label_end_date').text().replace('*', '');
            let _start_name = $('.label_start_date').text().replace('*', '');
            let nameId = $('.validation_end_date').attr('id');
            $('.validation_end_date').after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>' + _finish_name + ' should be less than ' + _start_name + ' </span>');
            return false;
        }
    }

    // FOR TIME FIELD
    if ($('.validation_start_date').val() != undefined || $('.validation_end_date').val() != undefined) {
        $('#validactual_finish_time').remove();
        $('#validactual_finish_date').remove();
        if ($('.validation_start_date').val().toString() == $('.validation_end_date').val().toString()) {
            let _finish_name = $('.label_end_time').text().replace('*', '');
            let _start_name = $('.label_start_time').text().replace('*', '');
            let nameId = $('.validation_end_time').attr('id');

            if ($('.validation_start_time').val() != undefined && $('.validation_end_time').val() != undefined) {
                if (parseInt($('.validation_start_time').val().split(':')[0]) > parseInt($('.validation_end_time').val().split(':')[0])) {
                    $('.validation_end_time').after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>' + _finish_name + ' should be less than ' + _start_name + '</span>');
                    return false;
                }
                if (parseInt($('.validation_start_time').val().split(':')[0]) == parseInt($('.validation_end_time').val().split(':')[0])) {
                    if (parseInt($('.validation_start_time').val().split(':')[1]) > parseInt($('.validation_end_time').val().split(':')[1])) {
                        $('.validation_end_time').after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>' + _finish_name + ' should be less than ' + _start_name + '</span>');
                        return false;
                    }
                }
            }
        }
    }




    //FOR DR AND CR VALIDATION IN JOURNAL ENTRY FORM
    if ($('.lbl_dr').text() != $('.lbl_cr').text()) {
        sweetAlert("", "Dr. and Cr. should be equal!", "error");
        return false;
    }
    //for payment receipt on brs 

    if ($('.pay_total').text() != undefined) {
        if ((parseFloat($('.pay_total').text()) + parseFloat($('.rec_total').text())) == 0) {
            sweetAlert("", "No Data to save!", "error");
            return false;
        }
    }
    //FOR LINE ITEM IN EVERY FORM MUST HAVE MORE THAN 0 ROWS
    if ($('#ContactGrid tbody tr td').hasClass('dataTables_empty')) {
        sweetAlert("", "Some mandatory fields left blank!", "error");
        return false;
    }

    if ($('#WPTag tbody tr td').hasClass('dataTables_empty')) {
        if ($('#WPBatch tbody tr td').hasClass('dataTables_empty')) {
            sweetAlert("", "Add Items to create", "error");
            return false;
        }

    }
    if ($('#WPBatch tbody tr td').hasClass('dataTables_empty')) {
        if ($('#WPTag tbody tr td').hasClass('dataTables_empty')) {
            sweetAlert("", "Add Items to create", "error");
            return false;
        }

    }
    //for qc quantity check

    var inspection_lot_qty = $("#inspection_lot_qty").val();
    var sample_size_checked = $("#sample_size_checked").val();
    var sample_size_accepted = $("#sample_size_accepted").val();
    var sample_size_rejected = $("#sample_size_rejected").val();
    var total_accepted_qty = $("#total_accepted_qty").val();
    var total_rejected_qty = $("#total_rejected_qty").val();
    if (inspection_lot_qty != undefined) {
        if (parseFloat(inspection_lot_qty) < parseFloat(sample_size_checked)) {
            sweetAlert("", "Sample size checked cannot be greater than inspection lot quantity !", "error");
            return false;
        }
        if (parseFloat(sample_size_checked) < (parseFloat(sample_size_accepted) + parseFloat(sample_size_rejected))) {
            sweetAlert("", "Sample size accepted and rejected cannot be greater than Sample size checked !", "error");
            return false;
        }
        if (parseFloat(inspection_lot_qty) != (parseFloat(total_accepted_qty) + parseFloat(total_rejected_qty))) {
            sweetAlert("", "Total accepted and rejected quantity should be equal to inspection lot quantity !", "error");
            return false;
        }
    }
    //    var q=$("#QUALITY_MANAGED").val();
    //    if(q=="true")
    //    {
    //     if ($('#ParameterGrid tbody tr td').hasClass('dataTables_empty')) {
    //        sweetAlert("", "Some mandatory fields left blank!", "error");
    //        return false;
    //    }
    //}
    //CHECK IF EMPTY GRID IN ITEM MASTER
    if ($('#ITEM_VALUATION_ID').val() == 2) {
        var txt_ctrl = $('#item_valuation_grid tbody tr').length;
        for (var k = 1; k <= txt_ctrl; k++) {
            var txt_validation = $('#txt' + k).val();
            if (txt_validation == 0) {
                sweetAlert("", "Standard cost value should not zero!", "error");
                return false;
            }
            if (txt_validation == '') {
                sweetAlert("", "Standard cost value should not be left blank!", "error");
                return false;
            }
        }
    }
    //check delivery type for items in purchase order
    if ($('#delivery_type_id').val() == 2) {
        var t = $('#ContactGrid tbody tr').length;
        // var rowcount = t.fnGetData().length;
        if (t > 1) {
            sweetAlert("", "More than one item cannot be entered!", "error");
            return false;
        }
        if (parseFloat($("#diff1").val()) != parseFloat($("#diff2").val())) {
            sweetAlert("", "Staggered quantity and line item quantity should be equal!", "error");
            return false;
        }
    }
    if ($('#delivery_type_id').val() == 3) {
        var t = 0;
        if (parseFloat($("#maximum_limit_qty").val()) <= 0) {
            sweetAlert("", "Maximum Limit Qty. should be greater than zero !", "error");
            return false;
        }
        $('#ContactGrid tbody tr').each(function () {
            var Quantity = $(this).find("td").eq(4).html();
            if (Quantity > 1) {
                sweetAlert("", "Quantity cannot be greater than one for Blanket Order!", "error");
                t += 1;
            }
        });
        if (t != 0) {
            return false;
        }
    }
    //FOR CHECK VALIDATION ON POSTING DATE IN EVERY PAGE
    if ($('.postingdate').val() != '') {
        if ($('.postingdate').val() != undefined) {
            var st = '';
            var status = $('.postingdate').val();
            var saledate = $(".salesdate").val();
            $.ajax({
                url: '/Generic/CheckValidation',
                //  url: '@Url.Action("CheckValidation","Generic")',
                method: 'get',
                async: false,
                data: { entity: "postingperiods", id: 0, posting_date: status },
                success: function (data) {
                    st = data;
                }
            });
            if (st != 'Ok') {
                if (st == 'Unlocked except Sales') {
                    if (saledate == undefined) {
                        // return true;
                    }
                    else {
                        sweetAlert("", "Posting period is locked !", "error");
                        return false;
                    }
                }
                else {
                    sweetAlert("", st, "error");
                    return false;
                }

            }
        }
    }
    //TO CHECK DOCUMENTNUMBERING 
    if ($(".postingdocumentdate").val() != '') {
        if ($('.category').val() != undefined) {
            var st = '';
            var postingdate = $('.postingdocumentdate').val();
            var category = $('.category').val();
            $.ajax({
                url: '/Generic/CheckValidation',
                // url: '@Url.Action("CheckValidation","Generic")',
                method: 'get',
                async: false,
                data: { entity: "checkdocumentnumbering", id: category, posting_date: postingdate },
                success: function (data) {
                    st = data;
                }
            });
            if (st == "Not Ok") {
                sweetAlert("", "Document Numbering does not exists !", "error");
                return false;
            }
        }
    }
    //TO CHECK DOCUMENTNUMBERING for rcm
    if ($(".PRCM").val() != '') {
        if ($('.PRCM').val() != undefined) {
            var st = '';
            $.ajax({
                url: '/Generic/CheckValidation',
                method: 'get',
                async: false,
                data: { entity: "checkdocumentnumbringforrcm", id: $(".selectedbycategory").val() },
                success: function (data) {
                    st = data;
                }
            });
            if (st == "Not Ok") {
                sweetAlert("", "Document Numbering does not exists for RCM !", "error");
                return false;
            }
        }
    }
    //FOR CHECK CURRENCY VALIDATION
    if ($(".postingcurrency").val() != '') {
        if ($('.postingcurrency').val() != undefined) {
            var st = '';
            var currency = $('.postingcurrency').val();
            var dt = $('.postingdate').val();
            $.ajax({
                url: '/Generic/CheckValidation',
                // url: '@Url.Action("CheckValidation","Generic")',
                method: 'get',
                async: false,
                data: { entity: "exchangerate", id: currency, posting_date: dt },
                success: function (data) {
                    st = data;
                }
            });
            if (st != "Ok") {
                sweetAlert("", st, "error");
                return false;
            }
        }
    }

    //FOR TELEPHONE CODE
    var requirectrltelephonecode = $('.telephonecode');
    for (i = 0; i <= requirectrltelephonecode.length - 1; i++) {
        var telephonecodectrl = requirectrltelephonecode[i];
        var nameId = $(telephonecodectrl).attr('id');
        var nextinputid = $(telephonecodectrl).parent('div').next('label').next('div').find('input').attr('id');
        var lbl5 = $(telephonecodectrl).parent('div').prev('label').text();
        var name = lbl5.replace('*', "");
        if ($(telephonecodectrl).val() != '') {
            if (!($.isNumeric($(telephonecodectrl).val()))) {
                $('#valid' + nameId).remove();
                $(telephonecodectrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Telephonecode is not valid</span>');
            }
            else {
                if ($('#' + nextinputid).val() == '') {
                    $('#valid' + nextinputid).remove();
                    $(telephonecodectrl).parent('div').next('label').next('div').find('input').after('<span class="dirty" style="color:#a94442" id=valid' + nextinputid + '>Telephone is required </span>');
                }
            }
        }

    }


    //FOR TELEPHONE
    var requirectrltelephone = $('.telephone');
    for (i = 0; i <= requirectrltelephone.length - 1; i++) {
        var telephonectrl = requirectrltelephone[i];
        var nameId = $(telephonectrl).attr('id');
        var preinputid = $(telephonectrl).parent('div').prev('label').prev('div').find('input').attr('id');
        var lbl6 = $(telephonectrl).parent('div').prev('label').text();
        var name = lbl6.replace('*', "");
        if ($(telephonectrl).val() != '') {
            if (!($.isNumeric($(telephonectrl).val()))) {
                $('#valid' + nameId).remove();
                $(telephonectrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Telephone is not valid</span>');
            }
            else {
                if ($(telephonectrl).val().length != 8) {
                    $('#valid' + nameId).remove();
                    $(telephonectrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Telephone is not valid</span>');
                }
                if ($(telephonectrl).val().length == 8) {
                    if ($('#' + preinputid).val() == '') {
                        $('#valid' + preinputid).remove();
                        $(telephonectrl).parent('div').prev('label').prev('div').find('input').after('<span class="dirty" style="color:#a94442" id=valid' + preinputid + '>Telephonecode is required </span>');
                    }
                }
            }
        }

    }

    //FOR MOBILE 
    var requirectrlmobile = $('.mobile');
    for (i = 0; i <= requirectrlmobile.length - 1; i++) {
        var mobilectrl = requirectrlmobile[i];
        var nameId = $(mobilectrl).attr('id');
        var lbl7 = $(mobilectrl).parent('div').prev('label').text();
        var name = lbl7.replace('*', "");
        if ($(mobilectrl).val() != '') {
            if (!($.isNumeric($(mobilectrl).val()))) {
                $('#valid' + nameId).remove();
                $(mobilectrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Mobile is not valid</span>');
            }
            else {
                if ($(telephonectrl).val().length != 10) {
                    $('#valid' + nameId).remove();
                    $(mobilectrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Mobile is not valid</span>');
                }
            }
        }

    }

    //FOR PAN
    var requirectrlpan = $('.pan');
    for (i = 0; i <= requirectrlpan.length - 1; i++) {
        var panctrl = requirectrlpan[i];
        var nameId = $(panctrl).attr('id');
        var lbl8 = $('label[for=' + nameId + ']').text();
        var name = lbl8.replace('*', "");
        if ($(panctrl).val() != '') {
            if (fnValidatePAN($(panctrl).val()) == false) {
                $('#valid' + nameId).remove();
                $(panctrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>PAN No is not valid </span>');
            }
        }
    }


    //FOR PINCODE
    var requirectrlpincode = $('.pincode');
    for (i = 0; i <= requirectrlpincode.length - 1; i++) {
        var pincodectrl = requirectrlpincode[i];
        var nameId = $(pincodectrl).attr('id');
        var lbl9 = $('label[for=' + nameId + ']').text();
        var name = lbl9.replace('*', "");
        if ($(pincodectrl).val() != '') {
            if (pincode($(pincodectrl).val()) == false) {
                $('#valid' + nameId).remove();
                $(pincodectrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Pincode is not valid</span>');
            }
        }
    }
    //FOR GSTIN
    var requirectrlgstin = $('.gstin');
    for (i = 0; i <= requirectrlgstin.length - 1; i++) {
        var pincodectrl = requirectrlgstin[i];
        var nameId = $(pincodectrl).attr('id');
        var lbl9 = $('label[for=' + nameId + ']').text();
        var name = lbl9.replace('*', "");
        if ($(pincodectrl).val() != '') {
            if ($(pincodectrl).val() != "NA") {
                if (GSTIN($(pincodectrl).val()) == false) {
                    $('#valid' + nameId).remove();
                    $(pincodectrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>GSTIN is not valid</span>');
                }
            }

        }
    }
    //for onaccount 
    var onaccount = $(".on_account");
    for (i = 0; i <= onaccount.length - 1; i++) {
        var a = onaccount[i];
        var b = $(a).html();
        if (parseFloat(b) < 0) {
            sweetAlert("", "On Account amount can not be negative.", "error");
            return false;
        }
    }
    //FOR LEDGER GRID
    var count = 0;
    var ledger = $('.ledger_grid');
    for (i = 0; i <= ledger.length - 1; i++) {
        var ledgerctrl = ledger[i];
        var nameId = $(ledgerctrl).attr('id');
        var lbl10 = $(ledgerctrl).text();
        if (lbl10 == '') {
            if (count == 0) {
                $('#valid' + nameId).remove();
                $(ledgerctrl).after('<span class="dirty" style="color:#a94442" id=valid' + nameId + '>Ledger Code is required</span>');
                count = count + 1;
            }
            else {
                $('#valid' + nameId).remove();
                $(ledgerctrl).after('<span class="dirty" style="color:#a94442" id=valid' + nameId + '>Ledger Name is required</span>');
                count = 0;
            }
        }
        else {
            $('#valid' + nameId).remove();
        }
    }







    var spanlength = $('.dirty');
    if (spanlength.length > 0) {
        sweetAlert("", "Some mandatory fields left blank!", "error");
        return false;
    }
    var invalidspanlength = $('.dirtyinvalid');
    if (invalidspanlength.length > 0) {
        sweetAlert("", "Some fields are not valid!", "error");
        return false;
    }



    else {
        var ctrl = $(this).attr('data-controller');
        var ct = ctrl.replace(/([a-z])([A-Z])/g, "$1 $2");
        var chekid = $(this).attr('data-id');
        var num = $('.num').val();
        var name = $('.num1').val();
        var name1 = $('.num2').val();

        var idd = "";
        if (chekid == "IgnoreCheck") {
            document.forms[0].submit();
        }
        if (chekid != undefined) {
            if (chekid != "IgnoreCheck") {
                idd = chekid;
            }
        }
        else {
            idd = 0;
        }
        $.ajax({
            url: '/Generic/CheckDuplicate',
            //  url: @Url.Action("CheckDuplicate","Generic"),
            type: "GET",
            dataType: "JSON",
            asynch: true,
            data: { codeornum: num, name: name, name1: name1, id: idd, ctrlname: ctrl },
            success: function (output) {
                if (output != 0) {
                    sweetAlert("", ct + " already exists !", "error");
                }
                else {
                    $('.removedisabled').removeAttr('disabled', 'disabled');
                    document.forms[0].submit();
                }
            }
        });
    }
});








validateForm();
function validateForm() {
    $('input,select,textarea').focusout(function () {
        var nameId = $(this).attr('id');
        var ctrl = "";
        if ($(this).hasClass('validinput')) {
            var ctrl = $(this);
        }
        var lbl6 = $(this).parent('div').prev('label').text();
        var name = lbl6.replace('*', "");
        $('#valid' + nameId).remove();
        if (ctrl != undefined) {
            if (ctrl.length > 0) {
                if ($(ctrl).val() == '' || $(ctrl).val() == '--Select--') {
                    $(this).after('<span class="dirty" style="color:#a94442" id=valid' + nameId + '>' + name + ' is required </span>');
                }
            }
        }


        //FOR EMAIL ON FOCUS
        if ($(this).hasClass('email')) {
            if ($(this).val() != '') {
                if (validemail($(this).val()) == false) {
                    $('#valid' + nameId).remove();
                    $(this).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Email is not valid </span>');
                }
            }
        }
        //FOR URL FIELD ON FOCUS
        if ($(this).hasClass('url')) {
            if ($(this).val() != '') {
                if (validurl($(this).val()) == false) {
                    $('#valid' + nameId).remove();
                    $(this).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>URL is not valid </span>');
                }
            }
        }
        //FOR TELEPHONE CODE ON FOCUS
        if ($(this).hasClass('telephonecode')) {
            var nextinputid = $(this).parent('div').next('label').next('div').find('input').attr('id');
            if ($(this).val() != '') {
                if (!($.isNumeric($(this).val()))) {
                    $('#valid' + nameId).remove();
                    $(this).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Telephonecode is not valid </span>');
                }
                else {
                    if ($('#' + nextinputid).val() == '') {
                        $('#valid' + nextinputid).remove();
                        $(this).parent('div').next('label').next('div').find('input').after('<span class="dirty" style="color:#a94442" id=valid' + nextinputid + '>Telephone is required </span>');
                    }
                }
            }
            else {
                $('#valid' + nextinputid).remove();
            }
        }


        //FOR TELEPHONE ON FOCUS
        if ($(this).hasClass('telephone')) {
            var preinputid = $(this).parent('div').prev('label').prev('div').find('input').attr('id');
            if ($(this).val() != '') {
                if (!($.isNumeric($(this).val()))) {
                    $('#valid' + nameId).remove();
                    $(this).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Telephone is not valid </span>');
                }
                else {
                    if ($(this).val().length != 8) {
                        $('#valid' + nameId).remove();
                        $(this).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Telephone is not valid</span>');
                    }
                    if ($(this).val().length == 8) {
                        if ($('#' + preinputid).val() == '') {
                            $('#valid' + preinputid).remove();
                            $(this).parent('div').prev('label').prev('div').find('input').after('<span class="dirty" style="color:#a94442" id=valid' + preinputid + '>Telephonecode is required </span>');
                        }
                    }
                }
            }
            else {
                $('#valid' + preinputid).remove();
            }
        }




        //FOR MOBILE ON FOCUS
        if ($(this).hasClass('mobile')) {
            if ($(this).val() != '') {
                if ($(this).val().length < 10) {
                    $('#valid' + nameId).remove();
                    if (!($.isNumeric($(this).val()))) {
                        $(this).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Mobile is not valid </span>');
                    }
                    else {
                        if ($(telephonectrl).val().length != 10) {
                            $('#valid' + nameId).remove();
                            $(mobilectrl).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Mobile is not valid</span>');
                        }
                    }
                }
            }
            else {
                $('#valid' + nameId).remove();
            }
        }

        //FOR PAN ON FOCUS
        if ($(this).hasClass('pan')) {
            if ($(this).val() != '') {
                if (fnValidatePAN($(this).val()) == false) {
                    $('#valid' + nameId).remove();
                    $(this).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>PAN No is not valid </span>');
                }
            }
        }
        //FOR PINCODE ON FOCUS
        if ($(this).hasClass('pincode')) {
            if ($(this).val() != '') {
                if (pincode($(this).val()) == false) {
                    $('#valid' + nameId).remove();
                    $(this).after('<span class="dirtyinvalid" style="color:#a94442" id=valid' + nameId + '>Pincode is not valid</span>');
                }
            }
        }
    });
    $('input,select,textarea').focusin(function () {
        var nameId = $(this).attr('id');
        $('#valid' + nameId).remove();
    });
}



























function validemail(inputtxt) {
    var email = new RegExp('^[a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,15})$', 'i');
    if (inputtxt != undefined) {
        if (email.test(inputtxt)) {
            return true;
        }
        else {
            return false;
        }
    }
}
function validurl(inputtxt) {
    var url = new RegExp('((http\://|https\://|ftp\://)|(www.))+(([a-zA-Z0-9\.-]+\.[a-zA-Z]{2,4})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9%:/-_\?\.~]*)?', 'i');
    if (inputtxt != undefined) {
        if (url.test(inputtxt)) {
            return true;
        }
        else {
            return false;
        }
    }
}



function fnValidatePAN(Obj) {
    if (Obj != "") {
        if (Obj != undefined) {
            ObjVal = Obj;
            var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
            var code = /([C,P,H,F,A,T,B,L,J,G])/;
            var code_chk = ObjVal.substring(3, 4);
            if (ObjVal.search(panPat) == -1) {
                return false;
            }
            if (code.test(code_chk) == false) {
                return false;
            }
        }
    }
}


function pincode(inputtxt) {
    if (inputtxt != undefined) {
        var pin = /^\d{6}$/;
        if (inputtxt.match(pin)) {
            return true;
        }
        else {
            return false;
        }
    }
}

function GSTIN(inputtxt) {
    if (inputtxt != undefined) {
        var pin = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;
        if (inputtxt.match(pin)) {
            return true;
        }
        else {
            return false;
        }
    }
}














// select plant on the basis of category load or change
$.setPlntbycategory = function (cat_id, plant_id) {
    try {
        $.ajax({
            url: '/Generic/plantby_doc_no',
            method: 'get',
            data: { category_id: cat_id },
            success: function (plant) {
                $('#' + plant_id).val(plant).trigger('change');
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    }
    catch (msg) {
        console.log(msg.message);
    }
}
try {
    var category_id = $('.setPlant').val();
    var plant_id = $('.selectedbycategory').attr('id');
    if (category_id != '') {
        if (category_id != undefined) {
            $.setPlntbycategory($('.setPlant').val(), $('.selectedbycategory').attr('id'));
        }
    }
}
catch (msg) {
    console.log(msg.message);
}

try {
    $('.setPlant').on('change', function () {
        var category_id = $(this).val();
        var plant_id = $('.selectedbycategory').attr('id');
        if (category_id != '') {
            if (category_id != undefined) {
                $.setPlntbycategory(category_id, plant_id);
            }
        }
    });
}
catch (msg) {
    console.log(msg.message);
}

try {
    //select auto if drop down has one value it applies when called
    $.getSelectedifLengthisOne = function (ctrl_id) {
        var length = $('#' + ctrl_id + '  > option').length;
        if (length == 2) {
            $("select#" + ctrl_id + "").prop('selectedIndex', 1).trigger('change');
        }
    }
}
catch (msg) {
    console.log(msg.message);
}
//select auto if drop down has one value it applies for all on load
try {
    if($(".egnore").val() == undefined){
    var numSelect = $('select');
    if (numSelect != undefined) {
        for (var i = 0; i <= numSelect.length - 1; i++) {
            var ctrl = numSelect[i];
            if (!($(ctrl).hasClass('ignoreOneSelect'))) {
                var length = $(ctrl).find('option').length
                if (length == 2) {
                    var emptyOption = $(ctrl).find('option:eq(0)').val();
                    if (emptyOption == undefined || emptyOption == 0) {
                        $(ctrl).prop('selectedIndex', 1).trigger('change');
                    }
                }
            }
        }
    };
    }
}
catch (msg) {
    console.log(msg.message);
}
try {
    if ($(".egnore").val() == undefined) {
    $(window).on('shown.bs.modal', function () {
        var numSelect = $('.modal').find('select');
        if (numSelect != undefined) {
            for (var i = 0; i <= numSelect.length - 1; i++) {
                var ctrl = numSelect[i];
                if (!($(ctrl).hasClass('ignoreOneSelect'))) {
                    var length = $(ctrl).find('option').length
                    if (length == 2) {
                        $(ctrl).prop('selectedIndex', 1).trigger('change');
                    }
                }
            }
        };
    });
    }
}
catch (msg) {
    console.log(msg.message);
}


//function to set categorylist by plant on uper grid's button click
$.filter_category = function (category_id, plant_val, doc_id) {
    try {
        $.ajax({
            url: '/Generic/CategoryListByPlant',
            type: 'get',
            async: false,
            data: { plant_id: plant_val, id: doc_id },
            success: function (category) {
                $('#' + category_id).empty();
                $('#' + category_id).append('<option>--Select--</option>');
                $.each(category, function (i, element) {
                    $('#' + category_id).append('<option value=' + element.document_numbring_id + '>' + element.category + '</option>');
                });
            }
        });
    }
    catch (msg) { console.log(msg.message) }
}
