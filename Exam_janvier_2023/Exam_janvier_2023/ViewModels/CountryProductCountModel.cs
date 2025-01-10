using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Exam_janvier_2023.Models;

namespace Exam_janvier_2023.ViewModels;

public class CountryProductCountModel
{
    public string Country { get; set; } = "Unknown"; // Valeur par défaut
    public int ProductCount { get; set; }
}
