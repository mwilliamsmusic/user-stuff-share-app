﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using user_stuff_share_app;

namespace user_stuff_share_app.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211104022251_meh")]
    partial class meh
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ssa_database.Models.Bookmark_Models.BookmarkCollect", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CollectId")
                        .HasColumnType("bigint")
                        .HasColumnName("collect_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_bookmark_collect");

                    b.HasIndex("CollectId")
                        .HasDatabaseName("ix_bookmark_collect_collect_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_bookmark_collect_user_id");

                    b.ToTable("bookmark_collect");
                });

            modelBuilder.Entity("ssa_database.Models.Bookmark_Models.BookmarkItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint")
                        .HasColumnName("item_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_bookmark_item");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("ix_bookmark_item_item_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_bookmark_item_user_id");

                    b.ToTable("bookmark_item");
                });

            modelBuilder.Entity("ssa_database.Models.Collect_Models.Collect", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CollectForm")
                        .HasColumnType("text")
                        .HasColumnName("collect_form");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text")
                        .HasColumnName("image_path");

                    b.Property<string>("Status")
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTimeOffset>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_collect");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_collect_user_id");

                    b.ToTable("collect");
                });

            modelBuilder.Entity("ssa_database.Models.Collect_Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CollectId")
                        .HasColumnType("bigint")
                        .HasColumnName("collect_id");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text")
                        .HasColumnName("image_path");

                    b.Property<string>("ItemForm")
                        .HasColumnType("text")
                        .HasColumnName("item_form");

                    b.Property<string>("Status")
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTimeOffset>("Updated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_item");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_item_user_id");

                    b.ToTable("item");
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolCollect", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CollectId")
                        .HasColumnType("bigint")
                        .HasColumnName("collect_id");

                    b.Property<long>("Value")
                        .HasColumnType("bigint")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_cool_collect");

                    b.HasIndex("CollectId")
                        .HasDatabaseName("ix_cool_collect_collect_id");

                    b.ToTable("cool_collect");
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolCollectJoin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CoolCollectId")
                        .HasColumnType("bigint")
                        .HasColumnName("cool_collect_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_cool_collect_join");

                    b.HasIndex("CoolCollectId")
                        .HasDatabaseName("ix_cool_collect_join_cool_collect_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_cool_collect_join_user_id");

                    b.ToTable("cool_collect_join");
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint")
                        .HasColumnName("item_id");

                    b.Property<long>("Value")
                        .HasColumnType("bigint")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_cool_item");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("ix_cool_item_item_id");

                    b.ToTable("cool_item");
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolItemJoin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CoolItemId")
                        .HasColumnType("bigint")
                        .HasColumnName("cool_item_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_cool_item_join");

                    b.HasIndex("CoolItemId")
                        .HasDatabaseName("ix_cool_item_join_cool_item_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_cool_item_join_user_id");

                    b.ToTable("cool_item_join");
                });

            modelBuilder.Entity("ssa_database.Models.Email_Models.Reset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<int>("Passcode")
                        .HasColumnType("integer")
                        .HasColumnName("passcode");

                    b.HasKey("Id")
                        .HasName("pk_reset");

                    b.ToTable("reset");
                });

            modelBuilder.Entity("ssa_database.Models.Flag_Models.Flag", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Rating")
                        .HasColumnType("text")
                        .HasColumnName("rating");

                    b.Property<string>("Reason")
                        .HasColumnType("text")
                        .HasColumnName("reason");

                    b.Property<string>("Status")
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_flag");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_flag_user_id");

                    b.ToTable("flag");
                });

            modelBuilder.Entity("ssa_database.Models.Tag_Models.Tag", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_tag");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("ssa_database.Models.Tag_Models.TagCollectJoin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CollectId")
                        .HasColumnType("bigint")
                        .HasColumnName("collect_id");

                    b.Property<string>("TagName")
                        .HasColumnType("text")
                        .HasColumnName("tag_name");

                    b.HasKey("Id")
                        .HasName("pk_tag_collect_join");

                    b.HasIndex("CollectId")
                        .HasDatabaseName("ix_tag_collect_join_collect_id");

                    b.ToTable("tag_collect_join");
                });

            modelBuilder.Entity("ssa_database.Models.Tag_Models.TagItemJoin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint")
                        .HasColumnName("item_id");

                    b.Property<string>("TagName")
                        .HasColumnType("text")
                        .HasColumnName("tag_name");

                    b.HasKey("Id")
                        .HasName("pk_tag_item_join");

                    b.HasIndex("ItemId")
                        .HasDatabaseName("ix_tag_item_join_item_id");

                    b.HasIndex("TagName")
                        .HasDatabaseName("ix_tag_item_join_tag_name");

                    b.ToTable("tag_item_join");
                });

            modelBuilder.Entity("ssa_database.Models.User_Models.FollowUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("FollowUserId")
                        .HasColumnType("bigint")
                        .HasColumnName("follow_user_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_follow_user");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_follow_user_user_id");

                    b.ToTable("follow_user");
                });

            modelBuilder.Entity("ssa_database.Models.User_Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<DateTimeOffset>("Joined")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("joined");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("Status")
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("Username")
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user");
                });

            modelBuilder.Entity("ssa_database.Models.Bookmark_Models.BookmarkCollect", b =>
                {
                    b.HasOne("ssa_database.Models.Collect_Models.Collect", "Collect")
                        .WithMany("BookmarkCollects")
                        .HasForeignKey("CollectId")
                        .HasConstraintName("fk_bookmark_collect_collect_collect_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ssa_database.Models.User_Models.User", null)
                        .WithMany("BookmarkCollects")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_bookmark_collect_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collect");
                });

            modelBuilder.Entity("ssa_database.Models.Bookmark_Models.BookmarkItem", b =>
                {
                    b.HasOne("ssa_database.Models.Collect_Models.Item", "Item")
                        .WithMany("BookmarkItems")
                        .HasForeignKey("ItemId")
                        .HasConstraintName("fk_bookmark_item_item_item_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ssa_database.Models.User_Models.User", null)
                        .WithMany("BookmarkItems")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_bookmark_item_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ssa_database.Models.Collect_Models.Collect", b =>
                {
                    b.HasOne("ssa_database.Models.User_Models.User", "User")
                        .WithMany("Collects")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_collect_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ssa_database.Models.Collect_Models.Item", b =>
                {
                    b.HasOne("ssa_database.Models.User_Models.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_item_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolCollect", b =>
                {
                    b.HasOne("ssa_database.Models.Collect_Models.Collect", null)
                        .WithMany("CoolCollects")
                        .HasForeignKey("CollectId")
                        .HasConstraintName("fk_cool_collect_collect_collect_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolCollectJoin", b =>
                {
                    b.HasOne("ssa_database.Models.Cool_Models.CoolCollect", null)
                        .WithMany("CoolCollectJoins")
                        .HasForeignKey("CoolCollectId")
                        .HasConstraintName("fk_cool_collect_join_cool_collect_cool_collect_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ssa_database.Models.User_Models.User", "User")
                        .WithMany("CoolCollectJoins")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_cool_collect_join_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolItem", b =>
                {
                    b.HasOne("ssa_database.Models.Collect_Models.Item", null)
                        .WithMany("CoolItems")
                        .HasForeignKey("ItemId")
                        .HasConstraintName("fk_cool_item_item_item_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolItemJoin", b =>
                {
                    b.HasOne("ssa_database.Models.Cool_Models.CoolItem", null)
                        .WithMany("CoolItemJoins")
                        .HasForeignKey("CoolItemId")
                        .HasConstraintName("fk_cool_item_join_cool_item_cool_item_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ssa_database.Models.User_Models.User", "User")
                        .WithMany("CoolItemJoins")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_cool_item_join_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ssa_database.Models.Flag_Models.Flag", b =>
                {
                    b.HasOne("ssa_database.Models.User_Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_flag_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ssa_database.Models.Tag_Models.TagCollectJoin", b =>
                {
                    b.HasOne("ssa_database.Models.Collect_Models.Collect", "Collect")
                        .WithMany("TagCollectJoins")
                        .HasForeignKey("CollectId")
                        .HasConstraintName("fk_tag_collect_join_collect_collect_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collect");
                });

            modelBuilder.Entity("ssa_database.Models.Tag_Models.TagItemJoin", b =>
                {
                    b.HasOne("ssa_database.Models.Collect_Models.Item", "Item")
                        .WithMany("TagItemJoins")
                        .HasForeignKey("ItemId")
                        .HasConstraintName("fk_tag_item_join_item_item_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ssa_database.Models.Tag_Models.Tag", null)
                        .WithMany("TagItemJoins")
                        .HasForeignKey("TagName")
                        .HasConstraintName("fk_tag_item_join_tag_tag_name");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ssa_database.Models.User_Models.FollowUser", b =>
                {
                    b.HasOne("ssa_database.Models.User_Models.User", null)
                        .WithMany("FollowUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_follow_user_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ssa_database.Models.Collect_Models.Collect", b =>
                {
                    b.Navigation("BookmarkCollects");

                    b.Navigation("CoolCollects");

                    b.Navigation("TagCollectJoins");
                });

            modelBuilder.Entity("ssa_database.Models.Collect_Models.Item", b =>
                {
                    b.Navigation("BookmarkItems");

                    b.Navigation("CoolItems");

                    b.Navigation("TagItemJoins");
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolCollect", b =>
                {
                    b.Navigation("CoolCollectJoins");
                });

            modelBuilder.Entity("ssa_database.Models.Cool_Models.CoolItem", b =>
                {
                    b.Navigation("CoolItemJoins");
                });

            modelBuilder.Entity("ssa_database.Models.Tag_Models.Tag", b =>
                {
                    b.Navigation("TagItemJoins");
                });

            modelBuilder.Entity("ssa_database.Models.User_Models.User", b =>
                {
                    b.Navigation("BookmarkCollects");

                    b.Navigation("BookmarkItems");

                    b.Navigation("Collects");

                    b.Navigation("CoolCollectJoins");

                    b.Navigation("CoolItemJoins");

                    b.Navigation("FollowUsers");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
