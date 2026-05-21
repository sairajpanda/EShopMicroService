using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingCartWeb.Pages;

public class ConfirmationModel : PageModel
{
    public string Message { get; set; }
    public void OnGetContact()
    {
        Message = "Your Email Was Sent.";
    }

    public void OnGetOrderSubmitted()
    {
        Message = "Your order has been submitted successfully.";
    }
}
