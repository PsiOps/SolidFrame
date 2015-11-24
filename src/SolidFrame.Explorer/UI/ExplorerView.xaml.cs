
namespace SolidFrame.Explorer.UI
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
