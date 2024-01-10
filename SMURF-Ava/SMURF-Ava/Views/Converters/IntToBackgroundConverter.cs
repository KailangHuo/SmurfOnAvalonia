using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace SMURF_Ava.ViewModels.Converters;

public class IntToBackgroundConverter : IValueConverter{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        int indexNum = (int)value;
        string backgroundStr = indexNum % 2 == 0 ? "#76292929" : "#65A8A8A8";
        return Brush.Parse(backgroundStr);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}