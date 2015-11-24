using System;

namespace SolidFrame.Core.Interfaces
{
	public interface IListViewModel
	{
		Guid Id { get; }
		string Title { get; }
	}
}