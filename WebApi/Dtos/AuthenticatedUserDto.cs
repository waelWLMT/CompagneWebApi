using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class AuthenticatedUserDto
    {

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Matricule { get; set; }
        public string Email { get; set; }
        public string TelNumber { get; set; }
        public int ClientId { get; set; }
        public int RoleId { get; set; }
        public List<MenuDto> Menus { get; set; }

    }
}
