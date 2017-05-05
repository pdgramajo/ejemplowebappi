using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi_Aurelia.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
            : base("name=SBNContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
    }

    public class ExampleDataContext : DbContext
    {
        public ExampleDataContext()
            : base("name=ExampleContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}