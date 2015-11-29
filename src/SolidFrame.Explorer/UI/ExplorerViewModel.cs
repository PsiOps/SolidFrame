﻿using SolidFrame.Core.Interfaces;
using SolidFrame.Explorer.Types;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SolidFrame.Explorer.UI
{
	public interface IExplorerViewModel : IListViewModel
	{
		ObservableCollection<ExplorerItem> ItemCategories { get; set; }
	}

	public class ExplorerViewModel : IExplorerViewModel
	{
		public ObservableCollection<ExplorerItem> ItemCategories { get; set; }

		public ExplorerViewModel(IExplorerViewModelDependencies dependencies)
		{
			var explorerItemFactory = dependencies.ExplorerItemFactory;
			var documentCategoryCatalog = dependencies.DocumentCategoryCatalog;
			var documents = dependencies.DocumentConfigurations;

			ItemCategories = new ObservableCollection<ExplorerItem>();

			foreach (var documentCategory in documentCategoryCatalog.List)
			{
				var category = documentCategory;

				var categoryItem = explorerItemFactory.CreateCategoryItem(documentCategory, documents.Where(d => d.CategoryId == category.Id));

				ItemCategories.Add(categoryItem);
			}
		}

		public Guid Id { get {return new Guid("AFDFF6A8-5549-44DB-83FF-ED699C6005B8");}}
		public string Title { get { return "TK_Navigation"; }}
	}
}
