using Prism.Regions;
using SolidFrame.Core.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SolidFrame.Core.Interfaces;

namespace SolidFrame.Explorer.UI
{
	public interface IExplorerItemFactory
	{
		ExplorerItem CreateCategoryItem(IDocumentCategory documentCategory, IEnumerable<IDocumentConfiguration> documentConfigurations);
	}

	public class ExplorerItemFactory : IExplorerItemFactory
	{
		private readonly IRegionManager _regionManager;

		public ExplorerItemFactory(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		private ExplorerItem CreateDocumentItem(IDocumentConfiguration documentConfiguration)
		{
			return new ExplorerItem(documentConfiguration, _regionManager);
		}

		public ExplorerItem CreateCategoryItem(IDocumentCategory documentCategory, IEnumerable<IDocumentConfiguration> documentConfigurations)
		{
			var documentItems = new Collection<ExplorerItem>();

			foreach (var documentConfiguration in documentConfigurations)
			{
				documentItems.Add(CreateDocumentItem(documentConfiguration));
			}

			return new ExplorerItem(documentCategory, documentItems);
		}
	}
}
