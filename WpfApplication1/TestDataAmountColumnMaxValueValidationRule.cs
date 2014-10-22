using Xceed.Wpf.DataGrid.ValidationRules;
using Xceed.Wpf.DataGrid;
using System.Globalization;
using System.Windows.Controls;

namespace WpfApplication1
{
    public class TestDataAmountColumnMaxValueValidationRule : CellValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo culture, CellValidationContext context)
        {
            decimal decimalValue;
            decimal.TryParse(value.ToString(), out decimalValue);

            return decimalValue > 50 ? new ValidationResult(false, "Bsp. Validierung: Wert darf nicht größer als 50 sein.") : ValidationResult.ValidResult;
        }

    }
}
