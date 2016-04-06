namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //id是欄位預設值(0), 代表是create狀態; 否則就是已存在的id, 為update狀態
            if (this.ProductId == default(int))
            {
                //Create Data
            }
            else
            {
                //Update Date
            }

            if (this.Price > 100)
            {
                //yield return new ValidationResult("價格不可高於100元", new string[] { "Price" });
            }

            if (this.Stock > 100)
            {
                //yield return new ValidationResult("庫存不可超過100個", new string[] { "Stock" });
            }

            yield return ValidationResult.Success;
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [CheckProductNameContainTwoSpace(ErrorMessage = "產品名稱必須包含兩個空白字元")]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }        
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
