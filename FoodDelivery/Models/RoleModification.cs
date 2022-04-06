using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    // The RoleModification class code will help in doing the changes to a role.
    public class RoleModification
    {
        public RoleModification()
        {
            // By default, if it doesn't have an initial value, then this is the same
            // as adding [Required] property to it. Because of this I used a linked list instead of an array
            this.AddIds = new LinkedList<string>();
            this.DeleteIds = new LinkedList<string>();
        }
        public string RoleName { get; set; }
        public string RoleId { get; set; }

        //public string[] AddIds { get; set; }
        //public string[] DeleteIds { get; set; }
        public LinkedList<string> AddIds { get; set; }
        public LinkedList<string> DeleteIds { get; set; }
    }
}