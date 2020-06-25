using System.Collections.Generic;
using ApprovalApp.Models;

namespace ApprovalApp.Data
{
    // public class MockApprovalAppRepo : IApprovalAppRepo
    public class MockApprovalAppRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boil an egg", Line="Boil Water", Platform="Kettle & Pan"},
                new Command { Id = 1, HowTo = "Cut bread", Line = "Get a Knife", Platform = "Knife & Chopping board" },
                new Command { Id = 2, HowTo = "Make cup of tea", Line = "Place teabag in cup", Platform = "Kettle & Cup" }
            };
            return commands;
            // throw new System.NotImplementedException();
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil Water", Platform = "Kettle & Pan" };
        }
    }
}