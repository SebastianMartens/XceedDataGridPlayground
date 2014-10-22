using System.Collections.Generic;
using WpfApplication1.Infrastructure;

namespace WpfApplication1
{
    public class MainWindowViewModel: ViewModelBase
    {     
        private IList<TestDataRowDto> _data = new List<TestDataRowDto>();
        
        public MainWindowViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                _data.Add(new TestDataRowDto { Name = "Device " + i, Amount = 42, Category = "Console" });
                _data.Add(new TestDataRowDto { Name = "Small Device " +i, Amount = 42, Category = "Switch" });
                _data.Add(new TestDataRowDto { Name = "Black Device Extended " +i, Amount = 356, Category = "Switch" });    
            }
            
        }

        public IList<TestDataRowDto> TestData
        {
            get
            {
                return _data;
            }
        }



    }

    public class TestDataRowDto
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }
    }
}
