using Moq;
using NUnit.Framework;
using Prism.Regions;
using SolidFrame.Core.Interfaces;
using SolidFrame.Core.Types;
using SolidFrame.Explorer.Types;
using SolidFrame.Explorer.UI;
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
		private List<IDocumentConfiguration> _documentConfigurations;

		[SetUp]
		public void BeforeEach()
		{
			_dependenciesMock = new Mock<IExplorerViewModelDependencies>();

			_regionManagerMock = new Mock<IRegionManager>();

			_explorerItemFactoryMock = new Mock<IExplorerItemFactory>();
			_explorerItemFactoryMock.Setup(f => f.CreateCategoryItem(It.IsAny<IDocumentCategory>(), It.IsAny<IEnumerable<IDocumentConfiguration>>()))
				.Returns<IDocumentCategory, IEnumerable<IDocumentConfiguration>>((dc, documents) => new ExplorerItem(dc, documents.Select(d => new ExplorerItem(d, _regionManagerMock.Object)))).Verifiable();
			_dependenciesMock.SetupGet(d => d.ExplorerItemFactory).Returns(_explorerItemFactoryMock.Object);

			var documentCategory1 = new DocumentCategory();
			var documentCategory2 = new DocumentCategory();

			_categories = new List<IDocumentCategory> {documentCategory1, documentCategory2};

			var documentCategoryCatalogMock = new Mock<IDocumentCategoryCatalog>();
			documentCategoryCatalogMock.SetupGet(dcc => dcc.List)
				.Returns(_categories);
			_dependenciesMock.SetupGet(d => d.DocumentCategoryCatalog).Returns(documentCategoryCatalogMock.Object);

			//var category1Document1 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory1);
			var category1Document1Mock = new Mock<IDocumentConfiguration>();
			category1Document1Mock.Setup(c => c.CategoryId).Returns(documentCategory1.Id);
			//var category1Document2 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory1);
			var category1Document2Mock = new Mock<IDocumentConfiguration>();
			category1Document2Mock.Setup(c => c.CategoryId).Returns(documentCategory1.Id);
			//var category2Document1 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory2);
			var category2Document1Mock = new Mock<IDocumentConfiguration>();
			category2Document1Mock.Setup(c => c.CategoryId).Returns(documentCategory2.Id);
			//var category2Document2 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory2);
			var category2Document2Mock = new Mock<IDocumentConfiguration>();
			category2Document2Mock.Setup(c => c.CategoryId).Returns(documentCategory2.Id);
			//var category2Document3 = new Document(Guid.NewGuid(), "Test", typeof(object), documentCategory2);
			var category2Document3Mock = new Mock<IDocumentConfiguration>();
			category2Document3Mock.Setup(c => c.CategoryId).Returns(documentCategory2.Id);

			_documentConfigurations = new List<IDocumentConfiguration>
			{
				category1Document1Mock.Object,
				category1Document2Mock.Object,
				category2Document1Mock.Object,
				category2Document2Mock.Object,
				category2Document3Mock.Object
			};

			_dependenciesMock.SetupGet(d => d.DocumentConfigurations).Returns(_documentConfigurations);

			_explorerViewModel = new ExplorerViewModel(_dependenciesMock.Object);
		}

		[Test]
		public void It_calls_ExplorerItemFactory_CreateCategoryItem_for_each_DocumentCategory_in_Catalog_with_correct_Documents()
		{
			foreach (var documentCategory in _categories)
			{
				var category = documentCategory;
				var categoryDocuments =
					_documentConfigurations.Where(d => d.CategoryId == category.Id);

				_explorerItemFactoryMock.Verify(f => f.CreateCategoryItem(category, categoryDocuments));
			}
		}
	}
}
