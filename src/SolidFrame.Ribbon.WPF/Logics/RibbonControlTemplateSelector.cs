using SolidFrame.Core.Interfaces.Ribbon;
using System.Windows;
using System.Windows.Controls;

namespace SolidFrame.Ribbon.Logics
{
	public class RibbonControlTemplateSelector : DataTemplateSelector
	{
		public DataTemplate Button { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if(item is IRibbonButtonControl)
				return Button;

			return base.SelectTemplate(item, container);
		}
	}
}
