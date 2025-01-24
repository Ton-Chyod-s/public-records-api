﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PublicRecords.Infraestructure.Context;

#nullable disable

namespace DiarioOficial.Infraestructure.Migrations
{
    [DbContext(typeof(OfficialDiaryDbContext))]
    [Migration("20250119142540_RemoveTrueAtAuthorized")]
    partial class RemoveTrueAtAuthorized
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DiarioOficial.Domain.Entities.OfficialStateDiary.OfficialDiaries", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("File")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SessionId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("SessionId");

                    b.ToTable("OfficialDiaries");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.Person.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Authorized")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.Session.Session", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("PersonID")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PersonID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.Token.AuthToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Bearer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuthToken");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.User.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("PassWordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Roles")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.OfficialStateDiary.OfficialDiaries", b =>
                {
                    b.HasOne("DiarioOficial.Domain.Entities.Person.Person", "Person")
                        .WithMany("OfficialDiaries")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiarioOficial.Domain.Entities.Session.Session", "Session")
                        .WithMany("OfficialStateDiaries")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Person");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.Person.Person", b =>
                {
                    b.HasOne("DiarioOficial.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.Session.Session", b =>
                {
                    b.HasOne("DiarioOficial.Domain.Entities.Person.Person", "Person")
                        .WithMany("Sessions")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.Token.AuthToken", b =>
                {
                    b.HasOne("DiarioOficial.Domain.Entities.User.User", "User")
                        .WithMany("AuthToken")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.Person.Person", b =>
                {
                    b.Navigation("OfficialDiaries");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.Session.Session", b =>
                {
                    b.Navigation("OfficialStateDiaries");
                });

            modelBuilder.Entity("DiarioOficial.Domain.Entities.User.User", b =>
                {
                    b.Navigation("AuthToken");
                });
#pragma warning restore 612, 618
        }
    }
}
