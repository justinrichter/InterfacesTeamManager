using InterfacesTeamManager.Data;
using InterfacesTeamManager.Helpers;
using InterfacesTeamManager.Models;
using System;
using System.Collections.Generic;

namespace InterfacesTeamManager
{
    class Program
    {
        // User commands that can be performed 
        const string CommandListemployees = "l";
        const string CommandListemployee = "i";
        const string CommandListemployeeProperties = "p";
        const string CommandAddemployee = "a";
        const string CommandUpdateemployee = "u";
        const string CommandDeleteemployee = "d";
        const string CommandSave = "s";
        const string CommandCancel = "c";
        const string CommandQuit = "q";

        // A collection of the employee editable properties.
        // This collection of property names needs to match the list 
        // of the properties listed on the Employee Detail screen.
        readonly static List<string> EditableProperties = new List<string>()
        {
            "TeamId",
            "Title",
            "Description",
            "HiredDate",
            "AverageRating"
        };

        static void Main(string[] args)
        {
            string command = CommandListemployees;
            IList<int> employeeIds = null;

            // If the current command doesn't equal "Quit" then evaluate and process the command.
            while (command != CommandQuit)
            {
                switch (command)
                {
                    case CommandListemployees:
                        employeeIds = Listemployees();
                        break;
                    case CommandAddemployee:
                        Addemployee();
                        command = CommandListemployees;
                        continue;
                    default:
                        if (AttemptDisplayemployee(command, employeeIds))
                        {
                            command = CommandListemployees;
                            continue;
                        }
                        else
                        {
                            ConsoleHelper.OutputLine("Sorry, but I didn't understand that command.");
                        }
                        break;
                }

                // Displays a list of available commands
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                int employeeCount = Repository.GetemployeeCount();
                if (employeeCount > 0)
                {
                    ConsoleHelper.Output("Enter a Number 1-{0}, ", employeeCount);
                }
                ConsoleHelper.OutputLine("A - Add, Q - Quit", false);

                // Prompt for the next command from the user
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);
            }
        }


        // Parses the provided command as a line number then displays the Employee Detail screen for that referenced employee
        private static bool AttemptDisplayemployee(
            string command, IList<int> employeeIds)
        {
            var successful = false;
            int? employeeId = null;

            // Only attempt to parse the command to a line number if I have a collection of employee IDs available.
            if (employeeIds != null)
            {
                // Attempt to parse the command to a line number.
                int lineNumber = 0;
                int.TryParse(command, out lineNumber);

                // If the number is within range then get that employee ID.
                if (lineNumber > 0 && lineNumber <= employeeIds.Count)
                {
                    employeeId = employeeIds[lineNumber - 1];
                    successful = true;
                }
            }

            // If I have an employee ID, then display the employee
            if (employeeId != null)
            {
                Displayemployee(employeeId.Value);
            }

            return successful;
        }

        // Prompts the user for the employee values to add and adds the employee to my database
        private static void Addemployee()
        {
            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("ADD HCHB EMPLOYEE");

            // Get the employee values from the user

            var employee = new Employee();
            employee.TeamId = GetTeamId();
            employee.LastName = GetLastName();
            employee.LastName = GetFirstName();
            employee.Title = GetTitle();
            employee.Description = GetDescription();
            employee.HiredDate = GetHiredDateDate();
            employee.AverageRating = GetAverageRating();

            var employeeArist = new employeeSkill();
            employeeArist.SkillId = GetSkillId();
            employee.Skills.Add(employeeArist);

            // Add the employee to the database
            Repository.Addemployee(employee);
        }

        // Gets the team ID from the user
        private static int GetTeamId()
        {
            int? TeamId = null;
            IList<Team> Team = Repository.GetTeam();

            // While the team ID is null, prompt the user to select a team ID from the provided list
            while (TeamId == null)
            {
                ConsoleHelper.OutputBlankLine();

                foreach (Team s in Team)
                {
                    ConsoleHelper.OutputLine("{0}) {1}", Team.IndexOf(s) + 1, s.Title);
                }

                // Get the line number for the selected team
                string lineNumberInput = ConsoleHelper.ReadInput(
                    "Enter the line number of the Team that you want to add the employee to: ");

                // Attempt to parse the user's input to a line number
                int lineNumber = 0;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= Team.Count)
                    {
                        TeamId = Team[lineNumber - 1].Id;
                    }
                }

                // If I was't able to parse the provided line number to a team ID then display an error message.
                if (TeamId == null)
                {
                    ConsoleHelper.OutputLine("Sorry, but that wasn't a valid line number.");
                }
            }

            return TeamId.Value;
        }

        // Gets the employee's first name from the user
        private static string GetFirstName()
        {
            return ConsoleHelper.ReadInput(
                "Enter the employee's first name: ");
        }

        // Get the employee's last name from the user
        private static string GetLastName()
        {
            return ConsoleHelper.ReadInput(
                "Enter the employee's last name: ");
        }


        // Gets the Skill ID from the user
        private static int GetSkillId()
        {
            int? SkillId = null;
            IList<Skill> Skills = Repository.GetSkills();

            // Prompt the user to select a Skill ID from the provided list, while the Skill ID is null
            while (SkillId == null)
            {
                ConsoleHelper.OutputBlankLine();

                foreach (Skill a in Skills)
                {
                    ConsoleHelper.OutputLine("{0}) {1}", Skills.IndexOf(a) + 1, a.Name);
                }

                // Get the line number for the selected Skill
                string lineNumberInput = ConsoleHelper.ReadInput(
                    "Enter the line number of the Skill that you want to add to this employee: ");

                // Parse the user's input to a line number
                int lineNumber = 0;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= Skills.Count)
                    {
                        SkillId = Skills[lineNumber - 1].Id;
                    }
                }

                // If I wasn't able to parse the provided line number to an Skill ID then display an error message
                if (SkillId == null)
                {
                    ConsoleHelper.OutputLine("Sorry, but that wasn't a valid line number.");
                }
            }

            return SkillId.Value;
        }

        // Gets the job title from the user
        private static string GetTitle()
        {
            return ConsoleHelper.ReadInput(
                "Enter a Job Title for the employee: ");
        }

        // Gets the description from the user
        private static string GetDescription()
        {
            return ConsoleHelper.ReadInput(
                "Enter a description for the employee: ");
        }


        // Gets the employee's hired date from the user
        private static DateTime GetHiredDateDate()
        {
            DateTime HiredDateDate = DateTime.MinValue;

            // Prompt the user to provide a hired on date
            while (HiredDateDate == DateTime.MinValue)
            {
                // Get the hired on date from the user
                string HiredDateDateInput = ConsoleHelper.ReadInput(
                    "Enter the date this employee was hired on: ");

                // Parse the input to a date/time
                DateTime.TryParse(HiredDateDateInput, out HiredDateDate);

                // If I'm not able to parse the provided hired on date to a date/time then display an error message
                if (HiredDateDate == DateTime.MinValue)
                {
                    ConsoleHelper.OutputLine("Sorry, but that wasn't a valid date.");
                }
            }

            return HiredDateDate;
        }

        // Gets the average rating from the user
        private static decimal? GetAverageRating()
        {
            decimal? averageRating = null;
            var promptUser = true;

            // Continue to prompt the user for an average rating until I get a valid value or an empty value
            while (promptUser)
            {
                // Get the average rating from the user
                string averageRatingInput = ConsoleHelper.ReadInput(
                    "Enter the average rating for this employee: ");

                // Did I get a value from the user?
                if (!string.IsNullOrWhiteSpace(averageRatingInput))
                {
                    // Attempt to parse the user's input to a decimal
                    decimal averageRatingValue;
                    if (decimal.TryParse(averageRatingInput, out averageRatingValue))
                    {
                        averageRating = averageRatingValue;

                        // If I was able to parse the provided average rating then set the prompt user flag to "false" so I 
                        // break out of the while loop
                        promptUser = false;
                    }
                    else
                    {
                        // If I was't able to parse the provided average rating to a decimal then display an error message
                        ConsoleHelper.OutputLine("Sorry, but that wasn't a valid number.");
                    }
                }
                else
                {
                    // If I didn't get a value from the user then set the prompt user flag to "false" so I break out of the while loop
                    promptUser = false;
                }
            }

            return averageRating;
        }

        // Retrieves a collection of employee IDs from the database and lists them to the screen to allow the user to select one
        private static IList<int> Listemployees()
        {
            var employeeIds = new List<int>();
            IList<Employee> employees = Repository.Getemployees();

            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("HCHB EMPLOYEES");

            ConsoleHelper.OutputBlankLine();

            foreach (Employee employee in employees)
            {
                employeeIds.Add(employee.Id);

                ConsoleHelper.OutputLine("{0}) {1}",
                    employees.IndexOf(employee) + 1,
                    employee.DisplayTextMainScreen);
            }

            return employeeIds;
        }

        // Displays the employee detail for the provided employee ID
        private static void Displayemployee(int employeeId)
        {
            string command = CommandListemployee;

            // If the current command doesn't equal the "Cancel" command then evaluate and process the command.
            while (command != CommandCancel)
            {
                switch (command)
                {
                    case CommandListemployee:
                        Listemployee(employeeId);
                        break;
                    case CommandUpdateemployee:
                        Updateemployee(employeeId);
                        command = CommandListemployee;
                        continue;
                    case CommandDeleteemployee:
                        if (Deleteemployee(employeeId))
                        {
                            command = CommandCancel;
                        }
                        else
                        {
                            command = CommandListemployee;
                        }
                        continue;
                    default:
                        ConsoleHelper.OutputLine("Sorry, but I didn't understand that command.");
                        break;
                }

                // List the available commands
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                ConsoleHelper.OutputLine("U - Update, D - Delete, C - Cancel", false);

                // Get the next command from the user
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);
            }
        }

        // Confirms that the user wants to delete the employee and if so, deletes the employee from my database
        private static bool Deleteemployee(int employeeId)
        {
            var successful = false;

            // Prompt the user if they want to continue with deleting this employee
            string input = ConsoleHelper.ReadInput(
                "Are you sure you want to delete this employee (Y/N)? ", true);

            // If the user entered "y", then delete the employee
            if (input == "y")
            {
                Repository.Deleteemployee(employeeId);
                successful = true;
            }

            return successful;
        }

        // Lists the detail for the provided employee ID
        private static void Listemployee(int employeeId)
        {
            Employee employee = Repository.Getemployee(employeeId);

            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("EMPLOYEE DETAIL");

            ConsoleHelper.OutputLine(employee.DisplayTextDetailScreen);

            ConsoleHelper.OutputBlankLine();
            ConsoleHelper.OutputLine("Title: {0}", employee.Title);
            ConsoleHelper.OutputLine("Team: {0}", employee.Team.Title);
            
            ConsoleHelper.OutputBlankLine();
            ConsoleHelper.OutputLine("Hired Date: {0}", employee.HiredDate.ToShortDateString());
            ConsoleHelper.OutputLine("Average Rating: {0}",
                employee.AverageRating != null ?
                employee.AverageRating.Value.ToString("n2") : "N/A");

            if (!string.IsNullOrWhiteSpace(employee.Description))
            {
                ConsoleHelper.OutputLine(employee.Description);
            }

            ConsoleHelper.OutputLine("Skills:");

            foreach (employeeSkill Skill in employee.Skills)
            {
                ConsoleHelper.OutputLine("{0}", Skill.Skill.Name);
            }
        }

        // Lists the editable properties for the provided employee ID and prompts the user to select a property to edit

        private static void Updateemployee(int employeeId)
        {
            Employee employee = Repository.Getemployee(employeeId);

            string command = CommandListemployeeProperties;

            // If the current command doesn't equal the "Cancel" command then evaluate and process the command
            while (command != CommandCancel)
            {
                switch (command)
                {
                    case CommandListemployeeProperties:
                        ListemployeeProperties(employee);
                        break;
                    case CommandSave:
                        Repository.Updateemployee(employee);
                        command = CommandCancel;
                        continue;
                    default:
                        if (AttemptUpdateemployeeProperty(command, employee))
                        {
                            command = CommandListemployeeProperties;
                            continue;
                        }
                        else
                        {
                            ConsoleHelper.OutputLine("Sorry, but I didn't understand that command.");
                        }
                        break;
                }

                // List the available commands
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                if (EditableProperties.Count > 0)
                {
                    ConsoleHelper.Output("Enter a Number 1-{0}, ", EditableProperties.Count);
                }
                ConsoleHelper.OutputLine("S - Save, C - Cancel", false);

                // Get the next command from the user
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);
            }

            ConsoleHelper.ClearOutput();
        }

        // Parses the provided command as a line number and if successful, calls the appropriate user input method for the selected employee property
        private static bool AttemptUpdateemployeeProperty(
            string command, Employee employee)
        {
            var successful = false;

            // Attempt to parse the command to a line number
            int lineNumber = 0;
            int.TryParse(command, out lineNumber);

            // If the number is within range then get that employee ID
            if (lineNumber > 0 && lineNumber <= EditableProperties.Count)
            {
                // Retrieve the property name for the provided line number
                string propertyName = EditableProperties[lineNumber - 1];

                // Switch on the provided property name and call the associated user input method for that property name
                switch (propertyName)
                {
                    case "TeamId":
                        employee.TeamId = GetTeamId();
                        employee.Team = Repository.GetTeam(employee.TeamId);
                        break;
                    case "Last Name":
                        employee.Description = GetLastName();
                        break;
                    case "First Name":
                        employee.Description = GetFirstName();
                        break;
                    case "Title":
                        employee.Title = GetTitle();
                        break;
                    case "Description":
                        employee.Description = GetDescription();
                        break;
                    case "HiredDate":
                        employee.HiredDate = GetHiredDateDate();
                        break;
                    case "AverageRating":
                        employee.AverageRating = GetAverageRating();
                        break;
                    default:
                        break;
                }

                successful = true;
            }

            return successful;
        }

        // List the editable employee properties to the screen
        private static void ListemployeeProperties(Employee employee)
        {
            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("Update Employee");

            ConsoleHelper.OutputBlankLine();
            ConsoleHelper.OutputLine("1) Team: {0}", employee.Team.Title);
            ConsoleHelper.OutputLine("2) Job Title: {0}", employee.Title);
            ConsoleHelper.OutputLine("3) Description: {0}", employee.Description);
            ConsoleHelper.OutputLine("4) Hired Date: {0}", employee.HiredDate.ToShortDateString());
            ConsoleHelper.OutputLine("5) Average Rating: {0}", employee.AverageRating);
        }
    }
}
