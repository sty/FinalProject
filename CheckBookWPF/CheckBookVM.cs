using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows.Input;

namespace CheckBookWPF
{
    public class CheckBookVM : BaseVM
    {
        public CheckBookVM()
        {

        }
        CbDb _Db = new CbDb();
       
        private ObservableCollection<Transaction> _Transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; OnPropertyChanged(); OnPropertyChanged("Accounts"); }
        }

        private ObservableCollection<Account> _Accounts;
        public ObservableCollection<Account> Accounts
        {
            get { return _Accounts; }
            set { _Accounts = value; OnPropertyChanged(); }
        }
        public Transaction _newTransaction = new Transaction { Date = DateTime.Now };
        public Transaction newTransaction 
        {
            get { return _newTransaction; }
            set { _newTransaction = value; OnPropertyChanged(); }

    }
        public Account _NewAccount = new Account();
        public Account NewAccount
        {
            get { return _NewAccount; }
            set { _NewAccount = value; OnPropertyChanged(); }
        }

        private DelegateCommand _Save;
        public ICommand Save
        {
            get
            {
                if (_Save == null)
                {
                    _Save = new DelegateCommand
                    {
                        ExecuteFunction = x =>
                        {
                            _Db.Transactions.Add(_newTransaction);
                            Account query= (from i in Accounts where i == _newTransaction.Account select i).Single();
                            query.Amount += _newTransaction.Amount;
                            _Db.SaveChanges(); newTransaction = new Transaction { Date = DateTime.Now };

                        },
                        CanExecuteFunction = x => newTransaction.Amount != 0 && newTransaction.Account != null
                    };
                    _newTransaction.PropertyChanged += (s, e) => _Save.OnCanExecuteChanged();
                }
                return _Save;
            }
        }

        private DelegateCommand _Create_Account;
        public ICommand Create_Account
        {
            get
            {

                return new DelegateCommand
                {
                    ExecuteFunction = _ =>
                    {
                        _Db.Accounts.Add(_NewAccount);
                        _Db.SaveChanges(); NewAccount = new Account();
                    },
                    CanExecuteFunction = x => NewAccount.Name != null && NewAccount.Institution !=null
                };

            }
        }

        

        public void Fill()
        {
            Transactions = _Db.Transactions.Local;
            Accounts = _Db.Accounts.Local;
            _Db.Accounts.ToList();
            _Db.Transactions.ToList();
        }
    }
}
