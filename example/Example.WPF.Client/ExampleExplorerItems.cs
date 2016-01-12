using Example.WPF.Person.UI;
using Prism.Regions;
using SolidFrame.Core.Interfaces.Explorer;
using SolidFrame.Explorer.Types;
using SolidFrame.Resources;
using System.Collections.ObjectModel;

namespace Example.WPF.Client
{
	public interface IExampleExplorerItems
	{
		IExplorerItem TopNode { get; }
	}

	public class ExampleExplorerItems : IExampleExplorerItems
	{
		private readonly IRegionManager _regionManager;

		public ExampleExplorerItems(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public IExplorerItem TopNode { get
		{
			return new ExplorerItem("Example", () => { }, new Collection<IExplorerItem>
			{
				new ExplorerItem("Person", () => _regionManager.RegisterViewWithRegion(Regions.Document,
					typeof (PersonView)))
			});
		}}
	}
}
