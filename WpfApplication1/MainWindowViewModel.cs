using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;


namespace WpfApplication1
{
    public class MainWindowViewModel : ViewModelBase 
    {
        private readonly IList<TestDataRowDto> _data = new List<TestDataRowDto>();        

        public MainWindowViewModel()
        {            
            for (int i = 0; i < 2; i++)
            {
                _data.Add(new TestDataRowDto {Name = "Device " + i, Amount = 42, Category = "Console"});
                _data.Add(new TestDataRowDto {Name = "Small Device " + i, Amount = 42, Category = "Switch"});
                _data.Add(new TestDataRowDto {Name = "Black Device Extended " + i, Amount = 356, Category = "Switch"});
            }

            SelectionChangedCommand = new RelayCommand<IList>(OnSelectionChangedCommandExecuted);
        }

        public IList<TestDataRowDto> TestData
        {
            get { return _data; }
        }

        #region try out binding to SelectedItems (plural! - bindind to SelectedItem works out of the box)

        public IList SelectedItems { get; set; }

        private int _numberOfItemsSelected;
        public int NumberOfItemsSelected
        {
            get { return _numberOfItemsSelected; }
            set
            {
                if (_numberOfItemsSelected == value) return;
                _numberOfItemsSelected = value;
                RaisePropertyChanged(() => NumberOfItemsSelected);                
            }
        }

        public RelayCommand<IList> SelectionChangedCommand { get; private set; }

        private void OnSelectionChangedCommandExecuted(IList items)
        {
            if (items == null)
            {
                NumberOfItemsSelected = 0; 
                return;
            }
            NumberOfItemsSelected = items.Count;
            SelectedItems = items;

        }

        #endregion

        #region handling insertions

        // see also: http://doc.xceedsoft.com/products/xceedwpfdatagrid/Inserting_Data.html
        // or: http://doc.xceedsoft.com/products/XceedWpfToolkit/#Inserting_Data.html

        
        // InsertionRow is a premium feature!!
        //http://doc.xceedsoft.com/products/XceedWpfToolkit/#Xceed.Wpf.DataGrid.html

        #endregion
    }

    public class TestDataRowDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }
    }
}
