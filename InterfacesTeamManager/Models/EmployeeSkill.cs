using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesTeamManager.Models
{
    /// Employee-Skill object
    public class employeeSkill
    {
        public int Id { get; set; }
        public int employeeId { get; set; }
        public int SkillId { get; set; }

        public Employee employee { get; set; }
        public Skill Skill { get; set; }
    }
}
