using Moq;
using NUnit.Framework;
using Prism.Regions;
using SolidFrame.Core.Interfaces;
using SolidFrame.Core.Types;
using SolidFrame.Explorer.Types;
using SolidFrame.Explorer.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidFrame.Explorer.Test
{
	[TestFixture]
	public class DescribeConstruction
	{
		private ExplorerViewModel _explorerViewModel;
		private Mock<IExplorerViewModelDependencies> _dependenciesMock;
		private Mock<IExplorerItemFactory> _explorerItemFactoryMock;
		private Mock<IRegionManager> _regionManagerMock;
		private List<IDocumentCategory> _categories;
		private List<IDocument> _documents;

		[SetUp]
		public void BeforeEach()
		{
			_dependenciesMock = new Mock<IExplorerViewModelDependencies>();

			_regionManagerMock = new Mock<IRegionManager>();

			_explorerItemFactoryMock = new Mock<IExplorerItemFactory>();
			_explorerItemFactoryMock.Setup(f => f.CreateCategoryItem(It.IsAny<IDocumentCategory>(), It.IsAny<IEnumerable<IDocument>>()))
				.Returns<IDocumentCategory, IEnumerable<IDocument>>((dc, documents) => new ExplorerItem(dc, documents.Select(d => new ExplorerItem(d, _regionManagerMock.Object)))).Verifiable();
			_dependenciesMock.SetupGet(d => d.ExplorerItemFactory).Returns(_explorerItemFactoryMock.Object);

			var documentCategory1 = new DocumentCategory();
			var documentCategory2 = new DocumentCategory();

			_categories = new List<IDocumentCategory> {documentCategory1, documentCategory2};

			var documentCategoryCatalogMock = new Mock<IDocumentCategoryCatalog>();
			documentCategoryCatalogMock.SetupGet(dcc => dcc.List)
				.Returns(_categories);
			_dependenciesMock.SetupGet(d => d.DocumentCategoryCatalog).Returns(documentCategoryCatalogMock.Object);

			var category1Document1 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory1);
			var category1Document2 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory1);
			var category2Document1 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory2);
			var category2Document2 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory2);
			var category2Document3 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory2);

			_documents = new List<IDocument>
			{
				category1Document1,
				category1Document2,
				category2Document1,
				category2Document2,
				category2Document3
			};

			var documentCatalogMock = new Mock<IDocumentCatalog>();
			documentCatalogMock.SetupGet(dc => dc.List)
				.Returns(_documents);
			_dependenciesMock.SetupGet(d => d.DocumentCatalog).Returns(documentCatalogMock.Object);

			_explorerViewModel = new ExplorerViewModel(_dependenciesMock.Object);
		}

		[Test]
		public void It_calls_ExplorerItemFactory_CreateCategoryItem_for_each_DocumentCategory_in_Catalog_with_correct_Documents()
		{
			foreach (var documentCategory in _categories)
			{
				var categoryDocuments =
					_documents.Where(d => d.Category == documentCategory);

				_explorerItemFactoryMock.Verify(f => f.CreateCategoryItem(documentCategory, categoryDocuments));
			}
		}
	}
}
