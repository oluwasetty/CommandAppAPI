using System.Collections.Generic;
using ApprovalApp.Models;

namespace ApprovalApp.Interfaces
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
    }
}