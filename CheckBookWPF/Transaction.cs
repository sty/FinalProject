using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBookWPF
{
    public class Transaction : BaseVM
    {
        public int Id { get; set; }

        private DateTime _Date;
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged(); }
        }
        private string _Payee;
        public string Payee
        {
            get { return _Payee; }
            set { _Payee = value; OnPropertyChanged(); }
        }
        public int AccountId { get; set; }
        private Account _Account;
        public virtual Account Account
        {
            get { return _Account; }
            set { _Account = value; OnPropertyChanged(); }
        }
        private double _Amount;
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged(); }
        }
        private string _Tag;
        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; OnPropertyChanged(); }
        }
    }

}
