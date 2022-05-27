using Car_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_App.Helpers
{
    public static class CarDataValidator
    {
        private static bool LenghtOfData(string data, int requiredLenght)
        {
            return string.IsNullOrEmpty(data) ? false : data.Length == requiredLenght;
        }
        internal static string InputDataValidator(Car carDataToValidate)
        {
            bool IsVinValid = LenghtOfData(carDataToValidate.VIN, 17) || LenghtOfData(carDataToValidate.VIN, 7);
            bool IsRegisterNumberValid = LenghtOfData(carDataToValidate.RegistrationNumber, 7);
            if (!IsVinValid && !IsRegisterNumberValid)
            {
                carDataToValidate.RegistrationNumber = string.Empty;
                carDataToValidate.VIN = string.Empty;
                return "Registration number must contain 7 characters and VIN Number must contain 7 or 17 Characters";
            }
            if (!IsRegisterNumberValid)
            {
                carDataToValidate.VIN = string.Empty;
                return "Registration number must contain 7 characters";
            }
            if (!IsVinValid)
            {
                carDataToValidate.RegistrationNumber = string.Empty;
                return "VIN Number must contain 7 or 17 Characters";
            }
            return string.Empty;
        }
    }
}
