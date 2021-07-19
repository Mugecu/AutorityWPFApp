using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutorityWPFApp.Models
{
    interface IActionWithData
    {
        UserEntity ReadData();
        bool WriteData(UserEntity _user);
        bool CheackData(string _loginName, string _password);
        bool DeleteData(UserEntity _user);
    }
}
