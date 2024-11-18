using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VolanGo.Requests
{
    public class RegisterRequestPerson
    {
        public string JMBG {get; set;}

        public string Name {get; set;}

        public string Surname {get; set;}

        public string Gender {get; set;}

        public DateOnly DateOfBirth {get; set;}

        public string email {get; set;}

    }
}