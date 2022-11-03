﻿// <auto-generated />
using System;
using BackendDev.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackendDev.Migrations
{
    [DbContext(typeof(ContextDataBase))]
    [Migration("20221018060236_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BackendDev.Data.Models.GenreModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("BackendDev.Data.Models.MovieModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AgeLimit")
                        .HasColumnType("int");

                    b.Property<int?>("Budget")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Fees")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tagline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MovieModels");
                });

            modelBuilder.Entity("BackendDev.Data.Models.ReviewModelBd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreateDateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MovieModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("isAnonymous")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MovieModelId");

                    b.HasIndex("UserModelId");

                    b.ToTable("ReviewModels");
                });

            modelBuilder.Entity("BackendDev.Data.Models.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GenreModelMovieModel", b =>
                {
                    b.Property<Guid>("MovieGenresId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieGenresId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MovieGenresId", "MovieGenresId1");

                    b.HasIndex("MovieGenresId1");

                    b.ToTable("GenreModelMovieModel");
                });

            modelBuilder.Entity("MovieModelUserModel", b =>
                {
                    b.Property<Guid>("UserMoviesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserMoviesId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserMoviesId", "UserMoviesId1");

                    b.HasIndex("UserMoviesId1");

                    b.ToTable("MovieModelUserModel");
                });

            modelBuilder.Entity("BackendDev.Data.Models.ReviewModelBd", b =>
                {
                    b.HasOne("BackendDev.Data.Models.MovieModel", null)
                        .WithMany("Reviews")
                        .HasForeignKey("MovieModelId");

                    b.HasOne("BackendDev.Data.Models.UserModel", null)
                        .WithMany("Reviews")
                        .HasForeignKey("UserModelId");
                });

            modelBuilder.Entity("GenreModelMovieModel", b =>
                {
                    b.HasOne("BackendDev.Data.Models.GenreModel", null)
                        .WithMany()
                        .HasForeignKey("MovieGenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendDev.Data.Models.MovieModel", null)
                        .WithMany()
                        .HasForeignKey("MovieGenresId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieModelUserModel", b =>
                {
                    b.HasOne("BackendDev.Data.Models.MovieModel", null)
                        .WithMany()
                        .HasForeignKey("UserMoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendDev.Data.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserMoviesId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackendDev.Data.Models.MovieModel", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BackendDev.Data.Models.UserModel", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
