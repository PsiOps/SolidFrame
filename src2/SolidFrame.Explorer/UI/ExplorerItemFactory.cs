using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Core.Types;
using System;
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
		private IExplorerItem CreateDocumentItem(IDocumentConfiguration documentConfiguration, Action loadDocumentAction)
		{
			return new ExplorerItem(documentConfiguration, loadDocumentAction);
		}

		public IExplorerItem CreateCategoryItem(IDocumentCategory documentCategory, IEnumerable<IDocumentConfiguration> documentConfigurations, )
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
