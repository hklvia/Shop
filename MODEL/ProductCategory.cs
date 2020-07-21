//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ProductCategory : BaseModel
    {
        [Required(ErrorMessage = "分类名称不能为空")]
        [StringLength(50, ErrorMessage = "分类名称最大长度不能超过50")]
        public string Name { get; set; }

        public string Img { get; set; }
        public Nullable<int> PID { get; set; }

        [Required(ErrorMessage = "排序号不能为空")]
        [Range(1, 10000, ErrorMessage = "排序号范围必须在1-10000之间")]
        public Nullable<int> OrderNum { get; set; }

        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }

        public string KeyWords { get; set; }
        public string Path { get; set; }
    }
}
