using System;
using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;

namespace AutorityWPFApp.Models
{
    class ActionWithDb : IActionWithData
    {
        #region Строка подключения к БД _ConectionString
        private readonly string connectionString = @"Data Source = D:\VisualStudio2017\AutorityWPFApp\AutorityWPFApp\Data\LoginData.db3;Version=3;";
        #endregion
        #region Свойство с SQLiteConnection
        private SQLiteConnection con { get; set; }
        #endregion
        #region Метод проверки наличие пользователя в БД
        public bool CheackData(string loginName, string password)
        {
            con = CreateSQLiteConectionAndOpen();
            bool flag = false;
            using (DataContext dataContext = new DataContext(con))
            {
                Table<UserEntity> users = dataContext.GetTable<UserEntity>();
                foreach (UserEntity us in users)
                {
                    if (us.LoginName == loginName && us.Password == password)
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
        #region Метод Чтения данныъ из БД
        public UserEntity ReadData()
        {
            // TODO: Реализовать метод чтения пользователей
            throw new NotImplementedException();
        }
        #endregion
        #region Метод добавления пользователя в БД
        public bool WriteData(UserEntity user)
        {
            using (con = CreateSQLiteConectionAndOpen())
            {
                string query = string.Format("INSERT INTO UsersEntity(UserName, LoginName, Password, AccessRight) VALUES (@UserName,@LoginName,@Password,@AccessRight)");
                SQLiteCommand command = con.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add("@UserName", System.Data.DbType.String).Value = user.UserName;
                command.Parameters.Add("@LoginName", System.Data.DbType.String).Value = user.LoginName;
                command.Parameters.Add("@Password", System.Data.DbType.String).Value = user.Password;
                command.Parameters.Add("@AccessRight", System.Data.DbType.Int32).Value = user.AccessRight;
                command.ExecuteNonQuery();

                if ((user = GetRowFromDb(user)) != null)
                {
                    if (SetRegistrationData(user, this.con))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }                    
                }
                else
                {
                    return false;
                }
            }
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
        #region Метод установки даты регистрации пользователя в БД.
        private bool SetRegistrationData(UserEntity user, SQLiteConnection connection)
        {
            try
            {
                AutorityDate date = new AutorityDate()
                {
                    Id = user.Id,
                    Date_Time = DateTime.Now
                };
                string query = string.Format("INSERT INTO AutorityDates(Id,AutorityDate) VALUES (@Id,@Date)");
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.Add("@Id", System.Data.DbType.Int32).Value = user.Id;
                command.Parameters.Add("@AutorityDate", System.Data.DbType.String).Value = (date.Date_Time.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region Метод получения кортежа из БД.
        private UserEntity GetRowFromDb(UserEntity user)
        {
            DataContext context = new DataContext(CreateSQLiteConectionAndOpen());
            Table<UserEntity> entity = context.GetTable<UserEntity>();            
            foreach (UserEntity us in entity)
            {
                if (us.LoginName == user.LoginName && us.UserName == user.UserName)
                    user = us;
            }
            return user;
        }
        #endregion
    }
}

