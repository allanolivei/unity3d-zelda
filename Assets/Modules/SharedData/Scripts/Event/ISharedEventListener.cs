

namespace SharedData
{
	public interface ISharedEventListener
	{
		void DispatchHandler(SharedEvent e);
	}
}