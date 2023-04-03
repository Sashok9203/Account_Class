using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account_Class
{
    internal class Account
    {
		private const string _def_email = "default@user";
        private const string _def_password = "123Pass";
        private string? _email;
        private string? _password;

        public Account()  { _email = _def_email; _password = _def_password; }
        public Account(string email,string password)
        {
			Email = email;
			Password = password;
        }
        public string Email
		{
			get => _email;
			set
			{
				if(string.IsNullOrWhiteSpace(value)) throw new ApplicationException(" Email address cannot be empty...");
                value = value.Trim();
				if(value.Length < 4 || value.Length > 50) throw new ApplicationException(" Too many or not enough symbols in email addres ...");
				int tmp = value.IndexOf("@");
				if (tmp >= 0)
				{
					if(tmp == 0 || tmp == value.Length - 1)    throw new ApplicationException($" The address cannot start or end with @ ...");
                    if (value.IndexOf("@", tmp + 1) >= 0) throw new ApplicationException($" Too many  sumbols '@' ...");
                }
				else throw new ApplicationException($" Symbol '@' not found ...");
			    foreach (char c in value)
					if(!Char.IsAsciiDigit(c) && !Char.IsLetter(c) && c != '_' && c != '@') throw new ApplicationException($" Invalid  email addres symbol \'{c}\' ...");
                _email = value;

                //var email = new EmailAddressAttribute();
                //if (email.IsValid(value)) _email = value;
                //else throw new ApplicationException(" Invalid email addres ...");
            }
		}

		public string Password
		{
			set 
			{
				bool num = false,simb = false; 
                if (value.Length < 6 ) throw new ApplicationException(" Not enough symbols in password ...");
				foreach (var item in value)
				{
					if (!num && Char.IsDigit(item)) num = true;
					else if (!simb && Char.IsLetter(item)) simb = true;
					if (simb && num)
					{
						_password = value;
						return;
					}
				}
                throw new ApplicationException(" Password must contain at least one leter and at least one digit ...");
            }
		}
		public bool CheckPassword(string pass) => pass == _password;
		
		public void SetAccountInfo()
		{
			uint count = 0;
			do
			{
				try
				{
					switch (count)
					{
						case 0:
							Console.Write(" Enter email addres : ");
							Email = Console.ReadLine();
							break;
						case 1:
                            Console.Write(" Enter password : ");
							Password = Console.ReadLine();
                            break;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.ReadKey();
					Console.Clear();
					continue;
				}
				++count;
			} while (count != 2);
			Console.WriteLine(" Account information saved...");
			Console.ReadKey();
		}

        public string GetAccountInfo()
		{
			StringBuilder sb = new StringBuilder($" Email    : {_email ?? "Error"}\n");
            Console.Write(" Enter password : ");
			if (_password == Console.ReadLine())
                 sb.AppendLine($" Password : {_password}");
			else sb.AppendLine($" Password : {new string('*',_password?.Length ?? 0)}");
            return sb.ToString();
        }

    }
}
