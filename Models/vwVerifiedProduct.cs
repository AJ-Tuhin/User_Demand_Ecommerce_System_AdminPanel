//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace uCommerceMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwVerifiedProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int ConditionId { get; set; }
        public int UserId { get; set; }
        public Nullable<double> RegularPrice { get; set; }
        public Nullable<double> OfferPrice { get; set; }
        public Nullable<bool> Negotiable { get; set; }
        public string Links { get; set; }
        public string Video { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string IP { get; set; }
    }
}
