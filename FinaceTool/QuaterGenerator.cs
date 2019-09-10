using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FinaceTool
{
    public static class QuaterGenerator
    {
        public static string GetQuarter(this DateTime date)
        {
            if (date.Month >= 4 && date.Month <= 6)
                return "AMJ" + date.Year.ToString().Substring(2);
            else if (date.Month >= 7 && date.Month <= 9)
                return "JAS" + date.Year.ToString().Substring(2);
            else if (date.Month >= 10 && date.Month <= 12)
                return "OND" + date.Year.ToString().Substring(2);
            else
                return "JFM" + date.Year.ToString().Substring(2);
        }

        public static void UpdateActiveQuaters()
        {
            DateTime dt = System.DateTime.Now;
            string CurrentQuater = QuaterGenerator.GetQuarter(dt);
            string FirstQuater = QuaterGenerator.GetQuarter(dt.AddMonths(3));
            string SecondQuater = QuaterGenerator.GetQuarter(dt.AddMonths(6));
            string ThiredQuater = QuaterGenerator.GetQuarter(dt.AddMonths(9));
            FinanceToolEntities obj = new FinanceToolEntities();
            obj.Usp_UpdateIsActiveQuaters(CurrentQuater, FirstQuater, SecondQuater, ThiredQuater);

        }
       

        public static bool EditRestrictionByRole(string RoleID)
        {
            bool i = true;
            string startDay = ConfigurationManager.AppSettings["StartDayKey"];
            string endDay = ConfigurationManager.AppSettings["EndDayKey"];
            int startTime = Convert.ToInt32(ConfigurationManager.AppSettings["StartTimeKey"]);
            int endTime = Convert.ToInt32(ConfigurationManager.AppSettings["EndTimeKey"]);
            string today = DateTime.Now.DayOfWeek.ToString();
            string todaytime = DateTime.Now.Hour.ToString();
            int sttime = Convert.ToInt32(todaytime);

          
                if (RoleID != "9" && ( startDay == today || endDay == today))
                {
                    if (Enumerable.Range(startTime, 24).Contains(sttime) || Enumerable.Range(0, endTime).Contains(sttime))
                    {
                        i = false;
                        return i;
                    }
                    else
                    {
                        i = true;
                        return i;
                    }
                }
                else
                {
                    i = true;
                    return i;
                }
        }
    }
}