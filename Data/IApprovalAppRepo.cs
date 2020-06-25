using System.Collections.Generic;
using ApprovalApp.Models;

namespace ApprovalApp.Data
{
    public interface IApprovalAppRepo
    {
        bool SaveChanges();

        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}