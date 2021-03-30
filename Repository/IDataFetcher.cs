using System.Collections.Generic;

namespace Repository
{
    public interface IDataFetcher
    {
        List<List<string>> RepoGetStateData(int Id);
        List<List<string>> RepoGetStoreData();
        List<List<string>> RepoGetStoreData(int Id);
        List<List<string>> RepoGetUserData();
        List<List<string>> RepoGetUserData(string email);
        bool RepoIsUser(string email);
        List<List<string>> RepoGetItemData();
        List<List<string>> RepoGetItemData(int Id);
        List<List<int>> RepoGetInvDataForStore(int Id);
    }
}