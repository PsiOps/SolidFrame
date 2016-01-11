using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces.Crud;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Ribbon.Logics;
using SolidFrame.Ribbon.Types;
using SolidFrame.Ribbon.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Ribbon.Test
{
	[TestFixture]
	public class DescribeCrudControlServiceConstruction
	{
		private CrudGroupController _crudGroupController;
		private Mock<IRibbonButtonControl> _addButtonMock;
		private Mock<IRibbonButtonControl> _saveButtonMock;
		private Mock<IRibbonControlGroup> _crudRibbonControlGroupMock;
		private Mock<IRibbonTab> _crudRibbonTabMock;
		private Mock<ICollection<IRibbonControlGroup>> _crudRibbonTabRibbonControlGroupsMock;
		private Mock<IRibbonTabFactory> _ribbonTabFactoryMock;
		private Mock<IRibbonControlFactory> _ribbonControlFactoryMock;
		private Mock<ICollection<IRibbonTab>> _ribbonViewModelTabsMock;
		private Mock<IRibbonViewModel> _ribbonViewModelMock;

		[SetUp]
		public void BeforeEach()
		{
			_addButtonMock = new Mock<IRibbonButtonControl>();
			_saveButtonMock = new Mock<IRibbonButtonControl>();

			_ribbonControlFactoryMock = new Mock<IRibbonControlFactory>();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonButton("TK_Add")).Returns(_addButtonMock.Object).Verifiable();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonButton("TK_Save")).Returns(_saveButtonMock.Object).Verifiable();

			_crudRibbonControlGroupMock = new Mock<IRibbonControlGroup>();
			_crudRibbonControlGroupMock.SetupAllProperties();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonControlGroup("TK_Crud")).Returns(_crudRibbonControlGroupMock.Object).Verifiable();

			_crudRibbonTabRibbonControlGroupsMock = new Mock<ICollection<IRibbonControlGroup>>();
			_crudRibbonTabRibbonControlGroupsMock.SetupAllProperties();

			_crudRibbonTabMock = new Mock<IRibbonTab>();
			_crudRibbonTabMock.Setup(t => t.RibbonControlGroups).Returns(_crudRibbonTabRibbonControlGroupsMock.Object);

			_ribbonTabFactoryMock = new Mock<IRibbonTabFactory>();
			_ribbonTabFactoryMock.Setup(f => f.Create("TK_Crud")).Returns(_crudRibbonTabMock.Object);

			_ribbonViewModelTabsMock = new Mock<ICollection<IRibbonTab>>();
			_ribbonViewModelTabsMock.SetupAllProperties();

			_ribbonViewModelMock = new Mock<IRibbonViewModel>();
			_ribbonViewModelMock.Setup(r => r.RibbonTabs).Returns(_ribbonViewModelTabsMock.Object).Verifiable();
			_ribbonViewModelMock.SetupSet(r => r.SelectedTab = _crudRibbonTabMock.Object).Verifiable();

			var dependenciesMock = new Mock<ICrudGroupControllerDependencies>();
			dependenciesMock.SetupGet(d => d.RibbonControlFactory).Returns(_ribbonControlFactoryMock.Object);
			dependenciesMock.SetupGet(d => d.RibbonTabFactory).Returns(_ribbonTabFactoryMock.Object);
			dependenciesMock.SetupGet(d => d.RibbonViewModel).Returns(_ribbonViewModelMock.Object);

			_crudGroupController = new CrudGroupController(dependenciesMock.Object);
		}

		[Test]
		public void It_creates_the_AddButton()
		{
			_ribbonControlFactoryMock.Verify(f => f.CreateRibbonButton("TK_Add"), Times.Once);

			Assert.AreEqual(_addButtonMock.Object, _crudGroupController.AddButton);
		}

		[Test]
		public void It_creates_the_SaveButton()
		{
			_ribbonControlFactoryMock.Verify(f => f.CreateRibbonButton("TK_Save"), Times.Once);

			Assert.AreEqual(_addButtonMock.Object, _crudGroupController.AddButton);
		}

		[Test]
		public void It_creates_the_Crud_RibbonTab()
		{
			_ribbonTabFactoryMock.Verify(f => f.Create("TK_Crud"), Times.Once);
		}

		[Test]
		public void It_creates_the_Crud_RibbonControlGroup()
		{
			_ribbonControlFactoryMock.Verify(f => f.CreateRibbonControlGroup("TK_Crud"), Times.Once);
		}

		[Test]
		public void It_adds_the_AddButton_to_the_Crud_RibbonControlGroup()
		{
			_crudRibbonControlGroupMock.Verify(g => g.Add(_addButtonMock.Object), Times.Once);
		}

		[Test]
		public void It_adds_the_SaveButton_to_the_Crud_RibbonControlGroup()
		{
			_crudRibbonControlGroupMock.Verify(g => g.Add(_saveButtonMock.Object), Times.Once);
		}

		[Test]
		public void It_adds_the_Crud_RibbonControlGroup_to_the_Crud_RibbonTab_s_RibbonControlGroups()
		{
			_crudRibbonTabRibbonControlGroupsMock.Verify(c => c.Add(_crudRibbonControlGroupMock.Object), Times.Once);
		}

		[Test]
		public void It_adds_the_Crud_RibbonTab_to_the_RibbonViewModel_Tabs()
		{
			_ribbonViewModelTabsMock.Verify(c => c.Add(_crudRibbonTabMock.Object), Times.Once);
		}

		[Test]
		public void It_sets_the_Crud_RibbonTab_as_the_SelectedTab()
		{
			_ribbonViewModelMock.VerifySet(r => r.SelectedTab = _crudRibbonTabMock.Object, Times.Once);
		}
	}

	[TestFixture]
	public class DescribeRegistrationOfAddListViewModel
	{
		private Mock<IListViewModel> _listViewModelMock;
		private CrudGroupController _crudGroupController;

		private Mock<IRibbonButtonControl> _addButtonMock;
		private Mock<IRibbonButtonControl> _saveButtonMock;
		private Mock<IRibbonControlGroup> _crudRibbonControlGroupMock;
		private Mock<IRibbonTab> _crudRibbonTabMock;
		private Mock<IRibbonTabFactory> _ribbonTabFactoryMock;
		private Mock<IRibbonControlFactory> _ribbonControlFactoryMock;
		private Mock<IRibbonViewModel> _ribbonViewModelMock;
		private Action _addButtonExecuteAction;
		private Func<bool> _addButtonCanExecuteFunc;

		[SetUp]
		public void BeforeEach()
		{
			_listViewModelMock = new Mock<IListViewModel>();

			_addButtonMock = new Mock<IRibbonButtonControl>();
			_saveButtonMock = new Mock<IRibbonButtonControl>();

			_addButtonMock.SetupSet(b => b.ExecuteAction = It.IsAny<Action>()).Callback<Action>(a => _addButtonExecuteAction = a);
			_addButtonMock.SetupSet(b => b.CanExecute = It.IsAny<Func<bool>>()).Callback<Func<bool>>(f => _addButtonCanExecuteFunc = f);
			_addButtonMock.Setup(b => b.RaiseCanExecuteChanged()).Verifiable();
			
			_ribbonControlFactoryMock = new Mock<IRibbonControlFactory>();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonButton("TK_Add")).Returns(_addButtonMock.Object).Verifiable();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonButton("TK_Save")).Returns(_saveButtonMock.Object).Verifiable();

			_crudRibbonControlGroupMock = new Mock<IRibbonControlGroup>();
			_crudRibbonControlGroupMock.SetupAllProperties();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonControlGroup("TK_Crud")).Returns(_crudRibbonControlGroupMock.Object).Verifiable();

			_crudRibbonTabMock = new Mock<IRibbonTab>();
			_crudRibbonTabMock.Setup(t => t.RibbonControlGroups).Returns(new Collection<IRibbonControlGroup>());

			_ribbonTabFactoryMock = new Mock<IRibbonTabFactory>();
			_ribbonTabFactoryMock.Setup(f => f.Create("TK_Crud")).Returns(_crudRibbonTabMock.Object);

			_ribbonViewModelMock = new Mock<IRibbonViewModel>();
			_ribbonViewModelMock.Setup(r => r.RibbonTabs).Returns(new Collection<IRibbonTab>()).Verifiable();

			var dependenciesMock = new Mock<ICrudGroupControllerDependencies>();
			dependenciesMock.SetupGet(d => d.RibbonControlFactory).Returns(_ribbonControlFactoryMock.Object);
			dependenciesMock.SetupGet(d => d.RibbonTabFactory).Returns(_ribbonTabFactoryMock.Object);
			dependenciesMock.SetupGet(d => d.RibbonViewModel).Returns(_ribbonViewModelMock.Object);

			_crudGroupController = new CrudGroupController(dependenciesMock.Object);
		}

		[Test]
		public void It_links_IAdd_IListViewModel_to_IRibbonViewModel_s_AddButton()
		{
			// If the IListViewModel implements IAdd interface
			_listViewModelMock.As<IAdd>().Setup(l => l.Add()).Verifiable();
			_listViewModelMock.As<IAdd>().Setup(l => l.CanAdd()).Verifiable();

			_crudGroupController.Register(_listViewModelMock.Object);

			// It gives Add Action to ViewModel's Add Button Command
			_listViewModelMock.As<IAdd>().Verify(l => l.Add(), Times.Never);
			_addButtonExecuteAction();
			_listViewModelMock.As<IAdd>().Verify(l => l.Add(), Times.Once);

			// It gives CanExecute function to ViewModel's Add Button Command
			_listViewModelMock.As<IAdd>().Verify(l => l.CanAdd(), Times.Never);
			_addButtonCanExecuteFunc();
			_listViewModelMock.As<IAdd>().Verify(l => l.CanAdd(), Times.Once);

			// It subscribes RaiseCanExecuteChanged to IListViewModel's CanAddChanged event
			_addButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Never);
			_listViewModelMock.As<IAdd>().Raise(l => l.CanAddChanged += null);
			_addButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
		}

		[Test]
		public void It_disables_IRibbonViewModel_s_AddButton_if_IListViewModel_not_IAdd()
		{
			_crudGroupController.Register(_listViewModelMock.Object);

			Assert.IsFalse(_addButtonCanExecuteFunc());
		}
	}

	[TestFixture]
	public class DescribeRegistrationOfSaveListViewModel
	{
		private Mock<IListViewModel> _listViewModelMock;
		private CrudGroupController _crudGroupController;

		private Mock<IRibbonButtonControl> _addButtonMock;
		private Mock<IRibbonButtonControl> _saveButtonMock;
		private Mock<IRibbonControlGroup> _crudRibbonControlGroupMock;
		private Mock<IRibbonTab> _crudRibbonTabMock;
		private Mock<IRibbonTabFactory> _ribbonTabFactoryMock;
		private Mock<IRibbonControlFactory> _ribbonControlFactoryMock;
		private Mock<IRibbonViewModel> _ribbonViewModelMock;
		private Action _saveButtonExecuteAction;
		private Func<bool> _saveButtonCanExecuteFunc;

		[SetUp]
		public void BeforeEach()
		{
			_listViewModelMock = new Mock<IListViewModel>();

			_addButtonMock = new Mock<IRibbonButtonControl>();
			_saveButtonMock = new Mock<IRibbonButtonControl>();

			_saveButtonMock.SetupSet(b => b.ExecuteAction = It.IsAny<Action>()).Callback<Action>(a => _saveButtonExecuteAction = a);
			_saveButtonMock.SetupSet(b => b.CanExecute = It.IsAny<Func<bool>>()).Callback<Func<bool>>(f => _saveButtonCanExecuteFunc = f);
			_saveButtonMock.Setup(b => b.RaiseCanExecuteChanged()).Verifiable();

			_ribbonControlFactoryMock = new Mock<IRibbonControlFactory>();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonButton("TK_Add")).Returns(_addButtonMock.Object).Verifiable();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonButton("TK_Save")).Returns(_saveButtonMock.Object).Verifiable();

			_crudRibbonControlGroupMock = new Mock<IRibbonControlGroup>();
			_crudRibbonControlGroupMock.SetupAllProperties();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonControlGroup("TK_Crud")).Returns(_crudRibbonControlGroupMock.Object).Verifiable();

			_crudRibbonTabMock = new Mock<IRibbonTab>();
			_crudRibbonTabMock.Setup(t => t.RibbonControlGroups).Returns(new Collection<IRibbonControlGroup>());

			_ribbonTabFactoryMock = new Mock<IRibbonTabFactory>();
			_ribbonTabFactoryMock.Setup(f => f.Create("TK_Crud")).Returns(_crudRibbonTabMock.Object);

			_ribbonViewModelMock = new Mock<IRibbonViewModel>();
			_ribbonViewModelMock.Setup(r => r.RibbonTabs).Returns(new Collection<IRibbonTab>()).Verifiable();

			var dependenciesMock = new Mock<ICrudGroupControllerDependencies>();
			dependenciesMock.SetupGet(d => d.RibbonControlFactory).Returns(_ribbonControlFactoryMock.Object);
			dependenciesMock.SetupGet(d => d.RibbonTabFactory).Returns(_ribbonTabFactoryMock.Object);
			dependenciesMock.SetupGet(d => d.RibbonViewModel).Returns(_ribbonViewModelMock.Object);

			_crudGroupController = new CrudGroupController(dependenciesMock.Object);
		}

		[Test]
		public void It_links_ISave_IListViewModel_to_IRibbonViewModel_s_SaveButton()
		{
			// If the IListViewModel implements IAdd interface
			_listViewModelMock.As<ISave>().Setup(l => l.Save()).Verifiable();
			_listViewModelMock.As<ISave>().Setup(l => l.CanSave()).Verifiable();

			_crudGroupController.Register(_listViewModelMock.Object);

			// It gives Add Action to ViewModel's Add Button Command
			_listViewModelMock.As<ISave>().Verify(l => l.Save(), Times.Never);
			_saveButtonExecuteAction();
			_listViewModelMock.As<ISave>().Verify(l => l.Save(), Times.Once);

			// It gives CanExecute function to ViewModel's Add Button Command
			_listViewModelMock.As<ISave>().Verify(l => l.CanSave(), Times.Never);
			_saveButtonCanExecuteFunc();
			_listViewModelMock.As<ISave>().Verify(l => l.CanSave(), Times.Once);

			// It subscribes RaiseCanExecuteChanged to IListViewModel's CanAddChanged event
			_saveButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Never);
			_listViewModelMock.As<ISave>().Raise(l => l.CanSaveChanged += null);
			_saveButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
		}

		[Test]
		public void It_disables_IRibbonViewModel_s_SaveButton_if_IListViewModel_not_ISave()
		{
			_crudGroupController.Register(_listViewModelMock.Object);

			Assert.IsFalse(_saveButtonCanExecuteFunc());
		}
	}
	[TestFixture]
	public class DescribeListViewModelUnregistration
	{
		private Mock<IListViewModel> _listViewModelMock;
		private CrudGroupController _crudGroupController;

		private Mock<IRibbonButtonControl> _addButtonMock;
		private Action _addButtonExecuteAction;
		private Func<bool> _addButtonCanExecuteFunc;

		private Mock<IRibbonButtonControl> _saveButtonMock;
		private Action _saveButtonExecuteAction;
		private Func<bool> _saveButtonCanExecuteFunc;

		private Mock<IRibbonControlGroup> _crudRibbonControlGroupMock;
		private Mock<IRibbonTab> _crudRibbonTabMock;
		private Mock<IRibbonTabFactory> _ribbonTabFactoryMock;
		private Mock<IRibbonControlFactory> _ribbonControlFactoryMock;
		private Mock<IRibbonViewModel> _ribbonViewModelMock;

		[SetUp]
		public void BeforeEach()
		{
			_listViewModelMock = new Mock<IListViewModel>();

			_addButtonMock = new Mock<IRibbonButtonControl>();
			_addButtonMock.SetupSet(b => b.ExecuteAction = It.IsAny<Action>()).Callback<Action>(a => _addButtonExecuteAction = a);
			_addButtonMock.SetupSet(b => b.CanExecute = It.IsAny<Func<bool>>()).Callback<Func<bool>>(f => _addButtonCanExecuteFunc = f);
			_addButtonMock.Setup(b => b.RaiseCanExecuteChanged()).Verifiable();

			_saveButtonMock = new Mock<IRibbonButtonControl>();
			_saveButtonMock.SetupSet(b => b.ExecuteAction = It.IsAny<Action>()).Callback<Action>(a => _saveButtonExecuteAction = a);
			_saveButtonMock.SetupSet(b => b.CanExecute = It.IsAny<Func<bool>>()).Callback<Func<bool>>(f => _saveButtonCanExecuteFunc = f);
			_saveButtonMock.Setup(b => b.RaiseCanExecuteChanged()).Verifiable();

			_ribbonControlFactoryMock = new Mock<IRibbonControlFactory>();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonButton("TK_Add")).Returns(_addButtonMock.Object).Verifiable();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonButton("TK_Save")).Returns(_saveButtonMock.Object).Verifiable();

			_crudRibbonControlGroupMock = new Mock<IRibbonControlGroup>();
			_crudRibbonControlGroupMock.SetupAllProperties();
			_ribbonControlFactoryMock.Setup(f => f.CreateRibbonControlGroup("TK_Crud")).Returns(_crudRibbonControlGroupMock.Object).Verifiable();

			_crudRibbonTabMock = new Mock<IRibbonTab>();
			_crudRibbonTabMock.Setup(t => t.RibbonControlGroups).Returns(new Collection<IRibbonControlGroup>());

			_ribbonTabFactoryMock = new Mock<IRibbonTabFactory>();
			_ribbonTabFactoryMock.Setup(f => f.Create("TK_Crud")).Returns(_crudRibbonTabMock.Object);

			_ribbonViewModelMock = new Mock<IRibbonViewModel>();
			_ribbonViewModelMock.Setup(r => r.RibbonTabs).Returns(new Collection<IRibbonTab>()).Verifiable();

			var dependenciesMock = new Mock<ICrudGroupControllerDependencies>();
			dependenciesMock.SetupGet(d => d.RibbonControlFactory).Returns(_ribbonControlFactoryMock.Object);
			dependenciesMock.SetupGet(d => d.RibbonTabFactory).Returns(_ribbonTabFactoryMock.Object);
			dependenciesMock.SetupGet(d => d.RibbonViewModel).Returns(_ribbonViewModelMock.Object);

			_crudGroupController = new CrudGroupController(dependenciesMock.Object);

			_listViewModelMock.As<IAdd>().Setup(l => l.Add());
			_listViewModelMock.As<IAdd>().Setup(l => l.CanAdd());

			_listViewModelMock.As<ISave>().Setup(l => l.Save());
			_listViewModelMock.As<ISave>().Setup(l => l.CanSave());

			_crudGroupController.Register(_listViewModelMock.Object);

			_listViewModelMock.As<IAdd>().Verify(l => l.Add(), Times.Never);
			_addButtonExecuteAction();
			_listViewModelMock.As<IAdd>().Verify(l => l.Add(), Times.Once);

			_listViewModelMock.As<ISave>().Verify(l => l.Save(), Times.Never);
			_saveButtonExecuteAction();
			_listViewModelMock.As<ISave>().Verify(l => l.Save(), Times.Once);

			_listViewModelMock.As<IAdd>().Verify(l => l.CanAdd(), Times.Never);
			_addButtonCanExecuteFunc();
			_listViewModelMock.As<IAdd>().Verify(l => l.CanAdd(), Times.Once);

			_listViewModelMock.As<ISave>().Verify(l => l.CanSave(), Times.Never);
			_saveButtonCanExecuteFunc();
			_listViewModelMock.As<ISave>().Verify(l => l.CanSave(), Times.Once);

			_addButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Never);
			_listViewModelMock.As<IAdd>().Raise(l => l.CanAddChanged += null);
			_addButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);

			_saveButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Never);
			_listViewModelMock.As<ISave>().Raise(l => l.CanSaveChanged += null);
			_saveButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
		}

		[Test]
		public void It_sets_ExecuteActions_and_CanExecutes_to_null_for_IAdd_ListViewModel()
		{
			_crudGroupController.UnRegister(_listViewModelMock.Object);

			Assert.IsNull(_addButtonExecuteAction);
			Assert.IsNull(_addButtonCanExecuteFunc);
			Assert.IsNull(_saveButtonExecuteAction);
			Assert.IsNull(_saveButtonCanExecuteFunc);
		}

		[Test]
		public void It_unregisters_RaiseCanExecuteChanged_from_ListViewModel_s_CanCrudChanged_event()
		{
			_crudGroupController.UnRegister(_listViewModelMock.Object);

			_addButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
			_listViewModelMock.As<IAdd>().Raise(l => l.CanAddChanged += null);
			_addButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);

			_saveButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
			_listViewModelMock.As<ISave>().Raise(l => l.CanSaveChanged += null);
			_saveButtonMock.Verify(b => b.RaiseCanExecuteChanged(), Times.Once);
		}
	}
}
