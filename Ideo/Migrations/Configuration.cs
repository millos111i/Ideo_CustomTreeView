namespace Ideo.Migrations
{
    using Ideo.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Ideo.DAL.TreeViewContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Ideo.DAL.TreeViewContext";
        }

        protected override void Seed(Ideo.DAL.TreeViewContext context)
        {
            var data = new List<TreeView>
            {
                new TreeView{Text = "first", Parent = null },
                new TreeView{Text = "second", Parent = null }
            };
            data.ForEach(d => context.Trees.AddOrUpdate(d));
            context.SaveChanges();

        }
    }
}
