using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.Data.SQLite.Linq;

namespace AutorityWPFApp.Models
{
    class ActionWithDb : IActionWithData
    {
        #region Строка подключения к БД _ConectionString
        private readonly string connectionString = @"Data Source = D:\VisualStudio2017\AutorityWPFApp\AutorityWPFApp\Data\LoginData.db3;Version=3;";
        #endregion
        private SQLiteConnection con { get; set; }
        #region Метод проверки наличие пользователя в БД
        public bool CheackData(string loginName, string password)
        {
            con = CreateSQLiteConectionAndOpen();
            bool flag = false;
            using (DataContext dataContext = new DataContext(con))
            {
                Table<UserEntity> users = dataContext.GetTable<UserEntity>();
                foreach(var user in users)
                {
                    if (user.LoginName == loginName && user.Password == password)
                        flag = true;
                }
            }
            con.Close();
            return flag;
        }
        #endregion
        #region Метод удаления поьзователя из БД
        public bool DeleteData(UserEntity _userEntity)
        {
            // TODO: Реализовать метод удаления
            throw new NotImplementedException();
        }
        #endregion
        public UserEntity ReadData()
        {
            // TODO: Реализовать метод чтения пользователей
            throw new NotImplementedException();
        }
        #region Метод добавления пользователя в БД
        public bool WriteData(UserEntity user)
        {
            // TODO: Реализовать метод Записи пользователя в БД
            using (con = CreateSQLiteConectionAndOpen())
            {
                DataContext context = new DataContext(con);
                context.GetTable<UserEntity>().InsertOnSubmit(user);
                context.SubmitChanges();
            }
            return true;
        }
        #endregion
        #region Метод создания открытого SQLite соединения.
        private SQLiteConnection CreateSQLiteConectionAndOpen()
        {
            if (con == null)
            {
                con = new SQLiteConnection(connectionString);
                con.Open();
            }
            return con;
        }
        #endregion
        #region Метод получения ID пользователя из БД.

        #endregion
    }
}
