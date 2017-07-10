using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//using SQLite;
//using SQLiteNetExtensions.Attributes;

namespace ExnessTask.DataRepository.TableModels
{
    public class VendorDto
    {
        //[PrimaryKey]
        public string VendorId { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //[ManyToMany(typeof(VendorCategory))]
        //public List<Category> Categories { get; set; }
    }
}