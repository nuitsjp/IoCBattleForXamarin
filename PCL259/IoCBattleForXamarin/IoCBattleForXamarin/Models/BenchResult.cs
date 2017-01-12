using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCBattleForXamarin.Models
{
    public class BenchResult : NotifiableBase
    {
        private string _name;
        private string _mode;
        private TimeSpan _elapsed;
        private string _category;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Mode
        {
            get { return _mode; }
            set { SetProperty(ref _mode, value); }
        }

        public string Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        public TimeSpan Elapsed
        {
            get { return _elapsed; }
            set { SetProperty(ref _elapsed, value); }
        }
    }
}
