using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantQuar.DTO.HelperClasses
{
    public static class WeightCalculation
    {
        /// <summary>
        /// convert to total number Ton+Kilo+Gram
        /// </summary>
        /// <param name="Ton"></param>
        /// <param name="Kilo"></param>
        /// <param name="Gram"></param>
        /// <returns></returns>
        public static decimal ConvertWeightToKilo(decimal Ton, decimal Kilo, decimal Gram)
        {
            decimal weight;
            weight = (Ton * 1000) + Kilo + (Gram / 1000);
            return Math.Round(weight, 3);
        }

        /// <summary>
        /// return number to Ton+Kilo+Gram
        /// </summary>
        /// <param name="weight"></param>
        /// <returns>Ton/Kilo/Gram</returns>
        public static decimal[] ReCalculate(decimal weight)
        {
            decimal ton = Math.Truncate(weight /1000);
            weight = weight - (ton * 1000);
            decimal kilo =Math.Truncate( weight);
            decimal gram = (weight - kilo) * 1000;
            return new decimal[] { ton, kilo, gram };
        }
    }
}