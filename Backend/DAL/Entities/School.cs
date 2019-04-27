using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DAL.Entities
{
    public class School : IIdentityEntity
    {
        public int Id { get;set; }
        public String Name { get; set; }
        public int ClassesQuantity { get { return _classes.Count; } }
        private ICollection<Class> _classes { get; set; }
        public virtual IReadOnlyCollection<Class> Classes { get { return _classes as Collection<Class>; } }

        public School()
        {
            this._classes = new Collection<Class>();
        }

        public void AddClass(Class c)
        {
            _classes.Add(c);
        }
    }
}
