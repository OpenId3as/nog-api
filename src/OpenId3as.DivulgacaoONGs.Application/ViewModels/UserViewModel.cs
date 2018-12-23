using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using System;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels
{
    public class UserViewModel : LinkContainer
    {
        public UserViewModel()
        {

        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool FirstAccess { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }
    }
}
