using InterfacesTeamManager.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace InterfacesTeamManager.Data
{
    // My Repository class that provides my SQL queries and CRUD operations
    public static class Repository
    {
        // Returns a database context instance
        static Context GetContext()
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;
        }

        // Returns a count of the employees
        public static int GetemployeeCount()
        {
            using (Context context = GetContext())
            {
                return context.employees.Count();
            }
        }

        // Returns an IList collection of employees ordered by the team and job title
        public static IList<Employee> Getemployees()
        {
            using (Context context = GetContext())
            {
                return context.employees
                    .Include(e => e.Team)
                    .OrderBy(e => e.Team.Title)
                    .ThenBy(e => e.Title)
                    .ToList();
            }
        }

        // Returns a single employee ID
        public static Employee Getemployee(int employeeId)
        {
            using (Context context = GetContext())
            {
                return context.employees
                    .Include(e => e.Team)
                    .Include(s => s.Skills.Select(e => e.Skill))
                    .Where(e => e.Id == employeeId)
                    .SingleOrDefault();
            }
        }

        // Returns an IList collection of teams ordered by the team's title
        public static IList<Team> GetTeam()
        {
            using (Context context = GetContext())
            {
                return context.Team
                    .OrderBy(t => t.Title)
                    .ToList();
            }
        }

        // Returns a single team ID
        public static Team GetTeam(int TeamId)
        {
            using (Context context = GetContext())
            {
                return context.Team
                    .Where(t => t.Id == TeamId)
                    .SingleOrDefault();
            }
        }

        // Returns an IList collection of Skills ordered by name
        public static IList<Skill> GetSkills()
        {
            using (Context context = GetContext())
            {
                return context.Skills
                    .OrderBy(a => a.Name)
                    .ToList();
            }
        }

        // Adds an employee instance
        public static void Addemployee(Employee employee)
        {
            using (Context context = GetContext())
            {
                context.employees.Add(employee);

                var employeeEntry = context.Entry(employee);
                var TeamEntry = context.Entry(employee.Team);

                if (employee.Team != null && employee.Team.Id > 0)
                {
                    context.Entry(employee.Team).State = EntityState.Unchanged;
                }

                context.SaveChanges();
            }
        }

        // Updates an employee instance
        public static void Updateemployee(Employee employee)
        {
            using (Context context = GetContext())
            {
                context.employees.Attach(employee);
                var employeeEntry = context.Entry(employee);
                employeeEntry.State = EntityState.Modified;
                employeeEntry.Property("Title").IsModified = false;

                context.SaveChanges();
            }
        }

        // Deletes an employee instance
        public static void Deleteemployee(int employeeId)
        {
            using (Context context = GetContext())
            {
                var employee = new Employee() { Id = employeeId };
                context.Entry(employee).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
    }
}
