using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SMURF_Ava.ViewModels.Converters;

public class CollectionCountToBoolConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        int count = (int)value;
        return count <= 1;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}