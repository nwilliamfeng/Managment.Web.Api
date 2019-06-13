using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.Model
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string Password { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public override int GetHashCode()
        {
            return this.Id==null? base.GetHashCode():this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is User))
                return false;
            return this.Id==(obj as User).Id;
        }
    }
}
