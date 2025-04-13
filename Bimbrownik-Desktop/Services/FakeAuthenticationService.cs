using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimbrownik_Desktop.Services;

public class FakeAuthenticationService : AuthenticationService
{
    private const string ValidUsername = "admin";
    private const string ValidPassword = "1234";

    public bool Login(string username, string password) =>
        username == ValidUsername && password == ValidPassword;
}
