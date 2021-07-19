using System;
using System.Data.Linq.Mapping;
using AutorityWPFApp.Data;

namespace AutorityWPFApp.Models
{
    [Table(Name ="AutorityDates")]
    class AutorityDate
    {
        [Column(Name ="Id", IsDbGenerated =true)]
        public int Id { get; set; }

        [Column (Name ="AutorityDate")]
        public DateTime Date_Time { get; set; }
    }
}
