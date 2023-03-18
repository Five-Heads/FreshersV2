﻿// <auto-generated />
using System;
using FreshersV2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FreshersV2.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BaseImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Base64Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Object")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BaseImages");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BlurredImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Base64Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BaseImageId")
                        .HasColumnType("int");

                    b.Property<int>("BlurrLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BaseImageId");

                    b.ToTable("BlurredImages");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BlurredImageContest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BaseImageId")
                        .HasColumnType("int");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<int>("SecondsPerRound")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BaseImageId");

                    b.ToTable("BlurredImageContests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.UserBlurredImageContest", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BlurredImageContestId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("Ranking")
                        .HasColumnType("int");

                    b.HasKey("UserId", "BlurredImageContestId");

                    b.HasIndex("BlurredImageContestId");

                    b.ToTable("UserBlurredImageContests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Checkpoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AssignedPersonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFinal")
                        .HasColumnType("bit");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("QRCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TreasureHuntId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TreasureHuntId");

                    b.ToTable("Checkpoints");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.GroupTreasureHunt", b =>
                {
                    b.Property<int>("TreasureHuntId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Done")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("NextId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Started")
                        .HasColumnType("bit");

                    b.HasKey("TreasureHuntId", "GroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("NextId");

                    b.ToTable("GroupTreasureHunts");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Leaderboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Leaderboard");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.TreasureHunt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TreasureHunts");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacultyNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("VoteImageContestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("VoteImageContestId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("FreshersV2.Data.Models.UserTreasureHunt", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TreasureHuntId")
                        .HasColumnType("int");

                    b.Property<string>("Done")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NextId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "TreasureHuntId");

                    b.HasIndex("NextId");

                    b.HasIndex("TreasureHuntId");

                    b.ToTable("UserTreasureHunts");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("int");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.RoundDrawingUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RoundId")
                        .HasColumnType("int");

                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoundId");

                    b.HasIndex("ContestId");

                    b.HasIndex("RoundId");

                    b.HasIndex("UserId", "ContestId");

                    b.ToTable("RoundDrawingUsers");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.RoundVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Image1Votes")
                        .HasColumnType("int");

                    b.Property<int>("Image2Votes")
                        .HasColumnType("int");

                    b.Property<int>("RoundId")
                        .HasColumnType("int");

                    b.Property<int>("VoteImageRoundId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.HasIndex("VoteImageRoundId")
                        .IsUnique();

                    b.ToTable("RoundVotes");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.UserContest", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.Property<string>("UserHubId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VoteImageContestId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ContestId");

                    b.HasIndex("ContestId");

                    b.HasIndex("VoteImageContestId");

                    b.ToTable("UserContests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Base64Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.Property<int>("ContestId1")
                        .HasColumnType("int");

                    b.Property<int?>("RoundId")
                        .HasColumnType("int");

                    b.Property<int>("RoundVoteId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VoteImageRoundId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.HasIndex("ContestId1");

                    b.HasIndex("RoundId");

                    b.HasIndex("RoundVoteId");

                    b.HasIndex("VoteImageRoundId");

                    b.HasIndex("UserId", "ContestId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DrawTime")
                        .HasColumnType("int");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VoteTime")
                        .HasColumnType("int");

                    b.Property<string>("Words")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImageRound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("RoundId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("VoteImageRound");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BlurredImage", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.BlurredImageGame.BaseImage", "BaseImage")
                        .WithMany("BlurredImages")
                        .HasForeignKey("BaseImageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BaseImage");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BlurredImageContest", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.BlurredImageGame.BaseImage", "BaseImage")
                        .WithMany("Contests")
                        .HasForeignKey("BaseImageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BaseImage");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.UserBlurredImageContest", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.BlurredImageGame.BlurredImageContest", "BlurredImageContest")
                        .WithMany()
                        .HasForeignKey("BlurredImageContestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BlurredImageContest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Checkpoint", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.TreasureHunt", "TreasureHunt")
                        .WithMany("Checkpoints")
                        .HasForeignKey("TreasureHuntId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TreasureHunt");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.GroupTreasureHunt", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.Group", "Group")
                        .WithMany("TreasureHunts")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.Checkpoint", "Next")
                        .WithMany()
                        .HasForeignKey("NextId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.TreasureHunt", "TreasureHunt")
                        .WithMany()
                        .HasForeignKey("TreasureHuntId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Next");

                    b.Navigation("TreasureHunt");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Leaderboard", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("FreshersV2.Data.Models.Leaderboard", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.User", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId");

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", null)
                        .WithMany("Participants")
                        .HasForeignKey("VoteImageContestId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.UserTreasureHunt", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.Checkpoint", "Next")
                        .WithMany()
                        .HasForeignKey("NextId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.TreasureHunt", "TreasureHunt")
                        .WithMany()
                        .HasForeignKey("TreasureHuntId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Next");

                    b.Navigation("TreasureHunt");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.Round", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", "Contest")
                        .WithMany("Rounds")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Contest");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.RoundDrawingUser", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", "Contest")
                        .WithMany()
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.Round", "Round")
                        .WithMany("DrawingUsers")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.UserContest", "User")
                        .WithMany("RoundsDrawing")
                        .HasForeignKey("UserId", "ContestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Contest");

                    b.Navigation("Round");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.RoundVote", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.Round", "Round")
                        .WithMany("Votes")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageRound", "VoteImageRound")
                        .WithOne("RoundVote")
                        .HasForeignKey("FreshersV2.Data.Models.VoteImageGame.RoundVote", "VoteImageRoundId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Round");

                    b.Navigation("VoteImageRound");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.UserContest", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", "Contest")
                        .WithMany()
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", null)
                        .WithMany("UserContests")
                        .HasForeignKey("VoteImageContestId");

                    b.Navigation("Contest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImage", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", null)
                        .WithMany("Images")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", "Contest")
                        .WithMany()
                        .HasForeignKey("ContestId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.Round", null)
                        .WithMany("Images")
                        .HasForeignKey("RoundId");

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.RoundVote", "Vote")
                        .WithMany()
                        .HasForeignKey("RoundVoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.VoteImageRound", "VoteImageRound")
                        .WithMany("Images")
                        .HasForeignKey("VoteImageRoundId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.UserContest", "User")
                        .WithMany("Images")
                        .HasForeignKey("UserId", "ContestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Contest");

                    b.Navigation("User");

                    b.Navigation("Vote");

                    b.Navigation("VoteImageRound");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImageRound", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.VoteImageGame.Round", null)
                        .WithMany("ImageRounds")
                        .HasForeignKey("RoundId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FreshersV2.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FreshersV2.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BaseImage", b =>
                {
                    b.Navigation("BlurredImages");

                    b.Navigation("Contests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Group", b =>
                {
                    b.Navigation("TreasureHunts");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.TreasureHunt", b =>
                {
                    b.Navigation("Checkpoints");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.Round", b =>
                {
                    b.Navigation("DrawingUsers");

                    b.Navigation("ImageRounds");

                    b.Navigation("Images");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.UserContest", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("RoundsDrawing");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImageContest", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Participants");

                    b.Navigation("Rounds");

                    b.Navigation("UserContests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImageRound", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("RoundVote")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
