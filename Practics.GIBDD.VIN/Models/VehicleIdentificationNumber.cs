using System;
using System.Collections.Generic;
using System.Linq;
using Practics.GIBDD.VIN.Exceptions;
using Practics.GIBDD.VIN.Extensions;
using Practics.GIBDD.VIN.Resources;

namespace Practics.GIBDD.VIN.Models
{
    public class VehicleIdentificationNumber
    {
        private readonly List<char> ALLOWED_SYMBOLS = new List<char>()
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'J',
            'K',
            'L',
            'M',
            'N',
            'P',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z'
        };

        private const int CHECK_NUM_POSITION = 8;
        private const int VIS_YEAR_POSITION = 9;
        private const int VIN_LENGTH = 17;

        private readonly List<int?> WEIGHTS = new List<int?>() { 8, 7, 6, 5, 4, 3, 2, 10, null, 9, 8, 7, 6, 5, 4, 3, 2 };

        public string VIN { get; }
        public string Country => GetVINManufacturerCountry();
        public int Year => GetVehicleProducingYear();
        
        public VehicleIdentificationNumber(string vin)
        {
            VIN = vin;

            if (!IsCorrect(vin))
                throw new VINIsNotCorrectException();
        }

        private int GetVehicleProducingYear()
        {
            List<int> years = VISYears.VIS_YEARS[VIN[VIS_YEAR_POSITION]]
                .Where(year => year <= DateTime.Now.Year)
                .ToList();

            return years.Min();
        }
        
        private bool IsCorrect(string vin)
        {
            if (vin.Length != VIN_LENGTH)
                return false;

            if (!vin.ToList().TrueForAll(character => ALLOWED_SYMBOLS.Contains(character)))
                return false;

            List<int> numbers = vin.Select(GetIntEquivalent).ToList();
            List<int?> weights = WEIGHTS
                .ToTupledPairs(numbers)
                .Select(number => MultiplicateNullableInt(number.Item1, number.Item2))
                .Where(number => number.HasValue)
                .ToList();

            int sum = (int) weights.Sum();

            double checkSum = sum - (Math.Floor(sum / 11d) * 11);

            if (checkSum == 10)
            {
                return vin[CHECK_NUM_POSITION] == 'X';
            }

            return vin[CHECK_NUM_POSITION] == char.Parse(checkSum.ToString());
        }

        public string GetVINManufacturerCountry()
        {
            var manufacturerInfo = VIN.Substring(0, 2);

            return WMIManufacturers.WMI_INDEXES[manufacturerInfo];
        }
        
        private int GetIntEquivalent(char character)
        {
            return char.IsDigit(character) 
                ? int.Parse(character.ToString()) 
                : VINWeights.WEIGHTS[character];
        }
        
        private static int? MultiplicateNullableInt(int? a, int? b)
        {
            if (a == null)
            {
                return null;
            }
            if (b == null)
            {
                return null;
            }
            return a * b;
        }
    }
}