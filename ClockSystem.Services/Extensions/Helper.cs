namespace ClockSystemCA_AndrewByrne.Extensions
{
    public class Helper
    {
        public static DateTime GetStartOfWeek(DateTime selectedDate)
        {
            // Keep checking the current date of the week and subtract until it is Mon
            var curDay = selectedDate.DayOfWeek;
            while (curDay != DayOfWeek.Monday)
            {
                selectedDate = selectedDate.AddDays(-1);

                curDay = selectedDate.DayOfWeek;
            }

            return selectedDate;
        }
    }
}
