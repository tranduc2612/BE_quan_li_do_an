using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class ProjectOutlineDTO
    {
        [Required]
        public string UserName { get; set; }

        public string? NameProject { get; set; }

        public string? PlantOutline { get; set; }

        public string? TechProject { get; set; }

        public string? ExpectResult { get; set; }
        public string? ContentProject { get; set; }
        public string? GroupReviewOutlineId { get; set; }
        public string? NameGroupReviewOutline { get; set; }

        public virtual ICollection<CommentDTO>? Comments { get; set; } = new List<CommentDTO>();

        public virtual GroupReviewOutlineDTO? GroupReviewOutline { get; set; }

        public virtual ProjectDTO? UserNameNavigation { get; set; }
    }
}
