using System;

namespace SolidFrame.Core.Interfaces.General
{
	public interface IListViewModel
	{
		Guid Id { get; }
		string Title { get; }
	}
}