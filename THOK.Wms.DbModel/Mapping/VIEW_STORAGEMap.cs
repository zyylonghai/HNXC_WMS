﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace THOK.Wms.DbModel.Mapping
{
    public  class VIEW_STORAGEMap : EntityTypeConfiguration<VIEW_STORAGE>
    {
        public VIEW_STORAGEMap()
        {
            // Primary Key
            this.HasKey(t => t.CELL_CODE);

            // Properties
            this.Property(t => t.CELL_CODE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.PRODUCT_CODE)
                .HasMaxLength(20);

            this.Property(t => t.PRODUCT_BARCODE)
                .HasMaxLength(200);

            this.Property(t => t.BILL_NO)
                .HasMaxLength(20);

            this.Property(t => t.GRADE_CODE)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ENGLISH_CODE)
                .HasMaxLength(6);

            this.Property(t => t.USER_CODE)
                .HasMaxLength(10);

            this.Property(t => t.GRADE_NAME)
                .HasMaxLength(20);

            this.Property(t => t.MEMO)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("VIEW_STORAGE", "HNXC");
            this.Property(t => t.CELL_CODE).HasColumnName("CELL_CODE");
            this.Property(t => t.PRODUCT_CODE).HasColumnName("PRODUCT_CODE");
            this.Property(t => t.PRODUCT_BARCODE).HasColumnName("PRODUCT_BARCODE");
            this.Property(t => t.BILL_NO).HasColumnName("BILL_NO");
            this.Property(t => t.REAL_WEIGHT).HasColumnName("REAL_WEIGHT");
            this.Property(t => t.GRADE_CODE).HasColumnName("GRADE_CODE");
            this.Property(t => t.ENGLISH_CODE).HasColumnName("ENGLISH_CODE");
            this.Property(t => t.USER_CODE).HasColumnName("USER_CODE");
            this.Property(t => t.GRADE_NAME).HasColumnName("GRADE_NAME");
            this.Property(t => t.MEMO).HasColumnName("MEMO");
        }
    }
}
