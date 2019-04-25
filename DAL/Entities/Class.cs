using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Class : IIdentityEntity
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public int SchoolId { get; set; }
        public virtual School School { get; set; }
    }
}
