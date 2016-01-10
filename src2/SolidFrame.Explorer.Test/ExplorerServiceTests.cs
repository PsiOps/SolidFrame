using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces.Explorer;
using SolidFrame.Explorer.Logics;
using SolidFrame.Explorer.Types;
using SolidFrame.Explorer.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SolidFrame.Explorer.Test
{
	[TestFixture]
	public class DescribeExplorerServiceAddExplorerItem
	{
		private Mock<IExplorerViewModel> _explorerViewModelMock;
		private Mock<IExplorerItemViewModelFactory> _itemViewModelFactoryMock;
		private IExplorerService _explorerService;
		private ExplorerItem _mainNode;
		private Collection<IExplorerItemViewModel> _viewModels;

		[SetUp]
		public void BeforeEach()
		{
			var dependenciesMock = new Mock<IExplorerServiceDependencies>();

			_explorerViewModelMock = new Mock<IExplorerViewModel>();

			_viewModels = new Collection<IExplorerItemViewModel>();

			_explorerViewModelMock.Setup(vm => vm.ExplorerItemViewModels).Returns(_viewModels);

			dependenciesMock.SetupGet(d => d.ExplorerViewModel).Returns(_explorerViewModelMock.Object);

			_itemViewModelFactoryMock = new Mock<IExplorerItemViewModelFactory>();

			_itemViewModelFactoryMock.Setup(
				f => f.Create(It.IsAny<IExplorerItem>(), It.IsAny<IEnumerable<IExplorerItemViewModel>>()))
				.Returns<IExplorerItem, IEnumerable<IExplorerItemViewModel>>((i, c) => new ExplorerItemViewModel(i));

			dependenciesMock.SetupGet(d => d.ExplorerItemViewModelFactory).Returns(_itemViewModelFactoryMock.Object);

			_explorerService = new ExplorerService(dependenciesMock.Object);

			var level2Nodes1Node1 = new ExplorerItem("EndNode1", () => { });

			var level2Nodes1 = new Collection<IExplorerItem>
			{
				level2Nodes1Node1
			};

			var level2Nodes2Node1 = new ExplorerItem("EndNode2", () => { });

			var level2Nodes2 = new Collection<IExplorerItem>
			{
				level2Nodes2Node1
			};

			var level1Node1 = new ExplorerItem("Level1Node1", () => { }, level2Nodes1);
			var level1Node2 = new ExplorerItem("Level1Node2", () => { }, level2Nodes2);

			var level1Nodes = new Collection<IExplorerItem>
			{
				level1Node1,
				level1Node2
			};

			_mainNode = new ExplorerItem("MainNode", () => { }, level1Nodes);

			_explorerService.AddExplorerItem(_mainNode);
		}

		[Test]
		public void It_creates_ViewModels_for_all_items_recursively()
		{
			_itemViewModelFactoryMock.Verify(f => f.Create(It.IsAny<IExplorerItem>(), It.IsAny<IEnumerable<IExplorerItemViewModel>>()), Times.Exactly(5));
		}

		[Test]
		public void It_adds_the_node_to_the_ExplorerViewModel_DataSource()
		{
			Assert.IsTrue(_viewModels.Any(i => i.Name == _mainNode.Name));
		}
	}
}
