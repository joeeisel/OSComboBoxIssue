namespace OSComboBoxIssue
{
    using System.Globalization;
    using System.Windows.Data;
    using System;

    public class ComboBoxItemNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                try
                {
                    if (targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        return null;
                    }

                    return 0;
                }
                catch
                {
                    return 0;
                }
            }

            if (value is Int32)
            {
                try
                {
                    if (targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        if ((int)value <= 0)
                        {
                            return null;
                        }
                    }
                }
                catch
                {
                }
            }
            else if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                try
                {
                    if (targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        return null;
                    }

                    return 0;
                }
                catch
                {
                    return 0;
                }
            }

            return value;
        }
    }
}