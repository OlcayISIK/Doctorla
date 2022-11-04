using Doctorla.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data
{
    public class MultiString
    {
        public MultiString()
        {
            English = "[Translation missing]";
            Turkish = "[Çeviri yok]";
            German = "[Übersetzung fehlt]";
            Spanish = "[Sin Traducción]";
        }
        public string English { get; set; }
        public string Turkish { get; set; }
        public string German { get; set; }
        public string Spanish { get; set; }

        public static void AssignValues(MultiString source, MultiString destination)
        {
            destination.English = source?.English;
            destination.German = source?.German;
            destination.Turkish = source?.Turkish;
            destination.Spanish = source?.Spanish;
        }
        /// <summary>
        /// Returns value according to language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public string GetValue(Language language)
        {
            switch (language)
            {
                case Language.English:
                    return English;
                case Language.German:
                    return German;
                case Language.Turkish:
                    return Turkish;
                case Language.Spanish:
                    return Spanish;
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
        }

        public string GetPropertyValue(Language language)
        {
            Object o = GetType().GetProperty(language.ToString())?.GetValue(this, null);
            return o?.ToString();
        }

        public static implicit operator string(MultiString v)
        {
            throw new NotImplementedException();
        }
    }
}
