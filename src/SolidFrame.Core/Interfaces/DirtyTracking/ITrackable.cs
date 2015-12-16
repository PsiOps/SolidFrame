using SolidFrame.Core.Interfaces.General;
using System.ComponentModel;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITrackable : INotifyPropertyChanged, IHaveId
	{
	}
}