using System.Collections.Generic;

namespace Repository
{
    public interface IDataSaver
    {
        public void RepoSaveNewUser(List<string> data);
    }
}