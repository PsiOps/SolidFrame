
using SolidFrame.Explorer.UI;

namespace SolidFrame.Explorer.WPF.UI
{
	/// <summary>
	/// Interaction logic for ExplorerView.xaml
	/// </summary>
	public partial class ExplorerView
	{
		public ExplorerView(IExplorerViewModel viewModel)
		{
			DataContext = viewModel;
			InitializeComponent();
		}
	}
}
