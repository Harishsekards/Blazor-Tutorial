﻿using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Pages
{
    public class CustomInputSelect<TValue> : InputSelect<TValue>
    {
        protected override bool TryParseValueFromString(string value, out TValue result, out string validationErrorMessage)
        {
            if (typeof(TValue) == typeof(int))
            {
                if (int.TryParse(value, out var resultInt))
                {
                    result = (TValue)(object)resultInt;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage = $"The selected value {value} is Invalid";
                    return false;
                }
            }
            else if (typeof(TValue) == typeof(Guid))
            {
                if (int.TryParse(value, out var intResult))
                {
                    result = (TValue)(object)intResult;
                    validationErrorMessage = $"Guid {value} is not an Integer.Please do something";
                    return false;
                }
                else
                {
                    return base.TryParseValueFromString(value, out result, out validationErrorMessage);
                }
            }
            else
            {
                return base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }
        }
    }
}