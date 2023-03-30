using BtCS_Library.Model.Storage;
using BtCS_Library.Modules.Interface;
using BtCS_Library.Modules.Static;
using LiteDB;

namespace BtCS_Library.Modules.Helper.Storage;

public class UserDataHelper : IStorageDataHelper<UserModel>
{
    private LiteDatabase _storage;
    private readonly ILiteCollection<UserModel> _collection;

    public bool CheckIfUserExists(string userLabel)
    {
        try
        {
            var userList = _collection.Query()
                .OrderBy(x => x.Label)
                .Select(x => x.Label)
                .ToList();
            return userList.Contains(userLabel);
        }
        catch (Exception)
        {
            return false;
        }
    }
    public UserDataHelper(LiteDatabase Storage)
    {
        _storage = Storage;
        _collection = _storage.GetCollection<UserModel>("Users");
    }
    
    public int GetUserIdByLabel(string userLabel)
    {
        if (userLabel == null) throw new ArgumentNullException(nameof(userLabel));
        return _collection.Query().Where(x => x.Label.Equals(userLabel)).First().Id;
    }
    
    public UserModel GetRecordById(int id)
    {
        return _collection.FindById(id);
    }

    public int InsertRecord(UserModel record)
    {
        try
        {
            if (record == null) throw new ArgumentNullException(nameof(record));
            _collection.Insert(record);
            return _collection.Query().Where(x => x.Label.Equals(record.Label) && x.Password.Equals(record.Password)).First().Id;
        }
        catch (Exception)
        {
            LogModule.WriteErrorMessageToLog($"Could not insert {nameof(record)}");
            return -1;
        }
    }

    public bool UpdateRecord(UserModel record)
    {
        try
        {
            if (record == null) throw new ArgumentNullException(nameof(record));
            _collection.Update(record);
            return true;
        }
        catch (Exception)
        {
            LogModule.WriteErrorMessageToLog($"Could not update {nameof(record)} with id {record.Id}");
            return false;
        }
    }

    public bool DeleteRecord(UserModel record)
    {
        try
        {
            if (record == null) throw new ArgumentNullException(nameof(record));
            _collection.Delete(record.Id);
            return true;
        }
        catch (Exception)
        {
            LogModule.WriteErrorMessageToLog($"Could not delete {nameof(record)} with id {record.Id}");
            return false;
        }
    }
}