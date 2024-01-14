﻿// <auto-generated />
using System;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MyDataBaseContext))]
    [Migration("20210212093730_CorrectionDesignModel")]
    partial class CorrectionDesignModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BusinessTypeCampaign", b =>
                {
                    b.Property<int>("CampaignBusinessTypesId")
                        .HasColumnType("int");

                    b.Property<int>("CampaignsId")
                        .HasColumnType("int");

                    b.HasKey("CampaignBusinessTypesId", "CampaignsId");

                    b.HasIndex("CampaignsId");

                    b.ToTable("BusinessTypeCampaign");
                });

            modelBuilder.Entity("CampaignProductType", b =>
                {
                    b.Property<int>("ProductTypeCampaignsId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTypesId")
                        .HasColumnType("int");

                    b.HasKey("ProductTypeCampaignsId", "ProductTypesId");

                    b.HasIndex("ProductTypesId");

                    b.ToTable("CampaignProductType");
                });

            modelBuilder.Entity("CampaignTown", b =>
                {
                    b.Property<int>("CampaignTownsId")
                        .HasColumnType("int");

                    b.Property<int>("CampaignsId")
                        .HasColumnType("int");

                    b.HasKey("CampaignTownsId", "CampaignsId");

                    b.HasIndex("CampaignsId");

                    b.ToTable("CampaignTown");
                });

            modelBuilder.Entity("Core.Models.BusinessType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("MapCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("businessTypes");
                });

            modelBuilder.Entity("Core.Models.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExecutionDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("ForecastBudget")
                        .HasColumnType("real");

                    b.Property<string>("Goal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PenetraionRate")
                        .HasColumnType("int");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalCost")
                        .HasColumnType("real");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RegionId");

                    b.HasIndex("UserId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("Core.Models.CampaignBusiness", b =>
                {
                    b.Property<int>("BusinessTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CompagnId")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("BusinessTypeId", "CompagnId");

                    b.HasIndex("CompagnId");

                    b.ToTable("CompaignBusinesses");
                });

            modelBuilder.Entity("Core.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxIdNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Core.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CampaignBusinessBusinessTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("CampaignBusinessCompagnId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CampaignBusinessBusinessTypeId", "CampaignBusinessCompagnId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Core.Models.Product", b =>
                {
                    b.Property<int>("CampaignId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<int>("NbrProductPerBusiness")
                        .HasColumnType("int");

                    b.HasKey("CampaignId", "ProductTypeId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Core.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("Core.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Core.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Borough")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Canton")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Departement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifAt")
                        .HasColumnType("datetime2");

                    b.Property<float>("Lat")
                        .HasColumnType("real");

                    b.Property<float>("Lng")
                        .HasColumnType("real");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matricule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("TelNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessTypeCampaign", b =>
                {
                    b.HasOne("Core.Models.BusinessType", null)
                        .WithMany()
                        .HasForeignKey("CampaignBusinessTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Campaign", null)
                        .WithMany()
                        .HasForeignKey("CampaignsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CampaignProductType", b =>
                {
                    b.HasOne("Core.Models.Campaign", null)
                        .WithMany()
                        .HasForeignKey("ProductTypeCampaignsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.ProductType", null)
                        .WithMany()
                        .HasForeignKey("ProductTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CampaignTown", b =>
                {
                    b.HasOne("Core.Models.Town", null)
                        .WithMany()
                        .HasForeignKey("CampaignTownsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Campaign", null)
                        .WithMany()
                        .HasForeignKey("CampaignsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Models.Campaign", b =>
                {
                    b.HasOne("Core.Models.Customer", "Customer")
                        .WithMany("Campaigns")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Region", "Region")
                        .WithMany("Campaigns")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Core.Models.User", null)
                        .WithMany("Compagns")
                        .HasForeignKey("UserId");

                    b.Navigation("Customer");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Core.Models.CampaignBusiness", b =>
                {
                    b.HasOne("Core.Models.BusinessType", "BusinessType")
                        .WithMany()
                        .HasForeignKey("BusinessTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.Campaign", "Campaign")
                        .WithMany("CampaignBusinesses")
                        .HasForeignKey("CompagnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BusinessType");

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("Core.Models.Customer", b =>
                {
                    b.HasOne("Core.Models.User", null)
                        .WithMany("Customers")
                        .HasForeignKey("UserId");

                    b.OwnsOne("Core.CompelxeTypes.Address", "Address", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .UseIdentityColumn();

                            b1.Property<string>("CountryName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("TownName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Core.Models.Photo", b =>
                {
                    b.HasOne("Core.Models.CampaignBusiness", "CampaignBusiness")
                        .WithMany("Photos")
                        .HasForeignKey("CampaignBusinessBusinessTypeId", "CampaignBusinessCompagnId");

                    b.Navigation("CampaignBusiness");
                });

            modelBuilder.Entity("Core.Models.Product", b =>
                {
                    b.HasOne("Core.Models.Campaign", "Campaign")
                        .WithMany("CampaignProducts")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("Core.Models.ProductType", b =>
                {
                    b.OwnsOne("Core.CompelxeTypes.Size", "Size", b1 =>
                        {
                            b1.Property<int>("ProductTypeId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .UseIdentityColumn();

                            b1.Property<float>("Height")
                                .HasColumnType("real");

                            b1.Property<int>("Unit")
                                .HasColumnType("int");

                            b1.Property<float>("Width")
                                .HasColumnType("real");

                            b1.HasKey("ProductTypeId");

                            b1.ToTable("ProductTypes");

                            b1.WithOwner()
                                .HasForeignKey("ProductTypeId");
                        });

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Core.Models.Town", b =>
                {
                    b.HasOne("Core.Models.Region", "Region")
                        .WithMany("RegionTowns")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Core.Models.Campaign", b =>
                {
                    b.Navigation("CampaignBusinesses");

                    b.Navigation("CampaignProducts");
                });

            modelBuilder.Entity("Core.Models.CampaignBusiness", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Core.Models.Customer", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("Core.Models.Region", b =>
                {
                    b.Navigation("Campaigns");

                    b.Navigation("RegionTowns");
                });

            modelBuilder.Entity("Core.Models.User", b =>
                {
                    b.Navigation("Compagns");

                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}