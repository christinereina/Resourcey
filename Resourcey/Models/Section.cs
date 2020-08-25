using System.Collections.Generic;

namespace Resourcey.Models
{
    public class Section
    {
        public Section()
        {
            this.Resources = new HashSet<Resource>();
        }

        public int SectionId { get; set; }
        public int ClassroomId { get; set; }
        public string Name { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }

    }
}