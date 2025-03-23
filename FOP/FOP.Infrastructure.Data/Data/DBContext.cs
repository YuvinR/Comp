
using FOP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FOP.Infrastructure.Data.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<GrossPerformanceBatchModel> GrossPerformanceBatch { get; set; }
        public DbSet<NetPerformanceBatchModel> NetPerformanceBatches { get; set; }
        public DbSet<PortfolioAuditAccountsModel> PortfolioAuditAccounts{ get; set; }
        public DbSet<PortfolioAuditRegistrationsModel> PortfolioAuditRegistrations { get; set; }
        public DbSet<SleevedRegistrationsContributionsModel> SleevedRegistrationsContributions { get; set; }
        public DbSet<CashQueryModel> CashQueryModels { get; set; }
        public DbSet<ModelChangesModel> ModelChanges { get; set; }
        public DbSet<TerminatedAccountsModel> TerminatedAccounts { get; set; }
        public DbSet<MonthlyUploadsModel> monthlyUploads { get; set; }
        public DbSet<SleeveStrategyModel> sleeveStrategies { get; set; }
        public DbSet<AllMasterModel> allMasterModels { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ConnectionStrings:DefaultConnection");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
        }
    }
}
    