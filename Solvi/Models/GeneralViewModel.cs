using Microsoft.AspNetCore.Mvc.Rendering;
using Solvi.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Solvi.Models
{
    public class ConnectionStringsConfig
    {
        public string DefaultConnection { get; set; } = "";
    }

    public class GeneralConfig
    {
        public bool? DemoAccount { get; set; }
        public bool ConfirmEmailToLogin { get; set; }
    }

    public class TableLengthViewModel
    {
        //public List<SelectListItem> Options { get; set; } = TableHelper.DefaultPageSizeOptions;
        public string SearchPlaceholder { get; set; } = "Search...";
        public bool ShowFilter { get; set; } = false;
        public string FilterDivId { get; set; } = "";
    }

    public class TableActionViewModel
    {
        public string TableId { get; set; } = "";
        public string Sort { get; set; } = "";
        public int Size { get; set; }
        public string Search { get; set; }= "";
        public int Page { get; set; }
    }

    public class ExportTableViewModel
    {
        public string TableId { get; set; } = "";
        /// <summary>
        /// Determines the PDF layout. Set to true for Portrait mode, false for Landscape mode.
        /// </summary>
        public bool IsPortrait { get; set; } = true;
    }

    public class ImportFromExcelError
    {
        public string? Row { get; set; }
        public List<string?> Errors { get; set; } = new List<string?>();
    }

    public class ExtractZipFileViewModel
    {
        public string ExtractPath { get; set; } = "";
        public string[] ExtractedFiles { get; set; } = new string[0];
    }

    public class Breadcrumb
    {
        public string? Url { get; set; } = "";
        public string? Name { get; set; } = "";
    }

    public class FileUploadViewModel
    {
        public string FileName { get; set; } = "";
        public string UniqueFileName { get; set; } = "";
        public string FileUrl { get; set; } = "";
    }

    public class IntChart
    {
        public string DataLabel { get; set; } = "";
        public int DataValue { get; set; }
    }

    public class SidebarItem
    {
        public string? Title { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? Icon { get; set; }
        public bool? IsActive { get; set; }
        public bool CanAccess { get; set; } = true;
    }

    //public class ColumnHtmlTemplateViewModel
    //{
    //    public List<ColumnProperty> ColumnsInCurrentRow { get; set; } = new List<ColumnProperty>();
    //    public ColumnProperty? CurrentColumn { get; set; }
    //}

    //public class TableRowsViewModel
    //{
    //    public List<ColumnProperty> TableColumns { get; set; } = new List<ColumnProperty>();
    //    public string? EditButtonUrl { get; set; }
    //    public string? DeleteButtonUrl { get; set; }
    //    public string? ConfirmDeleteTitle { get; set; }
    //}

    //public class TableViewModel
    //{
    //    public List<TableRowsViewModel> TableRows { get; set; } = new List<TableRowsViewModel>();
    //    public List<ColumnProperty> TableHeads { get; set; } = new List<ColumnProperty>();
    //    public List<object?> ColumnValues { get; set; } = new List<object?>();
    //    public int PageIndex { get;  set; }
    //    public int TotalPages { get;  set; }
    //    public int PageSize { get;  set; }
    //    public int TotalItems { get;  set; }
    //    public bool HasPreviousPage { get; set; }
    //    public bool HasNextPage { get; set; }
    //    public string SearchMessage { get;  set; } = "";
    //    public bool ShowActionColumn { get; set; } = true;
    //}

    public class TicketSettingViewModel
    {
        public string? Id { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; } = "";

        [Display(Name = "Description", ResourceType = typeof(Resources.Resource))]
        public string? Description { get; set; }

        [Display(Name = "Order", ResourceType = typeof(Resources.Resource))]
        public int? Ordering { get; set; }

        public string? CreatedBy { get; set; }
        [Display(Name = "CreatedOn", ResourceType = typeof(Resources.Resource))]
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [Display(Name = "CreatedOn", ResourceType = typeof(Resources.Resource))]
        public string? IsoUtcCreatedOn { get; set; }
        public string? IsoUtcModifiedOn { get; set; }

        [Display(Name = "Action", ResourceType = typeof(Resources.Resource))]
        public string? Action { get; set; }
    }
}
