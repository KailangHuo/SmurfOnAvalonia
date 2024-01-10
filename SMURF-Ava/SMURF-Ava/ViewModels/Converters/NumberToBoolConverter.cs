using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SMURF_Ava.ViewModels.Converters;

public class NumberToBoolConverter : IValueConverter{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        int number = (int)value;
        return number <= 1;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}