using Moq;
using NUnit.Framework;
using SolidFrame.Ribbon.Logics;
using SolidFrame.Ribbon.Types;
using SolidFrame.Ribbon.UI;

namespace SolidFrame.Ribbon.Test
{
	[TestFixture]
	public class DescribeConstruction
	{
		private RibbonViewModel _ribbonViewModel;
		private Mock<IRibbonTabFactory> _tabFactoryMock; 
		private Mock<IRibbonViewModelDependencies> _dependenciesMock;
			
		[SetUp]
		public void BeforeEach()
		{
			_tabFactoryMock = new Mock<IRibbonTabFactory>();
			_tabFactoryMock.Setup(f => f.Create("Crud"));

			_dependenciesMock = new Mock<IRibbonViewModelDependencies>();
			_dependenciesMock.SetupGet(d => d.RibbonTabFactory).Returns(_tabFactoryMock.Object);

			_ribbonViewModel = new RibbonViewModel(_dependenciesMock.Object);
		}

		[Test]
		public void It_creates_the_first_RibbonTab()
		{
			_tabFactoryMock.Verify(f => f.Create("Crud"), Times.Once);
		}
	}
}
