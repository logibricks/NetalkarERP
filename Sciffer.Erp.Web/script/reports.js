
        $(document).ready(function () {
            try {
                $(".loading").hide();
                $('.mickey').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    maxHeight: '200',
                    enableCaseInsensitiveFiltering: true,
                    allowClear: true,
                    maximumResultsForSearch: 1,
                    numberDisplayed: 0,
                });
                $(".mickey").multiselect('selectAll', false);
                $('.mickey').multiselect('updateButtonText');
                if (document.getElementById('fromDate') != null) {
                 document.getElementById('fromDate').valueAsDate = new Date();
                }
                 if( document.getElementById('toDate')!=null){
                 document.getElementById('toDate').valueAsDate = new Date();
                 }
                 if (document.getElementById('posting_dates') != null) {
                     document.getElementById('posting_dates').valueAsDate = new Date();
                 }
              
            } catch (msg) { console.log(msg.message) }
        });
        function GetStorageLocation(id) {
            if (id == "") {
                id = 0;
            }
            var st;
            st = "";
            $(".sloc_id").empty();
            $.ajax({
                url: '/Generic/GetStorageLocation',
                type: "GET",
                dataType: "JSON",
                data: { id: id },
                success: function (id) {
                    $.each(id, function (i, cycle) {
                        st += "<option value=" + cycle.storage_location_id + ">" + cycle.storage_location_name + "</option>";
                    });
                    $(".sloc_id").html(st).show();
                    $(".sloc_id").multiselect('rebuild');
                }
            });
        }
        function DateCheck() {
            try {
             if( document.getElementById('fromDate')!=null){
                 var StartDate = document.getElementById('fromDate').value;
                 var sDate = new Date(StartDate);
                }
                if( document.getElementById('toDate')!=null){
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }                
                
               
                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                 if( document.getElementById('fromDate')!=null){
                  var fromDate = $("#fromDate").val();
                }
                if( document.getElementById('toDate')!=null){
                 var toDate = $("#toDate").val();
                }                
                if ($('.customer_category_id').val() != undefined) {
                    var cus_category = '';
                    if ($('.customer_category_id').next('div').find('button').find('span').text().includes("All")) {
                        cus_category = -1;
                    }
                    else {
                        if ($('.customer_category_id :selected').length > 0) {
                            var selectedCustomeCategory = [];
                            $('.customer_category_id :selected').each(function (i, selected) {
                                selectedCustomeCategory[i] = $(selected).val();
                            });
                            cus_category = selectedCustomeCategory + "";
                        }
                    }
                }
                if ($('.customer_priority_id').val() != undefined) {
                    var cus_priority = '';
                    if ($('.customer_priority_id').next('div').find('button').find('span').text().includes("All")) {
                        cus_priority = -1;
                    }
                    else {
                        if ($('.customer_priority_id :selected').length > 0) {
                            var selectedCustomePriority = [];
                            $('.customer_priority_id :selected').each(function (i, selected) {
                                selectedCustomePriority[i] = $(selected).val();
                            });
                            cus_priority = selectedCustomePriority + "";
                        }
                    }
                }
                if ($('.sales_rm').val() != undefined) {
                    var SalesRM = '';
                    if ($('.sales_rm').next('div').find('button').find('span').text().includes("All")) {
                        SalesRM = -1;
                    }
                    else {
                        if ($('.sales_rm :selected').length > 0) {
                            var selectedSalesRM = [];
                            $('.sales_rm :selected').each(function (i, selected) {
                                selectedSalesRM[i] = $(selected).val();
                            });
                            SalesRM = selectedSalesRM + "";
                        }
                    }
                }
                if ($('.territory_id').val() != undefined) {
                    var territories = '';
                    if ($('.territory_id').next('div').find('button').find('span').text().includes("All")) {
                        territories = -1;
                    }
                    else {
                        if ($('.territory_id :selected').length > 0) {
                            var selectedTerritory = [];
                            $('.territory_id :selected').each(function (i, selected) {
                                selectedTerritory[i] = $(selected).val();
                            });
                            territories = selectedTerritory + "";
                        }
                    }
                }
                if ($('.currency_id').val() != undefined) {
                    var Currencies = '';
                    if ($('.currency_id').next('div').find('button').find('span').text().includes("All")) {
                        Currencies = -1;
                    }
                    else {
                        if ($('.currency_id :selected').length > 0) {
                            var selectedCurrency = [];
                            $('.currency_id :selected').each(function (i, selected) {
                                selectedCurrency[i] = $(selected).val();
                            });
                            Currencies = selectedCurrency + "";
                        }
                    }
                }
                if ($('.plant_id').val() != undefined) {
                    var Plants = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        Plants = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedPlant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedPlant[i] = $(selected).val();
                            });
                            var Plants = selectedPlant + "";
                        }
                    }
                }
                if ($('.business_unit_id').val() != undefined) {
                    var BusinessUnits = '';
                    if ($('.business_unit_id').next('div').find('button').find('span').text().includes("All")) {
                        BusinessUnits = -1;
                    }
                    else {
                        if ($('.business_unit_id :selected').length > 0) {
                            var selectedBusinessUnit = [];
                            $('.business_unit_id :selected').each(function (i, selected) {
                                selectedBusinessUnit[i] = $(selected).val();
                            });
                            BusinessUnits = selectedBusinessUnit + "";
                        }
                    }
                }
                if ($('.item_id').val() != undefined) {
                    var itemss = '';
                    if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                        itemss = -1;
                    }
                    else {
                        if ($('.item_id :selected').length > 0) {
                            var selecteditemss = [];
                            $('.item_id :selected').each(function (i, selected) {
                                selecteditemss[i] = $(selected).val();
                            });
                            itemss = selecteditemss + "";
                        }
                    }
                }
                if ($('.customer_id').val() != undefined) {
                    var customer = '';
                    if ($('.customer_id').next('div').find('button').find('span').text().includes("All")) {
                        customer = -1;
                    }
                    else {
                        if ($('.customer_id :selected').length > 0) {
                            var selectedcustomer = [];
                            $('.customer_id :selected').each(function (i, selected) {
                                selectedcustomer[i] = $(selected).val();
                            });
                            customer = selectedcustomer + "";
                        }
                    }
                }
                var item_priority = '';
                var item_group = '';
                var item_categories = '';
                var status = '';
                var created_bys = '';
                var source = '';
                var posting_dates = '';
                var item_service = '';
                var partial_v = $('#partial').val();
                $(".loading").show();
                $.ajax({
                    url: '/SalesReport/Get_Partial',
                    method: "Post",
                     cache: false,
                data: {
                    entity:entity,customer_category_id:cus_category,customer_priority_id:cus_priority,item_priority_id:item_priority,customer_id:customer,from_date:fromDate,
                    to_date: toDate, item_group_id: item_group, territory_id: territories, item_category: item_categories, status_id: status, created_by: created_bys, plant_id: Plants,
                    source_id: source, posting_date: posting_dates, currency_id: Currencies, business_unit_id: BusinessUnits, item_service_id: item_service, item_id: itemss, sales_rm: SalesRM,partial_v:partial_v

                },
                success: function (data) {
                    $(".loading").hide();
                    $('#searchresult').empty().append(data);
                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText + 'message   ' + error.message);
                }
            });



        } catch (msg) { console.log(msg.message) }
        };
       
        function OnToolbarClick1(args) {
            try{
            if (args.itemName.indexOf("Export") > -1) {
                var entity = $('#entity').val();
                var fromDate = $("#fromDate").val();
                var toDate = $("#toDate").val();
                if ($('.customer_category_id').val() != undefined) {
                    var cus_category = '';
                    if ($('.customer_category_id').next('div').find('button').find('span').text().includes("All")) {
                        cus_category = -1;
                    }
                    else {
                        if ($('.customer_category_id :selected').length > 0) {
                            var selectedCustomeCategory = [];
                            $('.customer_category_id :selected').each(function (i, selected) {
                                selectedCustomeCategory[i] = $(selected).val();
                            });
                            cus_category = selectedCustomeCategory + "";
                        }
                    }
                }
                if ($('.customer_priority_id').val() != undefined) {
                    var cus_priority = '';
                    if ($('.customer_priority_id').next('div').find('button').find('span').text().includes("All")) {
                        cus_priority = -1;
                    }
                    else {
                        if ($('.customer_priority_id :selected').length > 0) {
                            var selectedCustomePriority = [];
                            $('.customer_priority_id :selected').each(function (i, selected) {
                                selectedCustomePriority[i] = $(selected).val();
                            });
                            cus_priority = selectedCustomePriority + "";
                        }
                    }
                }
                if ($('.sales_rm').val() != undefined) {
                    var SalesRM = '';
                    if ($('.sales_rm').next('div').find('button').find('span').text().includes("All")) {
                        SalesRM = -1;
                    }
                    else {
                        if ($('.sales_rm :selected').length > 0) {
                            var selectedSalesRM = [];
                            $('.sales_rm :selected').each(function (i, selected) {
                                selectedSalesRM[i] = $(selected).val();
                            });
                            SalesRM = selectedSalesRM + "";
                        }
                    }
                }
                if ($('.territory_id').val() != undefined) {
                    var territories = '';
                    if ($('.territory_id').next('div').find('button').find('span').text().includes("All")) {
                        territories = -1;
                    }
                    else {
                        if ($('.territory_id :selected').length > 0) {
                            var selectedTerritory = [];
                            $('.territory_id :selected').each(function (i, selected) {
                                selectedTerritory[i] = $(selected).val();
                            });
                            territories = selectedTerritory + "";
                        }
                    }
                }
                if ($('.currency_id').val() != undefined) {
                    var Currencies = '';
                    if ($('.currency_id').next('div').find('button').find('span').text().includes("All")) {
                        Currencies = -1;
                    }
                    else {
                        if ($('.currency_id :selected').length > 0) {
                            var selectedCurrency = [];
                            $('.currency_id :selected').each(function (i, selected) {
                                selectedCurrency[i] = $(selected).val();
                            });
                            Currencies = selectedCurrency + "";
                        }
                    }
                }
                if ($('.plant_id').val() != undefined) {
                    var Plants = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        Plants = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedPlant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedPlant[i] = $(selected).val();
                            });
                            var Plants = selectedPlant + "";
                        }
                    }
                }
                if ($('.business_unit_id').val() != undefined) {
                    var BusinessUnits = '';
                    if ($('.business_unit_id').next('div').find('button').find('span').text().includes("All")) {
                        BusinessUnits = -1;
                    }
                    else {
                        if ($('.business_unit_id :selected').length > 0) {
                            var selectedBusinessUnit = [];
                            $('.business_unit_id :selected').each(function (i, selected) {
                                selectedBusinessUnit[i] = $(selected).val();
                            });
                            BusinessUnits = selectedBusinessUnit + "";
                        }
                    }
                }
                if ($('.item_id').val() != undefined) {
                    var itemss = '';
                    if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                        itemss = -1;
                    }
                    else {
                        if ($('.item_id :selected').length > 0) {
                            var selecteditemss = [];
                            $('.item_id :selected').each(function (i, selected) {
                                selecteditemss[i] = $(selected).val();
                            });
                            itemss = selecteditemss + "";
                        }
                    }
                }
                if ($('.customer_id').val() != undefined) {
                    var customer = '';
                    if ($('.customer_id').next('div').find('button').find('span').text().includes("All")) {
                        customer = -1;
                    }
                    else {
                        if ($('.customer_id :selected').length > 0) {
                            var selectedcustomer = [];
                            $('.customer_id :selected').each(function (i, selected) {
                                selectedcustomer[i] = $(selected).val();
                            });
                            customer = selectedcustomer + "";
                        }
                    }
                }
                var item_priority = '';
                var item_group = '';
                var item_categories = '';
                var status = '';
                var created_bys = '';
                var source = '';
                var posting_dates = '';
                var item_service = '';
                this.model["from_date"] = $("#fromDate").val();
                this.model["to_date"] = $("#toDate").val();
                this.model["plant_id"] = Plants;
                this.model["customer_category_id"] = cus_category;
                this.model["sales_rm"] = SalesRM;
                this.model["territory_id"] = territories;
                this.model["customer_priority_id"] = cus_priority;
                this.model["currency_id"] = Currencies;
                this.model["business_unit_id"] = BusinessUnits;
                this.model["item_id"] = itemss;
                this.model["customer_id"] = customer;
                this.model["entity"] = entity;                
            }
            } catch (msg) { console.log(msg.message) }
        }

//FOR PURCHASE REPORT
        function DateCheckp() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }


                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }
                if ($('.vendor_category_id').val() != undefined) {
                    var vendor_category = '';
                    if ($('.vendor_category_id').next('div').find('button').find('span').text().includes("All")) {
                        vendor_category = -1;
                    }
                    else {
                        if ($('.vendor_category_id :selected').length > 0) {
                            var selectedvendor_category = [];
                            $('.vendor_category_id :selected').each(function (i, selected) {
                                selectedvendor_category[i] = $(selected).val();
                            });
                            vendor_category = selectedvendor_category + "";
                        }
                    }
                }
                if ($('.vendor_priority_id').val() != undefined) {
                    var vendor_priority = '';
                    if ($('.vendor_priority_id').next('div').find('button').find('span').text().includes("All")) {
                        vendor_priority = -1;
                    }
                    else {
                        if ($('.vendor_priority_id :selected').length > 0) {
                            var selectedvendor_priority = [];
                            $('.vendor_priority_id :selected').each(function (i, selected) {
                                selectedvendor_priority[i] = $(selected).val();
                            });
                            vendor_priority = selectedvendor_priority + "";
                        }
                    }
                }
                if ($('.item_priority_id').val() != undefined) {
                    var item_priority = '';
                    if ($('.item_priority_id').next('div').find('button').find('span').text().includes("All")) {
                        item_priority = -1;
                    }
                    else {
                        if ($('.item_priority_id :selected').length > 0) {
                            var selecteditem_priority = [];
                            $('.item_priority_id :selected').each(function (i, selected) {
                                selecteditem_priority[i] = $(selected).val();
                            });
                            item_priority = selecteditem_priority + "";
                        }
                    }
                }
                if ($('.vendor_id').val() != undefined) {
                    var vendor = '';
                    if ($('.vendor_id').next('div').find('button').find('span').text().includes("All")) {
                        vendor = -1;
                    }
                    else {
                        if ($('.vendor_id :selected').length > 0) {
                            var selectedvendor = [];
                            $('.vendor_id :selected').each(function (i, selected) {
                                selectedvendor[i] = $(selected).val();
                            });
                            vendor = selectedvendor + "";
                        }
                    }
                }
                if ($('.item_group_id').val() != undefined) {
                    var item_group = '';
                    if ($('.item_group_id').next('div').find('button').find('span').text().includes("All")) {
                        item_group = -1;
                    }
                    else {
                        if ($('.item_group_id :selected').length > 0) {
                            var selecteditem_group = [];
                            $('.item_group_id :selected').each(function (i, selected) {
                                selecteditem_group[i] = $(selected).val();
                            });
                            item_group = selecteditem_group + "";
                        }
                    }
                }
                if ($('.territory_id').val() != undefined) {
                    var territory = '';
                    if ($('.territory_id').next('div').find('button').find('span').text().includes("All")) {
                        territory = -1;
                    }
                    else {
                        if ($('.territory_id :selected').length > 0) {
                            var selectedterritory = [];
                            $('.territory_id :selected').each(function (i, selected) {
                                selectedterritory[i] = $(selected).val();
                            });
                            var territory = selectedterritory + "";
                        }
                    }
                }
                if ($('.item_category').val() != undefined) {
                    var item_categories = '';
                    if ($('.item_category').next('div').find('button').find('span').text().includes("All")) {
                        item_categories = -1;
                    }
                    else {
                        if ($('.item_category :selected').length > 0) {
                            var selecteditem_categories = [];
                            $('.item_category :selected').each(function (i, selected) {
                                selecteditem_categories[i] = $(selected).val();
                            });
                            item_categories = selecteditem_categories + "";
                        }
                    }
                }
                if ($('.status_id').val() != undefined) {
                    var status = '';
                    if ($('.status_id').next('div').find('button').find('span').text().includes("All")) {
                        status = -1;
                    }
                    else {
                        if ($('.status_id :selected').length > 0) {
                            var selectedstatus = [];
                            $('.status_id :selected').each(function (i, selected) {
                                selectedstatus[i] = $(selected).val();
                            });
                            status = selectedstatus + "";
                        }
                    }
                }
                if ($('.plant_id').val() != undefined) {
                    var plant = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        plant = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedplant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedplant[i] = $(selected).val();
                            });
                            plant = selectedplant + "";
                        }
                    }
                }
                if ($('.source_id').val() != undefined) {
                    var source = '';
                    if ($('.source_id').next('div').find('button').find('span').text().includes("All")) {
                        source = -1;
                    }
                    else {
                        if ($('.source_id :selected').length > 0) {
                            var selectedsource = [];
                            $('.source_id :selected').each(function (i, selected) {
                                selectedsource[i] = $(selected).val();
                            });
                            source = selectedsource + "";
                        }
                    }
                }              
                if ($('.currency_id').val() != undefined) {
                    var currency = '';
                    if ($('.currency_id').next('div').find('button').find('span').text().includes("All")) {
                        currency = -1;
                    }
                    else {
                        if ($('.currency_id :selected').length > 0) {
                            var selectedcurrency = [];
                            $('.currency_id :selected').each(function (i, selected) {
                                selectedcurrency[i] = $(selected).val();
                            });
                            currency = selectedcurrency + "";
                        }
                    }
                }
                if ($('.business_unit_id').val() != undefined) {
                    var business_unit = '';
                    if ($('.business_unit_id').next('div').find('button').find('span').text().includes("All")) {
                        business_unit = -1;
                    }
                    else {
                        if ($('.business_unit_id :selected').length > 0) {
                            var selectedbusiness_unit = [];
                            $('.business_unit_id :selected').each(function (i, selected) {
                                selectedbusiness_unit[i] = $(selected).val();
                            });
                            business_unit = selectedbusiness_unit + "";
                        }
                    }
                }
                if ($('.item_service_id').val() != undefined) {
                    var item_service = '';
                    if ($('.item_service_id').next('div').find('button').find('span').text().includes("All")) {
                        item_service = -1;
                    }
                    else {
                        if ($('.item_service_id :selected').length > 0) {
                            var selecteditem_service = [];
                            $('.item_service_id :selected').each(function (i, selected) {
                                selecteditem_service[i] = $(selected).val();
                            });
                            item_service = selecteditem_service + "";
                        }
                    }
                }
                if ($('.item_id').val() != undefined) {
                    var item = '';
                    if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                        item = -1;
                    }
                    else {
                        if ($('.item_id :selected').length > 0) {
                            var selecteditem_id = [];
                            $('.item_id :selected').each(function (i, selected) {
                                selecteditem_id[i] = $(selected).val();
                            });
                            item = selecteditem_id + "";
                        }
                    }
                }
                var posting_dates = '';
                if (document.getElementById('posting_dates') != null) {
                    var posting_dates = document.getElementById('posting_dates').value;
                }
                $(".loading").show();
                var partial_v = $('#partial').val();
                $.ajax({
                    url: '/PurchaseReport/Get_Partial',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, vendor_category_id: vendor_category, vendor_priority_id: vendor_priority, item_priority_id: item_priority, vendor_id: vendor,
                        from_date:fromDate,to_date:toDate,
                        item_group_id: item_group, territory_id: territory, item_category: item_categories, status_id: status, plant_id: plant, source_id: source,
                        posting_date: posting_dates, currency_id: currency, business_unit_id: business_unit, item_service_id: item_service,item_id: item, partial_v: partial_v

                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });



            } catch (msg) { console.log(msg.message) }
        };

        function OnToolbarClickp(args) {
            try {
                if (args.itemName.indexOf("Export") > -1) {
                    let entity = $('#entity').val();
                    var fromDate = $("#fromDate").val();
                    var toDate = $("#toDate").val();
                    if ($('.vendor_category_id').val() != undefined) {
                        var vendor_category = '';
                        if ($('.vendor_category_id').next('div').find('button').find('span').text().includes("All")) {
                            vendor_category = -1;
                        }
                        else {
                            if ($('.vendor_category_id :selected').length > 0) {
                                var selectedvendor_category = [];
                                $('.vendor_category_id :selected').each(function (i, selected) {
                                    selectedvendor_category[i] = $(selected).val();
                                });
                                vendor_category = selectedvendor_category + "";
                            }
                        }
                    }
                    if ($('.vendor_priority_id').val() != undefined) {
                        var vendor_priority = '';
                        if ($('.vendor_priority_id').next('div').find('button').find('span').text().includes("All")) {
                            vendor_priority = -1;
                        }
                        else {
                            if ($('.vendor_priority_id :selected').length > 0) {
                                var selectedvendor_priority = [];
                                $('.vendor_priority_id :selected').each(function (i, selected) {
                                    selectedvendor_priority[i] = $(selected).val();
                                });
                                vendor_priority = selectedvendor_priority + "";
                            }
                        }
                    }
                    if ($('.item_priority_id').val() != undefined) {
                        var item_priority = '';
                        if ($('.item_priority_id').next('div').find('button').find('span').text().includes("All")) {
                            item_priority = -1;
                        }
                        else {
                            if ($('.item_priority_id :selected').length > 0) {
                                var selecteditem_priority = [];
                                $('.item_priority_id :selected').each(function (i, selected) {
                                    selecteditem_priority[i] = $(selected).val();
                                });
                                item_priority = selecteditem_priority + "";
                            }
                        }
                    }
                    if ($('.vendor_id').val() != undefined) {
                        var vendor = '';
                        if ($('.vendor_id').next('div').find('button').find('span').text().includes("All")) {
                            vendor = -1;
                        }
                        else {
                            if ($('.vendor_id :selected').length > 0) {
                                var selectedvendor = [];
                                $('.vendor_id :selected').each(function (i, selected) {
                                    selectedvendor[i] = $(selected).val();
                                });
                                vendor = selectedvendor + "";
                            }
                        }
                    }
                    if ($('.item_group_id').val() != undefined) {
                        var item_group = '';
                        if ($('.item_group_id').next('div').find('button').find('span').text().includes("All")) {
                            item_group = -1;
                        }
                        else {
                            if ($('.item_group_id :selected').length > 0) {
                                var selecteditem_group = [];
                                $('.item_group_id :selected').each(function (i, selected) {
                                    selecteditem_group[i] = $(selected).val();
                                });
                                item_group = selecteditem_group + "";
                            }
                        }
                    }
                    if ($('.territory_id').val() != undefined) {
                        var territory = '';
                        if ($('.territory_id').next('div').find('button').find('span').text().includes("All")) {
                            territory = -1;
                        }
                        else {
                            if ($('.territory_id :selected').length > 0) {
                                var selectedterritory = [];
                                $('.territory_id :selected').each(function (i, selected) {
                                    selectedterritory[i] = $(selected).val();
                                });
                                var territory = selectedterritory + "";
                            }
                        }
                    }
                    if ($('.item_category').val() != undefined) {
                        var item_categories = '';
                        if ($('.item_category').next('div').find('button').find('span').text().includes("All")) {
                            item_categories = -1;
                        }
                        else {
                            if ($('.item_category :selected').length > 0) {
                                var selecteditem_categories = [];
                                $('.item_category :selected').each(function (i, selected) {
                                    selecteditem_categories[i] = $(selected).val();
                                });
                                item_categories = selecteditem_categories + "";
                            }
                        }
                    }
                    if ($('.status_id').val() != undefined) {
                        var status = '';
                        if ($('.status_id').next('div').find('button').find('span').text().includes("All")) {
                            status = -1;
                        }
                        else {
                            if ($('.status_id :selected').length > 0) {
                                var selectedstatus = [];
                                $('.status_id :selected').each(function (i, selected) {
                                    selectedstatus[i] = $(selected).val();
                                });
                                status = selectedstatus + "";
                            }
                        }
                    }
                    if ($('.plant_id').val() != undefined) {
                        var plant = '';
                        if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                            plant = -1;
                        }
                        else {
                            if ($('.plant_id :selected').length > 0) {
                                var selectedplant = [];
                                $('.plant_id :selected').each(function (i, selected) {
                                    selectedplant[i] = $(selected).val();
                                });
                                plant = selectedplant + "";
                            }
                        }
                    }
                    if ($('.source_id').val() != undefined) {
                        var source = '';
                        if ($('.source_id').next('div').find('button').find('span').text().includes("All")) {
                            source = -1;
                        }
                        else {
                            if ($('.source_id :selected').length > 0) {
                                var selectedsource = [];
                                $('.source_id :selected').each(function (i, selected) {
                                    selectedsource[i] = $(selected).val();
                                });
                                source = selectedsource + "";
                            }
                        }
                    }                   
                    if ($('.currency_id').val() != undefined) {
                        var currency = '';
                        if ($('.currency_id').next('div').find('button').find('span').text().includes("All")) {
                            currency = -1;
                        }
                        else {
                            if ($('.currency_id :selected').length > 0) {
                                var selectedcurrency = [];
                                $('.currency_id :selected').each(function (i, selected) {
                                    selectedcurrency[i] = $(selected).val();
                                });
                                currency = selectedcurrency + "";
                            }
                        }
                    }
                    if ($('.business_unit_id').val() != undefined) {
                        var business_unit = '';
                        if ($('.business_unit_id').next('div').find('button').find('span').text().includes("All")) {
                            business_unit = -1;
                        }
                        else {
                            if ($('.business_unit_id :selected').length > 0) {
                                var selectedbusiness_unit = [];
                                $('.business_unit_id :selected').each(function (i, selected) {
                                    selectedbusiness_unit[i] = $(selected).val();
                                });
                                business_unit = selectedbusiness_unit + "";
                            }
                        }
                    }
                    if ($('.item_service_id').val() != undefined) {
                        var item_service = '';
                        if ($('.item_service_id').next('div').find('button').find('span').text().includes("All")) {
                            item_service = -1;
                        }
                        else {
                            if ($('.item_service_id :selected').length > 0) {
                                var selecteditem_service = [];
                                $('.item_service_id :selected').each(function (i, selected) {
                                    selecteditem_service[i] = $(selected).val();
                                });
                                item_service = selecteditem_service + "";
                            }
                        }
                    }
                    if ($('.item_id').val() != undefined) {
                    var item = '';
                    if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                        item = -1;
                    }
                    else {
                        if ($('.item_id :selected').length > 0) {
                            var selecteditem_id = [];
                            $('.item_id :selected').each(function (i, selected) {
                                selecteditem_id[i] = $(selected).val();
                            });
                            item = selecteditem_id + "";
                        }
                    }
                }
                    var posting_dates = '';
                    if (document.getElementById('posting_dates') != null) {
                        var posting_dates = document.getElementById('posting_dates').value;
                    }
                    this.model["from_date"] = $("#fromDate").val();
                    this.model["to_date"] = $("#toDate").val();
                    this.model["vendor_category_id"] = vendor_category;
                    this.model["vendor_priority_id"] = vendor_priority;
                    this.model["item_priority_id"] = item_priority;
                    this.model["vendor_id"] = vendor;
                    this.model["item_group_id"] = item_group;
                    this.model["territory_id"] = territory;
                    this.model["item_category"] = item_categories;
                    this.model["status_id"] = status;
                    this.model["plant_id"] = plant;
                    this.model["source_id"] = source;
                    this.model["posting_date"] = posting_dates;
                    this.model["currency_id"] = currency;
                    this.model["business_unit_id"] = business_unit;
                    this.model["item_service_id"] = item_service;
                    this.model["item_id"] = item;
                    this.model["entity"] = entity;
                }
            } catch (msg) { console.log(msg.message) }
        }

//FOR INVENTORY REPORT
        function DateChecki() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }


                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }
                if ($('.sloc_id').val() != undefined) {
                    var sloc = '';
                    if ($('.sloc_id').next('div').find('button').find('span').text().includes("All")) {
                        sloc = -1;
                    }
                    else {
                        if ($('.sloc_id :selected').length > 0) {
                            var selectedsloc = [];
                            $('.sloc_id :selected').each(function (i, selected) {
                                selectedsloc[i] = $(selected).val();
                            });
                            sloc = selectedsloc + "";
                        }
                    }
                }
                if ($('.bucket_id').val() != undefined) {
                    var bucket = '';
                    if ($('.bucket_id').next('div').find('button').find('span').text().includes("All")) {
                        bucket = -1;
                    }
                    else {
                        if ($('.bucket_id :selected').length > 0) {
                            var selectedbucket = [];
                            $('.bucket_id :selected').each(function (i, selected) {
                                selectedbucket[i] = $(selected).val();
                            });
                            bucket = selectedbucket + "";
                        }
                    }
                }
                if ($('.item_id').val() != undefined) {
                    var item = '';
                    if(entity=="getinventoryledgerdetailed" || entity=="getinventoryledgersummary")
                    {
                      item=$('.item_id').val();
                    }  
                    else
                    {
                    if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                        item = -1;
                    }
                    else {
                        if ($('.item_id :selected').length > 0) {
                            var selecteditem = [];
                            $('.item_id :selected').each(function (i, selected) {
                                selecteditem[i] = $(selected).val();
                            });
                            item = selecteditem + "";
                        }
                    }
                    }
                }              
                if ($('.reason_code_id').val() != undefined) {
                    var reason_code = '';
                    if (!$('.reason_code_id').attr('multiple')) {
                        reason_code = $('.reason_code_id').val();
                    }
                    if ($('.reason_code_id').next('div').find('button').find('span').text().includes("All")) {
                        reason_code = -1;
                    }
                    else {
                        if ($('.reason_code_id :selected').length > 0) {
                            var selectedreason_code = [];
                            $('.reason_code_id :selected').each(function (i, selected) {
                                selectedreason_code[i] = $(selected).val();
                            });
                            reason_code = selectedreason_code + "";
                        }
                    }
                }
               
                if ($('.sloc_id1').val() != undefined) {
                    var sloc1 = '';
                    if ($('.sloc_id1').next('div').find('button').find('span').text().includes("All")) {
                        sloc1 = -1;
                    }
                    else {
                        if ($('.sloc_id1 :selected').length > 0) {
                            var selectedsloc1 = [];
                            $('.sloc_id1 :selected').each(function (i, selected) {
                                selectedsloc1[i] = $(selected).val();
                            });
                            var sloc1 = selectedsloc1 + "";
                        }
                    }
                }
                if ($('.item_category_id').val() != undefined) {
                    var item_categories = '';
                    if ($('.item_category_id').next('div').find('button').find('span').text().includes("All")) {
                        item_categories = -1;
                    }
                    else {
                        if ($('.item_category_id :selected').length > 0) {
                            var selecteditem_categories = [];
                            $('.item_category_id :selected').each(function (i, selected) {
                                selecteditem_categories[i] = $(selected).val();
                            });
                            item_categories = selecteditem_categories + "";
                        }
                    }
                }
                if ($('.bucket_id1').val() != undefined) {
                    var bucket1 = '';
                    if ($('.bucket_id1').next('div').find('button').find('span').text().includes("All")) {
                        bucket1 = -1;
                    }
                    else {
                        if ($('.bucket_id1 :selected').length > 0) {
                            var selectedbucket1 = [];
                            $('.bucket_id1 :selected').each(function (i, selected) {
                                selectedbucket1[i] = $(selected).val();
                            });
                            bucket1 = selectedbucket1 + "";
                        }
                    }
                }
                if ($('.plant_id').val() != undefined) {
                    var plant = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        plant = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedplant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedplant[i] = $(selected).val();
                            });
                            plant = selectedplant + "";
                        }
                    }
                }
                if ($('.reason_id').val() != undefined) {
                    var reason = '';
                    if ($('.reason_id').next('div').find('button').find('span').text().includes("All")) {
                        reason = -1;
                    }
                    else {
                        if ($('.reason_id :selected').length > 0) {
                            var selectedreason = [];
                            $('.reason_id :selected').each(function (i, selected) {
                                selectedreason[i] = $(selected).val();
                            });
                            reason = selectedreason + "";
                        }
                    }
                }
                var partial = $('#partial').val();
                $(".loading").show();
                $.ajax({
                    url: '/InventoryReport/Get_Partial',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, from_date: fromDate, to_date: toDate, plant_id:plant,sloc_id:sloc,bucket_id:bucket,item_id:item,
                        item_category_id: item_categories, reason_code_id: reason_code, sloc_id1: sloc1, bucket_id1: bucket1, reason_id: reason, partial_v: partial

                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });

            } catch (msg) { console.log(msg.message) }
};
        function DateChecki3() {
        try {
            var entity = $('#entity').val();
        if ($('.item_id').val() != undefined) {
            var item = '';

            if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                item = -1;
            }
            else {
                if ($('.item_id :selected').length > 0) {
                    var selectedreason = [];
                    $('.item_id :selected').each(function (i, selected) {
                        selectedreason[i] = $(selected).val();
                    });
                    item = selectedreason + "";
                }
            }
        }
        if ($('.plant_id').val() != undefined) {
            var reason3 = '';

            if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                reason3 = -1;
            }
            else {
                if ($('.plant_id :selected').length > 0) {
                    var selectedreason = [];
                    $('.plant_id :selected').each(function (i, selected) {
                        selectedreason[i] = $(selected).val();
                    });
                    reason3 = selectedreason + "";
                }
            }
        }
        var partial = $('#partial').val();
        //alert(item);
        //alert(reason3);
        $(".loading").show();
        $.ajax({
            url: '/InventoryReport/Get_Partial',
            method: "Post",
            cache: false,
            data: {
                entity: entity, item_id: item,
                partial_v: partial, item_category_id: reason3

            },
            success: function (data) {
                $(".loading").hide();
                $('#searchresult').empty().append(data);
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText + 'message   ' + error.message);
            }
        });

    } catch (msg) { console.log(msg.message) }
};

        function materialinout() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }


                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }
            
         
                if ($('.item_id').val() != undefined) {
                    var item = '';
                    if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                        item = -1;
                    }
                    else {
                        if ($('.item_id :selected').length > 0) {
                            var selecteditem = [];
                            $('.item_id :selected').each(function (i, selected) {
                                selecteditem[i] = $(selected).val();
                            });
                            item = selecteditem + "";
                        }
                    }
                }
          
                if ($('.employee_id').val() != undefined) {
                    var employees = '';
                    if ($('.employee_id').next('div').find('button').find('span').text().includes("All")) {
                        employees = -1;
                    }
                    else {
                        if ($('.employee_id :selected').length > 0) {
                            var selectedemployees = [];
                            $('.employee_id :selected').each(function (i, selected) {
                                selectedemployees[i] = $(selected).val();
                            });
                            employees = selectedemployees + "";
                        }
                    }
                }
             
                if ($('.plant_id').val() != undefined) {
                    var plant = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        plant = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedplant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedplant[i] = $(selected).val();
                            });
                            plant = selectedplant + "";
                        }
                    }
                }
          
                var partial = $('#partial').val();
                $(".loading").show();
                $.ajax({
                    url: '/InventoryReport/Get_PartialM',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, from_date: fromDate, to_date: toDate, plant_id: plant, item_id: item,
                        employee_id: employees, partial_v: partial

                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });

            } catch (msg) { console.log(msg.message) }
        };

        function OnToolbarClicki(args) {
            try {
                if (args.itemName.indexOf("Export") > -1) {
                    let entity = $('#entity').val();
                    var fromDate = $("#fromDate").val();
                    var toDate = $("#toDate").val();
                    if ($('.sloc_id').val() != undefined) {
                        var sloc = '';
                        if ($('.sloc_id').next('div').find('button').find('span').text().includes("All")) {
                            sloc = -1;
                        }
                        else {
                            if ($('.sloc_id :selected').length > 0) {
                                var selectedsloc = [];
                                $('.sloc_id :selected').each(function (i, selected) {
                                    selectedsloc[i] = $(selected).val();
                                });
                                sloc = selectedsloc + "";
                            }
                        }
                    }
                    if ($('.bucket_id').val() != undefined) {
                        var bucket = '';
                        if ($('.bucket_id').next('div').find('button').find('span').text().includes("All")) {
                            bucket = -1;
                        }
                        else {
                            if ($('.bucket_id :selected').length > 0) {
                                var selectedbucket = [];
                                $('.bucket_id :selected').each(function (i, selected) {
                                    selectedbucket[i] = $(selected).val();
                                });
                                bucket = selectedbucket + "";
                            }
                        }
                    }
                    if ($('.item_id').val() != undefined) {
                        var item = '';
                        if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                            item = -1;
                        }
                        else {
                            if ($('.item_id :selected').length > 0) {
                                var selecteditem = [];
                                $('.item_id :selected').each(function (i, selected) {
                                    selecteditem[i] = $(selected).val();
                                });
                                item = selecteditem + "";
                            }
                        }
                    }
                    if ($('.reason_code_id').val() != undefined) {
                        var reason_code = '';
                        if (!$('.reason_code_id').attr('multiple')) {
                            reason_code = $('.reason_code_id').val();
                        }
                        if ($('.reason_code_id').next('div').find('button').find('span').text().includes("All")) {
                            reason_code = -1;
                        }
                        else {
                            if ($('.reason_code_id :selected').length > 0) {
                                var selectedreason_code = [];
                                $('.reason_code_id :selected').each(function (i, selected) {
                                    selectedreason_code[i] = $(selected).val();
                                });
                                reason_code = selectedreason_code + "";
                            }
                        }
                    }
                    if ($('.sloc_id1').val() != undefined) {
                        var sloc1 = '';
                        if ($('.sloc_id1').next('div').find('button').find('span').text().includes("All")) {
                            sloc1 = -1;
                        }
                        else {
                            if ($('.sloc_id1 :selected').length > 0) {
                                var selectedsloc1 = [];
                                $('.sloc_id1 :selected').each(function (i, selected) {
                                    selectedsloc1[i] = $(selected).val();
                                });
                                var sloc1 = selectedsloc1 + "";
                            }
                        }
                    }
                    if ($('.item_category_id').val() != undefined) {
                        var item_categories = '';
                        if ($('.item_category_id').next('div').find('button').find('span').text().includes("All")) {
                            item_categories = -1;
                        }
                        else {
                            if ($('.item_category_id :selected').length > 0) {
                                var selecteditem_categories = [];
                                $('.item_category_id :selected').each(function (i, selected) {
                                    selecteditem_categories[i] = $(selected).val();
                                });
                                item_categories = selecteditem_categories + "";
                            }
                        }
                    }
                    if ($('.bucket_id1').val() != undefined) {
                        var bucket1 = '';
                        if ($('.bucket_id1').next('div').find('button').find('span').text().includes("All")) {
                            bucket1 = -1;
                        }
                        else {
                            if ($('.bucket_id1 :selected').length > 0) {
                                var selectedbucket1 = [];
                                $('.bucket_id1 :selected').each(function (i, selected) {
                                    selectedbucket1[i] = $(selected).val();
                                });
                                bucket1 = selectedbucket1 + "";
                            }
                        }
                    }
                    if ($('.plant_id').val() != undefined) {
                        var plant = '';
                        if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                            plant = -1;
                        }
                        else {
                            if ($('.plant_id :selected').length > 0) {
                                var selectedplant = [];
                                $('.plant_id :selected').each(function (i, selected) {
                                    selectedplant[i] = $(selected).val();
                                });
                                plant = selectedplant + "";
                            }
                        }
                    }
                    if ($('.reason_id').val() != undefined) {
                        var reason = '';
                        if ($('.reason_id').next('div').find('button').find('span').text().includes("All")) {
                            reason = -1;
                        }
                        else {
                            if ($('.reason_id :selected').length > 0) {
                                var selectedreason = [];
                                $('.reason_id :selected').each(function (i, selected) {
                                    selectedreason[i] = $(selected).val();
                                });
                                reason = selectedreason + "";
                            }
                        }
                    }                   
                    this.model["from_date"] = $("#fromDate").val();
                    this.model["to_date"] = $("#toDate").val();
                    this.model["sloc_id"] = sloc;
                    this.model["bucket_id"] = bucket;
                    this.model["item_id"] = item;
                    this.model["reason_code_id"] = reason_code;
                    this.model["sloc_id1"] = sloc1;
                    this.model["item_category_id"] = item_categories;
                    this.model["bucket_id1"] = bucket1;
                    this.model["plant_id"] = plant;
                    this.model["reason_id"] = reason;
                    this.model["entity"] = entity;
                }
            } catch (msg) { console.log(msg.message) }
        }

//FOR PlantMaintenanceReport

 function DateCheckplan() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }


                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }
                if ($('.plant_id').val() != undefined) {
                    var plant = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        plant = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedplant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedplant[i] = $(selected).val();
                            });
                            plant = selectedplant + "";
                        }
                    }
                }
                if ($('.machine_id').val() != undefined) {
                    var machine = '';
                    if ($('.machine_id').next('div').find('button').find('span').text().includes("All")) {
                        machine = -1;
                    }
                    else {
                        if ($('.machine_id :selected').length > 0) {
                            var selectedmachine = [];
                            $('.machine_id :selected').each(function (i, selected) {
                                selectedmachine[i] = $(selected).val();
                            });
                            machine = selectedmachine + "";
                        }
                    }
                }
                if ($('.maintenance_type_id').val() != undefined) {
                    var maintenance_type = '';
                    if ($('.maintenance_type_id').next('div').find('button').find('span').text().includes("All")) {
                        maintenance_type = -1;
                    }
                    else {
                        if ($('.maintenance_type_id :selected').length > 0) {
                            var selectedmaintenance_type = [];
                            $('.maintenance_type_id :selected').each(function (i, selected) {
                                selectedmaintenance_type[i] = $(selected).val();
                            });
                            maintenance_type = selectedmaintenance_type + "";
                        }
                    }
                }
                if ($('.item_id').val() != undefined) {
                    var item = '';
                    if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                        item = -1;
                    }
                    else {
                        if ($('.item_id :selected').length > 0) {
                            var selecteditem = [];
                            $('.item_id :selected').each(function (i, selected) {
                                selecteditem[i] = $(selected).val();
                            });
                            item = selecteditem + "";
                        }
                    }
                }              
                if ($('.frequency_id').val() != undefined) {
                    var frequency = '';
                    if ($('.frequency_id').next('div').find('button').find('span').text().includes("All")) {
                        frequency = -1;
                    }
                    else {
                        if ($('.frequency_id :selected').length > 0) {
                            var selectedfrequency = [];
                            $('.frequency_id :selected').each(function (i, selected) {
                                selectedfrequency[i] = $(selected).val();
                            });
                            frequency = selectedfrequency + "";
                        }
                    }
                }
                if ($('.status_id').val() != undefined) {
                    var status = '';
                    if ($('.status_id').next('div').find('button').find('span').text().includes("All")) {
                        status = -1;
                    }
                    else {
                        if ($('.status_id :selected').length > 0) {
                            var selectedstatus = [];
                            $('.status_id :selected').each(function (i, selected) {
                                selectedstatus[i] = $(selected).val();
                            });
                            var status = selectedstatus + "";
                        }
                    }
                }
                if ($('.auto_manual').val() != undefined) {
                    var auto = '';
                    if ($('.auto_manual').next('div').find('button').find('span').text().includes("All")) {
                        auto = -1;
                    }
                    else {
                        if ($('.auto_manual :selected').length > 0) {
                            var selectedauto = [];
                            $('.auto_manual :selected').each(function (i, selected) {
                                selectedauto[i] = $(selected).val();
                            });
                            auto = selectedauto + "";
                        }
                    }
                }
                if ($('.notification_type').val() != undefined) {
                    var notification = '';
                    if ($('.notification_type').next('div').find('button').find('span').text().includes("All")) {
                        notification = -1;
                    }
                    else {
                        if ($('.notification_type :selected').length > 0) {
                            var selectednotification = [];
                            $('.notification_type :selected').each(function (i, selected) {
                                selectednotification[i] = $(selected).val();
                            });
                            notification = selectednotification + "";
                        }
                    }
                }
               
                if ($('.employee_id').val() != undefined) {
                    var employee = '';
                    if ($('.employee_id').next('div').find('button').find('span').text().includes("All")) {
                        employee = -1;
                    }
                    else {
                        if ($('.employee_id :selected').length > 0) {
                            var selectedemployee = [];
                            $('.employee_id :selected').each(function (i, selected) {
                                selectedemployee[i] = $(selected).val();
                            });
                            employee = selectedemployee + "";
                        }
                    }
                }
                if ($('.machine_category_id').val() != undefined) {
                    var machine_category = '';
                    if ($('.machine_category_id').next('div').find('button').find('span').text().includes("All")) {
                        machine_category = -1;
                    }
                    else {
                        if ($('.machine_category_id :selected').length > 0) {
                            var selectedmachine_category = [];
                            $('.machine_category_id :selected').each(function (i, selected) {
                                selectedmachine_category[i] = $(selected).val();
                            });
                            machine_category = selectedmachine_category + "";
                        }
                    }
                }
                $(".loading").show();
                var partial_v = $('#partial_v').val();
                $.ajax({
                    url: '/PlantMaintenanceReport/Get_Partial',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, plant_id: plant, machine_id: machine, maintenance_type_id: maintenance_type,
                        frequency_id: frequency, status_id: status, from_date: fromDate, to_date: toDate, item_id: item, auto_manual: auto,
                        notification_type: notification, employee_id: employee, partial_v: partial_v,machine_category_id:machine_category

                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });



            } catch (msg) { console.log(msg.message) }
        };

        function OnToolbarClickplan(args) {
            try {
                if (args.itemName.indexOf("Export") > -1) {
                    let entity = $('#entity').val();
                    var fromDate = $("#fromDate").val();
                    var toDate = $("#toDate").val();
                    if ($('.plant_id').val() != undefined) {
                        var plant = '';
                        if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                            plant = -1;
                        }
                        else {
                            if ($('.plant_id :selected').length > 0) {
                                var selectedplant = [];
                                $('.plant_id :selected').each(function (i, selected) {
                                    selectedplant[i] = $(selected).val();
                                });
                                plant = selectedplant + "";
                            }
                        }
                    }
                    if ($('.machine_id').val() != undefined) {
                        var machine = '';
                        if ($('.machine_id').next('div').find('button').find('span').text().includes("All")) {
                            machine = -1;
                        }
                        else {
                            if ($('.machine_id :selected').length > 0) {
                                var selectedmachine = [];
                                $('.machine_id :selected').each(function (i, selected) {
                                    selectedmachine[i] = $(selected).val();
                                });
                                machine = selectedmachine + "";
                            }
                        }
                    }
                    if ($('.maintenance_type_id').val() != undefined) {
                        var maintenance_type = '';
                        if ($('.maintenance_type_id').next('div').find('button').find('span').text().includes("All")) {
                            maintenance_type = -1;
                        }
                        else {
                            if ($('.maintenance_type_id :selected').length > 0) {
                                var selectedmaintenance_type = [];
                                $('.maintenance_type_id :selected').each(function (i, selected) {
                                    selectedmaintenance_type[i] = $(selected).val();
                                });
                                maintenance_type = selectedmaintenance_type + "";
                            }
                        }
                    }
                    if ($('.item_id').val() != undefined) {
                        var item = '';
                        if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                            item = -1;
                        }
                        else {
                            if ($('.item_id :selected').length > 0) {
                                var selecteditem = [];
                                $('.item_id :selected').each(function (i, selected) {
                                    selecteditem[i] = $(selected).val();
                                });
                                item = selecteditem + "";
                            }
                        }
                    }
                    if ($('.frequency_id').val() != undefined) {
                        var frequency = '';
                        if ($('.frequency_id').next('div').find('button').find('span').text().includes("All")) {
                            frequency = -1;
                        }
                        else {
                            if ($('.frequency_id :selected').length > 0) {
                                var selectedfrequency = [];
                                $('.frequency_id :selected').each(function (i, selected) {
                                    selectedfrequency[i] = $(selected).val();
                                });
                                frequency = selectedfrequency + "";
                            }
                        }
                    }
                    if ($('.status_id').val() != undefined) {
                        var status = '';
                        if ($('.status_id').next('div').find('button').find('span').text().includes("All")) {
                            status = -1;
                        }
                        else {
                            if ($('.status_id :selected').length > 0) {
                                var selectedstatus = [];
                                $('.status_id :selected').each(function (i, selected) {
                                    selectedstatus[i] = $(selected).val();
                                });
                                var status = selectedstatus + "";
                            }
                        }
                    }
                    if ($('.auto_manual').val() != undefined) {
                        var auto = '';
                        if ($('.auto_manual').next('div').find('button').find('span').text().includes("All")) {
                            auto = -1;
                        }
                        else {
                            if ($('.auto_manual :selected').length > 0) {
                                var selectedauto = [];
                                $('.auto_manual :selected').each(function (i, selected) {
                                    selectedauto[i] = $(selected).val();
                                });
                                auto = selectedauto + "";
                            }
                        }
                    }
                    if ($('.notification_type').val() != undefined) {
                        var notification = '';
                        if ($('.notification_type').next('div').find('button').find('span').text().includes("All")) {
                            notification = -1;
                        }
                        else {
                            if ($('.notification_type :selected').length > 0) {
                                var selectednotification = [];
                                $('.notification_type :selected').each(function (i, selected) {
                                    selectednotification[i] = $(selected).val();
                                });
                                notification = selectednotification + "";
                            }
                        }
                    }

                    if ($('.employee_id').val() != undefined) {
                        var employee = '';
                        if ($('.employee_id').next('div').find('button').find('span').text().includes("All")) {
                            employee = -1;
                        }
                        else {
                            if ($('.employee_id :selected').length > 0) {
                                var selectedemployee = [];
                                $('.employee_id :selected').each(function (i, selected) {
                                    selectedemployee[i] = $(selected).val();
                                });
                                employee = selectedemployee + "";
                            }
                        }
                    }
                    if ($('.machine_category_id').val() != undefined) {
                        var machine_category = '';
                        if ($('.machine_category_id').next('div').find('button').find('span').text().includes("All")) {
                            machine_category = -1;
                        }
                        else {
                            if ($('.machine_category_id :selected').length > 0) {
                                var selectedmachine_category = [];
                                $('.machine_category_id :selected').each(function (i, selected) {
                                    selectedmachine_category[i] = $(selected).val();
                                });
                                machine_category = selectedmachine_category + "";
                            }
                        }
                    }
                  
                    this.model["from_date"] = $("#fromDate").val();
                    this.model["to_date"] = $("#toDate").val();
                    this.model["plant_id"] = plant;
                    this.model["machine_id"] = machine;
                    this.model["maintenance_type_id"] = maintenance_type;
                    this.model["item_id"] = item;
                    this.model["frequency_id"] = frequency;
                    this.model["status_id"] = status;
                    this.model["auto_manual"] = auto;
                    this.model["notification_type"] = notification;                   
                    this.model["employee_id"] = employee;
                    this.model["machine_category_id"] = machine_category;
                    this.model["entity"] = entity;                   
                }
            } catch (msg) { console.log(msg.message) }
        }

//FOR Banking Report

        function DateCheckb() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }


                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }
                if ($('.bank_cash_account_id').val() != undefined) {
                    var bank_cash_account = '';
                    if ($('.bank_cash_account_id').next('div').find('button').find('span').text().includes("All")) {
                        bank_cash_account = -1;
                    }
                    else {
                        if ($('.bank_cash_account_id :selected').length > 0) {
                            var selectedbank_cash_account_id = [];
                            $('.bank_cash_account_id :selected').each(function (i, selected) {
                                selectedbank_cash_account_id[i] = $(selected).val();
                            });
                            bank_cash_account = selectedbank_cash_account_id + "";
                        }
                    }
                }
                if ($('.cash_bank').val() != undefined) {
                    var cash_banks = '';
                    if ($('.cash_bank').next('div').find('button').find('span').text().includes("All")) {
                        cash_banks = -1;
                    }
                    else {
                        if ($('.cash_bank :selected').length > 0) {
                            var selectedcash_banks = [];
                            $('.cash_bank :selected').each(function (i, selected) {
                                selectedcash_banks[i] = $(selected).val();
                            });
                            cash_banks = selectedcash_banks + "";
                        }
                    }
                }
                if ($('.in_out').val() != undefined) {
                    var in_outs = '';
                    if ($('.in_out').next('div').find('button').find('span').text().includes("All")) {
                        in_outs = -1;
                    }
                    else {
                        if ($('.in_out :selected').length > 0) {
                            var selectedin_outs = [];
                            $('.in_out :selected').each(function (i, selected) {
                                selectedin_outs[i] = $(selected).val();
                            });
                            in_outs = selectedin_outs + "";
                        }
                    }
                }
                $(".loading").show();
                var partial_v = $('#partial_v').val();
                $.ajax({
                    url: '/BankingReport/Get_Partial',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, bank_cash_account_id: bank_cash_account, from_date: fromDate, to_date: toDate, cash_bank: cash_banks, in_out: in_outs, partial_v: partial_v

                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });



            } catch (msg) { console.log(msg.message) }
        };

        function OnToolbarClickb(args) {
            try {
                if (args.itemName.indexOf("Export") > -1) {
                    let entity = $('#entity').val();
                    var fromDate = $("#fromDate").val();
                    var toDate = $("#toDate").val();
                    if ($('.bank_cash_account_id').val() != undefined) {
                        var bank_cash_account = '';
                        if ($('.bank_cash_account_id').next('div').find('button').find('span').text().includes("All")) {
                            bank_cash_account = -1;
                        }
                        else {
                            if ($('.bank_cash_account_id :selected').length > 0) {
                                var selectedbank_cash_account_id = [];
                                $('.bank_cash_account_id :selected').each(function (i, selected) {
                                    selectedbank_cash_account_id[i] = $(selected).val();
                                });
                                bank_cash_account = selectedbank_cash_account_id + "";
                            }
                        }
                    }
                    if ($('.cash_bank').val() != undefined) {
                        var cash_banks = '';
                        if ($('.cash_bank').next('div').find('button').find('span').text().includes("All")) {
                            cash_banks = -1;
                        }
                        else {
                            if ($('.cash_bank :selected').length > 0) {
                                var selectedcash_banks = [];
                                $('.cash_bank :selected').each(function (i, selected) {
                                    selectedcash_banks[i] = $(selected).val();
                                });
                                cash_banks = selectedcash_banks + "";
                            }
                        }
                    }
                    if ($('.in_out').val() != undefined) {
                        var in_outs = '';
                        if ($('.in_out').next('div').find('button').find('span').text().includes("All")) {
                            in_outs = -1;
                        }
                        else {
                            if ($('.in_out :selected').length > 0) {
                                var selectedin_outs = [];
                                $('.in_out :selected').each(function (i, selected) {
                                    selectedin_outs[i] = $(selected).val();
                                });
                                in_outs = selectedin_outs + "";
                            }
                        }
                    }
                  

                    this.model["from_date"] = $("#fromDate").val();
                    this.model["to_date"] = $("#toDate").val();
                    this.model["bank_cash_account_id"] = bank_cash_account;
                    this.model["cash_bank"] = cash_banks;
                    this.model["in_out"] = in_outs;
                    this.model["entity"] = entity;
                }
            } catch (msg) { console.log(msg.message) }
        }


        //FOR Account Report

        function DateChecka() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }


                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }
                if ($('.customer_category_id').val() != undefined) {
                    var customer_category = '';
                    if (!$('.customer_category_id').attr('multiple')) {
                        customer_category = $('.customer_category_id').val();
                    }
                    if ($('.customer_category_id').next('div').find('button').find('span').text().includes("All")) {
                        customer_category = -1;
                    }
                    else {
                        if ($('.customer_category_id :selected').length > 0) {
                            var selectedcustomer_category = [];
                            $('.customer_category_id :selected').each(function (i, selected) {
                                selectedcustomer_category[i] = $(selected).val();
                            });
                            customer_category = selectedcustomer_category + "";
                        }
                    }
                }
                if ($('.priority_id').val() != undefined) {
                    var priority = '';
                    if ($('.priority_id').next('div').find('button').find('span').text().includes("All")) {
                        priority = -1;
                    }
                    else {
                        if ($('.priority_id :selected').length > 0) {
                            var selectedpriority = [];
                            $('.priority_id :selected').each(function (i, selected) {
                                selectedpriority[i] = $(selected).val();
                            });
                            priority = selectedpriority + "";
                        }
                    }
                }
                if ($('.currency_id').val() != undefined) {
                    var currency = '';
                    if ($('.currency_id').next('div').find('button').find('span').text().includes("All")) {
                        currency = -1;
                    }
                    else {
                        if ($('.currency_id :selected').length > 0) {
                            var selectedcurrency = [];
                            $('.currency_id :selected').each(function (i, selected) {
                                selectedcurrency[i] = $(selected).val();
                            });
                            currency = selectedcurrency + "";
                        }
                    }
                }
                if ($('.plant_id').val() != undefined) {
                    var plant = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        plant = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedplant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedplant[i] = $(selected).val();
                            });
                            plant = selectedplant + "";
                        }
                    }
                }
                if ($('.business_unit_id').val() != undefined) {
                    var business_unit = '';
                    if ($('.business_unit_id').next('div').find('button').find('span').text().includes("All")) {
                        business_unit = -1;
                    }
                    else {
                        if ($('.business_unit_id :selected').length > 0) {
                            var selectedbusiness_unit = [];
                            $('.business_unit_id :selected').each(function (i, selected) {
                                selectedbusiness_unit[i] = $(selected).val();
                            });
                            business_unit = selectedbusiness_unit + "";
                        }
                    }
                }
                if ($('.document_based_on').val() != undefined) {
                    var document_based = '';
                    if ($('.document_based_on').next('div').find('button').find('span').text().includes("All")) {
                        document_based = -1;
                    }
                    else {
                        if ($('.document_based_on :selected').length > 0) {
                            var selecteddocument_based = [];
                            $('.document_based_on :selected').each(function (i, selected) {
                                selecteddocument_based[i] = $(selected).val();
                            });
                            document_based = selecteddocument_based + "";
                        }
                    }
                }
                if ($('.days_per_interval').val() != undefined) {
                    var days_per = '';
                    if ($('.days_per_interval').next('div').find('button').find('span').text().includes("All")) {
                        days_per = -1;
                    }
                    else {
                        if ($('.days_per_interval :selected').length > 0) {
                            var selecteddays_per = [];
                            $('.days_per_interval :selected').each(function (i, selected) {
                                selecteddays_per[i] = $(selected).val();
                            });
                            days_per = selecteddays_per + "";
                        }
                    }
                }
                if ($('.no_of_interval').val() != undefined) {
                    var no_of = '';
                    if ($('.no_of_interval').next('div').find('button').find('span').text().includes("All")) {
                        no_of = -1;
                    }
                    else {
                        if ($('.no_of_interval :selected').length > 0) {
                            var selectedno_of = [];
                            $('.no_of_interval :selected').each(function (i, selected) {
                                selectedno_of[i] = $(selected).val();
                            });
                            no_of = selectedno_of + "";
                        }
                    }
                }
                if ($('.entity_id').val() != undefined) {
                    var entity_ids = '';
                    if (!$('.entity_id').attr('multiple')) {
                        entity_ids = $('.entity_id').val();
                    }
                    if ($('.entity_id').next('div').find('button').find('span').text().includes("All")) {
                        entity_ids = -1;
                    }
                    else {
                        if ($('.entity_id :selected').length > 0) {
                            var selectedentity_ids = [];
                            $('.entity_id :selected').each(function (i, selected) {
                                selectedentity_ids[i] = $(selected).val();
                            });
                            entity_ids = selectedentity_ids + "";
                        }
                    }
                }
                if ($('.tds_code_id').val() != undefined) {
                    var tds_code = '';
                    if ($('.tds_code_id').next('div').find('button').find('span').text().includes("All")) {
                        tds_code = -1;
                    }
                    else {
                        if ($('.tds_code_id :selected').length > 0) {
                            var selectedtds_code = [];
                            $('.tds_code_id :selected').each(function (i, selected) {
                                selectedtds_code[i] = $(selected).val();
                            });
                            tds_code = selectedtds_code + "";
                        }
                    }
                }
                if ($('.show_value_by').val() != undefined) {
                    var show_value = '';
                    if ($('.show_value_by').next('div').find('button').find('span').text().includes("All")) {
                        show_value = -1;
                    }
                    else {
                        if ($('.show_value_by :selected').length > 0) {
                            var selectedshow_value = [];
                            $('.show_value_by :selected').each(function (i, selected) {
                                selectedshow_value[i] = $(selected).val();
                            });
                            show_value = selectedshow_value + "";
                        }
                    }
                }
                if ($('.entity_type_id').val() != undefined) {
                    var entity_type = '';
                    if (!$('.entity_type_id').attr('multiple')) {
                        entity_type = $('.entity_type_id').val();
                    }
                    if ($('.entity_type_id').next('div').find('button').find('span').text().includes("All")) {
                        entity_type = -1;
                    }
                    else {
                        if ($('.entity_type_id :selected').length > 0) {
                            var selectedentity_type = [];
                            $('.entity_type_id :selected').each(function (i, selected) {
                                selectedentity_type[i] = $(selected).val();
                            });
                            entity_type = selectedentity_type + "";
                        }
                    }
                }

                $(".loading").show();
                var partial_v = $('#partial_v').val();
                $.ajax({
                    url: '/AccountsReport/Get_Partial',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, from_date: fromDate, to_date: toDate, customer_category_id: customer_category, priority_id: priority, currency_id: currency,
                        plant_id: plant, business_unit_id: business_unit, document_based_on: document_based, days_per_interval: days_per, no_of_interval: no_of, entity_id: entity_ids,
                        tds_code_id: tds_code, show_value_by: show_value, entity_type_id: entity_type, partial_v: partial_v

                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });



            } catch (msg) { console.log(msg.message) }
        };

        function OnToolbarClicka(args) {
            try {
                if (args.itemName.indexOf("Export") > -1) {
                    let entity = $('#entity').val();
                    var fromDate = $("#fromDate").val();
                    var toDate = $("#toDate").val();
                    if ($('.customer_category_id').val() != undefined) {
                        var customer_category = '';
                        if (!$('.customer_category_id').attr('multiple')) {
                            customer_category = $('.customer_category_id').val();
                        }
                        if ($('.customer_category_id').next('div').find('button').find('span').text().includes("All")) {
                            customer_category = -1;
                        }
                        else {
                            if ($('.customer_category_id :selected').length > 0) {
                                var selectedcustomer_category = [];
                                $('.customer_category_id :selected').each(function (i, selected) {
                                    selectedcustomer_category[i] = $(selected).val();
                                });
                                customer_category = selectedcustomer_category + "";
                            }
                        }
                    }
                    if ($('.priority_id').val() != undefined) {
                        var priority = '';
                        if ($('.priority_id').next('div').find('button').find('span').text().includes("All")) {
                            priority = -1;
                        }
                        else {
                            if ($('.priority_id :selected').length > 0) {
                                var selectedpriority = [];
                                $('.priority_id :selected').each(function (i, selected) {
                                    selectedpriority[i] = $(selected).val();
                                });
                                priority = selectedpriority + "";
                            }
                        }
                    }
                    if ($('.currency_id').val() != undefined) {
                        var currency = '';
                        if ($('.currency_id').next('div').find('button').find('span').text().includes("All")) {
                            currency = -1;
                        }
                        else {
                            if ($('.currency_id :selected').length > 0) {
                                var selectedcurrency = [];
                                $('.currency_id :selected').each(function (i, selected) {
                                    selectedcurrency[i] = $(selected).val();
                                });
                                currency = selectedcurrency + "";
                            }
                        }
                    }
                    if ($('.plant_id').val() != undefined) {
                        var plant = '';
                        if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                            plant = -1;
                        }
                        else {
                            if ($('.plant_id :selected').length > 0) {
                                var selectedplant = [];
                                $('.plant_id :selected').each(function (i, selected) {
                                    selectedplant[i] = $(selected).val();
                                });
                                plant = selectedplant + "";
                            }
                        }
                    }
                    if ($('.business_unit_id').val() != undefined) {
                        var business_unit = '';
                        if ($('.business_unit_id').next('div').find('button').find('span').text().includes("All")) {
                            business_unit = -1;
                        }
                        else {
                            if ($('.business_unit_id :selected').length > 0) {
                                var selectedbusiness_unit = [];
                                $('.business_unit_id :selected').each(function (i, selected) {
                                    selectedbusiness_unit[i] = $(selected).val();
                                });
                                business_unit = selectedbusiness_unit + "";
                            }
                        }
                    }
                    if ($('.document_based_on').val() != undefined) {
                        var document_based = '';
                        if ($('.document_based_on').next('div').find('button').find('span').text().includes("All")) {
                            document_based = -1;
                        }
                        else {
                            if ($('.document_based_on :selected').length > 0) {
                                var selecteddocument_based = [];
                                $('.document_based_on :selected').each(function (i, selected) {
                                    selecteddocument_based[i] = $(selected).val();
                                });
                                document_based = selecteddocument_based + "";
                            }
                        }
                    }
                    if ($('.days_per_interval').val() != undefined) {
                        var days_per = '';
                        if ($('.days_per_interval').next('div').find('button').find('span').text().includes("All")) {
                            days_per = -1;
                        }
                        else {
                            if ($('.days_per_interval :selected').length > 0) {
                                var selecteddays_per = [];
                                $('.days_per_interval :selected').each(function (i, selected) {
                                    selecteddays_per[i] = $(selected).val();
                                });
                                days_per = selecteddays_per + "";
                            }
                        }
                    }
                    if ($('.no_of_interval').val() != undefined) {
                        var no_of = '';
                        if ($('.no_of_interval').next('div').find('button').find('span').text().includes("All")) {
                            no_of = -1;
                        }
                        else {
                            if ($('.no_of_interval :selected').length > 0) {
                                var selectedno_of = [];
                                $('.no_of_interval :selected').each(function (i, selected) {
                                    selectedno_of[i] = $(selected).val();
                                });
                                no_of = selectedno_of + "";
                            }
                        }
                    }
                    if ($('.entity_id').val() != undefined) {
                        var entity_ids = '';
                        if (!$('.entity_id').attr('multiple')) {
                            entity_ids = $('.entity_id').val();
                        }
                        if ($('.entity_id').next('div').find('button').find('span').text().includes("All")) {
                            entity_ids = -1;
                        }
                        else {
                            if ($('.entity_id :selected').length > 0) {
                                var selectedentity_ids = [];
                                $('.entity_id :selected').each(function (i, selected) {
                                    selectedentity_ids[i] = $(selected).val();
                                });
                                entity_ids = selectedentity_ids + "";
                            }
                        }
                    }
                    if ($('.tds_code_id').val() != undefined) {
                        var tds_code = '';
                        if ($('.tds_code_id').next('div').find('button').find('span').text().includes("All")) {
                            tds_code = -1;
                        }
                        else {
                            if ($('.tds_code_id :selected').length > 0) {
                                var selectedtds_code = [];
                                $('.tds_code_id :selected').each(function (i, selected) {
                                    selectedtds_code[i] = $(selected).val();
                                });
                                tds_code = selectedtds_code + "";
                            }
                        }
                    }
                    if ($('.show_value_by').val() != undefined) {
                        var show_value = '';
                        if ($('.show_value_by').next('div').find('button').find('span').text().includes("All")) {
                            show_value = -1;
                        }
                        else {
                            if ($('.show_value_by :selected').length > 0) {
                                var selectedshow_value = [];
                                $('.show_value_by :selected').each(function (i, selected) {
                                    selectedshow_value[i] = $(selected).val();
                                });
                                show_value = selectedshow_value + "";
                            }
                        }
                    }
                    if ($('.entity_type_id').val() != undefined) {
                        var entity_type = '';
                        if (!$('.entity_type_id').attr('multiple')) {
                            entity_type = $('.entity_type_id').val();
                        }
                        if ($('.entity_type_id').next('div').find('button').find('span').text().includes("All")) {
                            entity_type = -1;
                        }
                        else {
                            if ($('.entity_type_id :selected').length > 0) {
                                var selectedentity_type = [];
                                $('.entity_type_id :selected').each(function (i, selected) {
                                    selectedentity_type[i] = $(selected).val();
                                });
                                entity_type = selectedentity_type + "";
                            }
                        }
                    }


                    this.model["from_date"] = $("#fromDate").val();
                    this.model["to_date"] = $("#toDate").val();
                    this.model["customer_category_id"] = customer_category;
                    this.model["priority_id"] = priority;
                    this.model["currency_id"] = currency;
                    this.model["plant_id"] = plant;
                    this.model["business_unit_id"] = business_unit;
                    this.model["document_based_on"] = document_based;
                    this.model["days_per_interval"] = days_per;
                    this.model["no_of_interval"] = no_of;
                    this.model["entity_id"] = entity_ids;
                    this.model["tds_code_id"] = tds_code;
                    this.model["show_value_by"] = show_value;
                    this.model["entity_type_id"] = entity_type;
                    this.model["entity"] = entity;
                }
            } catch (msg) { console.log(msg.message) }
        }

        //FOR Taxation as GSTRReport

        function DateCheckg() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }


                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }



                if ($('.plant_id').val() != undefined) {
                    var plant = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        plant = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedplant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedplant[i] = $(selected).val();
                            });
                            plant = selectedplant + "";
                        }
                    }
                }


                $(".loading").show();
                var partial_v = $('#partial').val();
                $.ajax({
                    url: '/GSTRReport/GSTR_Report',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, from_date: fromDate, to_date: toDate, plant_id: plant, partial_v: partial_v

                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });



            } catch (msg) { console.log(msg.message) }
        };

        function OnToolbarClickg(args) {
            try {
                if (args.itemName.indexOf("Export") > -1) {
                    let entity = $('#entity').val();
                    var fromDate = $("#fromDate").val();
                    var toDate = $("#toDate").val();



                    if ($('.plant_id').val() != undefined) {
                        var plant = '';
                        if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                            plant = -1;
                        }
                        else {
                            if ($('.plant_id :selected').length > 0) {
                                var selectedplant = [];
                                $('.plant_id :selected').each(function (i, selected) {
                                    selectedplant[i] = $(selected).val();
                                });
                                plant = selectedplant + "";
                            }
                        }
                    }

                    this.model["from_date"] = $("#fromDate").val();
                    this.model["to_date"] = $("#toDate").val();
                    this.model["plant_id"] = plant;
                    this.model["entity"] = entity;
                }
            } catch (msg) { console.log(msg.message) }
        }

        //FOR Quality Report

        function DateCheckq() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }


                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }
                if ($('.plant_id').val() != undefined) {
                    var plant = '';
                    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                        plant = -1;
                    }
                    else {
                        if ($('.plant_id :selected').length > 0) {
                            var selectedplant = [];
                            $('.plant_id :selected').each(function (i, selected) {
                                selectedplant[i] = $(selected).val();
                            });
                            plant = selectedplant + "";
                        }
                    }
                }
                if ($('.document_type_code').val() != undefined) {
                    var document_type = '';
                    if (!$('.document_type_code').attr('multiple')) {
                        document_type = $('.document_type_code').val();
                    }
                    if ($('.document_type_code').next('div').find('button').find('span').text().includes("All")) {
                        document_type = -1;
                    }
                    else {
                        if ($('.document_type_code :selected').length > 0) {
                            var selecteddocument_type = [];
                            $('.document_type_code :selected').each(function (i, selected) {
                                selecteddocument_type[i] = $(selected).val();
                            });
                            document_type = selecteddocument_type + "";
                        }
                    }
                }
                if ($('.item_id').val() != undefined) {
                    var item = '';
                    if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                        item = -1;
                    }
                    else {
                        if ($('.item_id :selected').length > 0) {
                            var selecteditem = [];
                            $('.item_id :selected').each(function (i, selected) {
                                selecteditem[i] = $(selected).val();
                            });
                            item = selecteditem + "";
                        }
                    }
                }
                if ($('.status_id').val() != undefined) {
                    var status = '';
                    if ($('.status_id').next('div').find('button').find('span').text().includes("All")) {
                        status = -1;
                    }
                    else {
                        if ($('.status_id :selected').length > 0) {
                            var selectedstatus = [];
                            $('.status_id :selected').each(function (i, selected) {
                                selectedstatus[i] = $(selected).val();
                            });
                            status = selectedstatus + "";
                        }
                    }
                }
                if ($('.sloc_id').val() != undefined) {
                    var sloc = '';
                    if ($('.sloc_id').next('div').find('button').find('span').text().includes("All")) {
                        sloc = -1;
                    }
                    else {
                        if ($('.sloc_id :selected').length > 0) {
                            var selectedsloc = [];
                            $('.sloc_id :selected').each(function (i, selected) {
                                selectedsloc[i] = $(selected).val();
                            });
                            sloc = selectedsloc + "";
                        }
                    }
                }
                if ($('.reason_id').val() != undefined) {
                    var reason = '';
                    if ($('.reason_id').next('div').find('button').find('span').text().includes("All")) {
                        reason = -1;
                    }
                    else {
                        if ($('.reason_id :selected').length > 0) {
                            var selectedreason = [];
                            $('.reason_id :selected').each(function (i, selected) {
                                selectedreason[i] = $(selected).val();
                            });
                            reason = selectedreason + "";
                        }
                    }
                }

                $(".loading").show();
                var partial_v = $('#partial').val();
                $.ajax({
                    url: '/QualityReport/Get_Partial',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, from_date: fromDate, to_date: toDate, plant_id: plant, document_type_code: document_type,
                        item_id: item, status_id: status, sloc_id: sloc, partial_v: partial_v, reason_id: reason
                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });



            } catch (msg) { console.log(msg.message) }
        };

        function OnToolbarClickq(args) {
            try {
                if (args.itemName.indexOf("Export") > -1) {
                    let entity = $('#entity').val();
                    var fromDate = $("#fromDate").val();
                    var toDate = $("#toDate").val();
                    if ($('.plant_id').val() != undefined) {
                        var plant = '';
                        if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                            plant = -1;
                        }
                        else {
                            if ($('.plant_id :selected').length > 0) {
                                var selectedplant = [];
                                $('.plant_id :selected').each(function (i, selected) {
                                    selectedplant[i] = $(selected).val();
                                });
                                plant = selectedplant + "";
                            }
                        }
                    }
                    if ($('.document_type_code').val() != undefined) {
                        var document_type = '';
                        if (!$('.document_type_code').attr('multiple')) {
                            document_type = $('.document_type_code').val();
                        }
                        if ($('.document_type_code').next('div').find('button').find('span').text().includes("All")) {
                            document_type = -1;
                        }
                        else {
                            if ($('.document_type_code :selected').length > 0) {
                                var selecteddocument_type = [];
                                $('.document_type_code :selected').each(function (i, selected) {
                                    selecteddocument_type[i] = $(selected).val();
                                });
                                document_type = selecteddocument_type + "";
                            }
                        }
                    }
                    if ($('.item_id').val() != undefined) {
                        var item = '';
                        if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
                            item = -1;
                        }
                        else {
                            if ($('.item_id :selected').length > 0) {
                                var selecteditem = [];
                                $('.item_id :selected').each(function (i, selected) {
                                    selecteditem[i] = $(selected).val();
                                });
                                item = selecteditem + "";
                            }
                        }
                    }
                    if ($('.status_id').val() != undefined) {
                        var status = '';
                        if ($('.status_id').next('div').find('button').find('span').text().includes("All")) {
                            status = -1;
                        }
                        else {
                            if ($('.status_id :selected').length > 0) {
                                var selectedstatus = [];
                                $('.status_id :selected').each(function (i, selected) {
                                    selectedstatus[i] = $(selected).val();
                                });
                                status = selectedstatus + "";
                            }
                        }
                    }
                    if ($('.sloc_id').val() != undefined) {
                        var sloc = '';
                        if ($('.sloc_id').next('div').find('button').find('span').text().includes("All")) {
                            sloc = -1;
                        }
                        else {
                            if ($('.sloc_id :selected').length > 0) {
                                var selectedsloc = [];
                                $('.sloc_id :selected').each(function (i, selected) {
                                    selectedsloc[i] = $(selected).val();
                                });
                                sloc = selectedsloc + "";
                            }
                        }
                    }
                    if ($('.reason_id').val() != undefined) {
                        var reason = '';
                        if ($('.reason_id').next('div').find('button').find('span').text().includes("All")) {
                            reason = -1;
                        }
                        else {
                            if ($('.reason_id :selected').length > 0) {
                                var selectedreason = [];
                                $('.reason_id :selected').each(function (i, selected) {
                                    selectedreason[i] = $(selected).val();
                                });
                                reason = selectedreason + "";
                            }
                        }
                    }

                    this.model["from_date"] = $("#fromDate").val();
                    this.model["to_date"] = $("#toDate").val();
                    this.model["plant_id"] = plant;
                    this.model["document_type_code"] = document_type;
                    this.model["item_id"] = item;
                    this.model["status_id"] = status;
                    this.model["sloc_id"] = sloc;
                    this.model["reason_id"] = reason;
                    this.model["entity"] = entity;
                }
            } catch (msg) { console.log(msg.message) }
        }

        //For Fixed Asset Report
        function DateCheckForFixedAsset() {
            try {
                if (document.getElementById('fromDate') != null) {
                    var StartDate = document.getElementById('fromDate').value;
                    var sDate = new Date(StartDate);
                }
                if (document.getElementById('toDate') != null) {
                    var EndDate = document.getElementById('toDate').value;
                    var eDate = new Date(EndDate);
                }

                if (StartDate != '' && StartDate != '' && sDate > eDate) {
                    sweetAlert("", "Please ensure that the From Date is less than or equal to the To Date.", "error");
                    return false;
                }
                var entity = $('#entity').val();
                if (document.getElementById('fromDate') != null) {
                    var fromDate = $("#fromDate").val();
                }
                if (document.getElementById('toDate') != null) {
                    var toDate = $("#toDate").val();
                }
                //if ($('.plant_id').val() != undefined) {
                //    var plant = '';
                //    if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
                //        plant = -1;
                //    }
                //    else {
                //        if ($('.plant_id :selected').length > 0) {
                //            var selectedplant = [];
                //            $('.plant_id :selected').each(function (i, selected) {
                //                selectedplant[i] = $(selected).val();
                //            });
                //            plant = selectedplant + "";
                //        }
                //    }
                //}
                //if ($('.document_type_code').val() != undefined) {
                //    var document_type = '';
                //    if (!$('.document_type_code').attr('multiple')) {
                //        document_type = $('.document_type_code').val();
                //    }
                //    if ($('.document_type_code').next('div').find('button').find('span').text().includes("All")) {
                //        document_type = -1;
                //    }
                //    else {
                //        if ($('.document_type_code :selected').length > 0) {
                //            var selecteddocument_type = [];
                //            $('.document_type_code :selected').each(function (i, selected) {
                //                selecteddocument_type[i] = $(selected).val();
                //            });
                //            document_type = selecteddocument_type + "";
                //        }
                //    }
                //}

                var depreciation_type = 'slm_wdv';

               var asset_code = -1;
               if ($('.asset_master_data_id').val() != undefined) {
                   asset_code = '';
                    if ($('.asset_master_data_id').next('div').find('button').find('span').text().includes("All")) {
                        asset_code = -1;
                    }
                    else {
                        if ($('.asset_master_data_id :selected').length > 0) {
                            var selectedasset_code = [];
                            $('.asset_master_data_id :selected').each(function (i, selected) {
                                selectedasset_code[i] = $(selected).val();
                            });
                            asset_code = selectedasset_code + "";
                        }
                    }
               }
                
               var depreciation_area = -1
               if ($('.dep_area_id').val() != undefined) {
                     depreciation_area = '';
                    if ($('.dep_area_id').next('div').find('button').find('span').text().includes("All")) {
                        depreciation_area = -1;
                    }
                    else {
                        if ($('.dep_area_id :selected').length > 0) {
                            var selecteddepreciation_area = [];
                            $('.dep_area_id :selected').each(function (i, selected) {
                                selecteddepreciation_area[i] = $(selected).val();
                            });
                            depreciation_area = selecteddepreciation_area + "";
                        }
                    }
               }

               var asset_class = -1;
                  if ($('.asset_class_id').val() != undefined) {
                     asset_class = '';
                    if ($('.asset_class_id').next('div').find('button').find('span').text().includes("All")) {
                        asset_class = -1;
                    }
                    else {
                        if ($('.asset_class_id :selected').length > 0) {
                            var selectedasset_class = [];
                            $('.asset_class_id :selected').each(function (i, selected) {
                                selectedasset_class[i] = $(selected).val();
                            });
                            asset_class = selectedasset_class + "";
                        }
                    }
                  }

                  var asset_group = -1;
                  if ($('.asset_group_id').val() != undefined) {
                     asset_group = '';
                     if ($('.asset_group_id').next('div').find('button').find('span').text().includes("All")) {
                        asset_group = -1;
                    }
                    else {
                         if ($('.asset_group_id :selected').length > 0) {
                            var selectedasset_group = [];
                            $('.asset_group_id :selected').each(function (i, selected) {
                                selectedasset_group[i] = $(selected).val();
                            });
                            asset_group = selectedasset_group + "";
                        }
                    }
                  }
                  


                //if ($('.status_id').val() != undefined) {
                //    var status = '';
                //    if ($('.status_id').next('div').find('button').find('span').text().includes("All")) {
                //        status = -1;
                //    }
                //    else {
                //        if ($('.status_id :selected').length > 0) {
                //            var selectedstatus = [];
                //            $('.status_id :selected').each(function (i, selected) {
                //                selectedstatus[i] = $(selected).val();
                //            });
                //            status = selectedstatus + "";
                //        }
                //    }
                //}
                //if ($('.sloc_id').val() != undefined) {
                //    var sloc = '';
                //    if ($('.sloc_id').next('div').find('button').find('span').text().includes("All")) {
                //        sloc = -1;
                //    }
                //    else {
                //        if ($('.sloc_id :selected').length > 0) {
                //            var selectedsloc = [];
                //            $('.sloc_id :selected').each(function (i, selected) {
                //                selectedsloc[i] = $(selected).val();
                //            });
                //            sloc = selectedsloc + "";
                //        }
                //    }
                //}
                //if ($('.reason_id').val() != undefined) {
                //    var reason = '';
                //    if ($('.reason_id').next('div').find('button').find('span').text().includes("All")) {
                //        reason = -1;
                //    }
                //    else {
                //        if ($('.reason_id :selected').length > 0) {
                //            var selectedreason = [];
                //            $('.reason_id :selected').each(function (i, selected) {
                //                selectedreason[i] = $(selected).val();
                //            });
                //            reason = selectedreason + "";
                //        }
                //    }
                //}

                $(".loading").show();
                var partial_v = $('#partial_v').val();
                $.ajax({
                    url: '/FixedAssetReports/Get_Partial',
                    method: "Post",
                    cache: false,
                    data: {
                        entity: entity, from_date: fromDate, to_date: toDate, depreciation_type: depreciation_type, asset_code: asset_code, depreciation_area: depreciation_area, asset_class: asset_class, asset_group: asset_group,
                        partial_v: partial_v
                    },
                    success: function (data) {
                        $(".loading").hide();
                        $('#searchresult').empty().append(data);
                    },
                    error: function (xhr, status, error) {
                        console.log(xhr.responseText + 'message   ' + error.message);
                    }
                });



            } catch (msg) { console.log(msg.message) }
        };

        function OnToolbarClickFixedAsset(args) {
            //try {
            //    if (args.itemName.indexOf("Export") > -1) {
            //        let entity = $('#entity').val();
            //        var fromDate = $("#fromDate").val();
            //        var toDate = $("#toDate").val();
            //        if ($('.plant_id').val() != undefined) {
            //            var plant = '';
            //            if ($('.plant_id').next('div').find('button').find('span').text().includes("All")) {
            //                plant = -1;
            //            }
            //            else {
            //                if ($('.plant_id :selected').length > 0) {
            //                    var selectedplant = [];
            //                    $('.plant_id :selected').each(function (i, selected) {
            //                        selectedplant[i] = $(selected).val();
            //                    });
            //                    plant = selectedplant + "";
            //                }
            //            }
            //        }
            //        if ($('.document_type_code').val() != undefined) {
            //            var document_type = '';
            //            if (!$('.document_type_code').attr('multiple')) {
            //                document_type = $('.document_type_code').val();
            //            }
            //            if ($('.document_type_code').next('div').find('button').find('span').text().includes("All")) {
            //                document_type = -1;
            //            }
            //            else {
            //                if ($('.document_type_code :selected').length > 0) {
            //                    var selecteddocument_type = [];
            //                    $('.document_type_code :selected').each(function (i, selected) {
            //                        selecteddocument_type[i] = $(selected).val();
            //                    });
            //                    document_type = selecteddocument_type + "";
            //                }
            //            }
            //        }
            //        if ($('.item_id').val() != undefined) {
            //            var item = '';
            //            if ($('.item_id').next('div').find('button').find('span').text().includes("All")) {
            //                item = -1;
            //            }
            //            else {
            //                if ($('.item_id :selected').length > 0) {
            //                    var selecteditem = [];
            //                    $('.item_id :selected').each(function (i, selected) {
            //                        selecteditem[i] = $(selected).val();
            //                    });
            //                    item = selecteditem + "";
            //                }
            //            }
            //        }
            //        if ($('.status_id').val() != undefined) {
            //            var status = '';
            //            if ($('.status_id').next('div').find('button').find('span').text().includes("All")) {
            //                status = -1;
            //            }
            //            else {
            //                if ($('.status_id :selected').length > 0) {
            //                    var selectedstatus = [];
            //                    $('.status_id :selected').each(function (i, selected) {
            //                        selectedstatus[i] = $(selected).val();
            //                    });
            //                    status = selectedstatus + "";
            //                }
            //            }
            //        }
            //        if ($('.sloc_id').val() != undefined) {
            //            var sloc = '';
            //            if ($('.sloc_id').next('div').find('button').find('span').text().includes("All")) {
            //                sloc = -1;
            //            }
            //            else {
            //                if ($('.sloc_id :selected').length > 0) {
            //                    var selectedsloc = [];
            //                    $('.sloc_id :selected').each(function (i, selected) {
            //                        selectedsloc[i] = $(selected).val();
            //                    });
            //                    sloc = selectedsloc + "";
            //                }
            //            }
            //        }
            //        if ($('.reason_id').val() != undefined) {
            //            var reason = '';
            //            if ($('.reason_id').next('div').find('button').find('span').text().includes("All")) {
            //                reason = -1;
            //            }
            //            else {
            //                if ($('.reason_id :selected').length > 0) {
            //                    var selectedreason = [];
            //                    $('.reason_id :selected').each(function (i, selected) {
            //                        selectedreason[i] = $(selected).val();
            //                    });
            //                    reason = selectedreason + "";
            //                }
            //            }
            //        }

            //        this.model["from_date"] = $("#fromDate").val();
            //        this.model["to_date"] = $("#toDate").val();
            //        this.model["plant_id"] = plant;
            //        this.model["document_type_code"] = document_type;
            //        this.model["item_id"] = item;
            //        this.model["status_id"] = status;
            //        this.model["sloc_id"] = sloc;
            //        this.model["reason_id"] = reason;
            //        this.model["entity"] = entity;
            //    }
            //} catch (msg) { console.log(msg.message) }
        }