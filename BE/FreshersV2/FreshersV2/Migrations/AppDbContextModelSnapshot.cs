﻿// <auto-generated />
using System;
using FreshersV2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BaseImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Base64Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Object")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BaseImages");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BlurredImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Base64Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BaseImageId")
                        .HasColumnType("integer");

                    b.Property<int>("BlurrLevel")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BaseImageId");

                    b.ToTable("BlurredImages");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BlurredImageContest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseImageId")
                        .HasColumnType("integer");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("integer");

                    b.Property<int>("SecondsPerRound")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BaseImageId");

                    b.ToTable("BlurredImageContests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.UserBlurredImageContest", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("BlurredImageContestId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "BlurredImageContestId");

                    b.HasIndex("BlurredImageContestId");

                    b.ToTable("UserBlurredImageContests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Checkpoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AssignedPersonName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsFinal")
                        .HasColumnType("boolean");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("integer");

                    b.Property<string>("QRCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TreasureHuntId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TreasureHuntId");

                    b.ToTable("Checkpoints");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.GroupTreasureHunt", b =>
                {
                    b.Property<int>("TreasureHuntId")
                        .HasColumnType("integer");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NextId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Started")
                        .HasColumnType("boolean");

                    b.HasKey("TreasureHuntId", "GroupId");

                    b.HasIndex("GroupId");

                    b.HasIndex("NextId");

                    b.ToTable("GroupTreasureHunts");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.Leaderboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Leaderboard");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.TreasureHunt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("TreasureHunts");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FacultyNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int?>("VoteImageContestId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("VoteImageContestId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("FreshersV2.Data.Models.UserTreasureHunt", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("TreasureHuntId")
                        .HasColumnType("integer");

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<int>("NextId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "TreasureHuntId");

                    b.HasIndex("NextId");

                    b.HasIndex("TreasureHuntId");

                    b.ToTable("UserTreasureHunts");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ContestId")
                        .HasColumnType("integer");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.RoundDrawingUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer");

                    b.Property<int>("ContestId")
                        .HasColumnType("integer");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Image1Votes")
                        .HasColumnType("integer");

                    b.Property<int>("Image2Votes")
                        .HasColumnType("integer");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer");

                    b.Property<int>("VoteImageRoundId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.HasIndex("VoteImageRoundId")
                        .IsUnique();

                    b.ToTable("RoundVotes");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.UserContest", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("ContestId")
                        .HasColumnType("integer");

                    b.Property<string>("UserHubId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("VoteImageContestId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "ContestId");

                    b.HasIndex("ContestId");

                    b.HasIndex("VoteImageContestId");

                    b.ToTable("UserContests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Base64Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ContestId")
                        .HasColumnType("integer");

                    b.Property<int>("ContestId1")
                        .HasColumnType("integer");

                    b.Property<int?>("RoundId")
                        .HasColumnType("integer");

                    b.Property<int>("RoundVoteId")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VoteImageRoundId")
                        .HasColumnType("integer");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DrawTime")
                        .HasColumnType("integer");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VoteTime")
                        .HasColumnType("integer");

                    b.Property<string>("Words")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("FreshersV2.Data.Models.VoteImageGame.VoteImageRound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("RoundId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoundId");

                    b.ToTable("VoteImageRound");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

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
                        .WithMany("UserBlurredImageContests")
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

            modelBuilder.Entity("FreshersV2.Data.Models.BlurredImageGame.BlurredImageContest", b =>
                {
                    b.Navigation("UserBlurredImageContests");
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
