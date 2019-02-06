using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesTeamManager.Models
{
    /// Team object
    public class Team
    {
        public Team()
        {
            employees = new List<Employee>();
        }

        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Title { get; set; }

        public ICollection<Employee> employees { get; set; }
    }
}
