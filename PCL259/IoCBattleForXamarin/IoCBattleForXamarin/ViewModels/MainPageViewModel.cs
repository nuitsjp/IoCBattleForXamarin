using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using IoCBattleForXamarin.Containers;
using IoCBattleForXamarin.Models;
using Reactive.Bindings;
using Xamarin.Forms;

namespace IoCBattleForXamarin.ViewModels
{
    public class MainPageViewModel
    {
        public string MainText { get; set; } = "Hello, IoC Battle.";

        public int ExecuteCount { get; set; } = 100000;

        private readonly BenchEngine _benchEngine = new BenchEngine();

        private readonly IContainer[] _containers = {
            new NewContainer(), 
            new DryIocContainer(), 
            new SimpleInjectorContainer(), 
            new AutoFacContainer(), 
            new AutoFacLambdaContainer(), 
            new UnityContainer(), 
            //new NinjectContainer(), 
        };

        public ReactiveProperty<bool> IsRunning { get; } = new ReactiveProperty<bool>();

        public ReadOnlyReactiveCollection<BenchResult> BenchResults { get; }

        public ICommand StartCommand => new Command(Start);

        public MainPageViewModel()
        {
            BenchResults = _benchEngine.BenchResults.ToReadOnlyReactiveCollection();
        }
        private async void Start()
        {
            IsRunning.Value = true;
            await _benchEngine.Start(ExecuteCount, _containers);
            IsRunning.Value = false;
        }
    }
}
