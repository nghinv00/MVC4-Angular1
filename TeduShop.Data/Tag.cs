//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeduShop.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tag
    {
        public Tag()
        {
            this.PostTags = new HashSet<PostTag>();
            this.ProductTags = new HashSet<ProductTag>();
        }
    
        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<int> C_ID { get; set; }
    
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
