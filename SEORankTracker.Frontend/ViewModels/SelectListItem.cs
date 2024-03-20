namespace SEORankTracker.Frontend.ViewModels;

// Taken from: Assembly System.Web.WebPages

//
// Summary:
//     Represents an item in an HTML select list.
public sealed class SelectListItem
{
    //
    // Summary:
    //     Gets or sets the text that is used to display the System.Web.WebPages.Html.SelectListItem
    //     instance on a web page.
    //
    // Returns:
    //     The text that is used to display the select list item.
    public required string Text { get; init; }

    //
    // Summary:
    //     Gets or sets the value of the HTML value attribute of the HTML option element
    //     that is associated with the System.Web.WebPages.Html.SelectListItem instance.
    //
    //
    // Returns:
    //     The value of the HTML value attribute that is associated with the select list
    //     item.
    public required string Value { get; init; }

    //
    // Summary:
    //     Gets or sets a value that indicates whether the System.Web.WebPages.Html.SelectListItem
    //     instance is selected.
    //
    // Returns:
    //     true if the select list item is selected; otherwise, false.
    public bool Selected { get; init; }
}
