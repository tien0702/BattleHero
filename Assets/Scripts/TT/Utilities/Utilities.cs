using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TT
{
    public class Utilities
    {
        public static string MoneyToString(long money)
        {
            if (money >= 1000000000)
            {
                return ((float)money / 1000000000).ToString("#.##") + "B";
            }
            if (money >= 1000000)
            {
                return ((float)money / 1000000).ToString("#.##") + "M";
            }
            if (money >= 1000)
            {
                return ((float)money / 1000).ToString("#.##") + "K";
            }
            return money.ToString();
        }

        public static string ConvertToHH_MM_DD(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            string formattedTime = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
            return formattedTime;
        }

        public static string ConvertToMM_DD(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public static string FormatTime(int hours, int minutes, int seconds)
        {
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[Random.Range(0, chars.Length)]);
            }

            return stringBuilder.ToString();
        }
    }
}
