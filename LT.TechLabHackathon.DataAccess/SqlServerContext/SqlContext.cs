using LT.TechLabHackathon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.DataAccess.SqlServerContext
{
    public class SqlContext(DbContextOptions<SqlContext> options) : DbContext(options)
    {
        private IDbContextTransaction _transaction;

        #region DbSets
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<ChallengeConstraint> ChallengeConstraints { get; set; }
        public DbSet<ChallengeExample> ChallengeExamples { get; set; }
        public DbSet<ChallengeLevel> ChallengeLevels { get; set; }
        public DbSet<ChallengeValidation> ChallengeValidations { get; set; }
        public DbSet<ChallengeInputParameter> ChallengeInputParameters { get; set; }
        public DbSet<ChallengeLanguageSignature> ChallengeLanguageSignatures { get; set; }
        public DbSet<ChallengeInputSetupParameter> ChallengeInputSetupParameters { get; set; }

        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<ProgrammingLanguageReservedWord> ProgrammingLanguageReservedWords { get; set; }
        public DbSet<ProgrammingLanguageDataType> ProgrammingLanguageDataTypes { get; set; }

        public DbSet<GeneralDataType> GeneralDataTypes { get; set; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<AuthUserKey> AuthUserKeys { get; set; }
        public DbSet<UserChallenge> UserChallenges { get; set; }
        public DbSet<UserChallengeHistory> UserChallengeHistories { get; set; }
        public DbSet<UserScore> UserScores { get; set; }

        public DbSet<Status> Statuses { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.Examples)
                .WithOne(c => c.Challenge)
                .HasForeignKey(c => c.ChallengeId);

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.Constraints)
                .WithOne(c => c.Challenge)
                .HasForeignKey(c => c.ChallengeId);

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.Validations)
                .WithOne(c => c.Challenge)
                .HasForeignKey(c => c.ChallengeId);

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.LanguageSignatures)
                .WithOne(c => c.Challenge)
                .HasForeignKey(c => c.ChallengeId);

            modelBuilder.Entity<Challenge>()
                .HasOne(c=>c.Level)
                .WithMany(c=>c.Challenges)
                .HasForeignKey(c => c.LevelId);

            modelBuilder.Entity<Challenge>()
                .HasMany(c => c.InputSetupParameters)
                .WithOne(c => c.Challenge)
                .HasForeignKey(c => c.ChallengeId);

            modelBuilder.Entity<Challenge>()
                .HasOne(c => c.DataType)
                .WithMany(c => c.Challenges)
                .HasForeignKey(c => c.ResultDataTypeId)
                .HasPrincipalKey(dt=>dt.DataTypeId);

            modelBuilder.Entity<Challenge>().Navigation(c => c.Constraints).AutoInclude();
            modelBuilder.Entity<Challenge>().Navigation(c => c.Examples).AutoInclude();
            modelBuilder.Entity<Challenge>().Navigation(c => c.Validations).AutoInclude();
            modelBuilder.Entity<Challenge>().Navigation(c => c.LanguageSignatures).AutoInclude();
            modelBuilder.Entity<Challenge>().Navigation(c => c.InputSetupParameters).AutoInclude();
            modelBuilder.Entity<Challenge>().Navigation(c => c.DataType).AutoInclude();

            modelBuilder.Entity<ChallengeLevel>()
                .HasMany(l => l.Challenges)
                .WithOne(l => l.Level)
                .HasForeignKey(l => l.LevelId);

            modelBuilder.Entity<ChallengeValidation>()
                .HasMany(v => v.InputParameters)
                .WithOne(v => v.Validation)
                .HasForeignKey(v => v.ValidationId);
            
            modelBuilder.Entity<ChallengeValidation>().Navigation(v=>v.InputParameters).AutoInclude();

            modelBuilder.Entity<ChallengeInputSetupParameter>()
                .HasOne(isp => isp.DataType)
                .WithMany(isp => isp.InputSetupParameters)
                .HasForeignKey(isp => isp.DataTypeId);

            modelBuilder.Entity<ChallengeInputSetupParameter>().Navigation(isp => isp.DataType).AutoInclude();

            
            modelBuilder.Entity<ProgrammingLanguage>()
                .HasMany(p=>p.ReservedWords)
                .WithOne(p=>p.ProgrammingLanguage)
                .HasForeignKey(p=>p.ProgrammingLanguageId);

            modelBuilder.Entity<ProgrammingLanguage>()
                .HasMany(p => p.DataTypes)
                .WithOne(p => p.ProgrammingLanguage)
                .HasForeignKey(p => p.ProgrammingLanguageId);

            modelBuilder.Entity<ProgrammingLanguage>().Navigation(p => p.ReservedWords).AutoInclude();
            modelBuilder.Entity<ProgrammingLanguage>().Navigation(p => p.DataTypes).AutoInclude();
            
            modelBuilder.Entity<ProgrammingLanguageDataType>()
                .HasOne(p => p.DataType)
                .WithMany(pdt=>pdt.ProgrammingLanguageDataTypes)
                .HasForeignKey(pdt => pdt.DataTypeId);

            modelBuilder.Entity<ProgrammingLanguageDataType>().Navigation(pdt => pdt.DataType).AutoInclude();


            modelBuilder.Entity<GeneralDataType>()
                .HasMany(p => p.InputSetupParameters)
                .WithOne(p => p.DataType)
                .HasForeignKey(p => p.DataTypeId);

            modelBuilder.Entity<User>()
                .HasIndex(u=>u.Email).IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u=>u.UserChallenges)
                .WithOne(u=>u.User)
                .HasForeignKey(u=>u.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserChallengeHistories)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserScores)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Status>()
                .HasMany(s=>s.Users)
                .WithOne(s=>s.Status)
                .HasForeignKey(s=>s.StatusId);


            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (Database.CurrentTransaction != null)
                    await _transaction.RollbackAsync();
                return 0;
            }
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public async Task<bool> CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
            return true;
        }

        public async Task RollBackAsync()
        {
            await _transaction.RollbackAsync();
        }
    }
}
