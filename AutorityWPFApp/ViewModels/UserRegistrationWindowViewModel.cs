using System;
using System.Security;
using System.ComponentModel;
using AutorityWPFApp.Models;
using System.Windows.Input;
using AutorityWPFApp.Infrastructure;

namespace AutorityWPFApp.ViewModels
{
    class UserRegistrationWindowViewModel : INotifyPropertyChanged
    {
        #region Сылка на интерфейс взаимодействия с Model
        private IActionWithData _actionWithData;
        #endregion
        #region Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, string _propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged();
            return true;
        }
        #endregion
        #region Данные ФИО из TextBox
        private string _GetStringFromTextBoxFIO;
        public string GetStringFromTextBoxFIO
        {
            get => _GetStringFromTextBoxFIO;
            set
            {
                if (Equals(_GetStringFromTextBoxFIO, value))
                    return;
                _GetStringFromTextBoxFIO = value;
                OnPropertyChanged(nameof(GetStringFromTextBoxFIO));
                
            }
        }
        #endregion
        #region Данные Логин из TextBox
        private string _GetStringFromTextBoxLogin;

        public string GetStringFromTextBoxLogin
        {
            get => _GetStringFromTextBoxLogin;
            set
            {
                if (Equals(_GetStringFromTextBoxLogin, value))
                    return;
                _GetStringFromTextBoxLogin = value;
                OnPropertyChanged(nameof(GetStringFromTextBoxLogin));
            }
        }
        #endregion
        #region Свойство первичный ввод пароля PasswordBox
        private SecureString _DataFromPasswordBox;
        public SecureString DataFromPasswordBox
        {
            private get => _DataFromPasswordBox;
            set
            {
                if (Equals(_DataFromPasswordBox, value))
                    return;
                _DataFromPasswordBox = value;
                OnPropertyChanged(nameof(DataFromPasswordBox));
            }
        }
        #endregion
        #region Свойство хранящее повторный ввод пароля PasswordBox
        private SecureString _PasswordBoxRepitPassword;
        public SecureString PasswordBoxRepitPassword
        {
            private get => _PasswordBoxRepitPassword;
            set
            {
                if (Equals(_PasswordBoxRepitPassword, value))
                    return;
                _PasswordBoxRepitPassword = value;
                OnPropertyChanged(nameof(PasswordBoxRepitPassword));
            }
        }
        #endregion
        #region Свойство определяет пользователя в систееме GetUserNumber
        private int GetUserNumber { get; set; }
        #endregion
        #region Команда добавления пользователя в БД
        public ICommand AddUserInDbCommand { get; }
        private bool CanAddUserInDbCommandExecute(object p) => true;

        private void OnAddUserInDbCommandExecuted(object p)
        {
            // TODO : Реализовать запись пользователя в БД
            _actionWithData = new ActionWithDb();
            UserEntity user = new UserEntity();
            // TODO : Реализовать дату регистрации пользователя в БД
            AutorityDate autorityDate = new AutorityDate();
            if (CheackPasswordBoxForCorrectRepeatPassword())
            {
                if (GetUserNumber > 0 && GetUserNumber < 4)
                {
                    user.UserName = GetStringFromTextBoxFIO;
                    user.LoginName = GetStringFromTextBoxLogin;
                    user.Password = ConvertFromSecurityString(DataFromPasswordBox);
                    user.AccessRight = GetUserNumber;                   

                    _actionWithData.WriteData(user);


                }
            }
        }
        #endregion
        #region Команда получения выброного RadioButton
        public ICommand GetRadioButtonEnable { get; }
        private bool CanRadioButtonEnableExecute(object p) => true;

        private void OnRadioButtonEnableExecuted(object p)
        {
            GetUserNumber = Convert.ToInt32(p);
        }

        #endregion   
        #region Определение конструктор класса
        public UserRegistrationWindowViewModel()
        {
            GetRadioButtonEnable = new LambdaCommand(OnRadioButtonEnableExecuted,CanRadioButtonEnableExecute);
            AddUserInDbCommand = new LambdaCommand(OnAddUserInDbCommandExecuted, CanAddUserInDbCommandExecute);
        }
        #endregion
  
        #region Метод преобразования SecurityString в string
        private string ConvertFromSecurityString(SecureString security)
        {
            return new System.Net.NetworkCredential(string.Empty, security).Password;
        }
        #endregion
        #region Метод проверки правильности введенного пароля PasswordBox в UserRegistrationWindow
        private bool CheackPasswordBoxForCorrectRepeatPassword()
        {
            if (Equals(ConvertFromSecurityString(DataFromPasswordBox),ConvertFromSecurityString(PasswordBoxRepitPassword)))
                return true;
            return false;
        }
        #endregion
        #region Проверка наличия такого пользователя в БД
        private bool GetUserFromDB()
        {
            _actionWithData = new ActionWithDb();
            if (_actionWithData.CheackData(GetStringFromTextBoxFIO, GetStringFromTextBoxLogin))
                return false;
            else
                return true;
        }
        #endregion
    }
}