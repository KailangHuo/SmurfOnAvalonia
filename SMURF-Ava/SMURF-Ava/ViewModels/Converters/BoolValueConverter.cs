using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SMURF_Ava.ViewModels.Converters;

public class BoolValueConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        bool boo = (bool)value;
        if (boo) return 100;
        else return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}