using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Xceed.Wpf.DataGrid;


namespace WpfApplication1
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        public MainWindowViewModel()
        {
            ProvideTestData();
            
            SelectionChangedCommand = new RelayCommand<IList>(OnSelectionChangedCommandExecuted);
            CancelEditCommand = new RelayCommand(OnCancelEditExecuted);
            GroupByCategoryCommand = new RelayCommand(OnGroupByCategoryCommandExecuted);
        }

        #region provide test data

        public DataGridCollectionView TestData { get; private set; }
        private void ProvideTestData()
        {
            var data = new List<TestDataRowDto>();
            for (int i = 0; i < 2; i++)
            {
                data.Add(new TestDataRowDto { Name = "Device " + i, Amount = 42, Category = "Console" });
                data.Add(new TestDataRowDto { Name = "Small Device " + i, Amount = 42, Category = "Switch" });
                data.Add(new TestDataRowDto { Name = "Black Device Extended " + i, Amount = 356, Category = "Switch" });
            }
            TestData = new DataGridCollectionView(data);
        }

        #endregion

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

        // But we can add new Items by code with the DataGridCollectionView:
        // TestData.AddNew();

        #endregion

        #region test edit mode with binding/ ViewModel

        public RelayCommand CancelEditCommand { get; set; }

        private void OnCancelEditExecuted()
        {
            // - Das Editieren per Code abbrechen ist möglich durch das Aufrufen der CancelEdit Methode der Row (code behind).
            //   Doc: http://doc.xceedsoft.com/products/xceedwpfdatagrid/Editing_Data.html
                       
            // Wenn das Beenden des Editierens vom ViewModel ausgehen soll (Vermeidung von code behind) kann eine DataGridCollectionView verwendet werden:
            TestData.CancelEdit();
            TestData.MoveCurrentToNext(); // Hack for reset the selection :-(            

            // - Ebenso kann durch das Abfangen des Events dataGrid.EditBeginning verhindert werden, dass der Edit-Modus überhaupt aktiv wird.
            //   Hierbei kann man natürlich auch Eigenschaften des ViewModels (".DataContext") wie ein Binding hinzuziehen.
            //   Bsp.: http://xceed.com/CS/forums/thread/27955.aspx
        }

        #endregion

        #region test grouping by ViewModel

        public RelayCommand GroupByCategoryCommand { get; set; }

        private void OnGroupByCategoryCommandExecuted()
        {
           TestData.GroupDescriptions.Add(new DataGridGroupDescription("Category"));            
        }

        #endregion

    }

    public class TestDataRowDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }
    }
}
