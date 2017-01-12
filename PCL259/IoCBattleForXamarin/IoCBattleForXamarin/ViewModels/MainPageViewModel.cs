﻿using System;
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

        private readonly BenchEngine _benchEngine = new BenchEngine(1000);

        private readonly IContainer[] _containers = {
            new NewContainer(), 
            new AutoFacContainer(), 
            new AutoFacLambdaContainer(), 
            new UnityContainer(), 
            new NinjectContainer(), 
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
            await _benchEngine.Start(_containers);
            IsRunning.Value = false;
        }
    }
}