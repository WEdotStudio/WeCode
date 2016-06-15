namespace Developer_Hub_For_UWP.Presentation
{
    public class ResponsiveGridView
    {
        public static double RecalculateLayoutWidth(double containerWidth, double DesiredWidth)
        {
            int _columns=0;
            if (containerWidth == 0 || DesiredWidth == 0)
            {
                return 0;
            }
            if (_columns == 0)
            {
                _columns = CalculateColumns(containerWidth, DesiredWidth);
            }
            else
            {
                var desiredColumns = CalculateColumns(containerWidth, DesiredWidth);
                if (desiredColumns != _columns)
                {
                    _columns = desiredColumns;
                }
            }
            
            double ItemWidth = (containerWidth / _columns) - 5;
            if (DesiredWidth < ItemWidth) ItemWidth = DesiredWidth;
                return ItemWidth;
        }

        private static int CalculateColumns(double containerWidth, double itemWidth)
        {
            var columns = (int)(containerWidth / itemWidth);
            if (columns == 0)
            {
                columns = 1;
            }
            return columns;
        }

    }
}
