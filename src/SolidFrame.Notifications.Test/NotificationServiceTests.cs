using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces.Notifications;
using SolidFrame.Notifications.Logics;
using SolidFrame.Notifications.Types;
using SolidFrame.Notifications.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Notifications.Test
{
	[TestFixture]
	public class DescribeNotificationAdding
	{
		private INotificationService _notificationService;
		private Mock<INotificationListViewModel> _notificationListViewModelMock;
		private INotificationViewModel _notificationAdded;
		private Mock<ICollection<INotificationViewModel>> _itemsSourceMock;
		private Mock<INotificationFactory> _notificationFactoryMock;
		private readonly Guid _subjectId = Guid.NewGuid();
		private readonly Guid _notificationId = Guid.NewGuid();
		private const string SubjectName = "TestName";
		private const string Message = "TestMessage";

		[SetUp]
		public void BeforeEach()
		{
			var notificationMock = new Mock<INotificationViewModel>();
			notificationMock.SetupGet(n => n.Id).Returns(_notificationId);
			notificationMock.SetupGet(n => n.SubjectId).Returns(_subjectId);
			notificationMock.SetupGet(n => n.SubjectName).Returns(SubjectName);
			notificationMock.SetupGet(n => n.Message).Returns(Message);

			_itemsSourceMock = new Mock<ICollection<INotificationViewModel>>();
			_itemsSourceMock.Setup(i => i.Add(It.IsAny<INotificationViewModel>())).Callback<INotificationViewModel>(m =>
			{
				_notificationAdded = m;
			});

			_notificationListViewModelMock = new Mock<INotificationListViewModel>();
			_notificationListViewModelMock.SetupGet(l => l.ItemsSource).Returns(_itemsSourceMock.Object).Verifiable();

			_notificationFactoryMock = new Mock<INotificationFactory>();
			_notificationFactoryMock.Setup(f => f.Create(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>())).Returns(notificationMock.Object).Verifiable();

			var notificationServiceDependenciesMock = new Mock<INotificationServiceDependencies>();
			notificationServiceDependenciesMock.SetupGet(d => d.NotificationListViewModel)
				.Returns(_notificationListViewModelMock.Object);

			notificationServiceDependenciesMock.SetupGet(d => d.NotificationFactory).Returns(_notificationFactoryMock.Object);

			_notificationService = new NotificationService(notificationServiceDependenciesMock.Object);
		}

		[Test]
		public void It_adds_a_notification_to_the_notification_list()
		{
			
			_notificationService.AddNotification(_notificationId, _subjectId, SubjectName, Message);

			_notificationFactoryMock.Verify(f => f.Create(_notificationId, _subjectId, SubjectName, Message));

			_notificationListViewModelMock.VerifyGet(l => l.ItemsSource, Times.Once);
			_itemsSourceMock.Verify(i => i.Add(_notificationAdded));

			Assert.AreEqual(_notificationId, _notificationAdded.Id);
			Assert.AreEqual(_subjectId, _notificationAdded.SubjectId);
			Assert.AreEqual(SubjectName, _notificationAdded.SubjectName);
			Assert.AreEqual(Message, _notificationAdded.Message);
		}
	}
	
	[TestFixture]
	public class DescribeNotificationRemoval
	{
		private INotificationService _notificationService;
		private readonly Guid _subjectId = Guid.NewGuid();
		private readonly Guid _notificationId = Guid.NewGuid();
		private Mock<INotificationListViewModel> _notificationListViewModelMock;
		private INotificationViewModel _notificationRemoved;
		private Mock<ICollection<INotificationViewModel>> _itemsSourceMock;

		[SetUp]
		public void BeforeEach()
		{
			var notificationMock = new Mock<INotificationViewModel>();
			notificationMock.SetupGet(n => n.Id).Returns(_notificationId);
			notificationMock.SetupGet(n => n.SubjectId).Returns(_subjectId);

			_itemsSourceMock = new Mock<ICollection<INotificationViewModel>>();
			_itemsSourceMock.Setup(i => i.Remove(It.IsAny<INotificationViewModel>())).Callback<INotificationViewModel>(m =>
			{
				_notificationRemoved = m;
			});
			_itemsSourceMock.Setup(i => i.GetEnumerator())
				.Returns(new Collection<INotificationViewModel> {notificationMock.Object}.GetEnumerator);

				_notificationListViewModelMock = new Mock<INotificationListViewModel>();
			_notificationListViewModelMock.SetupGet(l => l.ItemsSource).Returns(_itemsSourceMock.Object).Verifiable();

			var notificationServiceDependenciesMock = new Mock<INotificationServiceDependencies>();
			notificationServiceDependenciesMock.SetupGet(d => d.NotificationListViewModel)
				.Returns(_notificationListViewModelMock.Object);

			_notificationService = new NotificationService(notificationServiceDependenciesMock.Object);
		}

		[Test]
		public void It_removes_a_notification_if_present()
		{
			_notificationService.TryRemoveNotification(_notificationId, _subjectId);

			_notificationListViewModelMock.VerifyGet(l => l.ItemsSource, Times.Exactly(2));

			_itemsSourceMock.Verify(i => i.Remove(_notificationRemoved), Times.Once);

			Assert.AreEqual(_notificationId, _notificationRemoved.Id);
			Assert.AreEqual(_subjectId, _notificationRemoved.SubjectId);
		}

		[Test]
		public void It_does_nothing_if_notification_is_not_present()
		{
			_notificationService.TryRemoveNotification(Guid.NewGuid(), _subjectId);

			_notificationListViewModelMock.VerifyGet(l => l.ItemsSource, Times.Once);

			_itemsSourceMock.Verify(i => i.Remove(_notificationRemoved), Times.Never);
		}
	}
}
