using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebEffectiveWorkersMVC.Models {
    public class Worker {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}