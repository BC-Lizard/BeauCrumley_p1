using System.Collections.Generic;

namespace Repository
{
    public interface IDataSaver
    {
        void RepoSaveNewUser(List<string> data);
        bool RepoSaveNewOrder(List<string> data, List<List<string>> items);
    }
}