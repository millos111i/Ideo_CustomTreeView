using Ideo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideo.DAL
{
    public class TreeViewInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TreeViewContext>
    {
        protected override void Seed(TreeViewContext context)
        {
            var data = new List<TreeView>
            {
                new TreeView{Text = "first", Parent = null },
                new TreeView{Text = "second", Parent = null }
            };
            data.ForEach(d => context.Trees.Add(d));
            context.SaveChanges();
            
        }
    }
}