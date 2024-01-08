using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SMURF_Ava.ViewModels.Converters;

public class ImageCountToStrConverter : IValueConverter{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (string.IsNullOrEmpty((string)value)) return 0;
        int number = int.Parse((string)value) - 1;
        return "+"+ number + " pics";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}