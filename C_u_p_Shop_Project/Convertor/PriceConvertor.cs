namespace Crops_Shop_Project.Convertor
{
    public static class PriceConvertor
    {
        public static string ToMoreReadable(this ulong price)
        {
            List<string> result = new List<string>();
            string res;
            ulong TempPrice = price;
            ulong RemainderDividedByThousand = 0;
            int RemainderDividedByThousandNumberOfDigit;
            while (TempPrice != 0)
            {

                RemainderDividedByThousand = (TempPrice % 1000);
                RemainderDividedByThousandNumberOfDigit = RemainderDividedByThousand.NumberOfDigit();
                if (RemainderDividedByThousandNumberOfDigit == 1)
                    res = "00" + RemainderDividedByThousand;
                else if (RemainderDividedByThousandNumberOfDigit == 2)
                    res = "0" + RemainderDividedByThousand.ToString();
                else
                    res = RemainderDividedByThousand.ToString();
                if (TempPrice / 1000 == 0)
                    res = RemainderDividedByThousand.ToString();
                result.Add(res);
                TempPrice /= 1000;
            }
            string FinalResult = "";
            result.Reverse();
            for (int index = 0; index < result.Count; index++)
            {
                FinalResult += result[index];
                if (index != (result.Count) - 1)
                    FinalResult += ",";
            }
            return FinalResult;
        }
        public static int NumberOfDigit(this ulong number)
        {
            int count = 0;
            while (number > 10)
            {
                count++;
                number /= 10;
            }
            count++;
            return count;
        }
    }
}
