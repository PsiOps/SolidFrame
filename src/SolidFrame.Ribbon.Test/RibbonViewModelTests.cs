using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Logics;
using SolidFrame.Ribbon.Types;
using SolidFrame.Ribbon.UI;
using System.Collections.ObjectModel;
using System.Linq;

namespace SolidFrame.Ribbon.Test
{
	[TestFixture]
	public class DescribeConstruction
	{
		private RibbonViewModel _ribbonViewModel;
		private Mock<IRibbonTabFactory> _tabFactoryMock;
		private Mock<ICrudGroupController> _crudGroupControllerMock; 
		private Mock<IRibbonViewModelDependencies> _dependenciesMock;
			
		[SetUp]
		public void BeforeEach()
		{
			_tabFactoryMock = new Mock<IRibbonTabFactory>();
			_tabFactoryMock.Setup(f => f.Create("Crud")).Returns(new RibbonTab("Crud"));

			_crudGroupControllerMock = new Mock<ICrudGroupController>();
			_crudGroupControllerMock.Setup(c => c.RibbonControlGroups).Returns(new Collection<IRibbonControlGroup>{new RibbonControlGroup("Test")});

			_dependenciesMock = new Mock<IRibbonViewModelDependencies>();
			_dependenciesMock.SetupGet(d => d.RibbonTabFactory).Returns(_tabFactoryMock.Object);
			_dependenciesMock.SetupGet(d => d.CrudGroupController).Returns(_crudGroupControllerMock.Object);

			_ribbonViewModel = new RibbonViewModel(_dependenciesMock.Object);
		}

		[Test]
		public void It_creates_the_first_RibbonTab()
		{
			_tabFactoryMock.Verify(f => f.Create("Crud"), Times.Once);
		}

		[Test]
		public void It_adds_the_CrudGroupController_s_RibbonControlGroups_to_the_Crud_tab()
		{
			Assert.AreEqual(1, _ribbonViewModel.RibbonTabs.Single().RibbonControlGroups.Count);
			Assert.AreEqual("Test", _ribbonViewModel.RibbonTabs.Single().RibbonControlGroups.Single().Name);
		}
	}
}
