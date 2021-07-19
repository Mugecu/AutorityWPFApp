using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using AutorityWPFApp.Infrastructure;
using AutorityWPFApp.Models;
using System.Security;
using AutorityWPFApp.Views.Windows;

namespace AutorityWPFApp.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Ссылка на интерфейс взаимодействия с Model
        IActionWithData _actionWithData;
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
        #region Свойство установки названия окна
        private string _Title = "Окно авторизации";
        public string Title
        {
            get => _Title;
            set
            {
                if (Equals(_Title, value))
                    return;
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        #endregion
        #region Свойство хранящее текстовое поле Hellow
        private string _Hellow = "Здравствуйте";
        public string Hellow
        {
            get => _Hellow;
            set
            {
                if (Equals(_Hellow, value))
                    return;
                _Hellow = value;
                OnPropertyChanged(nameof(Hellow));
            }
        }
        #endregion
        #region Свойство хранящее логин из TextBox
        private string _LoginNameTextBox = "Введите логин";
        public string LoginNameTextBox
        {
            get => _LoginNameTextBox;
            set
            {
                if (Equals(_LoginNameTextBox, value))
                    return;
                _LoginNameTextBox = value;
                OnPropertyChanged(nameof(LoginNameTextBox));
            }
        }
        #endregion
        #region Свойство хранящее пароль из PasswordBox
        private SecureString _PasswordBox ;
        public SecureString PasswordBox
        {

            private get => _PasswordBox;
            set
            {
                if (Equals(_PasswordBox, value))
                    return;
                _PasswordBox = value;
                OnPropertyChanged(nameof(PasswordBox));
            }
        }
        #endregion
        #region Команда закрытия окна.
        public ICommand CloseAplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion
        #region Комманда открытия окна регистрации пользователя
        public ICommand OpenRegistrationWindowCommand { get; }
        private bool CanOpenRegistrationWindowCommandExecute(object p) => true;
        private void OnOpenRegistrationWindowCommandExecuted(object p)
        {
            // HACK: Переделать логику открытия окна. Привести в соответствие с патерном MVVM
            SetSetCentrPositionAndOpen(new UserRegistrationWindow());
        }
        #endregion
        #region Команда авторизации пользоватея в главном окне авторизации
        public ICommand AutorityUserInAppCommand { get; }
        private bool CanAutorityUserInAppCommandExecute(object p) => true;
        private void OnAutorityUserInAppCommandExecuted(object p)
        {
            _actionWithData = new ActionWithDb();
            if (_actionWithData.CheackData(LoginNameTextBox, ConvertFromSecurityString()) == true)
            {
                MessageBox.Show("Такой пользователь есть в базе данных. Добро пожаловать!");
                Hellow = "Здравствуйте, " + _LoginNameTextBox;
                _LoginNameTextBox = "";
                _PasswordBox.Clear();
            }
            else
            {
                MessageBox.Show("Неверно введены логин или пароль. Попробуйте авторизоваться еще раз.");
                _PasswordBox.Clear();
                _LoginNameTextBox = "";
            }
        }
        #region Метод преобразования SecurityString в string
        private string ConvertFromSecurityString()
        {   
            return new System.Net.NetworkCredential(string.Empty, PasswordBox).Password;            
        }
        #endregion
        #endregion
        #region Определение конструктора
        public MainWindowViewModel()
        {
            CloseAplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            AutorityUserInAppCommand = new LambdaCommand(OnAutorityUserInAppCommandExecuted, CanAutorityUserInAppCommandExecute);
            OpenRegistrationWindowCommand = new LambdaCommand(OnOpenRegistrationWindowCommandExecuted, CanOpenRegistrationWindowCommandExecute);
        }
        #endregion
        #region Метод установки главного окна и показ его.
        private void SetSetCentrPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }
        #endregion
    }
}
