using System;
using System.Text.RegularExpressions;

namespace CheckWordsterForWindows.Services
{
    public class CheckWordster
    {
        string _wordDigits;

        readonly string[] _wordsForMillenia = {"thousand", "million", "billion", "trillion"};

        readonly string[] _wordsForDecadesTwentyAndAbove =
            {"", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};
        readonly string[] _wordsForBelowTenty =
        {
            "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve",
            "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
        };

        string[] _wordsForDigits = { };

        public CheckWordster(string stringOfNumberAsDigits)
        {
            _wordDigits = stringOfNumberAsDigits.Trim();
            CheckInput();
            RemoveCommas();
        }

        private void RemoveCommas()
        {
            _wordDigits = _wordDigits.Replace(",", "");
        }

        private void CheckInput()
        {
            if (_wordDigits.Equals("")) throw new Exception("CheckWordster - Null number");
            if (!Regex.IsMatch(_wordDigits, "^[0-9\\+\\-\\.\\,]+$"))
                throw new Exception("CheckWordster - Invalid characters");
            if (_wordDigits.StartsWith("+") || _wordDigits.StartsWith("-"))
            {
                if (_wordDigits.Substring(1, _wordDigits.Length-1).Contains("-"))
                    throw new Exception("CheckWordster - Invalid signed number");
                else if (_wordDigits.Substring(1, _wordDigits.Length-1).Contains("+"))
                    throw new Exception("CheckWordster - Invalid signed number");
                else throw new Exception("CheckWordster - Signed number");
            }

            if (_wordDigits.Contains(",") && _wordDigits.Contains("."))
            {
                if (!(Regex.IsMatch(_wordDigits, "^[0-9]+(,[0-9]{3})+(\\.[0-9]{0,2})$")))
                    throw new Exception("CheckWordster - Invalid format");
            }

            if (_wordDigits.Contains(",") && !_wordDigits.Contains("."))
            {
                if (!(Regex.IsMatch(_wordDigits, "^[0-9]+(,[0-9]{3})+$")))
                    throw new Exception("CheckWordster - Invalid format");
            }

            if (!_wordDigits.Contains(",") && _wordDigits.Contains("."))
            {
                if (!(Regex.IsMatch(_wordDigits, "^[0-9]+\\.[0-9]{0,2}$")))
                    throw new Exception("CheckWordster - Invalid format");
            }

            if (_wordDigits.Length > 15) throw new Exception("CheckWordster - Too many digits");
        }

        public string GetWords()
        {
            var wordsOut = "";
            var digitsIn = "";
            string decimalsOut = "";

            if (_wordDigits.Contains("."))
            {
                string[] wordPieces = _wordDigits.Split('.');
                digitsIn = wordPieces[0];
                var decimalsIn = wordPieces[1];
                if (decimalsIn.Length == 1) decimalsIn = decimalsIn + "0";
                decimalsOut = " and " + decimalsIn + "/100";
            }
            else
            {
                digitsIn = _wordDigits;
            }

            for (var m = -1; m < _wordsForMillenia.Length; m++)
            {
                while (digitsIn.Length < 3) digitsIn = "0" + digitsIn;
                string last3 = digitsIn.Substring(digitsIn.Length - 3, 3);
                string last3Out = WordsForUpTo1000(last3);
                if ((m != -1) && (!last3Out.Equals("")))
                {
                    wordsOut = last3Out + " " + _wordsForMillenia[m] + " " + wordsOut;
                }
                else
                {
                    wordsOut = (last3Out + " " + wordsOut).Trim();
                }

                digitsIn = digitsIn.Substring(0, digitsIn.Length - 3);
            }

            return wordsOut.Substring(0, 1).Trim().ToUpper() + wordsOut.Substring(1, wordsOut.Length-1).Trim() +
                   decimalsOut;
        }

        public string WordsForUpTo1000(string digits3)
        {
            string result = "";
            int[] digits = new int[3];

            while (digits3.Length < 3) digits3 = "0" + digits3;

            for (var i = 0; i < 3; i++)
            {
                digits[i] = int.Parse(digits3.Substring(i, 1 ));
            }

            if (digits[0] > 0)
            {
                result += _wordsForBelowTenty[digits[0]] + " hundred ";
            }

            if (digits[1] > 0)
            {
                if (digits[1] > 1)
                {
                    result += _wordsForDecadesTwentyAndAbove[digits[1]] + " ";
                    if (digits[2] > 0)
                    {
                        result += _wordsForBelowTenty[digits[2]] + " ";
                    }
                }
                else
                {
                    result += _wordsForBelowTenty[digits[1] * 10 + digits[2]] + " ";
                }
            }
            else
            {
                result += _wordsForBelowTenty[digits[2]];
            }

            return result.Trim();
        }
    }
}
