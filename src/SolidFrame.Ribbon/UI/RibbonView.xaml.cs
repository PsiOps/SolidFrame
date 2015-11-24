using System.Windows.Controls;

namespace SolidFrame.Ribbon.UI
{
	/// <summary>
	/// Interaction logic for RibbonView.xaml
	/// </summary>
	public partial class RibbonView : UserControl
	{
		public RibbonView(IRibbonViewModel viewModel)
		{
			DataContext = viewModel;
			InitializeComponent();
		}
	}
}
