using Moq;
using NUnit.Framework;
using Prism.Regions;
using SolidFrame.Core.Types;
using SolidFrame.Explorer.UI;
using SolidFrame.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Explorer.Test
{
	[TestFixture]
    public class DescribeCategoryConstruction
	{
		private ExplorerItem _explorerItem;
		private DocumentCategory _documentCategory;
		private ICollection<ExplorerItem> _items;
			
		[SetUp]
		public void BeforeEach()
		{
			var regionManagerMock = new Mock<IRegionManager>();

			_documentCategory = new DocumentCategory {Name = "Test"};

			_items  = new Collection<ExplorerItem>
			{
				new ExplorerItem(new Document(Guid.NewGuid(), "Test", typeof(object), _documentCategory), regionManagerMock.Object),
				new ExplorerItem(new Document(Guid.NewGuid(), "Test", typeof(object), _documentCategory), regionManagerMock.Object),
			};

			_explorerItem = new ExplorerItem(_documentCategory, _items);
		}

		[Test]
		public void It_sets_the_name_of_the_document_category()
		{
			Assert.AreEqual(_documentCategory.Name, _explorerItem.Name);
		}

		[Test]
		public void It_sets_the_child_items()
		{
			CollectionAssert.AreEqual(_items, _explorerItem.Items);
		}

		[Test]
		public void It_sets_IsExpanded_to_true()
		{
			Assert.IsTrue(_explorerItem.IsExpanded);
		}

		[Test]
		public void It_toggles_IsExpanded_when_clicked()
		{
			_explorerItem.ClickCommand.Execute(null);

			Assert.IsFalse(_explorerItem.IsExpanded);
		}
    }

	[TestFixture]
	public class DescribeDocumentConstruction
	{
		private ExplorerItem _explorerItem;
		private Document _document;
		private Mock<IRegionManager> _regionManagerMock;
			
		[SetUp]
		public void BeforeEach()
		{
			_document = new Document(Guid.NewGuid(), "Test", typeof(object), new DocumentCategory());

			_regionManagerMock = new Mock<IRegionManager>();

			_regionManagerMock.Setup(rm => rm.RegisterViewWithRegion(Regions.Document, typeof (object)));

			_explorerItem = new ExplorerItem(_document, _regionManagerMock.Object);
		}

		[Test]
		public void It_sets_the_name_of_the_document()
		{
			Assert.AreEqual(_document.Name, _explorerItem.Name);
		}

		[Test]
		public void It_calls_regionmanager_RegisterViewWithRegion_method_when_clicked()
		{
			_explorerItem.ClickCommand.Execute(null);

			_regionManagerMock.VerifyAll();
		}
	}

}
