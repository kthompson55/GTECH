using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Collection_Game_Tool.Services
{
    //This is a ServiceValidator that checks the entire application (starting from MainWindow, because I called the IsValid function from there) and looks to see if and ValidationRule
    //is not met, and returns false if the validationRules are ever invalid, returns true if they are valid;
    class ServiceValidator
    {
        public static bool IsValid(DependencyObject parent)
        {
            LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                LocalValueEntry entry = localValues.Current;
                if (BindingOperations.IsDataBound(parent, entry.Property))
                {
                    Binding binding = BindingOperations.GetBinding(parent, entry.Property);
                    foreach (ValidationRule rule in binding.ValidationRules)
                    {
                        ValidationResult result = rule.Validate(parent.GetValue(entry.Property), null);
                        if (!result.IsValid)
                        {
                            BindingExpression expression = BindingOperations.GetBindingExpression(parent, entry.Property);
                            System.Windows.Controls.Validation.MarkInvalid(expression, new System.Windows.Controls.ValidationError(rule, expression, result.ErrorContent, null));
                            return false;
                        }
                    }
                }
            }

            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
