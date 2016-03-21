using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
    public class CheckProductNameContainTwoSpaceAttribute : DataTypeAttribute
    {
        public CheckProductNameContainTwoSpaceAttribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            var strProductName = (string)value;
            return strProductName.Count(p => p == ' ') == 2;
        }
    }
}