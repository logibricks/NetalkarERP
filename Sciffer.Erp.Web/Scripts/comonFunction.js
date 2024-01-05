
    $(document).ready(function () {
        $('.mickey').multiselect({
            includeSelectAllOption: true,
            enableFiltering: true,
            maxHeight: '200',
            enableCaseInsensitiveFiltering: true,
            allowClear: true,
            maximumResultsForSearch: 1,
            numberDisplayed: 0,
        });
    });





jQuery.setCurrentTime = function (currentDate) {
    var d = currentDate,
               h = d.getHours(),
               m = d.getMinutes();
    if (h < 10) h = '0' + h;
    if (m < 10) m = '0' + m;
    return h + ':' + m;
}

jQuery.formatDate = function (date) {
    var dt = new Date(date);
    return (dt.getDate() + "-" + (parseInt(dt.getMonth()) + 1) + "-" + dt.getFullYear())
}


////Validate all blank field on line item grid pasing control id in this function
//jQuery.validateBlankField = function (controlId1,controlId2,controlId3,controlId4,controlId5,controlId6,controlId7,controlId8,controlId9,controlId10,controlId11,controlId12,controlId13,controlId14,controlId15,controlId16,controlId17,controlId18,controlId19,controlId20) {
//    for (var i = 1; i <= 20; i++) {
//        $('#line_item_validation' + i).remove();
//            var thisControlValue = $(controlId + '' + i).val();
//            var thisControlValidatioMSG = $(controlId + '' + i).attr('data-validation_msg');
//            if (thisControl == '') {
//                $(controlId + '' + i).after('<span class="line_item_blank_validation" style="color:#a94442" id=line_item_validation' + i + '>'+thisControlValidatioMSG+' can not be left blank </span>');
//            }
//        }
  
//    var blank_field = $('.line_item_validation');
//    if (blank_field.length > 0) {
//        return false;
//    }
//}



////validate comparison field pasing two control id
//jQuery.validateComparisonField = function (greaterControlId, lowerControlId) {
//    $(line_item_comparison_validation + '' + lowerControlId).remove();
//    var thisGreaterControlMSG = $(greaterControlId).attr('data-validation_msg');
//    var thisLowerControlMSG = $(lowerControlId).attr('data-validation_msg');
//    var greater = parseFloat($(greaterControlId).val());
//    var lower = parseFloat($(lowerControlId).val());
//    if (lower > greater) {
//        $(lowerControlId).after('<span class="line_item_comparison_validation" style="color:#a94442" id=line_item_comparison_validation' + lowerControlId + '>' + thisLowerControlMSG + ' can not be greater than ' + thisGreaterControlMSG + ' </span>');
//        return false;
//    }
//}

    


//    jQuery.getfunctionName = function (functionName) {
//        jQuery.getControllerParameters = function (ctrlparameter1, ctrlparameter2, ctrlparameter3, ctrlparameter4, ctrlparameter5, ctrlparameter6, ctrlparameter7, ctrlparameter8, ctrlparameter9, ctrlparameter10) {
//            jQuery.pageParameters = function (pageparameter1, pageparameter2, pageparameter3, pageparameter4, pageparameter5, pageparameter6, pageparameter7, pageparameter8, pageparameter9, pageparameter10) {
//                var dt = {}
//                dt[ctrlparameter1] = pageparameter1;
//                dt[ctrlparameter2] = pageparameter2;
//                dt[ctrlparameter3] = pageparameter3;
//                dt[ctrlparameter4] = pageparameter4;
//                dt[ctrlparameter5] = pageparameter5;
//                dt[ctrlparameter6] = pageparameter6;
//                dt[ctrlparameter7] = pageparameter7;
//                dt[ctrlparameter8] = pageparameter8;
//                dt[ctrlparameter9] = pageparameter9;
//                dt[ctrlparameter10] = pageparameter10;
//                $.ajax({
//                    url: '/report/' + functionName,
//                    method: "get",
//                    cache: false,
//                    data: dt,
//                    success: function (data) {
//                        $(".loading").hide();
//                        $('#searchresult').empty().append(data);
//                    },
//                    error: function (http) {
//                        console.log(http);
//                    }
//                });
//            }
//        }
//    }


