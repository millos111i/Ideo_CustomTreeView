using Ideo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Ideo.DAL
{
    public class TreeViewContext : DbContext
    {
        public TreeViewContext() : base("TreeViewContext")
        {
        }
        public DbSet<TreeView> Trees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}