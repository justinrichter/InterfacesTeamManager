using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesTeamManager.Models
{
    /// Skill object
    public class Skill
    {
        public Skill()
        {
            employees = new List<employeeSkill>();
        }

        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        
        public ICollection<employeeSkill> employees { get; set; }
    }
}
