using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBookWPF
{
    class AccountVM:BaseVM
    {
        CbDb _Db = new CbDb();
        public bool isSave = false;

        private Account _Data = new Account { };
        public Account Data
        {
            get { return _Data; }
            set { _Data = value; OnPropertyChanged(); }
        }

        public DelegateCommand SaveAccount
        {
            get
            {
                return new DelegateCommand
                {
                    ExecuteFunction = _ =>
                    {
                        _Db.Accounts.Add(_Data);
                        _Db.SaveChanges(); isSave = true;
                    },
                   
                };
            }
        }
    }
}
