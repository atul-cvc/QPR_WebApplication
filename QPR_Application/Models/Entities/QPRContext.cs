﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Response;

namespace QPR_Application.Models.Entities
{
    public partial class QPRContext : DbContext
    {
        public QPRContext(DbContextOptions<QPRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<adviceofcvcqrs> adviceofcvcqrs { get; set; }
        public virtual DbSet<againstchargedtable> againstchargedtable { get; set; }
        public virtual DbSet<agewisependency> agewisependency { get; set; }
        public virtual DbSet<allegation> allegation { get; set; }
        public virtual DbSet<appellateauthoritytable> appellateauthoritytable { get; set; }
        public virtual DbSet<cacasesqpr> cacasesqpr { get; set; }
        public virtual DbSet<cdi_nom> cdi_nom { get; set; }
        public virtual DbSet<cmptable> cmptable { get; set; }
        public virtual DbSet<complain> complain { get; set; }
        public virtual DbSet<complaintsqrs> complaintsqrs { get; set; }
        public virtual DbSet<complaintsqrs123> complaintsqrs123 { get; set; }
        public virtual DbSet<cpa_complaint> cpa_complaint { get; set; }
        public virtual DbSet<csvupdate> csvupdate { get; set; }
        public virtual DbSet<cvcadvicetable> cvcadvicetable { get; set; }
        public virtual DbSet<cvo> cvo { get; set; }
        public virtual DbSet<cvo_detail> cvo_detail { get; set; }
        public virtual DbSet<cvomonthlyreport> cvomonthlyreport { get; set; }
        public virtual DbSet<departmentalproceedingsqrs> departmentalproceedingsqrs { get; set; }
        public virtual DbSet<existing_para_clause> existing_para_clause { get; set; }
        public virtual DbSet<ficasesqpr> ficasesqpr { get; set; }
        public virtual DbSet<final> final { get; set; }
        public virtual DbSet<fnsstage> fnsstage { get; set; }
        public virtual DbSet<forward> forward { get; set; }
        public virtual DbSet<forward1> forward1 { get; set; }
        public virtual DbSet<fstage> fstage { get; set; }
        public virtual DbSet<fulltimecvo> fulltimecvo { get; set; }
        public virtual DbSet<groupmap> groupmap { get; set; }
        public virtual DbSet<ind_det> ind_det { get; set; }
        public virtual DbSet<login_details> login_details { get; set; }
        public virtual DbSet<login_history> login_history { get; set; }
        public virtual DbSet<logs> logs { get; set; }
        public virtual DbSet<morecomplainee> morecomplainee { get; set; }
        public virtual DbSet<moreorganization> moreorganization { get; set; }
        public virtual DbSet<noting> noting { get; set; }
        public virtual DbSet<notingdetail> notingdetail { get; set; }
        public virtual DbSet<org_name> org_name { get; set; }
        public virtual DbSet<orgadd> orgadd { get; set; }
        public virtual DbSet<parttimecvo> parttimecvo { get; set; }
        public virtual DbSet<pend> pend { get; set; }
        public virtual DbSet<pincode> pincode { get; set; }
        public virtual DbSet<preventivevigi_a_qpr> preventivevigi_a_qpr { get; set; }
        public virtual DbSet<preventivevigi_b_qpr> preventivevigi_b_qpr { get; set; }
        public virtual DbSet<preventivevigilanceqrs> preventivevigilanceqrs { get; set; }
        public virtual DbSet<proprofile> proprofile { get; set; }
        public virtual DbSet<prosecutionsanctionsqrs> prosecutionsanctionsqrs { get; set; }
        public virtual DbSet<public_procurement> public_procurement { get; set; }
        public virtual DbSet<punitivevigilanceqrs> punitivevigilanceqrs { get; set; }
        public virtual DbSet<qpr> qpr { get; set; }
        public virtual DbSet<qpr_new> qpr_new { get; set; }
        public virtual DbSet<registration> registration { get; set; }
        public virtual DbSet<sectiondetail> sectiondetail { get; set; }
        public virtual DbSet<sectiondetail_11072018> sectiondetail_11072018 { get; set; }
        public virtual DbSet<sstage> sstage { get; set; }
        public virtual DbSet<statusofpendencyqrs> statusofpendencyqrs { get; set; }
        public virtual DbSet<suggested_para_clause> suggested_para_clause { get; set; }
        public virtual DbSet<transfer> transfer { get; set; }
        public virtual DbSet<usercomplaintdetail> usercomplaintdetail { get; set; }
        public virtual DbSet<userregister_detail> userregister_detail { get; set; }
        public virtual DbSet<vig_case> vig_case { get; set; }
        public virtual DbSet<vigilanceactivitiescvcqrs> vigilanceactivitiescvcqrs { get; set; }
        public virtual DbSet<viginvestigationqrs> viginvestigationqrs { get; set; }
        public virtual DbSet<vijclearancedetail> vijclearancedetail { get; set; }
        public virtual DbSet<vijclearanceofficerdetail> vijclearanceofficerdetail { get; set; }
        public virtual DbSet<workex> workex { get; set; }
        public virtual DbSet<Years> Years { get; set; }
        public virtual DbSet<LoginUser> LoginUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=10.25.34.120;Initial Catalog=QPR;Persist Security Info=True;User ID=sa;Password=sa@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().HasNoKey();

            modelBuilder.Entity<Years>().HasNoKey();
            
            modelBuilder.Entity<LoginUser>().HasNoKey();

            modelBuilder.Entity<qpr>().HasKey(q => q.referencenumber);

            modelBuilder.Entity<prosecutionsanctionsqrs>().HasKey(p => p.prosecutionsanctions_id);

            modelBuilder.Entity<agewisependency>().HasKey(a => a.pend_id);

            modelBuilder.Entity<viginvestigationqrs>().HasKey(v => v.viginvestigations_id);

            modelBuilder.Entity<departmentalproceedingsqrs>().HasKey(d => d.departproceedings_id);

            modelBuilder.Entity<againstchargedtable>().HasKey(a => a.pend_id);

            modelBuilder.Entity<cmptable>(entity =>
            {
                entity.Property(e => e.complaintnumber).ValueGeneratedNever();
            });

            modelBuilder.Entity<complaintsqrs>(entity =>
            {
                entity.Property(e => e.scrutinyreportpendinginvestigationtotal).IsFixedLength();
            });

            modelBuilder.Entity<complaintsqrs123>(entity =>
            {
                entity.HasKey(e => e.complaints_id)
                    .HasName("PK__complain__5C8661EE31D4F645");
            });

            modelBuilder.Entity<public_procurement>(entity =>
            {
                entity.HasKey(e => e.refno)
                    .HasName("PK__public_p__19842C59A153C2DC");
            });

            modelBuilder.Entity<qpr>(entity =>
            {
                entity.Property(e => e.referencenumber).ValueGeneratedNever();
            });

            modelBuilder.Entity<registration>(entity =>
            {
                entity.HasKey(e => e.usercode)
                    .HasName("PK_Registration_Id");

                entity.Property(e => e.usercode).ValueGeneratedNever();
            });

            modelBuilder.Entity<suggested_para_clause>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<usercomplaintdetail>(entity =>
            {
                entity.HasKey(e => e.complaintnumber)
                    .HasName("PK__usercomp__F85FF08C214FBFCB");

                entity.Property(e => e.complaintnumber).ValueGeneratedNever();
            });


            modelBuilder.Entity<orgadd>().HasKey(e=>e.Id);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}