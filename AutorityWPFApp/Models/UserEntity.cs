using System;
using System.Data.Linq.Mapping;
using AutorityWPFApp.Data;

namespace AutorityWPFApp.Models
{
    [Table (Name ="UsersEntity")]
    class UserEntity
    {
        [Column(Name ="Id", DbType ="int", IsDbGenerated =true)]
        public int Id { get; set; }

        [Column(Name = "UserName")]
        public string UserName { get; set; }

        [Column(Name = "LoginName")]
        public string LoginName { get; set; }

        [Column(Name = "Password")]
        public string Password { get; set; }

        [Column(Name = "AccessRight")]
        public int AccessRight { get; set; }
    }
}
