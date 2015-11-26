using System.Windows.Controls;

namespace Example.WPF.Person.UI
{
	/// <summary>
	/// Interaction logic for PersonView.xaml
	/// </summary>
	public partial class PersonView : UserControl
	{
		public PersonView(IPersonListViewModel listViewModel)
		{
			DataContext = listViewModel;
			InitializeComponent();
		}
	}
}
