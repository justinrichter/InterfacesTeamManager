using InterfacesTeamManager.Models;
using System;
using System.Data.Entity;

namespace InterfacesTeamManager.Data
{
    // Custom class to populate my database with the initial seed data
    internal class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            var TeamDev911 = new Team()
            {
                Title = "Interfaces Dev 911",
            };
            var TeamOlympians = new Team()
            {
                Title = "Olympians",
            };
            var TeamCCM = new Team()
            {
                Title = "Clinically Clouded Madness",
            };

            var skillSQL = new Skill()
            {
                Name = "SQL"
            };
            var skillCacheObjectScript = new Skill()
            {
                Name = "Cache ObjectScript"
            };
            var skillCSharp = new Skill()
            {
                Name = "C#"
            };
            var skillXML = new Skill()
            {
                Name = "XML"
            };
            var skillLeadership = new Skill()
            {
                Name = "Leadership"
            };
            var skillJavaScript = new Skill()
            {
                Name = "JavaScript"
            };

            var employee1 = new Employee()
            {
                Team = TeamDev911,
                LastName = "Sharma",
                FirstName = "Omprakash",
                Title = "Citius Tech Developer",
                Description = "Om is one of the most promising Citius Tech Contractors with a wide range of skills.",
                HiredDate = new DateTime(2016, 3, 1),
                AverageRating = 9
            };
            employee1.AddSkill(skillSQL);
            employee1.AddSkill(skillCacheObjectScript);
            context.employees.Add(employee1);

            var employee2 = new Employee()
            {
                Team = TeamDev911,
                LastName = "Mandlik",
                FirstName = "Satin",
                Title = "Citius Tech Developer",
                Description = "Satin is a dependable developer that you want on your team.",
                HiredDate = new DateTime(2017, 7, 1),
                AverageRating = 8
            };
            employee2.AddSkill(skillSQL);
            employee2.AddSkill(skillCacheObjectScript);
            context.employees.Add(employee2);

            var employee3 = new Employee()
            {
                Team = TeamDev911,
                LastName = "Pai",
                FirstName = "Deepali",
                Title = "Software Engineer II - Interfaces",
                Description = "Deepali has a wide-range of development expertise and tackles some of the highest priority items on the team.",
                HiredDate = new DateTime(2012, 7, 1),
                AverageRating = 10
            };
            employee3.AddSkill(skillSQL);
            employee3.AddSkill(skillCacheObjectScript);
            context.employees.Add(employee3);

            var employee4 = new Employee()
            {
                Team = TeamOlympians,
                LastName = "Ramachandran",
                FirstName = "Ramesh",
                Title = "Sr. Systems Developer - Interfaces",
                Description = "Ramesh's in-depth understanding of InterSystems and our most complicated interfaces is key to the team's success!",
                HiredDate = new DateTime(2010, 5, 1),
                AverageRating = 10
            };
            employee4.AddSkill(skillSQL);
            employee4.AddSkill(skillCacheObjectScript);
            context.employees.Add(employee4);

            var employee5 = new Employee()
            {
                Team = TeamCCM,
                LastName = "Hornsby",
                FirstName = "Warren",
                Title = "Senior Software Engineer - Interfaces",
                Description = "Warren's experience and ability to create internal tools such as IAM has allowed the Interfaces Team to reach new heights.",
                HiredDate = new DateTime(2009, 6, 1),
                AverageRating = 10
            };
            employee5.AddSkill(skillSQL);
            employee5.AddSkill(skillCSharp);
            employee5.AddSkill(skillJavaScript);
            context.employees.Add(employee5);

            context.SaveChanges();
        }
    }
}
