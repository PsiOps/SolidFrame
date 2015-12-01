using Prism.Regions;
using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Core.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Explorer.UI
{
	public interface IExplorerItemFactory
	{
		IExplorerItem CreateCategoryItem(IDocumentCategory documentCategory, IEnumerable<IDocumentConfiguration> documentConfigurations);
	}

	public class ExplorerItemFactory : IExplorerItemFactory
	{
		private readonly IRegionManager _regionManager;

		public ExplorerItemFactory(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		private IExplorerItem CreateDocumentItem(IDocumentConfiguration documentConfiguration)
		{
			return new ExplorerItem(documentConfiguration, _regionManager);
		}

		public IExplorerItem CreateCategoryItem(IDocumentCategory documentCategory, IEnumerable<IDocumentConfiguration> documentConfigurations)
		{
			var documentItems = new Collection<IExplorerItem>();

			foreach (var documentConfiguration in documentConfigurations)
			{
				documentItems.Add(CreateDocumentItem(documentConfiguration));
			}

			return new ExplorerItem(documentCategory, documentItems);
		}
	}
}
