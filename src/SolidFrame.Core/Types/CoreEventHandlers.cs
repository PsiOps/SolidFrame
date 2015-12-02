
namespace SolidFrame.Core.Types
{
	public delegate void CanCrudChangedHandler();
	public delegate void ValidationTriggerHandler<T>(T type, string propertyName);
}
