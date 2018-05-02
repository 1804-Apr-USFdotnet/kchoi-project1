using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject
{
    public class Reviewer
    {
        private string _name;
        private int _id;

        public string Name { get => _name; set => _name = value; }
        public int ID { get => _id; set => _id = value; }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            var hashCode = 1997165910;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            return hashCode;
        }
    }
}
