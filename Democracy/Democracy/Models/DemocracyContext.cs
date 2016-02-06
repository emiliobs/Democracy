using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Democracy.Models
{
    public class DemocracyContext:DbContext
    {
        //Conexión xon la base de datos: 
        //Contructor:
        public DemocracyContext():base("DefaultConnection")
        {
        }

        public DbSet<State> States { get; set; }
    }
}                                    