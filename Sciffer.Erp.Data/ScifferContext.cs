using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sciffer.Erp.Domain.Model;
using Sciffer.Erp.Domain.ViewModel;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;

namespace Sciffer.Erp.Data
{
    public class ScifferContext : IdentityDbContext<ApplicationUser>
    {

        public ScifferContext()
            : base("ERPDbContext", throwIfV1Schema: false)
        {
            // Get the ObjectContext related to this DbContext
            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 180;
        }

        public static ScifferContext Create()
        {

            return new ScifferContext();
        }
        public DbSet<inv_item_transaction> inv_item_transaction { get; set; }
        public DbSet<ref_cycle_time> ref_cycle_time { get; set; }
        public DbSet<ref_send_mail> ref_send_mail { get; set; }
        public DbSet<ref_task_type> ref_task_type { get; set; }
        public DbSet<ref_task_periodicity> ref_task_periodicity { get; set; }
        public DbSet<ref_task_log> ref_task_log { get; set; }
        public DbSet<ref_task> ref_task { get; set; }
        public DbSet<pur_srn> pur_srn { get; set; }
        public DbSet<pur_srn_detail> pur_srn_detail { get; set; }
        public DbSet<ref_easy_hr_data> ref_easy_hr_data { get; set; }
        public DbSet<material_requision_note_user_approval> material_requision_note_user_approval { get; set; }
        public DbSet<ref_customer_complaint> ref_customer_complaint { get; set; }
        public DbSet<ref_fin_template_gl_mapping> ref_fin_template_gl_mapping { get; set; }
        public DbSet<REF_VENDOR_CONTACTS> REF_VENDOR_CONTACTS { get; set; }
        public DbSet<ref_role_dashboard_mapping> ref_role_dashboard_mapping { get; set; }
        public DbSet<ref_dashboard> ref_dashboard { get; set; }
        public DbSet<pur_po_detail_tax_element> pur_po_detail_tax_element { get; set; }
        public DbSet<ref_mode_of_transport> ref_mode_of_transport { get; set; }
        public DbSet<ref_sac> ref_sac { get; set; }
        public DbSet<ref_gst_tds_code> ref_gst_tds_code { get; set; }
        public DbSet<ref_gst_tds_code_detail> ref_gst_tds_code_detail { get; set; }
        public DbSet<ref_gst_customer_type> ref_gst_customer_type { get; set; }
        public DbSet<ref_gst_applicability> ref_gst_applicability { get; set; }
        public DbSet<pur_pi_return> pur_pi_return { get; set; }
        public DbSet<pur_pi_return_form> pur_pi_return_form { get; set; }
        public DbSet<fin_internal_reconcile> fin_internal_reconcile { get; set; }
        public DbSet<fin_bank_reco> fin_bank_reco { get; set; }
        public DbSet<fin_contra_entry> fin_contra_entry { get; set; }
        public DbSet<fin_ledger_balance> fin_ledger_balance { get; set; }
        public DbSet<ref_item_plant_valuation> ref_item_plant_valuation { get; set; }
        public DbSet<pur_grn_detail_tax_element> pur_grn_detail_tax_element { get; set; }
        public DbSet<sal_si_detail_tax_element> sal_si_detail_tax_element { get; set; }
        public DbSet<ref_hsn_code> ref_hsn_code { get; set; }
        public DbSet<ref_item_category_gl> ref_item_category_gl { get; set; }
        public DbSet<inv_item_inventory> inv_item_inventory { get; set; }
        public DbSet<ref_tax_charged_on> ref_tax_charged_on { get; set; }
        public DbSet<REF_FINANCIAL_YEAR> REF_FINANCIAL_YEAR { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<REF_CATEGORY> REF_CATEGORY { get; set; }
        public DbSet<REF_COUNTRY> REF_COUNTRY { get; set; }
        public DbSet<pur_requisition> pur_requisition { get; set; }
        public DbSet<pur_requisition_detail> pur_requisition_detail { get; set; }
        public DbSet<REF_STATE> REF_STATE { get; set; }
        public DbSet<goods_issue> goods_issue { get; set; }
        public DbSet<goods_issue_detail> goods_issue_detail { get; set; }
        public DbSet<goods_receipt> goods_receipt { get; set; }
        public DbSet<goods_receipt_detail> goods_receipt_detail { get; set; }
        public DbSet<REF_CURRENCY> REF_CURRENCY { get; set; }
        public DbSet<pla_transfer> pla_transfer { get; set; }
        public DbSet<pla_transfer_detail> pla_transfer_detail { get; set; }
        public DbSet<REF_ENTITY_TYPE> REF_ENTITY_TYPE { get; set; }
        public DbSet<inventory_revaluation> inventory_revaluation { get; set; }
        public DbSet<inventory_revaluation_detail> inventory_revaluation_detail { get; set; }
        public DbSet<REF_ORG_TYPE> REF_ORG_TYPE { get; set; }
        public DbSet<ref_module> ref_module { get; set; }
        public DbSet<REF_FREIGHT_TERMS> REF_FREIGHT_TERMS { get; set; }
        public DbSet<ref_module_form> ref_module_form { get; set; }
        public DbSet<REF_PAYMENT_TERMS> REF_PAYMENT_TERMS { get; set; }
        public DbSet<ref_exchange_rate> ref_exchange_rate { get; set; }
        public DbSet<ref_machine> ref_machine { get; set; }
        public DbSet<ref_machine_details> ref_machine_details { get; set; }
        public DbSet<ref_machine_category> ref_machine_category { get; set; }
        public DbSet<fin_ledger> fin_ledger { get; set; }
        public DbSet<pur_pi> pur_pi { get; set; }
        public DbSet<pur_pi_detail> pur_pi_detail { get; set; }
        public DbSet<ref_plan_maintenance> ref_plan_maintenance { get; set; }
        public DbSet<ref_plan_maintenance_detail> ref_plan_maintenance_detail { get; set; }
        public DbSet<ref_maintenance_type> ref_maintenance_type { get; set; }
        public DbSet<ref_plan_maintenance_component> ref_plan_maintenance_component { get; set; }
        public DbSet<ref_plan_maintenance_schedule> ref_plan_maintenance_schedule { get; set; }

        public DbSet<ref_plan_maintenance_order> ref_plan_maintenance_order { get; set; }
        public DbSet<ref_plan_maintenance_order_cost> ref_plan_maintenance_order_cost { get; set; }
        public DbSet<ref_plan_maintenance_order_parameter> ref_plan_maintenance_order_parameter { get; set; }
        public DbSet<ref_parameter_list> ref_parameter_list { get; set; }
        public DbSet<REF_PAYMENT_CYCLE> REF_PAYMENT_CYCLE { get; set; }

        public DbSet<ref_user_management> ref_user_management { get; set; }
        public DbSet<ref_user_role_mapping> ref_user_role_mapping { get; set; }
        public DbSet<ref_user_role_rights> ref_user_role_rights { get; set; }

        public DbSet<ref_user_management_role> ref_user_management_role { get; set; }
        public DbSet<ref_value_slab_po_approval> ref_value_slab_po_approval { get; set; }

        public DbSet<pur_po> pur_po { get; set; }
        public DbSet<pur_po_temp_close_cancel> pur_po_temp_close_cancel { get; set; }
        public DbSet<pur_po_detail> pur_po_detail { get; set; }
        public DbSet<pur_po_attribute_value> pur_po_attribute_value { get; set; }
        public DbSet<pur_po_form> pur_po_form { get; set; }
        public DbSet<REF_PAYMENT_CYCLE_TYPE> REF_PAYMENT_CYCLE_TYPE { get; set; }
        public DbSet<ref_account_assignment> ref_account_assignment { get; set; }
        public DbSet<ref_status> ref_status { get; set; }
        public DbSet<REF_PRIORITY> REF_PRIORITY { get; set; }
        public DbSet<ref_bank> ref_bank { get; set; }
        public DbSet<REF_TDS_SECTION> REF_TDS_SECTION { get; set; }
        public DbSet<REF_DEPARTMENT> REF_DEPARTMENT { get; set; }
        public DbSet<REF_TERRITORY> REF_TERRITORY { get; set; }
        public DbSet<REF_DIVISION> REF_DIVISION { get; set; }
        public DbSet<REF_UOM> REF_UOM { get; set; }
        public DbSet<pur_grn> pur_grn { get; set; }
        public DbSet<pur_grn_attribute> pur_grn_attribute { get; set; }
        public DbSet<pur_grn_detail> pur_grn_detail { get; set; }
        public DbSet<pur_grn_form> pur_grn_form { get; set; }
        public DbSet<ref_party_type> ref_party_type { get; set; }
        public DbSet<ref_shifts> ref_shifts { get; set; }
        public DbSet<ref_tax_type> ref_tax_type { get; set; }
        public DbSet<REF_CUSTOMER_PARENT> REF_CUSTOMER_PARENT { get; set; }
        public DbSet<ref_frequency> ref_frequency { get; set; }
        public DbSet<REF_CUSTOMER_CATEGORY> REF_CUSTOMER_CATEGORY { get; set; }
        public DbSet<journal_entry> journal_entry { get; set; }
        public DbSet<journal_entry_detail> journal_entry_detail { get; set; }
        public DbSet<REF_CUSTOMER> REF_CUSTOMER { get; set; }
        public DbSet<ref_gl_acount_type> ref_gl_acount_type { get; set; }
        public DbSet<REF_CUSTOMER_CONTACTS> REF_CUSTOMER_CONTACTS { get; set; }
        public DbSet<ref_posting_periods> ref_posting_periods { get; set; }
        public DbSet<ref_posting_periods_detail> ref_posting_periods_detail { get; set; }
        public DbSet<ref_general_ledger> ref_general_ledger { get; set; }
        public DbSet<ref_batch_numbering> ref_batch_numbering { get; set; }
        public DbSet<REF_USER> REF_USER { get; set; }
        public DbSet<ref_cost_center> ref_cost_center { get; set; }
        public DbSet<ref_sub_ledger> REF_SUB_LEDGER { get; set; }
        public DbSet<ref_item_alternate_UOM> ref_item_alternate_UOM { get; set; }
        public DbSet<REF_ATTRIBUTES> REF_ATTRIBUTES { get; set; }
        public DbSet<ref_item_type> ref_item_type { get; set; }
        public DbSet<ref_budget_master> ref_budget_master { get; set; }
        public DbSet<REF_VENDOR_ATTRIBUTE_VALUE> REF_VENDOR_ATTRIBUTE_VALUE { get; set; }
        public DbSet<ref_instruction_type> ref_instruction_type { get; set; }
        public DbSet<REF_VENDOR_CATEGORY> REF_VENDOR_CATEGORY { get; set; }
        public DbSet<ref_price_list_vendor> ref_price_list_vendor { get; set; }
        public DbSet<REF_VENDOR_PARENT> REF_VENDOR_PARENT { get; set; }
        public DbSet<ref_price_list_vendor_details> ref_price_list_vendor_details { get; set; }
        public DbSet<REF_VENDOR> REF_VENDOR { get; set; }
        public DbSet<ref_tax_detail> ref_tax_detail { get; set; }
        public DbSet<REF_ITEM_ACCOUNTING> REF_ITEM_ACCOUNTING { get; set; }
        public DbSet<ref_vendor_item_group> ref_vendor_item_group { get; set; }
        public DbSet<REF_ITEM_CATEGORY> REF_ITEM_CATEGORY { get; set; }
        public DbSet<REF_ITEM_GROUP> REF_ITEM_GROUP { get; set; }
        public DbSet<ref_ledger_account_type> ref_ledger_account_type { get; set; }
        public DbSet<REF_ITEM_VALUATION> REF_ITEM_VALUATION { get; set; }
        public DbSet<ref_amount_type> ref_amount_type { get; set; }
        public DbSet<REF_EXCISE_CATEGORY> REF_EXCISE_CATEGORY { get; set; }
        public DbSet<ref_employee_balance_detail> ref_employee_balance_detail { get; set; }
        public DbSet<REF_BRAND> REF_BRAND { get; set; }
        public DbSet<ref_employee_balance> ref_employee_balance { get; set; }
        public DbSet<REF_ITEM> REF_ITEM { get; set; }
        public DbSet<ref_customer_balance> ref_customer_balance { get; set; }
        public DbSet<REF_SALES_CATEGORY> REF_SALES_CATEGORY { get; set; }
        public DbSet<ref_vendor_balance_details> ref_vendor_balance_details { get; set; }
        public DbSet<ref_customer_balance_details> ref_customer_balance_details { get; set; }
        public DbSet<ref_vendor_balance> ref_vendor_balance { get; set; }
        public DbSet<ref_general_ledger_balance> ref_general_ledger_balance { get; set; }
        public DbSet<ref_price_list_customer> ref_price_list_customer { get; set; }
        public DbSet<ref_general_ledger_balance_details> ref_general_ledger_balance_details { get; set; }
        public DbSet<ref_price_list_customer_details> ref_price_list_customer_details { get; set; }
        public DbSet<REF_BUSINESS_UNIT> REF_BUSINESS_UNIT { get; set; }
        public DbSet<REF_MARITAL_STATUS> REF_MARITAL_STATUS { get; set; }
        public DbSet<REF_PLANT> REF_PLANT { get; set; }
        public DbSet<REF_PLANT_ATTRIBUTE> REF_PLANT_ATTRIBUTE { get; set; }
        public DbSet<REF_BRANCH> REF_BRANCH { get; set; }
        public DbSet<REF_FORM> REF_FORM { get; set; }
        public DbSet<ref_tax> ref_tax { get; set; }
        public object Project()
        {
            throw new NotImplementedException();
        }
        public DbSet<ref_item_parameter> ref_item_parameter { get; set; }
        public DbSet<REF_SALUTATION> REF_SALUTATION { get; set; }
        public DbSet<SAL_QUOTATION> SAL_QUOTATION { get; set; }
        public DbSet<SAL_QUOTATION_DETAIL> SAL_QUOTATION_DETAIL { get; set; }
        public DbSet<sal_quotation_detail_tax> sal_quotation_detail_tax { get; set; }
        public DbSet<SAL_QUOTATION_FORM> SAL_QUOTATION_FORM { get; set; }
        public DbSet<REF_GRADE> REF_GRADE { get; set; }
        public DbSet<sal_so> SAL_SO { get; set; }
        public DbSet<ref_shelf_life> ref_shelf_life { get; set; }
        public DbSet<sal_so_detail> SAL_SO_DETAIL { get; set; }
        public DbSet<sal_so_form> SAL_SO_FORM { get; set; }
        public DbSet<sal_si> SAL_SI { get; set; }
        public DbSet<sal_si_detail> SAL_SI_DETAIL { get; set; }
        public DbSet<sal_si_form> SAL_SI_FORM { get; set; }
        public DbSet<REF_STORAGE_LOCATION> REF_STORAGE_LOCATION { get; set; }
        public DbSet<REF_EMPLOYEE> REF_EMPLOYEE { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_document_numbring> ref_document_numbring { get; set; }
        public DbSet<REF_SOURCE> REF_SOURCE { get; set; }
        public DbSet<REF_COMPANY> REF_COMPANY { get; set; }
        public DbSet<REF_DESIGNATION> REF_DESIGNATION { get; set; }
        public DbSet<REF_GENDER> REF_GENDER { get; set; }
        public DbSet<REF_PAYMENT_TYPE> REF_PAYMENT_TYPE { get; set; }
        public DbSet<REF_ACCOUNT_TYPE> REF_ACCOUNT_TYPE { get; set; }
        public DbSet<REF_REASON_DETERMINATION> REF_REASON_DETERMINATION { get; set; }
        public DbSet<ref_bank_account> ref_bank_account { get; set; }
        public DbSet<MFG_BATCH> MFG_BATCH { get; set; }
        public DbSet<ref_credit_card> ref_credit_card { get; set; }
        public DbSet<REF_PAYMENT_TERMS_DUE_DATE> REF_PAYMENT_TERMS_DUE_DATE { get; set; }
        public DbSet<ref_sales_rm> ref_sales_rm { get; set; }

        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_tax_element> ref_tax_element { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_tax_element_detail> ref_tax_element_detail { get; set; }

        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_tds_code> ref_tds_code { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_tds_code_detail> ref_tds_code_detail { get; set; }

        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_workflow> ref_workflow { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_workflow_detail> ref_workflow_detail { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_workflow_approval> ref_workflow_approval { get; set; }

        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_document_type> ref_document_type { get; set; }
        public DbSet<ref_validation> ref_validation { get; set; }
        public DbSet<ref_validation_gl> ref_validation_gl { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.ViewModel.ref_posting_periods_vm> ref_posting_periods_vm { get; set; }
        public DbSet<ref_bucket> ref_bucket { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_inventory_balance> ref_inventory_balance { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_inventory_balance_details> ref_inventory_balance_details { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.pur_incoming_excise> pur_incoming_excise { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.pur_incoming_excise_detail> pur_incoming_excise_detail { get; set; }
        public DbSet<pur_qa> pur_qa { get; set; }
        public DbSet<pur_qa_detail> pur_qa_detail { get; set; }
        public DbSet<fin_ledger_payment> fin_ledger_payment { get; set; }
        public DbSet<fin_ledger_payment_detail> fin_ledger_payment_detail { get; set; }
        public DbSet<inv_item_batch> inv_item_batch { get; set; }
        public DbSet<inv_item_batch_detail> inv_item_batch_detail { get; set; }
        public DbSet<in_jobwork_in_detail> in_jobwork_in_detail { get; set; }
        public DbSet<in_jobwork_in> in_jobwork_in { get; set; }
        public DbSet<ref_cash_account> ref_cash_account { get; set; }
        public DbSet<material_requision_note> material_requision_note { get; set; }
        public DbSet<material_requision_note_detail> material_requision_note_detail { get; set; }
        public DbSet<map_mfg_process_machine> map_mfg_process_machine { get; set; }
        public DbSet<mfg_process_sequence> mfg_process_sequence { get; set; }
        public DbSet<mfg_process_sequence_detail> mfg_process_sequence_detail { get; set; }
        public DbSet<mfg_qc> mfg_qc { get; set; }
        public DbSet<mfg_qc_parameter> mfg_qc_parameter { get; set; }
        public DbSet<mfg_tag_numbering> mfg_tag_numbering { get; set; }
        public DbSet<ref_mfg_process> ref_mfg_process { get; set; }
        public DbSet<mfg_prod_order> mfg_prod_order { get; set; }
        public DbSet<mfg_prod_order_detail> mfg_prod_order_detail { get; set; }
        public DbSet<mfg_prod_order_bom> mfg_prod_order_bom { get; set; }
        public DbSet<fin_credit_debit_note> fin_credit_debit_note { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.ref_mfg_bom> ref_mfg_bom { get; set; }
        public DbSet<ref_mfg_bom_detail> ref_mfg_bom_detail { get; set; }
        public DbSet<prod_issue> prod_issue { get; set; }
        public DbSet<prod_issue_detail> prod_issue_detail { get; set; }
        public DbSet<pur_pi_form> pur_pi_form { get; set; }
        public DbSet<inv_Inventory_stock> inv_Inventory_stock { get; set; }
        public DbSet<inv_Inventory_stock_detail> inv_Inventory_stock_detail { get; set; }
        public DbSet<inv_material_out> inv_material_out { get; set; }
        public DbSet<inv_material_out_detail> inv_material_out_detail { get; set; }
        public DbSet<inv_material_in> inv_material_in { get; set; }
        public DbSet<inv_material_in_detail> inv_material_in_detail { get; set; }

        public DbSet<mfg_prod_order_detail_tag> mfg_prod_order_detail_tag { get; set; }
        public DbSet<mfg_machine_task> mfg_machine_task { get; set; }
        public DbSet<mfg_machine_task_op_qc_detail> mfg_machine_task_op_qc_detail { get; set; }
        public DbSet<mfg_qc_rule> mfg_qc_rule { get; set; }
        public DbSet<mfg_op_qc_parameter> mfg_op_qc_parameter { get; set; }
        public DbSet<mfg_op_qc_parameter_list> mfg_op_qc_parameter_list { get; set; }
        public DbSet<mfg_qc_qc_parameter> mfg_qc_qc_parameter { get; set; }
        public DbSet<mfg_qc_qc_parameter_list> mfg_qc_qc_parameter_list { get; set; }
        public DbSet<ref_mfg_qc_reason> ref_mfg_qc_reason { get; set; }
        public DbSet<mfg_machine_task_qc_qc> mfg_machine_task_qc_qc { get; set; }
        public DbSet<mfg_machine_task_qc_qc_detail> mfg_machine_task_qc_qc_detail { get; set; }

        public DbSet<ref_mfg_machine_task_status> ref_mfg_machine_task_status { get; set; }
        public DbSet<mfg_item_status_log> mfg_item_status_log { get; set; }

        public DbSet<inv_item_batch_detail_tag> inv_item_batch_detail_tag { get; set; }
        public DbSet<prod_receipt> prod_receipt { get; set; }
        public System.Data.Entity.DbSet<Sciffer.Erp.Domain.Model.inter_pla_transfer> intra_pla_transfer { get; set; }
        public DbSet<inter_pla_transfer_detail> intra_pla_transfer_detail { get; set; }
        public DbSet<inter_pla_transfer_detail_tag> intra_pla_transfer_detail_tag { get; set; }
        public DbSet<mfg_process_seq_alt> mfg_process_seq_alt { get; set; }

        public DbSet<ref_tool> ref_tool { get; set; }
        public DbSet<ref_tool_life> ref_tool_life { get; set; }
        public DbSet<ref_tool_renew_type> ref_tool_renew_type { get; set; }
        public DbSet<ref_tool_machine_item_usage> ref_tool_machine_item_usage { get; set; }
        public DbSet<ref_tool_machine_usage> ref_tool_machine_usage { get; set; }
        public DbSet<Ref_permit_template> Ref_permit_template { get; set; }
        public DbSet<ref_plant_notification> ref_plant_notification { get; set; }
        public DbSet<REF_NOTIFICATION_TYPE> REF_NOTIFICATION_TYPE { get; set; }
        public DbSet<Ref_checkpoints> Ref_checkpoints { get; set; }
        public DbSet<ref_pm_notification> ref_pm_notification { get; set; }
        public DbSet<ref_plan_breakdown_order> ref_plan_breakdown_order { get; set; }
        public DbSet<map_machine_breakdown_order> map_machine_breakdown_order { get; set; }
        public DbSet<ref_permit_issue> ref_permit_issue { get; set; }
        public DbSet<temp> temp { get; set; }
        public DbSet<rejection_receipt> rejection_receipt { get; set; }
        public DbSet<mfg_tag_numbering_ref> mfg_tag_numbering_ref { get; set; }
        public DbSet<ref_item_wip_valuation> ref_item_wip_valuation { get; set; }
        public DbSet<mfg_operator_shift> mfg_operator_shift { get; set; }
        public DbSet<mfg_incentive_rule> mfg_incentive_rule { get; set; }
        public DbSet<jobwork_rejection> jobwork_rejection { get; set; }
        public DbSet<ref_plan_breakdown_cost> ref_plan_breakdown_cost { get; set; }
        public DbSet<ref_cancellation_reason> ref_cancellation_reason { get; set; }
        public DbSet<ref_mfg_nc_status> ref_mfg_nc_status { get; set; }
        public DbSet<mfg_rejection_detail> mfg_rejection_detail { get; set; }
        public DbSet<sal_si_return> sal_si_return { get; set; }
        public DbSet<jobwork_rejection_detail> jobwork_rejection_detail { get; set; }
        public DbSet<map_operation_operator> map_operation_operator { get; set; }
        public DbSet<ref_fin_template> ref_fin_template { get; set; }
        public DbSet<ref_fin_template_detail> ref_fin_template_detail { get; set; }
        public DbSet<mfg_machine_item_upgradation> mfg_machine_item_upgradation { get; set; }
        public DbSet<ref_tool_operation_map> ref_tool_operation_map { get; set; }
        public DbSet<ref_tool_usage_type> ref_tool_usage_type { get; set; }
        public DbSet<ref_tool_category> ref_tool_category { get; set; }
        public DbSet<pur_po_user_approval> pur_po_user_approval { get; set; }
        public DbSet<update_stock_count> update_stock_count { get; set; }
        public DbSet<post_variances> post_variances { get; set; }
        public DbSet<create_stock_sheet_detail> create_stock_sheet_detail { get; set; }
        public DbSet<create_stock_sheet> create_stock_sheet { get; set; }
        public DbSet<stock_take_blocked> stock_take_blocked { get; set; }
        public DbSet<ref_mfg_incentive_benchmark> ref_mfg_incentive_benchmark { get; set; }
        public DbSet<ref_mfg_operator_incentive_appl> ref_mfg_operator_incentive_appl { get; set; }
        public DbSet<ref_mfg_multi_machining> ref_mfg_multi_machining { get; set; }
        public DbSet<mfg_operator_incentive> mfg_operator_incentive { get; set; }
        public DbSet<ref_incentive_status> ref_incentive_status { get; set; }
        public DbSet<ref_mfg_incentive_holiday> ref_mfg_incentive_holiday { get; set; }
        public DbSet<ref_asset_group> ref_asset_group { get; set; }
        public DbSet<ref_asset_master_data> ref_asset_master_data { get; set; }
        public DbSet<ref_asset_master_data_dep_parameter> ref_asset_master_data_dep_parameter { get; set; }
        public DbSet<fin_ledger_capitalization> fin_ledger_capitalization { get; set; }
        public DbSet<ref_dep_posting_period> ref_dep_posting_period { get; set; }
        public DbSet<ref_asset_class> ref_asset_class { get; set; }
        public DbSet<ref_asset_class_depreciation> ref_asset_class_depreciation { get; set; }
        public DbSet<ref_dep_area> ref_dep_area { get; set; }
        public DbSet<ref_dep_type> ref_dep_type { get; set; }
        public DbSet<ref_asset_class_gl> ref_asset_class_gl { get; set; }

        public DbSet<ref_asset_transaction> ref_asset_transaction { get; set; }
        public DbSet<ref_asset_current_data> ref_asset_current_data { get; set; }
        public DbSet<ref_asset_initial_data_header> ref_asset_initial_data_header { get; set; }
        public DbSet<ref_asset_initial_data> ref_asset_initial_data { get; set; }
        public DbSet<prod_plan> prod_plan { get; set; }
        public DbSet<prod_plan_detail> prod_plan_detail { get; set; }
        public DbSet<prod_downtime> prod_downtime { get; set; }
        public DbSet<ref_level> ref_level { get; set; }
        public DbSet<ref_operator_level_mapping> ref_operator_level_mapping { get; set; }
        public DbSet<ref_machine_level_mapping> ref_machine_level_mapping { get; set; }
        public DbSet<operator_change_request> operator_change_request { get; set; }
        public DbSet<mfg_shiftwise_production_master> _Shiftwise_Production_Masters { get; set; }
        public DbSet<ref_temp_operator_level_mapping> ref_temp_operator_level_mapping { get; set; }

    }
}
