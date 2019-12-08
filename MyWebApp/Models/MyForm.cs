using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class MyForm
    {
        [Display(Name = "メッセージ")]
        public string Message { get; set; }
    }
}