﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCompany.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyEntities : DbContext
    {
        public MyEntities()
            : base("name=MyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<inPurchase> inPurchase { get; set; }
        public virtual DbSet<inPurchaseDetail> inPurchaseDetail { get; set; }
        public virtual DbSet<tblPartner> tblPartner { get; set; }
        public virtual DbSet<tblUser> tblUser { get; set; }
        public virtual DbSet<inExpend> inExpend { get; set; }
        public virtual DbSet<tblCampaign> tblCampaign { get; set; }
        public virtual DbSet<inExpendDetail> inExpendDetail { get; set; }
        public virtual DbSet<tblProductHistory> tblProductHistory { get; set; }
        public virtual DbSet<tblProduct> tblProduct { get; set; }
        public virtual DbSet<View_Product> View_Product { get; set; }
        public virtual DbSet<tblSupplier> tblSupplier { get; set; }
        public virtual DbSet<View_Expend> View_Expend { get; set; }
        public virtual DbSet<View_ExpendDetail> View_ExpendDetail { get; set; }
    }
}