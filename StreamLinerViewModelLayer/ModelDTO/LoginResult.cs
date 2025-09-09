using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class LoginResult
    {
        public bool Succeeded { get; set; }
        public string success { get; set; }
        public string Error { get; set; }

        public static LoginResult Success(string success) => new LoginResult { Succeeded = true, success = "Success" };
        public static LoginResult Failed(string error) => new LoginResult { Succeeded = false, Error = error };
    }
}
