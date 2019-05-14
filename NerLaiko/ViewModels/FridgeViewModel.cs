using System.ComponentModel.DataAnnotations;

namespace NerLaiko.ViewModels
{
    public class FridgeViewModel
    {
        [Required] public string Location { get; set; }
    }
}
