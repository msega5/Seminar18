﻿using Microsoft.EntityFrameworkCore;
using ChatCommon.Models;


namespace ChatDB
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext()
        {

        }

        public ChatContext(DbContextOptions<ChatContext> dbc) : base(dbc)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=gb;Integrated Security=False;TrustServerCertificate=True;Trusted_Connection=True;")
            .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(x => x.Id).HasName("userPk");
                entity.HasIndex(x => x.FullName).IsUnique();
                entity.Property(e => e.FullName)
                .HasColumnName("FullName")
                .HasMaxLength(255)
                .IsRequired();
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");
                entity.HasKey(x => x.MessageId).HasName("messagePk");

                entity.Property(e => e.Text)
                .HasColumnName("messageText");
                entity.Property(e => e.DateSend)
                .HasColumnName("messageData");
                entity.Property(e => e.IsSent)
                .HasColumnName("is_sent");
                entity.Property(e => e.MessageId)
                .HasColumnName("id");

                entity.HasOne(x => x.UserTo)
                .WithMany(m => m.MessageTo)
                .HasForeignKey(x => x.UserToId)
                .HasConstraintName("messageToUserFK");

                entity.HasOne(x => x.UserFrom)
                .WithMany(m => m.MessageFrom)
                .HasForeignKey(x => x.UserFromId)
                .HasConstraintName("messageFromUserFK");
            });
        }
    }
}
