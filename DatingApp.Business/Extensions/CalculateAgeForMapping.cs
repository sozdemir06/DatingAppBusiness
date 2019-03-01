using System;

namespace DatingApp.Business.Extensions
{
    public static class CalculateAgeForMapping
    {
        public static int Calculate(this DateTime dateTime)
        {
            var age=DateTime.Today.Year-dateTime.Year;
            if(dateTime.AddYears(age)>DateTime.Today)
            {
                age--;
            }
            return age;
        }    
    }
}