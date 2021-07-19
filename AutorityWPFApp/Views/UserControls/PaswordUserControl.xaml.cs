using System.Windows.Controls;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace AutorityWPFApp.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PaswordUserControl.xaml
    /// </summary>
    public partial class PaswordUserControl : UserControl
    {
        #region Свойсво зависимости PaswordBox
        public SecureString _DataFromPaswordBox
        {
            get { return(SecureString) GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("_DataFromPaswordBox", typeof(SecureString), typeof(PaswordUserControl),
                new PropertyMetadata(default(SecureString)));
        #endregion
        public PaswordUserControl()
        {
            InitializeComponent();

        }
        #region Метод измененияя данных в свойсте зависимости _DataFromPasswordBox
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _DataFromPaswordBox = passwordBox.SecurePassword;
        }
        #endregion
    }
}
