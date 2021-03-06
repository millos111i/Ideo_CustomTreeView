﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ideo.Models
{
    public class TreeView
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z\s-]+$",ErrorMessage ="String may contain letters and spaces or dash")]
        public string Text { get; set; }
        [Display(Name ="Parent")]
        public int? ParentID { get; set; }
        public virtual TreeView Parent { get; set; }
        public virtual ICollection<TreeView> Children { get; set; }
    }
}