using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aqxpov_gyak3_VersionControl.Entities
{
    class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Utónév { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format(
                    "{0} {1}",
                    LastName,
                    Utónév);
            }
        }
    }
}
