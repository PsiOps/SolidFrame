using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Logics;
using SolidFrame.Ribbon.Types;
using System;

namespace SolidFrame.Ribbon.Test
{
	[TestFixture]
	public class DescribeCrudControlServiceConstruction
	{
		private CrudControlService _crudControlService;
		private Mock<IRibbonButtonControl> _buttonMock; 
		private Mock<IRibbonControlFactory> _ribbonControlFactoryMock;

		[SetUp]
		public void BeforeEach()
		{
			_buttonMock = new Mock<IRibbonButtonControl>();

			_ribbonControlFactoryMock = new Mock<IRibbonControlFactory>();
			_ribbonControlFactoryMock.Setup(f => f.GetRibbonButton()).Returns(_buttonMock.Object).Verifiable();

			var dependenciesMock = new Mock<ICrudControlServiceDependencies>();
			dependenciesMock.SetupGet(d => d.RibbonControlFactory).Returns(_ribbonControlFactoryMock.Object);

			_crudControlService = new CrudControlService(dependenciesMock.Object);
		}

		[Test]
		public void It_sets_the_AddButton_property_with_the_RibbonControlFactory_CreateRibbonButton_method_output()
		{
			_ribbonControlFactoryMock.Verify(f => f.GetRibbonButton(), Times.Once);

			Assert.AreEqual(_buttonMock.Object, _crudControlService.AddButton);
		}
	}

	[TestFixture]
	public class DescribeRegistrationOfAddListViewModel
	{
		private Mock<IListViewModel> _listViewModelMock;
		private CrudControlService _crudControlService;

		private Mock<IRibbonButtonControl> _buttonMock;
		private Mock<IRibbonControlFactory> _ribbonControlFactoryMock;
		private Action _buttonExecuteAction;
		private Func<bool> _buttonCanExecuteFunc;

		[SetUp]
		public void BeforeEach()
		{
			_listViewModelMock = new Mock<IListViewModel>();

			_buttonMock = new Mock<IRibbonButtonControl>();

			_buttonMock.SetupSet(b => b.ExecuteAction = It.IsAny<Action>()).Callback<Action>(a => _buttonExecuteAction = a);
			_buttonMock.SetupSet(b => b.CanExecute = It.IsAny<Func<bool>>()).Callback<Func<bool>>(f => _buttonCanExecuteFunc = f);
			_buttonMock.Setup(b => b.RaiseCanExecuteChanged()).Verifiable();

			_ribbonControlFactoryMock = new Mock<IRibbonControlFactory>();
			_ribbonControlFactoryMock.Setup(f => f.GetRibbonButton()).Returns(_buttonMock.Object);

			var dependenciesMock = new Mock<ICrudControlServiceDependencies>();
			dependenciesMock.SetupGet(d => d.RibbonControlFactory).Returns(_ribbonControlFactoryMock.Object);

			_crudControlService = new CrudControlService(dependenciesMock.Object);
		}

		[Test]
		public void It_links_IAdd_IListViewModel_to_IRibbonViewModel_s_AddButton()
		{
			// If the IListViewModel implements IAdd interface
			_listViewModelMock.As<IAdd>().Setup(l => l.Add()).Verifiable();
			_listViewModelMock.As<IAdd>().Setup(l => l.CanAdd()).Verifiable();

			_crudControlService.Register(_listViewModelMock.Object);

			// It gives Add Action to ViewModel's Add Button Command
			_listViewModelMock.As<IAdd>().Verify(l => l.Add(), Times.Never);
			_buttonExecuteAction();
			_listViewModelMock.As<IAdd>().Verify(l => l.Add(), Times.Once);

			// It gives CanExecute function to ViewModel's Add Button Command
			_listViewModelMock.As<IAdd>().Verify(l => l.CanAdd(), Times.Never);
			_buttonCanExecuteFunc();
			_listViewModelMock.As<IAdd>().Verify(l => l.CanAdd(), Times.Once);

			// It subscribes RaiseCanExecuteChanged to IListViewModel's CanAddChanged event
			_buttonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Never);
			_listViewModelMock.As<IAdd>().Raise(l => l.CanAddChanged += null);
			_buttonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
		}

		[Test]
		public void It_disables_IRibbonViewModel_s_AddButton_if_IListViewModel_not_IAdd()
		{
			_crudControlService.Register(_listViewModelMock.Object);

			Assert.IsFalse(_buttonCanExecuteFunc());
		}
	}

	[TestFixture]
	public class DescribeListViewModelUnregistration
	{
		private Mock<IListViewModel> _listViewModelMock;
		private CrudControlService _crudControlService;
		private Mock<IRibbonControlFactory> _ribbonControlFactoryMock;

		private Mock<IRibbonButtonControl> _buttonMock;
		private Action _buttonExecuteAction;
		private Func<bool> _buttonCanExecuteFunc;

		[SetUp]
		public void BeforeEach()
		{
			_listViewModelMock = new Mock<IListViewModel>();

			_buttonMock = new Mock<IRibbonButtonControl>();

			_buttonMock.SetupSet(b => b.ExecuteAction = It.IsAny<Action>()).Callback<Action>(a => _buttonExecuteAction = a);
			_buttonMock.SetupSet(b => b.CanExecute = It.IsAny<Func<bool>>()).Callback<Func<bool>>(f => _buttonCanExecuteFunc = f);
			_buttonMock.Setup(b => b.RaiseCanExecuteChanged()).Verifiable();

			_ribbonControlFactoryMock = new Mock<IRibbonControlFactory>();
			_ribbonControlFactoryMock.Setup(f => f.GetRibbonButton()).Returns(_buttonMock.Object);

			var dependenciesMock = new Mock<ICrudControlServiceDependencies>();
			dependenciesMock.SetupGet(d => d.RibbonControlFactory).Returns(_ribbonControlFactoryMock.Object);

			_crudControlService = new CrudControlService(dependenciesMock.Object);

			_listViewModelMock.As<IAdd>().Setup(l => l.Add());
			_listViewModelMock.As<IAdd>().Setup(l => l.CanAdd());

			_crudControlService.Register(_listViewModelMock.Object);

			_listViewModelMock.As<IAdd>().Verify(l => l.Add(), Times.Never);
			_buttonExecuteAction();
			_listViewModelMock.As<IAdd>().Verify(l => l.Add(), Times.Once);

			_listViewModelMock.As<IAdd>().Verify(l => l.CanAdd(), Times.Never);
			_buttonCanExecuteFunc();
			_listViewModelMock.As<IAdd>().Verify(l => l.CanAdd(), Times.Once);

			_buttonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Never);
			_listViewModelMock.As<IAdd>().Raise(l => l.CanAddChanged += null);
			_buttonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
		}

		[Test]
		public void It_sets_AddButton_ExecuteAction_and_CanExecute_to_null_for_IAdd_ListViewModel()
		{
			_crudControlService.UnRegister(_listViewModelMock.Object);

			Assert.IsNull(_buttonExecuteAction);
			Assert.IsNull(_buttonCanExecuteFunc);
		}

		[Test]
		public void It_unregisters_RaiseCanExecuteChanged_from_IAdd_ListViewModel_s_CanAddChanged_event()
		{
			_crudControlService.UnRegister(_listViewModelMock.Object);

			_buttonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
			_listViewModelMock.As<IAdd>().Raise(l => l.CanAddChanged += null);
			_buttonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
		}
	}
}
