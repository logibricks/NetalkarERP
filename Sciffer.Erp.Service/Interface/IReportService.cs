using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sciffer.Erp.Service.Interface
{
    public interface IReportService : IDisposable
    {
        List<dashboard_vm> GetDashBoardData(string entity);
        List<report_sales_summary> SalesSummaryReport(string customer_category_id, string sales_rm_id, string territory_id, string priority_id, string control_account_id, string currency_id, string business_unit_id, DateTime? start_date, DateTime? end_date,string plant);
        List<fin_payment_receipt_report> Payment_Receipt_Report(string entity, string bank_cash_account_id, DateTime? from_date,
      DateTime? to_date, int? cash_bank, int? in_out);

        //List<report_sales_summary> SalesSummary(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string Control_account, string currency, string plant, string business_unit);
        List<Report_Sales_header_level_VM> Salesheaderlevel(DateTime? From_date, DateTime? To_date, string Sales_RM, string Territory, string Priority, string Control_account, string currency, string plant, string business_unit);
        List<Report_Sales_detail_level_VM> Salesdetaillevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string Control_account, string currency, string plant, string business_unit);
        List<Report_customer_contact_details_VM> customercontactdetails();
        List<Report_Customer_Payment_Master_List_vm> CustomerPaymentMasterList(string Customer_category, string Prioirty);
        List<Report_Client_Ledger_VM> ClientLedger(DateTime? From_date, DateTime? To_date, string Customer_COde, string Customer_Description_);
        List<Report_Forex_Sales__VM> ForexSales(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string Control_account, string currency, string plant, string business_unit);
        List<Report_Sales_Quotation_Summary_VM> SalesQuotationSummary(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit);
        List<Report_sales_Quotation_header_level_VM> salesQuotationheaderlevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit);
        List<Report_sales_Quotation_Detail_level_VM> salesQuotationDetaillevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit);
        List<Report_Sales_Order_Summary_VM> SalesOrderSummary(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit);
        List<Report_sales_order_header_level_vm> salesorderheaderlevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit);
        List<Report_sales_Order_Detail_level_vm> salesOrderDetaillevel(DateTime? From_date, DateTime? To_date, string Customer_category, string Sales_RM, string Territory, string Priority, string currency, string plant, string business_unit);
        List<Report_Sales_Order_Item_wise_Plan_vm> SalesOrderItemwisePlan(DateTime? From_date, DateTime? To_date, string Item_Code, string Plant, string Customer_COde);
        List<Report_Item_Wise_Sales_Summary_vm> ItemWiseSalesSummary(DateTime? From_date, DateTime? To_date, string Item_Code, string Plant);
        List<Report_Item_Wise_Sales_Detail_vm> ItemWiseSalesDetail(DateTime? From_date, DateTime? To_date, string Item_Code, string Plant, string Customer_COde);
        List<Report_Sales_Order_Qty_Tracker_vm> SalesOrderQtyTracker();
        List<Report_Collections_Received_Report_vm> CollectionsReceivedReport(DateTime? From_date, DateTime? To_date);
        List<Report_Collections_Expected_Report__vm> CollectionsExpectedReport(DateTime? From_date, DateTime? To_date, string Customer_Category, string Plant, string Business_Unit);
        List<Report_Customer_Master_Discount_Details_vm> CustomerMasterDiscountDetails();
        List<Report_Sales_Price_List_vm> SalesPriceList(string Customer_Category, string Item_Group, string Customer_Priority, string Item_Prioirty, string Territory);
        List<Report_Open_Sales_Order_vm> OpenSalesOrder(DateTime? As_of_Date_, string Customer_Category, string Plant, string Business_Unit);
        List<Report_Vendor_Contact_Details_vm> VendorContactDetails();
        List<Report_VENDOR_Payment_Master_List__vm> VENDORPaymentMasterList();
        List<Report_Vendor_Ledger_vm> VendorLedger(DateTime? From_Date, DateTime? To_Date, string VENDOR_COde, string VENDOR_Description_);
        List<Report_Vendor_Master_Discount_Details__vm> VendorMasterDiscountDetails();
        List<Report_Purchase_Price_List__vm> PurchasePriceList(string Vendor_Category, string Item_Group, string Vendor_Priority, string Item_Prioirty, string Territory);
        List<Report_Purchase_Requisition_Summary__vm> PurchaseRequisitionSummary(DateTime? From_Date, DateTime? To_Date, string Item_Category, string Status);
        List<Report_Purchase_Requisition_Detailed__vm> PurchaseRequisitionDetailed(DateTime? From_Date, DateTime? To_Date, string Vendir_Category, string Item_Category, string Created_by, string Plant, string Source);
        List<Report_Open_PR_Report__vm> OpenPRReport(DateTime? As_of_Date_, string Item_Category, string Delivery_Date, string From, string To);
        List<Report_Purchase_Order_Summary_vm> PurchaseOrderSummary(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit);
        List<Report_Purchase_Order_Header__vm> PurchaseOrderHeader(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item);
        List<purchase_order_report_vm> Purchase_Order_Report(string entity, string vendor_category_id, string vendor_priority_id, string item_priority_id, string vendor_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id, string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date, string currency_id, string business_unit_id, string item_service_id,string item_id);
        List<sales_order_report_vm> Sales_Order_Report(string entity, string customer_category_id, string customer_priority_id, string item_priority_id, string customer_id, DateTime? from_date, DateTime? to_date, string item_group_id, string territory_id, string item_category, string status_id, string created_by, string plant_id, string source_id, DateTime? posting_date, string currency_id, string business_unit_id, string item_service_id, string item_id, string sales_rm);
        List<Report_GRN_Summary__vm> GRNSummary(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit);
        List<Report_GRN_Header__vm> GRNHeader(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code);
        List<Report_GRN_Item_Level_vm> GRNItemLevel(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code);
        List<Report_GRN_Pending_for_Excise__vm> GRNPendingforExcise(DateTime? As_of_Date_);
        List<Report_Purchase_Invoice_Summary__vm> PurchaseInvoiceSummary(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit);
        List<Report_Purchase_Invoice_Header__vm> PurchaseInvoiceHeader(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item_);
        List<Report_Purchase_Invoice_Item_Level_vm> PurchaseInvoiceItemLevel(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item_);
        List<Report_LD_report__vm> LDreport(DateTime? GRN_From_date, DateTime? GRN_To_date, string Vendor_category, string Priority, string plant, string business_unit, string Vendor_Code);
        List<Report_Purchase_Return_Summary__vm> PurchaseReturnSummary(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit);
        List<Report_Purchase_Return_Header__vm> PurchaseReturnHeader(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item_);
        List<Report_Purchase_Return_Item_Level_vm> PurchaseReturnItemLevel(DateTime? From_date, DateTime? To_date, string Vendor_category, string Priority, string currency, string plant, string business_unit, string Vendor_Code, string Item_);
        List<Report_PO_to_Payment_Tracker_vm> POtoPaymentTracker(DateTime? GRN_From_Date, DateTime? GRN_TO_Date, string Plant);
        List<Report_Sales_Contribution_vm> SalesContribution(DateTime? From_date, DateTime? To_date, string Customer_category, string Priority, string currency, string plant, string business_unit);
        List<Report_ARAGeingReport_vm> ARAgeingReport(DateTime? As_of_Date_, string Customer_Category_);
        List<Report_APAgeingReport_vm> APAgeingReport(DateTime? As_of_Date_, string Vendor_Category_);
        List<Report_LedgerDetails_vm> LedgerDetails(DateTime? From_date, DateTime? To_date, string Account_Type);
        List<Report_TrialBalance_vm> TrialBalance(DateTime? From_date, DateTime? To_date, string Account_Type);
        List<Report_GeneralLedger_vm> GeneralLedger(DateTime? From_date, DateTime? To_date, string Account_Type, string Category);
        List<Report_Item_Accounting_Report_vm> ItemAccountingReport();
        List<Report_InventoryledgerDetailed_vm> InventoryledgerDetailed(DateTime? From_Date, DateTime? to_Date, string Postingg_date_, string Plant, string Sloc, string Item_Code, string Item_Category, string Bucket);
        List<Report_Inventory_ledger_Summary_vm> InventoryledgerSummary(DateTime? From_Date, DateTime? to_Date);
        List<Report_InventoryCostingReport_vm> InventoryCostingReport();
        List<Report_InventoryValuationReport_vm> InventoryValuationReport();
        List<Report_GoodsReceipt_vm> GoodsReceiptreport(DateTime? From_Date, DateTime? To_Date, string Plant, string Sloc, string Item_Code, string Item_Category, string Bucket, string Reason_Code);
        List<Report_GoodsIssue_vm> Goodsissuereport(DateTime? From_Date, DateTime? To_Date, string Plant, string Sloc, string Item_Code, string Item_Category, string Bucket, string Reason_Code);
        List<Report_WithinPlanTrasfer_vm> WithinPlanTrasfer(DateTime? From_Date, DateTime? To_Date, string Plant, string Sloc, string Item_Code, string Item_Category, string Bucket, string Reason_Code);
        List<Report_InventoryRevalautionReport_vm> InventoryRevalautionReport(DateTime? From_Date, DateTime? To_Date, string Plant, string Item_Category, string Item_Code);
        List<Report_InventoryRateHistory_vm> InventoryrateHistory(string Item_Code_, string Item_Category_);
        List<Report_MaterialRequisitionReportStatusDetailed_vm> MaterialRequisitionReportStatusDetailed(DateTime? From, DateTime? To);
        List<Report_MaterialRequisitionReportStatusSummary_vm> MaterialRequisitionReportStatusSummary(DateTime? From, DateTime? To);
        List<Report_OpenMaterialRequisitionReport_vm> OpenMaterialRequisitionReport(DateTime? As_of_);
        List<Report_QCLotSummaryReport_vm> QCLotSummaryReport(DateTime? From_Date, DateTime? To_Date, string Status, string Plant, string Item_Code, string Base_DOcument);
        List<Report_QCParametersReport_vm> QCParametersReport(DateTime? From_Date, DateTime? To_Date, string Status, string Plant, string Item_Code, string Base_DOcument);
        List<Report_QCUsageDecisionReport_vm> QCUsageDecisionReport(DateTime? From_Date, DateTime? To_Date, string Status, string Plant, string Item_Code, string Base_DOcument);
        List<Report_QCLOTShelfLifeReport_vm> QCLOTShelfLifeReport(DateTime? From_Date, DateTime? To_Date, string Status, string Plant, string Item_Code, string Base_DOcument);
        List<Report_ShelfLifeExpiredReport_vm> ShelfLifeExpiredReport(DateTime? As_of_Date_, string Plant, string Item_Category, string Item_Code, string SLoc);
        List<Report_ShelfLifeAbouttoExpireReport_vm> ShelfLifeAbouttoExpireReport(DateTime? From_Date, DateTime? TO_Date, string Plant, string Item_Category, string Item_Code, string SLoc);
        List<Report_BatchRevalidated_vm> BatchRevalidAtedreport(DateTime? From_Date, DateTime? To_Date, string pLANT, string iTEM_cATEGORY, string iTEM_cODE);
        List<Report_MATERIAL_CONSUMEDPOSTEXPIRY_vm> MATERIALcONSUMEDPOSTEXPIRY(string pLANT, string iTEM_cATEGORY, string iTEM_cODE, string Reason_code);
        List<Report_ITEM_MASTERQCPARAMETERS_vm> ITEMMASTERQCPARAMETERS();
        List<Report_BatchRevalidationCount_vm> BatchRevalidationCountreport();
        List<Report_LedgerDetails_vm> Report_LedgerDetails_vm(DateTime? From_date, DateTime? To_date, string Customer);
        List<Report_CustomerLedgerDetails_vm> Report_CustomerLedgerDetails_vm(DateTime? From_date, DateTime? To_date, string Customer);
        List<Report_VendorLedgerDetails_vm> Report_VendorLedgerDetails_vm(DateTime? From_date, DateTime? To_date, string vendor);
        List<Report_EmployeeLedgerDetails_vm> Report_EmployeeLedgerDetails_vm(DateTime? From_date, DateTime? To_date, string employee);
        List<Report_GeneralLedgerDetails_vm> Report_GeneralLedgerDetails_vm(DateTime? From_date, DateTime? To_date, string gl_account);
        List<Report_TrialBalance_vm> Report_TrialBalance_vm(DateTime? From_date, DateTime? To_date);
        List<report_inventory_vm> InventoryReport(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string sloc_id, string bucket_id, string item_id, string item_category_id, string reason_code_id, string sloc_id1, string bucket_id1);
        List<mfg_machine_task_VM> Machine_Entry_Report(string entity, string tag_number, string item_list, string prod_order_list, string item_status_id, DateTime? fromDate, DateTime? toDate);
        List<gstr_report_vm> GETGSTRReport(string entity, DateTime from_date, DateTime to_date, string plant_id);
        List<accounts_report_vm> GetAccountsReport(string entity, DateTime? from_date, DateTime? to_date, string customer_category_id, string priority_id,
           string currency_id, string plant_id, string business_unit_id, string document_based_on, string days_per_interval, string no_of_interval, string entity_id,
           string tds_code_id, string show_value_by, string entity_type_id);
        List<report_plant_maintenanace> MaintenancePlanCycleandFrequencyReport(string entity, string plant_id, string machine_id, string maintenance_type_id, string frequency_id, string status_id, DateTime? from_date, DateTime? to_date, string item_id, string auto_manual, string notification_type, string employee_id);
        List<report_production_vm> report_production(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string machine_id, string tag_id, string item_id, string tag_number, string prod_order_id, string item_status_id, string user_id,string process_id, string shift_id, string is_qc_remark);
        List<report_quality_vm> report_quality(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string document_type_code, string item_id, string status_id, string sloc_id,string reason_id);
        List<Report_incentive_vm> GetIncentiveReport(string entity, DateTime from_date, DateTime to_date, string plant_id);
        List<Report_NCRejection_vm> GetNCRejecetionReport(string entity, DateTime from_date, DateTime to_date);

        List<report_material_in_out_vm> MaterialInOutDetailReport(string entity, DateTime? from_date, DateTime? to_date, string plant_id, string item_id, string employee_id);
        List<Report_OEE_vm> OverallEquipmentEffectivenessReport(string entity, DateTime? from_date, DateTime? to_date, string shift_id, string item_id, string machine_id, string supervisor_id, string plant_id);
        List<fixed_asset_report_vm> FixedAssetReport(string entity, DateTime? from_date, DateTime? to_date, string depreciation_type, string asset_code_id, string depreciation_area_id, string asset_class_id, string asset_group_id);
        List<report_inventory_vm> InventoryReport1(string entity, string item_id, string process_id3);
        List<ref_mfg_process> GetOprnList();

    }
}
