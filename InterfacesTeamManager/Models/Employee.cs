using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesTeamManager.Models
{
    /// Employee instance
    public class Employee
    {
        public Employee()
        {
            Skills = new List<employeeSkill>();
        }

        public int Id { get; set; }
        public int TeamId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime HiredDate { get; set; }
        public decimal? AverageRating { get; set; }
        public Team Team { get; set; }
        public ICollection<employeeSkill> Skills { get; set; }

        /// Display text for an employee in the Main Screen
        public string DisplayTextMainScreen
        {
            get
            {
                return $"{FirstName + " " + LastName}";
            }
        }

        /// Display text for an employee in the Detail Screen
        public string DisplayTextDetailScreen
        {
            get
            {
                return $"{FirstName+" "+LastName}";
            }
        }

        /// Adds a Skill to the Employee
        public void AddSkill(Skill Skill)
        {
            Skills.Add(new employeeSkill()
            {
                Skill = Skill,
            });
        }
    }
}
