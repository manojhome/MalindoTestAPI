using System;
using System.ComponentModel.DataAnnotations;

namespace MalindoTestAPI.Common
{

    public class DOBDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value) // Return a boolean value: true == IsValid, false != IsValid
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now; //Dates Greater than or equal to today are valid (true)

        }
    }

}
