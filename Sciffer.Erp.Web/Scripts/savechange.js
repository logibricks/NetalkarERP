/************ FOR SAVE CHANGE MESSAGE ON PAGE *****************/
var unsaved = false;
$(window).bind('beforeunload', function () {
    if (unsaved) {
        return "You have unsaved changes on this page. Do you want to leave this page and discard your changes or stay on this page?";
    }
});

// Monitor dynamic inputs
$(document).on('change', ':input', function () {
    unsaved = true;
});
$("#create").click(function (e) {
    e.preventDefault();
    unsaved = false;
  
    var ctrl = $(this).attr('data-controller');
    var chekid = $(this).attr('data-id');
    var txt1 = $('label:contains(*)');

    //FOR GENERAL
    for (var i = 0; i <= txt1.length; i++) {
        var lbl = txt1[i];
        var lbl1 = $(lbl).attr('for');


        var lbltxt = $(lbl).text();
        var name = lbltxt.replace('*', "");
        var nameId = name.replace(/\s/g, "");
        $('#' + nameId).remove();
        $('#' + lbl1 + '_validationMessage').remove();



        
       



        var input = $(lbl).next('div').find('input').val();
        var select = $(lbl).next('div').find('select').val();
        var textarea = $(lbl).next('div').find('textarea').val();
        var multiselect = $(lbl).next('div').find('select option:selected').length;


        if (input == '' || input == 0) {
            $(lbl).next('div').find('input').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>' + name + ' is required </span>');
            $(lbl).next('div').find('input').addClass('validation');
        }
        else {
            $(lbl).next('div').find('input').removeClass('validation');
        }
        if (select == '' || select == 0) {
            $(lbl).next('div').find('select').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>' + name + ' is required </span>');
            $(lbl).next('div').find('select').addClass('validation');
        }
        else {
            $(lbl).next('div').find('select').removeClass('validation');
        }
        if (textarea == '') {
            $(lbl).next('div').find('textarea').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>' + name + ' is required </span>');
            $(lbl).next('div').find('textarea').addClass('validation');
        }
        else {
            $(lbl).next('div').find('textarea').removeClass('validation');
        }
        if (multiselect < 1) {
            $(lbl).next('div').find('.multiselect').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>' + name + ' is required </span>');
            $(lbl).next('div').find('.multiselect').addClass('validation');
        }
        else {
            $(lbl).next('div').find('.multiselect').removeClass('validation');
        }
    }




    //FORM EMAIL
    var lblemailss = $('label:contains(Email)');
    for (var j = 0; j <= lblemailss.length; j++) {
        var lbl = lblemailss[j];
        var lblemailtext = $(lbl).text();
        if (lblemailtext != '') {
            var input = $(lbl).next('div').find('input').val();
            if (input != '') {
                var nameId = lblemailtext.replace(/\s/g, "");
                var EMAIL_REGEXP = new RegExp('^[a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,15})$', 'i');
                if (input != '') {
                    if (!(EMAIL_REGEXP.test(input))) {
                        $(lbl).next('div').find('span').remove();
                        $(lbl).next('div').find('input').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>Email is not vliad</span>');
                        $(lbl).next('div').find('input').addClass('emailvalidation');
                    }
                    else {
                        $(lbl).next('div').find('input').removeClass('emailvalidation');
                    }
                }
            }
            else {
                $(lbl).next('div').find('input').removeClass('emailvalidation');
            }
        }
    }



    //FOR URL
    var lblweb = $('label:contains(Website)');
    for (var j = 0; j <= lblweb.length-1; j++) {
        var wb = lblweb[j];
        var lblwebtext = $(wb).text();
        if (lblwebtext != '') {
            var input = $(wb).next('div').find('input').val();
            if (input != '') {
                var nameId = lblwebtext.replace(/\s/g, "");
                var EMAIL_REGEXP = new RegExp('((http\://|https\://|ftp\://)|(www.))+(([a-zA-Z0-9\.-]+\.[a-zA-Z]{2,4})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9%:/-_\?\.~]*)?', 'i');
                if (input != '') {
                    if (!(EMAIL_REGEXP.test(input))) {
                        $(wb).next('div').find('span').remove();
                        $(wb).next('div').find('input').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>URL is not vliad</span>');
                        $(wb).next('div').find('input').addClass('weblvalidation');
                    }
                    else {
                        $(wb).next('div').find('input').removeClass('weblvalidation');
                    }
                }
            }
            else {
                $(wb).next('div').find('input').removeClass('weblvalidation');
            }
        }
    }


    //FOR MOBILE 
    var lblmobile = $('label:contains(Mobile)');
    for (var j = 0; j <= lblmobile.length-1; j++) {
        var tx = lblmobile[j];
        var lblmobiletext = $(tx).text();
        if (lblmobiletext != '') {
            var input = $(tx).next('div').find('input').val();
            if (input != '') {
                var nameId = lblmobiletext.replace(/\s/g, "");
                if (input != '') {
                    var valid = mobilenumber(input);
                    if (valid==false) {
                        $(tx).next('div').find('span').remove();
                        $(tx).next('div').find('input').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>Mobile is not vliad</span>');
                        $(tx).next('div').find('input').addClass('mobilelvalidation');
                    }
                    else {
                        $(tx).next('div').find('input').removeClass('mobilelvalidation');
                    }
                }
            }
            else {
                $(tx).next('div').find('input').removeClass('mobilelvalidation');
            }
        }
    }



    //FOR TELEPHONE 
    var lbltele = $('label:contains(Phone)');
    for (var j = 0; j <= lbltele.length - 1; j++) {
        var tx = lbltele[j];
        var lblmobiletext = $(tx).text();
        if (lblmobiletext != '') {
            var input = $(tx).next('div').find('input').val();
            if (input != '') {
                var nameId = lblmobiletext.replace(/\s/g, "");
                if (input != '') {
                    var valid = mobilenumber(input);
                    if (valid == false) {
                        $(tx).next('div').find('span').remove();
                        $(tx).next('div').find('input').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>Telephone is not vliad</span>');
                        $(tx).next('div').find('input').addClass('televalidation');
                    }
                    else {
                        $(tx).next('div').find('input').removeClass('televalidation');
                    }
                }
            }
            else {
                $(tx).next('div').find('input').removeClass('televalidation');
            }
        }
    }




    //FOR FAX 
    var lbltele = $('label:contains(Fax)');
    for (var j = 0; j <= lbltele.length - 1; j++) {
        var tx = lbltele[j];
        var lblmobiletext = $(tx).text();
        if (lblmobiletext != '') {
            var input = $(tx).next('div').find('input').val();
            if (input != '') {
                var nameId = lblmobiletext.replace(/\s/g, "");
                if (input != '') {
                    var valid = mobilenumber(input);
                    if (valid == false) {
                        $(tx).next('div').find('span').remove();
                        $(tx).next('div').find('input').after('<span class="dirty" style="color:#a94442" id=' + nameId + '>Fax is not vliad</span>');
                        $(tx).next('div').find('input').addClass('faxvalidation');
                    }
                    else {
                        $(tx).next('div').find('input').removeClass('faxvalidation');
                    }
                }
            }
            else {
                $(tx).next('div').find('input').removeClass('faxvalidation');
            }
        }
    }



    //FOR TELEPHONE CODE
    //var phonecodes = $('.telephonecode');
    //for (var s = 0; s <= phonecodes.length-1; s++) {
    //    var code = phonecodes[s];
    //    var iddd = $(code).attr('id');
    //    $('#valid' + iddd).remove();
    //    if ($(code).val() != '') {
    //        var cd = phonecode($(code).val());
    //        if (cd == false) {
    //            $(code).after('<span class="dirty" style="color:#a94442" id=valid' + iddd + '>Telephone Code is not valid</span>');
    //            $(code).addClass('phonecodevalidation');
    //        }
    //    }
    //}

    //FOR TELEPHONE PCO
    var phone = $('.telephone');
    for (var a = 0; a <= phone.length-1; a++) {
        var Tcode = phone[a];
        var idddD = $(Tcode).attr('id');
        $('#valid' + idddD).remove();
        if ($(Tcode).val() != '') {
            var cd = phonenumber($(Tcode).val());
            if (cd == false) {
                $(Tcode).after('<span class="dirty" style="color:#a94442" id=valid' + idddD + '>Telephone is not valid</span>');
                $(Tcode).addClass('phonevalidation');
            }
        }
    }









   
        $('.required').closest('label').next('div').find('.dirty').empty();
   
    $('.required').closest('label').next('div').find('input').removeClass('validation');
    $('.required').closest('label').next('div').find('select').removeClass('validation');
    $('.required').closest('label').next('div').find('textarea').removeClass('validation');

    var v = $('.validation');
    if (v.length > 0) {
        sweetAlert("", "Some mandatory fields left blank!", "error");
        return false;
    }
    var attr = $('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').val();
    if ($('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').val() == '') {
        $('#pan_no').remove();
        $('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').after('<span class="dirty" style="color:#a94442" id=pan_no>PAN is required </span>');
        sweetAlert("", "Some mandatory fields left blank!", "error");
        return false;
    }
    var b = $('.emailvalidation');
    if (b.length > 0) {
        sweetAlert("", "Some field are invalid!", "error");
        return false;
    }
    var w = $('.weblvalidation');
    if (w.length > 0) {
        sweetAlert("", "Some fields are invalid!", "error");
        return false;
    }
    var m = $('.mobilelvalidation');
    if (m.length > 0) {
        sweetAlert("", "Some fields are invalid!", "error");
        return false;
    }
    var cdd = $('.phonecodevalidation');
    if (cdd.length > 0) {
        $('input').removeClass('phonecodevalidation');
        sweetAlert("", "Some fields are invalid!", "error");
        return false;
    }
    var ph = $('.phonevalidation');
    if (ph.length > 0) {
        $('input').removeClass('phonevalidation');
        sweetAlert("", "Some fields are invalid!", "error");
        return false;
    }

    var tele = $('.televalidation');
    if (tele.length > 0) {
        $('input').removeClass('televalidation');
        sweetAlert("", "Some fields are invalid!", "error");
        return false;
    }
    var faxs = $('.faxvalidation');
    if (faxs.length > 0) {
        $('input').removeClass('faxvalidation');
        sweetAlert("", "Some fields are invalid!", "error");
        return false;
    }


     if ($('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').val() != '' || $('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').val() != undefined) {
        var pan = $('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').val();
        if (pan != '') {
            if (pan != undefined) {
                var vl = fnValidatePAN(pan);
                if (vl == false) {
                    $('#pan_no').remove();
                    $('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').after('<span class="dirty" style="color:#a94442" id=pan_no>PAN is not valid</span>');
                    sweetAlert("", "Some fields are invalid!", "error");
                    return false;
                }
            }
        }
    }




     if (($('#registered_telephone_code').val() != "") && (phonecode($('#registered_telephone_code').val()) == false)) {
        $('#validregistered_telephone_code').remove();
        $('#registered_telephone_code').after('<span class="dirty" style="color:#a94442" id=validregistered_telephone_code>Telephone Code is not valid</span>');
        sweetAlert("", "Some fields are invalid!", "error");
    }
     if (($('#corporate_telephone_code').val() != "") && (phonecode($('#corporate_telephone_code').val()) == false)) {
        sweetAlert("", "Some fields are invalid!", "error");
        $('#validcorporate_telephone_code').remove();
        $('#corporate_telephone_code').after('<span class="dirty" style="color:#a94442" id=validcorporate_telephone_code>Telephone Code is not valid</span>');
        sweetAlert("", "Some fields are invalid!", "error");
    }
     if (($('#REGISTERED_TELEPHONE').val() != "") && (phonenumber($('#REGISTERED_TELEPHONE').val()) == false)) {
        sweetAlert("", "Some fields are invalid!", "error");
        $('#validREGISTERED_TELEPHONE').remove();
        $('#REGISTERED_TELEPHONE').after('<span class="dirty" style="color:#a94442" id=validREGISTERED_TELEPHONE>Telephone is not valid</span>');
        sweetAlert("", "Some fields are invalid!", "error");
    }
     if (($('#CORPORATE_TELEPHONE').val() != "") && (phonenumber($('#CORPORATE_TELEPHONE').val()) == false)) {
        sweetAlert("", "Some fields are invalid!", "error");
        $('#validCORPORATE_TELEPHONE').remove();
        $('#CORPORATE_TELEPHONE').after('<span class="dirty" style="color:#a94442" id=validCORPORATE_TELEPHONE>Telephone is not valid</span>');
        sweetAlert("", "Some fields are invalid!", "error");
    }
    else {
         var num = $('.num').val();
         var des = $('.num1').val();
         var des1 = $('.num2').val();
        
        var idd = "";
        if (chekid != undefined) {
            idd = chekid;
        }
        else {
            idd = 0;
        }
        $.ajax({
            url: '/' + ctrl + '/CheckDuplicate',
            type: "GET",
            dataType: "JSON",
            asynch: true,
            data: { st: num, st1 : des, id: idd },
            success: function (output) {
                if (output != 0) {
                    sweetAlert("", ctrl + " already exists !", "error");
                }
                else {
                    document.forms[0].submit();
                }
            }
        });
    }

});



//VALIDATION
validateForm();
function validateForm() {
    $('input,select,textarea').focusout(function () {
        var txt = $(this).attr('name');
            var name = $('label[for=' + txt + ']').text();
            var lbl = $('label:contains(*)[for=' + txt + ']').text();
            var name1 = name.replace('*', "");
            var name2 = name.replace(/\s/g, "");
            var nameId = name2.replace('*', "");
        if (lbl != '') {
            if (lbl != undefined) {
                if ($(this).val() == '' || $(this).val() == 0) {
                    $('#' + nameId).remove();
                    $('#' + txt + '_validationMessage').remove();
                    $(this).after('<span style="color:#a94442" id=' + nameId + '>' + name1 + ' is required </span>');
                    return false;
                }
                else {
                    $('#' + nameId).remove();
                    $('#' + txt + '_validationMessage').remove();
                }
            }
        }




            //  Email
            var lblemailtxt = $('label:contains(Email)[for=' + txt + ']').text();
            if (lblemailtxt != '') {
                var email = $(this).val();
                var nameId = lblemailtxt.replace(/\s/g, "");
                var EMAIL_REGEXP = new RegExp('^[a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,15})$', 'i');
                if (email != '') {
                    if (!(EMAIL_REGEXP.test(email))) {
                        $('#' + nameId).remove();
                        $(this).after('<span style="color:#a94442" id=' + nameId + '>Email is not valid </span>');
                    }
                }
            }



            //FOR URL
            var lblwebsite = $('label:contains(Website)[for=' + txt + ']').text();
            if (lblwebsite != '') {
                var email = $(this).val();
                var nameId = lblwebsite.replace(/\s/g, "");
                var EMAIL_REGEXP = new RegExp('((http\://|https\://|ftp\://)|(www.))+(([a-zA-Z0-9\.-]+\.[a-zA-Z]{2,4})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9%:/-_\?\.~]*)?', 'i');
                if (email != '') {
                    if (!(EMAIL_REGEXP.test(email))) {
                        $('#' + nameId).remove();
                        $(this).after('<span style="color:#a94442" id=' + nameId + '>URL is not valid </span>');
                    }
                }
            }



            //FOR MOBILE
            var lblmobile = $('label:contains(Mobile)[for=' + txt + ']').text();
            if (lblmobile != '') {
                var email = $(this).val();
                var nameId = lblmobile.replace(/\s/g, "");
                var EMAIL_REGEXP = new RegExp('[0-9]{10}|[(]{1}[0-9]{0,3}[) -]{0,3}?[0-9]{3}[ -]{0,4}?[0-9]{4}', 'i');
                if (email != '') {
                    if (!(EMAIL_REGEXP.test(email))) {
                        $('#' + nameId).remove();
                        $(this).after('<span style="color:#a94442" id=' + nameId + '>Mobile Number is not valid </span>');
                    }
                }
            }


            ////FOR PAN
            //if ($('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').val() == '') {
            //    $('#pan_no').remove();
            //    $('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').after('<span class="dirty" style="color:#a94442" id=pan_no>PAN is required </span>');
            //}
            var id = $(this).attr('id');

            if ($(this).hasClass('telephonecode')) {
                if ($(this).val() != '') {
                    var valid = phonecode($(this).val());
                    if (valid == false) {
                        $('#valid' + id).remove();
                        $(this).after('<span style="color:#a94442" id=valid' + id + '>Telephone Code is not valid </span>');
                    }
                }
            }
        
            if ($(this).hasClass('telephone')) {
                if ($(this).val() != '') {
                    var valid = phonenumber($(this).val());
                    if (valid==false) {
                        $('#valid' + id).remove();
                        $(this).after('<span style="color:#a94442" id=valid' + id + '>Telephone is not valid </span>');
                    }
                }
            }
        











        
        


        });
        $('input,select,textarea').focusin(function () {
            var txt = $(this).attr('name');
            var names = $('label[for=' + txt + ']').text();
            var name1 = names.replace('*', "");
            var name2 = names.replace(/\s/g, "");
            var nameId = name2.replace('*', "");
            var id = $(this).attr('id');
            $('#' + nameId).remove();
            $('#valid' + id).remove();
            $('#' + txt + '_validationMessage').remove();
        });
    
    }
    

//$('#ATTRIBUTE_VM_0__ATTRIBUTE_VALUE').focusin(function () {
//    $('#pan_no').remove();
//});


function phonenumber(inputtxt) {
    var phoneno = /^\d{8}$/;
    if (inputtxt != undefined) {
    if (inputtxt.match(phoneno)) {
        return true;
    }
    else {
        return false;

    }
}
}
function mobilenumber(inputtxt) {
    var phoneno = /^\d{10}$/;
    if (inputtxt != undefined) {
    if (inputtxt.match(phoneno)) {
        return true;
    }
    else {
        return false;

    }
    }
}

function phonecode(inputtxt) {
    var phoneno = /^\d{3}$/;
    if (inputtxt != undefined) {
    if (inputtxt.match(phoneno)) {
        return true;
    }
    else {
        return false;
    }
}
}

function fnValidatePAN(Obj) {
    if (Obj != "") {
    if(Obj!=undefined){
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