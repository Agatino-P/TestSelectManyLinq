using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<string> _inputData=new ObservableCollection<string>(); 
        public ObservableCollection<string> InputData { get => _inputData; set { Set(() => InputData, ref _inputData, value); }}

        private RelayCommand _testCmd;
        public RelayCommand TestCmd => _testCmd ?? (_testCmd = new RelayCommand(
            () => test(),
            () => { return 1 == 1; },
            keepTargetAlive: true
            ));

        private void test()
        {
            //DoTheWork
            foreach(var el in InputData)
            {
                outputData.Add(new Permutation { el,el,el });
            }

            //RenderTheWork
            foreach (Permutation permut in outputData)
            {
                Results.Add(listToString<string>(InputData));
            }

            //Results = new ObservableCollection<string>(
            //var flattened = outputData.SelectMany(p => listToString<string>(p));
                
                //permut,permut=>permut);
                
        }



        public MainWindowViewModel()
        {
            loadInputData();
        }

        private void loadInputData()
        {
            InputData.Add("Uno");
            InputData.Add("Due");
            InputData.Add("Tre");
            InputData.Add("Quattro");
        }

        private string listToString<T>(IEnumerable<T> inputEnumerable            )
        {
            StringBuilder sb = new StringBuilder();
            bool firstElement=true;
            foreach (T el in  inputEnumerable)
            {
                if (firstElement == false) sb.Append(",");
                firstElement = false;
                sb.Append(el.ToString());
            }
            return sb.ToString();
        }
    }
}
