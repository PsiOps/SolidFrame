using SolidFrame.Ribbon.Logics;

namespace SolidFrame.Ribbon.Types
{
	public interface ICrudControlServiceDependencies
	{
		IRibbonControlFactory RibbonControlFactory { get; }
	}

	public class CrudControlServiceDependencies : ICrudControlServiceDependencies
	{
		public IRibbonControlFactory RibbonControlFactory { get; private set; }
	}
}