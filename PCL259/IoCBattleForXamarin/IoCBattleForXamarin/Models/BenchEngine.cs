using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using IoCBattleForXamarin.Containers;

namespace IoCBattleForXamarin.Models
{
	public class BenchEngine
	{

        private int Count { get; }

	    public ObservableCollection<BenchResult> BenchResults { get; } = new ObservableCollection<BenchResult>();

        public BenchEngine(int count = 100000)
        {
            Count = count;
        }

        public Task Start(IContainer[] containers)
		{
		    return Task.Run(() =>
		    {
		        foreach (var container in containers)
		        {
                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                    GC.WaitForPendingFinalizers();
                    // Thread.Sleep(1000);

                    RunBenchmark(container, container.SetupForSingletonTest, "Singleton");

                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                    GC.WaitForPendingFinalizers();
                    // Thread.Sleep(1000);

                    RunBenchmark(container, container.SetupForTransientTest, "Transient");
                }
            });
		}

		private void RunBenchmark(IContainer container, Action setupAction, string mode)
		{
			var regTimer = new Stopwatch();
			var resolveTimer = new Stopwatch();

			regTimer.Start();
			setupAction();
			regTimer.Stop();

            BenchResults.Add(new BenchResult
            {
                Name = container.Name,
                Mode = mode,
                Category = "Registartion",
                Elapsed = regTimer.Elapsed
            });

			resolveTimer.Start();

			for (int i = 0; i < Count; i++)
			{
				var instance = container.Resolve<IWebService>();
			}

			resolveTimer.Stop();
            BenchResults.Add(new BenchResult
            {
                Name = container.Name,
                Mode = mode,
                Category = "Component",
                Elapsed = resolveTimer.Elapsed
            });
		}
	}
}