using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TestSelectMany
{
    public class MainWindowViewModel : ViewModelBase
    {
        private class Permutation : List<string> { }
        private readonly List<Permutation> outputData = new List<Permutation>();

        private ObservableCollection<string> _results = new ObservableCollection<string>();
        public ObservableCollection<string> Results { get => _results; set => Set(() => Results, ref _results, value); }

        private ObservableCollection<string> _inputData = new ObservableCollection<string>();
        public ObservableCollection<string> InputData { get => _inputData; set => Set(() => InputData, ref _inputData, value); }

        private RelayCommand<string> _testCmd;
        public RelayCommand<string> TestCmd => _testCmd ?? (_testCmd = new RelayCommand<string>(
            (s) => test(int.Parse(s)),
            (s) => { return 1 == 1; },
            keepTargetAlive: true
            ));
        private void test(int i)
        {
            switch (i)
            {
                case 1:
                    doWork1();
                    break;
                case 2:
                    doWork2();
                    break;
                case 3:
                    doWork3();
                    break;

                default:
                    break;
            }
        }

        public MainWindowViewModel()
        {
            loadInputData();
        }

        private void loadInputData()
        {
            for (int i = 0; i < 4; i++)
            {
                InputData.Add(i.ToString());
            }
        }

            static int count = 0;
       
        public static bool func1<T>(IEnumerable<T> en_t, T t)
        {
            
            Debug.WriteLine(++count);
            return !en_t.Contains(t);
        }

        private static IEnumerable<IEnumerable<T>> getPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }
            //forDebug
            return  getPermutations(list, length - 1)
              .SelectMany(
                  t => list.Where(e=>func1<T>(t,e)),
                  (t1, t2) => t1.Concat(new T[] { t2 })
                  );

            /*real
            return getPermutations(list, length - 1)
              .SelectMany(
                  t => list.Where(e => !t.Contains(e)),
                  (t1, t2) => t1.Concat(new T[] { t2 })
                  );
            */
        }

        private void doWork3()
        {
            Results.Clear();
            Stopwatch sw = new Stopwatch();

            sw.Start();
            IEnumerable<IEnumerable<string>> res = getPermutations<string>(InputData, InputData.Count);
            sw.Stop();
            Results.Add($"Calculated in {sw.ElapsedMilliseconds}ms");

            sw.Start();
            foreach (IEnumerable<string> el in res)
            {
                Results.Add(enumerableToString(el));
            }
            sw.Stop();
            Results.Insert(1,$"Displayed in {sw.ElapsedMilliseconds}ms");

        }

        private string enumerableToString<T>(IEnumerable<T> inputEnumerable)
        {
            StringBuilder sb = new StringBuilder();
            bool firstElement = true;
            foreach (T el in inputEnumerable)
            {
                if (firstElement == false)
                {
                    sb.Append(",");
                }

                firstElement = false;
                sb.Append(el.ToString());
            }
            return sb.ToString();
        }

        private void doWork1()
        {
            foreach (var el in InputData)
            {
                outputData.Add(new Permutation { el, el, el });
            }

            Results.Clear();
            foreach (Permutation permut in outputData)
            {
                Results.Add(enumerableToString<string>(InputData));
            }

        }
        private void doWork2()
        {
            foreach (var el in InputData)
            {
                outputData.Add(new Permutation { el, el, el });
            }

            Results.Clear();
            foreach (string el in outputData.SelectMany(perm => perm))
            {
                Results.Add(el);
            }
        }
        private class PetOwner
        {
            public string Name { get; set; }
            public List<string> Pets { get; set; }
        }
        private static void doWork4()
        {
            PetOwner[] petOwners ={
                new PetOwner { Name="Higa",Pets = new List<string>{ "Scruffy", "Sam" } },
                new PetOwner { Name="Ashkenazi",Pets = new List<string>{ "Walker", "Sugar" } },
                new PetOwner { Name="Price",Pets = new List<string>{ "Scratches", "Diesel" } },
                new PetOwner { Name="Hines",Pets = new List<string>{ "Dusty" } }
                                    };
            var query = petOwners.SelectMany(
                   petOwner => petOwner.Pets,
                   (petOwner, petName) => new { petOwner, petName }
                   );
        }

    }
}
