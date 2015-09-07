namespace ORM
{
    using System.Data.Entity;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
        }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                 .HasMany(e => e.Users)
                 .WithRequired(e => e.Role)
                 .WillCascadeOnDelete(false);

        //    //modelBuilder.Entity<User>()
        //    //   .HasMany(e => e.Files)
        //    //   .WithRequired(e => e.User)
        //    //   .HasForeignKey(e => e.UserId);

        //    modelBuilder.Entity<Comment>()
        //        .HasMany(e => e.Files);
        }

    }
}
