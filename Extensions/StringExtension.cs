using System;
using System.Linq;
using System.Text;

namespace ArtRaid.Extensions {
    public static class StringExtension {

        public static string OnlyNumbers(this string str) {
            if (str == null) return null;
            return new String(str.Where(e => Char.IsDigit(e)).ToArray());
        }

        public static string OnlyLetters(this string str) {
            if (str == null) return null;
            return new String(str.Where(e => Char.IsLetter(e)).ToArray());
        }

        public static string OnlyNumbersAndLetters(this string str) {
            if (str == null) return null;
            return new String(str.Where(e => Char.IsDigit(e) || Char.IsLetter(e)).ToArray());
        }

        /// <summary>
        ///     Compares by numbers and letters </summary>
        public static bool CompareNL(string a, string b) {
            return a?.ToLower().OnlyNumbersAndLetters() == b?.ToLower().OnlyNumbersAndLetters();
        }

        public static string OnlyFloats(this string str) {
            if (str == null) return null;
            return new String(str.Where(e => Char.IsDigit(e) || e == '.').ToArray());
        }

        public static string Truncate(this string value, int maxLength) {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string ExtendLast(this string value, int lengthString, char ch) {
            if (String.IsNullOrEmpty(value)) return value;
            if (value.Length < lengthString) {
                return value + new String(ch, lengthString - value.Length);
            } else {
                return value;
            }
        }

        public static string ExtendFirst(this string value, int lengthString, char ch) {
            if (String.IsNullOrEmpty(value)) return value;
            if (value.Length < lengthString) {
                return new String(ch, lengthString - value.Length) + value;
            } else {
                return value;
            }
        }

        public static string FormatPhone(this string tel) {
            if (tel == null) return null;
            if (tel.All(e => Char.IsDigit(e)) == false) return tel;
            if (tel.Length == 10) {
                return $"({tel.Substring(0, 3)}) {tel.Substring(3, 3)}-{tel.Substring(6, 4)}";
            }
            if (tel.Length > 10) {
                return $"({tel.Substring(0, 3)}) {tel.Substring(3, 3)}-{tel.Substring(6, 4)} ({tel.Substring(10)})";
            } else if (tel.Length == 9) {
                return $"{tel.Substring(0, 3)}-{tel.Substring(3, 3)}-{tel.Substring(6, 3)}";
            } else {
                return tel;
            }
        }

        /// <summary>
        ///     (520) 223-0230 -> 15202230230 </summary>
        public static string ToUsPhone(string phone) {
            if (phone == null) return null;
            phone = OnlyNumbers(phone);
            if (phone.Length == 10) {
                return "1" + phone;
            } else {
                return phone;
            }
        }

        /// <summary>
        ///     (520) 223-0230 -> 5202230230 </summary>
        public static string ToLocalPhone(string phone) {
            if (phone == null) return null;
            phone = OnlyNumbers(phone);
            if (phone.Length == 10) {
                return phone;
            } else
            if (phone.Length == 11) {
                return phone.Substring(1, 10);
            } else {
                return phone;
            }
        }

        public static string ToTotalSuffix(long value, bool fraction = true) {
            string[] sizes = { "", "K", "M", "G" };
            double len = (long)value;
            int order = 0;
            while (len >= 1000 && order + 1 < sizes.Length) {
                order++;
                len = len / 1000;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            if (fraction) {
                return $"{len:0.#}{sizes[order]}";
            } else {
                return $"{len:0}{sizes[order]}";
            }
        }

        public static string ToString(TimeSpan ts) {
            if (ts < TimeSpan.FromSeconds(1)) {
                return $"{ts.TotalSeconds:F1}s";
            }
            if (ts < TimeSpan.FromMinutes(1)) {
                return $"{(int)ts.TotalSeconds}s";
            }
            if (ts < TimeSpan.FromHours(1)) {
                return $"{(int)ts.TotalMinutes}m.{ts.Seconds:D2}s";
            }
            if (ts.TotalHours < 2) {
                return $"{(int)ts.TotalHours}h.{ts.Minutes:D2}m";
            }
            if (ts.TotalHours < 24) {
                return $"{(int)ts.TotalHours}h";
            }
            if (ts.TotalDays < 10) {
                return $"{ts.TotalDays:F1}d";
            } else {
                return $"{(int)ts.TotalDays}d";
            }
        }

        public static string ToDaysAgo(TimeSpan ts) {
            if (ts.TotalDays < 1) {
                return $"now";
            } else if (ts.TotalDays < 2) {
                return $"1 day";
            } else if (ts.TotalDays < 30) {
                return $"{(int)ts.TotalDays} days";
            } else if (ts.TotalDays < 30 * 2) {
                return $"1 month";
            } else if (ts.TotalDays < 365) {
                return $"{(int)(ts.TotalDays / 30)} month";
            } else if (ts.TotalDays < 365 * 2) {
                return $"1 year";
            } else {
                return $"{(int)(ts.TotalDays / 365)} years";
            }
        }

        public static string ToDateTime(TimeSpan ts) {
            if (ts.TotalDays < 1) {
                return $"now";
            } else if (ts.TotalDays < 2) {
                return $"1 day";
            } else if (ts.TotalDays < 30) {
                return $"{(int)ts.TotalDays} days";
            } else if (ts.TotalDays < 30 * 2) {
                return $"1 month";
            } else if (ts.TotalDays < 365) {
                return $"{(int)(ts.TotalDays / 30)} month";
            } else if (ts.TotalDays < 365 * 2) {
                return $"1 year";
            } else {
                return $"{(int)(ts.TotalDays / 365)} years";
            }
        }

        public static string ToMonthAgoShort(this TimeSpan ts) {
            if (ts.TotalDays < 30) {
                return $"<1m";
            } else if (ts.TotalDays < 30 * 2) {
                return $"1m";
            } else if (ts.TotalDays < 365) {
                return $"{(int)(ts.TotalDays / 30)}m";
            } else if (ts.TotalDays < 365 * 2) {
                return $"1y";
            } else {
                return $"{(int)(ts.TotalDays / 365)}y";
            }
        }

        public static string ToMonthAgo(this TimeSpan ts) {
            if (ts.TotalDays < 30) {
                return $"less than a month";
            } else if (ts.TotalDays < 30 * 2) {
                return $"1 month";
            } else if (ts.TotalDays < 365) {
                return $"{(int)(ts.TotalDays / 30)} month";
            } else if (ts.TotalDays < 365 * 2) {
                return $"1 year";
            } else {
                return $"{(int)(ts.TotalDays / 365)} years";
            }
        }

        public static string ToAgoString(this DateTime dt) {
            var local = dt.ToLocalTime();
            var now = DateTime.Now;
            var ts = now - local;
            if (ts >= TimeSpan.FromDays(365)) {
                var val = now.Year - local.Year;
                if (val == 1) {
                    return $"{val} year";
                } else {
                    return $"{val} years";
                }
            }
            if (ts >= TimeSpan.FromDays(31)) {
                var val = (now.Year - local.Year) * 12 + (now.Month - local.Month) - (local.Day >= now.Day ? 1 : 0);
                if (val == 1) {
                    return $"{val} month";
                } else {
                    return $"{val} months";
                }
            }
            if (ts >= TimeSpan.FromDays(1)) {
                var val = (int)ts.TotalDays;
                if (val == 1) {
                    return $"{val} day";
                } else {
                    return $"{val} days";
                }
            }
            if (ts >= TimeSpan.FromHours(1)) {
                var val = (int)ts.TotalHours;
                if (val == 1) {
                    return $"{val} hour";
                } else {
                    return $"{val} hours";
                }
            }
            if (ts >= TimeSpan.FromMinutes(1)) {
                var val = (int)ts.TotalMinutes;
                if (val == 1) {
                    return $"{val} minute";
                } else {
                    return $"{val} minutes";
                }
            }

            {
                var val = (int)ts.TotalSeconds;
                if (val == 1) {
                    return $"{val} second";
                } else {
                    return $"{val} seconds";
                }
            }
        }

        public static string ToAgoString(this TimeSpan ts) {
            if (ts >= TimeSpan.FromDays(1)) {
                var val = (int)ts.TotalDays;
                if (val == 1) {
                    return $"{val} day";
                } else {
                    return $"{val} days";
                }
            }
            if (ts >= TimeSpan.FromHours(1)) {
                var val = (int)ts.TotalHours;
                if (val == 1) {
                    return $"{val} hour";
                } else {
                    return $"{val} hours";
                }
            }
            if (ts >= TimeSpan.FromMinutes(1)) {
                var val = (int)ts.TotalMinutes;
                if (val == 1) {
                    return $"{val} minute";
                } else {
                    return $"{val} minutes";
                }
            }

            {
                var val = (int)ts.TotalSeconds;
                if (val == 1) {
                    return $"{val} second";
                } else {
                    return $"{val} seconds";
                }
            }
        }

        public static string DoubleToString(double percent) {
            if (Math.Abs(percent) < 1) {
                return percent.ToString("F2");
            } else if (Math.Abs(percent) < 10) {
                return percent.ToString("F1");
            } else {
                return percent.ToString("F0");
            }
        }

        public static string ReplaceAll(this string str, String oldValue, String newValue) {
            var result = str.Replace(oldValue, newValue);
            if (result != str) {
                return ReplaceAll(result, oldValue, newValue);
            } else {
                return result;
            }
        }

        public static string BinaryToString(this byte[] binary) {
            return BitConverter.ToString(binary).Replace("-", string.Empty);
        }

        public static string ToYN(this bool value) {
            return value ? "Yes" : "No";
        }

        public static string ToYN(this bool? value) {
            return value.HasValue ? value.Value ? "Yes" : "No" : "-";
        }


        public static string ToYN(this string value) {
            return !String.IsNullOrEmpty(value) ? "Yes" : "-";
        }

        public static string ToZero(this int value) {
            return value != 0 ? value.ToString() : "-";
        }

        public static string ToZero(this byte value) {
            return value != 0 ? value.ToString() : "-";
        }

        public static string ToZero(this byte? value) {
            return value != 0 && value != null ? value.ToString() : "-";
        }

        public static string ToZero(this int? value) {
            return value != 0 && value != null ? value.ToString() : "-";
        }

        public static string ToZero(this string value) {
            return String.IsNullOrEmpty(value) == false ? value : "-";
        }

        public static object ToEmpty(this object value) {
            return value != null && String.IsNullOrEmpty(value.ToString()) == false ? value : "-";
        }

        public static string ToLink(this string value) {
            if (value == null) return null;
            return new String(value.ToLower().Replace(" ", "_").Where(e => e == '_' || Char.IsDigit(e) || Char.IsLetter(e)).ToArray());
        }

        public static string ToHTML(this string message) {
            return message.Replace("\n", "<br/>").Replace("  ", "&nbsp&nbsp");
        }

        public static int? ParseCurrency(this string value) {
            value = new String(value.Where(e => Char.IsDigit(e)).ToArray());
            if (String.IsNullOrEmpty(value)) {
                return null;
            }
            return Int32.Parse(value);
        }
    }

    /// <summary>
    ///     Short Name </summary>
    public static class StrExt {
        public static object ToEmpty(object value) {
            return StringExtension.ToEmpty(value);
        }
    }
}
