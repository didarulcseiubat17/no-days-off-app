﻿using NoDaysOffApp.Data.Helpers;
using NoDaysOffApp.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

namespace NoDaysOffApp.Data
{
    public interface INoDaysOffAppContext
    {
        DbSet<Athlete> Athletes { get; set; }
        DbSet<AthleteWeight> AthleteWeights { get; set; }
        DbSet<BoundedContext> BoundedContexts { get; set; }
        DbSet<Conversation> Conversations { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Tenant> Tenants { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<Dashboard> Dashboards { get; set; }
        DbSet<DashboardTile> DashboardTiles { get; set; }
        DbSet<Tile> Tiles { get; set; }
        DbSet<BodyPart> BodyParts { get; set; }
        DbSet<Exercise> Exercises { get; set; }
        DbSet<Day> Days { get; set; }
        DbSet<ScheduledExercise> ScheduledExercises { get; set; }
        DbSet<CompletedScheduledExercise> CompletedScheduledExercises { get; set; }
        DbSet<Video> Videos { get; set; }
        DbSet<Profile> Profiles { get; set; }
        Task<int> SaveChangesAsync(string username = null);
    }
    
    public class NoDaysOffAppContext : DbContext, INoDaysOffAppContext
    {
        public NoDaysOffAppContext ()
            :base("NoDaysOffAppContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
        }

        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<AthleteWeight> AthleteWeights { get; set; }
        public DbSet<BoundedContext> BoundedContexts { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardTile> DashboardTiles { get; set; }
        public DbSet<Tile> Tiles { get; set; }
        public DbSet<BodyPart> BodyParts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<ScheduledExercise> ScheduledExercises { get; set; }
        public DbSet<CompletedScheduledExercise> CompletedScheduledExercises { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }

        public int SaveChanges(string username)
        {
            UpdateLoggableEntries(username);
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync(string username)
        {
            UpdateLoggableEntries(username);
            return base.SaveChangesAsync();
        }

        public override int SaveChanges() => this.SaveChanges(null);

        public override Task<int> SaveChangesAsync() => this.SaveChangesAsync(null);

        public void UpdateLoggableEntries(string username = null)
        {
            foreach (var entity in ChangeTracker.Entries()
                .Where(e => e.Entity is ILoggable && ((e.State == EntityState.Added || (e.State == EntityState.Modified))))
                .Select(x => x.Entity as ILoggable))
            {
                var isNew = entity.CreatedOn == default(DateTime);
                entity.CreatedOn = isNew ? DateTime.UtcNow : entity.CreatedOn;
                entity.LastModifiedOn = DateTime.UtcNow;
                entity.CreatedBy = isNew ? username : entity.CreatedBy;
                entity.LastModifiedBy = username;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var convention = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
                "SoftDeleteColumnName",
                (type, attributes) => attributes.Single().ColumnName);

            modelBuilder.Conventions.Add(convention);
        }
    }
}