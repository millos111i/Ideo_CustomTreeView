using Ideo.DAL;
using Ideo.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ideo.Controllers
{
    public class TreeViewController : Controller
    {
        private TreeViewContext db = new TreeViewContext();

        // GET: TreeView
        public ActionResult Index()
        {
            var trees = db.Trees.Include(t => t.Parent);
            return View(trees.ToList());
        }

        // GET: TreeView/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreeView treeView = db.Trees.Find(id);
            if (treeView == null)
            {
                return HttpNotFound();
            }
            return View(treeView);
        }

        // GET: TreeView/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ParentID = null;
            ViewBag.ParentText = null;
            if (id != null)
            {
                var result = new List<TreeView>();
                foreach (TreeView tree in result)
                {
                    if (tree.Id == id)
                    {
                        ViewBag.ParentID = id;
                        ViewBag.ParentText = tree.Text;
                        break;
                    }
                }
            }
            return View();
        }

        // POST: TreeView/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,ParentID")] TreeView treeView, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Trees.Add(treeView);
                db.SaveChanges();
                if (id != null)
                {
                    var tree = db.Trees.Find(id);
                    if (tree != null)
                    {
                        treeView.Parent = tree;
                    }
                    else
                    {
                        treeView.Parent = null;
                    }
                }
                else
                {
                    treeView.Parent = null;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.Trees, "Id", "Text", treeView.ParentID);
            return View(treeView);
        }

        // GET: TreeView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreeView treeView = db.Trees.Find(id);
            if (treeView == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = GetSelectListItems(treeView);
            return View(treeView);
        }

        // POST: TreeView/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,ParentID")] TreeView treeView)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treeView).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = GetSelectListItems(treeView);
            return View(treeView);
        }

        private SelectList GetSelectListItems(TreeView tree)
        {
            List<TreeView> trees = DeleteChildrenFromList(db.Trees.ToList(), tree);
            if (trees.Contains(tree)) trees.Remove(tree);
            return new SelectList(trees, "Id", "Text", tree.ParentID);
        }

        private List<TreeView> DeleteChildrenFromList(List<TreeView> treeList, TreeView tree)
        {
            foreach (TreeView t in tree.Children)
            {
                if (t.Children.Count > 0 && t.Id != tree.Id)
                {
                    treeList = DeleteChildrenFromList(treeList, t);
                }
                treeList.Remove(t);
            }
            return treeList;
        }

        // GET: TreeView/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TreeView treeView = db.Trees.Find(id);
            if (treeView == null)
            {
                return HttpNotFound();
            }
            return View(treeView);
        }

        // POST: TreeView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TreeView treeView = db.Trees.Find(id);
            db.Trees.Remove(treeView);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
