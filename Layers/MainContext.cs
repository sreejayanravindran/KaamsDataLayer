using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Kaams.Entity
{

    public class MainContext : DbContext 
    {
        public MainContext()
            : base("Kaams-SQLConnection-String")
        {

        }

    }
}//KaamsNGPEntities